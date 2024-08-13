import { Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { AuthenticationPageComponent } from './components/authentication-page/authentication-page.component';

export const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'auth', component: AuthenticationPageComponent },
];
