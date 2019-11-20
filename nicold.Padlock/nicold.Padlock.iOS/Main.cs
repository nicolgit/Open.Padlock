using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Identity.Client;
using UIKit;

namespace nicold.Padlock.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {

            Models.Globals.CloudStorage = new Models.Services.OneDrive();
            Models.Globals.CloudStorage.Initialize();
            Models.Globals.CloudStorage.ParentWindow = null;

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
