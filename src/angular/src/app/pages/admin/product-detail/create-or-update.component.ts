import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../shared/services/admin.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { HttpClient } from '@angular/common/http';
import { ProductDetailGetPageDto } from 'src/app/shared/models/model';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import {
  RamDto, CpuDto, CardVGADto, HardDriveDto, ScreenDto, ColorDto,
  ProductDto, DiscountDto
} from '../../../shared/models/model';
import { Router } from '@angular/router';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
@Component({
  selector: 'app-create-or-update',
  templateUrl: './create-or-update.component.html',
  // styleUrls: ['./create-or-update.component.scss']
})
export class CreateOrUpdateProductDetailComponent implements OnInit {
  productDetailForm!: FormGroup;
  listData: ProductDetailGetPageDto[] = [];
  fileList: NzUploadFile[] = [];
  public Editor = ClassicEditor;
  loadRams: RamDto[] = [];
  loadCpus: CpuDto[] = [];
  loadCardVGAs: CardVGADto[] = [];
  loadHardDrives: HardDriveDto[] = [];
  loadScreens: ScreenDto[] = [];
  loadColors: ColorDto[] = [];
  loadProducts: ProductDto[] = [];
  loadDiscounts: DiscountDto[] = [];
  item: ProductDetailGetPageDto | null = null;
  constructor(
    private adminService: AdminService,
    private fb: FormBuilder,
    private message: NzMessageService,
    private http: HttpClient,
    private nzMessageService: NzMessageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadCombobox();
    this.item = history.state.item || null;
    this.productDetailForm = this.fb.group({
      id: [0],
      code: [null, [Validators.required, Validators.maxLength(50)]],
      price: [null, [Validators.required]],
      status: [1, [Validators.required]],
      //  oldPrice: [null, [Validators.required]],
      upgrade: [null],
      ramEntityId: [null],
      discountId: [null],
      productEntityId: [null, [Validators.required]],
      cpuEntityId: [null],
      cardVGAEntityId: [null],
      hardDriveEntityId: [null],
      screenEntityId: [null],
      colorEntityId: [null],
      description: [''],
    });
    if (this.item) {
      console.log(this.item)
      this.productDetailForm.patchValue(this.item);
    }
  }
  loadCombobox(): void {
    this.adminService.getListRam().subscribe(data => {
      this.loadRams = data.data;
    });
    this.adminService.getListCpu().subscribe(data => {
      this.loadCpus = data;
    });
    this.adminService.getListCardVGA().subscribe(data => {
      this.loadCardVGAs = data.data;
    });
    this.adminService.getListHardDrive().subscribe(data => {
      this.loadHardDrives = data;
    });
    this.adminService.getListScreen().subscribe(data => {
      this.loadScreens = data;
    });
    this.adminService.getListColor().subscribe(data => {
      this.loadColors = data;
    });
    this.adminService.getListProduct().subscribe(data => {
      this.loadProducts = data;
    });
    this.adminService.getListDiscount().subscribe(data => {
      this.loadDiscounts = data.data;
    });
  }
  create(): void {

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
  back(): void {
    this.router.navigate(['/product-detail']);
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
  onSubmit(): void {
    if (this.productDetailForm.valid) {
      const obj = this.productDetailForm.value;
      this.adminService.createOrUpdateProductDetail(obj.id, obj.code, obj.description, obj.price, obj.upgrade, obj.productEntityId,
        obj.colorEntityId, obj.ramEntityId, obj.cpuEntityId, obj.hardDriveEntityId, obj.screenEntityId, obj.cardVGAEntityId, obj.discountId,
        obj.status).subscribe(
          (response: any) => {
            console.log(response)
            if (response.succeeded) {
              this.nzMessageService.success(response.messages);
              this.router.navigate(['/product-detail']);
            } else {
              this.nzMessageService.error(response.messages);
            }
          },
          (error) => {
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

