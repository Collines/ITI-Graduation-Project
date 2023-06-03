import { Component, OnInit } from "@angular/core";
import { MessageService } from "../../../services/message.service";
import { Message } from "../../../interfaces/Message";
import { Router } from "@angular/router";

@Component({
  selector: "app-messages",
  templateUrl: "./messages.component.html",
  styleUrls: ["./messages.component.scss"],
})
export class MessagesComponent implements OnInit {
  constructor(private messageServices: MessageService) {}
  ngOnInit(): void {
    this.messageServices.GetAll().subscribe({
      next: (res) => {
        this.Messages = res;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
  Messages: Message[] = [];
  removeMessage(Msg: Message) {
    console.log(Msg.senderName + " deleted");
    this.Messages = this.Messages.filter((item) => item.id !== Msg.id);
  }
}
