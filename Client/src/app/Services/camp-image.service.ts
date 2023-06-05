import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CampImage } from '../Interfaces/campImage';
import { HeaderService } from './header.service';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root',
})
export class CampImageService {
  private baseURL = 'https://medical-api.creteagency.com/api/CampImage';
  constructor(private http: HttpClient, private Header: Headers) {}
  private headers = this.Header.getHeaders();

  GetImages() {
    return this.http.get<CampImage[]>(this.baseURL, {
      headers: this.headers,
    });
  }
  GetById(id: number, accessToken: string) {
    return this.http.get<CampImage>(this.baseURL + `${id}`, {
      headers: this.headers,
    });
  }
}
