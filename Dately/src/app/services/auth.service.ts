import { TokenPayload } from './../models/TokenPayload';
import { UserForDetail } from './../models/UserForDetail';
import { UserForRegister } from './../models/UserForRegister';
import { UserForLogin } from './../models/UserForLogin';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TokenService } from './token.service';
import { map, catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  uri = environment.api + 'auth/';

  constructor(private http: HttpClient, private tokenService: TokenService) { }

  login(resource: UserForLogin) {
    return this.http.post(this.uri + 'login', resource)
      .pipe(
        map((res: any) => {
          this.tokenService.setToken(res.token);
          this.tokenService.setRefreshToken(res.refreshToken.token);
          return true;
        }),
        catchError((error: HttpErrorResponse) => {
          console.log(error.error.errors || error.error);
          return throwError(error);
        })
      );
  }

  register(resource: UserForRegister) {
    return this.http.post(this.uri + 'register', resource)
      .pipe(
        map(res => res as UserForDetail),
        catchError((error: HttpErrorResponse) => {
          console.log(error.error.errors || error.error);
          return throwError(error);
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
          // console.log(error.error.errors || error.error);
          return throwError(error);
        })
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
