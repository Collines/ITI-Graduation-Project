using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_BL.DTO.ReservationDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject_BL.Managers
{
    public class ReservationManager : IReservationManager
    {
        private readonly IRepository<Reservation> repository;
        private readonly IPatientManager patients;
        private readonly IDoctorManager doctors;

        public ReservationManager(IRepository<Reservation> _repository,IPatientManager _patients, IDoctorManager _doctors)
        {
            repository = _repository;
            patients = _patients;
            doctors = _doctors;
        }

        public async Task<List<ReservationDTO>> GetAllAsync(string lang)
        {
            var reservations = await repository.GetAllAsync();

            var reservationsDTO = new List<ReservationDTO>();

            foreach (var reservation in reservations)
            {
                var patientDTO = await patients.GetByIdAsync(reservation.PatientId,lang);
                var doctorDTO = await doctors.GetByIdAsync(reservation.DoctorId, lang);

                ReservationDTO dto = new ReservationDTO() 
                {
                    Id = reservation.Id,
                    DateTime = reservation.DateTime,
                    Queue = reservation.Queue,
                    PatientId = reservation.PatientId,
                    Patient = patientDTO,
                    DoctorId = reservation.DoctorId,
                    Doctor = doctorDTO,
                    Status = reservation.Status,
                };

                reservationsDTO.Add(dto);
            }

            return reservationsDTO;
        }

        public async Task<ReservationDTO?> GetByIdAsync(int id, string lang)
        {
            var reservations = await repository.GetAllAsync();

            if(reservations != null)
            {
                var reservation = reservations.Find(x => x.Id == id);
                if(reservation != null)
                {
                    var patientDTO = await patients.GetByIdAsync(reservation.PatientId, lang);
                    var doctorDTO = await doctors.GetByIdAsync(reservation.DoctorId, lang);

                    ReservationDTO dto = new ReservationDTO()
                    {
                        Id = reservation.Id,
                        DateTime = reservation.DateTime,
                        Queue = reservation.Queue,
                        PatientId = reservation.PatientId,
                        Patient = patientDTO,
                        DoctorId = reservation.DoctorId,
                        Doctor = doctorDTO,
                        Status = reservation.Status
                    };

                    return dto;
                }   
            }

            return null;   
        }

        public async Task InsertAsync(Reservation item)
        {
            var reservations = await GetAllAsync("en");
            var QueueNum = reservations.Where(r => r.DoctorId == item.DoctorId && r.DateTime == item.DateTime).Count();
            item.Queue = QueueNum + 1;
            await repository.InsertAsync(item);
        }

        public async Task UpdateAsync(int id, Reservation item)
        {
            var reservations = await repository.GetAllAsync();

            if (reservations != null)
            {
                var reservation = reservations.Find(x => x.Id == id);
                if (reservation != null)
                {
                    reservation.DateTime = item.DateTime;
                    reservation.Queue = item.Queue;
                    reservation.PatientId = item.PatientId;
                    reservation.DoctorId = item.DoctorId;
                    reservation.Status = item.Status;

                    await repository.UpdateAsync(reservation.Id, reservation);
                }
            }
        }

        public async Task<List<ReservationDTO>?> GetAllPatientReservationsAsync(string lang, int patientId)
        {
            var reservations = await repository.GetAllAsync();
            var patientReservations = reservations.Where(r => r.PatientId == patientId).ToList();

            if (patientReservations != null && patientReservations.Count > 0)
            {
                var reservationsDTO = new List<ReservationDTO>();

                foreach (var reservation in patientReservations)
                {
                    var patientDTO = await patients.GetByIdAsync(reservation.PatientId, lang);
                    var doctorDTO = await doctors.GetByIdAsync(reservation.DoctorId, lang);

                    ReservationDTO dto = new ReservationDTO()
                    {
                        Id = reservation.Id,
                        DateTime = reservation.DateTime,
                        Queue = reservation.Queue,
                        Doctor = doctorDTO,
                        Status = reservation.Status,
                        PatientId = reservation.PatientId,
                        DoctorId = reservation.DoctorId,
                    };

                    reservationsDTO.Add(dto);
                }

                return reservationsDTO;
            }
            else
                return null;
        }

        public async Task CancelReservation(int id)
        {
            var reservations = await repository.GetAllAsync();

            if (reservations != null)
            {
                var reservation = reservations.Find(x => x.Id == id);
                if (reservation != null)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                    await repository.UpdateAsync(reservation.Id, reservation);
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
        }
    }
}
