import { DateUtils } from "./../../shared/dateutils";
import { MatIconModule } from "@angular/material/icon";
import { Component, OnInit } from "@angular/core";
import { User } from "../../models/user.model";
import { UserService } from "../../services/user/user.service";
import { ActivatedRoute, Router } from "@angular/router";
import { CommonModule } from "@angular/common";
import { Card } from "../../models/card.model";
import { Transaction } from "../../models/transaction.model";
import { CardUtils } from "../../shared/cardutils";
import { TransactionService } from "../../services/transaction/transaction.service";
import { AuthService } from "../../services/authentication/authentication.service";
import { HttpErrorResponse } from "@angular/common/http";

@Component({
  selector: "app-profile-page",
  standalone: true,
  imports: [MatIconModule, CommonModule],
  templateUrl: "./profile-page.component.html",
  styleUrls: [],
})
export class ProfilePageComponent implements OnInit {
  dateUtils: DateUtils = new DateUtils();
  cardUtils: CardUtils = new CardUtils();

  private userId?: string;

  public user?: User;

  public balance: number = 0;

  public cards: Card[] = [];

  public recentTransactions: Transaction[] = [];

  public isBalanceVisible: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private transactionService: TransactionService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.user = this.authService.convertTokenToUser();
      this.userId = this.user?.id;
      this.loadBalance();
      this.loadCards();
      this.loadRecentTransactions();
      this.isBalanceVisible = localStorage.getItem("showBalance") === "true";
    });
  }

  loadBalance(): void {
    if (!this.userId) return;

    this.userService.getBalance(this.userId).subscribe({
      next: (balance) => {
        this.balance = balance;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 404) {
          this.userService.addUser(this.user!).subscribe({
            error: () => {
              this.router.navigate([""]);
            },
          });
        }
      },
    });
  }

  loadCards(): void {}

  loadRecentTransactions(): void {
    if (!this.userId) return;

    this.transactionService.getTransactionsByUserId(this.userId).subscribe({
      next: (response) => {
        if (!response) return;
        this.recentTransactions = response.splice(0, 10);
      },
    });
  }

  toggleBalanceVisibility(): void {
    this.isBalanceVisible = !this.isBalanceVisible;
    localStorage.setItem("showBalance", String(this.isBalanceVisible));
  }

  viewTransaction(transaction: Transaction): void {
    this.router.navigate(["transaction", { id: transaction.id }]);
  }
}
