import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/services/doctors.service';
import { Gender } from 'src/app/enums/gender';

@Component({
  selector: 'app-all-doctors',
  templateUrl: './all-doctors.component.html',
  styleUrls: ['./all-doctors.component.scss']
})
export class AllDoctorsComponent implements OnInit {

  constructor(private DoctorsService:DoctorsService){}
  Doctors:any;
  Gender = Gender;

  ngOnInit(): void {
    this.DoctorsService.GetAllDoctors().subscribe({
      next: data => {this.Doctors = data;
    },
      error: err => console.log(err)
    })
  }

  DeleteDoctor(value:number){
    if(confirm(`Do You Want To Delete Doctor No. ${value}`))
    {
      this.DoctorsService.DeleteDoctor(value).subscribe({
        next: () => this.Doctors =  this.RemoveObjectWithId(this.Doctors,value),
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

}
