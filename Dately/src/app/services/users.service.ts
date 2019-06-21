import { UserResult } from './../core/results/user.result';
import { UserQuery } from '../core/queries/user.query';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { serializeQueryParams } from '../shared/query-params-serializer';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  uri = environment.api + 'users';

  constructor(private http: HttpClient) { }

  getList(query: UserQuery) {
    return this.http.get(`${this.uri}?${serializeQueryParams(query)}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`
      }
    })
    .pipe(
      map(res => res as UserResult)
    );
  }
}
