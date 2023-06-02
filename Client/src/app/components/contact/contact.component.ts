import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message } from 'src/app/Interfaces/Message';
import { MessageService } from 'src/app/Services/message.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
})
export class ContactComponent {
  constructor(private messageService: MessageService) {}
  Msg: Message = {
    SenderName: '',
    Email: '',
    Subject: '',
    Body: '',
  };
  Validation = new FormGroup({
    SenderName: new FormControl(null, [
      Validators.required,
      Validators.maxLength(50),
      Validators.minLength(3),
      Validators.pattern(RegExp('^[A-Za-z ]+$')),
    ]),
    Email: new FormControl(null, [
      Validators.pattern(
        RegExp(
          /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/
        )
      ),
      Validators.required,
      Validators.maxLength(50),
    ]),
    Subject: new FormControl(null, [
      Validators.required,
      Validators.maxLength(255),
      Validators.pattern(RegExp('^[A-Za-z 0-9%]+$')),
    ]),
    Body: new FormControl(null, [Validators.required]),
  });

  submitted = false;
  isError = false;
  isSuccess = false;

  get isSenderNameValid() {
    return this.Validation.controls.SenderName.valid;
  }
  get isEmailValid() {
    return this.Validation.controls.Email.valid;
  }
  get isSubjectValid() {
    return this.Validation.controls.Subject.valid;
  }
  get isBodyValid() {
    return this.Validation.controls.Body.valid;
  }

  onSubmit(error: HTMLElement) {
    this.submitted = true;
    if (this.Validation.invalid) return;
    else {
      (this.Msg.Body = this.Validation.value.Body!),
        (this.Msg.Email = this.Validation.value.Email!),
        (this.Msg.Subject = this.Validation.value.Subject!),
        (this.Msg.SenderName = this.Validation.value.SenderName!);
      this.messageService.Add(this.Msg).subscribe({
        next: (res) => {
          this.isError = false;
          this.isSuccess = true;
        },
        error: (e) => {
          this.isError = true;
          this.isSuccess = false;
          error.innerHTML = 'An Error Has been occurred';
        },
      });
    }
  }
}
