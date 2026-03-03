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
        var hits = new[] { "Jab", "Cross", "Hook", "Uppercut", "Body", "Slip" };
        foreach (var hit in hits)
        {
            var btn = new Button
            {
                Text = hit,
                Margin = 5,
                WidthRequest = 100,
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
        foreach (var hit in _selectedHits)
        {
            var pill = new Frame
            {
                BackgroundColor = Colors.DimGray,
                CornerRadius = 15,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(2),
                BorderColor = Colors.Transparent,
                Content = new Label { Text = hit, TextColor = Colors.White }
            };
            PillContainer.Children.Add(pill);
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Joining with a comma so your TTS sounds natural
        string result = string.Join(", ", _selectedHits).ToString();
        await CloseAsync(result);
    }
}