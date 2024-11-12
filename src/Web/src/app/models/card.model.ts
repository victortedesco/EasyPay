export interface Card {
  id: number;
  cardNumber: number,
  userId: number,
  userName: string,
  securityNumber: number,
  expireDate: string,
  cardLimit: number,
  totalExpenses: number,
  color: string,
  cardFlag: string,
}

/* Exemplo:
{
  "id": 2,
  "userId": "2bcdc3e3-0b60-4abd-9f77-c3099db8542f",
  "userName": "Victor Augusto Tedesco",
  "cardNumber": "5258288582231636",
  "securityNumber": "465",
  "expireDate": "2027-11-11",
  "cardLimit": 1000,
  "totalExpenses": 0,
  "cardFlag": "MasterCard"
}
*/
