import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartmentsService } from 'src/app/services/departments.service';

@Component({
  selector: 'app-details-department',
  templateUrl: './details-department.component.html',
  styleUrls: ['./details-department.component.scss']
})
export class DetailsDepartmentComponent implements OnInit {
  ID:any;
  Department:any;
  constructor(myRoute:ActivatedRoute, public myService:DepartmentsService){
    this.ID = myRoute.snapshot.params["id"];
  }
  ngOnInit(): void {
    this.myService.GetById(this.ID).subscribe({
      next:(data)=>{
        //console.log(data)
        this.Department = data;
      },
      error:(err)=>{console.log(err)}
    });
  }
}
