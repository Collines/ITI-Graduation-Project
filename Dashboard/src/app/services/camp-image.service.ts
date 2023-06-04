import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Headers } from "./Header";

@Injectable({
  providedIn: 'root'
})
export class CampImageService {
  constructor(private Client: HttpClient) { }
  private BaseURL = "http://35.204.41.209:7035/api/CampImage";
  private Header = new Headers().getHeaders();

  GetAllCampImages() { 
    return this.Client.get(this.BaseURL, { headers: this.Header });
  }

  GetCampImageInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`,{ headers: this.Header});
  }

  GetById(id: number) {
    return this.Client.get(`${this.BaseURL}/${id}`, { headers: this.Header});
  }

  AddCampImage(formData: FormData) {
    return this.Client.post(this.BaseURL, formData, { headers: this.Header});
  }

  DeleteCampImage(id: number) {
    return this.Client.delete(`${this.BaseURL}/${id}`, { headers: this.Header});
  }
}