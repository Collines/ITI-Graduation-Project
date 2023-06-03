import { Component, Input, OnInit } from "@angular/core";
import { Message } from "../../../interfaces/Message";
import { MessageStatus } from "../../../enums/messageStatus";
import { MessageService } from './../../../services/message.service';
import { Router } from '@angular/router';
declare var window :any;

@Component({
  selector: "li[app-message]",
  templateUrl: "./message.component.html",
  styleUrls: ["./message.component.scss"],
})
export class MessageComponent  implements OnInit{
 formModel:any;

  constructor( private MessageService:MessageService,private Router:Router){}

  ngOnInit(): void {
    this.formModel=new window.bootstrap.Model(
      document.getElementById(`#exampleModal-${this.message.id}`)
    )
  }

  toDisplay = false;
  model: any;

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

  DeleteMessage(value:number){
      this.MessageService.Delete(value).subscribe({
        next: () => this.message =  this.RemoveObjectWithId(this.message,value),
        error: err => console.log(err)
      })
      this.formModel.hide();
  }
  RemoveObjectWithId(arr:any, id:number) {
    const objWithIdIndex = arr.findIndex((obj:any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }

  
}
