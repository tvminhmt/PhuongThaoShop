import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html',
  styleUrls: ['./bill.component.scss']
})
export class BillComponent implements OnInit{
  billData: any;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.billData = history.state.billData;
    console.log(this.billData); // You can use this data in your component template
  }
  downloadPDF(): void {
    const data = document.querySelector('.printInvoice') as HTMLElement;
    if (data) {
      html2canvas(data).then(canvas => {
        const imgWidth = 208;
        const imgHeight = canvas.height * imgWidth / canvas.width;
        const contentDataURL = canvas.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4'); // Kích thước trang A4 của PDF
        const position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
        pdf.save('invoice.pdf'); // Lưu PDF đã tạo
      });
    }
  }
  
  // print(): void {
  //   const printContents = document.querySelector('.invoice')?.innerHTML;
  //   const originalContents = document.body.innerHTML;
  //   if (printContents) {
  //     document.body.innerHTML = printContents;
  //     window.print();
  //     document.body.innerHTML = originalContents;
  //     window.location.reload(); // Reload the page to reset the content
  //   }
  // }
  print(): void {
    const printContents = document.querySelector('.printInvoice')?.innerHTML;
    const originalContents = document.body.innerHTML;
    if (printContents) {
      document.body.innerHTML = printContents;
      window.print();
      document.body.innerHTML = originalContents;
      window.location.reload(); // Tải lại trang để khôi phục nội dung ban đầu
    }
  }
}
