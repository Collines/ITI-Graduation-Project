import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from '../Interfaces/Department';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  private baseURL = 'http://35.204.41.209:7035/api/Department/';
  private Headers: HttpHeaders = this.headers.getHeaders();

  constructor(private http: HttpClient, private headers: Headers) {}

  GetDepartments() {
    return this.http.get<Department[]>(this.baseURL, {
      headers: this.Headers,
    });
  }

  GetById(id: number, accessToken: string) {
    return this.http.get<Department>(this.baseURL + `${id}`, {
      headers: this.Headers,
    });
  }
}
