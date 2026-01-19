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
    public partial class Scene4_dc : Form
    {
        private int clickStage = 0;
        private Timer fadeTimer;
        private float fadeAlpha = 0f;
        private bool isFadingIn = false;
        private bool isFadingOut = false;
        private List<Label> currentFadeLabels = new List<Label>();
        private Suspect suspectDoctor;
        private Suspect suspectCountess;

        public Scene4_dc()
        {
            InitializeComponent();

            SetupInitialState();
            SetupTransparency();

            // Инициализируем подозреваемых
            suspectDoctor = new Suspect("Доктор Львов", "Личный врач графа");
            suspectCountess = new Suspect("Графиня Воронцова", "Жена графа");

            // Обработчики кликов
            this.Click += Scene4_dc_Click;
            scene4_dc_fon.Click += Scene4_dc_Click;
            scene4_dc11.Click += Scene4_dc_Click;
            scene4_dc21.Click += Scene4_dc_Click;
            scene4_dc22.Click += Scene4_dc_Click;
            scene4_dc31.Click += Scene4_dc_Click;
            scene4_dc32.Click += Scene4_dc_Click;
            scene4_dc41.Click += Scene4_dc_Click;

            // Обработчики для кнопок выбора
            scene4_dc_ch1.Click += Scene4_dc_ch1_Click;
            scene4_dc_ch2.Click += Scene4_dc_ch2_Click;
        }

        private void SetupInitialState()
        {
            // Скрываем все тексты и кнопки изначально
            scene4_dc11.Visible = false;
            scene4_dc21.Visible = false;
            scene4_dc22.Visible = false;
            scene4_dc31.Visible = false;
            scene4_dc32.Visible = false;
            scene4_dc41.Visible = false;
            scene4_dc_ch1.Visible = false;
            scene4_dc_ch2.Visible = false;
            scene4_dc_ch1.Enabled = false;
            scene4_dc_ch2.Enabled = false;

            // Устанавливаем начальную прозрачность (ВСЕ ТЕКСТЫ СЦЕНЫ БЕЛЫЕ)
            scene4_dc11.ForeColor = Color.FromArgb(0, Color.White);
            scene4_dc21.ForeColor = Color.FromArgb(0, Color.White);
            scene4_dc22.ForeColor = Color.FromArgb(0, Color.White);
            scene4_dc31.ForeColor = Color.FromArgb(0, Color.White);
            scene4_dc32.ForeColor = Color.FromArgb(0, Color.White);
            scene4_dc41.ForeColor = Color.FromArgb(0, Color.White);
        }

        private void SetupTransparency()
        {
            scene4_dc_fon.Dock = DockStyle.Fill;
            scene4_dc_fon.SizeMode = PictureBoxSizeMode.StretchImage;

            // Устанавливаем Parent для всех меток
            scene4_dc11.Parent = scene4_dc_fon;
            scene4_dc21.Parent = scene4_dc_fon;
            scene4_dc22.Parent = scene4_dc_fon;
            scene4_dc31.Parent = scene4_dc_fon;
            scene4_dc32.Parent = scene4_dc_fon;
            scene4_dc41.Parent = scene4_dc_fon;

            // ВСЕ ТЕКСТЫ СЦЕНЫ БЕЛОГО ЦВЕТА
            scene4_dc11.ForeColor = Color.White; // Заголовок - белый
            scene4_dc21.ForeColor = Color.White; // Диалог доктора - белый
            scene4_dc22.ForeColor = Color.White; // Диалог доктора - белый
            scene4_dc31.ForeColor = Color.White; // Реплика следователя - белый
            scene4_dc32.ForeColor = Color.White; // Реплика следователя - белый
            scene4_dc41.ForeColor = Color.White; // Диалог доктора - белый

            // Настраиваем UseCompatibleTextRendering
            scene4_dc11.UseCompatibleTextRendering = true;
            scene4_dc21.UseCompatibleTextRendering = true;
            scene4_dc22.UseCompatibleTextRendering = true;
            scene4_dc31.UseCompatibleTextRendering = true;
            scene4_dc32.UseCompatibleTextRendering = true;
            scene4_dc41.UseCompatibleTextRendering = true;
        }

        private void Scene4_dc_Click(object sender, EventArgs e)
        {
            if (isFadingIn || isFadingOut) return;

            switch (clickStage)
            {
                case 0: // Первый клик - показываем scene4_dc11
                    StartFadeIn(new List<Label> { scene4_dc11 });
                    clickStage = 1;
                    break;

                case 1: // Второй клик - показываем scene4_dc21 и scene4_dc22
                    StartFadeOut(new List<Label> { scene4_dc11 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_dc21, scene4_dc22 });
                        clickStage = 2;
                    });
                    break;

                case 2: // Третий клик - показываем scene4_dc31 и scene4_dc32
                    StartFadeOut(new List<Label> { scene4_dc21, scene4_dc22 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_dc31, scene4_dc32 });
                        clickStage = 3;
                    });
                    break;

                case 3: // Четвертый клик - показываем scene4_dc41
                    StartFadeOut(new List<Label> { scene4_dc31, scene4_dc32 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene4_dc41 });
                        clickStage = 4;
                    });
                    break;

                case 4: // Пятый клик - показываем кнопки выбора
                    StartFadeOut(new List<Label> { scene4_dc41 }, () =>
                    {
                        ShowChoiceButtons();
                        clickStage = 5;
                    });
                    break;
            }
        }

        private void ShowChoiceButtons()
        {
            // Анимация появления кнопок
            scene4_dc_ch1.Visible = true;
            scene4_dc_ch2.Visible = true;
            scene4_dc_ch1.Enabled = true;
            scene4_dc_ch2.Enabled = true;

            // Начальная позиция (под экраном)
            int centerX = this.ClientSize.Width / 2 - scene4_dc_ch1.Width / 2;
            scene4_dc_ch1.Location = new Point(centerX, this.ClientSize.Height + 50);
            scene4_dc_ch2.Location = new Point(centerX, this.ClientSize.Height + 180);

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

                scene4_dc_ch1.Location = new Point(scene4_dc_ch1.Location.X, newY1);
                scene4_dc_ch2.Location = new Point(scene4_dc_ch2.Location.X, newY2);

                if (step >= totalSteps)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();

                    // Финальные позиции
                    scene4_dc_ch1.Location = new Point(centerX, targetY1);
                    scene4_dc_ch2.Location = new Point(centerX, targetY2);
                }
            };

            animationTimer.Start();
        }

        private void Scene4_dc_ch1_Click(object sender, EventArgs e)
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

                // Создаем улику против графини Воронцовой
                InteractiveObject countessEvidence = InteractiveObject.ServantTestimony();

                // Добавляем улику графине
                suspectCountess.AddEvidence(countessEvidence.Name, countessEvidence.EvidenceValue);

                // Показываем стилизованный MessageBox с переходом на Scene5
                ShowStylizedMessageBoxWithTransition(
                    "+ Улика против Графини Воронцовой",
                    "Вы решили поверить доктору Львову.\n\n" +
                    "Его показания добавляют улику против графини Воронцовой:\n" +
                    "• Доктор видел горничную Анну у сейфа графа\n" +
                    "• Утверждает, что в доме все боятся графини\n" +
                    "• Возможно, графиня руководила действиями горничной\n\n" +
                    $"Уровень подозрительности графини Воронцовой: {suspectCountess.SuspicionLevel}",
                    "🔍 Улика добавлена",
                    Color.FromArgb(60, 30, 15)
                );
            };
            clickTimer.Start();
        }

        private void Scene4_dc_ch2_Click(object sender, EventArgs e)
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
                InteractiveObject doctorEvidence = InteractiveObject.MedicalRecords();

                // Добавляем улику доктору
                suspectDoctor.AddEvidence(doctorEvidence.Name, doctorEvidence.EvidenceValue);

                // Показываем стилизованный MessageBox с переходом на Scene5
                ShowStylizedMessageBoxWithTransition(
                    "+ Улика против Доктора Львова",
                    "Вы не поверили доктору Львову.\n\n" +
                    "Его показания против графини выглядят как попытка отвести подозрения:\n" +
                    "• Доктор признался в ненависти к графу\n" +
                    "• Имел доступ к ядам как врач\n" +
                    "• Мог использовать горничную для отвлечения внимания\n\n" +
                    $"Уровень подозрительности доктора Львова: {suspectDoctor.SuspicionLevel}",
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
            Scene5 nextScene = new Scene5();
            nextScene.Show();

            // Закрываем текущую форму
            this.Hide();
        }

        // СТАРЫЙ МЕТОД (ОСТАВЛЯЕМ ДЛЯ СОВМЕСТИМОСТИ)
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

        private void label1_Click(object sender, EventArgs e)
        {
            // Для совместимости
        }

        private void Scene4_dc_Load(object sender, EventArgs e)
        {
            // Можно оставить пустым или добавить инициализацию
        }

        private void scene4_dc21_Click(object sender, EventArgs e)
        {
            // Можно оставить пустым или использовать для отладки
        }

        private void scene4_dc32_Click(object sender, EventArgs e)
        {
            // Можно оставить пустым или использовать для отладки
        }

        private void scene4_dc31_Click(object sender, EventArgs e)
        {
            // Можно оставить пустым или использовать для отладки
        }

        private void scene4_dc22_Click(object sender, EventArgs e)
        {
            // Можно оставить пустым или использовать для отладки
        }

        private void scene4_dc41_Click(object sender, EventArgs e)
        {
            // Можно оставить пустым или использовать для отладки
        }
    }
}