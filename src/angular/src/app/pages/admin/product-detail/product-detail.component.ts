import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from '../../../shared/services/admin.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { HttpClient } from '@angular/common/http';
import { ProductDetailGetPageDto } from 'src/app/shared/models/model';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  productDetailForm!: FormGroup;
  listData: ProductDetailGetPageDto[] = [];
  fileList: NzUploadFile[] = [];
  public Editor = ClassicEditor;
  constructor(
    private adminService: AdminService,
    private fb: FormBuilder,
    private message: NzMessageService,
    private http: HttpClient,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadData();
  }
  loadData(): void {
    this.adminService.getPageProductDetail(1, 30, '').subscribe(response => {
      console.log(response.data)
      this.listData = response.data;
    });
  }
  create(): void {
    this.router.navigate(['/product-detail-create-or-update']);
  }
  edit(item: ProductDetailGetPageDto): void {
    this.router.navigate(['/product-detail-create-or-update'], { state: { item } });
  }
  delete(id: number): void {
    // this.adminService.deleteBill(id).subscribe(
    //   (response: any) => {
    //     if (response.isSuccessed) {
    //       this.nzMessageService.success('Thành công');
    //       this.loadData();
    //     } else {
    //       this.nzMessageService.error('Thất bại');
    //     }
    //   },
    //   (error) => {
    //     this.nzMessageService.error('Thất bại');
    //     console.error('API call failed:', error);
    //   }
    // );
  }
}
