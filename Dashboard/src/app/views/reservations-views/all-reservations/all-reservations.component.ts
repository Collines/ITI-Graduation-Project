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
  SearchResult: any;

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
      next: data => {this.Reservations = data;
        this.SearchResult = data;
      },
      error: err => console.log(err)
    })
  }

  ChangeReservationStatus(id:number,reservationStatus:any){

      let formData: FormData = new FormData();
      formData.append('status', reservationStatus);

      this.ReservationsService.ChangeReservationStatus(id, formData).subscribe({
        next: ()=>{this.Reservations.find((x:any) => x.id === id).status = reservationStatus;
      },
        error: ()=>{alert(`Error happend reservation status did't change`);
      }
    });
  }

  FilterByDate(element:any){
    this.SearchResult = [];

    if(element.target.value){
      if (this.Reservations.length > 0) {
        this.Reservations.forEach((item: any) => {
          if (new Date(item.dateTime).toISOString().split('T')[0] === element.target.value) {
            this.SearchResult.push(item);
          }
        });
      }
    }
    else{
      this.SearchResult = this.Reservations;
    }
  }
}
