import { ReservationStatus } from 'src/app/enums/reservationStatus';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { ReservationsService } from 'src/app/services/reservations.service';

@Component({
  selector: 'app-all-reservations',
  templateUrl: './all-reservations.component.html',
  styleUrls: ['./all-reservations.component.scss']
})
export class AllReservationsComponent implements OnInit {

  Reservations:any;
  ReservationStatus = ReservationStatus;

  constructor(
    private ReservationsService:ReservationsService,
    private Router:Router,
    private AccountService:AccountService){}

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.ReservationsService.getAll().subscribe({
      next: data => {this.Reservations = data;},
      error: err => console.log(err)
    })
  }

}
