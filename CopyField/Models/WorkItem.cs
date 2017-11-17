using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace CopyField.Models
{
    public class WorkItem
    {

        public WorkItemResponse.WorkItems GetWorkItemsDetailinBatch(int id, string credentials, string url, string version)
        {

            WorkItemResponse.WorkItems viewModel = new WorkItemResponse.WorkItems();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                HttpResponseMessage response = client.GetAsync(url + "DefaultCollection/_apis/wit/workitems?api-version=" + version + "&ids=" + id + "&$expand=relations").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkItemResponse.WorkItems>(res);
                }

                return viewModel;
            }
        }

        public bool UpdateWorkItem(int WItoUpdate, object[] patchWorkItem, string credentials, string URl, string version)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                // serialize the fields array into a json string          
                var patchValue = new StringContent(JsonConvert.SerializeObject(patchWorkItem), Encoding.UTF8, "application/json-patch+json"); // mediaType needs to be application/json-patch+json for a patch call

                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, URl + "DefaultCollection/_apis/wit/workitems/" + WItoUpdate + "?bypassRules=true&api-version=" + version + "") { Content = patchValue };
                var response = client.SendAsync(request).Result;

                return response.IsSuccessStatusCode;
            }
        }


        public bool updateFields(int id, string URL, string credentials, string TTD)
        {
            double[] result = new double[1];
            WorkItem objWi = new WorkItem();

            if (id > 0)
            {

                string TargetTurnOverDate = TTD;


                Object[] patchDocument = new Object[1];

                patchDocument[0] = new { op = "add", path = "/fields/Aspentech.Common.BaselineTurnoverDate", value = TargetTurnOverDate };

                bool isUpdated = objWi.UpdateWorkItem(id, patchDocument, credentials, URL, "2.2");
                result = new double[3];

            }
            return true;
        }

    }

}