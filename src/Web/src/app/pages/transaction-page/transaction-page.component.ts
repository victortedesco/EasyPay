import { TransactionService } from "./../../services/transaction/transaction.service";
import { UserService } from "./../../services/user/user.service";
import { Component, OnInit } from "@angular/core";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { HeaderComponent } from "../../shared/header/header.component";
import { MatIconModule } from "@angular/material/icon";
import { CommonModule } from "@angular/common";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { FormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { User } from "../../models/user.model";
import { Transaction } from "../../models/transaction.model";
import { AuthService } from "../../services/authentication/authentication.service";

@Component({
  selector: "app-transferencia",
  templateUrl: "./transaction-page.component.html",
  styleUrls: [],
  standalone: true,
  imports: [
    MatIconModule,
    CommonModule,
    HeaderComponent,
    NavbarComponent,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatSelectModule,
  ],
})
export class TransactionPageComponent implements OnInit {
  private userId?: string;
  public user?: User;
  private userBalance: number = 0;

  public recentTransactions: Transaction[] = [];
  public recentRecipients: User[] = [];
  public selectedRecipient?: User;
  public transferAmount: number = 0;

  transferType: any;
  bankNumber: string = "";
  document: string = "";
  pixKey: string = "";

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private transactionService: TransactionService  ) {}

  ngOnInit(): void {
    this.loadUser();
    this.loadRecentTransactions();
    this.loadRecipients();
  }

  loadUser(): void {
    this.user = this.authService.convertTokenToUser();
    this.userId = this.user?.id;
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

  loadRecipients(): void {
    this.recentTransactions.filter(
      (transaction) => transaction.receiverName != this.user?.fullname
    );

    this.recentRecipients = this.recentTransactions.map((transaction) => {
      return {
        id: transaction.receiverId,
        username: "",
        fullname: transaction.receiverName,
        email: "",
        document: "",
        phoneNumber: "",
      };
    });
  }

  loadBalance(): void {
    if (!this.user) return;
    this.userService.getBalance(this.user.id).subscribe({
      next: (balance) => {
        this.userBalance = balance;
      },
    });
  }

  addToBalance(user: User, value: number): void {
    if (user.id === this.selectedRecipient?.id) return;
    this.userService.setBalance(user.id, value).subscribe({
      next: () => {
        this.loadBalance();
      },
    });
  }

  confirmTransfer(): void {
    if (this.pixKey.length != 11 || !this.transferAmount) return;
    if (this.transferAmount > this.userBalance - 200) {
      return;
    }

    const transaction: Transaction = {
      id: "",
      senderId: this.user?.id!,
      senderName: this.user?.fullname!,
      receiverId: this.selectedRecipient?.id!,
      receiverName: this.selectedRecipient?.fullname!,
      value: this.transferAmount,
      date: new Date().toUTCString()
    };

    this.userService.getByDocument(this.pixKey).subscribe({
      next: (response) => {
        this.selectedRecipient = response;
        this.transactionService.addTransaction(transaction).subscribe({
          next: (response) => {
            this.loadBalance();
            this.addToBalance(this.user!, this.transferAmount * -1);
            this.addToBalance(this.selectedRecipient!, this.transferAmount);
            this.recentTransactions.unshift(response);
          },
        });
      },
      error: () => {
        return;
      }
    })
  }
}
