namespace Hungry_Man_Map_Editor
{
    partial class NewFile
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
            this.width = new System.Windows.Forms.Label();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.height = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // width
            // 
            this.width.AutoSize = true;
            this.width.Location = new System.Drawing.Point(12, 9);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(37, 13);
            this.width.TabIndex = 0;
            this.width.Text = "Šířka:";
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(107, 7);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.widthNumericUpDown.TabIndex = 1;
            this.widthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.widthNumericUpDown.ThousandsSeparator = true;
            this.widthNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.widthNumericUpDown.ValueChanged += new System.EventHandler(this.widthNumericUpDown_ValueChanged);
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(107, 33);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(90, 20);
            this.heightNumericUpDown.TabIndex = 3;
            this.heightNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.heightNumericUpDown.ThousandsSeparator = true;
            this.heightNumericUpDown.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.heightNumericUpDown.ValueChanged += new System.EventHandler(this.heightNumericUpDown_ValueChanged);
            // 
            // height
            // 
            this.height.AutoSize = true;
            this.height.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.height.Location = new System.Drawing.Point(12, 35);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(39, 13);
            this.height.TabIndex = 2;
            this.height.Text = "Výška:";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(50, 59);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 4;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(131, 59);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // NewFile
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(218, 87);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.heightNumericUpDown);
            this.Controls.Add(this.height);
            this.Controls.Add(this.widthNumericUpDown);
            this.Controls.Add(this.width);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewFile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nový";
            this.Load += new System.EventHandler(this.NewFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label width;
        internal System.Windows.Forms.NumericUpDown widthNumericUpDown;
        internal System.Windows.Forms.NumericUpDown heightNumericUpDown;
        private System.Windows.Forms.Label height;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;

    }
}