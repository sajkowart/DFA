using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DFA.Forms
{
    public partial class DailyGoalForm : Form
    {
        public TimeSpan returnTime;
        public DailyGoalForm()
        {

            InitializeComponent();

            awaitingInputConfirmation = false;
            buttonAccept.Visible = false;
        }


        public static void SaveDailyGoalTimespan(TimeSpan timeSpan)
        {



            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey("DFA", true);
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey("DFA", true);

            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            key.SetValue("DFADailyGoalHour", hours);
            key.SetValue("DFADailyGoalMinutes", minutes);

            key.Close();



        }

        public static bool GetDailyGoalTimespan(out TimeSpan result)
        {
            var key = Registry.CurrentUser.OpenSubKey("DFA", true);
            if (key != null)
            {
                int hours = (int)key.GetValue("DFADailyGoalHour");
                int minutes = (int)key.GetValue("DFADailyGoalMinutes");

                if (hours + minutes > 0)
                {

                    result = TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes);

                    return true;
                }


            }
            result = TimeSpan.FromMilliseconds(0);
            return false;
        }

      

        private bool awaitingInputConfirmation = false;
        private void ButtonAccept_MouseClick(object sender, MouseEventArgs e)
        {
            if (awaitingInputConfirmation)
            {

               SaveDailyGoalTimespan(returnTime);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ValidateInput()
        {
            buttonAccept.Visible = false;
            awaitingInputConfirmation = false;
            var text = textBoxInputTime.Text;

          

            bool success = TimeSpan.TryParse(text, out TimeSpan t);

            if (!success)
            {
                DisplayErrorMessage();
                return;
            }

            if (t.Days > 0)
            {
                label1.Text = t.Days + " " + t.TotalHours + " " + t.TotalMinutes + "You are setting your daily goal for more than a day!";
                return;
            }

            if (t.TotalMinutes < 1)
            {
                label1.Text = t.ToString() + "Your daily goal should be at least a minute!";
                return;
            }




            label1.Text = "You are setting your daily goal to " + t.ToString() + "\n Confirm your input";
            returnTime = t;

            buttonAccept.Visible = true;
            awaitingInputConfirmation = true;

            
        }



        private void DisplayErrorMessage()
        {
            label1.Text = "Input time incorrect format ex. \"1:30\" as of 1hour and 30minutes";
        }

        private void TextBoxInputTime_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();


        }
    }
}
