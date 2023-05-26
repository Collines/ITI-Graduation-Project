import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserEdit } from 'src/app/Interfaces/User/UserEdit';
import { UserUpdate } from 'src/app/Interfaces/User/userUpdate';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  constructor(private accountService: AccountService, private router: Router) {
    this.Validation = new FormGroup({
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
  }
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.accountService.getData(user.accessToken).subscribe({
            next: (udata) => {
              this.userEdit = udata;
              let date = new Date(udata.dob);
              this.dateStr =
                date.getFullYear() +
                '-' +
                (date.getMonth() + 1).toString().padStart(2, '0') +
                '-' +
                date.getDate().toString().padStart(2, '0');
              this.Validation.controls['FirstName'].value =
                this.userEdit.firstName_EN;
              this.Validation.controls['FirstNameAr'].value =
                this.userEdit.firstName_AR;
              this.Validation.controls['LastName'].value =
                this.userEdit.lastName_EN;
              this.Validation.controls['LastNameAr'].value =
                this.userEdit.lastName_AR;
              this.Validation.controls['email'].value = this.userEdit.email;
              this.Validation.controls['Phone'].value = this.userEdit.phone;
              this.Validation.controls['Date'].value = this.dateStr;

              this.Validation.controls['FirstName'].status = 'VALID';
              this.Validation.controls['LastName'].status = 'VALID';
              this.Validation.controls['FirstNameAr'].status = 'VALID';
              this.Validation.controls['LastNameAr'].status = 'VALID';
              this.Validation.controls['email'].status = 'VALID';
              this.Validation.controls['Phone'].status = 'VALID';
              this.Validation.controls['Date'].status = 'VALID';
              this.textArea = this.userEdit.medicalHistory;
            },
          });
        } else {
          this.router.navigate(['/login']);
        }
      },
    });
  }
  submitted = false;
  error = false;
  userEdit: UserEdit = {
    gender: 1,
    ssn: '',
    firstName_EN: '',
    firstName_AR: '',
    lastName_EN: '',
    lastName_AR: '',
    email: '',
    phone: '',
    dob: new Date(),
    medicalHistory: null,
    password: null,
  };

  dateStr = '';
  Validation: any;
  isDataLoaded = false;
  isResponsed = false;
  textArea: string | null = '';

  get isFirstNameValid() {
    return this.Validation.controls['FirstName'].status == 'VALID';
  }
  get isLastNameValid() {
    return this.Validation.controls['LastName'].status == 'VALID';
  }
  get isFirstNameArValid() {
    return this.Validation.controls['FirstNameAr'].status == 'VALID';
  }
  get isLastNameArValid() {
    return this.Validation.controls['LastNameAr'].status == 'VALID';
  }
  get isGenderValid() {
    return this.Validation.controls.Gender.valid;
  }
  get isEmailValid() {
    return this.Validation.controls['email'].status == 'VALID';
  }
  get isPasswordValid() {
    return this.Validation.controls['password'].status == 'VALID';
  }
  get isPhoneValid() {
    return this.Validation.controls['Phone'].status == 'VALID';
  }
  get isDateValid() {
    return this.Validation.controls['Date'].status == 'VALID';
  }

  public onValueChange(event: Event): void {
    const value = (event.target as any).value;
    this.textArea = value;
  }

  logout() {
    this.accountService.logout();
  }

  onSubmit(errorElement: any, SuccessElement: any) {
    this.submitted = true;
    if (this.Validation.invalid) {
      return;
    } else {
      let accessToken;
      this.accountService.currentUser$.subscribe({
        next: (user) => {
          if (user) accessToken = user.accessToken;
        },
      });
      if (accessToken) {
        let user: UserUpdate = {
          firstName_EN: this.Validation.controls['FirstName'].value,
          firstName_AR: this.Validation.controls['FirstNameAr'].value,
          lastName_EN: this.Validation.controls['LastName'].value,
          lastName_AR: this.Validation.controls['LastNameAr'].value,
          email: this.Validation.controls['email'].value,
          phone: this.Validation.controls['Phone'].value,
          dob: this.Validation.controls['Date'].value,
          medicalHistory: this.textArea,
          password: this.Validation.controls['password'].value,
        };
        this.accountService.update(accessToken, user).subscribe({
          next: (r: any) => {
            this.error = false;
            errorElement.classList.add('d-none');
            SuccessElement.innerHTML = r.message;
            this.isResponsed = true;
          },
          error: (e: any) => {
            this.error = true;
            this.isResponsed = false;
            errorElement.innerHTML = e.error;
          },
        });
      }
    }
  }
}
