import { Component, OnInit } from "@angular/core";
import {
  FormGroup,
  FormControl,
  Validators,
} from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { AccountService } from "src/app/Services/account.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  submitted = false;
  isLogged = false;
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

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (res) => {
        this.isLogged = !!res;
      },
    });
    if (this.isLogged) {
      this.router.navigate(["/dashboard"]);
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
            this.accountService.currentUser$.subscribe({
              next: (user) => {
                console.log(user);
              },
            });
            this.router.navigate(["../dashboard"]);
          },
          error: () => {
            this.isResponseSuccess = false;
          },
        });
    }
  }
}
