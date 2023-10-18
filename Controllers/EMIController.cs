using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.Payload;
using Demo_Senco_Admin.Models.ViewModel;
using Microsoft.Extensions.Primitives;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Senco_Admin.Controllers
{
    public class EMIController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: EMI
        public ActionResult Index(string paymentStatus, DateTime? startdate, DateTime? enddate, string searchFilter, string searchInput)
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         orderby TSP.temp_swarna_yojna_id descending
                         select new
                         {
                             OrderNo = TSP.temp_swarna_order_no,
                             PaymentDate = TSP.temp_swarna_paymentdate,
                             EMIno = TSP.temp_swarna_current_emi_no,
                             Amount = TSP.temp_swarna_payamount,
                             PaymentStatus = TSP.temp_payment_status,
                             SchemeNo = TSP.temp_swarna_payment_schemeentryno,
                             TransactionId = TSP.temp_swarna_transaction_id,
                             BankTransactionId = TSP.temp_swarna_banktransactionid,
                             PaymentEntryNo = TSP.temp_swarna_paymententryno,
                             CustomerName = TSP.temp_swarna_customer_name,
                             MobileNo = TSP.temp_swarna_mobile_no,
                             Email = TSP.temp_swarna_member_email ?? "NA",
                             PaymentReciept = TSP.temp_swarna_payment_reciept,
                             SchemeEntryNo= TSP.temp_swarna_payment_schemeentryno,
                         }).Take(100);

            var Result = query.ToList();

            var NewResult = Result
                                .Where(s => string.IsNullOrEmpty(searchFilter) ||
                                    (s.SchemeNo.ToString() == searchInput && searchFilter == "schemeNo") ||
                                    (s.CustomerName.Contains(searchInput) && searchFilter == "customerName") ||
                                    (s.Email.Contains(searchInput) && searchFilter == "email") ||
                                    (s.MobileNo.Contains(searchInput) && searchFilter == "mobile"))                                
                                .Where(s => string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus == (paymentStatus.ToLower() == "true" || paymentStatus.ToLower() == "false"))                                
                                .Where(s => !startdate.HasValue || s.PaymentDate >= startdate)
                                .Where(s => !enddate.HasValue || s.PaymentDate <= enddate)
                                .Select(item => new EMIPaymentViewModel
                                {
                                    OrderNo = item.OrderNo,
                                    PaymentDate = (DateTime)item.PaymentDate,
                                    EMIno = item.EMIno ?? 0,
                                    Amount = (decimal)item.Amount,
                                    PaymentStatus = item.PaymentStatus ?? false,
                                    SchemeNo = item.SchemeEntryNo,
                                    TransactionId = item.TransactionId ?? "N/A",
                                    BankTransactionId = item.BankTransactionId ?? "N/A",
                                    PaymentEntryNo = item.PaymentEntryNo ?? "N/A",
                                    CustomerName = item.CustomerName ?? "N/A",
                                    MobileNo = item.MobileNo ?? "N/A",
                                    Email = item.Email ?? "N/A",
                                    PaymentReciept = item.PaymentReciept ?? "N/A",
                                    SchemeEntryNo= item.SchemeEntryNo ?? "N/A",
                                }).ToList();



            ViewBag.CurrentFilterPaymentStatus = paymentStatus;
            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter = enddate;
            ViewBag.CurrentFilterSearchFilter = searchFilter;

            
            return View("Index", NewResult);
        }


        //Export to Excel
        public ActionResult ExporttoExcel()
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no                        
                         select new EMIPaymentViewModel
                         {
                             OrderNo = TSP.temp_swarna_order_no ?? "N/A",
                             PaymentDate = (DateTime)TSP.temp_swarna_paymentdate,
                             EMIno = TSP.temp_swarna_current_emi_no ?? 0,
                             Amount = (decimal)TSP.temp_swarna_payamount,
                             PaymentStatus = TSP.temp_payment_status ?? false,
                             SchemeNo = TSP.temp_swarna_payment_schemeentryno,
                             TransactionId = TSP.temp_swarna_transaction_id ?? "N/A",
                             BankTransactionId = TSP.temp_swarna_banktransactionid ?? "N/A",
                             PaymentEntryNo = TSP.temp_swarna_paymententryno ?? "N/A",
                             CustomerName = TSP.temp_swarna_customer_name ?? "N/A",
                             MobileNo = TSP.temp_swarna_mobile_no ?? "N/A",
                             Email = TSP.temp_swarna_member_email ?? "N/A",
                             
                         });

            var data = query.ToList();

            ExcelPackage excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            //for headers
            var headers = new string[]
            {
                "OrderNo",
                "PaymentDate",
                "EMIno",
                "Amount",
                "PaymentStatus",
                "SchemeNo",
                "TransactionId",
                "BankTransactionId",
                "PaymentEntryNo",
                "CustomerName",
                "MobileNo",
                "Email",
                
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            //for data
            int row = 2;
            foreach (var item in data)
            {
                int column = 1;

                foreach (var prop in typeof(EMIPaymentViewModel).GetProperties())
                {
                    worksheet.Cells[row, column].Value = prop.GetValue(item);
                    column++;
                }
                row++;
            }

            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);

            stream.Position = 0;

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("Content-Disposition", new StringValues("attachment; filename=ExportedEMIPaymentData.xlsx"));

            stream.CopyTo(Response.OutputStream);

            Response.Flush();

            Response.End();

            return RedirectToAction("Index");


        }
    }
}