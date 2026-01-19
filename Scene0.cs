using System;
using System.Drawing;
using System.Windows.Forms;

namespace NOVEL_
{
    public partial class Scene0 : Form
    {
        public Scene0()
        {
            InitializeComponent();

            // Подписываемся на событие изменения размера формы
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем заголовок
            SetupTitle();

            // Настраиваем кнопку
            SetupStartButton();

            // Отправляем PictureBox на задний план
            pictureBox1.SendToBack();

            // Центрируем элементы ПРИ ЗАГРУЗКЕ (после того как форма развернулась)
            CenterTitle();
            CenterStartButton();
        }

        private void SetupTitle()
        {
            // Делаем PictureBox родителем для прозрачности
            zag_sc1.Parent = pictureBox1;

            // Прозрачный фон
            zag_sc1.BackColor = Color.Transparent;

            // Настройка шрифта
            zag_sc1.Font = new Font("Georgia", 28, FontStyle.Bold);
            zag_sc1.ForeColor = Color.Gold;
            zag_sc1.TextAlign = ContentAlignment.MiddleCenter;
            zag_sc1.AutoSize = true;

            // Центрируем заголовок
            CenterTitle();
        }

        private void CenterTitle()
        {
            if (pictureBox1.Width > 0 && zag_sc1.Width > 0)
            {
                // Используем текущую ширину PictureBox для центрирования
                int x = (pictureBox1.Width - zag_sc1.Width) / 2;
                zag_sc1.Location = new Point(x, 50);
            }
        }

        private void SetupStartButton()
        {
            // Текст кнопки
            btn_start.Text = "НАЧАТЬ РАССЛЕДОВАНИЕ";

            // Настройка шрифта
            btn_start.Font = new Font("Times New Roman", 16, FontStyle.Bold);

            // Цвета
            btn_start.BackColor = Color.FromArgb(40, 20, 10);
            btn_start.ForeColor = Color.Gold;

            // Стиль кнопки
            btn_start.FlatStyle = FlatStyle.Flat;
            btn_start.FlatAppearance.BorderColor = Color.SaddleBrown;
            btn_start.FlatAppearance.BorderSize = 3;

            // Эффекты при наведении
            btn_start.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 15);
            btn_start.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 15, 5);

            // Размер кнопки
            btn_start.Size = new Size(350, 80);

            // Курсор
            btn_start.Cursor = Cursors.Hand;

            // Выравнивание текста
            btn_start.TextAlign = ContentAlignment.MiddleCenter;

            // Обработчик клика
            btn_start.Click += Btn_start_Click;

            // Центрируем кнопку
            CenterStartButton();
        }

        private void CenterStartButton()
        {
            if (this.ClientSize.Width > 0 && btn_start.Width > 0)
            {
                // Используем ТЕКУЩИЙ размер формы для центрирования
                btn_start.Left = (this.ClientSize.Width - btn_start.Width) / 2;
                btn_start.Top = (this.ClientSize.Height - btn_start.Height) / 2 + 50; // Чуть ниже центра
            }
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            // Анимация нажатия
            Color originalColor = btn_start.BackColor;
            btn_start.BackColor = Color.FromArgb(20, 10, 5);

            Timer clickTimer = new Timer();
            clickTimer.Interval = 150;
            clickTimer.Tick += (s, args) =>
            {
                btn_start.BackColor = originalColor;
                clickTimer.Stop();
                clickTimer.Dispose();

                // ПЕРЕХОД НА SCENE1
                Scene1 gameScene = new Scene1();
                gameScene.Show();          // Показываем новую форму
                this.Hide();               // Скрываем текущую форму
            };
            clickTimer.Start();
        }

        private void zag_sc1_Click(object sender, EventArgs e)
        {
            // Анимация клика по заголовку
            Color originalColor = zag_sc1.ForeColor;
            zag_sc1.ForeColor = Color.Silver;

            Timer titleTimer = new Timer();
            titleTimer.Interval = 300;
            titleTimer.Tick += (s, args) =>
            {
                zag_sc1.ForeColor = originalColor;
                titleTimer.Stop();
                titleTimer.Dispose();
            };
            titleTimer.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // При изменении размера формы пересчитываем позиции
            CenterTitle();
            CenterStartButton();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Проверяем, был ли клик в правом верхнем углу для секрета
            Point clickPoint = pictureBox1.PointToClient(Cursor.Position);

            if (clickPoint.X > pictureBox1.Width - 50 && clickPoint.Y < 50)
            {
                MessageBox.Show("💎 Секрет обнаружен!\n\n" +
                              "В особняке 13 комнат, но на плане только 12...",
                              "Секретная подсказка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        // Пустые методы (можно удалить если не используются)
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
    }
}