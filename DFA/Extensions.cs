using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    public static class Extensions
    {
        public static void DoubleBuffered(this Control control, bool enabled)
        {
            var prop = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(control, enabled, null);
        }
    }
}
