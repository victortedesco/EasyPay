import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-home-page",
  standalone: true,
  imports: [],
  templateUrl: "./home-page.component.html",
  styleUrls: [],
})
export class HomePageComponent implements OnInit {
  public hideCookieBanner: boolean = false;

  constructor(private router: Router) {}

  ngOnInit() {
    if (localStorage.getItem("cookies") === "accepted") {
      this.hideCookieBanner = true;
    }
  }

  acceptCookies() {
    localStorage.setItem("cookies", "accepted");
    this.hideCookieBanner = true;
  }

  goToLogin() {
    this.router.navigate(["/login"]);
  }

  goToRegister() {
    this.router.navigate(["/register"]);
  }
}
