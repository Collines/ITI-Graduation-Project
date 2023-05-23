import { Component,OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {
  doctors:any[]=[];
  constructor(private doctorService:DoctorsService){}//
  ngOnInit(): void {
    this.getDoctors();
  }
  getDoctors() {
    this.doctorService.GetAllDoctors().subscribe(
      (data:any) => {
        this.doctors = data;
      },
      (error) => {
        console.error('Error fetching doctors:', error);
      }
    );
  }

}
