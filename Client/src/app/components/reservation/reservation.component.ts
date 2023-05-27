import { Component, Input } from '@angular/core';
import { ReservationStatus } from 'src/app/Enums/ReservationStatus';
import { Reservation } from 'src/app/Interfaces/reservation';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css'],
})
export class ReservationComponent {
  @Input() reservation: Reservation = {
    id: 0,
    dateTime: '',
    queue: 0,
    status: 1,
    doctor: {
      id: 0,
      firstName: '',
      lastName: '',
      gender: 1,
      title: '',
      bio: '',
      departmentId: 1,
      departmentTitle: '',
      image: '',
    },
    patientId: 0,
    doctorId: 0,
  };
  get reservationStatus() {
    return ReservationStatus[this.reservation.status];
  }
  get reservationTime() {
    let date = new Date(this.reservation.dateTime);
    return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  }
}
