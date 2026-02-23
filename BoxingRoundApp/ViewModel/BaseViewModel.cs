using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    { 
        [ObservableProperty]
        public bool _isBusy;
    }
}
