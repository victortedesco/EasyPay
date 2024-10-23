import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap, switchMap, catchError, throwError } from "rxjs";
import { ConfigService } from "../config.service";

interface AuthResponse {
  access_token: string;
  refresh_token: string;
  expires_in: number;
  refresh_expires_in: number;
  token_type: string;
  "not-before-policy": number;
  session_state: string;
  scope: string;
}

@Injectable({
  providedIn: "root",
})
export class AuthenticationService {
  private adminUrl =
    "http://localhost:8070/realms/easypay/protocol/openid-connect/token";
  private registerUrl = "http://localhost:8070/admin/realms/easypay/users";

  private clientId = "account";

  constructor(
    private http: HttpClient,
    private router: Router,
    private jtwHelper: JwtHelperService,
    private configService: ConfigService
  ) {}

  private getToken(
    client_id: string,
    username: string,
    password: string
  ): Observable<AuthResponse> {
    const body = new URLSearchParams();
    body.set("client_id", client_id);
    body.set("grant_type", "password");
    body.set("username", username);
    body.set("password", password);

    const headers = new HttpHeaders({
      "Content-Type": "application/x-www-form-urlencoded",
    });

    return this.http
      .post<AuthResponse>(this.adminUrl, body.toString(), { headers })
      .pipe(
        tap((response: AuthResponse) => {
          this.storeTokens(response);
        })
      );
  }

  getAdminToken(): Observable<AuthResponse> {
    const adminPassword = this.configService.keycloakAdminPassword ?? "admin";
    return this.getToken(this.clientId, "admin", adminPassword);
  }

  login(username: string, password: string): Observable<AuthResponse> {
    return this.getToken(this.clientId, username, password);
  }

  signin(
    fullName: string,
    username: string,
    password: string,
    email: string,
    document: string
  ): Observable<any> {
    return this.getAdminToken().pipe(
      switchMap((response: AuthResponse) => {
        const token = response.access_token;

        const body = {
          firstName: fullName.split(" ")[0],
          lastName: fullName.split(" ").slice(1).join(" "),
          username: username,
          enabled: true,
          emailVerified: true,
          email: email,
          document: document,
          credentials: [
            {
              type: "password",
              value: password,
              temporary: false,
            },
          ],
        };

        const headers = new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        });

        return this.http.post(this.registerUrl, body, { headers });
      })
    );
  }

  private storeTokens(tokens: AuthResponse): void {
    localStorage.setItem("access_token", tokens.access_token);
    localStorage.setItem("refresh_token", tokens.refresh_token);
    localStorage.setItem("expires_in", tokens.expires_in.toString());
    localStorage.setItem("token_type", tokens.token_type);
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(["/login"]);
  }

  isAuthenticated(): boolean {
    return !!this.getAccessToken();
  }

  getAccessToken(): string {
    return localStorage.getItem("access_token") ?? "";
  }

  private isTokenExpired(token: string): boolean {
    return this.jtwHelper.isTokenExpired(token);
  }

  private refreshToken(): Observable<AuthResponse> {
    const accessToken = this.getAccessToken() ?? "";

    if (this.isTokenExpired(accessToken)) {
      const refreshToken = localStorage.getItem("refresh_token") ?? "";
      if (!refreshToken) {
        this.logout();
      }

      const body = new URLSearchParams();
      body.set("client_id", this.clientId);
      body.set("grant_type", "refresh_token");
      body.set("refresh_token", refreshToken);

      const headers = new HttpHeaders({
        "Content-Type": "application/x-www-form-urlencoded",
      });

      return this.http
        .post<AuthResponse>(this.adminUrl, body.toString(), { headers })
        .pipe(
          tap((response: AuthResponse) => {
            this.storeTokens(response);
          }),
          catchError((error) => {
            this.logout();
            return throwError(() => error);
          })
        );
    } else {
      return new Observable<AuthResponse>((observer) => {
        observer.next({
          access_token: accessToken,
          refresh_token: localStorage.getItem("refresh_token")!,
          expires_in: Number(localStorage.getItem("expires_in")),
          refresh_expires_in: 0,
          token_type: localStorage.getItem("token_type")!,
          "not-before-policy": 0,
          session_state: "",
          scope: "",
        });
        observer.complete();
      });
    }
  }

  getRoles(): string[] {
    if (this.isAuthenticated()) {
      const token = this.getAccessToken();
      const decodedToken = this.jtwHelper.decodeToken(token);
      const roles = decodedToken?.realm_access?.roles;

      return roles;
    }
    return [];
  }

  getRoleUrl(): string {
    if (this.isAuthenticated()) {
      const roles = this.getRoles();

      if (roles.includes("admin")) return "/admin";

      return "/profile";
    }
    return "";
  }
}
