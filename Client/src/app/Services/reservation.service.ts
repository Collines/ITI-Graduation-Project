import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservation } from '../Interfaces/reservation';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  constructor(private http: HttpClient) {}
  private BaseURL = 'https://localhost:7035/api/Reservation/';
  private Header: HttpHeaders = new HttpHeaders()
    .set('content-type', 'application/json')
    .set('Access-Control-Allow-Origin', '*');
  GetAllReservations(id: number, accessToken: string) {
    return this.http.post<Reservation[]>(
      this.BaseURL + 'My-Reservations?patientId=' + id,
      `\"${accessToken}\"`,
      { headers: this.Header.set('Authorization', `bearer ${accessToken}`) }
    );
  }
}
