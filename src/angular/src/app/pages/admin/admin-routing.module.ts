import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SlidebarComponent } from './slidebar/slidebar.component';
import { VoucherComponent } from './voucher/voucher.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductComponent } from './product/product.component';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from 'src/app/shared/services/auth-guard.service';
import { RamComponent } from './ram/ram.component';
import { RoleComponent } from './role/role.component';
const routes: Routes = [
  {
    //canActivate: [AuthGuard],
    path: '',canActivate: [AuthGuard],  component: DashboardComponent,
    children: [
      { path: 'login', component: LoginComponent},
     { path: 'main', component: MainComponent},
     { path: 'voucher', component: VoucherComponent},
     { path: 'product', component: ProductComponent},
     { path: 'product-detail', component: ProductDetailComponent},
     { path: 'ram', component: RamComponent},
     {path: 'role',component: RoleComponent},
    ], 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
