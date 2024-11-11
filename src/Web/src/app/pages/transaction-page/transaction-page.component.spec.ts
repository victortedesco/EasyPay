import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TransferenciaService {
  private apiUrl = 'http://localhost:4200/transaction'; 

  constructor(private http: HttpClient) {}

  getRecentTransfers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/recent`);
  }

  createTransfer(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
}
