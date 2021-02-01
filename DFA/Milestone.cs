using System;
using System.Collections.Generic;
using System.Text;

namespace DFA
{

    public class TimespanMilestone
    {

        public TimeSpan timeSpan;
        public bool checkedToday;


        public TimespanMilestone(TimeSpan timeSpan)
        {
            this.timeSpan = timeSpan;
        }

        public TimespanMilestone(int h, int m, int s)
        {
            this.timeSpan = new TimeSpan(h, m, s);
        }

        public static List<string> milestoneAchievedMessage = new List<string>
        {
            "Good job you achieved $s time",
            "Keep going!",
            "You rock"
        };

        
    }


    class Milestone
    {

        public string GetAchievedMessage()
        {
            Random r = new Random();

            return TimespanMilestone.milestoneAchievedMessage[r.Next(TimespanMilestone.milestoneAchievedMessage.Count)];
        }

        public List<int> clicksMilestones = new List<int>() { 0, 5, 10, 50, 100 };
        public List<TimespanMilestone> timeMilestone = new List<TimespanMilestone>
        {
            new TimespanMilestone(0,0,3 ),
            new TimespanMilestone(0,0,10 ),
            new TimespanMilestone(0,0,25 ),
            new TimespanMilestone(0,0,59 ),
            new TimespanMilestone(0,2,0 ),

        };


        public TimeSpan CheckTimespanMilestoneAchieved(TimeSpan currentActiveTime)
        {
            foreach (var item in timeMilestone)
            {
                if (item.checkedToday) continue;

                if(item.timeSpan <= currentActiveTime)
                {

                    item.checkedToday = true;
                    return item.timeSpan;
                }
            }
            return new TimeSpan(0,0,1);
        }


    }
}
