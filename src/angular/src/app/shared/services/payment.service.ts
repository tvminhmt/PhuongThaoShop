
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private apiUrl = `https://localhost:44302/api/vnpay`;

  constructor(private http: HttpClient) { }

  createPayment(orderType: string, amount: number, orderDescription: string, name: string, codeBill: string): Observable<any> {
    const body = {
      orderType,
      amount,
      orderDescription,
      name,
      codeBill
    };

    return this.http.post(`${this.apiUrl}/CreatePayment`, body);
  }
  paymentCallback(queryParams: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/PaymentCallback`, queryParams);
  }
}
