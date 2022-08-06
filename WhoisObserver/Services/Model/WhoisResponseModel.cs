namespace WhoisObserver.Services.Model
{
    public class WhoisResponseModel
    {
        public string Status { get; set; } = null;
        public string Country { get; set; } = null;
        public string CountryCode { get; set; } = null;
        public string Region { get; set; } = null;
        public string RegionName { get; set; } = null;
        public string City { get; set; } = null;
        public string Zip { get; set; } = null;
        public double Lat { get; set; } = 0;
        public double Lon { get; set; } = 0;
        public string Timezone { get; set; } = null;
        public string Isp { get; set; } = null;
        public string Org { get; set; } = null;
        public string As { get; set; } = null;
        public string Query { get; set; } = null;
    }
}
