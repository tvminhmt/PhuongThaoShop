import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../shared/services/account.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isAdminLogin: boolean = false;

  constructor(private accountService: AccountService,private fb: FormBuilder,private router: Router ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submitFormWithAdmin(): void {
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')!.value;
      const password = this.loginForm.get('password')!.value;
      
      this.accountService.login(username, password).subscribe(
        response => {
          // Xử lý phản hồi từ AuthService nếu cần
          if(response.isAdmin){
            this.router.navigateByUrl('/main');
            this.isAdminLogin = true
          }
          console.log(response);
          // Chuyển hướng hoặc thực hiện các hành động khác sau khi đăng nhập thành công
        },
        error => {
          // Xử lý lỗi nếu có
          console.error(error);
        }
      );
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const formData = new FormData();
      Object.keys(this.loginForm.controls).forEach(key => {
        formData.append(key, this.loginForm.get(key)?.value);
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
}
