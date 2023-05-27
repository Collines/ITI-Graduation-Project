import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from 'src/app/Interfaces/Department';
import { Doctor } from 'src/app/Interfaces/Doctor';
import { User } from 'src/app/Interfaces/User/user';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentService } from 'src/app/Services/department.service';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-department-detail',
  templateUrl: './department-detail.component.html',
  styleUrls: ['./department-detail.component.css'],
})
export class DepartmentDetailComponent implements OnInit {
  department: Department = {
    id: 0,
    title: '',
    description: '',
  };
  doctors: Doctor[] = [];
  user: User = {
    id: 0,
    fullName: '',
    accessToken: '',
    refreshToken: '',
    expiration: 0,
  };
  constructor(
    private Route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private departmentService: DepartmentService,
    private doctorService: DoctorsService
  ) {
    this.department.id = Route.snapshot.params['id'];
    accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
      error: (er) => {
        router.navigate(['/login']);
      },
    });
    departmentService
      .GetById(this.department.id, this.user.accessToken)
      .subscribe({
        next: (dept) => {
          if (dept) this.department = dept;
        },
      });
  }
  ngOnInit(): void {
    this.doctorService
      .GetDepartmentDoctors(this.department.id, this.user.accessToken)
      .subscribe({
        next: (doctors) => (this.doctors = doctors),
      });
  }
}
