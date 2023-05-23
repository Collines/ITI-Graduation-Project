import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DepartmentsService } from 'src/app/services/departments.service';

@Component({
  selector: 'app-all-departments',
  templateUrl: './all-departments.component.html',
  styleUrls: ['./all-departments.component.scss']
})
export class AllDepartmentsComponent implements OnInit {
  constructor(public myService:DepartmentsService,private router: Router){}
  departments:any;
  ngOnInit(): void {
    this.myService.GetAllDepartments().subscribe({
      next:(data)=>{
        this.departments = data;
      },
      error:(err)=>{console.log(err)}
    })
  }
  DeleteDept(id:any) {
    if(confirm(`Do You Want To Delete Department No. ${id}`))
    {
    this.myService.DeleteDepartment(id).subscribe(
      response=>{console.log('item Deleted successfully!')},
      error=>{console.log('item Still exist successfully!')}
    )
    this.ngOnInit();
  };
}
}
