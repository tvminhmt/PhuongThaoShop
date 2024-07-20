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
}
