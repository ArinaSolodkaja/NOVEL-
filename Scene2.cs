using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NOVEL_
{
    public partial class Scene2 : Form
    {
        private Form previousForm;
        private List<InteractiveObject> evidenceObjects;
        private Dictionary<string, Suspect> suspects;

        // Ссылки на элементы интерфейса
        private InteractiveObject knifeEvidence;
        private InteractiveObject glassEvidence;
        private InteractiveObject medicineEvidence; // Добавляем пузырек

        public Scene2(Form previousForm = null)
        {
            InitializeComponent();
            this.previousForm = previousForm;

            // Инициализируем систему улик и подозреваемых
            InitializeEvidenceSystem();

            // Настраиваем изображения улик
            SetupEvidenceObjects();

            // Настраиваем стиль кнопок
            SetupButtons();

            // Настраиваем порядок элементов
            pictureBox1.SendToBack();
        }

        private void InitializeEvidenceSystem()
        {
            // Создаем список улик
            evidenceObjects = new List<InteractiveObject>();

            // Создаем словарь подозреваемых
            suspects = new Dictionary<string, Suspect>
            {
                { "Алина Краснова", new Suspect("Алина Краснова", "Актриса Мариинского театра") },
                { "Доктор Львов", new Suspect("Доктор Львов", "Личный врач графа") },
                { "Графиня Воронцова", new Suspect("Графиня Воронцова", "Жена графа") }
            };
        }

        private void SetupEvidenceObjects()
        {
            // Улика 1: Кинжал (против Алины Красовой)
            knifeEvidence = new InteractiveObject(
                "Кинжал с гравировкой А.К.",
                "Старинный кинжал с гравировкой «А.К.»\nЛежал рядом с телом графа\n\nУлика против Алины Красовой",
                scene2_intob_knife.Image,
                "Алина Краснова"
            );

            // Улика 2: Бокал (против Доктора Львова)
            glassEvidence = new InteractiveObject(
                "Бокал с осадком",
                "Старинный бокал со странным осадком на дне\nВозможно, содержит следы яда\n\nУлика против Доктора Львова",
                scene2_intob_bokal.Image,
                "Доктор Львов"
            );

            // Улика 3: Пузырек с микстурой (против Доктора Львова)
            medicineEvidence = new InteractiveObject(
                "Пузырек с микстурой",
                "Микстура доктора Львова\nПохоже, он часто бывал здесь\n\n+ Одна улика против доктора Львова",
                scene2_intob_lek.Image,
                "Доктор Львов"
            );

            // Добавляем в список
            evidenceObjects.Add(knifeEvidence);
            evidenceObjects.Add(glassEvidence);
            evidenceObjects.Add(medicineEvidence);

            // Настраиваем PictureBox для улик
            SetupEvidencePictureBoxes();

            // Привязываем обработчики к кнопкам
            scene2_intob_poisk_knife.Click += (s, e) => ExamineEvidence(knifeEvidence);
            scene2_intob_poisk_bokal.Click += (s, e) => ExamineEvidence(glassEvidence);
            button1.Click += (s, e) => ExamineEvidence(medicineEvidence); // Пузырек
        }

        private void SetupEvidencePictureBoxes()
        {
            // Настройка бокала
            scene2_intob_bokal.Size = new Size(200, 250);
            scene2_intob_bokal.SizeMode = PictureBoxSizeMode.Zoom;
            scene2_intob_bokal.BackColor = Color.Transparent;

            // Настройка кинжала
            scene2_intob_knife.Size = new Size(300, 200);
            scene2_intob_knife.SizeMode = PictureBoxSizeMode.Zoom;
            scene2_intob_knife.BackColor = Color.Transparent;

            // Настройка пузырька (ВАЖНО: SizeMode.Zoom!)
            scene2_intob_lek.Size = new Size(150, 200); // Оптимальный размер
            scene2_intob_lek.SizeMode = PictureBoxSizeMode.Zoom; // Сохраняет пропорции
            scene2_intob_lek.BackColor = Color.Transparent;
        }

        private void ExamineEvidence(InteractiveObject evidence)
        {
            if (evidence.IsCollected)
            {
                MessageBox.Show($"Вы уже изучили {evidence.Name}",
                              "Улика изучена",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            // Анимация нажатия кнопки
            Button clickedButton = GetButtonForEvidence(evidence);
            if (clickedButton == null) return;

            Color originalColor = clickedButton.BackColor;
            clickedButton.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                clickedButton.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // Показываем форму осмотра улики
                ShowEvidenceForm(evidence);
            };
            clickTimer.Start();
        }

        private Button GetButtonForEvidence(InteractiveObject evidence)
        {
            if (evidence == knifeEvidence) return scene2_intob_poisk_knife;
            if (evidence == glassEvidence) return scene2_intob_poisk_bokal;
            if (evidence == medicineEvidence) return button1;
            return null;
        }

        private void ShowEvidenceForm(InteractiveObject evidence)
        {
            // Создаем форму для осмотра улики
            Form evidenceForm = new Form();
            evidenceForm.Text = $"Осмотр: {evidence.Name}";
            evidenceForm.Size = new Size(500, 350);
            evidenceForm.StartPosition = FormStartPosition.CenterScreen;
            evidenceForm.BackColor = Color.FromArgb(30, 15, 5);
            evidenceForm.ForeColor = Color.Gold;
            evidenceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            evidenceForm.MaximizeBox = false;
            evidenceForm.MinimizeBox = false;

            // Панель содержимого
            Panel contentPanel = new Panel();
            contentPanel.BackColor = Color.FromArgb(40, 20, 10);
            contentPanel.BorderStyle = BorderStyle.FixedSingle;
            contentPanel.Size = new Size(480, 300);
            contentPanel.Location = new Point(10, 10);

            // Изображение улики
            PictureBox evidencePicture = new PictureBox();
            evidencePicture.Image = evidence.Image;
            evidencePicture.SizeMode = PictureBoxSizeMode.Zoom;
            evidencePicture.Size = new Size(150, 150);
            evidencePicture.Location = new Point(20, 20);
            evidencePicture.BackColor = Color.Transparent;

            // Описание улики
            Label descriptionLabel = new Label();
            descriptionLabel.Font = new Font("Times New Roman", 11, FontStyle.Regular);
            descriptionLabel.ForeColor = Color.White;
            descriptionLabel.Location = new Point(180, 20);
            descriptionLabel.Size = new Size(280, 120);
            descriptionLabel.Text = evidence.Description;

            // Информация о подозреваемом
            Label suspectLabel = new Label();
            suspectLabel.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            suspectLabel.ForeColor = Color.Goldenrod;
            suspectLabel.Location = new Point(20, 180);
            suspectLabel.Size = new Size(440, 30);
            suspectLabel.Text = $"Улика против: {evidence.SuspectName}";
            suspectLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка "Добавить в улики"
            Button addEvidenceBtn = new Button();
            addEvidenceBtn.Text = "➕ Добавить в улики";
            addEvidenceBtn.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            addEvidenceBtn.Size = new Size(200, 35);
            addEvidenceBtn.Location = new Point(60, 220);
            addEvidenceBtn.BackColor = Color.FromArgb(50, 25, 12);
            addEvidenceBtn.ForeColor = Color.Goldenrod;
            addEvidenceBtn.FlatStyle = FlatStyle.Flat;
            addEvidenceBtn.FlatAppearance.BorderColor = Color.SaddleBrown;
            addEvidenceBtn.FlatAppearance.BorderSize = 2;
            addEvidenceBtn.Cursor = Cursors.Hand;
            addEvidenceBtn.Click += (s, args) =>
            {
                // Добавляем улику подозреваемому
                suspects[evidence.SuspectName].AddEvidence(evidence.Name, evidence.EvidenceValue);

                // Помечаем улику как собранную
                evidence.CollectEvidence();
                evidence.IsCollected = true;

                MessageBox.Show($"Улика «{evidence.Name}» добавлена!\n\n" +
                              $"Подозрительность {evidence.SuspectName}: " +
                              $"{suspects[evidence.SuspectName].SuspicionLevel}",
                              "Улика добавлена",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);

                addEvidenceBtn.Enabled = false;
                addEvidenceBtn.Text = "✅ Добавлено";
                addEvidenceBtn.BackColor = Color.FromArgb(30, 60, 30);

                // Обновляем кнопку в основной форме
                UpdateEvidenceButton(evidence);

                evidenceForm.Close();
            };

            // Кнопка "Закрыть"
            Button closeBtn = new Button();
            closeBtn.Text = "✕ Закрыть";
            closeBtn.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            closeBtn.Size = new Size(200, 35);
            closeBtn.Location = new Point(270, 220);
            closeBtn.BackColor = Color.FromArgb(40, 20, 10);
            closeBtn.ForeColor = Color.Gold;
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderColor = Color.SaddleBrown;
            closeBtn.FlatAppearance.BorderSize = 2;
            closeBtn.Cursor = Cursors.Hand;
            closeBtn.Click += (s, args) => evidenceForm.Close();

            // Добавляем элементы
            contentPanel.Controls.Add(evidencePicture);
            contentPanel.Controls.Add(descriptionLabel);
            contentPanel.Controls.Add(suspectLabel);
            contentPanel.Controls.Add(addEvidenceBtn);
            contentPanel.Controls.Add(closeBtn);

            evidenceForm.Controls.Add(contentPanel);
            evidenceForm.ShowDialog();
        }

        private void UpdateEvidenceButton(InteractiveObject evidence)
        {
            if (evidence == knifeEvidence)
            {
                scene2_intob_poisk_knife.Enabled = false;
                scene2_intob_poisk_knife.Text = "✅ Кинжал изучен";
                scene2_intob_poisk_knife.BackColor = Color.FromArgb(30, 60, 30);
            }
            else if (evidence == glassEvidence)
            {
                scene2_intob_poisk_bokal.Enabled = false;
                scene2_intob_poisk_bokal.Text = "✅ Бокал изучен";
                scene2_intob_poisk_bokal.BackColor = Color.FromArgb(30, 60, 30);
            }
            else if (evidence == medicineEvidence)
            {
                button1.Enabled = false;
                button1.Text = "✅ Пузырек изучен";
                button1.BackColor = Color.FromArgb(30, 60, 30);
            }
        }

        private void SetupButtons()
        {
            // Настройка кнопки "Далее"
            btnNext.FlatAppearance.BorderColor = Color.SaddleBrown;
            btnNext.FlatAppearance.BorderSize = 2;
            btnNext.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            btnNext.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);
            btnNext.Cursor = Cursors.Hand;
            btnNext.Click += btnNext_Click;

            // Настройка кнопки "Осмотреть кинжал"
            scene2_intob_poisk_knife.FlatAppearance.BorderColor = Color.SaddleBrown;
            scene2_intob_poisk_knife.FlatAppearance.BorderSize = 3;
            scene2_intob_poisk_knife.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            scene2_intob_poisk_knife.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);
            scene2_intob_poisk_knife.Cursor = Cursors.Hand;

            // Настройка кнопки "Осмотреть бокал"
            scene2_intob_poisk_bokal.FlatAppearance.BorderColor = Color.SaddleBrown;
            scene2_intob_poisk_bokal.FlatAppearance.BorderSize = 3;
            scene2_intob_poisk_bokal.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            scene2_intob_poisk_bokal.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);
            scene2_intob_poisk_bokal.Cursor = Cursors.Hand;

            // Настройка кнопки "Осмотреть пузырек" (button1)
            button1.FlatAppearance.BorderColor = Color.SaddleBrown;
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);
            button1.Cursor = Cursors.Hand;
        }

        private void Scene2_Load(object sender, EventArgs e)
        {
            SetupFormAppearance();
            PositionEvidenceObjects();
            PositionButtons();
        }

        private void SetupFormAppearance()
        {
            this.Text = "Осмотр улик";

            // Подсказки для кнопок
            toolTip1.SetToolTip(scene2_intob_poisk_knife, "Осмотреть кинжал с гравировкой А.К.");
            toolTip1.SetToolTip(scene2_intob_poisk_bokal, "Осмотреть бокал со странным осадком");
            toolTip1.SetToolTip(button1, "Осмотреть пузырек с микстурой доктора");
            toolTip1.SetToolTip(btnNext, "Перейти к следующей сцене");
        }

        private void PositionEvidenceObjects()
        {
            // Позиционируем кинжал
            scene2_intob_knife.Location = new Point(50, 100);

            // Позиционируем бокал в правом нижнем углу
            int bokalX = this.ClientSize.Width - scene2_intob_bokal.Width - 50;
            int bokalY = this.ClientSize.Height - scene2_intob_bokal.Height - 50;
            scene2_intob_bokal.Location = new Point(bokalX, bokalY);

            // Позиционируем пузырек в левой части (под кинжалом)
            int lekX = 50;
            int lekY = this.ClientSize.Height - scene2_intob_lek.Height - 100;
            scene2_intob_lek.Location = new Point(lekX, lekY);
        }

        private void PositionButtons()
        {
            // Привязываем кнопку "Осмотреть кинжал" под кинжалом
            scene2_intob_poisk_knife.Location = new Point(
                scene2_intob_knife.Location.X,
                scene2_intob_knife.Location.Y + scene2_intob_knife.Height + 20
            );

            // Привязываем кнопку "Осмотреть бокал" под бокалом
            scene2_intob_poisk_bokal.Location = new Point(
                scene2_intob_bokal.Location.X,
                scene2_intob_bokal.Location.Y + scene2_intob_bokal.Height + 20
            );

            // Привязываем кнопку "Осмотреть пузырек" под пузырьком
            button1.Location = new Point(
                scene2_intob_lek.Location.X,
                scene2_intob_lek.Location.Y + scene2_intob_lek.Height + 20
            );

            // Позиционируем кнопку "Далее" в правом верхнем углу
            btnNext.Location = new Point(
                this.ClientSize.Width - btnNext.Width - 20,
                20
            );
        }

        private void Scene2_Resize(object sender, EventArgs e)
        {
            // При изменении размера формы обновляем позиции
            PositionEvidenceObjects();
            PositionButtons();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Анимация нажатия кнопки
            Color originalColor = btnNext.BackColor;
            btnNext.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                btnNext.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // Показываем результаты перед переходом
                ShowEvidenceSummary();

                // ПЕРЕХОД НА SCENE3
                Scene3 nextScene = new Scene3();
                nextScene.Show();
                this.Hide();
            };
            clickTimer.Start();
        }

        private void ShowEvidenceSummary()
        {
            string summary = "🔍 **Собранные улики:**\n\n";

            foreach (var suspect in suspects.Values)
            {
                if (suspect.SuspicionLevel > 0)
                {
                    summary += $"**{suspect.Name}**\n";
                    summary += $"Уровень подозрительности: {suspect.SuspicionLevel}\n";
                    summary += $"Улики: {string.Join(", ", suspect.EvidenceAgainst)}\n\n";
                }
            }

            if (summary == "🔍 **Собранные улики:**\n\n")
            {
                summary += "Вы не собрали ни одной улики.\n\nВнимание: возможно, вы что-то упустили!";
            }

            MessageBox.Show(summary, "Итоги осмотра", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void Scene2_Click(object sender, EventArgs e) { }
        private void scene2_intob_knife_Click(object sender, EventArgs e) { }

        private ToolTip toolTip1 = new ToolTip();
    }
}