import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Department } from 'src/app/Interfaces/Department';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentService } from 'src/app/Services/department.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css'],
})
export class DepartmentsComponent implements OnInit {
  constructor(
    private departmentServices: DepartmentService,
    private accountServices: AccountService,
    private router: Router
  ) {}
  departments: Department[] = [];
  ngOnInit(): void {
    this.accountServices.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.departmentServices.GetDepartments(user.accessToken).subscribe({
            next: (res) => {
              this.departments = res;
            },
            error: (e) => {
              console.log(e);
            },
          });
        } else this.router.navigate(['/login']);
      },
      error: (error) => {
        this.router.navigate(['/login']);
      },
    });
  }
}
