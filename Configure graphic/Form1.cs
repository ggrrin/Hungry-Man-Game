using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Hungry_Man;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Configure_graphic
{
    public partial class Form1 : Form
    {
        private GraphicConnfiguration thisSetting;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.GetFiles("Content\\Configuration").Length == 0)
            {
                thisSetting = new GraphicConnfiguration();
                thisSetting.SaveResolution();
            }
            else
            {
                using (Stream lStream = new FileStream("Content\\Configuration\\GraphicConnfiguration.hmc", FileMode.Open))
                {
                    IFormatter lFormatter = new BinaryFormatter();
                    thisSetting = (GraphicConnfiguration)lFormatter.Deserialize(lStream);
                }
            }

            foreach(Resolution i in thisSetting.Resolution)
                this.comboBox1.Items.Add(i.ToString());

            this.comboBox1.SelectedIndex = (int)thisSetting.SelectResolution;

            this.checkBox1.Checked = thisSetting.IsFullScreen;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisSetting.SetResolution((uint)comboBox1.SelectedIndex);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            thisSetting.IsFullScreen = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thisSetting.SaveResolution();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisSetting.SaveResolution();

            System.Diagnostics.Process.Start("Hungry Man.exe");

            this.Close();
        }
    }
}
