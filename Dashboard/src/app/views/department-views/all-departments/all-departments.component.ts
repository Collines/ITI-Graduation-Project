import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentsService } from 'src/app/services/departments.service';

@Component({
  selector: 'app-all-departments',
  templateUrl: './all-departments.component.html',
  styleUrls: ['./all-departments.component.scss']
})
export class AllDepartmentsComponent implements OnInit {

  departments:any;

  constructor(
    public myService:DepartmentsService,
    private router: Router,
    private AccountService:AccountService){}

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.router.navigate(["/login"]);
    }

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
    this.myService.DeleteDepartment(id).subscribe({
      next: response=>this.departments =  this.RemoveObjectWithId(this.departments,id),
      error: error=>{alert('item Still exist')}
    })};
  }

  RemoveObjectWithId(arr:any, id:number) {
    const objWithIdIndex = arr.findIndex((obj:any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }
}
