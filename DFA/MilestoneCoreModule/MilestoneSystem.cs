using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    class MilestoneSystem
    {
        private const int MilestoneCheckingInterval = 300;
        private bool milestoneSystemActive = true;


        public List<IMilestoneModule> modules;


        public MilestoneSystem(IMainForm mainForm)
        {

            Timer timerMilestone = new Timer();
            timerMilestone.Interval = MilestoneCheckingInterval;
            timerMilestone.Tick += new EventHandler(TimerTick);
            timerMilestone.Start();

            modules.Add(new TimespanMilestoneModule(mainForm));
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (milestoneSystemActive)
            {
                foreach (var module in modules)
                {
                    module.Tick();
                }


            }
        }



    }
}
