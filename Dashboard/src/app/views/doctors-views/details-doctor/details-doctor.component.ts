import { DoctorsService } from 'src/app/services/doctors.service';
import { Router, ActivatedRoute} from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Gender } from 'src/app/enums/gender';
import { AccountService } from 'src/app/services/account.service';
@Component({
  selector: 'app-details-doctor',
  templateUrl: './details-doctor.component.html',
  styleUrls: ['./details-doctor.component.scss']
})
export class DetailsDoctorComponent implements OnInit {

  constructor(
    private DoctorsService:DoctorsService,
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

    this.DoctorsService.GetById(this.ID).subscribe({
      next: data => {this.Doctor = data;
      console.log(data)},
      error: err => console.log(err)
    })
  }
  ID:any;
  Doctor:any;
  Gender=Gender;

  DeleteDoctor(value:number){
    if(confirm(`Do You Want To Delete Doctor No. ${value}`))
    {
      this.DoctorsService.DeleteDoctor(value).subscribe({
        next: () => {alert(`Doctor No.${value} Has Been Deleted`);
                      this.Router.navigate(['/Doctors'])},
        error: err => console.log(err)
      })
    }
  }

}
