import { Component, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-page.component.html',
  styleUrls: []
})
export class RegisterPageComponent {
  passwordForm: FormGroup;
  showRequirements = false;
  showConfirmRequirements = false;

  // Estados dos requisitos da senha
  minLengthMet = false;
  uppercaseMet = false;
  lowercaseMet = false;
  numberMet = false;
  passwordsMatch = false;

  constructor(private fb: FormBuilder, private elementRef: ElementRef, private router: Router) {
    this.passwordForm = this.fb.group({
      password: [
        '',
        [Validators.required, Validators.minLength(8)]
      ],
      confirmPassword: [
        '',
        [Validators.required]
      ]
    });
  }

  goToAuth(){
    this.router.navigate(['/auth'])
  }

  onPasswordFocus(show : any) {
    this.showRequirements = show;
  }

  onConfirmPasswordFocus(show : any) {
    this.showConfirmRequirements = show;
    this.checkPasswordMatch();
  }

  onPasswordBlur() {
    this.checkRequirements();
  }

  onConfirmPasswordBlur() {
    this.checkPasswordMatch();
  }

  checkRequirements() {
    const password = this.passwordForm.get('password')?.value || '';
    this.minLengthMet = password.length >= 8;
    this.uppercaseMet = /[A-Z]/.test(password);
    this.lowercaseMet = /[a-z]/.test(password);
    this.numberMet = /[0-9]/.test(password);

    // Ocultar a mensagem se todos os requisitos forem atendidos
    if (this.isPasswordValid()) {
      this.showRequirements = false;
    }
  }

  checkPasswordMatch() {
    const password = this.passwordForm.get('password')?.value;
    const confirmPassword = this.passwordForm.get('confirmPassword')?.value;
    this.passwordsMatch = password === confirmPassword;

    // Ocultar o popup de confirmação se as senhas coincidirem
    if (this.passwordsMatch) {
      this.showConfirmRequirements = false;
    }
  }

  isPasswordValid(): boolean {
    return this.minLengthMet && this.uppercaseMet && this.lowercaseMet && this.numberMet;
  }
}
