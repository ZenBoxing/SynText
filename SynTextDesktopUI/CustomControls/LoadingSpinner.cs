using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SynTextDesktopUI.CustomControls
{
    public class LoadingSpinner : Control
    {
        static LoadingSpinner()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(LoadingSpinner), new FrameworkPropertyMetadata(typeof(LoadingSpinner)));
        }
    }
}
