import { MessageStatus } from "./../enums/messageStatus";
export interface Message {
  id: number;
  senderName: string;
  email: string;
  subject: string;
  status: MessageStatus;
  body: string;
  created_at: string;
}
