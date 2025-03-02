import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserInfo } from '../types/userInfo';
import { catchError, of, ReplaySubject, take } from 'rxjs';
import { RegistrationFormData } from '../types/registration-form';
import { Step2FormData } from '../types/step2-form';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);

  public userInfo$ = new ReplaySubject<UserInfo>(1);

  constructor() {
    this.updatePermissions();
  }

  updatePermissions(): void {
    this.http.get<UserInfo>('/api/user/info').pipe(
      take(1),
      catchError(() => of<UserInfo>({ accessLevel: 'none' }))
    )
    .subscribe({
      next: (userInfo) => { this.userInfo$.next(userInfo); }
    });
  }

  register(data: RegistrationFormData) {
    return this.http.post('/api/user/register', data);
  }

  send2step(data: Step2FormData) {
    return this.http.post('/api/user/step2', data);
  }
}
