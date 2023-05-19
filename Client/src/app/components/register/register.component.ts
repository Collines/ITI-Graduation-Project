import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  submitted=false
  Validation = new FormGroup({
    name: new FormControl(null, [Validators.required, Validators.minLength(3)]),
    email: new FormControl(null, [Validators.required, Validators.pattern(/[a-z0-9]+@[a-z]+\.[a-z]{2,3}/)]),
    password: new FormControl(null, [ Validators.required,Validators.minLength(8), Validators.pattern(/[a-zA-Z1-9]/)]),
  });
  get isNameValid() {
    return this.Validation.controls.name.valid;
  }
  get isEmailValid() {
    return this.Validation.controls.email.valid;
  }
  get isPasswordValid() {
    return this.Validation.controls.password.valid;
  }
  onSubmit(){
    this.submitted = true

    if(this.Validation.invalid){
      return
    }
    alert("Success")
  }

}
