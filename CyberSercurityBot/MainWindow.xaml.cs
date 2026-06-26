using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;

// Cybersecurity Awareness Bot - IIE Assignment Project

namespace CyberSecurityBot
{
    public partial class MainWindow : Window
    {
        // ---------- CHATBOT VARIABLES ----------
        private string userName = "";
        private bool waitingForName = true;

        // ---------- QUIZ VARIABLES ----------
        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
        private int currentQuestionIndex = 0;
        private int quizScore = 0;
        private bool quizActive = false;
        private int selectedAnswerIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // Chatbot events
            btnSend.Click += BtnSend_Click;
            btnClearChat.Click += BtnClearChat_Click;
            txtUserInput.KeyDown += TxtUserInput_KeyDown;

            // Quiz events
            btnStartQuiz.Click += BtnStartQuiz_Click;
            btnAnswer1.Click += (s, e) => AnswerClicked(0);
            btnAnswer2.Click += (s, e) => AnswerClicked(1);
            btnAnswer3.Click += (s, e) => AnswerClicked(2);
            btnAnswer4.Click += (s, e) => AnswerClicked(3);

            // Initialize quiz questions
            InitializeQuizQuestions();
        }

        // ---------- CHATBOT METHODS ----------
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddMessage("🛡️ ===== CYBERSECURITY AWARENESS BOT ===== 🛡️", Colors.Cyan);
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

        private void BtnClearChat_Click(object sender, RoutedEventArgs e)
        {
            rtbChat.Document.Blocks.Clear();
            AddMessage("Bot: Chat cleared! Ask me about cybersecurity!", Colors.Yellow);
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
                AddMessage($"Bot: Nice to meet you, {userName}! 🎉", Colors.LightGreen);
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
                return $"Goodbye {userName}! Stay safe online! 😊";

            // ADD THIS NEW CODE RIGHT HERE ↓↓↓
            if (lower.Contains("update") || lower.Contains("windows update"))
                return "🔄 **Keep Windows Updated:** Regular updates fix security vulnerabilities! Enable automatic updates!";

            if (lower.Contains("help"))
                return $"I can help with: passwords, phishing, safe browsing, 2FA, malware, and social media safety";

            if (lower.Contains("password"))
                return $"**Password Safety:** Use strong passwords (12+ characters, mix of letters/numbers/symbols";

            if (lower.Contains("phish"))
                return $"**Phishing Alert:** Never click suspicious links! Check sender email addresses carefully";

            if (lower.Contains("brows"))
                return $"**Website**";

            // ... rest of your code
        }

