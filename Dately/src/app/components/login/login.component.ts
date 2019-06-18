import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: FormGroup;
  loginFailed: boolean;

  constructor(fb: FormBuilder) {
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
    this.loginFailed = true;
  }
}
