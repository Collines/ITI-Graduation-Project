import { MessageStatus } from "./../enums/messageStatus";
export interface Message {
  id: number;
  SenderName: string;
  Email: string;
  Subject: string;
  Status: MessageStatus;
  Body: string;
  DateTime: Date;
}
