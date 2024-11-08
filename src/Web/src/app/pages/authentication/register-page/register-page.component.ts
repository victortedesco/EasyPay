import { Component } from "@angular/core";
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from "@angular/forms";
import { CommonModule } from "@angular/common";
import { Router } from "@angular/router";
import { AuthService } from "../../../services/authentication/authentication.service";

@Component({
  selector: "app-register-page",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: "./register-page.component.html",
  styleUrls: [],
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

  // Campos do usuario
  fullname = "";
  email = "";
  phoneNumber = "";
  birthDate = "";
  document = "";

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.passwordForm = this.fb.group({
      password: ["", [Validators.required, Validators.minLength(8)]],
      confirmPassword: ["", [Validators.required]],
    });
  }

  performRegistration() {
    if (this.passwordForm.valid) {
      this.authService.signin(
        this.fullname.trim().replaceAll(" ", "").toLowerCase(),
        this.fullname.trim(),
        this.passwordForm.get("password")?.value,
        this.email,
        this.phoneNumber,
        this.document,
        new Date(this.birthDate).toISOString(),
      ).subscribe({
        next: () => {
          this.router.navigate(["/login"]);
        },
        error: (error: any) => {
          console.log("Erro ao registrar usuário", error);
        },
      });
    }
  }

  goToLogin() {
    this.router.navigate(["/login"]);
  }

  onPasswordFocus(show: any) {
    this.showRequirements = show;
  }

  onConfirmPasswordFocus(show: any) {
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
    const password = this.passwordForm.get("password")?.value || "";
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
    const password = this.passwordForm.get("password")?.value;
    const confirmPassword = this.passwordForm.get("confirmPassword")?.value;
    this.passwordsMatch = password === confirmPassword;

    // Ocultar o popup de confirmação se as senhas coincidirem
    if (this.passwordsMatch) {
      this.showConfirmRequirements = false;
    }
  }

  isPasswordValid(): boolean {
    return (
      this.minLengthMet &&
      this.uppercaseMet &&
      this.lowercaseMet &&
      this.numberMet
    );
  }
}
