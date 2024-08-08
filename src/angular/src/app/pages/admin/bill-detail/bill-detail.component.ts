import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BillDetail, PagedRequest, ProductDetailDto, SerialDto } from 'src/app/shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-bill-detail',
  templateUrl: './bill-detail.component.html',
  styleUrls: ['./bill-detail.component.scss']
})
export class BillDetailComponent implements OnInit {
  isVisible = false;
  isSave = false;
  isAnotherFormVisible = false;
  modalTitle = 'Thêm sản phẩm vào đơn hàng';
  fbForm!: FormGroup;
  serialForm!: FormGroup;
  idBill!: number;
  statusBill!: number;
  billDetailId!: number;
  productDetails: ProductDetailDto[] = [];
  billDetails: BillDetail[] = [];
  serials: SerialDto[] = [];
  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private route: ActivatedRoute,
    private nzMessageService: NzMessageService
  ) { }

  ngOnInit(): void {

    this.fbForm = this.fb.group({
      id: [0],
      codeProductDetail: ['', [Validators.required]],
      quantity: [0, [Validators.required]],
    });
    this.serialForm = this.fb.group({
    //  serialNumber:['',Validators.required]
    serialNumber: [[]] 
    });

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      const numericId = Number(id); 

      if (numericId && !isNaN(numericId)) {
        this.idBill = numericId;
        this.loadBillDetails(this.idBill);
      } else {
        this.nzMessageService.error('ID hóa đơn không hợp lệ');
      }
    });
    this.loadCombobox();
  }

  loadBillDetails(billId: number): void {
    this.adminService.getBillDetailsByBillId(this.idBill).subscribe(response => {
      this.billDetails = response.data;
    });
  }
  loadCombobox(): void {
    this.adminService.getListProductDetail().subscribe(data => {
      this.productDetails = data.data;
    });
    this.adminService.getListSerial().subscribe(data => {
      this.serials = data.data;
    });
    console.log(this.serials)
    this.adminService.getBillById(this.idBill).subscribe(
      (billResponse: any) => {
        if (billResponse.succeeded) {
          this.statusBill = billResponse.data.status;
        }
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
      this.adminService.createOrUpdateBillDetail(obj.id, this.idBill, obj.codeProductDetail, obj.quantity).subscribe(
        (response: any) => {
          console.log(response)
          if (response.succeeded) {
            this.nzMessageService.success(response.messages);
            this.isSave = false;
            this.isVisible = false;
            this.loadBillDetails(this.idBill);
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
  edit(item: BillDetail): void {
    this.modalTitle = 'Sửa Hóa Đơn';
    console.log("Edited item:", item); // Debugging line
    console.log("Form value after patching:", this.fbForm.value); // Debugging line
    this.fbForm.patchValue(item);
    this.isVisible = true;
  }

  delete(id: number): void {
    this.adminService.deleteBillDetail(id).subscribe(
      () => {
        this.nzMessageService.success('Xóa thành công');
        this.loadBillDetails(this.idBill);
      },
      (error) => {
        this.nzMessageService.error('Xóa thất bại');
        console.error('API call failed:', error);
      }
    );
  }

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
  openAnotherForm(id: number) {
    this.billDetailId = id;
    this.isAnotherFormVisible = true;
  }

  closeAnotherForm() {
    this.isAnotherFormVisible = false;
  }

  saveAnotherForm() {
    if (this.serialForm.valid) {
      const obj = this.serialForm.value;
      console.log(obj.serialNumber)
      console.log(this.billDetailId);
      this.adminService.updateSerial(obj.serialNumber,this.billDetailId).subscribe(
        (response: any) => {
          console.log(response)
          if (response.succeeded) {
            this.nzMessageService.success(response.messages);
            this.isAnotherFormVisible = true;
            this.serialForm.reset({ id: '0' });
          } else {
            this.nzMessageService.error(response.messages);
            this.isAnotherFormVisible = false;
          }
        },
        (error) => {
          this.isAnotherFormVisible = false;
          this.nzMessageService.error('Thất bại');
          console.error('API call failed:', error);
        }
      );
    }
    else {
      this.nzMessageService.error('Hãy nhập đầy đủ giá trị');
    }
  }
}
