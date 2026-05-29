using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;

namespace CyberSecurityBot
{
    public partial class MainWindow : Window
    {
        private string userName = "";
        private bool waitingForName = true;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            btnSend.Click += BtnSend_Click;
            txtUserInput.KeyDown += TxtUserInput_KeyDown;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddMessage("Bot: Hello! Welcome to the Cybersecurity Awareness Bot!", Colors.LightGreen);
            AddMessage("Bot: What is your name?", Colors.Cyan);
        }

        private void AddMessage(string message, Color color)
        {
            Run run = new Run(message + "\n\n");
            run.Foreground = new SolidColorBrush(color);
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(run);
            rtbChat.Document.Blocks.Add(paragraph);
            scrollViewer.ScrollToEnd();
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ProcessInput();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                AddMessage("Bot: Please say something!", Colors.Orange);
                return;
            }

            AddMessage($"You: {input}", Colors.LightBlue);

            if (waitingForName)
            {
                userName = input;
                waitingForName = false;
                AddMessage($"Bot: Nice to meet you, {userName}!", Colors.LightGreen);
                AddMessage("Bot: Ask me about passwords, phishing, or type 'help'", Colors.Yellow);
            }
            else
            {
                string response = GetResponse(input);
                AddMessage($"Bot: {response}", Colors.White);
            }

            txtUserInput.Clear();
            txtUserInput.Focus();
        }

        private string GetResponse(string input)
        {
            string lower = input.ToLower();

            if (lower.Contains("exit") || lower.Contains("quit") || lower.Contains("bye"))
                return $"Goodbye {userName}! Stay safe online! 👋";

            if (lower.Contains("help"))
                return "📚 I can help with: passwords, phishing, safe browsing, 2FA. Just ask!";

            if (lower.Contains("password"))
                return "🔐 Use strong passwords (12+ characters, mix of letters/numbers/symbols). Never reuse passwords!";

            if (lower.Contains("phish") || lower.Contains("scam"))
                return "🎣 Don't click suspicious links! Check sender email addresses. Never share personal info.";

            if (lower.Contains("brows") || lower.Contains("website"))
                return "🌐 Look for 'https://' and the padlock icon. Avoid pop-ups and unknown downloads.";

            if (lower.Contains("2fa") || lower.Contains("two factor"))
                return "📱 2FA adds extra security. Use authenticator apps like Google Authenticator.";

            if (lower.Contains("how are you"))
                return $"I'm great {userName}! Ready to teach you about cybersecurity! 😊";

            return $"I didn't understand that, {userName}. Try asking about: passwords, phishing, or safe browsing!";
        }
    }
}