        // ---------- QUIZ METHODS ----------
        private void InitializeQuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What is the best way to create a strong password?",
                    new List<string>
                    {
                        "A) Use your pet's name and birth year",
                        "B) Use 12+ characters with letters, numbers, and symbols",
                        "C) Use 'password123'",
                        "D) Use the same password for all accounts"
                    },
                    1
                ),
                new QuizQuestion(
                    "What should you do if you receive a suspicious email asking for personal information?",
                    new List<string>
                    {
                        "A) Reply with the information requested",
                        "B) Click on the link to verify",
                        "C) Delete it immediately and report it",
                        "D) Forward it to your friends"
                    },
                    2
                ),
                new QuizQuestion(
                    "What does the padlock icon in your browser address bar indicate?",
                    new List<string>
                    {
                        "A) The website is using encryption (HTTPS)",
                        "B) The website is safe to download files from",
                        "C) The website is owned by a government",
                        "D) The website has no viruses"
                    },
                    0
                ),
                new QuizQuestion(
                    "What is Two-Factor Authentication (2FA)?",
                    new List<string>
                    {
                        "A) Having two different passwords",
                        "B) An extra security layer that requires a second verification step",
                        "C) Using two different browsers",
                        "D) Having two email accounts"
                    },
                    1
                ),
                new QuizQuestion(
                    "Which of these is a sign of a phishing email?",
                    new List<string>
                    {
                        "A) Professional language and correct spelling",
                        "B) Request for personal information and urgent demands",
                        "C) Personal greeting with your name",
                        "D) A well-designed company logo"
                    },
                    1
                ),
                new QuizQuestion(
                    "What should you do to protect yourself from malware?",
                    new List<string>
                    {
                        "A) Never use antivirus software",
                        "B) Download files from any website",
                        "C) Install reliable antivirus and keep it updated",
                        "D) Disable Windows updates"
                    },
                    2
                ),
                new QuizQuestion(
                    "Which password is the strongest?",
                    new List<string>
                    {
                        "A) password123",
                        "B) admin",
                        "C) 12345678",
                        "D) P@s5w0rd!2024$ecure"
                    },
                    3
                ),
                new QuizQuestion(
                    "What is the safest way to share personal information online?",
                    new List<string>
                    {
                        "A) Post it on social media",
                        "B) Share it in public chat rooms",
                        "C) Only share it through secure, verified websites",
                        "D) Email it to everyone you know"
                    },
                    2
                ),
                new QuizQuestion(
                    "Why should you use a password manager?",
                    new List<string>
                    {
                        "A) To store all your passwords in one place securely",
                        "B) To share passwords with everyone",
                        "C) To make passwords shorter",
                        "D) To write passwords down on paper"
                    },
                    0
                ),
                new QuizQuestion(
                    "What should you do if you think your account has been hacked?",
                    new List<string>
                    {
                        "A) Ignore it",
                        "B) Change your password immediately and enable 2FA",
                        "C) Post about it on social media",
                        "D) Continue using the same password"
                    },
                    1
                )
            };
        }

        private void BtnStartQuiz_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();
        }

        private void StartQuiz()
        {
            // Reset quiz state
            currentQuestionIndex = 0;
            quizScore = 0;
            quizActive = true;
            selectedAnswerIndex = -1;

            // Update UI
            btnStartQuiz.Content = "🔄 Restart Quiz";
            txtResult.Text = "";

            // Show first question
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (!quizActive || currentQuestionIndex >= quizQuestions.Count)
            {
                EndQuiz();
                return;
            }

            var question = quizQuestions[currentQuestionIndex];

            // Display question
            txtQuestion.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}\n\n" + question.Question;

            // Display answers
            btnAnswer1.Content = question.Answers[0];
            btnAnswer2.Content = question.Answers[1];
            btnAnswer3.Content = question.Answers[2];
            btnAnswer4.Content = question.Answers[3];

            // Reset button colors
            btnAnswer1.Background = new SolidColorBrush(Color.FromRgb(26, 26, 46));
            btnAnswer2.Background = new SolidColorBrush(Color.FromRgb(26, 26, 46));
            btnAnswer3.Background = new SolidColorBrush(Color.FromRgb(26, 26, 46));
            btnAnswer4.Background = new SolidColorBrush(Color.FromRgb(26, 26, 46));

            btnAnswer1.IsEnabled = true;
            btnAnswer2.IsEnabled = true;
            btnAnswer3.IsEnabled = true;
            btnAnswer4.IsEnabled = true;

            // Update progress
            txtScore.Text = $"Score: {quizScore}/{quizQuestions.Count}";
            txtProgress.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}";

            // Clear previous result
            txtResult.Text = "";
            selectedAnswerIndex = -1;
        }

        private void AnswerClicked(int answerIndex)
        {
            if (!quizActive || selectedAnswerIndex != -1 || currentQuestionIndex >= quizQuestions.Count)
                return;

            selectedAnswerIndex = answerIndex;
            var question = quizQuestions[currentQuestionIndex];
            bool isCorrect = answerIndex == question.CorrectAnswerIndex;

            // Highlight the answer
            Button clickedButton = GetButtonByIndex(answerIndex);

            if (isCorrect)
            {
                quizScore++;
                clickedButton.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // Green
                txtResult.Text = "✅ Correct! Well done! 🎉";
                txtResult.Foreground = new SolidColorBrush(Color.FromRgb(76, 175, 80));
            }
            else
            {
                clickedButton.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54)); // Red
                // Show correct answer
                Button correctButton = GetButtonByIndex(question.CorrectAnswerIndex);
                correctButton.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // Green
                txtResult.Text = $"❌ Incorrect! The correct answer was: {question.Answers[question.CorrectAnswerIndex]}";
                txtResult.Foreground = new SolidColorBrush(Color.FromRgb(244, 67, 54));
            }

            // Disable all answer buttons
            btnAnswer1.IsEnabled = false;
            btnAnswer2.IsEnabled = false;
            btnAnswer3.IsEnabled = false;
            btnAnswer4.IsEnabled = false;

            // Update score
            txtScore.Text = $"Score: {quizScore}/{quizQuestions.Count}";

            // Move to next question after delay
            currentQuestionIndex++;

            // Show next question after a delay
            if (currentQuestionIndex < quizQuestions.Count)
            {
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    ShowQuestion();
                };
                timer.Start();
            }
            else
            {
                // Quiz ended
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    EndQuiz();
                };
                timer.Start();
            }
        }

        private Button GetButtonByIndex(int index)
        {
            switch (index)
            {
                case 0: return btnAnswer1;
                case 1: return btnAnswer2;
                case 2: return btnAnswer3;
                case 3: return btnAnswer4;
                default: return null;
            }
        }

        private void EndQuiz()
        {
            quizActive = false;
            txtQuestion.Text = $"🏁 Quiz Complete!";

            double percentage = (double)quizScore / quizQuestions.Count * 100;
            string grade = percentage >= 80 ? "Excellent! 🌟" :
                          percentage >= 60 ? "Good! 👍" :
                          percentage >= 40 ? "Keep learning! 📚" : "Study more! 📖";

            txtResult.Text = $"You scored {quizScore} out of {quizQuestions.Count} ({percentage:F0}%) - {grade}";
            txtResult.Foreground = new SolidColorBrush(Color.FromRgb(255, 193, 7)); // Yellow

            txtScore.Text = $"Score: {quizScore}/{quizQuestions.Count}";
            txtProgress.Text = $"Complete!";

            btnAnswer1.Content = "";
            btnAnswer2.Content = "";
            btnAnswer3.Content = "";
            btnAnswer4.Content = "";

            btnAnswer1.IsEnabled = false;
            btnAnswer2.IsEnabled = false;
            btnAnswer3.IsEnabled = false;
            btnAnswer4.IsEnabled = false;
        }
    }

    // ---------- QUIZ CLASS ----------
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public QuizQuestion(string question, List<string> answers, int correctAnswerIndex)
        {
            Question = question;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}