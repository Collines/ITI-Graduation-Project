import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  submitted=false
  Validation = new FormGroup({
    FirstName: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
    ]),
    LastName: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
    ]),
    FirstNameAr: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
    ]),
    LastNameAr: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
    ]),
    email: new FormControl(null, [
      Validators.required,
      Validators.pattern(/[a-z0-9]+@[a-z]+\.[a-z]{2,3}/),
    ]),
    Phone: new FormControl(null, [
      Validators.required,
      Validators.pattern(/^01[0125][0-9]{8}$/),
    ]),
    Date: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [
      Validators.minLength(8),
      Validators.pattern(/[a-zA-Z1-9]/),
    ]),
  });

  get isFirstNameValid() {
    return this.Validation.controls.FirstName.valid;
  }
  get isLastNameValid() {
    return this.Validation.controls.LastName.valid;
  }
  get isFirstNameArValid() {
    return this.Validation.controls.FirstNameAr.valid;
  }
  get isLastNameArValid() {
    return this.Validation.controls.LastNameAr.valid;
  }
  get isEmailValid() {
    return this.Validation.controls.email.valid;
  }
  get isPasswordValid() {
    return this.Validation.controls.password.valid;
  }
  get isPhoneValid() {
    return this.Validation.controls.Phone.valid;
  }
  get isDateValid() {
    return this.Validation.controls.Date.valid;
  }

  onSubmit() {
    this.submitted = true;
    if(this.Validation.invalid){
      return
    }
    alert("Success")
}
}
