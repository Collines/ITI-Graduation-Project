import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from 'src/app/Interfaces/Department';
import { Doctor } from 'src/app/Interfaces/Doctor';
import { User } from 'src/app/Interfaces/User/user';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentService } from 'src/app/Services/department.service';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css'],
})
export class DepartmentComponent implements OnInit {
  constructor(
    private Route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
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
  }
  ngOnInit(): void {
    this.doctorService
      .GetDepartmentDoctors(this.department.id, this.user.accessToken)
      .subscribe({
        next: (doctors) => (this.doctors = doctors),
      });
  }

  @Input() department: Department = {
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
}
