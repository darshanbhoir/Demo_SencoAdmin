using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.Payload;
using Demo_Senco_Admin.Models.ViewModel;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Demo_Senco_Admin.Controllers
{
    public class EMIPaymentController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        

        // GET: EMI Payment Details
        public ActionResult EMIPaymentDashboard(string paymentStatus, DateTime? startdate, DateTime? enddate, string searchFilter, string searchInput)
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         where TSP.temp_swarna_yojna_id == 10822
                         //where TSP.temp_payment_status==false
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
                         }).Take(100);

            var Result = query.ToList();

            var NewResult = Result
                                .Where(s=>string.IsNullOrEmpty(searchFilter) || 
                                    ( s.SchemeNo.ToString() == searchInput && searchFilter=="schemeNo") || 
                                    ( s.CustomerName.Contains(searchInput) && searchFilter=="customerName") || 
                                    ( s.Email.Contains(searchInput) && searchFilter=="email") ||
                                    (s.MobileNo.Contains(searchInput) && searchFilter == "mobile"))
                                //.Where(s => !schemeNo.HasValue || s.SchemeNo == schemeNo)
                                //.Where(s => string.IsNullOrEmpty(customerName) || s.CustomerName.Contains(customerName))
                                //.Where(s => string.IsNullOrEmpty(email) || (s.Email != null && s.Email.Contains(email)))
                                //.Where(s => string.IsNullOrEmpty(mobile) || (s.MobileNo != null && s.MobileNo.Contains(mobile)))
                                .Where(s=> string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus == (paymentStatus.ToLower()=="true" || paymentStatus.ToLower()=="false"))
                                //.Where(s=> string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus== (paymentStatus.ToLower()=="false"))
                                .Where(s=> !startdate.HasValue || s.PaymentDate >= startdate)
                                .Where(s=> !enddate.HasValue|| s.PaymentDate <= enddate)
                                .Select(item => new EMIPaymentViewModel
                                {
                                    OrderNo = item.OrderNo ,
                                    PaymentDate = (DateTime)item.PaymentDate,
                                    EMIno = item.EMIno ?? 0,
                                    Amount = (decimal)item.Amount,
                                    PaymentStatus = item.PaymentStatus ?? false,
                                    SchemeNo = item.SchemeNo,
                                    TransactionId = item.TransactionId ?? "N/A",
                                    BankTransactionId = item.BankTransactionId ?? "N/A",
                                    PaymentEntryNo = item.PaymentEntryNo ?? "N/A",
                                    CustomerName = item.CustomerName ?? "N/A",
                                    MobileNo = item.MobileNo ?? "N/A",
                                    Email = item.Email ?? "N/A",
                                    PaymentReciept = item.PaymentReciept ?? "N/A",
                                }).ToList();


            
            ViewBag.CurrentFilterPaymentStatus = paymentStatus;
            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter = enddate;           
            ViewBag.CurrentFilterSearchFilter = searchFilter;

            //var json = new JavaScriptSerializer().Serialize(Result);
            //var json = JsonConvert.SerializeObject(Result);
            return View("EMIPaymentDashboard", NewResult);
        }



        //Exportb to Excel
        public ActionResult ExporttoExcel()
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         //where TSP.temp_payment_status == false && TSP.temp_payment_status== true
                         select new EMIPaymentViewModel
                         {
                             OrderNo = TSP.temp_swarna_order_no ?? "N/A",
                             PaymentDate = (DateTime)TSP.temp_swarna_paymentdate,
                             EMIno = TSP.temp_swarna_current_emi_no ?? 0,
                             Amount = (decimal)TSP.temp_swarna_payamount,
                             PaymentStatus = TSP.temp_payment_status ?? false,
                             SchemeNo = TSP.temp_swarna_payment_schemeentryno ,
                             TransactionId = TSP.temp_swarna_transaction_id ?? "N/A",
                             BankTransactionId = TSP.temp_swarna_banktransactionid ?? "N/A",
                             PaymentEntryNo = TSP.temp_swarna_paymententryno ?? "N/A",
                             CustomerName = TSP.temp_swarna_customer_name ?? "N/A",
                             MobileNo = TSP.temp_swarna_mobile_no ?? "N/A",
                             Email = TSP.temp_swarna_member_email ?? "N/A",
                             PaymentReciept = TSP.temp_swarna_payment_reciept ?? "N/A",
                         }).Take(10);

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
                "PaymentReciept"
            };

            for(int i=0; i< headers.Length; i++)
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

            return RedirectToAction("EMIPaymentDashboard");


        }



        //View EMI Details
        public ActionResult ViewandModifyDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emidetail = db.tbl_temp_swarna_paymentgateway.Find(id);

            if(emidetail == null)
            {
                HttpNotFound();
            }

            var setting = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate,
            };

            //Console.WriteLine("{ emidetail.temp_swarna_payload_json}");

            if(emidetail.temp_swarna_payload_json!= null)
            {
                var payloadData = JsonConvert.DeserializeObject<List<EMIPayload>>(emidetail.temp_swarna_payload_json, setting);

                var mostRecentEMI = payloadData.OrderBy(s => s.id).FirstOrDefault();


                var viewModel = new EMIPaymentViewModel
                {
                    OrderNo = emidetail.temp_swarna_order_no ?? "N/A",
                    PaymentDate = (DateTime)emidetail.temp_swarna_paymentdate,
                    EMIno = emidetail.temp_swarna_current_emi_no ?? 0,
                    Amount = (decimal)emidetail.temp_swarna_payamount,
                    PaymentStatus = emidetail.temp_payment_status ?? false,
                    SchemeNo = emidetail.temp_swarna_payment_schemeentryno,
                    TransactionId = emidetail.temp_swarna_transaction_id ?? "N/A",
                    BankTransactionId = emidetail.temp_swarna_banktransactionid ?? "N/A",
                    PaymentEntryNo = emidetail.temp_swarna_paymententryno ?? "N/A",
                    CustomerName = emidetail.temp_swarna_customer_name ?? "N/A",
                    MobileNo = emidetail.temp_swarna_mobile_no ?? "N/A",
                    Email = emidetail.temp_swarna_member_email ?? "N/A",
                    PaymentReciept = emidetail.temp_swarna_payment_reciept ?? "N/A",
                    PayloadData = mostRecentEMI,
                };
                return View(viewModel);
            }
            else
            {
                ViewBag.ErrorMessage = "Data is Missing";
                return View("Error");
                //var ErrorMessage = "Data is Missing";
                //ViewBag.AlertMessage = ErrorMessage;
                //return View();
            }
             
            

            
        }

        [HttpPost]
        public ActionResult ViewandModifyDetail(EMIPaymentViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var emidetail = db.tbl_temp_swarna_paymentgateway.Find(viewModel.SchemeNo);
                    if (emidetail == null)
                    {
                        return HttpNotFound();
                    }

                    emidetail.temp_swarna_order_no = viewModel.OrderNo;
                    emidetail.temp_swarna_paymentdate= viewModel.PaymentDate;
                    emidetail.temp_swarna_current_emi_no = viewModel.EMIno;
                    emidetail.temp_swarna_payamount = viewModel.Amount;
                    emidetail.temp_payment_status= viewModel.PaymentStatus;
                    emidetail.temp_swarna_payment_schemeentryno = viewModel.SchemeNo;
                    emidetail.temp_swarna_transaction_id = viewModel.TransactionId;
                    emidetail.temp_swarna_banktransactionid = viewModel.BankTransactionId;
                    emidetail.temp_swarna_paymententryno= viewModel.PaymentEntryNo;
                    emidetail.temp_swarna_customer_name= viewModel.CustomerName;
                    emidetail.temp_swarna_mobile_no= viewModel.MobileNo;
                    //emidetail.temp_swarna_payload_json = viewModel.PayloadData;

                    db.SaveChanges();
                    return RedirectToAction("ViewandModifyDetail");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the detail.");
                }
            }
            return View(viewModel);
        }



        //CreateReciept
        [HttpPost]
        public ActionResult CreateReciept(int? id)
        {
            if(ModelState.IsValid)
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return Json(new { success = false, message = "Scheme number is missing." });
                }

                var detail = db.tbl_temp_swarna_paymentgateway.Find(id);

                if (detail == null)
                {
                    //HttpNotFound();
                    return Json(new { success = false, message = "Record not Found" }, JsonRequestBehavior.AllowGet);
                }
                var viewmodel = new EMIPaymentViewModel
                {
                    SchemeNo= detail.temp_swarna_payment_schemeentryno,
                    CustomerName = detail.temp_swarna_customer_name ?? "N/A",
                    MobileNo = detail.temp_swarna_mobile_no ?? "N/A",
                    EMIno = detail.temp_swarna_current_emi_no ?? 0,
                    Amount = (decimal)detail.temp_swarna_payamount ,
                    TransactionId = detail.temp_swarna_transaction_id ?? "N/A",
                    BankTransactionId = detail.temp_swarna_banktransactionid ?? "N/A",
                    PaymentDate = ((DateTime)detail.temp_swarna_paymentdate),
                    PaymentStatus = detail.temp_payment_status ?? false,
                };

                //storing vewmodel dataa in session to use in ViewReciept method
                Session["RecieptView"] = viewmodel;

                return Json(new { success = true, data = viewmodel }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Validation Failed" });
            
        }
        


        //View Reciept
        public ActionResult ViewReciept()
        {
            var recieptDetail = Session["RecieptView"] as EMIPaymentViewModel;

            if(recieptDetail== null)
            {
                return RedirectToAction("EMIPaymentDashboard");
            }

            return View(recieptDetail);
        }

    }
}