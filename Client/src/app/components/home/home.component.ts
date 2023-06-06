import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/Interfaces/Department';
import { CampImageService } from 'src/app/Services/camp-image.service';
import { DepartmentService } from 'src/app/Services/department.service';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { PatientsService } from 'src/app/Services/patients.service';
import { ReservationService } from 'src/app/Services/reservation.service';
import { ArticleService } from './../../Services/article.service';
import { Article } from 'src/app/Interfaces/Article';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private doctorServices: DoctorsService,
    private patientServices: PatientsService,
    private departmentServices: DepartmentService,
    private reservationServices: ReservationService,
    private imageServices: CampImageService,
    private ArticleService:ArticleService,
    private Route: ActivatedRoute,
    private router: Router,
  ) {}
  Images: any;
  showImages: boolean = false;

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

    this.patientServices.GetPatientsCount().subscribe({
      next: (numberOfPatients) => {
        if (numberOfPatients) {
          if (numberOfPatients > 0) {
            let patientCountStop = setInterval(() => {
              this.patientCount++;
              if (this.patientCount == numberOfPatients) {
                clearInterval(patientCountStop);
              }
            }, 22);
          }
        }
      },
    });

    this.imageServices.GetImages().subscribe({
      next: (data) => {
        this.Images = data.map((i) => {
          i.image = '/' + i.image.replaceAll('\\', '/');
          return i.image;
        });
        this.showImages = data.length >= 6;
      },
      error: (err) => console.log(err),
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

    this.reservationServices.getReservationsCount().subscribe({
      next: (numberOfReservations) => {
        if (numberOfReservations) {
          if (numberOfReservations > 0) {
            let appointmentCountStop = setInterval(() => {
              this.appointmentCount++;
              if (this.appointmentCount == numberOfReservations) {
                clearInterval(appointmentCountStop);
              }
            }, 15);
          }
        }
      },
    });
    this.ArticleService.getAll().subscribe({
      next:res=> {
        if(res) {
          if(res.length>3){
            for(let i=0; i<3;i++) {
              this.news.push(res[i])
            }
          } else {
            this.news = res;
          }
        }
      },
      error: e => console.log(e)

    })


  }
  doctorCount: number = 0;
  patientCount: number = 0;
  bedCount: number = 0;
  appointmentCount: number = 0;
  news:Article[] = [];

}
