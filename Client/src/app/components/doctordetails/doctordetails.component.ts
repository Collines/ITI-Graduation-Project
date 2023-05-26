import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-doctordetailss',
  templateUrl: './doctordetails.component.html',
  styleUrls: ['./doctordetails.component.css'],
})
export class DoctordetailsComponent implements OnInit {
  ID: any;
  Doctor: any;
  constructor(
    private DoctorsService: DoctorsService,
    private Route: ActivatedRoute
  ) {
    this.ID = this.Route.snapshot.params['id'];
  }
  ngOnInit() {
    this.DoctorsService.GetById(this.ID).subscribe({
      next: (data) => {
        this.Doctor = data;
        console.log(data);
      },
      error: (err) => console.log(err),
    });
  }
}
