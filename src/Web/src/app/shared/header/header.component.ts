import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrls: []
})
export class HeaderComponent {
  @Input({ required: true }) pageName!: string;
}
