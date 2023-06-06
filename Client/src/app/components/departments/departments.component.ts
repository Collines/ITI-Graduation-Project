import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Department } from 'src/app/Interfaces/Department';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentService } from 'src/app/Services/department.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css'],
})
export class DepartmentsComponent implements OnInit {

   langauge = localStorage.getItem('language');
  constructor(
    private departmentServices: DepartmentService,
    private accountServices: AccountService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.departmentServices.GetDepartments().subscribe({
      next: (res) => {
        this.departments = res;
        this.SearchResult = res;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
  departments: Department[] = [];
  SearchResult: Department[] = [];
  searchQuery: string = '';
  searchTimer: any;
  direction = document.dir;

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
