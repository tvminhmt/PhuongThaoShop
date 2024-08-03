import { Component,OnInit } from '@angular/core';
import { PublicService } from '../../../shared/services/public.service';
import { ProductDetailDto } from '../../../shared/models/model';
import { VndFormatPipe } from '../../../shared/pipes/vnd-format.pipe'
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  products: ProductDetailDto[] = [];

  constructor(private publicService: PublicService,  private vndFormatPipe: VndFormatPipe) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.publicService.getProducts().subscribe(
      (data: ProductDetailDto[]) => {
        this.products = data;

        console.log(this.products)
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }
}
