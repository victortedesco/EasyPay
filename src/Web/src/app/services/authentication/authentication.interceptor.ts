import {
  HttpHandlerFn,
  HttpRequest,
} from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthResponse, AuthService } from "./authentication.service";
import { switchMap } from "rxjs";

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  // Inject the current `AuthService` and use it to get an authentication token:
  const authToken = inject(AuthService).getAccessToken();
  const isTokenExpired = inject(AuthService).isTokenExpired(authToken);

  if (authToken === "") return next(req);

  if (isTokenExpired) {
    return inject(AuthService).refreshToken().pipe(
      switchMap((newAccessToken: AuthResponse) => {
        const clonedReq = req.clone({
          headers: req.headers.append(
            "Authorization",
            `Bearer ${newAccessToken}`
          ),
        });
        return next(clonedReq);
      })
    );
  }
  // Clone the request to add the authentication header.
  const newReq = req.clone({
    headers: req.headers.append('Authorization', `Bearer ${authToken}`),
  });
  return next(newReq);
}
