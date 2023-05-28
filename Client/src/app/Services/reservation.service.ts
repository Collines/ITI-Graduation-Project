import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservation } from '../Interfaces/reservation';
import { HeaderService } from './header.service';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  constructor(
    private http: HttpClient,
    private header: HeaderService,
    private headers: Headers
  ) {}

  private BaseURL = 'https://localhost:7035/api/Reservation';
  private Header: HttpHeaders = this.header.Header;
  private Headers = this.headers.getHeaders();

  GetAllReservations(id: number, accessToken: string) {
    return this.http.post<Reservation[]>(
      this.BaseURL + '/My-Reservations?patientId=' + id,
      `\"${accessToken}\"`,
      { headers: this.Header.set('Authorization', `bearer ${accessToken}`) }
    );
  }

  CancelReservation(id: number, reservation: Reservation, accessToken: string) {
    return this.http.patch<Reservation>(
      this.BaseURL + '/CancelReservation/' + id,
      reservation,
      { headers: this.Header.set('Authorization', `bearer ${accessToken}`) }
    );
  }

  AddReservation(reservation: Reservation, accessToken: string) {
    return this.http.post(
      this.BaseURL + `?accessToken=${accessToken}`,
      reservation,
      { headers: this.Header.set('Authorization', `bearer ${accessToken}`) }
    );
  }

  getReservationsCount() {
    return this.http.get<number>(`${this.BaseURL}/Count`, {
      headers: this.Headers,
    });
  }
}
