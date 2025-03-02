import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Conuntry } from '../types/country';
import { Province } from '../types/province';
import { catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private readonly http = inject(HttpClient);

  GetAllCountries() {
    return this.http.get<Conuntry[]>('/api/countries');
  }

  GetProvinces(countryCode: string) {
    return this.http.get<Province[]>(`/api/provinces/${countryCode}`);
  }
}
