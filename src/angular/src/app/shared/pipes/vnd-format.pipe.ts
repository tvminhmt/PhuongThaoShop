import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'vndFormat'
})
export class VndFormatPipe implements PipeTransform {
  transform(value: number): string {
    return value.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
  }
}
