namespace Tanki5
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tankGame = new Tanki5.TankiGame(795, 520);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tankGame
            // 
            this.tankGame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tankGame.Location = new System.Drawing.Point(-2, -2);
            this.tankGame.MaximumSize = new System.Drawing.Size(795, 520);
            this.tankGame.MinimumSize = new System.Drawing.Size(795, 520);
            this.tankGame.Name = "tankGame";
            this.tankGame.Size = new System.Drawing.Size(795, 520);
            this.tankGame.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 510);
            this.Controls.Add(this.tankGame);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Tanks game";
            this.Text = "Tanks game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.ResumeLayout(false);

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        }

        #endregion

        private TankiGame tankGame;
        private System.Windows.Forms.Timer timer1;
    }
}

