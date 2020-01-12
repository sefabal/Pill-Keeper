using System;

namespace DrugKeeper.Services
{
    public interface ILocalNotificationService
    {
        void LocalNotification(string title, string body, int id, DateTime notifyTime,int frequencyHour);
        void Cancel(int id);
    }
}
