import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers } from './Header';


@Injectable({
  providedIn: 'root'
})
export class ReservationsService {

  constructor(private Client:HttpClient) { }

  private BaseURL = "https://localhost:7035/api/Reservation"
  private Header = new Headers().getHeaders();

  getAll(){
    return this.Client.get(this.BaseURL, { headers: this.Header });
  }

  ChangeReservationStatus(id:any, status:any){
    return this.Client.post(`${this.BaseURL}/ReservationStatus/${id}`, status, { headers: this.Header });
  }
}
