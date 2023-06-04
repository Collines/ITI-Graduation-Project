import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Headers } from "./Header";


@Injectable({
  providedIn: "root",
})
export class DoctorsService {
  constructor(private Client: HttpClient) {}

  private BaseURL = "http://35.204.41.209:7035/api/Doctors";
  private Header = new Headers().getHeaders();

  GetAllDoctors() { 
    return this.Client.get(this.BaseURL, { headers: this.Header });
  }

  GetDoctorInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`,{ headers: this.Header});
  }

  GetById(id: number) {
    return this.Client.get(`${this.BaseURL}/${id}`, { headers: this.Header});
  }

  AddDoctor(formData: FormData) {
    return this.Client.post(this.BaseURL, formData, { headers: this.Header});
  }

  DeleteDoctor(id: number) {
    return this.Client.delete(`${this.BaseURL}/${id}`, { headers: this.Header});
  }

  EditDoctor(id: number, formData: FormData) {
    return this.Client.patch(`${this.BaseURL}/${id}`, formData, { headers: this.Header});
  }
}
