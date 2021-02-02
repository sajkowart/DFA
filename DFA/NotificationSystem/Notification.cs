using System;

namespace DFA
{
    public class Notification : INotification
    {
        public string message;
        public bool requiresAction;
        public TimeSpan discardTimer;

        public Notification(string message, bool requiresAction, TimeSpan discardTimer)
        {
            this.message = message;
            this.requiresAction = requiresAction;
            this.discardTimer = discardTimer;
        }

        public void OnNotificationDisplayed()
        {
        }

        public void OnNotificationDiscarded()
        {
        }

        public void OnNotificationActionPassed()
        {
        }

        public void OnNotificationActionFailed()
        {
        }

        public void OnNotificationHidden()
        {
        }


        
    }
}
