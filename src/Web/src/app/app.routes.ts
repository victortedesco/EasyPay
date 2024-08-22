import { Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { AuthenticationPageComponent } from './pages/authentication-page/authentication-page.component';

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'auth', component: AuthenticationPageComponent },
];
