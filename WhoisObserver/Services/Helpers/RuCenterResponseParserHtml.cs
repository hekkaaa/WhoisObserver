using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.ClientModel;
using WhoisObserver.Services.Model.InputModel;

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

                Dictionary<string, string> resultDict = new Dictionary<string, string>();

                foreach (string valuesContents in splitHtmlContent)
                {
                    if (!valuesContents.Contains("%") && !valuesContents.Contains("#") && !valuesContents.Contains(">"))
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
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                }

                return resultDict;
            }

            return null;
        }

        public static Dictionary<string, string> ParseHtmlResponseContentWithNoFree(List<RuCenterListFormatterInputModel> formatterContent)
        {
            if (formatterContent != null)
            {
                Dictionary<string, string> resultDict = new Dictionary<string, string>();

                foreach (var valuesContents in formatterContent)
                {
                    if (valuesContents.name is null) continue;

                    else
                    {
                        try
                        {
                            // clearing the key of unnecessary characters '-'.
                            string[] tmpClearedKeyArrayFromChar = valuesContents.name.Split('-');
                            valuesContents.name = string.Empty;

                            // rebuild key after Split.
                            foreach (var key in tmpClearedKeyArrayFromChar)
                            {
                                valuesContents.name = string.Concat(valuesContents.name, key.ToLower());
                            }

                            resultDict.Add(valuesContents.name, valuesContents.value.ToString());
                        }
                        catch (Exception)
                        {
                            resultDict[valuesContents.name] += ", " + valuesContents.value.ToString();
                        }
                    }
                }

                return resultDict;
            }

            return null;
        }


        public static string ConvertDictInJsonHostnameResponceString(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            RuCenterHostnameModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<RuCenterHostnameModel>(json);
            WhoisResponseModel WhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return JsonConvert.SerializeObject(WhoisModel);
        }

        public static string ConvertDictInJsonIpResponceString(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            RuCenterIpAddressModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<RuCenterIpAddressModel>(json);
            WhoisResponseModel WhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return JsonConvert.SerializeObject(WhoisModel);
        }

        public static WhoisResponseModel ConvertDictInWhoisIpModel(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            RuCenterIpAddressModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<RuCenterIpAddressModel>(json);
            WhoisResponseModel resultWhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return resultWhoisModel;
        }

        public static WhoisResponseModel ConvertDictInWhoisHosnameModel(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            RuCenterHostnameModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<RuCenterHostnameModel>(json);
            WhoisResponseModel resultWhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return resultWhoisModel;
        }
    }
}
