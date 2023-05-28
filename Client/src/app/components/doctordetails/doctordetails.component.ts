import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { Location } from '@angular/common';
import { Doctor } from 'src/app/Interfaces/Doctor';

@Component({
  selector: 'app-doctordetailss',
  templateUrl: './doctordetails.component.html',
  styleUrls: ['./doctordetails.component.css'],
})
export class DoctordetailsComponent implements OnInit {
  ID: number;
  Doctor: Doctor = {
    id: 0,
    firstName: '',
    lastName: '',
    gender: 1,
    title: '',
    bio: '',
    departmentId: 0,
    departmentTitle: '',
    image: '',
  };
  constructor(
    private DoctorsService: DoctorsService,
    private Route: ActivatedRoute,
    private router: Router,
    private myAccountService: AccountService,
    private _location: Location
  ) {
    this.ID = this.Route.snapshot.params['id'];
  }
  ngOnInit() {
    this.DoctorsService.GetById(this.ID).subscribe({
      next: (data) => {
        this.Doctor = data;
        console.log(data);
      },
      error: (err) => console.log(err),
    });
  }
  returnBack() {
    this._location.back();
  }
}
