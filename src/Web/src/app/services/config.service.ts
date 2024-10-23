import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  private config: any;

  constructor(private http: HttpClient) {}

  loadConfig(): Observable<any> {
    return this.http.get('/assets/config.json');
  }

  get keycloakAdminPassword(): string {
    return this.config.keycloakAdminPassword;
  }

  setConfig(config: any) {
    this.config = config;
  }
}
