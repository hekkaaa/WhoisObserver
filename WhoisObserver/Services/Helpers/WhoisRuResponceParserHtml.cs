using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.ClientModel;

namespace WhoisObserver.Services.Helpers
{
    public static class WhoisRuResponceParserHtml
    {
        public static Dictionary<string, string> ParseHtmlResponseContent(string InnetTextParse)
        {
            List<string> res = SplitNativeTextinHtmlElement(InnetTextParse);
            return ConvertToDictionaryJson(res);
        }

        public static string ConvertDictInJsonIpResponceString(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            WhoisRuModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<WhoisRuModel>(json);
            WhoisResponseModel WhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return JsonConvert.SerializeObject(WhoisModel);
        }

        public static WhoisResponseModel ConvertDictInWhoisRuModel(Dictionary<string, string> convertDict, IMapper mapper)
        {
            string json = JsonConvert.SerializeObject(convertDict);
            WhoisRuModel objConvertNativeModel = System.Text.Json.JsonSerializer.Deserialize<WhoisRuModel>(json);
            WhoisResponseModel WhoisModel = mapper.Map<WhoisResponseModel>(objConvertNativeModel);

            return WhoisModel;
        }

        private static Dictionary<string, string> ConvertToDictionaryJson(List<string> notParseList)
        {
            Dictionary<string, string> resultDict = new Dictionary<string, string>();

            foreach (string values in notParseList)
            {
                List<string> tmpConvertList = new List<string>();

                try
                {
                    // pre-last split - delete ': and ' ' (space).
                    string[] splitStringCollection = values.Split(' ');
                    foreach (string value in splitStringCollection)
                    {
                        if (!string.IsNullOrWhiteSpace(value)) tmpConvertList.Add(value);
                    }

                    // clearing the key of unnecessary characters '-'.
                    string[] tmpClearedKeyArrayFromChar = tmpConvertList.First().Split('-',':');
                    tmpConvertList[0] = string.Empty;

                    // rebuild key after Split.s
                    foreach (var key in tmpClearedKeyArrayFromChar)
                    {
                        tmpConvertList[0] = string.Concat(tmpConvertList.First(), key.ToLower());
                    }

                    resultDict.Add(tmpConvertList.First(), tmpConvertList.Last());
                }
                catch (ArgumentException)
                {
                    resultDict[tmpConvertList.First()] += ", " + tmpConvertList.Last();
                }

            }

            return resultDict;
        }

        private static List<string> SplitNativeTextinHtmlElement(string notSplitText)
        {

            string[] firstSplit = notSplitText.Split('>');
            string[] twoIterationSplit = firstSplit.First().Split('\r', '\n');

            List<string> tmpListisNotNullOrEmpty = new List<string>();

            foreach (var values in twoIterationSplit)
            {
                if (!string.IsNullOrEmpty(values))
                {
                    tmpListisNotNullOrEmpty.Add(values);
                }
            }

            List<string> tmpLastIterationFilterText = new List<string>();
            foreach (var line in tmpListisNotNullOrEmpty)
            {
                if (!line.StartsWith("%") && !line.StartsWith("#"))
                {
                    tmpLastIterationFilterText.Add(line);
                }
            }

            return tmpLastIterationFilterText;
        }
    }
}
