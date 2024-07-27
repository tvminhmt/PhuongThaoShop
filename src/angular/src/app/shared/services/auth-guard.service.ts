import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  username!: boolean;
  constructor(private accountService: AccountService, private router: Router) { }
  async canActivate(): Promise<boolean> {
    const currentUserString = localStorage.getItem('currentUser');
    if (currentUserString) {
      const currentUser = JSON.parse(currentUserString);
      this.username = currentUser.isAdmin;
    }
    if (this.username) {
      return true;
    } 
    await this.router.navigateByUrl('/login', { skipLocationChange: true });
    return false;
  }
}
