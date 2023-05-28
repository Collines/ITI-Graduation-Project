import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservation } from '../Interfaces/reservation';
import { HeaderService } from './header.service';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  constructor(private http: HttpClient, private headers: Headers) {}

  private BaseURL = 'https://localhost:7035/api/Reservation';
  private Headers = this.headers.getHeaders();

  GetAllReservations(id: number, accessToken: string) {
    return this.http.post<Reservation[]>(
      this.BaseURL + '/My-Reservations?patientId=' + id,
      `\"${accessToken}\"`,
      { headers: this.Headers }
    );
  }

  CancelReservation(id: number, reservation: Reservation, accessToken: string) {
    return this.http.patch<Reservation>(
      this.BaseURL + '/CancelReservation/' + id,
      reservation,
      { headers: this.Headers }
    );
  }

  AddReservation(reservation: Reservation, accessToken: string) {
    return this.http.post(
      this.BaseURL + `?accessToken=${accessToken}`,
      reservation,
      { headers: this.Headers }
    );
  }

  getReservationsCount() {
    return this.http.get<number>(`${this.BaseURL}/Count`, {
      headers: this.Headers,
    });
  }
}
