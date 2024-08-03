import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { MainComponent } from './main/main.component';
import { RouterModule } from '@angular/router';
import { PublicRoutingModule } from './public-routing.module';
import { AntDesignModule } from 'src/app/shared/ant-design.module';
import { NotfoundComponent } from './notfound/notfound.component';
import { ProductComponent } from './product/product.component';
import { CartComponent } from './cart/cart.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';
import { DetailComponent } from './detail/detail.component';
import { CarouseleComponent } from './carousel';
import { VndFormatPipe } from '../../shared/pipes/vnd-format.pipe'
import { BillComponent } from './bill/bill.component';
import { ShippingComponent } from './shipping/shipping.component';
import { PaymentResultComponent } from './payment-result/payment-result.component';
import { FindBillComponent } from './find-bill/find-bill.component';


@NgModule({
  declarations: [
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    MainComponent,
    NotfoundComponent,
    ProductComponent,
    AboutComponent,
    ContactComponent,
    DetailComponent,
    CarouseleComponent,
    VndFormatPipe,
    BillComponent,
    CartComponent,
    ShippingComponent,
    PaymentResultComponent,
    FindBillComponent
  ],
  providers: [
    VndFormatPipe 
  ],
  imports: [
    CommonModule,
    RouterModule,
    AntDesignModule,
    PublicRoutingModule
  ]
})
export class PublicModule { }
