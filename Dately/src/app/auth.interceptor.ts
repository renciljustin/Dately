import { AuthService } from 'src/app/services/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { TokenService } from './services/token.service';
import { switchMap, filter, take, catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    isRefreshing = false;
    refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private authService: AuthService, private tokenService: TokenService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request)
            .pipe(
                catchError((error: HttpErrorResponse) => {
                    if (request.url.includes('login') || request.url.includes('refresh')) {
                        if (request.url.includes('refresh')) {
                            this.authService.logout();
                        }
                        return throwError(error);
                    }

                    if (error.status !== 401) {
                        return throwError(error);
                    }

                    if (this.isRefreshing) {
                        return this.refreshTokenSubject
                            .pipe(
                                filter(res => res !== null),
                                take(1),
                                switchMap(() => next.handle(this.addAuthorizationToken(request)))
                            );
                    } else {
                        this.isRefreshing = true;

                        this.refreshTokenSubject.next(null);

                        return this.authService.refresh()
                            .pipe(
                                switchMap(res => {
                                    this.isRefreshing = false;
                                    this.refreshTokenSubject.next(res.accessToken.token);

                                    return next.handle(this.addAuthorizationToken(request));
                                }),
                                catchError(err => {
                                    this.isRefreshing = false;

                                    this.authService.logout();

                                    return throwError(err);
                                })
                            );
                    }
                })
            );
    }

    private addAuthorizationToken(request: HttpRequest<any>) {

        const token = this.tokenService.getToken();

        if (!token) {
            return request;
        }

        return request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });
    }
}
