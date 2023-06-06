import { DepartmentsService } from './../../../services/departments.service';
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
  DateFilterResult: any;
  NoData:boolean = false;
  ErrorMessage:any;
  Departments:any

  constructor(
    private ReservationsService:ReservationsService,
    private Router:Router,
    private AccountService:AccountService,
    private DepartmentsService:DepartmentsService){

      this.DepartmentsService.GetAllDepartments().subscribe({
        next: data => this.Departments = data,
        error: err => console.log(err)
      })
    }

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.ReservationsService.getAll().subscribe({
      next: data => {this.Reservations = data;
        this.SearchResult = data;
        this.DateFilterResult = data
        this.NoData = false;
        if(!this.Reservations.length){
          this.NoData = true;
          this.ErrorMessage = "No Reservations Found"
        }
      },
      error: err => {
        this.NoData = true;
        this.ErrorMessage = "Can't Connect to Server"
      }
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

  FilterByDate(element:any,departmentSelector:HTMLSelectElement){
    this.SearchResult = [];
    departmentSelector.selectedIndex = 0;
    if(element.target.value){
      if (this.Reservations.length > 0) {
        this.Reservations.forEach((item: any) => {
          if (new Date(item.dateTime).toISOString().split('T')[0] === element.target.value) {
            this.SearchResult.push(item);
          }
          this.DateFilterResult = this.SearchResult;
        });
      }
    }
    else{
      this.SearchResult = this.Reservations;
      this.DateFilterResult = this.Reservations;
    }
  }

  FilterByDepartment(element:any){
    let temp:any =  [];

    if(element.target.value){
      if(element.target.value == "all"){
        this.SearchResult = this.DateFilterResult;
      }
      else{
        if (this.DateFilterResult.length > 0) {
          this.DateFilterResult.forEach((item: any) => {
            if (item.doctor.departmentId == element.target.value) {
              temp.push(item);
            }
            this.SearchResult = temp;
          });
        }
      }
    }
  }
}
