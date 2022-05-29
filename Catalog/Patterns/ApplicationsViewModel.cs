using Catalog.Wndows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Patterns
{
    class ApplicationsViewModel
    {
        private RelayCommand openAddGood;

        public RelayCommand OpenAddGood
        {
            get
            {
                return openAddGood ?? (openAddGood = new RelayCommand(obj =>
                {
                    AdminPanelWnd.adminWnd.OpenAddGoodWnd();
                }));
            }
        }

        private RelayCommand openAddParams;

        public RelayCommand OpenAddParams
        {
            get
            {
                return openAddParams ?? (openAddParams = new RelayCommand(obj =>
                {
                    AdminPanelWnd.adminWnd.OpenAddTypeWnd();
                }));
            }
        }
    }
}
