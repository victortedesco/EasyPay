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
  templateUrl: "./card-page.component.html",
  styleUrls: [],
})
export class CardPageComponent {
  cardUtils: CardUtils = new CardUtils();

  public user?: User;
  public card?: Card;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private cardService: CardService
  ) {}

  ngOnInit(): void {
    this.loadUser();
    this.route.params.subscribe((params) => {
      this.loadCard(params["id"]);
    });
  }

  loadUser(): void {
    this.user = this.authService.convertTokenToUser();
  }

  loadCard(id: string): void {
    this.cardService.getById(id).subscribe({
      next: (response) => {
        if (!response) return;
        this.card = response;
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
        this.router.navigate(["/cards"]);
      },
    });
  }
}
