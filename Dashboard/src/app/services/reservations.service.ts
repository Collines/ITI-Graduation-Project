import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ReservationsService {

  constructor(private Client:HttpClient) { }

  private BaseURL = "https://localhost:7035/api/Reservation"

  getAll(){
    return this.Client.get(this.BaseURL);
  }
}
