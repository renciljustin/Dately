import { UserForLogin } from './../../models/UserForLogin';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  loginFailed: boolean;

  constructor(fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.login = fb.group({
      userName: fb.control(null, [Validators.required]),
      password: fb.control(null, [Validators.required]),
    });
  }

  ngOnInit() {
  }

  get userName(): AbstractControl {
    return this.login.get('userName');
  }

  get password(): AbstractControl {
    return this.login.get('password');
  }

  onLogin() {
    const userToLogin = this.login.value as UserForLogin;

    this.authService.login(userToLogin)
      .subscribe(
        succeed => {
          if (succeed) {
            this.router.navigate(['/']);
          } else {
            this.loginFailed = false;
          }
        },
        error => {
          this.loginFailed = true;
        }
      );
  }
}
