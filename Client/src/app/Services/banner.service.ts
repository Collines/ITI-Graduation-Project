import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CampImage } from '../Interfaces/campImage';
import { HeaderService } from './header.service';
import { Headers } from '../utils/headers.utils';

@Injectable({
  providedIn: 'root'
})
export class BannerService {
  private BaseURL = 'https://localhost:7035/api/Banner';
  constructor(private Client: HttpClient, private Header: Headers) { }
  private headers = this.Header.getHeaders();


  GetAllBanners() {
    return this.Client.get(this.BaseURL, { headers: this.headers });
  }

  GetBannerInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`, { headers: this.headers });
  }

  GetById(id: number) {
    return this.Client.get(`${this.BaseURL}/${id}`, { headers: this.headers });
  }

  AddBanner(formData: FormData) {
    return this.Client.post(this.BaseURL, formData, { headers: this.headers });
  }

  DeleteBanner(id: number) {
    return this.Client.delete(`${this.BaseURL}/${id}`, { headers: this.headers });
  }

  EditBanner(id: number, formData: FormData) {
    return this.Client.patch(`${this.BaseURL}/${id}`, formData, { headers: this.headers});
  }
}
