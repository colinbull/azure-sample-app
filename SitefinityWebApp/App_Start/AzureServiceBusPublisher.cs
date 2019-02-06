using System;
using System.Diagnostics;
using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SitefinityWebApp.AzureServiceBusPublisher), "Start")]

namespace SitefinityWebApp {
    public static class AzureServiceBusPublisher {
        public static void Start() {
         Trace.WriteLine("AzureServicePublisher Starting");
         Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(Bootstrapper_Initialized);
        }
        public static void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            
            Trace.WriteLine("AzureServicePublisher: Bootstrapped");
            if (e.CommandName == "Bootstrapped")
            {
                PageManager.Executing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(PageManager_Executing);
            }
        }
        public static void PageManager_Executing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs e)
        {
            Trace.WriteLine($"AzureServicePublisher: Executing event {e.Dump()}");
        }
    }
    
}
