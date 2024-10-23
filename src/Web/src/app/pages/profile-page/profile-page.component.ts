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

  private userId: number = 0;

  public user?: User = {
    id: 1234,
    document: "123.456.789-10",
    name: "Victor Augusto Tedesco",
    email: "victor.a.tedesco@gmail.com",
    balance: 150001.33,
  };

  public cards: Card[] = [
    {
      id: 123,
      number: 5412123412341234,
      holderId: 1234,
      holderName: "VICTOR A TEDESCO",
      securityCode: 123,
      expirationDate: new Date(),
      limitValue: 100_000,
      expenseValue: 1_000,
      color: "Black",
      type: "Crédito/Débito",
    },
  ];

  public recentTransactions: Transaction[] = [
    {
      id: "1234565",
      senderId: 123456,
      senderName: "Victor Tedesco",
      receiverId: 123456,
      receiverName: "Vinicius Borges",
      value: 233.49,
      date: new Date(),
    },
  ];

  public isBalanceVisible: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.userId = parseInt(params.get("id") || "0");
      //this.loadUser();
      this.loadCards();
      this.loadRecentTransactions();
      this.isBalanceVisible = localStorage.getItem("showBalance") === "true";
    });
  }

  loadUser(): void {
    if (this.userId == 0) this.router.navigate([""]);

    this.userService.getById(this.userId).subscribe({
      next: (response) => {
        this.user = response;
      },
      error: () => {
        this.router.navigate([""]);
      },
    });
  }

  loadCards(): void {}

  loadRecentTransactions(): void {}

  toggleBalanceVisibility(): void {
    this.isBalanceVisible = !this.isBalanceVisible;
    localStorage.setItem("showBalance", String(this.isBalanceVisible));
  }

  viewTransaction(transaction: Transaction): void {
    this.router.navigate(["transaction", { id: transaction.id }]);
  }
}
