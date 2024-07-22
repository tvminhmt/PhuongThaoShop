import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './pages/admin/login/login.component';

const routes: Routes = [
  {path:'',loadChildren:()=> import('./pages/public/public.module').then((m) => m.PublicModule),},
  {path:'admin',loadChildren:()=> import('./pages/admin/admin.module').then((m) => m.AdminModule),},
  { path: 'login', component: LoginComponent }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
