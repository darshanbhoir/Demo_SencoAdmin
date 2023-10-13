using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.ViewModel;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Demo_Senco_Admin.Models.Payload;

namespace Demo_Senco_Admin.Controllers
{
    public class DashboardController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: Dashboard
        public ActionResult Index(DateTime? startdate, DateTime? enddate, string searchFilter, string searchInput)
        {
            var query = (from SSCD in db.tbl_swarna_scheme_creation_details
                         join UD in db.tbl_user_details on SSCD.scheme_member_id equals UD.user_no
                         join SUR in db.tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
                         where SSCD.scheme_member_id == 157707
                         select new
                         {
                             UserNumber = UD.user_no,
                             UserName = UD.user_name,
                             UserMobileNumber = UD.user_mobile_no,
                             UserEmail = UD.user_email,
                             CreatedOn = SUR.created_on,
                             SchemePayload = SSCD.scheme_payload,
                             UserRegPayload = SUR.user_reg_payload,
                             SchemeRegId = SSCD.scheme_reg_id,
                             UserCountry = UD.user_current_country,
                             UserCity = UD.user_current_city,
                         });
            var Result = query.ToList();

            var PResult = Result
                .Where(s => string.IsNullOrEmpty(searchFilter) ||
                    (s.SchemeRegId.ToString() == searchInput && searchFilter == "schemeNo") ||
                    (s.UserName.Contains(searchInput) && searchFilter == "customerName") ||
                    (s.UserEmail.Contains(searchInput) && searchFilter == "email") ||
                    (s.UserMobileNumber.Contains(searchInput) && searchFilter == "mobile"))                
                .Where(s => !startdate.HasValue || s.CreatedOn >= startdate)
                .Where(s => !enddate.HasValue || s.CreatedOn <= enddate)
                .Select(item => new SchemeDashboardViewModel
                {
                    UserNumber = item.UserNumber,
                    UserName = item.UserName ?? "N/A",
                    UserMobileNumber = item.UserMobileNumber ?? "N/A",
                    UserEmail = item.UserEmail ?? "N/A",
                    CreatedOn = item.CreatedOn != null ? item.CreatedOn.Value.ToString("yyyy-MM-dd") : string.Empty ?? "N/A",
                    SchemeAccountName = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].CustomerName")?.Value<string>() : null ?? "N/A",
                    SchemeAccountMobile = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeMobile")?.Value<string>() : null ?? "N/A",
                    SchemeAccountEmail = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeEmail")?.Value<string>() : null ?? "N/A",
                    SchemeRegId = item.SchemeRegId,
                    EMIAmount = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].EMIAmount")?.Value<string>() : null ?? "N/A",
                    Location = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].LocationName")?.Value<string>() : null ?? "N/A",
                    Street = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].Street")?.Value<string>() : null ?? "N/A",
                    StreetNumber = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].StreetNumber")?.Value<string>() : null ?? "N/A",
                    Country = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].CountryRegionId")?.Value<string>() : null ?? "N/A",
                    State = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].State")?.Value<string>() : null ?? "N/A",
                    City = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].City")?.Value<string>() : null ?? "N/A",
                    UserCountry = (int)item.UserCountry,
                    UserCity = (int)item.UserCity,
                }).ToList();



            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter = enddate;
            ViewBag.CurrentFilterSearchFilter = searchFilter;
            ViewBag.CurrentFilterInputFilter = searchInput;

            return View("index", PResult);
        }



        //Export to Excel
        public ActionResult ExporttoExcel()
        {
            var query = (from SSCD in db.tbl_swarna_scheme_creation_details
                         join UD in db.tbl_user_details on SSCD.scheme_member_id equals UD.user_no
                         join SUR in db.tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
                         //where SUR.created_on >= new DateTime(2022, 12, 15) && SUR.created_on <= new DateTime(2022, 12, 20)               
                         select new
                         {
                             UserNumber = UD.user_no,
                             UserName = UD.user_name,
                             UserMobileNumber = UD.user_mobile_no,
                             UserEmail = UD.user_email,
                             CreatedOn = SUR.created_on,
                             SchemePayload = SSCD.scheme_payload,
                             UserRegPayload = SUR.user_reg_payload,
                             SchemeRegId = SSCD.scheme_reg_id,
                             UserCountry = UD.user_current_country,
                             UserCity = UD.user_current_city,
                         }).ToList();

            var data = query
                            .Select(item => new SchemeDashboardViewModel
                            {
                                UserNumber = item.UserNumber,
                                UserName = item.UserName ?? "N/A",
                                UserMobileNumber = item.UserMobileNumber ?? "N/A",
                                UserEmail = item.UserEmail ?? "N/A",
                                CreatedOn = item.CreatedOn != null ? item.CreatedOn.Value.ToString("yyyy-MM-dd") : string.Empty ?? "N/A",
                                SchemeAccountName = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].CustomerName")?.Value<string>() : null ?? "N/A",
                                SchemeAccountMobile = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeMobile")?.Value<string>() : null ?? "N/A",
                                SchemeAccountEmail = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].NomineeEmail")?.Value<string>() : null ?? "N/A",
                                SchemeRegId = item.SchemeRegId,
                                EMIAmount = item.SchemePayload != null ? JObject.Parse(item.SchemePayload).SelectToken("_keys[0].EMIAmount")?.Value<string>() : null ?? "N/A",
                                Location = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].LocationName")?.Value<string>() : null ?? "N/A",
                                Street = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].Street")?.Value<string>() : null ?? "N/A",
                                StreetNumber = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].StreetNumber")?.Value<string>() : null ?? "N/A",
                                Country = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].CountryRegionId")?.Value<string>() : null ?? "N/A",
                                State = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].State")?.Value<string>() : null ?? "N/A",
                                City = item.UserRegPayload != null ? JObject.Parse(item.UserRegPayload).SelectToken("_keys[0].City")?.Value<string>() : null ?? "N/A",
                                UserCountry = (int)item.UserCountry,
                                UserCity = (int)item.UserCity,
                            }).ToList();

            ExcelPackage excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            //headers
            var headers = new string[]
            {
                "UserNumber",
                "UserName",
                "UserMobileNumber",
                "UserEmail",
                "CreatedOn",
                "SchemeAccountName",
                "SchemeAccountMobile",
                "SchemeAccountEmail",
                "SchemeRegId",
                "EMIAmount",
                "Location",
                "Street",
                "StreetNumber",
                "Country",
                "State",
                "City",
                "UserCountry",
                "UserCity"
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

                foreach (var prop in typeof(SchemeDashboardViewModel).GetProperties())
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
            Response.Headers.Add("Content-Disposition", new StringValues("attachment; filename=ExportedSchemeData.xlsx"));

            stream.CopyTo(Response.OutputStream);

            Response.Flush();

            Response.End();

            return RedirectToAction("SchemeDashboard");


        }


        //View Detail
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var schemedetail = db.tbl_swarna_scheme_creation_details.Find(id);

            if (schemedetail == null)
            {
                return HttpNotFound();
            }

            var setting = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate,
            };


            if (schemedetail.scheme_payload != null)
            {
                var payloadDataNewScheme = JsonConvert.DeserializeObject<NewSchemePayload>(schemedetail.scheme_payload, setting);

                var viewModel = new SchemeDashboardViewModel
                {
                    SchemeRegId = schemedetail.scheme_reg_id,
                    SchemeMemberId = schemedetail.scheme_member_id,
                    SchemePayload = payloadDataNewScheme,
                    Created_On = (DateTime)schemedetail.created_on,

                };
                return View(viewModel);
            }
            else
            {
                //var ErrorMessage = "Data is Missing";
                //ViewBag.AlertMessage = ErrorMessage;
                //return View();
                ViewBag.ErrorMessage = "Data is Missing";
                return View("Error");
            }

        }



    }
}