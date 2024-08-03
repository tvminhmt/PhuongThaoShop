import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { PublicService } from '../../../shared/services/public.service';
@Component({
  selector: 'app-find-bill',
  templateUrl: './find-bill.component.html',
  styleUrls: ['./find-bill.component.scss']
})
export class FindBillComponent implements OnInit {
  billData: any;
   billCode !: string;
  constructor(private route: ActivatedRoute, private publicService: PublicService) { }

  ngOnInit(): void {
    this.loadData();
   
  }
  loadData(): void {
    this.route.queryParams.subscribe(params => 
      this.billCode = params['codeBill']
    );
    this.publicService.getBillByInvoiceCode(this.billCode).subscribe(
      (billResponse: any) => {
        if (billResponse.isSuccessed) {
                this.billData = billResponse.resultObj;
                console.log(this.billData)
        }
      });
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
