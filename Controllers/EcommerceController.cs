using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Senco_Admin.Controllers
{
    public class EcommerceController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: Ecommerce Data
        public ActionResult Index()
        {
            var query = (from ET in db.tbl_ecommerce_transaction
                         orderby ET.ecommerce_id descending
                         select new
                         {
                             Ecommerce_Id = ET.ecommerce_id,
                             Member_Id = ET.member_id,
                             EcommercePayload = ET.payload,
                             Date_Time = ET.date_time,
                         });
            var Result = query.ToList();

            var PResult = Result
                            .Select(item => new EcommerceViewModel
                            {
                                Ecommerce_Id= item.Ecommerce_Id,
                                Member_Id= (int)item.Member_Id,
                                Name = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.name")?.Value<string>() : null,
                                Email = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.email")?.Value<string>() : null ,
                                Mobile = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.mobile")?.Value<string>() : null ,
                                PaymentDate = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.timestamp")?.Value<string>() : null ,
                                OrderId = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.oid")?.Value<string>() : null ,
                                Amount = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.amount")?.Value<string>() : null ,
                                Status = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.status")?.Value<string>() : null ,
                                //ItemCount= item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("productInfo.items[0].id.Count")?.Value<string>() : null,
                            });
            return Json(PResult, JsonRequestBehavior.AllowGet);
        }
    }
}