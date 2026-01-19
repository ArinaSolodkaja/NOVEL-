using System;
using System.Drawing;
using System.Windows.Forms;

namespace NOVEL_
{
    public partial class Scene1 : Form
    {
        private int clickStage = 0; // 0 = начальное, 1 = первый клик, 2 = второй клик, 3 = кнопки
        private PlayerState player;
        private Choice choiceSearchEvidence;
        private Choice choiceComfortCountess;

        public Scene1()
        {
            InitializeComponent();

            // СОЗДАЕМ ИГРОКА
            player = new PlayerState("crime_scene");
            player.ChangeStat("attention", 3);  // Даем характеристики
            player.ChangeStat("logic", 2);

            // СОЗДАЕМ ВЫБОРЫ ДЛЯ КНОПОК
            CreateChoiceObjects();

            // НАСТРАИВАЕМ КНОПКИ
            SetupButtons();

            // СКРЫВАЕМ КНОПКИ ИЗНАЧАЛЬНО
            HideButtons();

            // НАСТРАИВАЕМ СЦЕНУ
            SetupScene1();

            // ОБРАБОТЧИК ИЗМЕНЕНИЯ РАЗМЕРА
            this.Resize += Scene1_Resize;
        }

        private void CreateChoiceObjects()
        {
            // ВЫБОР 1: Искать улики
            choiceSearchEvidence = new Choice(
                "search_evidence",
                "НАЧАТЬ ИСКАТЬ УЛИКИ!",
                "evidence_scene",
                "crime_scene"
            );
            choiceSearchEvidence.AddRequirement("attention", 2);

            // ВЫБОР 2: Успокоить графиню
            choiceComfortCountess = new Choice(
                "comfort_countess",
                "УСПОКОИТЬ ГРАФИНЮ",
                "countess_scene",
                "crime_scene"
            );
            choiceComfortCountess.AddRequirement("logic", 2);
        }

        private void SetupButtons()
        {
            // НАСТРОЙКА СТИЛЯ КАК В SCENE0

            // Шрифт
            btn_scene1_ch1.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            btn_scene1_ch2.Font = new Font("Times New Roman", 16, FontStyle.Bold);

            // Цвета
            btn_scene1_ch1.BackColor = Color.FromArgb(40, 20, 10);
            btn_scene1_ch1.ForeColor = Color.Gold;
            btn_scene1_ch2.BackColor = Color.FromArgb(40, 20, 10);
            btn_scene1_ch2.ForeColor = Color.Gold;

            // Стиль Flat
            btn_scene1_ch1.FlatStyle = FlatStyle.Flat;
            btn_scene1_ch2.FlatStyle = FlatStyle.Flat;

            // Границы
            btn_scene1_ch1.FlatAppearance.BorderColor = Color.SaddleBrown;
            btn_scene1_ch1.FlatAppearance.BorderSize = 3;
            btn_scene1_ch2.FlatAppearance.BorderColor = Color.SaddleBrown;
            btn_scene1_ch2.FlatAppearance.BorderSize = 3;

            // Эффекты при наведении
            btn_scene1_ch1.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            btn_scene1_ch1.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);
            btn_scene1_ch2.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            btn_scene1_ch2.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);

            // Размер
            btn_scene1_ch1.Size = new Size(350, 80);
            btn_scene1_ch2.Size = new Size(350, 80);

            // Курсор
            btn_scene1_ch1.Cursor = Cursors.Hand;
            btn_scene1_ch2.Cursor = Cursors.Hand;

            // Выравнивание текста
            btn_scene1_ch1.TextAlign = ContentAlignment.MiddleCenter;
            btn_scene1_ch2.TextAlign = ContentAlignment.MiddleCenter;

            // ОБРАБОТЧИКИ КЛИКА
            btn_scene1_ch1.Click += BtnEvidence_Click;
            btn_scene1_ch2.Click += BtnCountess_Click;

