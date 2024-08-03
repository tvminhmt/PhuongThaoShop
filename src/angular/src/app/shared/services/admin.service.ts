import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ResponseDto, ProductDetailDto, VoucherDto, PagedResultDto, ServiceResponse, PagedRequest } from '../models/model';
import { tap } from 'rxjs/operators';
import { AccountService } from 'src/app/shared/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = 'https://localhost:44302/api/';
  private token!: string;
  constructor(private http: HttpClient, private accountService: AccountService) { }

  // getVouchers(pageNumber: number, pageSize: number, sorting: string): Observable<PagedResultDto<any>> {
  //   const url = `${this.apiUrl}Voucher/GetPaged`;
  //   const token = this.accountService.getAccessToken();
  //   const headers = new HttpHeaders({
  //     'Content-Type': 'application/json',
  //     'Authorization': `Bearer ${token}`
  //   });
  //   const body = {
  //     maxResultCount: pageSize,
  //     skipCount: (pageNumber - 1) * pageSize,
  //     sorting: sorting
  //   };
  //   return this.http.post<PagedResultDto<any>>(url, body,{ headers: headers });
  // }
  getPageBill(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}Bill/GetPage`, {
      page,
      pageSize,
      keywords
    });
  }
  getBillDetailsByBillId(billId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}BillDetail/GetByBillId`, {
      billId
    });
  }
  getPageVouchers(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}Voucher/GetPage`, {
      page,
      pageSize,
      keywords
    });
  }
  getPageProductDetail(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}ProductDetail/GetPage`, {
      page,
      pageSize,
      keywords
    });
  }
  getListRam(): Observable<any> {
    return this.http.get(`${this.apiUrl}Ram/GetAll`);
  }

  getPagedRam(request: PagedRequest): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<any>(`${this.apiUrl}Ram/GetPaged`, request, { headers: headers });
  }

  getByRamId(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}Ram/GetById?id=${id}`);
  }

  createOrUpdateRam(objDto: any): Observable<any> {
    console.log(objDto);
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Ram/CreateOrUpdateAsync`, objDto, { headers: headers });
  }

  deleteRam(id: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Ram/Delete?id=${id}`, null, { headers: headers });
  }

  getListProduct(): Observable<any> {
    return this.http.get(`${this.apiUrl}Product/GetList`);
  }
  getListDiscount(): Observable<any> {
    return this.http.get(`${this.apiUrl}Discount/GetAll`);
  }

  getListCpu(): Observable<any> {
    return this.http.get(`${this.apiUrl}Cpu/GetAll`);
  }
  getListCardVGA(): Observable<any> {
    return this.http.get(`${this.apiUrl}CardVGA/GetAll`);
  }
  getListHardDrive(): Observable<any> {
    return this.http.get(`${this.apiUrl}HardDrive/GetList`);
  }
  getListScreen(): Observable<any> {
    return this.http.get(`${this.apiUrl}Screen/GetList`);
  }
  getListColor(): Observable<any> {
    return this.http.get(`${this.apiUrl}Color/GetList`);
  }


  deleteBill(id: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Bill/Delete?id=${id}`, null, { headers: headers });
  }
  // role
  getListRole(): Observable<any> {
    return this.http.get(`${this.apiUrl}GetList`);
  }

  getPagedRole(request: PagedRequest): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<any>(`${this.apiUrl}Role/GetPaged`, request, { headers: headers });
  }

  getByRoleId(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}Role/GetById?id=${id}`);
  }

  createOrUpdateRole(objDto: any): Observable<any> {
    console.log(objDto);
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Role/CreateOrUpdateAsync`, objDto, { headers: headers });
  }
  createOrUpdateBill(id?: number, fullName?: string, address?: string, phoneNumber?: string, payment?: number, isPayment?: number, status?: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Bill/BillCreateOrUpdate`, { id,address, fullName, phoneNumber, payment, isPayment, status });
  }
  createOrUpdateProductDetail(id?: number, code?: string,description?: string, price?: number, upgrade?: string, productEntityId?: number, 
    colorEntityId?: number, ramEntityId?: number, cpuEntityId?: number, hardDriveEntityId?: number, screenEntityId?: number, 
    cardVGAEntityId?: number, discountId?: number, status?: number
  ): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}ProductDetail/CreateOrUpdate`, { id,code, description, price, upgrade, productEntityId,colorEntityId,ramEntityId,
      cpuEntityId, hardDriveEntityId,screenEntityId, cardVGAEntityId, discountId,status
     });
  }

  deleteRole(id: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Role/Delete?id=${id}`, null, { headers: headers });
  }
  uploadImage(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.apiUrl}image/upload`, formData);
  }
  getPageImage(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}Images/GetPage`, {
      page,
      pageSize,
      keywords
    });
  }
}
