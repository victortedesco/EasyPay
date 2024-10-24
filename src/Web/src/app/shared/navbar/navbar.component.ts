import { Component } from "@angular/core";
import { User } from "../../models/user.model";
import { DateUtils } from "../dateutils";

@Component({
  selector: "app-navbar",
  standalone: true,
  imports: [],
  templateUrl: "./navbar.component.html",
  styleUrls: [],
})
export class NavbarComponent {
  dateUtils: DateUtils = new DateUtils();
  public user?: User = {
    id: 1234,
    document: "123.456.789-10",
    name: "Victor Augusto Tedesco",
    email: "victor.a.tedesco@gmail.com",
    balance: 150001.33,
  };
}
