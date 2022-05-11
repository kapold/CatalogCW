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
        private RelayCommand openProfile;

        public RelayCommand OpenProfile
        {
            get
            {
                return openProfile ?? (openProfile = new RelayCommand(obj =>
                {
                    ProfileWnd profileWnd = new ProfileWnd();
                    profileWnd.Show();
                }));
            }
        }
    }
}
