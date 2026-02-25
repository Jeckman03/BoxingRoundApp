
using BoxingRoundApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingRoundApp.Services.Workouts
{
    public class TimerServices
    {
        private CancellationTokenSource _cts;
        private bool _isCanceled;

        public async Task RunWorkout(List<RoundSettingsModel> rounds, Action<int> onTick, Action<string> onPhaseChanged)
        {
            _cts = new CancellationTokenSource();

            foreach (var round in rounds)
            {
                _cts.Token.ThrowIfCancellationRequested();

                await TextToSpeech.Default.SpeakAsync($"Round {round.RoundNumber}");

                await Task.Delay(1000);

                await TextToSpeech.Default.SpeakAsync($"{round.RoundDescription}");

                await Task.Delay(2000);

                // await PlaySound("bell.mp3");
                await StartCountdown(round.DurationSeconds, true, onTick, _cts.Token);

                if (round != rounds.Last())
                {
                    await TextToSpeech.Default.SpeakAsync("Rest");
                    await StartCountdown(round.RestSeconds, false, onTick, _cts.Token);
                }
            }

            await TextToSpeech.Default.SpeakAsync("Workout complete. Well done.");
        }

        public async Task StartCountdown(int durationSeconds, bool isWork, Action<int> onTick, CancellationToken token)
        {
            // i is your 'remaining' variable
            for (int i = durationSeconds; i >= 0; i--)
            {
                // Check if user exited the page or hit stop
                token.ThrowIfCancellationRequested();

                // Update UI
                onTick(i);

                // Sound logic: 10 seconds remaining
                if (isWork && i == 10)
                {
                    // _ = means "Fire and forget" so the timer doesn't wait for the sound to finish
                    //_ = PlaySound("tick_tick.mp3");
                }

                // Voice logic: Rest countdown (5, 4, 3...)
                if (!isWork && i <= 5 && i > 0)
                {
                    // We DON'T await this because if the voice takes 1.2 seconds to say 
                    // "Five", your timer will wait 1.2 seconds, making the clock slow.
                    _ = TextToSpeech.Default.SpeakAsync(i.ToString(), options: null, cancelToken: token);
                }

                if (i == 0)
                {
                    //_ = PlaySound("bell.mp3");
                }

                // Wait 1 second, but allow the token to cancel the delay immediately
                await Task.Delay(1000, token);
            }
        }

        public void Stop() => _cts?.Cancel();
    }
}
