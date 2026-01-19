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
    public partial class Scene3 : Form
    {
        private bool buttonsShown = false;

        public Scene3()
        {
            InitializeComponent();

            // Назначаем обработчики кликов
            this.Click += Scene3_Click;
            txt_scene3_vst1.Click += Scene3_Click;
            txt_scene3_vst21.Click += Scene3_Click;
            txt_scene3_vst22.Click += Scene3_Click;
            scene3_fon.Click += Scene3_Click;

            // Настраиваем кнопки
            SetupButtons();
        }

        private void SetupButtons()
        {
            // Скрываем кнопки изначально
            scene3_theatre.Visible = false;
            scene3_doctor.Visible = false;

            // Устанавливаем Anchor для фиксации
            scene3_theatre.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            scene3_doctor.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Устанавливаем позиции
            int buttonWidth = 285;
            int buttonHeight = 68;
            int margin = 20;

            scene3_theatre.Location = new Point(margin, ClientSize.Height - buttonHeight - margin);
            scene3_doctor.Location = new Point(ClientSize.Width - buttonWidth - margin, ClientSize.Height - buttonHeight - margin);

            // Стили кнопок
            scene3_theatre.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            scene3_theatre.ForeColor = Color.Gold;
            scene3_theatre.BackColor = Color.FromArgb(40, 20, 10);
            scene3_theatre.FlatStyle = FlatStyle.Flat;
            scene3_theatre.FlatAppearance.BorderColor = Color.SaddleBrown;
            scene3_theatre.FlatAppearance.BorderSize = 2;
            scene3_theatre.Cursor = Cursors.Hand;

            scene3_doctor.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            scene3_doctor.ForeColor = Color.Gold;
            scene3_doctor.BackColor = Color.FromArgb(40, 20, 10);
            scene3_doctor.FlatStyle = FlatStyle.Flat;
            scene3_doctor.FlatAppearance.BorderColor = Color.SaddleBrown;
            scene3_doctor.FlatAppearance.BorderSize = 2;
            scene3_doctor.Cursor = Cursors.Hand;

            // Обработчики кликов для кнопок
            scene3_theatre.Click += Scene3_Theatre_Click;
            scene3_doctor.Click += Scene3_Doctor_Click;
        }

        private void Scene4_Load(object sender, EventArgs e)
        {
            // Инициализация видимости текста
            txt_scene3_vst1.Visible = true;
            txt_scene3_vst21.Visible = false;
            txt_scene3_vst22.Visible = false;
            scene3_theatre.Visible = false;
            scene3_doctor.Visible = false;

            // Поднимаем текст поверх изображения
            txt_scene3_vst1.BringToFront();
            txt_scene3_vst21.BringToFront();
            txt_scene3_vst22.BringToFront();

            // Настраиваем прозрачность фона лейблов
            SetLabelTransparency();
        }

        private void SetLabelTransparency()
        {
            // ОСНОВНОЙ СПОСОБ КАК В SCENE1: устанавливаем Parent для всех лейблов
            txt_scene3_vst1.Parent = scene3_fon;
            txt_scene3_vst1.BackColor = Color.Transparent;

            txt_scene3_vst21.Parent = scene3_fon;
            txt_scene3_vst21.BackColor = Color.Transparent;

            txt_scene3_vst22.Parent = scene3_fon;
            txt_scene3_vst22.BackColor = Color.Transparent;

            // Также для label1 если нужно
            label1.Parent = scene3_fon;
            label1.BackColor = Color.Transparent;

            // Включаем совместимую отрисовку текста для правильной прозрачности
            txt_scene3_vst1.UseCompatibleTextRendering = true;
            txt_scene3_vst21.UseCompatibleTextRendering = true;
            txt_scene3_vst22.UseCompatibleTextRendering = true;
            label1.UseCompatibleTextRendering = true;

            // Настраиваем PictureBox
            scene3_fon.Dock = DockStyle.Fill;
            scene3_fon.SizeMode = PictureBoxSizeMode.StretchImage;

            // Отправляем PictureBox на задний план
            scene3_fon.SendToBack();

            // Обновляем позиции кнопок
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            int buttonWidth = scene3_theatre.Width;
            int buttonHeight = scene3_theatre.Height;
            int margin = 20;

            // Устанавливаем позиции кнопок внизу формы
            scene3_theatre.Location = new Point(margin, ClientSize.Height - buttonHeight - margin);
            scene3_doctor.Location = new Point(ClientSize.Width - buttonWidth - margin, ClientSize.Height - buttonHeight - margin);
        }

        private void Scene3_Click(object sender, EventArgs e)
        {
            if (txt_scene3_vst1.Visible)
            {
                // Скрываем первый текст
                txt_scene3_vst1.Visible = false;

                // Показываем второй блок текста
                txt_scene3_vst21.Visible = true;
                txt_scene3_vst22.Visible = true;

                // Поднимаем на передний план
                txt_scene3_vst21.BringToFront();
                txt_scene3_vst22.BringToFront();
            }
            else if (txt_scene3_vst21.Visible && !buttonsShown)
            {
                // Показываем кнопки с анимацией
                ShowButtonsWithAnimation();
                buttonsShown = true;
            }
        }

        private void ShowButtonsWithAnimation()
        {
            // Устанавливаем кнопки в начальное положение (под экраном)
            int buttonWidth = scene3_theatre.Width;
            int buttonHeight = scene3_theatre.Height;
            int margin = 20;

            scene3_theatre.Location = new Point(margin, ClientSize.Height + buttonHeight);
            scene3_doctor.Location = new Point(ClientSize.Width - buttonWidth - margin, ClientSize.Height + buttonHeight);

            // Показываем кнопки
            scene3_theatre.Visible = true;
            scene3_doctor.Visible = true;
            scene3_theatre.BringToFront();
            scene3_doctor.BringToFront();

            // Анимация появления снизу
            Timer animationTimer = new Timer();
            animationTimer.Interval = 10;
            int step = 0;
            int totalSteps = 20;

            animationTimer.Tick += (s, args) =>
            {
                step++;

                // Вычисляем новую позицию
                int targetY = ClientSize.Height - buttonHeight - margin;
                int currentY = ClientSize.Height + buttonHeight;
                int newY = currentY - (int)((currentY - targetY) * ((float)step / totalSteps));

                scene3_theatre.Location = new Point(scene3_theatre.Location.X, newY);
                scene3_doctor.Location = new Point(scene3_doctor.Location.X, newY);

                if (step >= totalSteps)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();

                    // Устанавливаем конечные позиции
                    UpdateButtonPositions();
                }
            };

            animationTimer.Start();
        }

        private void Scene3_Theatre_Click(object sender, EventArgs e)
        {
            // Анимация нажатия
            Button button = sender as Button;
            if (button != null)
            {
                Color originalColor = button.BackColor;
                button.BackColor = Color.FromArgb(20, 10, 5);

                Timer clickTimer = new Timer();
                clickTimer.Interval = 150;
                clickTimer.Tick += (s, args) =>
                {
                    button.BackColor = originalColor;
                    clickTimer.Stop();
                    clickTimer.Dispose();

                    // ПЕРЕХОД НА SCENE4_TH (Театр - след «А.К.»)
                    try
                    {
                        // Создаем экземпляр сцены театра
                        Scene4_theatre theatreScene = new Scene4_theatre();
                        theatreScene.Show();

                        // Закрываем текущую форму
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        // Если класс Scene4_Theatre не существует, показываем сообщение
                        MessageBox.Show($"Ошибка при переходе на сцену театра:\n{ex.Message}\n\nУбедитесь, что класс Scene4_Theatre существует в проекте.",
                            "Ошибка перехода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                };
                clickTimer.Start();
            }
        }

        private void Scene3_Doctor_Click(object sender, EventArgs e)
        {
            // Анимация нажатия
            Button button = sender as Button;
            if (button != null)
            {
                Color originalColor = button.BackColor;
                button.BackColor = Color.FromArgb(20, 10, 5);

                Timer clickTimer = new Timer();
                clickTimer.Interval = 150;
                clickTimer.Tick += (s, args) =>
                {
                    button.BackColor = originalColor;
                    clickTimer.Stop();
                    clickTimer.Dispose();

                    // Переход на сцену доктора
                    try
                    {
                        // Создаем экземпляр сцены доктора
                        // SceneDoctor doctorScene = new SceneDoctor();
                        // doctorScene.Show();
                        // this.Hide();

                        // Временное сообщение, пока нет сцены доктора
                        MessageBox.Show("Сцена доктора (след яда) в разработке...");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при переходе на сцену доктора:\n{ex.Message}",
                            "Ошибка перехода",
                            MessageBoxButtons.OK,
                         // изм 7 
                            MessageBoxIcon.Error);
                    }
                };
                clickTimer.Start();
            }
        }

        // Обработчик изменения размера формы
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (buttonsShown)
            {
                UpdateButtonPositions();
            }
        }
    }
}