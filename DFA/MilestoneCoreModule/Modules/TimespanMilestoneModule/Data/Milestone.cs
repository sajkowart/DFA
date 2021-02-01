using System;
using System.Collections.Generic;

namespace DFA
{
    public abstract class Milestone : ITimespanMilestone
    {
        public abstract string GetMessageMilestoneAchieved();

        public abstract bool CheckIfPassed(TimeSpan currentActivatedTime);


        public static List<string> defaultMilestoneMessages = new List<string>
        {
            "You rock!",
            "Hell yeah!"
        };


    }
}
