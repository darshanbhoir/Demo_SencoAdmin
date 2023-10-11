<Query Kind="Expression">
  <Connection>
    <ID>a4e3b486-3300-436b-8755-6fb4a090a602</ID>
    <Persist>true</Persist>
    <Server>13.233.140.191,1433</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>SENCO_DB_LIVE</Database>
    <NoPluralization>true</NoPluralization>
    <NoCapitalization>true</NoCapitalization>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAa75ovtwweESHEbt/wDI3mgAAAAACAAAAAAAQZgAAAAEAACAAAACq1+Xtdk6syLG+iBngQvNNa09QK47tnaZIoK9I5uj9QAAAAAAOgAAAAAIAACAAAABPxN5uDT3cm9DSSQphsDZdbUFrHgBRifUs/2pSyGrH4CAAAAAW4QosSJUBMCcpVzToknHeH1GrOqL1ja1pTfBZHDpwaUAAAACq/bLf1NG6MpOUKPuTWyrgkYXwmRJOTNpkyBZRcd62MweKGiUhyB8vuW8hAigdqEn5eI7e0JbJuSkCVpMuej21</Password>
  </Connection>
</Query>

//from SSCD in tbl_swarna_scheme_creation_details
//join UD in tbl_user_details on SSCD.scheme_member_id equals  UD.user_no
//join SUR in tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
//where SUR.created_on >= new DateTime(2022,12,15) && SUR.created_on <= new DateTime(2022, 12, 20)
//select new
//{
//	UserNumber = UD.user_no,
//	UserName = UD.user_name,
//	UserMobileNumber = UD.user_mobile_no,
//	UserEmail = UD.user_email,
//	CreatedOn = SUR.created_on,
//	//SchemeAccountName= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].CustomerName")?.Value<string>(),
//	//SchemeAccountMobile= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].NomineeMobile")?.Value<string>(),
//	//SchemeAccountEmail= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].NomineeEmail")?.Value<string>(),	
//	SchemeRegId = SSCD.scheme_reg_id,
//	//EMIAmount= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].EMIAmount")?.Value<string>(),
//	//Location= JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].LocationName")?.Value<string>(),
//	//Street = JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].Street")?.Value<string>(),
//	//StreetNumber= JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].StreetNumber")?.Value<string>(),
//	//Country= JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].CountryRegionId")?.Value<string>(),
//	//State= JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].State")?.Value<string>(),
//	//City = JObject.Parse(SUR.user_reg_payload).SelectToken("_keys[0].City")?.Value<string>(),
//	
//	UserCountry = UD.user_current_country,
//	UserCity = UD.user_current_city,	
//}

////////////////////////////////////////////

//Newtonsoft.Json.Linq//


////////////////////////////////////////////


//(from TSP in tbl_temp_swarna_paymentgateway
//join UD in tbl_user_details on TSP.temp_swarna_member_id equals UD.user_no
//where TSP.temp_payment_status ==false
//select new 
//{
//	OrderNo = TSP.temp_swarna_order_no,
//	PaymentDate = TSP.temp_swarna_paymentdate,
//	EMIno = TSP.temp_swarna_current_emi_no,
//	Amount = TSP.temp_swarna_payamount,
//	PaymentStatus = TSP.temp_payment_status,
//	TransactionId = TSP.temp_swarna_transaction_id,
//	BankTransactionId = TSP.temp_swarna_banktransactionid,
//	PaymentEntryNo = TSP.temp_swarna_paymententryno,
//	CustomerName = TSP.temp_swarna_customer_name,
//	MobileNo = TSP.temp_swarna_mobile_no,
//	Email = TSP.temp_swarna_member_email,
//	PaymentReciept = TSP.temp_swarna_payment_reciept,
//})


/////////////////////////////////////////////////

//from SSCD in tbl_swarna_scheme_creation_details
//join UD in tbl_user_details on SSCD.scheme_member_id equals  UD.user_no
//join SUR in tbl_swarna_user_registration on SSCD.scheme_member_id equals SUR.user_member_id
//where SUR.created_on >= new DateTime(2022,12,15) && SUR.created_on <= new DateTime(2022, 12, 20)
//select new
//{
//	CustomerName= JObject.Parse(SSCD.scheme_payload).SelectToken("_keys[0].CustomerName")?.Value<string>(),
//
//}