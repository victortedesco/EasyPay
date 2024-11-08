import { Routes } from "@angular/router";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";
import { LoginPageComponent } from "./pages/authentication/login-page/login-page.component";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { RegisterPageComponent } from "./pages/authentication/register-page/register-page.component";
import { AuthGuard } from "./services/authentication/authentication.guard";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "profile", component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginPageComponent },
  { path: "register", component: RegisterPageComponent},
  { path: "**", redirectTo: "" },
];
