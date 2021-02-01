using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    public static class Utils
    {
        public static int ValueToProgressBarProcent(float current, float min, float max)
        {
            var range = max - min;
            var correctedStartVal = current - min;
            return (int)((correctedStartVal * 10000) / range);
        }

        public static float ProcentToProgressBarValue(ProgressBar progressBar, float percent)
        {
            return progressBar.Maximum * percent / 100;
        }



        public static int ToProgressBarProcent(ProgressBar progressBar, float current)
        {
            var range = progressBar.Maximum - progressBar.Minimum;
            var correctedStartVal = current - progressBar.Minimum;

            return (int)((correctedStartVal * progressBar.Maximum) / range);
        }

        public static float ToProcentage(float current, float min, float max)
        {
            var range = max - min;
            var correctedStartVal = current - min;

            return (correctedStartVal * 100) / range;
        }

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat + (secondFloat - firstFloat) * by;
        }

        public static System.Numerics.Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }

    }
}
