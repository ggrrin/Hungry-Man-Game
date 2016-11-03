namespace Hungry_Man_Map_Editor
{
    partial class hungryManEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hungryManEditor));
            this.menuStripHorizontal = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbox = new System.Windows.Forms.Panel();
            this.tool1 = new Hungry_Man_Map_Editor.Tool();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.monsterTool = new Hungry_Man_Map_Editor.Tool();
            this.playerTool = new Hungry_Man_Map_Editor.Tool();
            this.eatTool = new Hungry_Man_Map_Editor.Tool();
            this.wallTool = new Hungry_Man_Map_Editor.Tool();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.drawBoxhScrollBar = new System.Windows.Forms.HScrollBar();
            this.drawBoxvScrollBar = new System.Windows.Forms.VScrollBar();
            this.imageListDrawBox = new System.Windows.Forms.ImageList(this.components);
            this.openMapDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveMapDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStripHorizontal.SuspendLayout();
            this.toolbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.drawPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripHorizontal
            // 
            this.menuStripHorizontal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripHorizontal.Location = new System.Drawing.Point(0, 0);
            this.menuStripHorizontal.Name = "menuStripHorizontal";
            this.menuStripHorizontal.Size = new System.Drawing.Size(784, 24);
            this.menuStripHorizontal.TabIndex = 0;
            this.menuStripHorizontal.Text = "menuStripHorizontal";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeMapToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.fileToolStripMenuItem.Text = "&Soubor";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.newToolStripMenuItem.Text = "&Nový";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.openToolStripMenuItem.Text = "&Otevřít...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.saveToolStripMenuItem.Text = "U&ložit";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.saveAsToolStripMenuItem.Text = "Ul&ožit jako...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
            // 
            // closeMapToolStripMenuItem
            // 
            this.closeMapToolStripMenuItem.Enabled = false;
            this.closeMapToolStripMenuItem.Name = "closeMapToolStripMenuItem";
            this.closeMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeMapToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.closeMapToolStripMenuItem.Text = "&Zavřít mapu";
            this.closeMapToolStripMenuItem.Click += new System.EventHandler(this.closeMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.exitToolStripMenuItem.Text = "&Konec";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolbox
            // 
            this.toolbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.toolbox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolbox.Controls.Add(this.tool1);
            this.toolbox.Controls.Add(this.monsterTool);
            this.toolbox.Controls.Add(this.playerTool);
            this.toolbox.Controls.Add(this.eatTool);
            this.toolbox.Controls.Add(this.wallTool);
            this.toolbox.Location = new System.Drawing.Point(12, 27);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(78, 510);
            this.toolbox.TabIndex = 2;
            // 
            // tool1
            // 
            this.tool1.Image = global::Hungry_Man_Map_Editor.Properties.Resources.rubber;
            this.tool1.Location = new System.Drawing.Point(4, 81);
            this.tool1.Name = "tool1";
            this.tool1.PictureBox = this.drawBox;
            this.tool1.Size = new System.Drawing.Size(32, 32);
            this.tool1.TabIndex = 4;
            this.tool1.UseVisualStyleBackColor = true;
            this.tool1.ToolUse += new Hungry_Man_Map_Editor.Tool.ToolUseEventHandler(this.tool1_ToolUse);
            // 
            // drawBox
            // 
            this.drawBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.drawBox.Location = new System.Drawing.Point(4, 4);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(655, 477);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            // 
            // monsterTool
            // 
            this.monsterTool.Image = global::Hungry_Man_Map_Editor.Properties.Resources.Monster;
            this.monsterTool.Location = new System.Drawing.Point(42, 43);
            this.monsterTool.Name = "monsterTool";
            this.monsterTool.PictureBox = this.drawBox;
            this.monsterTool.Size = new System.Drawing.Size(32, 32);
            this.monsterTool.TabIndex = 3;
            this.monsterTool.UseVisualStyleBackColor = true;
            this.monsterTool.ToolUse += new Hungry_Man_Map_Editor.Tool.ToolUseEventHandler(this.monsterTool_ToolUse);
            // 
            // playerTool
            // 
            this.playerTool.Image = global::Hungry_Man_Map_Editor.Properties.Resources.HungryMan;
            this.playerTool.Location = new System.Drawing.Point(4, 42);
            this.playerTool.Name = "playerTool";
            this.playerTool.PictureBox = this.drawBox;
            this.playerTool.Size = new System.Drawing.Size(32, 32);
            this.playerTool.TabIndex = 2;
            this.playerTool.UseVisualStyleBackColor = true;
            this.playerTool.ToolUse += new Hungry_Man_Map_Editor.Tool.ToolUseEventHandler(this.playerTool_ToolUse);
            // 
            // eatTool
            // 
            this.eatTool.Image = global::Hungry_Man_Map_Editor.Properties.Resources.Eat;
            this.eatTool.Location = new System.Drawing.Point(42, 4);
            this.eatTool.Name = "eatTool";
            this.eatTool.PictureBox = this.drawBox;
            this.eatTool.Size = new System.Drawing.Size(32, 32);
            this.eatTool.TabIndex = 1;
            this.eatTool.UseVisualStyleBackColor = true;
            this.eatTool.ToolUse += new Hungry_Man_Map_Editor.Tool.ToolUseEventHandler(this.eatTool_ToolUse);
            // 
            // wallTool
            // 
            this.wallTool.Image = global::Hungry_Man_Map_Editor.Properties.Resources.Wall;
            this.wallTool.Location = new System.Drawing.Point(4, 4);
            this.wallTool.Name = "wallTool";
            this.wallTool.PictureBox = this.drawBox;
            this.wallTool.Size = new System.Drawing.Size(32, 32);
            this.wallTool.TabIndex = 0;
            this.wallTool.Text = " ";
            this.wallTool.UseVisualStyleBackColor = true;
            this.wallTool.ToolUse += new Hungry_Man_Map_Editor.Tool.ToolUseEventHandler(this.wallTool_ToolUse);
            // 
            // drawPanel
            // 
            this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.drawPanel.Controls.Add(this.drawBox);
            this.drawPanel.Controls.Add(this.drawBoxhScrollBar);
            this.drawPanel.Controls.Add(this.drawBoxvScrollBar);
            this.drawPanel.Location = new System.Drawing.Point(96, 27);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(688, 510);
            this.drawPanel.TabIndex = 3;
            // 
            // drawBoxhScrollBar
            // 
            this.drawBoxhScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBoxhScrollBar.Location = new System.Drawing.Point(3, 484);
            this.drawBoxhScrollBar.Name = "drawBoxhScrollBar";
            this.drawBoxhScrollBar.Size = new System.Drawing.Size(656, 17);
            this.drawBoxhScrollBar.TabIndex = 1;
            this.drawBoxhScrollBar.ValueChanged += new System.EventHandler(this.drawBoxhScrollBar_ValueChanged);
            // 
            // drawBoxvScrollBar
            // 
            this.drawBoxvScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBoxvScrollBar.Location = new System.Drawing.Point(662, 4);
            this.drawBoxvScrollBar.Name = "drawBoxvScrollBar";
            this.drawBoxvScrollBar.Size = new System.Drawing.Size(17, 477);
            this.drawBoxvScrollBar.TabIndex = 1;
            this.drawBoxvScrollBar.ValueChanged += new System.EventHandler(this.drawBoxvScrollBar_ValueChanged);
            // 
            // imageListDrawBox
            // 
            this.imageListDrawBox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDrawBox.ImageStream")));
            this.imageListDrawBox.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDrawBox.Images.SetKeyName(0, "Eat.png");
            this.imageListDrawBox.Images.SetKeyName(1, "HungryMan.png");
            this.imageListDrawBox.Images.SetKeyName(2, "Monster.png");
            this.imageListDrawBox.Images.SetKeyName(3, "Wall.png");
            this.imageListDrawBox.Images.SetKeyName(4, "None.png");
            // 
            // openMapDialog
            // 
            this.openMapDialog.Filter = "Hungry Man Map (*.hmm)|*.hmm";
            this.openMapDialog.Title = "Otevřít...";
            // 
            // saveMapDialog
            // 
            this.saveMapDialog.Filter = "Hungry Man Map (*.hmm)|*.hmm";
            this.saveMapDialog.Title = "Ulož jako...";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // hungryManEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.toolbox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripHorizontal);
            this.MainMenuStrip = this.menuStripHorizontal;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "hungryManEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hungry Man Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.hungryManEditor_FormClosing);
            this.Load += new System.EventHandler(this.hungryManEditor_Load);
            this.SizeChanged += new System.EventHandler(this.hungryManEditor_SizeChanged);
            this.menuStripHorizontal.ResumeLayout(false);
            this.menuStripHorizontal.PerformLayout();
            this.toolbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.drawPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripHorizontal;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Panel toolbox;
        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.ImageList imageListDrawBox;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openMapDialog;
        private System.Windows.Forms.SaveFileDialog saveMapDialog;
        private System.Windows.Forms.HScrollBar drawBoxhScrollBar;
        private System.Windows.Forms.VScrollBar drawBoxvScrollBar;
        private System.Windows.Forms.PictureBox drawBox;
        private Tool wallTool;
        private Tool eatTool;
        private Tool playerTool;
        private Tool monsterTool;
        private System.Windows.Forms.StatusStrip statusStrip;
        private Tool tool1;

    }
}

