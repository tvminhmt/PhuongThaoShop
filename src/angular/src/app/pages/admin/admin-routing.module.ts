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
import { ImageComponent } from './image/image.component';
import { SerialComponent } from './serial/serial.component';
import { ManageBillComponent } from './manage-bill/manage-bill.component';
import { BillDetailComponent } from './bill-detail/bill-detail.component';
import { CreateOrUpdateProductDetailComponent } from './product-detail/create-or-update.component';
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
     { path: 'image', component: ImageComponent},
     { path: 'role',component: RoleComponent},
     { path: 'serial',component: SerialComponent},
     { path: 'manage-bill',component: ManageBillComponent},
     { path: 'bill-detail/:id',component: BillDetailComponent},
     { path: 'product-detail-create-or-update',component: CreateOrUpdateProductDetailComponent},
    ], 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
