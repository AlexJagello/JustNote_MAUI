using JustNote_maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JustNote_maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Note), typeof(Note));
            Routing.RegisterRoute(nameof(NoteList), typeof(NoteList));

        }
    }
}