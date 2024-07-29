import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  BillDetail, PagedRequest } from 'src/app/shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bill-detail',
  templateUrl: './bill-detail.component.html',
  styleUrls: ['./bill-detail.component.scss']
})
export class BillDetailComponent implements OnInit {
  fbForm!: FormGroup;
  idBill!: number;
  billDetails: BillDetail[] = [];
  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private route: ActivatedRoute,
    private nzMessageService: NzMessageService
  ) {}

  ngOnInit(): void {
    this.fbForm = this.fb.group({
      id: [0],
      code: ['', [Validators.required]],
      codeProductDetail: [''],
      quantity: [0, [Validators.required]],
      price: [0, [Validators.required]],
      billEntityId: [0, [Validators.required]],
      crUserId: [0]
    });

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      const numericId = Number(id); // Chuyển đổi giá trị tham số thành số

      if (numericId && !isNaN(numericId)) {
        this.idBill = numericId;
        this.loadBillDetails(this.idBill);
      } else {
        this.nzMessageService.error('ID hóa đơn không hợp lệ');
      }
    });
  }

  loadBillDetails(billId: number): void {
    console.log(billId)
      this.adminService.getBillDetailsByBillId(billId).subscribe(response => {
      console.log(response.data)
      this.billDetails = response.data;
    });
  }

  // save(): void {
  //   if (this.fbForm.valid) {
  //     const billDetail: BillDetail = this.fbForm.value;
  //     if (billDetail.id === 0) {
  //       this.billDetailService.createBillDetail(billDetail).subscribe(
  //         () => {
  //           this.nzMessageService.success('Tạo thành công');
  //           this.loadBillDetails();
  //         },
  //         (error) => {
  //           this.nzMessageService.error('Tạo thất bại');
  //           console.error('API call failed:', error);
  //         }
  //       );
  //     } else {
  //       this.billDetailService.updateBillDetail(billDetail.id, billDetail).subscribe(
  //         () => {
  //           this.nzMessageService.success('Cập nhật thành công');
  //           this.loadBillDetails();
  //         },
  //         (error) => {
  //           this.nzMessageService.error('Cập nhật thất bại');
  //           console.error('API call failed:', error);
  //         }
  //       );
  //     }
  //   }
  // }

  // delete(): void {
  //   const id = this.fbForm.get('id')?.value;
  //   if (id !== 0) {
  //     this.billDetailService.deleteBillDetail(id).subscribe(
  //       () => {
  //         this.nzMessageService.success('Xóa thành công');
  //         this.loadBillDetails();
  //       },
  //       (error) => {
  //         this.nzMessageService.error('Xóa thất bại');
  //         console.error('API call failed:', error);
  //       }
  //     );
  //   }
  // }

  reset(): void {
    this.fbForm.reset({
      id: 0,
      code: '',
      codeProductDetail: '',
      quantity: 0,
      price: 0,
      billEntityId: this.idBill,
      crUserId: 0
    });
  }
}
