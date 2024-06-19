import { Component,OnInit } from '@angular/core';
import { PublicService } from '../../../shared/services/public.service';
import { ProductDetailDto } from '../../../shared/models/model';
import { VndFormatPipe } from '../../../shared/pipes/vnd-format.pipe'
@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent {
  //products: ProductDetailDto[] = [];
  products: ProductDetailDto[] = [];
  currentPage = 1;
  pageSize = 10;
  totalCount = 0;

  filters: any = {
    manufacturer: '',
    productType: '',
    ram: '',
    cpu: '',
    cardVGA: '',
    hardDrive: '',
    screen: '',
    search: '',
    sortBy: ''
  };
  constructor(private publicService: PublicService,  private vndFormatPipe: VndFormatPipe) { }

  ngOnInit(): void {
    this.loadProducts();
  }
  loadProducts(): void {
    this.publicService.getListProducts(this.currentPage, this.pageSize, this.filters.ram).subscribe({
      next: data => {
        this.products = data.data;
        console.log(this.products)
        this.totalCount = data.totalCount;
      },
      error: err => {
        console.error('Error loading products:', err);
      }
    });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProducts();
  }

  onFilterChange(): void {
    this.loadProducts();
  }

  // loadProducts(): void {
  //   this.publicService.getListProducts(this.currentPage, this.pageSize).subscribe(data => {
  //     this.products = data.data;
  //     this.totalCount = data.totalCount;
  //   });
  // }

  // onPageChange(page: number): void {
  //   this.currentPage = page;
  //   this.loadProducts();
  // }
  // loadProducts(): void {
  //   this.publicService.getProducts().subscribe(
  //     (data: ProductDetailDto[]) => {
  //       this.products = data;

  //       console.log(this.products)
  //     },
  //     (error) => {
  //       console.error('Error fetching products:', error);
  //     }
  //   );
  // }
}
