using Demo_Senco_Admin.Models;
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
        public string EMiPaymentDashboard()
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         where TSP.temp_payment_status == false
                         select new
                         {
                             OrderNo = TSP.temp_swarna_order_no,
                             PaymentDate = TSP.temp_swarna_paymentdate,
                             EMIno = TSP.temp_swarna_current_emi_no,
                             Amount = TSP.temp_swarna_payamount,
                             PaymentStatus = TSP.temp_payment_status,
                             TransactionId = TSP.temp_swarna_transaction_id,
                             BankTransactionId = TSP.temp_swarna_banktransactionid,
                             PaymentEntryNo = TSP.temp_swarna_paymententryno,
                             CustomerName = TSP.temp_swarna_customer_name,
                             MobileNo = TSP.temp_swarna_mobile_no,
                             Email = TSP.temp_swarna_member_email,
                             PaymentReciept = TSP.temp_swarna_payment_reciept,
                         });

            var Result = query.ToList();
            //var json = new JavaScriptSerializer().Serialize(Result);
            var json = JsonConvert.SerializeObject(Result);
            return json;
        }
    }
}