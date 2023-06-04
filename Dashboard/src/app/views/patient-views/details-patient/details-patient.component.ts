import { PatientsService } from 'src/app/services/patients.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { Gender } from 'src/app/enums/gender';

@Component({
  selector: 'app-details-patient',
  templateUrl: './details-patient.component.html',
  styleUrls: ['./details-patient.component.scss']
})
export class DetailsPatientComponent implements OnInit {

  Patient:any;
  ID:number;
  Gender = Gender;
  ToVisit:any;
  Visited:any;
  Cancelled:any;

  constructor(
    private PatientsService:PatientsService,
    private Route:ActivatedRoute,
    private Router:Router,
    private AccountService:AccountService)
    {
      this.ID = this.Route.snapshot.params["id"];
    }

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.PatientsService.GetByID(this.ID).subscribe({
      next: data => {
        this.Patient = data;
        this.ToVisit = this.Patient.reservations.filter( (x:any) => x.status == 1).length;
        this.Visited = this.Patient.reservations.filter( (x:any) => x.status == 2).length;
        this.Cancelled = this.Patient.reservations.filter( (x:any) => x.status == 3).length;
      },
      error: err => console.log(err)
    })
  }

  DeletePatient(value:number){
    if(confirm(`Do You Want To Delete Patient No. ${value}`))
    {
      this.PatientsService.Delete(value).subscribe({
        next: () => {alert(`Patient No.${value} Has Been Deleted`);
                      this.Router.navigate(['/Patients'])},
        error: err => console.log(err)
      })
    }
  }

}
