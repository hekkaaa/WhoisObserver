using System.Collections.Generic;

namespace WhoisObserver.Services.Model.InputModel
{
    public class RuCenterBodyInputModel
    {
        public string status { get; set; }
        public bool getPromoted { get; set; }
        public List<RuCenterListInputModel> list { get; set; }
    }
}
