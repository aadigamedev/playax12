using RAGE;
using RAGE.Ui;
using System; // Timer ke liye

public class LoadingScreen : Script
{
    private static HtmlWindow loadingScreen;

    public LoadingScreen()
    {
        Events.OnPlayerConnected += OnPlayerConnected; // Event ka naam sahi kiya
    }

    private void OnPlayerConnected(Player player) // Delegate sahi kiya
    {
        // लोडिंग स्क्रीन बनाएँ
        loadingScreen = new HtmlWindow("package://loadingscreen/index.html");

        // 20 सेकंड के बाद लोडिंग स्क्रीन को छुपाएँ
        System.Timers.Timer timer = new System.Timers.Timer(20000); // Timer object banaya
        timer.Elapsed += (s, args) => { // Timer elapse hone par kya karna hai
            if (loadingScreen != null)
            {
                loadingScreen.Destroy();
                loadingScreen = null;
            }
            timer.Stop(); // Timer ko stop karo
            timer.Dispose(); // Timer ko dispose karo
        };
        timer.AutoReset = false; // Timer ek baar hi chalega
        timer.Enabled = true; // Timer start karo
    }
}