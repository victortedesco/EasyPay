import { AuthService } from "./../../services/authentication/authentication.service";
import { Component, Input, OnInit } from "@angular/core";
import { HeaderComponent } from "../../shared/header/header.component";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { Transaction } from "../../models/transaction.model";
import { User } from "../../models/user.model";
import { ActivatedRoute } from "@angular/router";
import { TransactionService } from "../../services/transaction/transaction.service";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";
import { MatMenuModule } from "@angular/material/menu";
import { DateUtils } from "../../shared/dateutils";
import { UserService } from "../../services/user/user.service";

@Component({
  selector: "app-receipt-page",
  standalone: true,
  imports: [HeaderComponent, NavbarComponent, CommonModule, MatIconModule, MatMenuModule],
  templateUrl: "./receipt-page.component.html",
  styleUrls: [],
})
export class ReceiptPageComponent implements OnInit {
  dateUtils: DateUtils = new DateUtils();
  public transaction?: Transaction;

  private userId?: string;
  public user?: User;

  public sender?: User;
  public receiver?: User;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private transactionService: TransactionService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.user = this.authService.convertTokenToUser();
    this.userId = this.user!.id;

    this.route.params.subscribe((params) => {
      const transactionId = params["id"];
      this.loadTransaction(transactionId);
    });
  }

  loadTransaction(transactionId: string): void {
    this.transactionService
      .getTransactionById(transactionId)
      .subscribe((transaction) => {
        this.transaction = transaction;
        this.loadSender(transaction.senderId);
        this.loadReceiver(transaction.recipientId);
      });
  }

  loadSender(senderId: string): void {
    this.userService.getById(senderId).subscribe((sender) => {
      this.sender = sender;
    });
  }

  loadReceiver(recipientId: string): void {
    this.userService.getById(recipientId).subscribe((receiver) => {
      this.receiver = receiver;
    });
  }
}
