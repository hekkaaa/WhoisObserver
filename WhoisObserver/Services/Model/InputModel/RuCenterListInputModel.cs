using System.Collections.Generic;

namespace WhoisObserver.Services.Model.InputModel
{
    public class RuCenterListInputModel
    {
        public string registry { get; set; }
        public string html { get; set; }
        public List<RuCenterListFormatterInputModel> formatted { get; set; }
    }
}
