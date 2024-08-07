import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BillGetPageDto, PagedRequest } from 'src/app/shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-manage-bill',
  templateUrl: './manage-bill.component.html',
  styleUrls: ['./manage-bill.component.scss']
})
export class ManageBillComponent implements OnInit {
  isVisible = false;
  isSave = false;
  modalTitle = 'Thêm Hóa Đơn';
  listData: BillGetPageDto[] = [];
  fbForm!: FormGroup;
  searchKeyword = '';
  private intervalId: any;
  totalCount!: number;
  totalStatus!: number;
  totalStatus2!: number;
  totalStatus3!: number;
  totalStatus4!: number;
  totalStatus5!: number;
  totalStatus6!: number;
  totalStatus7!: number;
  totalStatus8!: number;
  status = 0;
  listOfCurrentPageData: readonly BillGetPageDto[] = [];
  constructor(private adminService: AdminService, private router: Router, private modal: NzModalService, private nzMessageService: NzMessageService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.fbForm = this.fb.group({
      id: '0',
      fullName: ['', [Validators.required]],
      address: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      payment: [null, [Validators.required]],
      isPayment: [false, [Validators.required]],
      status: [null, [Validators.required]],
    });
    this.loadData();
    this.intervalId = setInterval(() => {
      this.loadData();
    }, 300000);
  }
  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
  loadData(): void {
    this.adminService.getPageBill(1, 99999, this.searchKeyword,this.status).subscribe(response => {
      console.log(response.data)
      this.listData = response.data;
      this.totalCount = response.totalCount;
      this.totalStatus = response.totalStatus;
      this.totalStatus2 = response.totalStatus2;
      this.totalStatus3 = response.totalStatus3;
      this.totalStatus4 = response.totalStatus4;
      this.totalStatus5 = response.totalStatus5;
      this.totalStatus6 = response.totalStatus6;
      this.totalStatus7 = response.totalStatus7;
      this.totalStatus8 = response.totalStatus8;
    });
  }
  search(): void {
    this.loadData();  // Reload and filter the data based on the keyword
  }
  searchStatus(status:number): void {
    this.status = status;
    this.loadData(); 
  }
  create(): void {
    this.modalTitle = 'Thêm Hóa Đơn';
    this.fbForm.reset({
      id: '0'
    });
    this.isVisible = true;
  }
  save(): void {
    if (this.fbForm.valid) {
      const obj = this.fbForm.value;
      this.isSave = true;
      this.adminService.createOrUpdateBill(obj.id, obj.fullName, obj.address, obj.phoneNumber, obj.payment, obj.isPayment, obj.status).subscribe(
        (response: any) => {
          console.log(response)
          if (response.succeeded) {
            this.nzMessageService.success(response.messages);
            this.isSave = false;
            this.isVisible = false;
            this.loadData();
            this.fbForm.reset({ id: '0' });
          } else {
            this.nzMessageService.error(response.messages);
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
    else {
      this.nzMessageService.error('Hãy nhập đầy đủ giá trị');
    }
  }

  close(): void {
    this.isVisible = false;
  }
  cancel(): void {
    this.nzMessageService.info('Bạn đã hủy thao tác');
  }
  edit(item: BillGetPageDto): void {
    this.modalTitle = 'Sửa Hóa Đơn';
    this.fbForm.patchValue(item);
    this.isVisible = true;
  }

  delete(id: number): void {
    this.adminService.deleteBill(id).subscribe(
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
  openBillDetails(id: number): void {
    this.router.navigate(['/bill-detail', id]);
  }
}
