using System;

namespace DFA
{
    internal interface IMainForm
    {
        TimeSpan GetActivatedTime();
        static bool ArtistState { get; set; }
        static bool ArtistActive { get; set; }


        public void ShowNotification(Notification notification);
        public void SetMidLable(string text);
        
    }
}