using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Bill.DTOs
{
   public class BillGetPageDto : BillDto
    {
        public int Stt {  get; set; }   
        public string StrPayment { get; set; }  
        public string StrStatus { get; set; }
        public string StrIsPayment { get; set; }
        public string CrUserName { get; set; }
        public string UpdUserName { get; set; }

    }
}
