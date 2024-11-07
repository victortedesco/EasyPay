import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'app-authentication-page',
  standalone: true,
  imports: [],
  templateUrl: './authentication-page.component.html',
  styleUrls: []
})


export class AuthenticationPageComponent {
    constructor(private router: Router) {}
  
    goToRegister() {

      this.router.navigate(['/register']);
    }
}
