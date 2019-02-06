using System;
using System.Diagnostics;
using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SitefinityWebApp.AzureServiceBusPublisher), "Start")]

namespace SitefinityWebApp {
    public static class AzureServiceBusPublisher {
        public static void Start() {
         Trace.WriteLine("AzureServicePublisher Starting");
         Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(Bootstrapper_Initialized);
        }
        public static void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            
           
            if (e.CommandName == "Bootstrapped")
            {
                Trace.WriteLine("AzureServicePublisher: Bootstrapped");
                EventHub.Subscribe<IDataEvent>(OnDataEvent);
              //  PublishingManager.Executing += OnPublished;
               // ContentManager.Executing += OnContentEvent;
            }
        }

        private static void OnContentEvent(object sender, ExecutingEventArgs e)
        {
            Trace.WriteLine($"AzureServicePublisher: Executing content event {e.GetType().FullName} {e.Dump()}");
        }

        private static void OnPublished(object sender, ExecutingEventArgs e)
        {
            Trace.WriteLine($"AzureServicePublisher: Executing publishing event {e.GetType().FullName} {e.Dump()}");
   
        }
        
        private static void OnDataEvent(IDataEvent e)
        {
            var action = e.Action;
            var contentType = e.ItemType;
            var itemId = e.ItemId;
            var providerName = e.ProviderName;

//            var manager = ManagerBase.GetMappedManager(contentType, providerName);
//            var item = manager.GetItemOrDefault(contentType, itemId);
            
            Trace.WriteLine($"AzureServicePublisher: Executing data event {action} {contentType} {itemId} {providerName}");
        }

    }
    
}
