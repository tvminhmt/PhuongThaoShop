import { Component } from '@angular/core';

@Component({
  selector: 'app-serial',
  templateUrl: './serial.component.html',
  styleUrls: ['./serial.component.scss']
})
export class SerialComponent {
  constructor() {}

  // beforeUpload = (file: File) => {
  //   const isExcel = file.type.includes('spreadsheetml');
  //   if (!isExcel) {
  //     this.msg.error('Bạn chỉ có thể tải lên tệp Excel!');
  //   }
  //   return isExcel || Upload.LIST_IGNORE;
  // }

  handleChange(info: any): void {
    if (info.file.status === 'done') {
    //  this.msg.success(`${info.file.name} tệp đã được tải lên thành công.`);
    } else if (info.file.status === 'error') {
     // this.msg.error(`${info.file.name} tải lên thất bại.`);
    }
  }
}
