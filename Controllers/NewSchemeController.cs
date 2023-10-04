using Demo_Senco_Admin.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Demo_Senco_Admin.Controllers
{
    public class NewSchemeController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();

        // GET: Scheme Dashboard
        public string SchemeDashboard()
        {
            var query = from SSCD in db.tbl_swarna_scheme_creation_details
                        join UD in db.tbl_user_details on SSCD.scheme_member_id equals UD.user_no
                        join SUR in db.tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
                        where SUR.created_on >= new DateTime(2022, 12, 15) && SUR.created_on <= new DateTime(2022, 12, 20)
               
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

            var PResult = Result.Select(item=> new
            {
                UserNumber= item.UserNumber,
                UserName=item.UserName,
                UserMobileNumber = item.UserMobileNumber,
                UserEmail = item.UserEmail,
                CreatedOn = item.CreatedOn != null ? item.CreatedOn.Value.ToString("yyyy-MM-dd") : string.Empty,
                SchemeAccountName = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].CustomerName")?.Value<string>() : null,
                SchemeAccountMobile= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeMobile")?.Value<string>():null,
                SchemeAccountEmail= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeEmail")?.Value<string>() : null,
                item.SchemeRegId,
                EMIAmount= item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].EMIAmount")?.Value<string>() : null,
                Location = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].LocationName")?.Value<string>() : null,
                Street = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].Street")?.Value<string>() : null,
                StreetNumber = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].StreetNumber")?.Value<string>() : null,
                Country = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].CountryRegionId")?.Value<string>() : null,
                State = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].State")?.Value<string>() : null,
                City = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].City")?.Value<string>() : null,
                UserCountry = item.UserCountry,
                UserCity = item.UserCity,
            }).ToList();

            var json = new JavaScriptSerializer().Serialize(PResult);
            return json;

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
    }
}