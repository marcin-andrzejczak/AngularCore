import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RegisterFormService {

  errorMessage: string;
  registerForm: FormGroup;
  submitted = false;

  constructor() { }
}