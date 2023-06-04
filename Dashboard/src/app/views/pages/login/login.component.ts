import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AccountService } from "src/app/services/account.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  submitted = false;
  isResponseSuccess = true;

  Validation = new FormGroup({
    username: new FormControl(null, [
      Validators.required,
      Validators.minLength(4),
    ]),
    password: new FormControl(null, [
      Validators.required,
      Validators.minLength(4),
    ]),
  });

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    let admin = this.accountService.getAdmin();
    if (admin) {
      this.router.navigate(["/"]);
    }
  }

  get isUserNameValid() {
    return this.Validation.controls.username.valid;
  }

  get isPasswordValid() {
    return this.Validation.controls.password.valid;
  }

  onSubmit() {
    this.submitted = true;

    if (this.Validation.invalid) {
      return;
    } else {
      this.accountService
        .login({
          UserName: this.Validation.value.username,
          Password: this.Validation.value.password,
        })
        .subscribe({
          next: () => {
            console.log("LOGIN SUCCESS!");
            this.router.navigate(["/"]);
          },
          error: () => {
            this.isResponseSuccess = false;
          },
        });
    }
  }
}
