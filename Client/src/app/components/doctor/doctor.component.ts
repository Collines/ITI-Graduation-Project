import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Doctor } from 'src/app/Interfaces/Doctor';
import { User } from 'src/app/Interfaces/User/user';
import { Reservation } from 'src/app/Interfaces/reservation';
import { AccountService } from 'src/app/Services/account.service';
import { ReservationService } from 'src/app/Services/reservation.service';
declare var window: any;

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
})
export class DoctorComponent implements OnInit {
  constructor(
    private accountService: AccountService,
    private router: Router,
    private reservationService: ReservationService
  ) {}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }
  user: User = {
    id: 0,
    fullName: '',
    accessToken: '',
    refreshToken: '',
    expiration: 0,
  };
  modal: any;
  reservation: any = {
    id: 0,
    dateTime: '',
    queue: 2,
    status: 1,
    doctorId: 0,
    patientId: 0,
  };
  @Input() doctor: Doctor = {
    id: 0,
    firstName: 'string',
    lastName: 'string',
    gender: 1,
    title: 'string',
    bio: 'string',
    departmentId: 0,
    departmentTitle: 'string',
    image: 'string',
  };
  openModal(m: any) {
    this.modal = new window.bootstrap.Modal(m);
    this.modal.show();
  }
  closeModal() {
    this.modal.hide();
  }
  get Logged(): boolean {
    return this.user.refreshToken.length >0;
  }
  isSubmit = false;
  error = false;
  requestError = false;
  OnSubmit(/*time: any, */ date: any) {
    this.isSubmit = false;
    let dt1 = new Date(date.value).setHours(8);
    let dt = new Date(dt1);
    if (dt.getTime() > new Date().getTime()) {
      this.error = false;
      this.reservation.dateTime = `${dt.toLocaleString()}`;
      this.reservation.doctorId = this.doctor.id;
      this.reservation.patientId = this.user.id;
      this.reservationService;
      console.log(this.reservation);
      this.reservationService
        .AddReservation(this.reservation, this.user.accessToken)
        .subscribe({
          next: (e) => {
            this.isSubmit = true;
            this.requestError = false;
            console.log('reservation added!');
            setTimeout((e: any) => {
              this.closeModal();
            }, 1000);
          },
          error: () => {this.isSubmit = true;
            this.requestError = true;}
        });
    } else {
      this.isSubmit = true;
      this.error = true;
    }
  }
}
