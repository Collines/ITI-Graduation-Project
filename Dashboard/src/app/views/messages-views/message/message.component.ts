import { Component, Input } from "@angular/core";
import { Message } from "../../../interfaces/Message";
import { MessageStatus } from "../../../enums/messageStatus";

@Component({
  selector: "li[app-message]",
  templateUrl: "./message.component.html",
  styleUrls: ["./message.component.scss"],
})
export class MessageComponent {
  toDisplay = false;
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
}
