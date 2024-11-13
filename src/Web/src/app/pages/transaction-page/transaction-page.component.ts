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
import { AddTransactionRequest } from "../../models/request/add.transaction.request";
import { Router } from "@angular/router";

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
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private transactionService: TransactionService
  ) {}

  ngOnInit(): void {
    this.loadUser();
    this.loadRecentTransactions();
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
        this.recentTransactions = response.splice(0, 5);
        this.loadRecipients();
      },
    });
  }

  loadRecipients(): void {
    this.recentTransactions = this.recentTransactions.filter(
      (transaction) => transaction.recipientName != this.user?.fullname
    );

    this.recentRecipients = this.recentTransactions.map((transaction) => {
      return {
        id: transaction.recipientId,
        username: "",
        fullname: transaction.recipientName,
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
    if (this.pixKey.length > 14 && this.pixKey.length < 11 || !this.transferAmount) return;

    if (this.transferAmount - 200 > this.userBalance) {
      return;
    }
    
    this.transferAmount = Math.abs(this.transferAmount);

    this.userService.getByDocument(this.pixKey).subscribe({
      next: (response) => {
        console.log(response);
        this.selectedRecipient = response;
        const transaction: AddTransactionRequest = {
          recipientId: this.selectedRecipient?.id!,
          recipientName: this.selectedRecipient?.fullname!,
          amount: this.transferAmount,
        };
        console.log(transaction);
        this.transactionService.addTransaction(transaction).subscribe({
          next: (response) => {
            this.loadBalance();
            this.addToBalance(this.user!, this.transferAmount * -1);
            this.addToBalance(this.selectedRecipient!, this.transferAmount);
            this.recentTransactions.unshift(response);
            this.router.navigate(["/receipt", { id: response.id }]);
          },
        });
      },
    });
  }
}
