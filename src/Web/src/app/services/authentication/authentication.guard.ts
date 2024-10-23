import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
} from "@angular/router";
import { AuthenticationService } from "./authentication.service";

export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    return this.checkAuth(route);
  }

  canActivateChild(route: ActivatedRouteSnapshot): boolean {
    return this.checkAuth(route);
  }

  private checkAuth(route: ActivatedRouteSnapshot): boolean {
    const targetUrl = `/${route.url.join("/")}`;

    if (!this.authService.isAuthenticated()) {
      this.router.navigate(["/auth"]);
      return false;
    }

    if (targetUrl.includes("admin")) {
      if (this.authService.getRoles().includes("admin") === undefined) {
        this.router.navigate([""]);
        return false;
      }
    }

    return true;
  }
}
