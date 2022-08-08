using AutoMapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.ClientModel;

namespace WhoisObserver.Services.Helpers
{
    public static class RuCenterResponseParserHtml
    {
        public static Dictionary<string, string> ParseHtmlResponseContentWithIpInfo(string htmlContent)
        {
            if (htmlContent != null)
            {
                char[] delimiterChars = { '\n', '\r' };
                string[] splitHtmlContent = htmlContent.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

                //List<string> tmpCollection = new List<string>();
                Dictionary<string, string> resultDict = new Dictionary<string, string>();

                foreach (string valuesContents in splitHtmlContent)
                {
                    if (!valuesContents.Contains("%") && !valuesContents.Contains("#"))
                    {
                        try
                        {
                            // Split string key and value.
                            string[] tmpValuesContents = valuesContents.Split(':');

                            // clearing the key of unnecessary characters '-', ' '.
                            string[] tmpClearedKeyArrayFromChar = tmpValuesContents[0].Split('-', ' ');
                            tmpValuesContents[0] = string.Empty;

                            // rebuild key after Split.
                            foreach (var key in tmpClearedKeyArrayFromChar)
                            {
                                tmpValuesContents[0] = string.Concat(tmpValuesContents[0], key.ToLower());
                            }

                            resultDict.Add(tmpValuesContents[0], tmpValuesContents[1].Trim(new char[] { ' ' }));
                        }
                        catch (System.ArgumentException)
                        {
                            // Error when repeating the key.
                            continue;
                        }
                    }
                }

                return resultDict;
            }

            return null;
        }


        public static string ConvertInJsonWithNativeResponce(Dictionary<string, string> convertDict, IMapper mapper)
        {
            var json = JsonConvert.SerializeObject(convertDict);
            RuCenterModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<RuCenterModel>(json);
            var test1 = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            //var oo = JsonConvert.SerializeObject(account1);
            return json;
        }
    }
}
