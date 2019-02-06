using System;
using System.Diagnostics;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SitefinityWebApp.AzureServiceBusPublisher), "Start")]

namespace SitefinityWebApp {
    public static class AzureServiceBusPublisher {
        public static void Start() {
         Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(Bootstrapper_Initialized);
        }
        static void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                PageManager.Executing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(PageManager_Executing);
            }
        }
        private static void PageManager_Executing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs e)
        {
            if (e.CommandName == "CommitTransaction" || e.CommandName == "FlushTransaction")
            {
                var provider = sender as PageDataProvider;
                var dirtyItems = provider.GetDirtyItems();
                if (dirtyItems.Count != 0)
                {
                    foreach (var item in dirtyItems)
                    {
                        //Can be New, Updated, or Deleted
                        SecurityConstants.TransactionActionType itemStatus = provider.GetDirtyItemStatus(item);
                        var pageData = item as PageData;
                        var url = System.Web.HttpContext.Current.Request.Url.ToString();
                        if (pageData != null)
                        {
                            if (itemStatus == SecurityConstants.TransactionActionType.Updated)
                            {
                                Trace.WriteLine($"Publish Event {url}");
                                //Publish
//                                if (url.Contains("workflowOperation=Publish"))
//                                {
//                                    
//                                }
//                                //Batch Publish
//                                if (url.Contains("/batchPublishDraft/"))
//                                {
//                                    //Your logic here
//                                }
//                                //Batch unpublish
//                                if (url.Contains("/batchUnpublishPage/"))
//                                {
//                                    //Your logic here
//                                }
//                                //Save as Draft
//                                if (url.Contains("workflowOperation=SaveDraft"))
//                                {
//                                    //Your logic here
//                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
}
