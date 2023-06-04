import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../Interfaces/Doctor';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class DoctorsService {
  constructor(private http: HttpClient, private headers: Headers) {}

  private BaseURL = 'http://35.204.41.209:7035/api/Doctors';
  private Headers: HttpHeaders = this.headers.getHeaders();

  GetAllDoctors() {
    return this.http.get<Doctor[]>(this.BaseURL, {
      headers: this.Headers,
    });
  }
  GetById(id: number) {
    return this.http.get<Doctor>(`${this.BaseURL}/${id}`, {
      headers: this.Headers,
    });
  }
  GetDepartmentDoctors(id: number) {
    return this.http.get<Doctor[]>(`${this.BaseURL}/DepartmentDoctors/${id}`, {
      headers: this.Headers,
    });
  }
}
