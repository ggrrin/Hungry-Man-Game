using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Hungry_Man;

namespace Hungry_Man_Map_Editor
{
    public partial class hungryManEditor : Form
    {
        private Map map;
        private bool isChange;
        private Point drawZone;

        private Objects[,] brushs;

        public hungryManEditor()
        {
            brushs = new Objects[20, 20];
            InitializeComponent();
        }

        #region "Strip Menu Events" 
       
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile newFile = new NewFile();
            if (newFile.ShowDialog() == DialogResult.OK)
            {
                map = new Map((int)newFile.widthNumericUpDown.Value, (int)newFile.heightNumericUpDown.Value);
                isChange = true;

                SetOpen();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openMapDialog.ShowDialog() == DialogResult.OK)
            {
                IFormatter lFormatter = new BinaryFormatter();
                using(Stream lStream = new FileStream(openMapDialog.FileName, FileMode.Open))
                {
                    map = (Map)lFormatter.Deserialize(lStream);
                    isChange = false;
                }
                SetOpen();                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveOrSaveAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void closeMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isChange)
            {
                DialogResult a = MessageBox.Show("Chcete uložit mapu?", "Hungry Man map editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    SaveOrSaveAs();
                    SetClose();
                }
                else if (a == DialogResult.No)
                    SetClose();                
            }
            else
                SetClose();                      
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hungryManEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (map != null)
            {
                if (isChange)
                {
                    DialogResult a = MessageBox.Show("Chcete uložit mapu?", "Hungry Man map editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Yes)
                    {
                        SaveOrSaveAs();
                    }
                    else if (a == DialogResult.Cancel)
                        e.Cancel = true;
                }                
            }            
        }

        #endregion

        #region "My Function"

        private void UseBars()
        {
            drawZone = new Point(drawBox.Width / 42 + 1, drawBox.Height / 42 + 1);

            if (map.mainArray.GetLength(0) <= drawZone.X - 1)
            {
                drawBoxhScrollBar.Value = 0;
                drawBoxhScrollBar.Enabled = false;
            }

            else
            {
                drawBoxhScrollBar.Value = 0;
                drawBoxhScrollBar.Enabled = true;
                drawBoxhScrollBar.Minimum = 0;
                drawBoxhScrollBar.Maximum = map.mainArray.GetLength(0) - 1;
                drawBoxhScrollBar.LargeChange = drawZone.X - 1;
            }

            if (map.mainArray.GetLength(1) <= drawZone.Y - 1)
            {
                drawBoxvScrollBar.Value = 0;
                drawBoxvScrollBar.Enabled = false;
            }          
            else
            {
                drawBoxvScrollBar.Value = 0;
                drawBoxvScrollBar.Enabled = true;
                drawBoxvScrollBar.Minimum = 0;
                drawBoxvScrollBar.Maximum = map.mainArray.GetLength(1) - 1;
                drawBoxvScrollBar.LargeChange = drawZone.Y - 1;
            }
        }

        private void SaveAs()
        {
            if (saveMapDialog.ShowDialog() == DialogResult.OK)
            {
                map.path = saveMapDialog.FileName;
                using (Stream lStream = new FileStream(saveMapDialog.FileName, FileMode.Create))
                {
                    IFormatter lFormatter = new BinaryFormatter();
                    lFormatter.Serialize(lStream, map);
                    isChange = false;
                }
            }
        }

        private void SaveOrSaveAs()
        {
            if (map.path != null)            
                using (Stream lStream = new FileStream(map.path, FileMode.Create))
                {
                    IFormatter lFormatter = new BinaryFormatter();
                    lFormatter.Serialize(lStream, map);
                    isChange = false;
                }            
            else            
                SaveAs();
            
        }

        private void SetClose()
        {
            map = null;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            closeMapToolStripMenuItem.Enabled = false;
            drawBox.Invalidate();
            toolbox.Enabled = false;
            drawPanel.Enabled = false;
        }

        private void SetOpen()
        {
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            closeMapToolStripMenuItem.Enabled = true;
            toolbox.Enabled = true;
            drawPanel.Enabled = true;
            drawBox.Invalidate();
            UseBars();
        }

        #endregion

        #region "Toolbox Events" 
        private void wallTool_ToolUse(object sender, UseEventArgs e)
        {
            if (e.x + drawBoxhScrollBar.Value < map.mainArray.GetLength(0) && e.y + drawBoxvScrollBar.Value < map.mainArray.GetLength(1))
            {
                if (map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Count == 0)
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Wall);
                drawBox.Invalidate();
            }
        }

