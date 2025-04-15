import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../../../services/authentication/authentication.service";
import { FormsModule } from "@angular/forms";
import { User } from "../../../models/user.model";
@Component({
  selector: "app-login-page",
  standalone: true,
  imports: [FormsModule],
  templateUrl: "./login-page.component.html",
  styleUrls: [],
})
export class LoginPageComponent {
  username: string = "";
  password: string = "";

  constructor(private router: Router, private authService: AuthService) {}

  performLogin() {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        console.log(response);
        const roleBasedRoute = this.authService.getRoleUrl();
        this.router.navigate([roleBasedRoute]);
      },
      error: (error) => {
        console.error("Error to login", error);
      },
    });
  }
}
