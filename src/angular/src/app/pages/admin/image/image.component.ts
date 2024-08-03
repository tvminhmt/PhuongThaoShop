import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageDto, PagedRequest } from 'src/app/shared/models/model';
import { AppConstants } from 'src/app/constants';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadFile, NzUploadChangeParam } from 'ng-zorro-antd/upload';
export interface UploadFile {
  uid: string;
  name: string;
  status: string;
  response?: string;
  url?: string;
}
@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.scss']
})
export class ImageComponent {
  domainImage = AppConstants.API_URL_IMAGE;
  vouchers = [];
  total = 0;
  loading = true;
  pageSize = 10;
  pageIndex = 1;
  keywords = '';
  fileList: NzUploadFile[] = [];
  apiUrl = 'https://localhost:44302/api/images/upload';
  isVisible = false;
  isSave = false;
  modalTitle = 'Thêm Ram';
  listOfData: ImageDto[] = [];
  fbForm!: FormGroup;
  request: PagedRequest = { skipCount: 0, maxResultCount: 10 };
  listOfCurrentPageData: readonly ImageDto[] = [];
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
    this.loading = true;
    this.adminService.getPageImage(this.pageIndex, this.pageSize, this.keywords).subscribe(data => {
      this.loading = false;
      this.listOfData = data.data;
      console.log(this.listOfData)
      this.total = data.totalCount;
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
  edit(item: ImageDto): void {
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

  handleChange(event: { file: NzUploadFile }): void {
    if (event.file.status === 'done') {
      this.loadData();
      console.log('File uploaded successfully', event.file);
    } else if (event.file.status === 'error') {
      console.error('Error uploading file');
    }
  }
}
