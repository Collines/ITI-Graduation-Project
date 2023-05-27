import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  doctorCount:number=0;
  doctorCountStop:any=setInterval(()=>{
    this.doctorCount++;
    if(this.doctorCount == 300){
      clearInterval(this.doctorCountStop)
    }
  },22)

  patientCount:number=0;
  patientCountStop:any=setInterval(()=>{
    this.patientCount+=2;
    if(this.patientCount == 1040){
      clearInterval(this.patientCountStop)
    }
  },12)

  bedCount:number=0;
  bedCountStop:any=setInterval(()=>{
    this.bedCount++;
    if(this.bedCount == 500){
      clearInterval(this.bedCountStop)
    }
  },13)

  appointmentCount:number=0;
  appointmentCountStop:any=setInterval(()=>{
    this.appointmentCount+=5;
    if(this.appointmentCount == 2000){
      clearInterval(this.appointmentCountStop)
    }
  },15)

}
