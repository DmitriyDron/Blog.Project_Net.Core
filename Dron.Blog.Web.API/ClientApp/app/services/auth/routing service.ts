import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {

  constructor(private router: Router) { }
  toLogin() {
    this.router.navigate(['/login']);
    }

 toHome() {
        this.router.navigate(['']);
    }


  toRegister() {
    this.router.navigate(['/register']);
  }

  toPostsCategories() {
    this.router.navigate(['/posts/categories']);
  }


  toPageNotFound() {
    this.router.navigate(['page-not-found']);
  }
}