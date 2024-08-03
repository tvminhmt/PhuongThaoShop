import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) { }

  async canActivate(): Promise<boolean> {
    const currentUserString = localStorage.getItem('currentUser');
    if (currentUserString) {
      console.log('currentUser found in localStorage');
      const currentUser = JSON.parse(currentUserString);
      const token = currentUser.accessToken;
      if (token) {
        const isAdmin = await this.accountService.checkIsAdmin();
        if (isAdmin) {
          return true;
        }
      }
    }
    await this.router.navigateByUrl('/login', { skipLocationChange: true });
    return false;
  }
}
