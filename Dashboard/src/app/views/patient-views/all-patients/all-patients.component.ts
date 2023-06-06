import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { Gender } from 'src/app/enums/gender';
import { PatientsService } from 'src/app/services/patients.service';

@Component({
  selector: 'app-all-patients',
  templateUrl: './all-patients.component.html',
  styleUrls: ['./all-patients.component.scss']
})
export class AllPatientsComponent implements OnInit {

  Patients:any;
  Gender = Gender;
  searchQuery: string = "";
  private searchTimer: any;
  SearchResult: any;
  NoData:boolean = false;
  ErrorMessage:any;

  constructor(
    public myService:PatientsService,
    private router: Router,
    private AccountService:AccountService){}

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.router.navigate(["/login"]);
    }

    this.myService.GetAll().subscribe({
      next:(data)=>{this.Patients = data;
        this.SearchResult = data;
        this.NoData = false;
        if(!this.Patients){
          this.NoData = true;
          this.ErrorMessage = "No Patients Found"
        }
      },
      error:(err)=>{
        this.NoData = true;
        this.ErrorMessage = "Can't Connect to Server"
      }
    })
  }

  DeletePatient(id:any) {
    if(confirm(`Do You Want To Delete Patient No. ${id}`))
    {
      this.myService.Delete(id).subscribe({
        next: ()=>{this.Patients =  this.RemoveObjectWithId(this.Patients,id);
          this.SearchResult =  this.Patients;
          alert(`Patient No. ${id} has been deleted`)
        },
        error: ()=>{console.log('Patient Still exist')}
      });
    }
  }

  RemoveObjectWithId(arr:any, id:number) {
    const objWithIdIndex = arr.findIndex((obj:any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }

  ChangePatientBlockStatus(element:any, id:number){
    if(confirm(`Do You Want To ${element.target.checked?'Block':'Unblock'} Patient No. ${id}`))
    {
      let formData: FormData = new FormData();
      formData.append('BlockStatus', element.target.checked.toString());

      this.myService.ChangeBlockStatus(id, formData).subscribe({
        next: ()=>alert(`Patient No. ${id} has been ${element.target.checked?'Blocked':'Unblocked'}`),
        error: ()=>{alert(`Error happend patient block status did't change`);
        element.target.checked = !element.target.checked;
      }
      });
    }
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
      if (this.Patients.length > 0) {
        this.Patients.forEach((item: any) => {
          if (
            item.fullName
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
            item.email
              .toLocaleLowerCase()
              .includes(this.searchQuery.toLocaleLowerCase()) ||
              item.ssn
                .toLocaleLowerCase()
                .includes(this.searchQuery.toLocaleLowerCase())
          ) {

            this.SearchResult.push(item);
          }
        });
      }
    }  
    else{
      this.SearchResult = this.Patients;
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this.searchTimer);
  }
}
