import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../../models/user.model";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private url: string = "http://localhost:5130";

  constructor(private http: HttpClient) {}

  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.url}/api/user/${id}`);
  }
}