using DatabaseService.Models.Enums;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DatabaseService.Models {
    public class Client
    {
      
        public int Id { get; set; }
       
        public string Secret { get; set; }
       
        public string Name { get; set; }
        //private ApplicationType _applicationType;
        public ApplicationType ApplicationType { get; set; }
        
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        
        public string AllowedOrigin { get; set; }

    public int ApplicationTypeValue { set { ApplicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), value.ToString()); } }
  }
}
