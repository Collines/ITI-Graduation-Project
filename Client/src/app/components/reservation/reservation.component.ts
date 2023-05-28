import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationStatus } from 'src/app/Enums/ReservationStatus';
import { User } from 'src/app/Interfaces/User/user';
import { Reservation } from 'src/app/Interfaces/reservation';
import { AccountService } from 'src/app/Services/account.service';
import { ReservationService } from 'src/app/Services/reservation.service';
declare var window: any;

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css'],
})
export class ReservationComponent implements OnInit {
  constructor(
    private reservationService: ReservationService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) this.User = user;
        else this.router.navigate(['/login']);
      },
    });
  }
  modal: any;
  isSubmitted = false;
  error = false;
  User: User = {
    id: 0,
    fullName: '',
    accessToken: '',
    refreshToken: '',
    expiration: 0,
  };
  @Input() reservation: Reservation = {
    id: 0,
    dateTime: '',
    queue: 1,
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
  openModal(m: any) {
    this.modal = new window.bootstrap.Modal(m);
    this.modal.show();
  }
  closeModal() {
    this.modal.hide();
  }
  CancelReservation() {
    this.reservationService
      .CancelReservation(
        this.reservation.id,
        this.reservation,
        this.User?.accessToken
      )
      .subscribe({
        next: (res) => {
          this.error = false;
          this.isSubmitted = true;
        },
        complete: () => {
          setTimeout(() => {
            this.closeModal();
            this.reservation.status = 3;
          }, 500);
        },
        error: (e) => {
          this.isSubmitted = true;
          this.error = true;
          console.log(e);
        },
      });
  }
}
