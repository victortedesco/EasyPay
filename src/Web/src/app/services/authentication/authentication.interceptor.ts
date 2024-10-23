import { HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authenticationService: AuthenticationService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.authenticationService.getRoles().includes('admin')) {
      return this.authenticationService.getAdminToken().pipe(
        switchMap(token => {
          const clonedReq = req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`,
            }
          });
          return next.handle(clonedReq);
        })
      );
    }
    return next.handle(req);
  }
}
