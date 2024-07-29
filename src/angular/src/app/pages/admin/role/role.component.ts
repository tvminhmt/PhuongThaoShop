import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../shared/services/admin.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoleDto, PagedRequest } from 'src/app/shared/models/model';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {
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

