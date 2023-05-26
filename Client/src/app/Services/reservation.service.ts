import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservation } from '../Interfaces/reservation';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  constructor(private http: HttpClient, private header: HeaderService) {}
  private BaseURL = 'https://localhost:7035/api/Reservation/';
  private Header: HttpHeaders = this.header.Header;
  GetAllReservations(id: number, accessToken: string) {
    return this.http.post<Reservation[]>(
      this.BaseURL + 'My-Reservations?patientId=' + id,
      `\"${accessToken}\"`,
      { headers: this.Header.set('Authorization', `bearer ${accessToken}`) }
    );
  }
}
