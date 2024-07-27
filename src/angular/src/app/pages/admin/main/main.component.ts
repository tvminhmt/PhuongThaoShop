import { Component } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  loginForm!: FormGroup;
  isAdminLogin: boolean = false;

  constructor(private accountService: AccountService,private fb: FormBuilder,private router: Router ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submitFormWithAdmin(): void {
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')!.value;
      const password = this.loginForm.get('password')!.value;
      
      this.accountService.login(username, password).subscribe(
        response => {
          // Xử lý phản hồi từ AuthService nếu cần
          if(response.isAdmin){
            this.router.navigateByUrl('/main');
            this.isAdminLogin = true
          }
          console.log(response);
          // Chuyển hướng hoặc thực hiện các hành động khác sau khi đăng nhập thành công
        },
        error => {
          // Xử lý lỗi nếu có
          console.error(error);
        }
      );
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const formData = new FormData();
      Object.keys(this.loginForm.controls).forEach(key => {
        formData.append(key, this.loginForm.get(key)?.value);
      });
      this.fileList.forEach((file, index) => {
        formData.append(`files`, file as any);
      });

      this.http.post('https://localhost:44302/api/ProductDetail/Create', formData).subscribe(
        (res: any) => {
          if (res.success) {
            this.message.success('Product detail created successfully!');
          } else {
            this.message.error('Failed to create product detail');
          }
        },
        (err) => {
          this.message.error('An error occurred while creating product detail');
        }
      );
    }
  }

  getPageVouchers(page: number, pageSize: number, keywords: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}Voucher/GetPage`, {
      page,
      pageSize,
      keywords
    });
  }
  
  getListRam(): Observable<any> {
    return this.http.get(`${this.apiUrl}GetList`);
  }

  getPagedRam(request: PagedRequest): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<any>(`${this.apiUrl}Ram/GetPaged`, request,{ headers: headers });
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
    return this.http.post(`${this.apiUrl}Ram/CreateOrUpdateAsync`, objDto,{ headers: headers });
  }

  deleteRam(id: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Ram/Delete?id=${id}`,null,{ headers: headers });
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
    return this.http.post<any>(`${this.apiUrl}Role/GetPaged`, request,{ headers: headers });
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
    return this.http.post(`${this.apiUrl}Role/CreateOrUpdateAsync`, objDto,{ headers: headers });
  }

  deleteRole(id: number): Observable<any> {
    const token = this.accountService.getAccessToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.post(`${this.apiUrl}Role/Delete?id=${id}`,null,{ headers: headers });
  }
}
