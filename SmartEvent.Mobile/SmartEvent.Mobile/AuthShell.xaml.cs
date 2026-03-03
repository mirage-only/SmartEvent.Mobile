using SmartEvent.Mobile.Presentation.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile
{
    public partial class AuthShell : Shell
    {
        public AuthShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("register", typeof(RegisterPage));

        }
    }

}
