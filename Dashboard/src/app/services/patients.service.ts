import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers } from './Header';


@Injectable({
  providedIn: 'root'
})
export class PatientsService {

  constructor(private Client:HttpClient) { }


  private Base_URL = "https://localhost:7035/api/Patient";
  private Header = new Headers().getHeaders();

  GetAll(){
    return this.Client.get(this.Base_URL, { headers: this.Header });
  }

  GetByID(id:any){
    return this.Client.get(`${this.Base_URL}/${id}`, { headers: this.Header });
  }
  
  Delete(id: any) {
    return this.Client.delete(`${this.Base_URL}/${id}`, { headers: this.Header });
  }
}