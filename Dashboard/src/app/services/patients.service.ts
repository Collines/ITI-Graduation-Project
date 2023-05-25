import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class PatientsService {

  constructor(private Client:HttpClient) { }

  //private BaseURL = "https://localhost:7035/api/Patient"

  private Base_URL = "https://localhost:7035/api/Patient";
  GetAll(){
    return this.Client.get(this.Base_URL);
  }
  GetByID(id:any){
    return this.Client.get(`${this.Base_URL}/${id}`);
  }
  Delete(id: any) {
    return this.Client.delete(`${this.Base_URL}/${id}`);
  }
}