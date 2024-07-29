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
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submitForm(): void {
    if (this.loginForm.valid) {
      const userName = this.loginForm.get('userName')!.value;
      const password = this.loginForm.get('password')!.value;
      
      this.accountService.login(userName, password).subscribe(
        response => { 
          console.log(response);
          if(response.isAdmin){
            this.router.navigateByUrl('/main');
          }
         
        },
        error => {
          // Xử lý lỗi nếu có
          console.error(error);
        }
      );
    }
  }
}
