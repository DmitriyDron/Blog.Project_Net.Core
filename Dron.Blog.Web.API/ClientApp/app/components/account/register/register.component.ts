import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterInput } from '../../../models/account/registration/registration-input.model';
import { RegisterService } from '../../../services/register/register.service';
import { RoutingService } from '../../../services/auth/routing service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: RegisterInput;
  message: string;
  messageClass: string;
  constructor(private userService: RegisterService,
    private routingService: RoutingService) {
    this.user = new RegisterInput();
  }

  ngOnInit() {
  }

  register() {
    if (this.checkInputs()) {
        this.userService.add(this.user).then(data => {
            this.routingService.toLogin();
      });

    }

  }
  
  goLoginComponent() {
    this.routingService.toLogin();
  }

  private checkInputs(): boolean {
    if (!this.user.checkPassword()) {
      this.messageClass = 'warning';
      this.message = 'Password not match!';
      return false;
    }
    if (!this.user.checkInputs()) {
      this.message = 'Please checkout input fields!';
      this.messageClass = 'warning';
      return false;
    } else {
      this.message = 'Registration done. Wait a while to login...';
      this.messageClass = 'information';
    }
    return true;
  }
}