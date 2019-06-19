import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenPayload } from '../models/TokenPayload';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  helper = new JwtHelperService();

  constructor() { }

  setToken(token: string): void {
      localStorage.setItem('token', token);
  }

  getToken(): string {
    return localStorage.getItem('token');
  }

  isTokenExpired(): boolean {
    if (this.getToken()) {
      return this.helper.isTokenExpired(this.getToken());
    }
    return true;
  }

  decodedToken(): TokenPayload | null {
    if (this.getToken()) {
      return this.helper.decodeToken(this.getToken());
    }
    return null;
  }

  hasToken(): boolean {
    return localStorage.getItem('token') ? true : false;
  }

  setRefreshToken(refreshToken: string): void {
      localStorage.setItem('refreshToken', refreshToken);
  }

  getRefreshToken(): string {
    return localStorage.getItem('refreshToken');
  }

  hasRefreshToken(): boolean {
    return localStorage.getItem('refreshToken') ? true : false;
  }
}
