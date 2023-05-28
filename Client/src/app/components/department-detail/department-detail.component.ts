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
    this.doctorService.GetDepartmentDoctors(this.department.id).subscribe({
      next: (doctors) => {
        this.doctors = doctors;
        this.SearchResult = doctors;
      },
    });
  }
  department: Department = {
    id: 0,
    title: '',
    description: '',
    numberOfBeds: 0,
  };
  doctors: Doctor[] = [];
  user: User = {
    id: 0,
    fullName: '',
    accessToken: '',
    refreshToken: '',
    expiration: 0,
  };
  SearchResult: Doctor[] = [];
  searchQuery: string = '';
  searchTimer: any;

  onSearch(): void {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.performSearch();
    }, 300);
  }

  performSearch(): void {
    this.SearchResult = [];
    if (this.searchQuery.length > 0) {
      if (this.doctors.length > 0) {
        this.doctors.forEach((item: Doctor) => {
          if (
            item.firstName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
            item.lastName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase())
          ) {
            this.SearchResult.push(item);
          }
        });
      }
    } else {
      this.SearchResult = this.doctors;
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this.searchTimer);
  }
}
