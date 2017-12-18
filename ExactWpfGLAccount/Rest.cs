using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exact
{
    static class Rest
    {
        //============ getGLAccounts ===============================

        static public async Task<List<GLAccount>> getGLAccounts(string f = "")   //// In dit voorbeeld kan het ook met List ipv ObservableCollection
        {
            await OAuth.getAccess();

            string orderby = "&$orderby=ItemCode+asc";
            Uri request = new Uri(Constants.BASE_URI + "/api/v1/" + OAuth.CurrentDivision + "/logistics/SalesItemPrices?access_token=" + OAuth.AccessToken + orderby + "&$select=ItemCode,ItemDescription,Price");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            HttpResponseMessage respons = await client.GetAsync(request);
            if (respons.IsSuccessStatusCode == false)
            {
                throw new ExactError("getGLAccounts Mislukt:  status = " + respons.StatusCode.ToString());
            }
            respons.EnsureSuccessStatusCode();
            string responsecontent = await respons.Content.ReadAsStringAsync();

            JObject content = JObject.Parse(responsecontent);
            IList<JToken> results = content["d"]["results"].Children().ToList();

            List<GLAccount> searchResults = new List<GLAccount>();
            foreach (JToken result in results)
            {
                var x = result;
                GLAccount searchResult = JsonConvert.DeserializeObject<GLAccount>(result.ToString());
                searchResults.Add(searchResult);
            }
            return searchResults;
        }

        //============ getGLAccount ===============================

        static public async Task<GLAccount> getGLAccount(string id)
        {
            await OAuth.getAccess();

            Uri request = new Uri(Constants.BASE_URI + "/api/v1/" + OAuth.CurrentDivision + "/financial/GLAccounts(guid'" + id + "')?access_token=" + OAuth.AccessToken);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            HttpResponseMessage respons = await client.GetAsync(request);
            if (respons.IsSuccessStatusCode == false)
            {
                throw new ExactError("getGLAccount(id) Mislukt:  status = " + respons.StatusCode.ToString());
            }
            respons.EnsureSuccessStatusCode();
            string responsecontent = await respons.Content.ReadAsStringAsync();

            JObject content = JObject.Parse(responsecontent);
            GLAccount searchResult = JsonConvert.DeserializeObject<GLAccount>(content["d"].ToString());
            return searchResult;
        }

        //============ updateGLAccount ===============================

        static public async Task updateGLAccount(GLAccount gla)
        {
            await OAuth.getAccess();
            Uri request = new Uri(Constants.BASE_URI + "/api/v1/" + OAuth.CurrentDivision + "/financial/GLAccounts(guid\'" + gla.ItemCode + "\')?access_token=" + OAuth.AccessToken);
            HttpClient client = new HttpClient();

            string bodystring = JsonConvert.SerializeObject(gla);
            StringContent content = new StringContent(bodystring, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PutAsync(request, content);

            if (respons.IsSuccessStatusCode == false)
            {
                throw new ExactError("updateGLAccount Mislukt:  status = " + respons.StatusCode.ToString());
            }
            respons.EnsureSuccessStatusCode();
        }

        //============ insertGLAccount ===============================

        static public async Task insertGLAccount(GLAccount gla)
        {
            await OAuth.getAccess();
            Uri request = new Uri(Constants.BASE_URI + "/api/v1/" + OAuth.CurrentDivision + "/financial/GLAccounts?access_token=" + OAuth.AccessToken);
            HttpClient client = new HttpClient();

            gla.ItemCode = null;
            string bodystring = JsonConvert.SerializeObject(gla);
            StringContent content = new StringContent(bodystring, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, content);

            if (respons.IsSuccessStatusCode == false)
            {
                throw new ExactError("insertGLAccount Mislukt:  status = " + respons.StatusCode.ToString());
            }
            respons.EnsureSuccessStatusCode();
        }

        //============ updateGLAccount ===============================

        static public async Task deleteGLAccount(GLAccount gla)
        {
            await OAuth.getAccess();
            Uri request = new Uri(Constants.BASE_URI + "/api/v1/" + OAuth.CurrentDivision + "/financial/GLAccounts(guid\'" + gla.ItemCode + "\')?access_token=" + OAuth.AccessToken);
            HttpClient client = new HttpClient();

            HttpResponseMessage respons = await client.DeleteAsync(request);

            if (respons.IsSuccessStatusCode == false)
            {
                throw new ExactError("updateGLAccount Mislukt:  status = " + respons.StatusCode.ToString());
            }
            respons.EnsureSuccessStatusCode();
        }
    }
}
