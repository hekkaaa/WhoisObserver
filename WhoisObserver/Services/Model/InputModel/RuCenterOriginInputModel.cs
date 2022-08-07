namespace WhoisObserver.Services.Model.InputModel
{
    public class RuCenterOriginInputModel
    {
        public string requestId { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public RuCenterBodyInputModel body { get; set; }
    }
}
