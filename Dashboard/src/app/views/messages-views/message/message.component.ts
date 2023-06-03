import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Message } from "../../../interfaces/Message";
import { MessageStatus } from "../../../enums/messageStatus";
import { MessageService } from "./../../../services/message.service";
import { Router } from "@angular/router";
declare var window: any;

@Component({
  selector: "li[app-message]",
  templateUrl: "./message.component.html",
  styleUrls: ["./message.component.scss"],
})
export class MessageComponent {
  constructor(private MessageService: MessageService, private Router: Router) {}

  toDisplay = false;
  modal: any;

  showButton() {
    this.toDisplay = !this.toDisplay;
  }
  @Input() message: Message = {
    id: 0,
    senderName: "",
    email: "",
    subject: "",
    status: 0,
    body: "",
    created_at: "",
  };

  @Output() removeMessageEvent = new EventEmitter<Message>();

  DeleteBtnModal(element: any) {
    // this.modal = new window.bootstrap.Modal(element);
    this.modal = element;
    console.log(this.modal);
  }

  MessageIsDeleted() {
    this.removeMessageEvent.emit(this.message);
  }

  get Status() {
    return MessageStatus[this.message.status];
  }
  get Day() {
    let date = new Date(this.message.created_at);
    return this.splitted(date.toDateString())[2];
  }
  get Month() {
    let date = new Date(this.message.created_at);
    return this.splitted(date.toDateString())[1];
  }
  get Year() {
    let date = new Date(this.message.created_at);
    return this.splitted(date.toDateString())[3];
  }

  splitted(date: string) {
    var arr = date.split(" ");
    return arr;
  }

  DeleteMessage(value: number) {
    this.MessageService.Delete(value).subscribe({
      next: () => (this.message = this.RemoveObjectWithId(this.message, value)),
      error: (err) => console.log(err),
      complete: () => {
        this.MessageIsDeleted();
        this.modal.classList.remove("show");
        this.modal.classList.add("d-none");
        document.getElementsByClassName("modal-backdrop")[0].remove();
        document.body.classList.remove("modal-open");
        document.body.classList.add("overflow-auto");
      },
    });
  }
  RemoveObjectWithId(arr: any, id: number) {
    const objWithIdIndex = arr.findIndex((obj: any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }
}
