import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginInput } from '../../models/account/login/login-input.model';


@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient,
    private authService: AuthService) {
    this.getHttpOptions();
  }


  private getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `bearer ${this.authService.getToken()}`
      })
    };
  }

  login(user: LoginInput): Promise<any> {
      return new Promise((resolve) => {
          this.postWithToken('https://localhost:44360/api/Account/Login', user)
        .subscribe(data => {
          this.authService.authenticateUser(data);
          resolve(true);
        }, err => resolve(false));
    });
  }

  get(url, params = {}): Observable<any> {
    return this.http.get(url, { params });
  }

  post(url, params): Observable<any> {
    return this.http.post(url, params);
  }

  postWithToken(url, params): Observable<any> {
    return this.http.post(url, params, this.getHttpOptions());
  }
  public delete(url, params = {}): Observable<any> {
    return this.http.delete(url, this.getHttpOptions());
  }

  public put(url, params): Observable<any> {
    return this.http.put(url, params, this.getHttpOptions());
  }

}