using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Senco_Admin.Models.Payload
{
    public class NewScheme
    {
        
        public string BirthDate { get; set; }

        
        public string CustomerBankAccountNo { get; set; }

       
        public string CustomerBankIFSCCode { get; set; }

        
        public string CustomerBankName { get; set; }

       
        public string CustomerCode { get; set; }

       
        public string CustomerName { get; set; }

       
        public string EMIAmount { get; set; }

      
        public string GaurdianName { get; set; }

       
        public string IdentityProof { get; set; }

       
        public string IdProofNo { get; set; }

        
        public string Minor { get; set; }

        
        public string NomineeAddress { get; set; }

       
        public string NomineeBirthDate { get; set; }

       
        public string NomineeCity { get; set; }

       
        public string NomineeCountryRegionId { get; set; }

        
        public string NomineeEmail { get; set; }

        
        public string NomineeMobile { get; set; }

       
        public string NomineeName { get; set; }

       
        public string NomineeRelation { get; set; }

       
        public string NomineeState { get; set; }

        
        public string NomineeTelephone { get; set; }

        
        public string NomineeZipCode { get; set; }

        
        public string PassbookNo { get; set; }

       
        public string RelationWithMinor { get; set; }

       
        public string SchemeCode { get; set; }

        
        public string store { get; set; }

       
        public string InventLocationId { get; set; }

     
        public string KycDocumentId { get; set; }

        
        public string KycDocumentNo { get; set; }



        public NewScheme()
        {
            BirthDate = "-";
            CustomerBankAccountNo = "-";
            CustomerBankIFSCCode = "-";
            CustomerBankName = "-";
            CustomerCode = "-";
            CustomerName = "-";
            EMIAmount = "-";
            IdentityProof = "-";
            IdProofNo = "-";
            Minor = "-";
            NomineeAddress = "-";
            NomineeBirthDate = "-";
            NomineeCity = "-";
            NomineeCountryRegionId = "-";
            NomineeEmail = "-";
            NomineeMobile = "-";
            NomineeName = "-";
            NomineeRelation = "-";
            NomineeState = "-";
            NomineeTelephone = "-";
            NomineeZipCode = "-";
            PassbookNo = "-";
            RelationWithMinor = "-";
            SchemeCode = "-";
            store = "-";
            InventLocationId = "-";
            KycDocumentId = "-";
            KycDocumentNo = "-";
        }


    }
}