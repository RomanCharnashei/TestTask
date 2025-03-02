import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Step2Form, Step2FormData } from '../../../types/step2-form';
import { Observable, switchMap } from 'rxjs';
import { CountryService } from '../../../services/сountry.service';
import { Conuntry } from '../../../types/country';
import { Province } from '../../../types/province';
import { AsyncPipe } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-step-2',
  imports: [ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatSelectModule, AsyncPipe],
  templateUrl: './step-2.component.html',
  styleUrl: './step-2.component.scss',
  standalone: true
})
export class Step2Component {
  private readonly countryService = inject(CountryService);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  countries$: Observable<Conuntry[]>;
  provinces$: Observable<Province[]>;

  constructor() {
    this.countries$ = this.countryService.GetAllCountries();
    this.provinces$ = this.form.controls.country.valueChanges
      .pipe(
        switchMap(countryCode => this.countryService.GetProvinces(countryCode))
      );
  }

  form = new FormGroup<Step2Form>({
    country: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    province: new FormControl('', { nonNullable: true, validators: [Validators.required] })
  });

  onSubmit() {
    if (this.form.valid) {
      const data = this.form.getRawValue() as Step2FormData;

      this.authService.send2step(data)
        .subscribe({
          complete: () => this.authService.updatePermissions(),
          error: (error) => console.log(error)
        });
    }
  }
}
