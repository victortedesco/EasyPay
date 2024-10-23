export interface Transaction {
  id: string;
  senderId: number;
  senderName: string;
  receiverId: number;
  receiverName: string;
  value: number;
  date: Date;
}
