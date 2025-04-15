import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../../models/user.model";
import { AddUserRequest } from "../../models/request/add.user.request";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private url: string = "http://localhost:5130/api/users";

  constructor(private http: HttpClient) {}

  getById(id: string): Observable<User> {
    return this.http.get<User>(`${this.url}/${id}`);
  }

  getByDocument(document: string): Observable<User> {
    return this.http.get<User>(`${this.url}/document/${document}`);
  }

  getBalance(userId: string): Observable<number> {
    return this.http.get<number>(`${this.url}/balance/${userId}`);
  }

  setBalance(userId: string, value: number): Observable<void> {
    return this.http.patch<void>(`${this.url}/balance/${userId}`, value);
  }

  addUser(user: AddUserRequest): Observable<User> {
    return this.http.post<User>(`${this.url}`, user);
  }
}
