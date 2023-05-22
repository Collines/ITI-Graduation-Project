import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {

  constructor(private Clint:HttpClient) { }

  private BaseURL = "https://localhost:7035/api/Doctors"

  GetAllDoctors(){
    return this.Clint.get(this.BaseURL);
  }
  // getAllDoctors(): Observable<any> {
  //   return this.Clint.get(this.BaseURL);
  // }

  GetById(id:number){
    return this.Clint.get(`${this.BaseURL}/${id}`);
  }

  AddDoctor(NewDoctor:any){
    return this.Clint.post(this.BaseURL,NewDoctor);
  }

  DeleteDoctor(id:number){
    return this.Clint.delete(`${this.BaseURL}/${id}`);
  }

  EditDoctor(id:number,NewDoctor:any){
    return this.Clint.patch(`${this.BaseURL}/${id}`,NewDoctor);
  }
}
