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

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomerBankAccountNo { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomerBankIFSCCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomerBankName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomerCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string CustomerName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string EMIAmount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string GaurdianName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string IdentityProof { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string IdProofNo { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Minor { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeAddress { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeBirthDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeCity { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeCountryRegionId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeEmail { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeMobile { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeRelation { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeState { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeTelephone { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string NomineeZipCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string PassbookNo { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string RelationWithMinor { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string SchemeCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string store { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string InventLocationId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string KycDocumentId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string KycDocumentNo { get; set; }



        //public NewScheme()
        //{
        //    BirthDate = "-";
        //    CustomerBankAccountNo = "-";
        //    CustomerBankIFSCCode = "-";
        //    CustomerBankName = "-";
        //    CustomerCode = "-";
        //    CustomerName = "-";
        //    EMIAmount = "-";
        //    IdentityProof = "-";
        //    IdProofNo = "-";
        //    Minor = "-";
        //    NomineeAddress = "-";
        //    NomineeBirthDate = "-";
        //    NomineeCity = "-";
        //    NomineeCountryRegionId = "-";
        //    NomineeEmail = "-";
        //    NomineeMobile = "-";
        //    NomineeName = "-";
        //    NomineeRelation = "-";
        //    NomineeState = "-";
        //    NomineeTelephone = "-";
        //    NomineeZipCode = "-";
        //    PassbookNo = "-";
        //    RelationWithMinor = "-";
        //    SchemeCode = "-";
        //    store = "-";
        //    InventLocationId = "-";
        //    KycDocumentId = "-";
        //    KycDocumentNo = "-";
        //}

        
    }
}