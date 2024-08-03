import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GHNService {

  private apiUrl = 'https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee';
  private token = 'YOUR_GHN_API_TOKEN'; // Thay thế bằng token thực của bạn

  constructor(private http: HttpClient) { }

  calculateShippingFee(weight: number, length: number, width: number, height: number, toDistrictID: number, serviceTypeID: number): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Token': this.token
    });

    const body = {
      "service_id": serviceTypeID,
      "insurance_value": 0,
      "coupon": null,
      "from_district_id": 1450, // Mã quận/huyện gửi hàng (ví dụ)
      "to_district_id": toDistrictID,
      "weight": weight,
      "length": length,
      "width": width,
      "height": height,
      "pick_station_id": null,
      "deliver_station_id": null
    };

    return this.http.post<any>(this.apiUrl, body, { headers });
  }
}
