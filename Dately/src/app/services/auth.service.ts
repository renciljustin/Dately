import { TokenPayload } from '../core/models/token-payload.model';
import { UserDetail } from '../core/models/user-detail.model';
import { UserRegister } from '../core/models/user-register.model';
import { UserForLogin } from '../core/models/user-login.model';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TokenService } from './token.service';
import { map, catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  uri = environment.api + 'auth/';

  constructor(private http: HttpClient, private tokenService: TokenService, private router: Router) { }

  login(resource: UserForLogin) {
    return this.http.post(this.uri + 'login', resource)
      .pipe(
        map((res: any) => {
          this.tokenService.setToken(res.accessToken.token);
          this.tokenService.setRefreshToken(res.refreshToken.token);
          return true;
        }),
        catchError((error: HttpErrorResponse) => {
          console.log(error.error.errors || error.error);
          return throwError(error);
        })
      );
  }

  register(resource: UserRegister) {
    return this.http.post(this.uri + 'register', resource)
      .pipe(
        map(res => res as UserDetail),
        catchError((error: HttpErrorResponse) => {
          console.log(error.error.errors || error.error);
          return throwError(error);
        })
      );
  }

  refresh() {
    const form = new FormData();
    form.append('refreshToken', this.tokenService.getRefreshToken());

    return this.http.put(this.uri + 'refresh', form)
      .pipe(
        map((res: any) => {
          this.tokenService.setToken(res.accessToken.token);
          return res;
        })
      );
  }

  logout() {
    const form = new FormData();
    form.append('refreshToken', this.tokenService.getRefreshToken());

    return this.http.put(`${this.uri}logout`, form)
      .pipe(
        tap(() => {
          this.tokenService.destroyToken();
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        })
      ).subscribe(
        () => {
          this.router.navigate(['/login']);
        }
      );
  }

  userNameExists(userName: any) {
    return this.http.get(this.uri + 'check-username/' + userName);
  }

  emailExists(email: any) {
    return this.http.get(this.uri + 'check-email/' + email);
  }

  isAuthenticated() {
    return !this.tokenService.isTokenExpired();
  }

  get user(): TokenPayload | null {
    return this.tokenService.decodedToken();
  }
}
