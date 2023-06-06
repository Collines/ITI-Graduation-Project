import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/services/doctors.service';
import { Gender } from 'src/app/enums/gender';
import { AccountService } from 'src/app/Services/account.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-all-doctors',
  templateUrl: './all-doctors.component.html',
  styleUrls: ['./all-doctors.component.scss']
})
export class AllDoctorsComponent implements OnInit {

  Doctors:any;
  Gender = Gender;
  searchQuery: string = "";
  private searchTimer: any;
  SearchResult: any;
  NoDoctors:boolean = false;
  ErrorMessage:any;

  constructor(
    private DoctorsService:DoctorsService,private Router:Router,
    private AccountService:AccountService){}

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.DoctorsService.GetAllDoctors().subscribe({
      next: data => {this.Doctors = data;
        this.SearchResult = data;
        this.NoDoctors = false;
        if(!this.Doctors){
          this.NoDoctors = true;
          this.ErrorMessage = "No Department Found"
        }
    },
      error: err => {
        this.NoDoctors = true;
        this.ErrorMessage = "Can't Connect to Server"
      }
    })
  }

  DeleteDoctor(value:number){
    if(confirm(`Do You Want To Delete Doctor No. ${value}`))
    {
      this.DoctorsService.DeleteDoctor(value).subscribe({
        next: () => {this.Doctors =  this.RemoveObjectWithId(this.Doctors,value);
          this.SearchResult = this.Doctors;
          alert(`Doctor No. ${value} has been deleted`)
        },
        error: err => console.log(err)
      })
    }
  }

  RemoveObjectWithId(arr:any, id:number) {
    const objWithIdIndex = arr.findIndex((obj:any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }

  onSearch(): void {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.performSearch();
    }, 300);
  }

  performSearch(): void {
    this.SearchResult = [];

    if (this.searchQuery.length > 0) {
      if (this.Doctors.length > 0) {
        this.Doctors.forEach((item: any) => {
          if (
            item.firstName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
            item.lastName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
              item.departmentTitle
                .toLocaleLowerCase()
                .includes(this.searchQuery.toLocaleLowerCase())
          ) {

            this.SearchResult.push(item);
          }
        });
      }
    }  
    else{
      this.SearchResult = this.Doctors;
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this.searchTimer);
  }

}
