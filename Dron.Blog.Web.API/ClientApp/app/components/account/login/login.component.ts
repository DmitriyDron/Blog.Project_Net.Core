
import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { LoginInput } from '../../../models/account/login/login-input.model';
import { HttpService } from '../../../services/auth/http.service';
import { RoutingService } from '../../../services/auth/routing service';
import { AuthService } from '../../../services/auth/auth.service';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: LoginInput;
  loginMessage: string;
  constructor(private httpService: HttpService,
    private authService: AuthService,
    private routingService: RoutingService) {
    this.user = new LoginInput();
  }

  ngOnInit() {
      if (this.authService.getStatus()) {
          this.routingService.toHome();
    }
  }

  login() {
      this.httpService.login(this.user).then((data) => {
          if (data) {
              this.loginMessage = "Login success"
      } else {
        this.loginMessage = 'Failed to login';
      }
    });
  }

  goRegisterComponent() {
    this.routingService.toRegister();
  }
}