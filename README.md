# BoxingRoundApp 🥊

A high-performance, sports-themed boxing timer and workout architect built with **.NET MAUI**. Designed for fighters who need precise round management, custom punch combinations, and a "Combat-Dark" aesthetic.

---

## 🚀 Features

* **Dynamic Round Management:** Set unique work and rest durations for every single round.
* **Punch Combo Integration:** Display specific punch combinations (e.g., "jab-cross-hook-slip") directly on the timer screen.
* **High-Intensity UI:** A "Combat-Dark" theme using high-contrast colors (Boxing Red and Gold) for visibility during heavy training.
* **Smart Timer Service:** A foreground-capable timer service that remains accurate even if the app is backgrounded.
* **Visual Warning System:** The UI pulses red during the final 10 seconds of a round to alert the user through peripheral vision.
  
<img width="190" height="377" alt="Screenshot 2026-03-05 105030" src="https://github.com/user-attachments/assets/534e9f5a-ac93-42f4-9ff8-1d08d6b04555" />
<img width="190" height="377" alt="Screenshot 2026-03-05 105002" src="https://github.com/user-attachments/assets/6908d1a8-7d92-4b7b-8e68-67063eff78d5" />
<img width="190" height="377" alt="Screenshot 2026-03-05 104943" src="https://github.com/user-attachments/assets/735ac033-7f15-40c1-8380-fbcc365ad1b4" />
<img width="190" height="377" alt="Screenshot 2026-03-05 104856" src="https://github.com/user-attachments/assets/e0b635de-5c1c-4641-aed2-998e23e6607f" />
<img width="190" height="377" alt="Screenshot 2026-03-05 105105" src="https://github.com/user-attachments/assets/bf886669-c888-4151-9ae9-25a22f690418" />

---

## 🛠 Tech Stack

* **Framework:** .NET MAUI (Multi-platform App UI)
* **Language:** C# 12.0
* **Pattern:** MVVM (Model-View-ViewModel) with **CommunityToolkit.Mvvm**
* **Local Storage:** SQLite (via SQLite-net-pcl) for persisting workout profiles and round settings.
* **Toolkit:** .NET MAUI Community Toolkit (TouchBehaviors, MediaElement, and Animations).


---

## 🏗 Architecture Details

The app utilizes a centralized `TimerService` to manage the countdown logic. This ensures that UI updates are decoupled from the timing engine, allowing for a smooth 60fps experience even while the `MediaElement` handles audio cues.

* **Models:** `WorkoutProfile`, `RoundSettings`
* **ViewModels:** `MainViewModel`, `ActiveWorkoutViewModel`, `CreateProfileViewModel`
* **Services:** `ITimerService`, `IDatabaseService`

---

## 🧠 Challenges & Engineering Solutions

### 1. Synchronizing UI State with Background Services
**Challenge:** Keeping the UI timer in perfect sync with the underlying `TimerService` without causing "UI stutter" or thread-locking.
**Solution:** Implemented the `ObservableProperty` pattern from the **CommunityToolkit.Mvvm**. By using a thread-safe messaging system, the background service broadcasts ticks to the ViewModel, which updates the view via high-performance Data Bindings.

### 2. Android 12+ Splash Screen & Adaptive Icons
**Challenge:** Modern Android versions apply a circular mask to splash screens, which caused horizontal branding text to be clipped.
**Solution:** Redesigned the branding assets using a **Stacked SVG Architecture**. By calculating the "Safe Zone" coordinates (center 66%) within the SVG ViewBox, I ensured the branding remains perfectly centered and legible across all device aspect ratios.


### 3. State-Driven UI with DataTriggers
**Challenge:** Handling multiple visual states (Work, Rest, Paused, Warning) without bloating the code-behind with complex imperative logic.
**Solution:** Leveraged **XAML DataTriggers**. The UI color palette and button states transform automatically based on the `IsWorkPhase` and `IsPaused` booleans in the ViewModel. This ensures the Source of Truth" remains in the logic layer while the View reacts declaratively.

## ⚙️ Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/BoxingRoundApp.git](https://github.com/your-username/BoxingRoundApp.git)
    ```
2.  **Open the solution:**
    Open `BoxingRoundApp.sln` in **Visual Studio 2022**.
3.  **Restore workloads:**
    ```bash
    dotnet workload restore
    ```
4.  **Run:**
    Select your target (Android/iOS) and press **F5**.

---

## 📈 Future Roadmap
* [ ] **WearOS Support:** Control the timer from a smartwatch while wearing gloves.
* [ ] **Voice Commands:** Start/Stop rounds using voice recognition.
* [ ] **Workout Summary Sharing:** Generate high-contrast "stat cards" to share on social media.

---

## 📝 License

Distributed under the MIT License. See `LICENSE` for more information.
