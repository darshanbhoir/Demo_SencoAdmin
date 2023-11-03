using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.Payload;
using Demo_Senco_Admin.Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Demo_Senco_Admin.Controllers
{
    public class EcommerceController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: Ecommerce Data
        public ActionResult Index(int? Page_No, DateTime? startdate, DateTime? enddate, string searchFilter, string searchInput, string status)
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
                            .Select(item => new
                            {
                                Ecommerce_Id = item.Ecommerce_Id,
                                Member_Id = (int)item.Member_Id,
                                Date_Time = item.Date_Time,
                                Name = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.name")?.Value<string>() : null,
                                Email = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.email")?.Value<string>() : null,
                                Mobile = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("customerInfo.mobile")?.Value<string>() : null ,
                                PaymentDate = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.timestamp")?.Value<string>() : null,
                                OrderId = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.oid")?.Value<string>() : null,
                                Amount = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.amount")?.Value<string>() : null,
                                Status = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("paymentInfo.status")?.Value<string>() : null,
                                //ItemCount= item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload).SelectToken("productInfo.items[*]").Count().ToString() : "0",
                                ItemCount = item.EcommercePayload != null ? JObject.Parse(item.EcommercePayload)["productInfo"]?["items"]?.Count().ToString() : "0",
                            }).ToList();



            var NewResult = PResult
                                .Where(s => string.IsNullOrEmpty(searchFilter) ||
                                    (s.Name != null && s.Name.Contains(searchInput) && searchFilter == "customerName") ||
                                    (s.Email != null && s.Email.Contains(searchInput) && searchFilter == "email") ||
                                    (s.Mobile != null && s.Mobile.Contains(searchInput) && searchFilter == "mobile"))
                                .Where(s => string.IsNullOrEmpty(status) ||
                                    (s.Status != null && s.Status.Contains(status) && status == "success") ||
                                    (s.Status != null && s.Status.Contains(status) && status == "failed"))
                                .Where(s => !startdate.HasValue || s.Date_Time >= startdate)
                                .Where(s => !enddate.HasValue || s.Date_Time <= enddate.Value.Date.Add(new TimeSpan(23, 59, 59)))
                                .Select(item => new EcommerceViewModel
                                {
                                    Ecommerce_Id = item.Ecommerce_Id,
                                    Member_Id = item.Member_Id,
                                    Name = item.Name ?? "N/A",
                                    Email = item.Email ?? "N/A",
                                    Mobile = item.Mobile ?? "N/A",
                                    PaymentDate = item.Date_Time,
                                    OrderId = item.OrderId ?? "N/A",
                                    Amount = item.Amount ?? "N/A",
                                    Status = item.Status ?? "N/A",
                                    ItemCount = item.ItemCount ?? "N/A",
                                });

            ViewBag.StartDateFilter = startdate?.ToString("dd/MM/yyyy");
            ViewBag.EndDateFilter = enddate?.ToString("dd/MM/yyyy");
            ViewBag.CurrentFilterSearchFilter = searchFilter;
            ViewBag.CurrentFilterInputFilter = searchInput;
            ViewBag.CurrentFilterPaymentStatus = status;

            return View("Index", NewResult.ToPagedList(Page_No ?? 1, 30));
        }



        //View Details
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ecomDetail = db.tbl_ecommerce_transaction.Find(id);

            if (ecomDetail == null)
            {
                HttpNotFound();
            }
            var setting = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate,
            };
            if (ecomDetail.payload != null)
            {
                var payloadData = JsonConvert.DeserializeObject<EcommercePayload>(ecomDetail.payload, setting);
                //var payloadData = JsonConvert.DeserializeObject<List<EcommercePayload>>(ecomDetail.payload, setting);

                var viewModel = new EcommerceViewModel
                {
                    Ecommerce_Id = ecomDetail.ecommerce_id,
                    Member_Id = (int)ecomDetail.member_id,
                    Payload = payloadData,
                    datetime = ecomDetail.date_time,
                };
                return View(viewModel);
            }
            else
            {
                ViewBag.ErrorMessage = "Data is Missing";
                return View("Error");
            }
        }
    }
}