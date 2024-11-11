import { Component } from "@angular/core";
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
export class StatmentPageComponent {
  dateUtils: DateUtils = new DateUtils();

  private userId: number = 0;

  public user?: User;

  public recentTransactions: Transaction[] = [];

  constructor(private router: Router, private userService: UserService) {}

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

  loadRecentTransactions(): void {
    // Depois precisamos fazer um método no back para isso, mas isso vai servir para desenvolvimento.
    this.recentTransactions = this.recentTransactions.splice(0, 10);
  }

  viewTransaction(transaction: Transaction): void {
    this.router.navigate(["transaction", { id: transaction.id }]);
  }
}
