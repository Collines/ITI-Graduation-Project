import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers } from './Header';


@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

  constructor(private Client:HttpClient) { }

  private BaseURL = "https://medical-api.creteagency.com/api/Department";
  private Header = new Headers().getHeaders();

  GetAllDepartments(){
    return this.Client.get(this.BaseURL, { headers: this.Header });
  }

  GetById(id:number){
    return this.Client.get(`${this.BaseURL}/${id}`, { headers: this.Header });
  }

  AddDepartment(NewDepartment:any){
    return this.Client.post(this.BaseURL,NewDepartment, { headers: this.Header });
  }

  DeleteDepartment(id:number){
    return this.Client.delete(`${this.BaseURL}/${id}`, { headers: this.Header });
  }

  EditDepartment(id:number,NewDepartment:any){
    return this.Client.patch(`${this.BaseURL}/${id}`,NewDepartment, { headers: this.Header });
  }
  GetDepartmentInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`, { headers: this.Header });
  }
}
