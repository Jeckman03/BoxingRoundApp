using BoxingRoundApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoxingRoundApp.ViewModel
{
    [QueryProperty(nameof(ProfileId), "ProfileId")]
    public partial class ActivateWorkoutProfileViewModel : BaseViewModel
    {
        private readonly BoxingDatabase _boxingDatabase;

        [ObservableProperty]
        private int profileId;

        public ActivateWorkoutProfileViewModel(BoxingDatabase boxingDatabase)
        {
            _boxingDatabase = boxingDatabase;
        }

        partial void OnProfileIdChanged(int value)
        {
            _ = LoadWorkoutData(value);
        }

        private async Task LoadWorkoutData(int id)
        {
            try
            {
                var profile = await _boxingDatabase.GetProfileByIdAsync(id);
                var rounds = await _boxingDatabase.GetRoundSettingsAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading workout: {ex.Message}");
            }
        }
    }
}
