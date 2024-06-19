import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ResponseDto, ProductDetailDto, CartItemDto, ServiceResponse, RequestBillDto } from '../models/model';
import { AccountService } from 'src/app/shared/services/account.service';
import { AppConstants } from 'src/app/constants';
@Injectable({
  providedIn: 'root'
})
export class PublicService {
  private apiUrl = AppConstants.API_URL;

  constructor(private http: HttpClient, private accountService: AccountService) { }
  // getListProducts(page: number, pageSize: number, filters: any): Observable<any> {
  //   const body = {
  //     page: page,
  //     pageSize: pageSize,
  //     filters
  //   };

  //   return this.http.post<any>(this.apiUrl+'Public/GetListProduct', body);
  // }
  getListProducts(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}Public/GetListProduct`, {
      page,
      pageSize,
      keywords
    });
  }
  getProducts(): Observable<ProductDetailDto[]> {
    const params = {

    };

    return this.http.get<ResponseDto>(this.apiUrl + 'ProductDetail/PGetProductDetail', { params })
      .pipe(map(response => response.result));
  }
  getProductById(productId: string): Observable<ProductDetailDto> {
    const params = {

    };

    return this.http.get<ResponseDto>(`${this.apiUrl}ProductDetail/GetById?id=${productId}`, { params })
      .pipe(map(response => response.result));
  }
  getCartByUser(): Observable<CartItemDto[]> {
    const currentUserString = localStorage.getItem('currentUser');
    if (currentUserString) {
      const currentUser = JSON.parse(currentUserString);
      if (currentUser.username) {
        const params = {

        };
        return this.http.post<ResponseDto>(this.apiUrl + `Cart/GetCartByUser?username=${currentUser.username}`, { params })
          .pipe(map(response => response.result));
      }

    }
    const params = {

    };

    return this.http.post<ResponseDto>(this.apiUrl + 'Cart/GetCartByUser?username=kieumy', { params })
      .pipe(map(response => response.result));
  }
  addProductToCart(username: string, productId: string): Observable<ServiceResponse> {
    const params = {
      username: username,
      idProductDetail: productId
    };
    return this.http.post<ServiceResponse>(`${this.apiUrl}Cart/AddToCart`, params);
  }
  deleteCartDetai(Id: number): Observable<ServiceResponse> {
    const params = {
    };
    return this.http.delete<ServiceResponse>(`${this.apiUrl}Cart/DeleteCartDetail?Id=${Id}`, params);
  }
  updateQuantityCartItem(quantity: number, idCartDetail: number): Observable<ServiceResponse> {
    const params = {
      quantity: quantity,
      idCartDetail: idCartDetail,
    };
    return this.http.post<ServiceResponse>(`${this.apiUrl}Cart/UpdateQuantity`, params);
  }
  getListVouchers(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}Public/GetListVoucher`);
  }
  createBill(request: RequestBillDto): Observable<ResponseDto> {
    request.username = this.accountService.getUsername();
    const cartItemsString = localStorage.getItem('cartItems');
      if (cartItemsString) {
        request.cartItem = JSON.parse(cartItemsString);
      }
    console.log(request)
    const params = {
      requestBillDto: request,
    };
    return this.http.post<ResponseDto>(`${this.apiUrl}Bill/CreateBill`, params);
  }
}