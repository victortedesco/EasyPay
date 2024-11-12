import { Routes } from "@angular/router";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";
import { LoginPageComponent } from "./pages/authentication/login-page/login-page.component";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { RegisterPageComponent } from "./pages/authentication/register-page/register-page.component";
import { AuthGuard } from "./services/authentication/authentication.guard";
import { StatementPageComponent } from "./pages/statement-page/statement-page.component";
import { TransactionPageComponent } from "./pages/transaction-page/transaction-page.component";
import { ReceiptPageComponent } from "./pages/receipt-page/receipt-page.component";
import { CardPageComponent } from "./pages/card-page/card-page.component";
import { CardsPageComponent } from "./pages/cards-page/cards-page.component";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "profile", component: ProfilePageComponent },
  { path: "login", component: LoginPageComponent },
  { path: "register", component: RegisterPageComponent },
  { path: "statement", component: StatementPageComponent },
  { path: "send", component: TransactionPageComponent },
  { path: "receipt", component: ReceiptPageComponent },
  { path: "card", component: CardPageComponent },
  { path: "cards", component: CardsPageComponent },
  { path: "**", redirectTo: "" },
];
