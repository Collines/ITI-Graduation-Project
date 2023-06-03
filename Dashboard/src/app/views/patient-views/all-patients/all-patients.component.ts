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
      next:(data)=>{this.Patients = data;},
      error:(err)=>{console.log(err)}
    })
  }

  DeletePatient(id:any) {
    if(confirm(`Do You Want To Delete Patient No. ${id}`))
    {
      this.myService.Delete(id).subscribe({
        next: ()=>this.Patients =  this.RemoveObjectWithId(this.Patients,id),
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
}
