import { UserForLogin } from '../../core/models/user-login.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  loginFailed: boolean;

  constructor(fb: FormBuilder, private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    if (authService.isAuthenticated()) {
      router.navigate(['/home']);
    }

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
            this.route.snapshot.queryParamMap.has('returnUrl')
              ? this.router.navigate([this.route.snapshot.queryParamMap.get('returnUrl')])
              : this.router.navigate(['/']);
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
