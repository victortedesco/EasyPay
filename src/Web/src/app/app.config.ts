import { ApplicationConfig, provideZoneChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { routes } from "./app.routes";
import {
  provideHttpClient,
  withFetch,
  withInterceptorsFromDi,
} from "@angular/common/http";
import { JWT_OPTIONS, JwtHelperService } from "@auth0/angular-jwt";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withFetch(), withInterceptorsFromDi()),
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
  ],
};
