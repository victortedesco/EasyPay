import { Component, OnInit } from "@angular/core";
import { User } from "../../models/user.model";
import { DateUtils } from "../dateutils";
import { UserService } from "../../services/user/user.service";
import { AuthService } from "../../services/authentication/authentication.service";

@Component({
  selector: "app-navbar",
  standalone: true,
  imports: [],
  templateUrl: "./navbar.component.html",
  styleUrls: [],
})
export class NavbarComponent implements OnInit {
  dateUtils: DateUtils = new DateUtils();
  public user?: User;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.user = this.authService.convertTokenToUser();
  }
}
