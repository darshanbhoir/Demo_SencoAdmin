using Demo_Senco_Admin.Models;
using Demo_Senco_Admin.Models.Payload;
using Demo_Senco_Admin.Models.ViewModel;
using iText.Forms;
using iText.Kernel;
using iText.Kernel.Pdf;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo_Senco_Admin.Controllers
{
    public class EMIController : Controller
    {
        private SENCO_DB_AdminEntities db = new SENCO_DB_AdminEntities();
        // GET: EMI
        public ActionResult Index(string paymentStatus, DateTime? startdate, DateTime? enddate, string searchFilter, string searchInput, int? Page_No)
        {
            var query = (from TSP in db.tbl_temp_swarna_paymentgateway
                         join UD in db.tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
                         //where TSP.temp_swarna_yojna_id == 10822
                         orderby TSP.temp_swarna_yojna_id descending
                         select new
                         {
                             YojnaId= TSP.temp_swarna_yojna_id,
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
                             Email = TSP.temp_swarna_member_email,
                             PaymentReciept = TSP.temp_swarna_payment_reciept,
                             //SchemeEntryNo= TSP.temp_swarna_payment_schemeentryno,
                         }).Take(300);

            var Result = query.ToList();

            var NewResult = Result
                                .Where(s => string.IsNullOrEmpty(searchFilter) ||
                                    (s.SchemeNo.Contains(searchInput) && searchFilter == "schemeNo") ||
                                    (s.CustomerName.Contains(searchInput) && searchFilter == "customerName") ||
                                    (s.Email.Contains(searchInput) && searchFilter == "email") ||
                                    (s.MobileNo.Contains(searchInput) && searchFilter == "mobile"))                                
                                .Where(s => string.IsNullOrEmpty(paymentStatus) || s.PaymentStatus == (paymentStatus.ToLower() == "true" || paymentStatus.ToLower() == "false"))                                
                                .Where(s => !startdate.HasValue || s.PaymentDate >= startdate)
                                .Where(s => !enddate.HasValue || s.PaymentDate <= enddate)
                                .Select(item => new EMIPaymentViewModel
                                {
                                    YojnaId= item.YojnaId,
                                    OrderNo = item.OrderNo,
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
                                    PaymentReciept = item.PaymentReciept,
                                    //SchemeEntryNo= item.SchemeEntryNo ?? "N/A",
                                }).ToList();



            ViewBag.CurrentFilterPaymentStatus = paymentStatus;
            ViewBag.StartDateFilter = startdate;
            ViewBag.EndDateFilter = enddate;
            ViewBag.CurrentFilterSearchFilter = searchFilter;

            
            return View("Index", NewResult.ToPagedList(Page_No ?? 1, 100));
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

            //ExcelPackage excelPackage = new ExcelPackage();
            //var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            ////for headers
            //var headers = new string[]
            //{
            //    "OrderNo",
            //    "PaymentDate",
            //    "EMIno",
            //    "Amount",
            //    "PaymentStatus",
            //    "SchemeNo",
            //    "TransactionId",
            //    "BankTransactionId",
            //    "PaymentEntryNo",
            //    "CustomerName",
            //    "MobileNo",
            //    "Email",

            //};

            //for (int i = 0; i < headers.Length; i++)
            //{
            //    worksheet.Cells[1, i + 1].Value = headers[i];
            //}

            ////for data
            //int row = 2;
            //foreach (var item in data)
            //{
            //    int column = 1;

            //    foreach (var prop in typeof(EMIPaymentViewModel).GetProperties())
            //    {
            //        worksheet.Cells[row, column].Value = prop.GetValue(item);
            //        column++;
            //    }
            //    row++;
            //}

            //var stream = new MemoryStream();
            //excelPackage.SaveAs(stream);

            //stream.Position = 0;

            //Response.Clear();
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.Headers.Add("Content-Disposition", new StringValues("attachment; filename=ExportedEMIPaymentData.xlsx"));

            //stream.CopyTo(Response.OutputStream);

            GridView gridView = new GridView();
            gridView.DataSource = data.ToList();
            gridView.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-disposition", "attachment;filename=EMIData.xls");
            Response.Charset = "";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gridView.RenderControl(htw);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

            return RedirectToAction("Index");


        }


        //View&Modify EMI Details
        public ActionResult ViewandModify(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emidetail = db.tbl_temp_swarna_paymentgateway.Find(id);

            if (emidetail == null)
            {
                HttpNotFound();
            }

            var setting = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate,
            };            

            if (emidetail.temp_swarna_payload_json != null)
            {
                var payloadData = JsonConvert.DeserializeObject<List<EMIPayload>>(emidetail.temp_swarna_payload_json, setting);

                var mostRecentEMI = payloadData?.OrderBy(s => s.id).FirstOrDefault();


                var viewModel = new EMIPaymentViewModel
                {
                    YojnaId= emidetail.temp_swarna_yojna_id,
                    OrderNo = emidetail.temp_swarna_order_no,
                    PaymentDate = (DateTime)emidetail.temp_swarna_paymentdate,
                    EMIno = emidetail.temp_swarna_current_emi_no,
                    Amount = (decimal)emidetail.temp_swarna_payamount,
                    PaymentStatus = emidetail.temp_payment_status ,
                    SchemeNo = emidetail.temp_swarna_payment_schemeentryno,
                    TransactionId = emidetail.temp_swarna_transaction_id,
                    BankTransactionId = emidetail.temp_swarna_banktransactionid,
                    PaymentEntryNo = emidetail.temp_swarna_paymententryno,
                    CustomerName = emidetail.temp_swarna_customer_name,
                    MobileNo = emidetail.temp_swarna_mobile_no,
                    Email = emidetail.temp_swarna_member_email ,
                    PaymentReciept = emidetail.temp_swarna_payment_reciept ,
                    PayloadData = mostRecentEMI,
                };
                return View(viewModel);
            }
            else
            {
                ViewBag.ErrorMessage = "Data is Missing";
                return View("Error");
                
            }

        }

        [HttpPost]
        public ActionResult ViewandModify(EMIPaymentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emidetail = db.tbl_temp_swarna_paymentgateway.Find(viewModel.SchemeNo);
                    if (emidetail == null)
                    {
                        return HttpNotFound();
                    }

                    emidetail.temp_swarna_order_no = viewModel.OrderNo;
                    emidetail.temp_swarna_paymentdate = viewModel.PaymentDate;
                    emidetail.temp_swarna_current_emi_no = viewModel.EMIno;
                    emidetail.temp_swarna_payamount = viewModel.Amount;
                    emidetail.temp_payment_status = viewModel.PaymentStatus;
                    emidetail.temp_swarna_payment_schemeentryno = viewModel.SchemeNo;
                    emidetail.temp_swarna_transaction_id = viewModel.TransactionId;
                    emidetail.temp_swarna_banktransactionid = viewModel.BankTransactionId;
                    emidetail.temp_swarna_paymententryno = viewModel.PaymentEntryNo;
                    emidetail.temp_swarna_customer_name = viewModel.CustomerName;
                    emidetail.temp_swarna_mobile_no = viewModel.MobileNo;
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
        public async Task<ActionResult> CreateReciept(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {                    
                    return Json(new { success = false, message = "Yojna number is missing." });
                }

                var detail = db.tbl_temp_swarna_paymentgateway.Find(id);

                if (detail == null)
                {                    
                    return Json(new { success = false, message = "Record not Found" }, JsonRequestBehavior.AllowGet);
                }
                var viewmodel = new RecieptViewModel
                {
                    //OrderNo = detail.temp_swarna_order_no,
                    //SchemeNo = detail.temp_swarna_payment_schemeentryno,
                    //SchemeCode = detail.ttemp_swarna_payment_scheme_code,
                    //StoreType = detail.temp_swarna_tender_type,
                    //CustomerCode = detail.temp_swarna_customer_code,
                    //CustomerName = detail.temp_swarna_customer_name ,                    
                    //MobileNo = detail.temp_swarna_mobile_no ,
                    //EMIno = detail.temp_swarna_current_emi_no ,
                    //Amount = (decimal)detail.temp_swarna_payamount,
                    //TransactionId = detail.temp_swarna_transaction_id ,
                    //BankTransactionId = detail.temp_swarna_banktransactionid ,
                    //PaymentDate = ((DateTime)detail.temp_swarna_paymentdate),
                    //PaymentStatus = detail.temp_payment_status ?? false,
                    RecieptId= detail.temp_swarna_order_no,
                    CustomerId= detail.temp_swarna_customer_code,
                    CustomerName= detail.temp_swarna_customer_name,
                    CustomerEmail= detail.temp_swarna_member_email,
                    CustomerMobile= detail.temp_swarna_mobile_no,
                    Store= detail.temp_swarna_tender_type,
                    SchemeNo= detail.temp_swarna_payment_schemeentryno,
                    SchemeCode= detail.ttemp_swarna_payment_scheme_code,
                    EmiAmount= (decimal)detail.temp_swarna_payamount,
                    EmiNo= (int)detail.temp_swarna_current_emi_no,
                };

                var PdfTempUrl = "https://s3.ap-south-1.amazonaws.com/sencofiles/swarnayojna/SY_EMI_Payment_Receipt_41220229323647.pdf";

                var PdfByte = await EMIController.GeneratePdf(viewmodel, PdfTempUrl);
                //var PdfNumber = Guid.NewGuid().ToString();
                var PdfNameFormat = $"SY_EMI_Payment_Receipt_{DateTime.Now:ddMMyyyyHHmmss}.pdf";
                //var PdfPath = $"~/PdfStorage/{PdfNumber}.pdf";
                var Pdfpath = Path.Combine(Server.MapPath("~/PdfStorage"), PdfNameFormat);

                //System.IO.File.WriteAllBytes(Server.MapPath(PdfPath), PdfByte);
                System.IO.File.WriteAllBytes(Pdfpath, PdfByte);
                var PdfName= Path.GetFileName(Pdfpath);

                //Storing in Payment Reciept Column
                detail.temp_swarna_payment_reciept = PdfName;
                db.SaveChanges();

                //storing vewmodel dataa in session to use in ViewReciept method
                Session["RecieptView"] = viewmodel;

                return Json(new { success = true, data = viewmodel }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "An error occurred while creating the receipt." });

        }


        //View Reciept
        public ActionResult ViewReciept()
        {

                var recieptDetail = Session["RecieptView"] as EMIPaymentViewModel;

                if (recieptDetail == null)
                {
                    return Json(new { success = false, message = "Reciept not Found" });
                }

                //string apiURL = "https://s3.ap-south-1.amazonaws.com/sencofiles/swarnayojna";

                //var recieptData = new RecieptViewModel
                //{
                //    RecieptId = recieptDetail.OrderNo,
                //    CustomerId = recieptDetail.CustomerCode,
                //    CustomerName = recieptDetail.CustomerName,
                //    CustomerMobile = recieptDetail.MobileNo,
                //    CustomerEmail = recieptDetail.Email,
                //    SchemeNo = recieptDetail.SchemeNo,
                //    SchemeCode = recieptDetail.SchemeCode,
                //    EmiAmount = (decimal)recieptDetail.Amount,
                //    EmiNo = (int)recieptDetail.EMIno
                //};
                //var pdf = EMI.GeneratePdf(recieptData);

                var Pdf = recieptDetail.PaymentReciept;

                var PdfUrl = "https://s3.ap-south-1.amazonaws.com/sencofiles/swarnayojna/SY_EMI_Payment_Receipt_";
                var PdfView = $"{PdfUrl}{Pdf}.pdf";

                return Json(new { success = true, viewUrl = PdfView });            
            
        }


        //PDF Helper Class
        public static async Task<byte[]> GeneratePdf(RecieptViewModel viewmodel, string PdfTempUrl)
        {
            using(var httpClient = new HttpClient())
            {
                var PdfTemplate = await httpClient.GetByteArrayAsync(PdfTempUrl);

                using (var tempstream = new MemoryStream(PdfTemplate))
                {
                    try
                    {
                        //stream.Seek(0, SeekOrigin.Begin);
                        var reader = new PdfReader(tempstream);
                        var stream = new MemoryStream();

                        //if(stream.Length==0)
                        //{
                        //    return new byte[0];
                        //}

                        var writer = new PdfWriter(stream);
                        var pdf = new PdfDocument(reader, writer);

                        var form = PdfAcroForm.GetAcroForm(pdf, true);

                        form.GetField("RecieptId").SetValue(viewmodel.RecieptId);
                        form.GetField("CustomerId").SetValue(viewmodel.CustomerId);
                        form.GetField("CustomerName").SetValue(viewmodel.CustomerName);
                        form.GetField("CustomerMobile").SetValue(viewmodel.CustomerMobile);
                        form.GetField("CustomerEmail").SetValue(viewmodel.CustomerEmail);
                        form.GetField("SchemeNo").SetValue(viewmodel.SchemeNo);
                        form.GetField("SchemeCode").SetValue(viewmodel.SchemeCode);
                        form.GetField("EmiAmount").SetValue(viewmodel.EmiAmount.ToString());
                        form.GetField("EmiNo").SetValue(viewmodel.EmiNo.ToString());

                        pdf.Close();

                        return stream.ToArray();
                    }
                    catch (PdfException ex)
                    {
                        string exmsg = ex.Message;
                        Console.WriteLine(exmsg);
                        return new byte[0];
                    }

                }
            }
            
        }
    }
}