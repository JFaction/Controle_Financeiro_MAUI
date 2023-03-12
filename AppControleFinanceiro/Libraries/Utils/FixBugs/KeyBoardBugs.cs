using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Libraries.Utils.FixBugs
{
    internal class KeyBoardBugs
    {
        public static void HideKeyBoard()
        {
        #if ANDROID

            if (Platform.CurrentActivity.CurrentFocus != null)
            {
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
            }

        #endif
}
    }
}
