namespace BalkanAir.Common
{
    using System;
    using System.Collections.Generic;

    public sealed class NotificationSender
    {
        private static readonly object SyncRoot = new object();

        private static NotificationSender instance;
 

        private NotificationSender()
        {
             
        }

        public static NotificationSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new NotificationSender();
                        }
                    }
                }

                return instance;
            }
        }

        public void SendNotification(string content, DateTime date, string url, IEnumerable<string> users = null)
        {

        }
    }
}
