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
  private user?: User;
  private userBalance: number = 0;

  public recentTransfers: Transaction[] = [];
  public recentRecipients: User[] = [];
  public selectedRecipient?: User;
  public transferAmount: number = 0;

  transferType: any;
  bankNumber: any;
  cpfCnpj: any;
  pixKey: string = "";

  constructor() {}

  ngOnInit(): void {
    this.loadRecentTransfers();
    this.loadRecipients();
  }

  loadRecentTransfers(): void {}

  loadRecipients(): void {}

  confirmTransfer(): void {
    if (this.transferAmount > this.userBalance) {
      return;
    }

    const transferData = {
      recipient: this.selectedRecipient,
      amount: this.transferAmount,
    };

    alert("Transação Concluida com Sucesso");
  }
}
