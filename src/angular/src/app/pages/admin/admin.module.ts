import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SlidebarComponent } from './slidebar/slidebar.component';
import { AntDesignModule } from 'src/app/shared/ant-design.module';
import { RouterModule } from '@angular/router';
import { AdminRoutingModule } from './admin-routing.module';
import { VoucherComponent } from './voucher/voucher.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductComponent } from './product/product.component';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { RamComponent } from './ram/ram.component';
import { RoleComponent } from './role/role.component';
import { ImageComponent } from './image/image.component';
import { SerialComponent } from './serial/serial.component';
import { ManageBillComponent } from './manage-bill/manage-bill.component';
import { BillDetailComponent } from './bill-detail/bill-detail.component';
import { CreateOrUpdateProductDetailComponent } from './product-detail/create-or-update.component';



@NgModule({
  declarations: [
    DashboardComponent,
    SlidebarComponent,
    VoucherComponent,
    ProductDetailComponent,
    ProductComponent,
    MainComponent,
    LoginComponent,
    RamComponent,
    RoleComponent,
    ImageComponent,
    SerialComponent,
    ManageBillComponent,
    BillDetailComponent,
    CreateOrUpdateProductDetailComponent
  ],
  imports: [
    CommonModule,
    AntDesignModule,
    AdminRoutingModule,
    RouterModule
  ]
})
export class AdminModule { }
