import { Component, OnInit } from "@angular/core";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { HeaderComponent } from "../../shared/header/header.component";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";
import { DateUtils } from "../../shared/dateutils";
import { User } from "../../models/user.model";
import { UserService } from "../../services/user/user.service";
import { ActivatedRoute, Router } from "@angular/router";
import { Transaction } from "../../models/transaction.model";
import { MatMenuModule } from "@angular/material/menu";
import { AuthService } from "../../services/authentication/authentication.service";
import { TransactionService } from "../../services/transaction/transaction.service";

@Component({
  selector: "app-extract-page",
  standalone: true,
  imports: [
    NavbarComponent,
    HeaderComponent,
    CommonModule,
    MatIconModule,
    MatMenuModule,
  ],
  templateUrl: "./statment-page.component.html",
  styleUrl: "./statment-page.component.css",
})
export class StatmentPageComponent implements OnInit {
  dateUtils: DateUtils = new DateUtils();

  public user?: User;

  public recentTransactions: Transaction[] = [];

  constructor(private router: Router, private authService: AuthService, private transactionService: TransactionService) {}

  ngOnInit(): void {
    this.loadUser();
    this.loadRecentTransactions();
  }

  loadUser(): void {
    this.user = this.authService.convertTokenToUser();
  }

  loadRecentTransactions(): void {
    if (!this.user) return;

    this.transactionService.getTransactionsByUserId(this.user.id).subscribe({
      next: (response) => {
        if (!response) return;
        this.recentTransactions = response.splice(0, 10);
      },
    });
  }

  viewTransaction(transaction: Transaction): void {
    this.router.navigate(["transaction", { id: transaction.id }]);
  }
}
