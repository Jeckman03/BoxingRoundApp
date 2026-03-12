using CommunityToolkit.Maui.Views;

namespace BoxingRoundApp.Views;

public partial class ComboPickerPopup : Popup<string>
{
    private List<string> _selectedHits = new();

    public ComboPickerPopup(string existing)
    {
        InitializeComponent();
        if (!string.IsNullOrEmpty(existing))
        {
            _selectedHits = existing.Split(new[] { ", " }, StringSplitOptions.None).ToList();
            UpdatePills();
        }
        LoadButtons();
    }

    private void LoadButtons()
    {
        var hits = new[] { "Jab", "Cross", "Left Hook", "Right Hook", "Left Uppercut", "Right Uppercut", "Slip Left", "Slip Right", "Pivot" };
        foreach (var hit in hits)
        {
            var btn = new Button
            {
                Text = hit,
                Margin = 5,
                TextColor = Color.FromArgb("#ffffff"),
                BackgroundColor = Color.FromArgb("#333333")
            };
            btn.Clicked += (s, e) => {
                _selectedHits.Add(hit);
                UpdatePills();
            };
            ButtonsContainer.Children.Add(btn);
        }
    }

    private void UpdatePills()
    {
        PillContainer.Children.Clear();
        for (int i = 0; i < _selectedHits.Count; i++)
        {
            var index = i; // Local copy for the closure
            var hit = _selectedHits[i];

            var pillLabel = new Label
            {
                Text = $"{hit}  ✕", // Adding an 'X' icon for visual cue
                TextColor = Colors.White,
                VerticalTextAlignment = TextAlignment.Center
            };

            var pill = new Frame
            {
                BackgroundColor = Colors.DimGray,
                CornerRadius = 15,
                Padding = new Thickness(12, 5),
                Margin = new Thickness(4),
                BorderColor = Colors.Transparent,
                Content = pillLabel
            };

            // Add the Tap Gesture to remove this specific hit
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                _selectedHits.RemoveAt(index);
                UpdatePills(); // Refresh the UI to show the updated list
            };

            pill.GestureRecognizers.Add(tapGesture);
            PillContainer.Children.Add(pill);
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_selectedHits.Count == 0)
        {
            this.CloseAsync("None");
        }
        else
        {
            // Joining with a comma so your TTS sounds natural
            string result = string.Join(", ", _selectedHits).ToString();
            await CloseAsync(result);
        }
        
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        _selectedHits.Clear();
        UpdatePills();
    }
}