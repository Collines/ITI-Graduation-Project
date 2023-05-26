import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../Interfaces/Doctor';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root',
})
export class DoctorsService {
  constructor(private http: HttpClient, private header: HeaderService) {}

  private BaseURL = 'https://localhost:7035/api/Doctors';
  private Header: HttpHeaders = this.header.Header;

  GetAllDoctors(accessToken: string) {
    return this.http.get<Doctor[]>(this.BaseURL, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
  GetById(id: number, accessToken: string) {
    return this.http.get(`${this.BaseURL}/${id}`, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
  GetDepartmentDoctors(id: number, accessToken: string) {
    return this.http.get<Doctor[]>(`${this.BaseURL}/DepartmentDoctors/${id}`, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
}
