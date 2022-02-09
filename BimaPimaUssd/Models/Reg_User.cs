using BimaPimaUssd.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace BimaPimaUssd.Models
{
    public class Reg_User
    {
        public bool IsLogged { get; set; }
        public string Token { get; set; }

    }
    public class PBI
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        [JsonProperty("County")]
        public string County { get; set; }
        [JsonProperty("VC ID")]
        public string VCID { get; set; }
        [JsonProperty("VC")]
        public string VC { get; set; }
        [JsonProperty("farmerCode")]
        public string farmercode { get; set; }
        [JsonProperty("farmer_name")]
        public string farmer_name { get; set; }
        [JsonProperty("MainPhoneNumber")]
        public string MainPhoneNumber { get; set; }
        [JsonProperty("subsidy")]
        public string subsidy { get; set; }
        [JsonProperty("SubsidyAmount")]
        public string SubsidyAmount { get; set; }
        [JsonProperty("insurancePayment")]
        public string InsurancePayment { get; set; }
        [JsonProperty("Rate")]
        public string Rate { get; set; }
    }
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public string Msisdn { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PlantingMonth { get; set; }
        public string PlantingWeek { get; set; }
        public DateTime ComputedPlantingDate 
        {
            get
            {
                return new DateTime(DateOfQuery.Year, Convert.ToInt32(PlantingMonth),AppConstant.GetLastDayOfWeek(Convert.ToInt32(PlantingMonth), Convert.ToInt32(PlantingWeek)));
            }
        }

        public string NearestPrimarySchool { get; set; }
        public DateTime DateOfQuery { get; set; }

     

    }
   
}
