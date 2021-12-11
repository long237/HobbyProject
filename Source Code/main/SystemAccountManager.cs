using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{
    internal class SystemAccountManager
    {
        public void AccountController()
        {
            // Create objects
            UserAccount user = new UserAccount();
            UiPrint ui = new UiPrint();
            
            bool foo = true;

            while (foo == true)
            {
                // sub menu
                ui.SystemAccountMenu();
                // 

            }

        }
    }
}
