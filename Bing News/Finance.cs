namespace Bing_News
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    class Finance
    {
        
        public string T { get; set; }
        public string V { get; set; }
        public string Vw { get; set; }
        public string O { get; set; }

        public string C { get; set; }
        public string H { get; set; }
        public string L { get; set; }
        public string t { get; set; }
        public string N { get; set; }
        public static async Task<List<Finance>> GetFinance()
        {

            string url = $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/2023-01-09?adjusted=true&apiKey=Ifj4VX4KLycT96xm_YKpVa5RW_BY7mRo";
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(url);
            JObject FinanceData = JObject.Parse(response);
            JArray results = (JArray)FinanceData["results"];

            List<Finance> FinanceList = new List<Finance>();
            foreach (var result in results.Take(20))
            {
                Finance financeInfo = new Finance()
                {
                    T = (string)result["T"],
                    V = (string)result["v"],
                    Vw = (string)result["vw"],
                    O = (string)result["o"],
                    C = (string)result["c"],
                    H = (string)result["h"],
                    L = (string)result["l"],
                    t = (string)result["t"],
                    N = (string)result["n"]
                };
                FinanceList.Add(financeInfo);
            }

            return FinanceList;
        }

        public List<Finance> GetFinanceInfo()
        {
            return GetFinance().Result;
        }
        

        
    }
}