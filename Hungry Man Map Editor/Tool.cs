using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Hungry_Man_Map_Editor
{
    class Tool : Button, IComponent
    { 
        private PictureBox pictureBox;
        private bool isActive;
        private Point firstPoint;
        private Point lastPoint;

        #region "Properties"

        [Category("Tool"), Description("Gets or sets reference on pictureBox."), DisplayName("Picture box")]
        public PictureBox PictureBox
        {
            get { return pictureBox; }
            set
            { 
                pictureBox = value; 
                pictureBox.MouseClick += new MouseEventHandler(pictureBox_MouseClick);
                pictureBox.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
            }
        }

        [Browsable(false), Description("Gets value whether component is active.")]
        public bool IsActive
        {
            get { return isActive; }
        }

        #endregion

        public delegate void ToolUseEventHandler(object sender, UseEventArgs e);

        #region "Události"
        [Category("Tool"),Description("When component is active"), DisplayName("Component active")]
        public event EventHandler Active;

        [Category("Tool"), Description("When component is deactive"), DisplayName("Component deactive")]
        public event EventHandler Deactive;

        [Category("Tool"), Description("When you click on picture box"), DisplayName("Tool used")]
        public event ToolUseEventHandler ToolUse;
        #endregion

        public Tool()
        {
            isActive = false;

            base.Click += new EventHandler(button_Click);
            base.LostFocus += new EventHandler(button_LostFocus);
            base.Size = new Size(32, 32);
            base.Text = string.Empty;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (!isActive)
            {
                isActive = true;
                if (Active != null)
                    Active(this, new EventArgs());
            }
        }

        private void button_LostFocus(object sender, EventArgs e)
        {
            isActive = false;
            if (Deactive != null)
                Deactive(this, new EventArgs());
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (ToolUse != null && isActive)
                ToolUse(this, new UseEventArgs(new Point((int)(e.X / 42), (int)(e.Y / 42))));
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (ToolUse != null && isActive)
            {
                if(e.X > 0 && e.Y > 0)
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        if (firstPoint == null || firstPoint == new Point(-1))
                        {
                            firstPoint = new Point((int)(e.X / 42), (int)(e.Y / 42));
                            lastPoint = firstPoint;
                            ToolUse(this, new UseEventArgs(firstPoint, firstPoint));
                        }
                        else
                        {
                            if (lastPoint != new Point((int)(e.X / 42), (int)(e.Y / 42)))
                                ToolUse(this, new UseEventArgs(new Point((int)(e.X / 42), (int)(e.Y / 42)), firstPoint));
                        }
                    }
                    else if (firstPoint != null)
                        firstPoint = new Point(-1);
            }
        }

    }


   /* Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Using editor As MoodView = New MoodView(DirectCast(value, Mood))
            DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService).DropDownControl(editor)
            Return editor.Mood
        End Using
    End Function

    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        If e.Value Is Nothing Then
            e.Graphics.DrawLine(Pens.Red, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1)
            e.Graphics.DrawLine(Pens.Red, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom - 1)
        Else
            e.Graphics.DrawImage(MoodView.Icon(DirectCast(e.Value, Mood).Index), e.Bounds)
        End If
    End Sub

    Public Overrides ReadOnly Property IsDropDownResizable() As Boolean
        Get
            Return False
        End Get
    End Property*/

}

