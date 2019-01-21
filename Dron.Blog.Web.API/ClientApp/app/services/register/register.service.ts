
import { Injectable } from '@angular/core';
import { HttpService } from '../auth/http.service';
import { AuthService } from '../auth/auth.service';
import { RegisterInput } from '../../models/account/registration/registration-input.model';




@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpService: HttpService,
    private authService: AuthService) { }

  add(user: RegisterInput): Promise<any> {
    return new Promise((resolve) => {
      this.httpService.post('https://localhost:44360/api/Account/Register', user).subscribe(data => {
        const _user = new RegisterInput();
        _user.username = user.username;
        _user.email = user.email;
          _user.password = user.password;
          _user.confirmPassword = user.confirmPassword;
        this.httpService.login(_user).then(loginData => {
          resolve(loginData);
        });
      });
    });

  }
}
