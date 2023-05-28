import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/Interfaces/Department';
import { DepartmentService } from 'src/app/Services/department.service';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private doctorServices: DoctorsService,
    private departmentServices: DepartmentService
  ) {}

  ngOnInit(): void {
    this.doctorServices.GetAllDoctors().subscribe({
      next: (doctors) => {
        if (doctors) {
          let numberOfDoctors = doctors.length;
          if (numberOfDoctors > 0) {
            let doctorCountStop = setInterval(() => {
              this.doctorCount++;
              if (this.doctorCount >= numberOfDoctors) {
                clearInterval(doctorCountStop);
              }
            }, 22);
          }
        }
      },
    });

    this.departmentServices.GetDepartments().subscribe({
      next: (departments) => {
        if (departments) {
          let numberOfBeds = 0;
          departments.forEach((item: Department) => {
            numberOfBeds += item.numberOfBeds;
          });
          if (numberOfBeds > 0) {
            let bedCountStop = setInterval(() => {
              this.bedCount++;
              if (this.bedCount >= numberOfBeds) {
                clearInterval(bedCountStop);
              }
            }, 13);
          }
        }
      },
    });
  }

  doctorCount: number = 0;
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
