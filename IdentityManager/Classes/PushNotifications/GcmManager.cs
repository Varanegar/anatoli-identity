﻿using PushSharp.Core;
using PushSharp.Google;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Anatoli.IdentityServer.Classes.PushNotifications
{
    public class GcmManager
    {
        public GcmConfiguration Config
        {
            get;
            set;
        }

        public GcmServiceBroker Broker
        {
            get;
            set;
        }

        const string GcmSenderId = "269775973801";
        const string GcmAuthToken = "AIzaSyACVwqtAd3r8WkyO54mRkAAnRg8-WMBUHo";
        public GcmManager(NotificationFailureDelegate<GcmNotification> onNotificationFailed, NotificationSuccessDelegate<GcmNotification> onNotificationSucceeded)
        {
            Config = new GcmConfiguration(GcmSenderId, GcmAuthToken, null);
            Broker = new GcmServiceBroker(Config);
            Broker.OnNotificationFailed += onNotificationFailed;
            Broker.OnNotificationSucceeded += onNotificationSucceeded;
        }

        public void Send(string msg, List<string> GcmRegistrationIds)
        {
            Broker.Start();
            foreach (var regId in GcmRegistrationIds)
                Broker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> { regId },
                    Data = JObject.Parse("{ \"alert\" : \"Hassan\" }") //Priority= GcmNotificationPriority.High                    
                });
            Broker.Stop();
        }
    }
}