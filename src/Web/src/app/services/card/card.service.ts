import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Card } from "../../models/card.model";

@Injectable({
  providedIn: "root",
})
export class CardService {
  private url: string = "http://localhost:5130/api/cards";

  constructor(private http: HttpClient) {}

  getById(id: string): Observable<Card> {
    return this.http.get<Card>(`${this.url}/${id}`);
  }

  getByUserId(userId: string): Observable<Card[]> {
    return this.http.get<Card[]>(`${this.url}/user/${userId}`);
  }

  addCard(): Observable<Card> {
    return this.http.post<Card>(`${this.url}`, {});
  }

  updateCard(card: Card): Observable<void> {
    return this.http.put<void>(`${this.url}`, card);
  }

  deleteCard(id: string): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
