using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CopyField.Models
{
    public class RollUpWorkItem
    {
        public class Message
        {
            public string text { get; set; }
            public string html { get; set; }
            public string markdown { get; set; }
        }

        public class DetailedMessage
        {
            public string text { get; set; }
            public string html { get; set; }
            public string markdown { get; set; }
        }
        public class SystemRev
        {
            public int oldValue { get; set; }
            public int newValue { get; set; }
        }

        public class SystemWatermark
        {
            public int oldValue { get; set; }
            public int newValue { get; set; }
        }
        public class RevisedBy
        {
            public string id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public class SystemDescription
        {
            public string oldValue { get; set; }
            public string newValue { get; set; }
        }

        public class RemainingWork
        {
            public double oldValue { get; set; }
            public double newValue { get; set; }
        }
        public class OriginalEstimate
        {
            public double oldValue { get; set; }
            public double newValue { get; set; }
        }
        public class CompletedWork
        {
            public double oldValue { get; set; }
            public double newValue { get; set; }
        }

        public class Fields
        {
            [JsonProperty(PropertyName = "System.Rev")]
            public SystemRev SystemRev { get; set; }

            [JsonProperty(PropertyName = "System.Watermark")]
            public SystemWatermark SystemWatermark { get; set; }

            //[JsonProperty(PropertyName = "Aspentech.Common.BaselineTurnoverDate")]
            //public string BaselineTurnoverDate { get; set; }
        }
        public class Self
        {
            public string href { get; set; }
        }

        public class WorkItemUpdates
        {
            public string href { get; set; }
        }

        public class Parent
        {
            public string href { get; set; }
        }

        public class Html
        {
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
            public WorkItemUpdates workItemUpdates { get; set; }
            public Parent parent { get; set; }
            public Html html { get; set; }
        }

        public class Attributes
        {
            public bool isLocked { get; set; }
            public string comment { get; set; }
        }

        public class Relation
        {
            public string rel { get; set; }
            public string url { get; set; }
            public Attributes attributes { get; set; }
        }
        public class RevisionFileds
        {
            [JsonProperty(PropertyName = "System.WorkItemType")]
            public string WorkItemType { get; set; }

            [JsonProperty(PropertyName = "System.State")]
            public string State { get; set; }

            [JsonProperty(PropertyName = "Aspentech.Usecases.TargetTurnoverDate")]
            public string TargetTurnoverDate { get; set; }
            [JsonProperty(PropertyName = "Aspentech.Common.BaselineTurnoverDate")]
            public string BaselineTurnoverDate { get; set; }


        }
        public class Revision
        {
            public int id { get; set; }
            public int rev { get; set; }
            public RevisionFileds fields { get; set; }
            public IList<Relation> relations { get; set; }
            public string url { get; set; }
        }
        public class Added
        {
            public string rel { get; set; }
            public string url { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Removed
        {
            public string rel { get; set; }
            public string url { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Relations
        {
            public IList<Added> added { get; set; }
            public IList<Removed> removed { get; set; }
        }


        public class Resource
        {
            public int id { get; set; }
            public int workItemId { get; set; }
            public int rev { get; set; }
            public RevisedBy revisedBy { get; set; }
         
            public Fields fields { get; set; }
            public Links _links { get; set; }
            public Relations relations { get; set; }
            public string url { get; set; }
            public Revision revision { get; set; }
        }
        public class Collection
        {
            public string id { get; set; }
            public string baseUrl { get; set; }
        }

        public class Account
        {
            public string id { get; set; }
            public string baseUrl { get; set; }
        }
        public class Project
        {
            public string id { get; set; }
            public string baseUrl { get; set; }
        }

        public class ResourceContainers
        {
            public Collection collection { get; set; }
            public Account account { get; set; }
            public Project project { get; set; }
        }

        public class WorkItem
        {
            public string subscriptionId { get; set; }
            public int notificationId { get; set; }
            public string id { get; set; }
            public string eventType { get; set; }
            public string publisherId { get; set; }
            public string scope { get; set; }
            public Message message { get; set; }
            public DetailedMessage detailedMessage { get; set; }
            public Resource resource { get; set; }
            public string resourceVersion { get; set; }
            public ResourceContainers resourceContainers { get; set; }

        }

    }

    public class Event
    {
        public class WorkItem
        {
            public string eventType { get; set; }
        }
    }

}