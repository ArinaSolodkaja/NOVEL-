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
    public partial class Scene5_fin : Form
    {
        private int clickStage = 0;
        private Timer fadeTimer;
        private float fadeAlpha = 0f;
        private bool isFadingIn = false;
        private bool isFadingOut = false;
        private List<Label> currentFadeLabels = new List<Label>();

        // Подозреваемые
        private Suspect suspectAlina;
        private Suspect suspectDoctor;
        private Suspect suspectCountess;

        // Список всех подозреваемых для удобства
        private List<Suspect> allSuspects;

        public Scene5_fin()
        {
            InitializeComponent();

            // Инициализируем подозреваемых
            InitializeSuspects();

            SetupInitialState();
            SetupTransparency();
            SetupEventHandlers();
        }

        private void InitializeSuspects()
        {
            // Создаем подозреваемых (должны передаваться из предыдущих сцен)
            // Здесь для примера создаем с нуля - в реальном проекте нужно передавать из GameManager
            suspectAlina = new Suspect("Алина Краснова", "Актриса Мариинского театра");
            suspectDoctor = new Suspect("Доктор Львов", "Личный врач графа");
            suspectCountess = new Suspect("Графиня Воронцова", "Жена графа");

            // Добавляем их в общий список
            allSuspects = new List<Suspect> { suspectAlina, suspectDoctor, suspectCountess };

            // Для тестирования - добавляем тестовые улики
            // В реальном проекте улики должны передаваться из предыдущих сцен
            suspectAlina.AddEvidence("Кинжал с гравировкой А.К.", 2);
            suspectDoctor.AddEvidence("Бокал с ядом", 3);
            suspectDoctor.AddEvidence("Пузырек с микстурой", 2);
            suspectCountess.AddEvidence("Завещание графа", 3);
        }

        private void SetupInitialState()
        {
            // Скрываем все тексты и кнопку изначально
            scene5_fin11.Visible = false;
            scene5_fin12.Visible = false;
            scene5_fin21.Visible = false;
            scene5_fin23.Visible = false;
            scene5_fin24.Visible = false;
            scene5_fin_ch_sus.Visible = false;
            scene5_fin_ch_sus.Enabled = false;

            // Устанавливаем начальную прозрачность
            scene5_fin11.ForeColor = Color.FromArgb(0, scene5_fin11.ForeColor);
            scene5_fin12.ForeColor = Color.FromArgb(0, scene5_fin12.ForeColor);
            scene5_fin21.ForeColor = Color.FromArgb(0, scene5_fin21.ForeColor);
            scene5_fin23.ForeColor = Color.FromArgb(0, scene5_fin23.ForeColor);
            scene5_fin24.ForeColor = Color.FromArgb(0, scene5_fin24.ForeColor);
        }

        private void SetupTransparency()
        {
            scene5_lib_fon.Dock = DockStyle.Fill;
            scene5_lib_fon.SizeMode = PictureBoxSizeMode.StretchImage;

            // Устанавливаем Parent для всех меток
            scene5_fin11.Parent = scene5_lib_fon;
            scene5_fin12.Parent = scene5_lib_fon;
            scene5_fin21.Parent = scene5_lib_fon;
            scene5_fin23.Parent = scene5_lib_fon;
            scene5_fin24.Parent = scene5_lib_fon;

            // Настраиваем UseCompatibleTextRendering
            scene5_fin11.UseCompatibleTextRendering = true;
            scene5_fin12.UseCompatibleTextRendering = true;
            scene5_fin21.UseCompatibleTextRendering = true;
            scene5_fin23.UseCompatibleTextRendering = true;
            scene5_fin24.UseCompatibleTextRendering = true;
        }

        private void SetupEventHandlers()
        {
            // Обработчики кликов для всей формы и элементов
            this.Click += Scene5_Click;
            scene5_lib_fon.Click += Scene5_Click;
            scene5_fin11.Click += Scene5_Click;
            scene5_fin12.Click += Scene5_Click;
            scene5_fin21.Click += Scene5_Click;
            scene5_fin23.Click += Scene5_Click;
            scene5_fin24.Click += Scene5_Click;

            // Обработчик для кнопки выбора подозреваемого
            scene5_fin_ch_sus.Click += Scene5_fin_ch_sus_Click;

            // Начальный показ первого текста
            ShowInitialText();
        }

        private void ShowInitialText()
        {
            // Показываем первый текст с анимацией fade-in
            StartFadeIn(new List<Label> { scene5_fin11 });
            clickStage = 1;
        }

        private void Scene5_Click(object sender, EventArgs e)
        {
            if (isFadingIn || isFadingOut) return;

            switch (clickStage)
            {
                case 1: // Первый клик - скрываем scene5_fin11, показываем scene5_fin12
                    StartFadeOut(new List<Label> { scene5_fin11 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene5_fin12 });
                        clickStage = 2;
                    });
                    break;

                case 2: // Второй клик - скрываем scene5_fin12, показываем scene5_fin21
                    StartFadeOut(new List<Label> { scene5_fin12 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene5_fin21 });
                        clickStage = 3;
                    });
                    break;

                case 3: // Третий клик - скрываем scene5_fin21, показываем scene5_fin23
                    StartFadeOut(new List<Label> { scene5_fin21 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene5_fin23 });
                        clickStage = 4;
                    });
                    break;

                case 4: // Четвертый клик - скрываем scene5_fin23, показываем scene5_fin24
                    StartFadeOut(new List<Label> { scene5_fin23 }, () =>
                    {
                        StartFadeIn(new List<Label> { scene5_fin24 });
                        clickStage = 5;
                    });
                    break;

                case 5: // Пятый клик - показываем кнопку выбора подозреваемого
                    StartFadeOut(new List<Label> { scene5_fin24 }, () =>
                    {
                        ShowChoiceButton();
                        clickStage = 6;
                    });
                    break;
            }
        }

        private void ShowChoiceButton()
        {
            // Анимация появления кнопки
            scene5_fin_ch_sus.Visible = true;
            scene5_fin_ch_sus.Enabled = true;

            // Начальная позиция (под экраном)
            int centerX = this.ClientSize.Width / 2 - scene5_fin_ch_sus.Width / 2;
            scene5_fin_ch_sus.Location = new Point(centerX, this.ClientSize.Height + 50);

            // Анимация подъема кнопки
            Timer animationTimer = new Timer();
            animationTimer.Interval = 20;
            int step = 0;
            int totalSteps = 20;

            animationTimer.Tick += (s, args) =>
            {
                step++;

                // Вычисляем новую позицию
                int targetY = this.ClientSize.Height - 200;
                int startY = this.ClientSize.Height + 50;

                int newY = startY - (int)((startY - targetY) * ((float)step / totalSteps));
                scene5_fin_ch_sus.Location = new Point(scene5_fin_ch_sus.Location.X, newY);

                if (step >= totalSteps)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();

                    // Финальная позиция
                    scene5_fin_ch_sus.Location = new Point(centerX, targetY);
                }
            };

            animationTimer.Start();
        }

        private void Scene5_fin_ch_sus_Click(object sender, EventArgs e)
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

                // Показываем диалог выбора подозреваемого
                ShowSuspectChoiceDialog();
            };
            clickTimer.Start();
        }

        private void ShowSuspectChoiceDialog()
        {
            // Создаем форму выбора подозреваемого
            Form choiceForm = new Form();
            choiceForm.Text = "Выберите виновного";
            choiceForm.Size = new Size(600, 500);
            choiceForm.StartPosition = FormStartPosition.CenterParent;
            choiceForm.BackColor = Color.FromArgb(30, 15, 5);
            choiceForm.ForeColor = Color.Gold;
            choiceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            choiceForm.MaximizeBox = false;
            choiceForm.MinimizeBox = false;

            // Панель содержимого
            Panel contentPanel = new Panel();
            contentPanel.BackColor = Color.FromArgb(40, 20, 10);
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Size = new Size(580, 450);
            contentPanel.Location = new Point(10, 10);

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.Goldenrod;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(540, 40);
            titleLabel.Text = "🔍 Кто убил графа Воронцова?";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопки выбора для каждого подозреваемого
            int buttonY = 80;
            int buttonHeight = 70;
            int buttonSpacing = 15;

            List<Button> suspectButtons = new List<Button>();

            foreach (var suspect in allSuspects)
            {
                Button suspectButton = new Button();
                suspectButton.Text = $"{suspect.Name}\n" +
                                    $"Улики: {suspect.EvidenceAgainst.Count}\n" +
                                    $"Уровень подозрения: {suspect.SuspicionLevel}/10";
                suspectButton.Font = new Font("Times New Roman", 11, FontStyle.Bold);
                suspectButton.Size = new Size(300, buttonHeight);
                suspectButton.Location = new Point(140, buttonY);
                suspectButton.BackColor = Color.FromArgb(50, 25, 12);
                suspectButton.ForeColor = Color.Goldenrod;
                suspectButton.FlatStyle = FlatStyle.Flat;
                suspectButton.FlatAppearance.BorderColor = Color.SaddleBrown;
                suspectButton.FlatAppearance.BorderSize = 2;
                suspectButton.Cursor = Cursors.Hand;
                suspectButton.Tag = suspect; // Сохраняем подозреваемого в Tag

                // Эффекты при наведении
                suspectButton.MouseEnter += (s, args) => suspectButton.BackColor = Color.FromArgb(60, 30, 15);
                suspectButton.MouseLeave += (s, args) => suspectButton.BackColor = Color.FromArgb(50, 25, 12);
                suspectButton.MouseDown += (s, args) => suspectButton.BackColor = Color.FromArgb(30, 15, 5);

                // Обработчик выбора
                suspectButton.Click += (s, args) =>
                {
                    Suspect selectedSuspect = suspectButton.Tag as Suspect;
                    CheckSuspectChoice(selectedSuspect);
                    choiceForm.Close();
                };

                suspectButtons.Add(suspectButton);
                contentPanel.Controls.Add(suspectButton);
                buttonY += buttonHeight + buttonSpacing;
            }

            // Кнопка "Подумать еще"
            Button thinkButton = new Button();
            thinkButton.Text = "🤔 Подумать еще";
            thinkButton.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            thinkButton.Size = new Size(200, 40);
            thinkButton.Location = new Point(190, buttonY + 20);
            thinkButton.BackColor = Color.FromArgb(40, 20, 10);
            thinkButton.ForeColor = Color.Gold;
            thinkButton.FlatStyle = FlatStyle.Flat;
            thinkButton.FlatAppearance.BorderColor = Color.SaddleBrown;
            thinkButton.FlatAppearance.BorderSize = 2;
            thinkButton.Cursor = Cursors.Hand;
            thinkButton.Click += (s, args) => choiceForm.Close();

            // Эффекты при наведении
            thinkButton.MouseEnter += (s, args) => thinkButton.BackColor = Color.FromArgb(60, 30, 15);
            thinkButton.MouseLeave += (s, args) => thinkButton.BackColor = Color.FromArgb(40, 20, 10);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(thinkButton);
            choiceForm.Controls.Add(contentPanel);

            choiceForm.ShowDialog();
        }

        private void CheckSuspectChoice(Suspect selectedSuspect)
        {
            // Находим подозреваемого с максимальным уровнем подозрения
            Suspect mostSuspicious = allSuspects.OrderByDescending(s => s.SuspicionLevel).First();

            // Если есть несколько с одинаковым уровнем, выбираем того у кого больше улик
            var topSuspects = allSuspects.Where(s => s.SuspicionLevel == mostSuspicious.SuspicionLevel).ToList();
            if (topSuspects.Count > 1)
            {
                mostSuspicious = topSuspects.OrderByDescending(s => s.EvidenceAgainst.Count).First();
            }

            // Проверяем, правильный ли выбор
            bool isCorrectChoice = (selectedSuspect.Name == mostSuspicious.Name);

            // Показываем результат
            ShowResultMessage(isCorrectChoice, selectedSuspect, mostSuspicious);
        }

        private void ShowResultMessage(bool isCorrect, Suspect selected, Suspect actual)
        {
            string title, message;
            Color panelColor;

            if (isCorrect)
            {
                title = "✅ Верное решение!";
                message = $"Вы правильно определили убийцу!\n\n" +
                         $"**{selected.Name}** действительно виновен(-а) в убийстве графа Воронцова.\n\n" +
                         $"Улики против {selected.Name}:\n" +
                         $"{string.Join("\n", selected.EvidenceAgainst.Select(e => $"• {e}"))}\n\n" +
                         $"Уровень подозрения: {selected.SuspicionLevel}/10";
                panelColor = Color.FromArgb(30, 60, 30);
            }
            else
            {
                title = "❌ Неверное решение";
                message = $"Вы ошиблись в выборе.\n\n" +
                         $"**{selected.Name}** не является убийцей.\n\n" +
                         $"Настоящий убийца - **{actual.Name}**\n" +
                         $"Улики против {actual.Name}:\n" +
                         $"{string.Join("\n", actual.EvidenceAgainst.Select(e => $"• {e}"))}\n\n" +
                         $"Уровень подозрения: {actual.SuspicionLevel}/10";
                panelColor = Color.FromArgb(60, 30, 30);
            }

            // Создаем форму результата
            Form resultForm = new Form();
            resultForm.Text = title;
            resultForm.Size = new Size(550, 450);
            resultForm.StartPosition = FormStartPosition.CenterParent;
            resultForm.BackColor = Color.FromArgb(30, 15, 5);
            resultForm.ForeColor = Color.Gold;
            resultForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            resultForm.MaximizeBox = false;
            resultForm.MinimizeBox = false;

            // Панель содержимого
            Panel contentPanel = new Panel();
            contentPanel.BackColor = panelColor;
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Size = new Size(530, 400);
            contentPanel.Location = new Point(10, 10);

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Font = new Font("Times New Roman", 22, FontStyle.Bold);
            titleLabel.ForeColor = isCorrect ? Color.LightGreen : Color.Salmon;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(490, 50);
            titleLabel.Text = title;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Иконка
            Label iconLabel = new Label();
            iconLabel.Font = new Font("Arial", 36, FontStyle.Bold);
            iconLabel.ForeColor = isCorrect ? Color.LightGreen : Color.Salmon;
            iconLabel.Location = new Point(235, 80);
            iconLabel.Size = new Size(60, 60);
            iconLabel.Text = isCorrect ? "✅" : "❌";
            iconLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Сообщение
            Label messageLabel = new Label();
            messageLabel.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(20, 150);
            messageLabel.Size = new Size(490, 180);
            messageLabel.Text = message;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка OK
            Button okButton = new Button();
            okButton.Text = "Вернуться в главное меню";
            okButton.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            okButton.Size = new Size(250, 40);
            okButton.Location = new Point(140, 340);
            okButton.BackColor = Color.FromArgb(50, 25, 12);
            okButton.ForeColor = Color.Goldenrod;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.FlatAppearance.BorderColor = Color.SaddleBrown;
            okButton.FlatAppearance.BorderSize = 2;
            okButton.Cursor = Cursors.Hand;
            okButton.Click += (s, args) =>
            {
                resultForm.Close();
                NavigateToScene0();
            };

            // Эффекты при наведении на кнопку
            okButton.MouseEnter += (s, args) => okButton.BackColor = Color.FromArgb(60, 30, 15);
            okButton.MouseLeave += (s, args) => okButton.BackColor = Color.FromArgb(50, 25, 12);

            // Добавляем элементы
            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(iconLabel);
            contentPanel.Controls.Add(messageLabel);
            contentPanel.Controls.Add(okButton);

            resultForm.Controls.Add(contentPanel);
            resultForm.ShowDialog();
        }

        private void NavigateToScene0()
        {
            // Создаем и показываем главное меню
            Scene0 mainMenu = new Scene0();
            mainMenu.Show();

            // Закрываем текущую форму
            this.Hide();
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

        private void Scene5_Load(object sender, EventArgs e)
        {
            // Поднимаем все текстовые метки на передний план
            scene5_fin11.BringToFront();
            scene5_fin12.BringToFront();
            scene5_fin21.BringToFront();
            scene5_fin23.BringToFront();
            scene5_fin24.BringToFront();
        }
    }
}