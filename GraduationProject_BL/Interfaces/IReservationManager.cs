using GraduationProject_BL.DTO.ReservationDTOs;
using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;

namespace GraduationProject_BL.Interfaces
{
    public interface IReservationManager
    {
        public Task<List<ReservationDTO>> GetAllAsync(string lang);
        public Task<ReservationDTO?> GetByIdAsync(int id, string lang);
        public Task<bool> InsertAsync(Reservation item);
        public Task UpdateAsync(int id, Reservation item);
        public Task DeleteAsync(int id);
        public Task<List<ReservationDTO>?> GetAllPatientReservationsAsync(string lang, int patientId);
        public Task CancelReservation(int id);
        public Task ChangeReservationStatus(int id, ReservationStatus status);


    }
}
