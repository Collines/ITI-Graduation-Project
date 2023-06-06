import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  submitted = false;
  constructor(private accountService: AccountService, private router: Router) {}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (res) => {
        this.isLogged = !!res;
      },
    });
    if (this.isLogged) {
      this.router.navigate(['/home']);
    }
  }
  isLogged = false;
  Validation = new FormGroup({
    FirstName: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
      Validators.pattern('^[a-zA-z]+$'),
    ]),
    LastName: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
      Validators.pattern('^[a-zA-z]+$'),
    ]),
    FirstNameAr: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
      Validators.pattern('^[\u0621-\u064A]+$'),
    ]),
    LastNameAr: new FormControl(null, [
      Validators.required,
      Validators.minLength(3),
      Validators.pattern('^[\u0621-\u064A]+$'),
    ]),
    email: new FormControl(null, [
      Validators.required,
      Validators.pattern(/[a-z0-9]+@[a-z]+\.[a-z]{2,3}/),
    ]),
    Phone: new FormControl(null, [
      Validators.required,
      Validators.pattern(/^01[0125][0-9]{8}$/),
    ]),
    Gender: new FormControl(0, [
      Validators.required,
      Validators.pattern(/^[1-2]$/),
    ]),
    SSN: new FormControl(null, [
      Validators.required,
      Validators.pattern(/^\d+$/),
      Validators.maxLength(14),
      Validators.minLength(14),
    ]),
    Date: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [
      Validators.required,
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
  get isGenderValid() {
    return this.Validation.controls.Gender.valid;
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
  get isSSNValid() {
    return this.Validation.controls.SSN.valid;
  }
  isError = false;
  onSubmit(error: any) {
    this.submitted = true;
    if (this.Validation.invalid) {
      return;
    } else {
      let user = {
        SSN: this.Validation.value.SSN,
        FirstName_EN: this.Validation.value.FirstName,
        LastName_EN: this.Validation.value.LastName,
        FirstName_AR: this.Validation.value.FirstNameAr,
        LastName_AR: this.Validation.value.LastNameAr,
        Email: this.Validation.value.email,
        Phone: this.Validation.value.Phone,
        Gender: this.Validation.value.Gender,
        Password: this.Validation.value.password,
        DOB: this.Validation.value.Date,
      };
      this.accountService.register(user).subscribe({
        next: (res) => {
          this.router.navigate(['/profile']);
          this.isError = false;
        },
        error: (err) => {
          this.isError = true;
          error.innerHTML = err.error;
        },
      });
    }
  }
}
