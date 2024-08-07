import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PublicService } from '../../../shared/services/public.service';
import { ProductDetailDto, CartItemDto } from '../../../shared/models/model';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/shared/services/account.service';
import { VndFormatPipe } from '../../../shared/pipes/vnd-format.pipe'
import { NzMessageService } from 'ng-zorro-antd/message';
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  quantity: number = 1;
  imageObject!: Array<object>;
  product!: ProductDetailDto;
  productId!: string;
  userName!: string;
  private cartItems: CartItemDto[] = [];
  images: string[] = [];
  constructor(private route: ActivatedRoute, private publicService: PublicService, private router: Router,
    private accountService: AccountService, private location: Location, private vndFormatPipe: VndFormatPipe,
    private nzMessageService: NzMessageService
  ) { }

  ngOnInit(): void {

    this.location.onUrlChange(() => {
      window.scrollTo(0, 0);
    });
    this.productId = this.route.snapshot.params['id']; // Lấy từ route
    if (this.productId) {
      this.publicService.getProductById(this.productId).subscribe(
        (data: any) => {
          this.product = data.data;
          this.images = this.product.listImage;
          console.log(this.product)
        },
        (error) => {
          console.error('Có lỗi xảy ra:', error);
        }
      );
    }
  }
  addToCart(productId: string, quantity: number): void {
    if (this.product.availableQuantity >= quantity) {
      this.userName = this.accountService.getuserName()
      if (this.userName) {
        this.publicService.addProductToCart(this.userName, productId, quantity)
          .subscribe(
            (response) => {
              this.router.navigate(['/cart']);
            },
            (error) => {
              console.error('Xảy ra lỗi khi thêm sản phẩm vào giỏ hàng:', error);
            }
          );
      }
      else {
        function getRandomId(min: number, max: number): number {
          return Math.floor(Math.random() * (max - min + 1)) + min;
        }
        let newItem: CartItemDto = {
          id: getRandomId(1, 1000),
          userId: getRandomId(1, 1000),
          quantity: quantity,
          maProductDetail: this.product.code,
          idProductDetails: Number(this.product.id),
          newPrice: this.product.newPrice,
        };

        const existingCartItems = localStorage.getItem('cartItems');
        if (existingCartItems) {
          this.cartItems = JSON.parse(existingCartItems);
          const existingItemIndex = this.cartItems.findIndex(item => item.idProductDetails === newItem.idProductDetails);
          if (existingItemIndex !== -1) {
            this.cartItems[existingItemIndex].quantity++;
            this.router.navigate(['/cart']);
          } else {
            this.cartItems.push(newItem);
            this.router.navigate(['/cart']);
          }

          localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
        } else {
          this.cartItems = [newItem];
          localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
          this.router.navigate(['/cart']);
        }
      }
    }
    else {
      this.nzMessageService.error('Thêm sản phẩm vào giỏ hàng thất bại. Bạn chỉ có thể thêm tối đa '+this.product.availableQuantity + ' sản phẩm');
    }
  }

  // images: string[] = [
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg',
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg',
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg',
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg',
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg',
  //   'https://laptopaz.vn/media/product/3013_slim_5_2023.jpg'
  // ];

  // selectedImage: string = this.images[0];

  // onSelectImage(image: string): void {
  //   this.selectedImage = image;
  // }
}
