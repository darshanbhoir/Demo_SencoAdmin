using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Demo_Senco_Admin.Controllers
{
    public class EMIPaymentController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();

        // GET: EMI Payment Details
        public ActionResult EMIPaymentDashboard(int? schemeNo,string customerName, string email, string mobile, string paymentStatus, DateTime? startdate, DateTime? enddate)
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         //where TSP.temp_payment_status == false && TSP.temp_payment_status== true
                         select new 
                         {
                             OrderNo = TSP.temp_swarna_order_no,
                             PaymentDate = TSP.temp_swarna_paymentdate,
                             EMIno = TSP.temp_swarna_current_emi_no,
                             Amount = TSP.temp_swarna_payamount,
                             PaymentStatus = TSP.temp_payment_status,
                             SchemeNo = TSP.temp_swarna_yojna_id,
                             TransactionId = TSP.temp_swarna_transaction_id,
                             BankTransactionId = TSP.temp_swarna_banktransactionid,
                             PaymentEntryNo = TSP.temp_swarna_paymententryno,
                             CustomerName = TSP.temp_swarna_customer_name,
                             MobileNo = TSP.temp_swarna_mobile_no,
                             Email = TSP.temp_swarna_member_email,
                             PaymentReciept = TSP.temp_swarna_payment_reciept,
                         }).Take(10);

            var Result = query.ToList();

            var NewResult = Result
                                .Where(s=> !schemeNo.HasValue || s.SchemeNo== schemeNo)
                                .Where(s=> string.IsNullOrEmpty(customerName) || s.CustomerName.Contains(customerName))
                                .Where(s=> string.IsNullOrEmpty(email) || (s.Email != null && s.Email.Contains(email)))
                                .Where(s=> string.IsNullOrEmpty(mobile) || (s.MobileNo != null && s.MobileNo.Contains(mobile)))
                                .Where(s=> string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus == (paymentStatus.ToLower()=="true" || paymentStatus.ToLower()=="false"))
                                //.Where(s=> string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus== (paymentStatus.ToLower()=="false"))
                                .Where(s=> !startdate.HasValue || s.PaymentDate >= startdate)
                                .Where(s=> !enddate.HasValue|| s.PaymentDate <= enddate)
                                .Select(item => new EMIPaymentViewModel
                                {
                                    OrderNo = item.OrderNo,
                                    PaymentDate = (DateTime)item.PaymentDate,
                                    EMIno = item.EMIno,
                                    Amount = (decimal)item.Amount,
                                    PaymentStatus = item.PaymentStatus,
                                    SchemeNo = item.SchemeNo,
                                    TransactionId = item.TransactionId,
                                    BankTransactionId = item.BankTransactionId,
                                    PaymentEntryNo = item.PaymentEntryNo,
                                    CustomerName = item.CustomerName,
                                    MobileNo = item.MobileNo,
                                    Email = item.Email,
                                    PaymentReciept = item.PaymentReciept,
                                }).ToList();


            ViewBag.CurrentFilter = customerName;
            ViewBag.CurrentFilter = email;
            ViewBag.CurrentFilter = mobile;
            ViewBag.CurrentFilter = paymentStatus;
            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter = enddate;
            ViewBag.CurrentFilter = schemeNo;

            //var json = new JavaScriptSerializer().Serialize(Result);
            //var json = JsonConvert.SerializeObject(Result);
            return View("EMIPaymentDashboard", NewResult);
        }
    }
}