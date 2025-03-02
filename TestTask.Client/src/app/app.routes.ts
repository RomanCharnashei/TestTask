import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { fullAccessGuard } from './shared/guards/full-access.guard';
import { RegistrationComponent } from './pages/registration/registration.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', canActivate: [fullAccessGuard], component: HomeComponent },
    { path: 'registration', component: RegistrationComponent },
];
