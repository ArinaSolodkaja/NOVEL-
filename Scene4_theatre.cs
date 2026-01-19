using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOVEL_
{
    public partial class Scene4_theatre : Form
    {
        private int clickStage = 0;
        private Timer fadeTimer;
        private float fadeAlpha = 0f;
        private bool isFadingIn = false;
        private bool isFadingOut = false;
        private List<Label> currentFadeLabels = new List<Label>();
        private Suspect suspectAlina;
        private Suspect suspectDoctor;

        public Scene4_theatre()
        {
            InitializeComponent();

            SetupInitialState();
            SetupTransparency();

            // Инициализируем подозреваемых
            suspectAlina = new Suspect("Алина Краснова", "Актриса Мариинского театра");
            suspectDoctor = new Suspect("Доктор Львов", "Личный врач графа");

            // Обработчики кликов
            this.Click += Scene4_Click;
            scene4_th.Click += Scene4_Click;
            scene4_vs1.Click += Scene4_Click;
            scene4_vs21.Click += Scene4_Click;
            scene4_vs22.Click += Scene4_Click;
            scene4_vs23.Click += Scene4_Click;
            scene4_vs24.Click += Scene4_Click;
            scene4_vs31.Click += Scene4_Click;
            scene4_vs32.Click += Scene4_Click;
            scene4_vs33.Click += Scene4_Click;
            scene4_vs34.Click += Scene4_Click;

            // Обработчики для кнопок выбора
            scene4_th_ch1.Click += Scene4_th_ch1_Click;
            scene4_th_ch2.Click += Scene4_th_ch2_Click;
        }

        private void SetupInitialState()
        {
            // Скрываем все тексты и кнопки изначально
            scene4_vs1.Visible = false;
            scene4_vs21.Visible = false;
            scene4_vs22.Visible = false;
            scene4_vs23.Visible = false;
            scene4_vs24.Visible = false;
            scene4_vs31.Visible = false;
            scene4_vs32.Visible = false;
            scene4_vs33.Visible = false;
            scene4_vs34.Visible = false;
            scene4_th_ch1.Visible = false;
            scene4_th_ch2.Visible = false;
            scene4_th_ch1.Enabled = false;
            scene4_th_ch2.Enabled = false;

            // Устанавливаем начальную прозрачность
            scene4_vs1.ForeColor = Color.FromArgb(0, scene4_vs1.ForeColor);
            scene4_vs21.ForeColor = Color.FromArgb(0, scene4_vs21.ForeColor);
            scene4_vs22.ForeColor = Color.FromArgb(0, scene4_vs22.ForeColor);
            scene4_vs23.ForeColor = Color.FromArgb(0, scene4_vs23.ForeColor);
            scene4_vs24.ForeColor = Color.FromArgb(0, scene4_vs24.ForeColor);
            scene4_vs31.ForeColor = Color.FromArgb(0, scene4_vs31.ForeColor);
            scene4_vs32.ForeColor = Color.FromArgb(0, scene4_vs32.ForeColor);
            scene4_vs33.ForeColor = Color.FromArgb(0, scene4_vs33.ForeColor);
            scene4_vs34.ForeColor = Color.FromArgb(0, scene4_vs34.ForeColor);
        }

        private void SetupTransparency()
        {
            scene4_th.Dock = DockStyle.Fill;
            scene4_th.SizeMode = PictureBoxSizeMode.StretchImage;

            // Устанавливаем Parent для всех меток
            scene4_vs1.Parent = scene4_th;
            scene4_vs21.Parent = scene4_th;
            scene4_vs22.Parent = scene4_th;
            scene4_vs23.Parent = scene4_th;
            scene4_vs24.Parent = scene4_th;
            scene4_vs31.Parent = scene4_th;
            scene4_vs32.Parent = scene4_th;
            scene4_vs33.Parent = scene4_th;
            scene4_vs34.Parent = scene4_th;

            // Настраиваем UseCompatibleTextRendering
            scene4_vs1.UseCompatibleTextRendering = true;
            scene4_vs21.UseCompatibleTextRendering = true;
            scene4_vs22.UseCompatibleTextRendering = true;
            scene4_vs23.UseCompatibleTextRendering = true;
            scene4_vs24.UseCompatibleTextRendering = true;
            scene4_vs31.UseCompatibleTextRendering = true;
            scene4_vs32.UseCompatibleTextRendering = true;
            scene4_vs33.UseCompatibleTextRendering = true;
            scene4_vs34.UseCompatibleTextRendering = true;
        }

        private void Scene4_Theatre_Load(object sender, EventArgs e)
        {
            scene4_vs1.BringToFront();
            scene4_vs21.BringToFront();
            scene4_vs22.BringToFront();
            scene4_vs23.BringToFront();
            scene4_vs24.BringToFront();
            scene4_vs31.BringToFront();
            scene4_vs32.BringToFront();
            scene4_vs33.BringToFront();
            scene4_vs34.BringToFront();
            scene4_th_ch1.BringToFront();
            scene4_th_ch2.BringToFront();
        }

        private void Scene4_Click(object sender, EventArgs e)
        {
            if (isFadingIn || isFadingOut) return;

            switch (clickStage)
            {
                case 0: // Первый клик - показываем scene4_vs1
                    StartFadeIn(new List<Label> { scene4_vs1 });
                    clickStage = 1;
                    break;

                case 1: // Второй клик - показываем scene4_vs21 и scene4_vs22
                    StartFadeOut(new List<Label> { scene4_vs1 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_vs21, scene4_vs22 });
                        clickStage = 2;
                    });
                    break;

                case 2: // Третий клик - показываем scene4_vs23 и scene4_vs24
                    StartFadeOut(new List<Label> { scene4_vs21, scene4_vs22 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_vs23, scene4_vs24 });
                        clickStage = 3;
                    });
                    break;

                case 3: // Четвертый клик - показываем scene4_vs31 и scene4_vs32
                    StartFadeOut(new List<Label> { scene4_vs23, scene4_vs24 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_vs31, scene4_vs32 });
                        clickStage = 4;
                    });
                    break;

                case 4: // Пятый клик - показываем scene4_vs33 и scene4_vs34
                    StartFadeOut(new List<Label> { scene4_vs31, scene4_vs32 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_vs33, scene4_vs34 });
                        clickStage = 5;
                    });
                    break;

                case 5: // Шестой клик - показываем кнопки выбора
                    StartFadeOut(new List<Label> { scene4_vs33, scene4_vs34 }, () =>
                    {
                        ShowChoiceButtons();
                        clickStage = 6;
                    });
                    break;
            }
        }

        private void ShowChoiceButtons()
        {
            // Анимация появления кнопок
            scene4_th_ch1.Visible = true;
            scene4_th_ch2.Visible = true;
            scene4_th_ch1.Enabled = true;
            scene4_th_ch2.Enabled = true;

            // Начальная позиция (под экраном)
            int centerX = this.ClientSize.Width / 2 - scene4_th_ch1.Width / 2;
            scene4_th_ch1.Location = new Point(centerX, this.ClientSize.Height + 50);
            scene4_th_ch2.Location = new Point(centerX, this.ClientSize.Height + 180);

            // Анимация подъема кнопок
            Timer animationTimer = new Timer();
            animationTimer.Interval = 20;
            int step = 0;
            int totalSteps = 25;

            animationTimer.Tick += (s, args) =>
            {
                step++;

                // Вычисляем новую позицию
                int targetY1 = this.ClientSize.Height - 250;
                int targetY2 = this.ClientSize.Height - 120;
                int startY1 = this.ClientSize.Height + 50;
                int startY2 = this.ClientSize.Height + 180;

                int newY1 = startY1 - (int)((startY1 - targetY1) * ((float)step / totalSteps));
                int newY2 = startY2 - (int)((startY2 - targetY2) * ((float)step / totalSteps));

                scene4_th_ch1.Location = new Point(scene4_th_ch1.Location.X, newY1);
                scene4_th_ch2.Location = new Point(scene4_th_ch2.Location.X, newY2);

                if (step >= totalSteps)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();

                    // Финальные позиции
                    scene4_th_ch1.Location = new Point(centerX, targetY1);
                    scene4_th_ch2.Location = new Point(centerX, targetY2);
                }
            };

            animationTimer.Start();
        }

        private void Scene4_th_ch1_Click(object sender, EventArgs e)
        {
            // Анимация нажатия кнопки
            Button button = sender as Button;
            Color originalColor = button.BackColor;
            button.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                button.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // Создаем улику против доктора Львова
                InteractiveObject doctorEvidence = InteractiveObject.TestimonyAgainstDoctor();

                // Добавляем улику доктору
                suspectDoctor.AddEvidence(doctorEvidence.Name, doctorEvidence.EvidenceValue);

                // Показываем стилизованный MessageBox с переходом на Scene5
                ShowStylizedMessageBoxWithTransition(
                    "+ Улика против Доктора Львова",
                    "Вы решили поверить Алине Красновой.\n\n" +
                    "Её показания добавляют улику против доктора Львова:\n" +
                    "• Утверждает, что видела доктора у калитки после убийства\n" +
                    "• Описывает его паническое бегство\n" +
                    "• Кинжал мог быть подброшен\n\n" +
                    $"Уровень подозрительности доктора Львова: {suspectDoctor.SuspicionLevel}",
                    "🔍 Улика добавлена",
                    Color.FromArgb(60, 30, 15)
                );
            };
            clickTimer.Start();
        }

        private void Scene4_th_ch2_Click(object sender, EventArgs e)
        {
            // Анимация нажатия кнопки
            Button button = sender as Button;
            Color originalColor = button.BackColor;
            button.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                button.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // Создаем улику против Алины Красновой
                InteractiveObject alinaEvidence = InteractiveObject.KnifeEvidence();

                // Добавляем улику Алине
                suspectAlina.AddEvidence(alinaEvidence.Name, alinaEvidence.EvidenceValue);

                // Показываем стилизованный MessageBox с переходом на Scene5
                ShowStylizedMessageBoxWithTransition(
                    "+ Улика против Алины Красновой",
                    "Вы решили не доверять Алине Красновой.\n\n" +
                    "Кинжал с её инициалами - серьёзная улика:\n" +
                    "• Кинжал найден на месте преступления\n" +
                    "• Гравировка «А.К.» соответствует её инициалам\n" +
                    "• Её версия о возвращении кинжала неубедительна\n\n" +
                    $"Уровень подозрительности Алины Красновой: {suspectAlina.SuspicionLevel}",
                    "🔍 Улика добавлена",
                    Color.FromArgb(40, 20, 10)
                );
            };
            clickTimer.Start();
        }

        // НОВЫЙ МЕТОД С ПЕРЕХОДОМ НА SCENE5
        private void ShowStylizedMessageBoxWithTransition(string title, string message, string buttonText, Color panelColor)
        {
            // Создаем кастомную форму для сообщения
            Form messageForm = new Form();
            messageForm.Text = title;
            messageForm.Size = new Size(500, 400);
            messageForm.StartPosition = FormStartPosition.CenterParent;
            messageForm.BackColor = Color.FromArgb(30, 15, 5);
            messageForm.ForeColor = Color.Gold;
            messageForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            messageForm.MaximizeBox = false;
            messageForm.MinimizeBox = false;

            // Панель содержимого
            Panel contentPanel = new Panel();
            contentPanel.BackColor = panelColor;
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Size = new Size(480, 350);
            contentPanel.Location = new Point(10, 10);

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.Goldenrod;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(440, 40);
            titleLabel.Text = title;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Иконка
            Label iconLabel = new Label();
            iconLabel.Font = new Font("Arial", 36, FontStyle.Bold);
            iconLabel.ForeColor = Color.Gold;
            iconLabel.Location = new Point(220, 70);
            iconLabel.Size = new Size(60, 60);
            iconLabel.Text = "🔍";
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Сообщение
            Label messageLabel = new Label();
            messageLabel.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(20, 140);
            messageLabel.Size = new Size(440, 150);
            messageLabel.Text = message;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка OK
            Button okButton = new Button();
            okButton.Text = buttonText;
            okButton.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            okButton.Size = new Size(200, 40);
            okButton.Location = new Point(140, 300);
            okButton.BackColor = Color.FromArgb(50, 25, 12);
            okButton.ForeColor = Color.Goldenrod;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderColor = Color.SaddleBrown;
            okButton.FlatAppearance.BorderSize = 2;
            okButton.Cursor = Cursors.Hand;

            // ИЗМЕНЕННЫЙ ОБРАБОТЧИК: ЗАКРЫВАЕМ ФОРМУ И ПЕРЕХОДИМ НА SCENE5
            okButton.Click += (s, args) =>
            {
                messageForm.Close();
                // Переход на Scene5 после закрытия MessageBox
                NavigateToScene5();
            };

            // Эффекты при наведении на кнопку
            okButton.MouseEnter += (s, args) => okButton.BackColor = Color.FromArgb(60, 30, 15);
            okButton.MouseLeave += (s, args) => okButton.BackColor = Color.FromArgb(50, 25, 12);
            okButton.MouseDown += (s, args) => okButton.BackColor = Color.FromArgb(30, 15, 5);

            // Добавляем элементы
            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(iconLabel);
            contentPanel.Controls.Add(messageLabel);
            contentPanel.Controls.Add(okButton);

            messageForm.Controls.Add(contentPanel);

            // Обработчик закрытия формы (если пользователь закрыл крестиком)
            messageForm.FormClosed += (s, args) => NavigateToScene5();

            messageForm.ShowDialog();
        }

        // МЕТОД ДЛЯ ПЕРЕХОДА НА SCENE5
        private void NavigateToScene5()
        {
            // Создаем и показываем Scene5
            Scene5_fin nextScene = new Scene5_fin();
            nextScene.Show();

            // Закрываем текущую форму
            this.Hide();
        }

        // СТАРЫЙ МЕТОД (ОСТАВЛЯЕМ ДЛЯ СОВМЕСТИМОСТИ, ЕСЛИ ГДЕ-ТО ИСПОЛЬЗУЕТСЯ)
        private void ShowStylizedMessageBox(string title, string message, string buttonText, Color panelColor)
        {
            // Создаем кастомную форму для сообщения
            Form messageForm = new Form();
            messageForm.Text = title;
            messageForm.Size = new Size(500, 400);
            messageForm.StartPosition = FormStartPosition.CenterParent;
            messageForm.BackColor = Color.FromArgb(30, 15, 5);
            messageForm.ForeColor = Color.Gold;
            messageForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            messageForm.MaximizeBox = false;
            messageForm.MinimizeBox = false;

            // Панель содержимого
            Panel contentPanel = new Panel();
            contentPanel.BackColor = panelColor;
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Size = new Size(480, 350);
            contentPanel.Location = new Point(10, 10);

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.Goldenrod;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(440, 40);
            titleLabel.Text = title;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Иконка
            Label iconLabel = new Label();
            iconLabel.Font = new Font("Arial", 36, FontStyle.Bold);
            iconLabel.ForeColor = Color.Gold;
            iconLabel.Location = new Point(220, 70);
            iconLabel.Size = new Size(60, 60);
            iconLabel.Text = "🔍";
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Сообщение
            Label messageLabel = new Label();
            messageLabel.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(20, 140);
            messageLabel.Size = new Size(440, 150);
            messageLabel.Text = message;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка OK
            Button okButton = new Button();
            okButton.Text = buttonText;
            okButton.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            okButton.Size = new Size(200, 40);
            okButton.Location = new Point(140, 300);
            okButton.BackColor = Color.FromArgb(50, 25, 12);
            okButton.ForeColor = Color.Goldenrod;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderColor = Color.SaddleBrown;
            okButton.FlatAppearance.BorderSize = 2;
            okButton.Cursor = Cursors.Hand;
            okButton.Click += (s, args) => messageForm.Close();

            // Эффекты при наведении на кнопку
            okButton.MouseEnter += (s, args) => okButton.BackColor = Color.FromArgb(60, 30, 15);
            okButton.MouseLeave += (s, args) => okButton.BackColor = Color.FromArgb(50, 25, 12);
            okButton.MouseDown += (s, args) => okButton.BackColor = Color.FromArgb(30, 15, 5);

            // Добавляем элементы
            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(iconLabel);
            contentPanel.Controls.Add(messageLabel);
            contentPanel.Controls.Add(okButton);

            messageForm.Controls.Add(contentPanel);
            messageForm.ShowDialog();
        }

        private void StartFadeIn(List<Label> labels)
        {
            currentFadeLabels = labels;
            isFadingIn = true;
            fadeAlpha = 0f;

            foreach (var label in labels)
            {
                label.Visible = true;
                label.BringToFront();
            }

            fadeTimer = new Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += FadeIn_Tick;
            fadeTimer.Start();
        }

        private void StartFadeOut(List<Label> labels, Action onComplete = null)
        {
            currentFadeLabels = labels;
            isFadingOut = true;
            fadeAlpha = 1f;

            fadeTimer = new Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += (s, args) =>
            {
                fadeAlpha -= 0.05f;

                if (fadeAlpha <= 0)
                {
                    fadeAlpha = 0;
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                    isFadingOut = false;

                    foreach (var label in labels)
                    {
                        label.Visible = false;
                    }

                    onComplete?.Invoke();
                }

                foreach (var label in labels)
                {
                    label.ForeColor = Color.FromArgb((int)(fadeAlpha * 255), label.ForeColor);
                }
            };
            fadeTimer.Start();
        }

        private void FadeIn_Tick(object sender, EventArgs e)
        {
            fadeAlpha += 0.05f;

            if (fadeAlpha >= 1)
            {
                fadeAlpha = 1;
                fadeTimer.Stop();
                fadeTimer.Dispose();
                isFadingIn = false;
            }

            foreach (var label in currentFadeLabels)
            {
                label.ForeColor = Color.FromArgb((int)(fadeAlpha * 255), label.ForeColor);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (fadeTimer != null)
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();
            }
        }

        private void scene4_vs1_Click(object sender, EventArgs e)
        {
            Scene4_Click(sender, e);
        }
    }
}