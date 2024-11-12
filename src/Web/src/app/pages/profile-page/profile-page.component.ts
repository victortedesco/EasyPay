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
import { HeaderComponent } from "../../shared/header/header.component";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { CardService } from "../../services/card/card.service";
import { AddUserRequest } from "../../models/request/add.user.request";

@Component({
  selector: "app-profile-page",
  standalone: true,
  imports: [MatIconModule, CommonModule, HeaderComponent, NavbarComponent],
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
    private cardService: CardService,
    private transactionService: TransactionService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.loadUser();
      if (!this.userId) return;
      this.loadBalance();
      this.loadCards();
      this.loadRecentTransactions();
      this.isBalanceVisible = localStorage.getItem("showBalance") === "true";
    });
  }

  loadUser(): void {
    this.user = this.authService.convertTokenToUser();
    this.userId = this.user?.id;
  }

  loadBalance(): void {
    if (!this.userId) return;

    this.userService.getBalance(this.userId).subscribe({
      next: (balance) => {
        this.balance = balance;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 404) {
          const addUser: AddUserRequest = {
            name: this.user!.fullname,
            email: this.user!.email,
            document: this.user!.document,
          };

          this.userService.addUser(addUser).subscribe({
            error: () => {
              this.router.navigate(["/login"]);
            },
          });
        }
      },
    });
  }

  loadCards(): void {
    if (!this.userId) return;

    this.cardService.getByUserId(this.userId).subscribe({
      next: (cards) => {
        this.cards = cards;
      },
    });
  }

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
    this.router.navigate(["receipt", { id: transaction.id }]);
  }

  viewCard(cardId: number): void {
    this.router.navigate(["/card", { id: cardId }]);
  }
}
