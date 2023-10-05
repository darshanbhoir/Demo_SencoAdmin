using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Demo_Senco_Admin.Controllers
{
    public class NewSchemeController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();

        // GET: Scheme Dashboard
        public ActionResult SchemeDashboard(int? schemeNo, string customerName, string email, string mobile, DateTime? startdate, DateTime? enddate)
        {
            var query = from SSCD in db.tbl_swarna_scheme_creation_details
                        join UD in db.tbl_user_details on SSCD.scheme_member_id equals UD.user_no
                        join SUR in db.tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
                        //where SUR.created_on >= new DateTime(2022, 12, 15) && SUR.created_on <= new DateTime(2022, 12, 20)               
                        select new
                        {
                            UserNumber = UD.user_no,
                            UserName = UD.user_name,
                            UserMobileNumber = UD.user_mobile_no,
                            UserEmail = UD.user_email,
                            CreatedOn = SUR.created_on ,                            
                            SchemePayload = SSCD.scheme_payload,
                            UserRegPayload = SUR.user_reg_payload,
                            SchemeRegId = SSCD.scheme_reg_id,
                            UserCountry = UD.user_current_country,
                            UserCity = UD.user_current_city,
                        };
            var Result = query.ToList();

            var PResult = Result
                .Where(s=> !schemeNo.HasValue || s.SchemeRegId==schemeNo)
                .Where(s => string.IsNullOrEmpty(customerName) || s.UserName.Contains(customerName))
                .Where(s=> string.IsNullOrEmpty(email) || s.UserEmail.Contains(email))
                .Where(s=> string.IsNullOrEmpty(mobile) || s.UserMobileNumber.Contains(mobile))
                .Where(s=> !startdate.HasValue || s.CreatedOn >= startdate)
                .Where(s=> !enddate.HasValue || s.CreatedOn <= enddate)
                .Select(item=> new SchemeDashboardViewModel
                {
                    UserNumber= item.UserNumber,
                    UserName=item.UserName,
                    UserMobileNumber = item.UserMobileNumber,
                    UserEmail = item.UserEmail,
                    CreatedOn = item.CreatedOn != null ? item.CreatedOn.Value.ToString("yyyy-MM-dd") : string.Empty,
                    SchemeAccountName = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].CustomerName")?.Value<string>() : null,
                    SchemeAccountMobile= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeMobile")?.Value<string>():null,
                    SchemeAccountEmail= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeEmail")?.Value<string>() : null,
                    SchemeRegId=item.SchemeRegId,
                    EMIAmount= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].EMIAmount")?.Value<string>() : null,
                    Location = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].LocationName")?.Value<string>() : null,
                    Street = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].Street")?.Value<string>() : null,
                    StreetNumber = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].StreetNumber")?.Value<string>() : null,
                    Country = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].CountryRegionId")?.Value<string>() : null,
                    State = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].State")?.Value<string>() : null,
                    City = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].City")?.Value<string>() : null,
                    UserCountry = (int)item.UserCountry,
                    UserCity = (int)item.UserCity,
                }).ToList();


            ViewBag.CurrentFilter = customerName;
            ViewBag.CurrentFilter = email;
            ViewBag.CurrentFilter = mobile;
            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter= enddate;
            ViewBag.CurrentFilter = schemeNo;

            //var json = new JavaScriptSerializer().Serialize(PResult);
            return View("SchemeDashboard", PResult);

        }



        //View Scheme Detail
        public ActionResult ViewSchemeDetail(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var schemedetail = db.tbl_user_details.Find(id);

            if(schemedetail == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SchemeDashboardViewModel
            {
                UserNumber = schemedetail.user_no,
                UserName = schemedetail.user_name,
                UserMobileNumber = schemedetail.user_mobile_no,
                UserEmail = schemedetail.user_email,
                //CreatedOn = schemedetail.
            };
            return View(viewModel);
        }








    }
}





















//SchemeAccountName = GetSchemeAccountName(SSCD.scheme_payload),  
//SchemeAccountName= SSCD.scheme_payload != null ? JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].CustomerName")?.Value<string>() : null,
//SchemeAccountName= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].CustomerName")?.Value<string>(),

//
//private string GetSchemeAccountName(string scheme_payload)
//{
//    if (scheme_payload != null)
//    {
//        var json = JObject.Parse(scheme_payload);
//        var customerName = json.SelectToken("_keys[0].CustomerName");
//        return customerName != null ? customerName.Value<string>() : null;
//    }
//    return null;
//}