import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Transaction } from "../../models/transaction.model";
import { AddTransactionRequest } from "../../models/request/add.transaction.request";

@Injectable({
  providedIn: "root",
})
export class TransactionService {
  private url = "http://localhost:5131/api/transactions";

  constructor(private http: HttpClient) {}

  getTransactionById(transactionId: string): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.url}/${transactionId}`);
  }

  getTransactionsByUserId(userId: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.url}/userId/${userId}`);
  }

  getTransactionsBySenderId(userId: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.url}/senderId/${userId}`);
  }

  getTransactionsByRecipientId(userId: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.url}/recipientId/${userId}`);
  }

  addTransaction(transaction: AddTransactionRequest): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.url}`, transaction);
  }
}
