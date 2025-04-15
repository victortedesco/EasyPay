import { Component, Input, OnInit } from "@angular/core";
import { User } from "../../models/user.model";
import { DateUtils } from "../dateutils";
import { UserService } from "../../services/user/user.service";
import { AuthService } from "../../services/authentication/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-navbar",
  standalone: true,
  imports: [],
  templateUrl: "./navbar.component.html",
  styleUrls: [],
})
export class NavbarComponent {
  dateUtils: DateUtils = new DateUtils();

  @Input({required: true}) public user!: User;

  constructor(private router: Router, private authService: AuthService) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate([""]);
  }
}
