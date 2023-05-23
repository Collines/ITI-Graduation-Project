import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class PatientsService {

  constructor(private Client:HttpClient) { }

  private BaseURL = "https://localhost:7035/api/Patient"

  getAll(){
    return this.Client.get(this.BaseURL);
  }
}
