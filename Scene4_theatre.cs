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

        public Scene4_theatre()
        {
            InitializeComponent();

            SetupInitialState();
            SetupTransparency();

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
        }

        private void SetupInitialState()
        {
            // Скрываем все тексты изначально
            scene4_vs1.Visible = false;
            scene4_vs21.Visible = false;
            scene4_vs22.Visible = false;
            scene4_vs23.Visible = false;
            scene4_vs24.Visible = false;
            scene4_vs31.Visible = false;
            scene4_vs32.Visible = false;
            scene4_vs33.Visible = false;
            scene4_vs34.Visible = false;

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

                case 5: // Шестой клик - можно добавить переход на следующую сцену
                    // Здесь можно добавить переход на следующую сцену
                    break;
            }
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
        // изм 2
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