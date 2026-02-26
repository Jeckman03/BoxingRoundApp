
using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Services.Workouts
{
    public class AudioManager
    {
        private MediaElement _player;

        public void Initialize(MediaElement player)
        {
            _player = player;
        }

        public async Task PlaySound(string filename)
        {
            try
            {
                if (_player == null) return;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _player.Source = MediaSource.FromResource(filename);
                    _player.Play();
                });

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio Error: {ex.Message}");
            }
        }
    }
}
