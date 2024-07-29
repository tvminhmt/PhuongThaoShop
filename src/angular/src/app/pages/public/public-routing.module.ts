import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { HomeComponent } from './home/home.component';
import { DetailComponent } from './detail/detail.component';
import { CartComponent } from './cart/cart.component';
import { ProductComponent } from './product/product.component';
import { BillComponent } from './bill/bill.component';
import { PaymentResultComponent } from './payment-result/payment-result.component';
import { FindBillComponent } from './find-bill/find-bill.component';

const routes: Routes = [
  {
    path: '', component: MainComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'laptop-moi.html', component: ProductComponent },
      { path: 'product/:id', component: DetailComponent }, 
      { path: 'hoa-don.html', component: BillComponent },
     // { path: '**', component:  CartComponent },
      { path: 'cart', component: CartComponent },
      { path: 'hoa-don', component: FindBillComponent },
      { path: 'thanh-toan-that-bai', component: PaymentResultComponent },
      // { path: 'list-product', component: ListProductsComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
