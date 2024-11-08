export interface Card {
  id: number;
  number: number,
  holderId: number,
  holderName: string,
  securityCode: number,
  expirationDate: Date,
  limitValue: number,
  expenseValue: number,
  color: string,
  type: string,
}
