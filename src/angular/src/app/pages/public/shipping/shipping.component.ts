import { Component } from '@angular/core';
import { GHNService } from 'src/app/shared/services/ghn.service';

@Component({
  selector: 'app-shipping',
  templateUrl: './shipping.component.html',
  styleUrls: ['./shipping.component.scss']
})
export class ShippingComponent {
  weight!: number;
  length!: number;
  width!: number;
  height!: number;
  toDistrictID!: number;
  serviceTypeID!: number;
  shippingFee: number | null = null;
  errorMessage: string | null = null;

  constructor(private ghnService: GHNService) {}

  onSubmit(): void {
    this.ghnService.calculateShippingFee(this.weight, this.length, this.width, this.height, this.toDistrictID, this.serviceTypeID)
      .subscribe(
        (response) => {
          this.shippingFee = response.data.total;
          this.errorMessage = null;
        },
        (error) => {
          this.errorMessage = 'Có lỗi xảy ra. Vui lòng thử lại.';
          this.shippingFee = null;
        }
      );
  }
}
