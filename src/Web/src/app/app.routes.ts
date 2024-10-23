import { Routes } from "@angular/router";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";
import { AuthenticationPageComponent } from "./pages/authentication-page/authentication-page.component";
import { HomePageComponent } from "./pages/home-page/home-page.component";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "profile", component: ProfilePageComponent },
  { path: "auth", component: AuthenticationPageComponent },
];
