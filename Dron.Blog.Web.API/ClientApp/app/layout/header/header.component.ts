import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { RoutingService } from '../../services/auth/routing service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authService: AuthService,
    private routingService: RoutingService) { }

  ngOnInit() {
  }

  logOut() {
    this.authService.clearAuthentication();
    this.gotoLoginPage();
  }

  private gotoLoginPage() {
    this.routingService.toLogin();

  }
}