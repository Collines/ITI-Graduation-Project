import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

  constructor(private Client:HttpClient) { }

  private BaseURL = "https://localhost:7035/api/Department"

  GetAllDepartments(){
    return this.Client.get(this.BaseURL);
  }

  GetById(id:number){
    return this.Client.get(`${this.BaseURL}/${id}`);
  }

  AddDepartment(NewDepartment:any){
    return this.Client.post(this.BaseURL,NewDepartment);
  }

  DeleteDepartment(id:number){
    return this.Client.delete(`${this.BaseURL}/${id}`);
  }

  EditDepartment(id:number,NewDepartment:any){
    return this.Client.patch(`${this.BaseURL}/${id}`,NewDepartment);
  }
}
