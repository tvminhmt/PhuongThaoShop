
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ResponseDto, ProductDetailDto, VoucherDto, PagedResultDto, ServiceResponse } from '../models/model';
import { tap } from 'rxjs/operators';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'https://localhost:44302/api/';
  private token!: string;
  constructor(private http: HttpClient) { }
  login(username: string, password: string): Observable<any> {
    const url = `${this.apiUrl}Account/Login`;
    const body = { Username: username, Password: password };
    return this.http.post<any>(url, body).pipe(
      map(response => {
        if (response && response.accessToken) {
          localStorage.setItem('currentUser', JSON.stringify({
            username: response.userName,
            role: response.roleName,
            accessToken: response.accessToken,
            isAdmin: response.isAdmin
          }));
          return response;
        } else {
          throw new Error('Invalid response from server');
        }
      })
    );
  }

  getCurrentUser(): any {
    const currentUser = localStorage.getItem('currentUser');
    return currentUser ? JSON.parse(currentUser) : null;
  }
  getUsername(): string {
    const currentUser = localStorage.getItem('currentUser');
    return currentUser ? JSON.parse(currentUser).username : null;
  }
  getAccessToken(): string {
    const currentUser = localStorage.getItem('currentUser');
    return currentUser ? JSON.parse(currentUser).accessToken : null;
  }
  isLoggedIn(): boolean {
    // Kiểm tra xem người dùng đã đăng nhập hay chưa
    return !!localStorage.getItem('currentUser');
  }

  logout(): void {
    // Xóa thông tin người dùng khỏi local storage khi đăng xuất
    localStorage.removeItem('currentUser');
  }
}
