import { AuthService } from './../../services/auth.service';
import { UserForRegister } from './../../models/UserForRegister';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  register: FormGroup;
  minDate: Date;
  maxDate: Date;

  constructor(fb: FormBuilder, private authService: AuthService, private router: Router) {
    if (authService.isAuthenticated()) {
      router.navigate(['/home']);
    }

    this.register = fb.group({
      userName: fb.control(null, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20),
        Validators.pattern(/^[A-Za-z0-9]+$/)], this.userNameValidator.bind(this)),
      email: fb.control(null, [
        Validators.required,
        Validators.email], this.emailValidator.bind(this)),
      password: fb.control(null, [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(30),
        Validators.pattern(/^\S*$/)]),
      confirmPassword: fb.control(null, [Validators.required]),
      firstName: fb.control(null, [Validators.required]),
      lastName: fb.control(null, [Validators.required]),
      birthDate: fb.control(null, [Validators.required]),
      gender: fb.control(null, [Validators.required]),
      interest: fb.control(null, [Validators.required])
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

  get gender(): AbstractControl {
    return this.register.get('gender');
  }

  get birthDate(): AbstractControl {
    return this.register.get('birthDate');
  }

  get interest(): AbstractControl {
    return this.register.get('interest');
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

  // tslint:disable-next-line:member-ordering
  userNameValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      this.authService.userNameExists(control.value)
        .subscribe(
          exists => {
            if (exists) {
              resolve({
                userNameExists: true
              });
            }
            resolve(null);
          }
        );
    });
  }

   // tslint:disable-next-line:member-ordering
   emailValidator(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      this.authService.emailExists(control.value)
        .subscribe(
          exists => {
            if (exists) {
              resolve({
                emailExists: true
              });
            }
            resolve(null);
          }
        );
    });
  }

  onRegister() {
    const userToRegister = this.register.value as UserForRegister;

    this.authService.register(userToRegister)
      .subscribe(
        () => {
          this.register.reset();
          this.router.navigate(['/login']);
        }
      );
  }
}
