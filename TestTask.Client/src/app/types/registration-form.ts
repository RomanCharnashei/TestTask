import { FormControl } from '@angular/forms';

export type RegistrationFormData  = {
  login: string;
  password: string;
  confirmPassword: string;
  agree: boolean;
}

export type RegistrationForm  = {
  login: FormControl<string>;
  password: FormControl<string>;
  confirmPassword: FormControl<string>;
  agree: FormControl<boolean>;
}