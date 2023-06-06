import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserEdit } from 'src/app/Interfaces/User/UserEdit';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
  constructor(private accountService: AccountService, private router: Router) {
    this.Validation = new FormGroup({
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
              this.Validation.status = 'VALID';
              this.textArea = this.userEdit.medicalHistory ?? '';
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
    id: 0,
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
    image: '',
  };

  BaseImage: any = '';
  FileToUpload: any;
  dateStr = '';
  Validation: any;
  isDataLoaded = false;
  isResponsed = false;
  textArea: string = '';
  test: any;

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
        const formData: FormData = new FormData();
        if (this.FileToUpload) formData.append('Image', this.FileToUpload);

        formData.append(
          'FirstName_EN',
          this.Validation.controls['FirstName'].value
        );
        formData.append(
          'LastName_EN',
          this.Validation.controls['LastName'].value
        );
        formData.append(
          'FirstName_AR',
          this.Validation.controls['FirstNameAr'].value
        );
        formData.append(
          'LastName_AR',
          this.Validation.controls['LastNameAr'].value
        );
        formData.append('Gender', `${this.userEdit.gender}`);
        formData.append('Email', this.Validation.controls['email'].value);
        formData.append('Phone', this.Validation.controls['Phone'].value);
        formData.append('DOB', this.Validation.controls['Date'].value);
        formData.append('MedicalHistory', this.textArea);
        formData.append('SSN', this.userEdit.ssn);
        let pass = this.Validation.controls['password'].value;
        formData.append('Password', pass == '' ? null : pass);
        if (this.FileToUpload) {
          // this.DoctorsService.AddDoctor(formData).subscribe({
          //   next: data => {alert("Doctor Added Successfully")
          //                   this.Router.navigate(['/Doctors'])},
          //   error: err => console.log(err)});
        }
        this.test = formData;
        // let user: UserUpdate = {
        //   firstName_EN: this.Validation.controls['FirstName'].value,
        //   firstName_AR: this.Validation.controls['FirstNameAr'].value,
        //   lastName_EN: this.Validation.controls['LastName'].value,
        //   lastName_AR: this.Validation.controls['LastNameAr'].value,
        //   email: this.Validation.controls['email'].value,
        //   phone: this.Validation.controls['Phone'].value,
        //   dob: this.Validation.controls['Date'].value,
        //   medicalHistory: this.textArea,
        //   password: this.Validation.controls['password'].value,
        //   image: { id: 0, name: this.BaseImage, patientId: this.userEdit.id },
        //   gender:this.userEdit.gender
        // };
        this.accountService.update(accessToken, formData).subscribe({
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
            this.test.forEach((el: any, key: any) => {
              console.log(el, key);
            });
            console.log(e);
          },
        });
      }
    }
  }

  async ChangeSrc(e: any) {
    var file = e.target.files[0];
    if (file) {
      this.BaseImage = await this.ConvertBase64(file);
      this.FileToUpload = file;
    }
  }

  ConvertBase64 = (file: any) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();

      fileReader.readAsDataURL(file);
      fileReader.onload = () => resolve(fileReader.result);

      fileReader.onerror = (error) => reject(error);
    });
  };
}
