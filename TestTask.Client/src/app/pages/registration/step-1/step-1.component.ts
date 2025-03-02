import { Component, inject } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { RegistrationForm, RegistrationFormData } from '../../../types/registration-form';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-step-1',
  imports: [ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, MatButtonModule],
  templateUrl: './step-1.component.html',
  styleUrl: './step-1.component.scss',
  standalone: true
})
export class Step1Component {
  private readonly authService = inject(AuthService);

  form = new FormGroup<RegistrationForm>({
    login: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    password: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    confirmPassword: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    agree: new FormControl(false, { nonNullable: true, validators: [Validators.required] })
  }, { 
    validators: this.passwordMatchValidator 
  });

  onSubmit() {
    if (this.form.valid) {
      const registration = this.form.getRawValue() as RegistrationFormData;

      this.authService.register(registration)
        .subscribe({
          complete: () => this.authService.updatePermissions(),
          error: (error) => console.log(error)
        });
    }
  }

  private passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { passwordMismatch: true };
  };

}
