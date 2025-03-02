import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { map } from 'rxjs';

export const fullAccessGuard: CanActivateFn = () => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.userInfo$.pipe(
    map((userInfo) => userInfo.accessLevel === 'full' || router.createUrlTree(['/registration']))
  );
};
