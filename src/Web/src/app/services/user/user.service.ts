import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../../models/user.model";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private url: string = "http://localhost:5130/api/users";

  constructor(private http: HttpClient) {}

  getById(id: number): Observable<User> {
    return this.http.get<User>(`${this.url}/id/${id}`);
  }

  getByDocument(document: string): Observable<User> {
    return this.http.get<User>(`${this.url}/document/${document}`);
  }
}
