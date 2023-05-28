import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CampImage } from '../Interfaces/User/campImage';
import { HeaderService } from './header.service';

@Injectable({
  providedIn: 'root'
})
export class CampImageService {
  private baseURL = "https://localhost:44372/api/CampImage";
  private Header: HttpHeaders = this.header.Header;
  constructor(private http: HttpClient, private header: HeaderService) {}

  GetImages(accessToken: string) {
    return this.http.get<CampImage[]>(this.baseURL, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
  GetById(id: number, accessToken: string) {
    return this.http.get<CampImage>(this.baseURL + `${id}`, {
      headers: this.Header.set('Authorization', `bearer ${accessToken}`),
    });
  }
}
