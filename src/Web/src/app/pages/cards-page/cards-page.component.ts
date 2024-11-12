import { DateUtils } from "../../shared/dateutils";
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
import { MatMenuModule } from "@angular/material/menu";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { CardService } from "../../services/card/card.service";

@Component({
  selector: "app-card-page",
  standalone: true,
  imports: [
    MatMenuModule,
    MatIconModule,
    CommonModule,
    HeaderComponent,
    NavbarComponent,
  ],
  templateUrl: "./cards-page.component.html",
  styleUrls: [],
})
export class CardsPageComponent implements OnInit {
  cardUtils: CardUtils = new CardUtils();

  public user?: User;
  public cards: Card[] = [];

  constructor(
    private router: Router,
    private authService: AuthService,
    private cardService: CardService
  ) {}

  ngOnInit(): void {
    this.loadUser();
    this.loadCards();
  }

  loadUser(): void {
    this.user = this.authService.convertTokenToUser();
  }

  loadCards(): void {
    if (!this.user) return;

    this.cardService.getByUserId(this.user.id).subscribe({
      next: (response) => {
        if (!response) return;
        this.cards = response;
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
      },
    });
  }

  viewCard(id: string): void {
    this.router.navigate(["/card", id]);
  }
}
