import { FormControl } from "@angular/forms";

export type Step2FormData  = {
  country: string;
  province: string;
}

export type Step2Form  = {
  country: FormControl<string>;
  province: FormControl<string>;
}