            // ЦЕНТРИРУЕМ
            CenterButtons();
        }

        private void HideButtons()
        {
            btn_scene1_ch1.Visible = false;
            btn_scene1_ch2.Visible = false;
            btn_scene1_ch1.Enabled = false;
            btn_scene1_ch2.Enabled = false;
        }

        private void ShowChoiceButtons()
        {
            // ПРОВЕРЯЕМ ДОСТУПНОСТЬ ЧЕРЕЗ КЛАСС CHOICE
            bool canSearch = choiceSearchEvidence.IsAvailable(player);
            bool canComfort = choiceComfortCountess.IsAvailable(player);

            // КНОПКА 1: Искать улики
            if (canSearch)
            {
                btn_scene1_ch1.Text = choiceSearchEvidence.Text;
                btn_scene1_ch1.Enabled = true;
                btn_scene1_ch1.BackColor = Color.FromArgb(40, 20, 10);
                btn_scene1_ch1.ForeColor = Color.Gold;
            }
            else
            {
                btn_scene1_ch1.Text = "Недостаточно внимательности!";
                btn_scene1_ch1.Enabled = false;
                btn_scene1_ch1.BackColor = Color.FromArgb(80, 80, 80);
                btn_scene1_ch1.ForeColor = Color.Gray;
            }

            // КНОПКА 2: Успокоить графиню
            if (canComfort)
            {
                btn_scene1_ch2.Text = choiceComfortCountess.Text;
                btn_scene1_ch2.Enabled = true;
                btn_scene1_ch2.BackColor = Color.FromArgb(40, 20, 10);
                btn_scene1_ch2.ForeColor = Color.Gold;
            }
            else
            {
                btn_scene1_ch2.Text = "Недостаточно логики!";
                btn_scene1_ch2.Enabled = false;
                btn_scene1_ch2.BackColor = Color.FromArgb(80, 80, 80);
                btn_scene1_ch2.ForeColor = Color.Gray;
            }

            // ПОКАЗЫВАЕМ КНОПКИ
            btn_scene1_ch1.Visible = true;
            btn_scene1_ch2.Visible = true;

            // ЦЕНТРИРУЕМ
            CenterButtons();
        }

        private void CenterButtons()
        {
            if (!btn_scene1_ch1.Visible && !btn_scene1_ch2.Visible)
                return;

            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            if (btn_scene1_ch1.Visible && btn_scene1_ch2.Visible)
            {
                // Обе кнопки - располагаем вертикально
                btn_scene1_ch1.Location = new Point(
                    centerX - btn_scene1_ch1.Width / 2,
                    centerY - btn_scene1_ch1.Height - 20
                );
                btn_scene1_ch2.Location = new Point(
                    centerX - btn_scene1_ch2.Width / 2,
                    centerY + 20
                );
            }
            else if (btn_scene1_ch1.Visible)
            {
                // Только первая кнопка
                btn_scene1_ch1.Location = new Point(
                    centerX - btn_scene1_ch1.Width / 2,
                    centerY - btn_scene1_ch1.Height / 2
                );
            }
            else if (btn_scene1_ch2.Visible)
            {
                // Только вторая кнопка
                btn_scene1_ch2.Location = new Point(
                    centerX - btn_scene1_ch2.Width / 2,
                    centerY - btn_scene1_ch2.Height / 2
                );
            }
        }

        private void BtnEvidence_Click(object sender, EventArgs e)
        {
            // АНИМАЦИЯ НАЖАТИЯ КАК В SCENE0
            Color originalColor = btn_scene1_ch1.BackColor;
            btn_scene1_ch1.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                btn_scene1_ch1.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // СООБЩЕНИЕ О ВЫБОРЕ
                MessageBox.Show(
                    "Вы начинаете тщательный осмотр библиотеки...\n\n" +
                    "Внимательно изучаете каждый уголок в поисках улик.",
                    choiceSearchEvidence.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // ЗДЕСЬ БУДЕТ ПЕРЕХОД НА СЛЕДУЮЩУЮ СЦЕНУ:
                Scene2 evidenceScene = new Scene2();
                 evidenceScene.Show();
                 this.Hide();
            };
            clickTimer.Start();
        }

        private void BtnCountess_Click(object sender, EventArgs e)
        {
            // АНИМАЦИЯ НАЖАТИЯ КАК В SCENE0
            Color originalColor = btn_scene1_ch2.BackColor;
            btn_scene1_ch2.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                btn_scene1_ch2.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // СООБЩЕНИЕ О ВЫБОРЕ
                MessageBox.Show(
                    "Вы направляетесь к графине...\n\n" +
                    "Её слезы и волнение могут скрывать важные детали.",
                    choiceComfortCountess.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // ЗДЕСЬ БУДЕТ ПЕРЕХОД НА СЛЕДУЮЩУЮ СЦЕНУ:
                Scene3 countessScene = new Scene3();
                 countessScene.Show();
                 this.Hide();
            };
            clickTimer.Start();
        }

        private void SetupScene1()
        {
            // 1. ПРОЗРАЧНЫЙ ФОН ДЛЯ ВСЕХ ЛЕЙБЛОВ
            scene1_gos_vst1.Parent = scene1_gost;
            scene1_gos_vst1.BackColor = Color.Transparent;

            scene1_gos_vst2.Parent = scene1_gost;
            scene1_gos_vst2.BackColor = Color.Transparent;

            txt_scene1_vst11.Parent = scene1_gost;
            txt_scene1_vst11.BackColor = Color.Transparent;

            txt_scene1_vst12.Parent = scene1_gost;
            txt_scene1_vst12.BackColor = Color.Transparent;

            txt_scene1_vst13.Parent = scene1_gost;
            txt_scene1_vst13.BackColor = Color.Transparent;

            txt_scene1_vst21.Parent = scene1_gost;
            txt_scene1_vst21.BackColor = Color.Transparent;

            txt_scene1_vst22.Parent = scene1_gost;
            txt_scene1_vst22.BackColor = Color.Transparent;

            txt_scene1_vst23.Parent = scene1_gost;
            txt_scene1_vst23.BackColor = Color.Transparent;

            txt_scene1_vst24.Parent = scene1_gost;
            txt_scene1_vst24.BackColor = Color.Transparent;

            txt_scene1_vst25.Parent = scene1_gost;
            txt_scene1_vst25.BackColor = Color.Transparent;

            // 2. АВТОРАЗМЕР ДЛЯ ВИДИМОСТИ ВСЕГО ТЕКСТА
            scene1_gos_vst1.AutoSize = true;
            scene1_gos_vst2.AutoSize = true;
            txt_scene1_vst11.AutoSize = true;
            txt_scene1_vst12.AutoSize = true;
            txt_scene1_vst13.AutoSize = true;
            txt_scene1_vst21.AutoSize = true;
            txt_scene1_vst22.AutoSize = true;
            txt_scene1_vst23.AutoSize = true;
            txt_scene1_vst24.AutoSize = true;
            txt_scene1_vst25.AutoSize = true;

            // 3. ОТКЛЮЧИТЬ AutoEllipsis
            scene1_gos_vst1.AutoEllipsis = false;
            scene1_gos_vst2.AutoEllipsis = false;
            txt_scene1_vst11.AutoEllipsis = false;
            txt_scene1_vst12.AutoEllipsis = false;
            txt_scene1_vst13.AutoEllipsis = false;
            txt_scene1_vst21.AutoEllipsis = false;
            txt_scene1_vst22.AutoEllipsis = false;
            txt_scene1_vst23.AutoEllipsis = false;
            txt_scene1_vst24.AutoEllipsis = false;
            txt_scene1_vst25.AutoEllipsis = false;

            // 4. НАСТРОЙКА ПОЗИЦИЙ (Anchor для всех)
            scene1_gos_vst1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            scene1_gos_vst2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst11.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst13.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst21.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst22.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst23.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst24.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txt_scene1_vst25.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // 5. СКРЫТЬ ВСЕ ДОПОЛНИТЕЛЬНЫЕ ТЕКСТЫ ИЗНАЧАЛЬНО
            txt_scene1_vst11.Visible = false;
            txt_scene1_vst12.Visible = false;
            txt_scene1_vst13.Visible = false;
            txt_scene1_vst21.Visible = false;
            txt_scene1_vst22.Visible = false;
            txt_scene1_vst23.Visible = false;
            txt_scene1_vst24.Visible = false;
            txt_scene1_vst25.Visible = false;

            // 6. НАСТРОЙКА PictureBox
            scene1_gost.Dock = DockStyle.Fill;
            scene1_gost.SizeMode = PictureBoxSizeMode.StretchImage;

            // 7. ОТПРАВЛЯЕМ PictureBox НА ЗАДНИЙ ПЛАН
            scene1_gost.SendToBack();

            // 8. ДОБАВЛЯЕМ ОБРАБОТЧИКИ КЛИКОВ ДЛЯ ВСЕХ ЭЛЕМЕНТОВ
            scene1_gost.Click += AnyElement_Click;
            scene1_gos_vst1.Click += AnyElement_Click;
            scene1_gos_vst2.Click += AnyElement_Click;
            txt_scene1_vst11.Click += AnyElement_Click;
            txt_scene1_vst12.Click += AnyElement_Click;
            txt_scene1_vst13.Click += AnyElement_Click;
            txt_scene1_vst21.Click += AnyElement_Click;
            txt_scene1_vst22.Click += AnyElement_Click;
            txt_scene1_vst23.Click += AnyElement_Click;
            txt_scene1_vst24.Click += AnyElement_Click;
            txt_scene1_vst25.Click += AnyElement_Click;
        }

        // ОБРАБОТЧИК ДЛЯ ЛЮБОГО КЛИКА
        private void AnyElement_Click(object sender, EventArgs e)
        {
            if (clickStage == 0)
            {
                // ПЕРВЫЙ КЛИК: показываем первую группу текстов
                scene1_gos_vst1.Visible = false;
                scene1_gos_vst2.Visible = false;
                txt_scene1_vst11.Visible = true;
                txt_scene1_vst12.Visible = true;
                txt_scene1_vst13.Visible = true;

                clickStage = 1;
            }
            else if (clickStage == 1)
            {
                // ВТОРОЙ КЛИК: скрываем первую группу, показываем вторую группу
                txt_scene1_vst11.Visible = false;
                txt_scene1_vst12.Visible = false;
                txt_scene1_vst13.Visible = false;
                txt_scene1_vst21.Visible = true;
                txt_scene1_vst22.Visible = true;
                txt_scene1_vst23.Visible = true;
                txt_scene1_vst24.Visible = true;
                txt_scene1_vst25.Visible = true;

                clickStage = 2;
            }
            else if (clickStage == 2)
            {
                // ТРЕТИЙ КЛИК: скрываем второй блок текста, показываем кнопки
                txt_scene1_vst21.Visible = false;
                txt_scene1_vst22.Visible = false;
                txt_scene1_vst23.Visible = false;
                txt_scene1_vst24.Visible = false;
                txt_scene1_vst25.Visible = false;

                // ПОКАЗЫВАЕМ КНОПКИ С АНИМАЦИЕЙ
                ShowButtonsWithAnimation();

                clickStage = 3;
            }
        }

        private void ShowButtonsWithAnimation()
        {
            // СНАЧАЛА СКРЫВАЕМ
            btn_scene1_ch1.Visible = false;
            btn_scene1_ch2.Visible = false;

            // НАСТРАИВАЕМ ТЕКСТ И ДОСТУПНОСТЬ
            ShowChoiceButtons();

            // АНИМАЦИЯ ПОЯВЛЕНИЯ
            Timer animationTimer = new Timer();
            int step = 0;
            int startY1 = this.ClientSize.Height; // Начинаем снизу
            int startY2 = this.ClientSize.Height;

            // Устанавливаем начальную позицию (под экраном)
            btn_scene1_ch1.Location = new Point(
                btn_scene1_ch1.Location.X,
                startY1
            );
            btn_scene1_ch2.Location = new Point(
                btn_scene1_ch2.Location.X,
                startY2
            );

            // Показываем кнопки (но они еще под экраном)
            btn_scene1_ch1.Visible = true;
            btn_scene1_ch2.Visible = true;

            // Анимация "выезда" снизу
            animationTimer.Interval = 20;
            animationTimer.Tick += (s, args) =>
            {
                step++;

                // Новые позиции (двигаем вверх)
                int newY1 = startY1 - step * 10;
                int newY2 = startY2 - step * 10;

                btn_scene1_ch1.Location = new Point(
                    btn_scene1_ch1.Location.X,
                    newY1
                );
                btn_scene1_ch2.Location = new Point(
                    btn_scene1_ch2.Location.X,
                    newY2
                );

                // Когда достигли конечных позиций - останавливаемся
                if (step >= 15)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();

                    // Устанавливаем окончательные позиции
                    CenterButtons();
                }
            };

            // Запускаем анимацию через небольшую задержку
            Timer delayTimer = new Timer();
            delayTimer.Interval = 300;
            delayTimer.Tick += (s, args) =>
            {
                delayTimer.Stop();
                delayTimer.Dispose();
                animationTimer.Start();
            };
            delayTimer.Start();
        }
        // изм 14 

        // ОБРАБОТЧИК ИЗМЕНЕНИЯ РАЗМЕРА ОКНА
        private void Scene1_Resize(object sender, EventArgs e)
        {
            // ПЕРЕСЧИТЫВАЕМ ПОЗИЦИИ КНОПОК
            if (clickStage == 3) // Если кнопки уже показаны
            {
                CenterButtons();
            }
        }

        // СТАРЫЙ ОБРАБОТЧИК (оставляем для совместимости)
        private void label2_Click(object sender, EventArgs e)
        {
            AnyElement_Click(sender, e);
        }
    }
}