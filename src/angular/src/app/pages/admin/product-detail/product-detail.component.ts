import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { HttpClient } from '@angular/common/http';
import { NzUploadFile } from 'ng-zorro-antd/upload';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit{
  productDetailForm!: FormGroup;
  fileList: NzUploadFile[] = [];

  constructor(
    private fb: FormBuilder,
    private message: NzMessageService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.productDetailForm = this.fb.group({
      code: [null, [Validators.required, Validators.maxLength(50)]],
      price: [null, [Validators.required]],
      oldPrice: [null, [Validators.required]],
      productEntityId: [null, [Validators.required]],
      // Add more form controls as needed
    });
  }

  beforeUpload = (file: NzUploadFile): boolean => {
    this.fileList = this.fileList.concat(file);
    return false;
  };

  handleChange(info: { file: NzUploadFile }): void {
    if (info.file.status === 'done') {
      this.message.success(`${info.file.name} file uploaded successfully`);
    } else if (info.file.status === 'error') {
      this.message.error(`${info.file.name} file upload failed`);
    }
  }

  onSubmit(): void {
    if (this.productDetailForm.valid) {
      const formData = new FormData();
      Object.keys(this.productDetailForm.controls).forEach(key => {
        formData.append(key, this.productDetailForm.get(key)?.value);
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

  handleChangeProduct(info: { file: NzUploadFile }): void {
    if (info.file.status === 'done') {
      this.message.success(`${info.file.name} file uploaded successfully`);
    } else if (info.file.status === 'error') {
      this.message.error(`${info.file.name} file upload failed`);
    }
  }

  save(): void {
    if (this.fbForm.valid) {
      const obj = this.fbForm.value;
      this.isSave = true;
      this.adminService.createOrUpdateRam(obj).subscribe(
        (response: any) => {
          if (response.isSuccessed) {
            this.nzMessageService.success('Thành công');
            this.isSave = false;
            this.isVisible = false;
            this.loadData();
            this.fbForm.reset({ id: '0' });
          } else {
            this.nzMessageService.error('Thất bại');
            this.isSave = false;
          }
        },
        (error) => {
          this.isSave = false;
          this.nzMessageService.error('Thất bại');
          console.error('API call failed:', error);
        }
      );
    }
  }

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

  isVisible = false;
  isSave = false;
  modalTitle = 'Thêm Role';
  listData: RoleDto[] = [];
  fbForm!: FormGroup;
  request: PagedRequest = { skipCount: 0, maxResultCount: 10 }; 
  constructor(private adminService: AdminService,private modal: NzModalService,private nzMessageService: NzMessageService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.fbForm = this.fb.group({
      id: '0',
      ma: ['', [Validators.required]],
      thongSo: ['', [Validators.required]]
    });
    this.loadData();
  }

  loadData(): void {
    this.adminService.getPagedRole(this.request).subscribe(response => {
      this.listData = response.items;
    });
  }
  openModal(): void {
   // this.fbForm.reset();
    this.isVisible = true;
  }
  save(): void {
    if (this.fbForm.valid) {
      const obj = this.fbForm.value;
      this.isSave = true; 
      this.adminService.createOrUpdateRole(obj).subscribe(
        () => {
          this.nzMessageService.success('Thành công');
          this.isSave = false; 
          this.isVisible = false;
          this.loadData(); 
          this.fbForm.reset();
        },
        () => {
          this.isSave = false;
          this.nzMessageService.error('Thất bại');
        }
      );
    }
  }
  close(): void {
    this.isVisible = false;
  }
  edit(item: RoleDto): void {
    this.modalTitle = 'Sửa Role';
    this.fbForm.patchValue(item);
    this.isVisible = true;
  }

  delete(id: number): void {
    this.adminService.deleteRole(id).subscribe(() => {
      this.nzMessageService.success('Thành công');
      this.loadData();
    }, error => {
      this.nzMessageService.error('Thất bại');
    });
  }
}
