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

  constructor(private accountService: AccountService,private fb: FormBuilder,private router: Router ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submitForm(): void {
    if (this.loginForm.valid) {
      const username = this.loginForm.get('username')!.value;
      const password = this.loginForm.get('password')!.value;
      
      this.accountService.login(username, password).subscribe(
        response => {
          // Xử lý phản hồi từ AuthService nếu cần
          if(response.isAdmin){
            this.router.navigateByUrl('/main');
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
}
