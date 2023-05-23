import { Component, OnInit } from "@angular/core";

import { DoctorsService } from "src/app/services/doctors.service";
import { DepartmentsService } from "src/app/services/departments.service";
import { PatientsService } from "src/app/services/patients.service";
import { ReservationsService } from "src/app/services/reservations.service";

interface ISearchData {
  id: number;
  label: string;
  value: string;
  route: string;
}

@Component({
  templateUrl: "dashboard.component.html",
  styleUrls: ["dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  Departments: any = [];
  Doctors: any = [];
  Patients: any = [];
  Reservations: any = [];

  searchQuery: string = "";
  private searchTimer: any;

  SearchResult: ISearchData[] = [];

  constructor(
    private DepartmentsService: DepartmentsService,
    private DoctorsService: DoctorsService,
    private PatientsService: PatientsService,
    private ReservationsService: ReservationsService
  ) {}

  ngOnInit(): void {
    this.DepartmentsService.GetAllDepartments().subscribe({
      next: (data) => (this.Departments = data),
      error: (err) => console.log(err),
    });

    this.DoctorsService.GetAllDoctors().subscribe({
      next: (data) => (this.Doctors = data),
      error: (err) => console.log(err),
    });

    this.PatientsService.GetAll().subscribe({
      next: (data) => (this.Patients = data),
      error: (err) => console.log(err),
    });

    this.ReservationsService.getAll().subscribe({
      next: (data) => (this.Reservations = data),
      error: (err) => console.log(err),
    });
  }

  onSearch(): void {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.performSearch();
    }, 300);
  }

  performSearch(): void {
    this.SearchResult = [];

    if (this.searchQuery.length > 0) {
      if (this.Departments.length > 0) {
        this.Departments.forEach((item: any) => {
          if (
            item.title
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase())
          ) {
            let temp: ISearchData = {
              id: item.id,
              label: "Department",
              value: item.title,
              route: "Departments",
            };
            this.SearchResult.push(temp);
          }
        });
      }

      if (this.Doctors.length > 0) {
        this.Doctors.forEach((item: any) => {
          if (
            item.firstName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
            item.lastName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase())
          ) {
            let temp: ISearchData = {
              id: item.id,
              label: "Doctor",
              value: `${item.firstName} ${item.lastName}`,
              route: "Doctors",
            };
            this.SearchResult.push(temp);
          }
        });
      }

      if (this.Patients.length > 0) {
        this.Patients.forEach((item: any) => {
          if (
            item.fullName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase())
          ) {
            let temp: ISearchData = {
              id: item.id,
              label: "Patient",
              value: item.fullName,
              route: "Patients",
            };
            this.SearchResult.push(temp);
          }
        });
      }
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this.searchTimer);
  }
}
