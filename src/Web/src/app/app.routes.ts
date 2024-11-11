import { Routes } from "@angular/router";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";
import { LoginPageComponent } from "./pages/authentication/login-page/login-page.component";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { RegisterPageComponent } from "./pages/authentication/register-page/register-page.component";
import { AuthGuard } from "./services/authentication/authentication.guard";
import { StatmentPageComponent } from "./pages/statment-page/statment-page.component";
import { TransactionPageComponent } from "./pages/transaction-page/transaction-page.component";
import { ReceiptPageComponent } from "./pages/receipt-page/receipt-page.component";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "profile", component: ProfilePageComponent },
  { path: "login", component: LoginPageComponent },
  { path: "register", component: RegisterPageComponent},
  { path: "statment", component: StatmentPageComponent, canActivate: [AuthGuard] },
  { path: "send", component: TransactionPageComponent, canActivate: [AuthGuard] },
  { path: "receipt", component: ReceiptPageComponent, canActivate: [AuthGuard] },
  { path: "**", redirectTo: "" },
];
