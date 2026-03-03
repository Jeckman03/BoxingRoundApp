using BoxingRoundApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    [QueryProperty(nameof(Round), "Round")]
    public partial class ComboViewModel : BaseViewModel
    {
        [ObservableProperty]
        private RoundSettingsModel _round;
    }
}
