import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientsService } from 'src/app/services/patients.service';

@Component({
  selector: 'app-all-patients',
  templateUrl: './all-patients.component.html',
  styleUrls: ['./all-patients.component.scss']
})
export class AllPatientsComponent implements OnInit {
  constructor(public myService:PatientsService,private router: Router){}
  Patients:any;
  ngOnInit(): void {
    this.myService.GetAll().subscribe({
      next:(data)=>{
        this.Patients = data;
      },
      error:(err)=>{console.log(err)}
    })
  }
  DeletePatient(id:any) {
    if(confirm(`Do You Want To Delete Patient No. ${id}`))
    {
    this.myService.Delete(id).subscribe(
      response=>{console.log('item Deleted successfully!')},
      error=>{console.log('item Still exist successfully!')}
    )
    this.ngOnInit();
  };
}
}
