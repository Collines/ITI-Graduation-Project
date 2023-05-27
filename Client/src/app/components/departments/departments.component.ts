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
  ngOnInit(): void {
    this.accountServices.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.departmentServices.GetDepartments(user.accessToken).subscribe({
            next: (res) => {
              this.departments = res;
              this.SearchResult = res;
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
  departments: Department[] = [];
  SearchResult: Department[] = [];
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
      if (this.departments.length > 0) {
        this.departments.forEach((item: Department) => {
          if (
            item.title
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase())
          ) {
            this.SearchResult.push(item);
          }
        });
      }
    } else {
      this.SearchResult = this.departments;
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this.searchTimer);
  }
}
