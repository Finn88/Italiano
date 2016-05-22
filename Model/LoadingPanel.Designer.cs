namespace OurStudents
{
    partial class LoadingPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadingText = new MetroFramework.Controls.MetroLabel();
            this.Spinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // LoadingText
            // 
            this.LoadingText.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.LoadingText.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.LoadingText.Location = new System.Drawing.Point(1, 165);
            this.LoadingText.Name = "LoadingText";
            this.LoadingText.Size = new System.Drawing.Size(470, 25);
            this.LoadingText.TabIndex = 0;
            this.LoadingText.Text = "Пожалуйста подождите...";
            this.LoadingText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Spinner
            // 
            this.Spinner.Location = new System.Drawing.Point(185, 49);
            this.Spinner.Maximum = 100;
            this.Spinner.Minimum = 10;
            this.Spinner.Name = "Spinner";
            this.Spinner.Size = new System.Drawing.Size(65, 55);
            this.Spinner.Speed = 2F;
            this.Spinner.Style = MetroFramework.MetroColorStyle.Green;
            this.Spinner.TabIndex = 1;
            this.Spinner.UseSelectable = true;
            this.Spinner.Value = 20;
            // 
            // LoadingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 266);
            this.ControlBox = false;
            this.Controls.Add(this.Spinner);
            this.Controls.Add(this.LoadingText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "LoadingPanel";
            this.Resizable = false;
            this.ShowIcon = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel LoadingText;
        private MetroFramework.Controls.MetroProgressSpinner Spinner;
    }
}