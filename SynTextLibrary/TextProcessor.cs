using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SynTextLibrary
{
    public class TextProcessor
    {
        public static async Task<ResponseString> LoadReadablityAsync(SampleText sampleText)
        {
            string url = "http://localhost:53146/api/Text";

            var value = new Dictionary<string, string>{ { "Text", sampleText.Text } };

            var content = new FormUrlEncodedContent(value);

            //Uri uri = new Uri(url);

            //using (HttpResponseMessage response = await APIHelper.ApiClient.PostAsJsonAsync<SampleText>(uri, sampleText))
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        ResponseString rs = await response.Content.ReadAsAsync<ResponseString>();
            //        return rs;
            //    }
            //    else
            //    {
            //        throw new Exception(response.ReasonPhrase);
            //    }
            //}

            using (APIHelper.ApiClient)
            {
                Uri uri = new Uri(url);
                APIHelper.ApiClient.BaseAddress = uri;
                var response = await APIHelper.ApiClient.PostAsJsonAsync<SampleText>("Text", sampleText);

                if (response.IsSuccessStatusCode)
                {
                    ResponseString rs = await response.Content.ReadAsAsync<ResponseString>();
                    return rs;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
