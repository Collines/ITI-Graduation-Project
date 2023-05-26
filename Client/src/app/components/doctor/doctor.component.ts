import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Doctor } from 'src/app/Interfaces/Doctor';
import { AccountService } from 'src/app/Services/account.service';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
})
export class DoctorComponent {
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
  constructor(
    private doctorService: DoctorsService,
    private accountServices: AccountService,
    private router: Router
  ) {} //
}
