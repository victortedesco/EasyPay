import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, switchMap } from "rxjs";
import { AuthResponse, AuthService } from "./authentication.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()) {
      const token = this.authService.getAccessToken();

      if (this.authService.isTokenExpired(token)) {
        return this.authService.refreshToken().pipe(
          switchMap((newAccessToken: AuthResponse) => {
            const clonedReq = req.clone({
              headers: req.headers.set(
                "Authorization",
                `Bearer ${newAccessToken}`
              ),
            });
            return next.handle(clonedReq);
          })
        );
      }

      const clonedReq = req.clone({
        headers: req.headers.set("Authorization", `Bearer ${token}`),
      });

      return next.handle(clonedReq);
    }

    return next.handle(req);
  }
}
