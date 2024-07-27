import { Component, OnInit, HostListener, ElementRef, ViewChild, Output, EventEmitter } from '@angular/core';
import { PublicService } from '../../../shared/services/public.service';
import { CartItemDto, RequestBillDto, ResponseDto, VoucherDto, } from '../../../shared/models/model';
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from '@angular/forms';
import { AccountService } from 'src/app/shared/services/account.service';
import { NzMessageService } from 'ng-zorro-antd/message';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  isVisible = false;
  isOkLoading = false;
  username: string | null = null;
  totalCartItem !: number ;
  constructor( private publicService: PublicService,private accountService: AccountService,private fb: NonNullableFormBuilder, private nzMessageService: NzMessageService) {
    const currentUserString = localStorage.getItem('currentUser');
    if (currentUserString) {
      const currentUser = JSON.parse(currentUserString);
      this.username = currentUser.username;
    }
  }
  @Output() refreshNavbar = new EventEmitter<void>();

  // Hàm này sẽ được gọi khi nút refresh được nhấn
  onRefreshClick() {
    this.refreshNavbar.emit();
  }
  ngOnInit(): void {
    this.loadTotalCart()
  }
  showModal(): void {
    
    this.isVisible = true;
  }
  handleCancel(): void {
    this.isVisible = false;
  }
  loginForm: FormGroup<{
    username: FormControl<string>;
    password: FormControl<string>;
    remember: FormControl<boolean>;
  }> = this.fb.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
    remember: [true]
  });

  submitForm(): void {
    if (this.loginForm.valid) {
      console.log('submit', this.loginForm.value);
      const username = this.loginForm.get('username')!.value;
      const password = this.loginForm.get('password')!.value;
      
      this.accountService.login(username, password).subscribe(
        response => {
          if(response.isSuccess){
            window.location.reload();
          }
          this.nzMessageService.info('Tài khoản hoặc mật khẩu sai ');
        },
        error => {
          this.nzMessageService.info('Tài khoản hoặc mật khẩu sai ');
          console.error(error);
        }
      );
    } else {
      Object.values(this.loginForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
  
  logOut(): void {
   this.accountService.logout();
   window.location.reload();
  }
  loadTotalCart(): void {
    this.username = this.accountService.getUsername();
    if (this.username) {
      this.publicService.getCartByUser().subscribe(
        (data: CartItemDto[]) => {
          this.totalCartItem = data.length;
        },
        (error) => {
          console.error('Error fetching products:', error);
        }
      );
    }
    else {
      const cartItemsString = localStorage.getItem('cartItems');
      if (cartItemsString) {
        this.totalCartItem = JSON.parse(cartItemsString).length;
      }
    }
  }
}
