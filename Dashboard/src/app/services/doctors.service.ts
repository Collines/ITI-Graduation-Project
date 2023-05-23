import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


@Injectable({
  providedIn: "root",
})
export class DoctorsService {
  constructor(private Client: HttpClient) {}

  private BaseURL = "https://localhost:7035/api/Doctors";

  GetAllDoctors() { 
    return this.Client.get(this.BaseURL);
  }

  GetDoctorInsertDTO(id: number) {
    return this.Client.get(`${this.BaseURL}/InsertDTO/${id}`);
  }

  GetById(id: number) {
    return this.Client.get(`${this.BaseURL}/${id}`);
  }

  AddDoctor(formData: FormData) {
    return this.Client.post(this.BaseURL, formData);
  }

  DeleteDoctor(id: number) {
    return this.Client.delete(`${this.BaseURL}/${id}`);
  }

  EditDoctor(id: number, formData: FormData) {
    return this.Client.patch(`${this.BaseURL}/${id}`, formData);
  }
}
