import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../register/register.component.css'],
})
export class LoginComponent implements OnInit {
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
  submitted = false;
  isLogged = false;
  isResponseSuccess = true;
  Validation = new FormGroup({
    email: new FormControl(null, [
      Validators.required,
      Validators.pattern(/[a-z0-9]+@[a-z]+\.[a-z]{2,3}/),
    ]),
    password: new FormControl(null, [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/[a-zA-Z1-9]/),
    ]),
  });
  get isEmailValid() {
    return this.Validation.controls.email.valid;
  }
  get isPasswordValid() {
    return this.Validation.controls.password.valid;
  }
  onSubmit(ErrorDiv: any) {
    this.submitted = true;

    if (this.Validation.invalid) {
      return;
    } else {
      this.accountService
        .login({
          Email: this.Validation.value.email,
          Password: this.Validation.value.password,
        })
        .subscribe({
          next: (resp) => {
            this.router.navigate(['/dashboard']);
          },
          error: (err) => {
            this.isResponseSuccess = false;
            console.log(err);

            if (err.error.responseMessage) {
              ErrorDiv.innerHTML = err.error.responseMessage;
            } else {
              ErrorDiv.innerHTML = "Can't Connect to Server";
            }
          },
        });
    }
  }
}
