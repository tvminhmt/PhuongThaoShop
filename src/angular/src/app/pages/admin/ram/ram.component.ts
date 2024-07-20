import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RamDto, PagedRequest } from 'src/app/shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-ram',
  templateUrl: './ram.component.html',
  styleUrls: ['./ram.component.scss']
})
export class RamComponent implements OnInit {
  isVisible = false;
  isSave = false;
  modalTitle = 'Thêm Ram';
  listData: RamDto[] = [];
  fbForm!: FormGroup;
  request: PagedRequest = { skipCount: 0, maxResultCount: 10 };
  listOfCurrentPageData: readonly RamDto[] = [];
  constructor(private adminService: AdminService, private modal: NzModalService, private nzMessageService: NzMessageService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.fbForm = this.fb.group({
      id: '0',
      ma: ['', [Validators.required]],
      thongSo: ['', [Validators.required]]
    });
    this.loadData();
  }

  loadData(): void {
    this.adminService.getPagedRam(this.request).subscribe(response => {
      this.listData = response.items;
    });
  }
  create(): void {
    this.fbForm.reset({
      id: '0'
    });
    this.isVisible = true;
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

  close(): void {
    this.isVisible = false;
  }
  edit(item: RamDto): void {
    this.modalTitle = 'Sửa Ram';
    this.fbForm.patchValue(item);
    this.isVisible = true;
  }

  delete(id: number): void {
    this.adminService.deleteRam(id).subscribe(
      (response: any) => {
        if (response.isSuccessed) {
          this.nzMessageService.success('Thành công');
          this.loadData();
        } else {
          this.nzMessageService.error('Thất bại');
        }
      },
      (error) => {
        this.nzMessageService.error('Thất bại');
        console.error('API call failed:', error);
      }
    );
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
}