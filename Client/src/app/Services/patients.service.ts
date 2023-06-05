import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class PatientsService {
  constructor(private http: HttpClient, private headers: Headers) {}

  private BaseURL: string = 'https://medical-api.creteagency.com/api/patient';
  private Headers: HttpHeaders = this.headers.getHeaders();

  GetPatientsCount() {
    return this.http.get<number>(`${this.BaseURL}/Count`, {
      headers: this.Headers,
    });
  }
}
