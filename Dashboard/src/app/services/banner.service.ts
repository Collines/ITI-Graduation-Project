

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Headers } from "./Header";


@Injectable({
  providedIn: "root",
})
export class BannerService {

  constructor(private Client: HttpClient) { }
  private BaseURL = 'https://medical-api.creteagency.com/api/Banner';
  private Header = new Headers().getHeaders();


  GetAllBanners() {
    return this.Client.get(this.BaseURL, { headers: this.Header });
  }

  GetBannerInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`, { headers: this.Header });
  }

  GetById(id: number) {
    return this.Client.get(`${this.BaseURL}/${id}`, { headers: this.Header });
  }

  AddBanner(formData: FormData) {
    return this.Client.post(this.BaseURL, formData, { headers: this.Header });
  }

  DeleteBanner(id: number) {
    return this.Client.delete(`${this.BaseURL}/${id}`, { headers: this.Header });
  }

  EditBanner(id: number, formData: FormData) {
    return this.Client.patch(`${this.BaseURL}/${id}`, formData, { headers: this.Header});
  }

}
