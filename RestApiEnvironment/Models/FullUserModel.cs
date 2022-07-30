namespace RestApiEnvironment.Models
{
     
    public class FullUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public CompanyModel Company { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }

            var user = obj as FullUserModel;
            return this.Id == user.Id
                   && this.Name == user.Name
                   && this.Email == user.Email
                   && this.Phone == user.Phone
                   && this.Website == user.Website
                   && this.Address.City == user.Address.City
                   && this.Address.Street == user.Address.Street
                   && this.Address.Suite == user.Address.Suite
                   && this.Address.ZipCode == user.Address.ZipCode
                   && this.Address.Geo.Lat == user.Address.Geo.Lat
                   && this.Address.Geo.Lng == user.Address.Geo.Lng
                   && this.Company.Name == user.Company.Name
                   && this.Company.Bs == user.Company.Bs
                   && this.Company.CatchPhrase == user.Company.CatchPhrase;
        }

        public override int GetHashCode()
        {
            return $"{Id}{Name}{Email}{Phone}{Website}{Address.City}{Address.Street}{Address.Suite}{Address.ZipCode}{Address.Geo.Lat}{Address.Geo.Lng}{Company.Name}{Company.Bs}{Company.CatchPhrase}".GetHashCode();
        }
    }

    public class AddressModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public GeoModel Geo { get; set; }
    }
    
    public class GeoModel
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }

    public class CompanyModel
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
    }
    
    
}