import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/Interfaces/Department';
import { DepartmentService } from 'src/app/Services/department.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private departmentServices: DepartmentService) {}

  ngOnInit(): void {
    this.departmentServices.GetDepartments().subscribe({
      next: (departments) => {
        if (departments) {
          let numberOfBeds = 0;
          departments.forEach((item: Department) => {
            numberOfBeds += item.numberOfBeds;
          });
          let bedCountStop = setInterval(() => {
            this.bedCount++;
            if (this.bedCount == numberOfBeds) {
              clearInterval(bedCountStop);
            }
          }, 13);
        }
      },
    });
  }

  doctorCount: number = 0;
  doctorCountStop: any = setInterval(() => {
    this.doctorCount++;
    if (this.doctorCount == 300) {
      clearInterval(this.doctorCountStop);
    }
  }, 22);

  patientCount: number = 0;
  patientCountStop: any = setInterval(() => {
    this.patientCount += 2;
    if (this.patientCount == 1040) {
      clearInterval(this.patientCountStop);
    }
  }, 12);

  bedCount: number = 0;

  appointmentCount: number = 0;
  appointmentCountStop: any = setInterval(() => {
    this.appointmentCount += 5;
    if (this.appointmentCount == 2000) {
      clearInterval(this.appointmentCountStop);
    }
  }, 15);
}
