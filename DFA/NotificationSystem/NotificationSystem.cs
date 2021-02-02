using DFA.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    class NotificationSystem
    {

        private MainForm mainForm;

        public LinkedList<Notification> notificationQueue;

        public Keys keyToAccept = Keys.D4;
        public Keys keyToDiscard = Keys.D3;

        public bool checkForKey = false;



        Timer notificationTimer;

        public NotificationSystem(MainForm mainForm)
        {
            this.mainForm = mainForm;
            notificationQueue = new LinkedList<Notification>();

            mainForm.GetNotificationPictureBox().Size = mainForm.GetNotificationPictureBox().MinimumSize;

        }
        public void ShowNotification(Notification notification)
        {

            InsertNotificationInQueue(notification);


        }

        private void InsertNotificationInQueue(Notification newNotification)
        {
            notificationQueue.AddLast(newNotification);
            HandleShowingNotification();
        }

        private void HandleShowingNotification()
        {
            if (notificationTimer != null)
                return;
            else
            {
                if (notificationQueue.Count>0)
                    CreateNotification();

            }
        }

        private void CreateNotification()
        {
            var notification = notificationQueue.First.Value;

            mainForm.GetNotificationLabel().Text = notification.message;

            CreateNotificationTimer();
            CreateNotificationGraphicTimer();


            desiredSize = new Size(mainForm.GetNotificationPictureBox().Width, mainForm.GetNotificationPictureBox().MaximumSize.Height);


            notification.OnNotificationDisplayed();
        }

        public void StopNotificationTimer()
        {
            if (notificationTimer != null)
            {
                notificationTimer.Stop();
                notificationTimer.Dispose();
                notificationTimer = null;

            }
        }

        public void CreateNotificationTimer()
        {
            StopNotificationTimer();


            notificationTimer = new Timer();
            notificationTimer.Interval = 1000;
            notificationTimer.Tick += new EventHandler(NotificationTimerTick);
            notificationTimer.Start();


        }


        private void NotificationTimerTick(object sender, EventArgs e)
        {

            notificationQueue.First.Value.discardTimer -= TimeSpan.FromSeconds(1);
            if (notificationQueue.First.Value.discardTimer.TotalSeconds <= 1)
            {


                StartHidingNotification();

            }

        }


        public void StartHidingNotification()
        {
            StopNotificationTimer();
            desiredSize = new Size(mainForm.GetNotificationPictureBox().Width, mainForm.GetNotificationPictureBox().MinimumSize.Height);
            var notification = notificationQueue.First.Value;
            notification.OnNotificationHidden();
            CreateNotificationGraphicTimer();

        }

        public void FinishHidingNotification()
        {

            StopNotificationTimer();
            notificationQueue.RemoveFirst();
            
            HandleShowingNotification();

        }


        private void StopNotificationGraphicTimer()
        {
            if (notificationGraphicTimer != null)
            {
                notificationGraphicTimer.Stop();
                notificationGraphicTimer.Dispose();
            }
        }
        Size desiredSize;
        Timer notificationGraphicTimer;

        private void CreateNotificationGraphicTimer()
        {
            StopNotificationGraphicTimer();
            notificationGraphicTimer = new Timer();
            notificationGraphicTimer.Interval = mainForm.graphicalProgressBarUpdateInMiliseconds;
            notificationGraphicTimer.Tick += new EventHandler(NotificationGraphicTimerTick);
            notificationGraphicTimer.Start();

        }


        private float lerpTime = 0.1f;
        private void NotificationGraphicTimerTick(object sender, EventArgs e)
        {



            var startingW = mainForm.GetNotificationPictureBox().Size.Width;
            var startingH = mainForm.GetNotificationPictureBox().Size.Height;



            Size newSize;


            if (notificationQueue.First.Value.discardTimer.TotalSeconds <= 1)
            {

                newSize = new Size(
                    (int)Math.Floor(Utils.Lerp(startingW, desiredSize.Width, lerpTime)),
                    (int)Math.Floor(Utils.Lerp(startingH, desiredSize.Height, lerpTime))
                    );
            }
            else
            {
                newSize = new Size(
                                (int)Math.Ceiling(Utils.Lerp(startingW, desiredSize.Width, lerpTime)),
                                (int)Math.Ceiling(Utils.Lerp(startingH, desiredSize.Height, lerpTime))
                                );
            }



            mainForm.GetNotificationPictureBox().Size = newSize;



            if (Math.Abs(desiredSize.Height - mainForm.GetNotificationPictureBox().Size.Height) < 0.1f)
            {
                mainForm.GetNotificationPictureBox().Size = desiredSize;
                StopNotificationGraphicTimer();
                if (mainForm.GetNotificationPictureBox().Size.Height <= mainForm.GetNotificationPictureBox().MinimumSize.Height)
                    FinishHidingNotification();
            }
            mainForm.Invalidate();
        }
    }
}
