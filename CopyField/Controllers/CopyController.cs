
using CopyField.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CopyField.Controllers
{
    public class CopyController : ApiController
    {
        // GET api/<controller>
        private string logPath;
        private string logFileName;

        [HttpPost]
        [Route("api/Copy")]
        public void Rollupworkitem(object requestJson)
        {
            var re = Request;
            var headers = re.Headers;

            int[] parentWorkItems = new int[4] { 0, 0, 0, 0 };

            Stopwatch watch = new Stopwatch();
            watch.Start();
            string eventOccured = string.Empty;
            string workItemType = string.Empty;
            string state = string.Empty;

            WorkItem objWi = new WorkItem();
            string URL = string.Empty;
            string credentials = headers.Authorization.Parameter;



            try
            {
                Event.WorkItem objEvent = JsonConvert.DeserializeObject<Event.WorkItem>(requestJson.ToString());

                RollUpWorkItem.WorkItem workItemChanged = new RollUpWorkItem.WorkItem();
                eventOccured = objEvent.eventType;

                if (eventOccured == "workitem.updated")
                {
                    workItemChanged = JsonConvert.DeserializeObject<RollUpWorkItem.WorkItem>(requestJson.ToString());
                    workItemType = workItemChanged.resource.revision.fields.WorkItemType;
                    state = workItemChanged.resource.revision.fields.State;
                }

                if (workItemType == "Use Case" && (eventOccured == "workitem.updated") && state == "Committed")
                {
                    
                    if (workItemChanged.resource != null)
                    {
                        if (workItemChanged.resource.revision.fields.BaselineTurnoverDate == null)
                        {
                            URL = workItemChanged.resource.revision.url;
                            int _apisIndex = URL.IndexOf("_apis");
                            URL = URL.Substring(0, _apisIndex);

                            WorkItemResponse.WorkItems responseObj = new WorkItemResponse.WorkItems();

                            WorkItem obj = new WorkItem();
                            responseObj = obj.GetWorkItemsDetailinBatch(workItemChanged.resource.workItemId, credentials, URL, "2.2");
                            if (responseObj.value.FirstOrDefault().fields.BaselineTurnoverDate == null)
                            {
                                
                                var ttd =workItemChanged.resource.revision.fields.TargetTurnoverDate;
                                obj.updateFields(workItemChanged.resource.workItemId, URL, credentials,ttd);

                                logPath = System.Web.HttpContext.Current.Server.MapPath("~/ApiLog");
                                logFileName = logPath + "\\SuccessData_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                LogData("Vsts call has been made at:" + DateTime.Now + Environment.NewLine + Environment.NewLine + headers + Environment.NewLine + "Time Taken: " + watch.Elapsed.TotalSeconds.ToString() + Environment.NewLine + requestJson.ToString());
                                watch.Stop();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logPath = System.Web.HttpContext.Current.Server.MapPath("~/ApiLog/Errors");
                logFileName = logPath + "\\ERROR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                LogData("Vsts call has been made at:" + DateTime.Now + Environment.NewLine + Environment.NewLine + headers + Environment.NewLine + "Time Taken: " + watch.Elapsed.TotalSeconds.ToString() + Environment.NewLine + "Error has been occured :" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + requestJson.ToString());
                watch.Stop();
            }
        }
        private void LogData(string message)
        {
            //File.Create(logFileName);
            System.IO.File.AppendAllText(logFileName, message);
        }
    }
}