import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from '../Interfaces/Department';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private baseURL = 'https://localhost:7035/api/Department/';
  private Header: HttpHeaders = this.header.Header;
  constructor(private http: HttpClient, private header: HeaderService) {}
  GetDepartments(accessToken: string) {
    return this.http.get<Department[]>(this.baseURL, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
  GetById(id: number, accessToken: string) {
    return this.http.get<Department>(this.baseURL + `${id}`, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
}