        private void eatTool_ToolUse(object sender, UseEventArgs e)
        {
            if (e.x + drawBoxhScrollBar.Value < map.mainArray.GetLength(0) && e.y + drawBoxvScrollBar.Value < map.mainArray.GetLength(1))
            {
                if (map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Count == 0)
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Eat);
                else
                {
                    foreach (Objects o in map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value])
                    {
                        if (o == Objects.Wall || o == Objects.Eat || o == Objects.Player)
                            return;
                        map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Eat);
                        drawBox.Invalidate();
                        return;
                    }
                }
                drawBox.Invalidate();
            }
        }

        private void playerTool_ToolUse(object sender, UseEventArgs e)
        {
            if (e.x + drawBoxhScrollBar.Value < map.mainArray.GetLength(0) && e.y + drawBoxvScrollBar.Value < map.mainArray.GetLength(1))
            {
                if (map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Count == 0)
                {
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Player);
                    playerTool.Enabled = false;
                }
                drawBox.Invalidate();  
            }
        }

        private void monsterTool_ToolUse(object sender, UseEventArgs e)
        {
            if (e.x + drawBoxhScrollBar.Value < map.mainArray.GetLength(0) && e.y + drawBoxvScrollBar.Value < map.mainArray.GetLength(1))
            {
                if (map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Count == 0)
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Monster);
                else
                {
                    foreach (Objects o in map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value])
                    {
                        if (o == Objects.Wall || o == Objects.Player || o == Objects.Monster)
                            return;
                        
                    }
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Add(Objects.Monster);
                    drawBox.Invalidate();
                    return;
                }
                drawBox.Invalidate();  
            }
        }

        private void tool1_ToolUse(object sender, UseEventArgs e)
        {
            if (e.x + drawBoxhScrollBar.Value < map.mainArray.GetLength(0) && e.y + drawBoxvScrollBar.Value < map.mainArray.GetLength(1))
            {
                if (map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Count != 0)
                {
                    foreach (Objects p in map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value])
                    {
                        if (p == Objects.Player)
                            playerTool.Enabled = true;
                    }
                    map.mainArray[e.x + drawBoxhScrollBar.Value, e.y + drawBoxvScrollBar.Value].Clear();
                }
                drawBox.Invalidate();
            }
        }


        #endregion

        #region "Other"

        private void hungryManEditor_SizeChanged(object sender, EventArgs e)
        {            
            UseBars();
        }

        private void hungryManEditor_Load(object sender, EventArgs e)
        {
            SetClose();            
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            if (map != null)
                for (int y = drawBoxvScrollBar.Value; y < ((drawBoxvScrollBar.Value + drawZone.Y < map.mainArray.GetLength(1)) ? drawBoxvScrollBar.Value + drawZone.Y : map.mainArray.GetLength(1)); y++)
                    for (int x = drawBoxhScrollBar.Value; x < ((drawBoxhScrollBar.Value + drawZone.X < map.mainArray.GetLength(0)) ? drawBoxhScrollBar.Value + drawZone.X : map.mainArray.GetLength(0)); x++)
                        foreach (Objects o in map.mainArray[x, y])
                        {
                            if (o == Objects.Eat)
                                e.Graphics.DrawImage(imageListDrawBox.Images[0], (x - drawBoxhScrollBar.Value) * 42, (y - drawBoxvScrollBar.Value) * 42);
                            else if (o == Objects.Player)
                                e.Graphics.DrawImage(imageListDrawBox.Images[1], (x - drawBoxhScrollBar.Value) * 42, (y - drawBoxvScrollBar.Value) * 42);
                            else if (o == Objects.Monster)
                                e.Graphics.DrawImage(imageListDrawBox.Images[2], (x - drawBoxhScrollBar.Value) * 42, (y - drawBoxvScrollBar.Value) * 42);
                            else if (o == Objects.Wall)
                                e.Graphics.DrawImage(imageListDrawBox.Images[3], (x - drawBoxhScrollBar.Value) * 42, (y - drawBoxvScrollBar.Value) * 42);
                        }
        }

        private void drawBoxvScrollBar_ValueChanged(object sender, EventArgs e)
        {
            drawBox.Invalidate();
        }

        private void drawBoxhScrollBar_ValueChanged(object sender, EventArgs e)
        {
            drawBox.Invalidate();
        }
        #endregion

    }
}
