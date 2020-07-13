using System;
using System.Collections.Generic;

namespace Deviot.Hermes.Common
{
    public interface INotifier
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}
