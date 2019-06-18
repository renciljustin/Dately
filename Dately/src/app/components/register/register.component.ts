import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  register: FormGroup;
  minDate: Date;
  maxDate: Date;

  constructor(fb: FormBuilder) {
    this.register = fb.group({
      userName: fb.control(null, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.pattern(/^[A-Za-z0-9]+$/)]),
      email: fb.control(null, [
        Validators.required,
        Validators.email]),
      password: fb.control(null, [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(30),
        Validators.pattern(/^\S*$/)]),
      confirmPassword: fb.control(null, [Validators.required]),
      firstName: fb.control(null, [Validators.required]),
      lastName: fb.control(null, [Validators.required]),
      birthDate: fb.control(new Date(2000, 0, 1), [Validators.required]),
      gender: fb.control(0, [Validators.required]),
      interest: fb.control(1, [Validators.required])
    },
    {
      validators: this.passwordMatchValidator
    });
  }

  ngOnInit() {
    this.minDate = new Date(1900, 0, 1);
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 17);
  }

  get userName(): AbstractControl {
    return this.register.get('userName');
  }

  get email(): AbstractControl {
    return this.register.get('email');
  }

  get password(): AbstractControl {
    return this.register.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.register.get('confirmPassword');
  }

  get firstName(): AbstractControl {
    return this.register.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.register.get('lastName');
  }

  get birthDate(): AbstractControl {
    return this.register.get('birthDate');
  }

  passwordMatchValidator(g: FormGroup): ValidationErrors | null {
    if (!g.get('password').value ||
        !g.get('confirmPassword').value ||
        g.get('password').value === g.get('confirmPassword').value) {
      return null;
    }

    return {
      mismatchPassword: true
    };
  }
}
