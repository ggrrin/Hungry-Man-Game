using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Hungry_Man.Menu
{
    class Label : IFenceObject
    {
        private string text;
        private bool isMultiLine;
        private Vector2 location;
        private Vector2 size;
        private float textScale;
        private float textSize;
        private bool isCenter;
        private bool visible;
        private float layerDepth;


        //Graphic
        private SpriteFont font;
        private Color color;

        //toUse
        private List<string> rows;
        private Vector2 margin;

        #region "Properties"

        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value + 0.01f; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public string Text
        {
            get {return text;}
            set 
            {
                text = value;
            }
        }

        public float TextSize
        {
            get
            {
                return textSize;
            }
            set
            {
                textSize = value;
                textScale = textSize / font.MeasureString("A").Y;
            }
        }

        public bool IsCenter
        {
            get { return isCenter; }
            set { isCenter = value; }
        }

        public bool IsMultiLine
        {
            get {return isMultiLine;}
            set
            {
                isMultiLine = value;
            }
        }

        public Vector2 Location
        {
            get {return location;}
            set
            {
                location = value;
                location = new Vector2(location.X , location.Y);
            }
        }

        public Vector2 Size
        {
            get {return size;}
            set
            {
                size = value;
                size = new Vector2(size.X , size.Y );
            }
        }

        public SpriteFont Font
        {
            get {return font;}
            set
            {
                font = value;
                textScale = textSize / font.MeasureString("A").Y;
            }
        }

        public Color Color
        {
            get {return color;}
            set {color = value;}
        }

        #endregion
        
        public Label()
        {
            rows = new List<string>();
            font = Configuration.content.Load<SpriteFont>("Fonts\\font");

            TextSize = 10;
            Text = "Label";
            IsMultiLine = false;
            Location = new Vector2(0, 0);
            Size = new Vector2(font.MeasureString(text).X, font.MeasureString(text).Y);
            Color = Color.White;
            IsCenter = false;
            Visible = true;
            layerDepth = 0.62f;
            
        }

        public void Update(GameTime time, Vector2 margin)
        {
            if (visible)
            {
                EditText();
            }
        }

        public void Draw(SpriteBatch sprite, GameTime time, Vector2 margin)
        {
            if (visible)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    sprite.DrawString(font, rows[i].ToString(), (this.margin + margin + location + new Vector2(0, i * textSize * Configuration.menuScale)) * Configuration.menuScale, color, 0.0f, Vector2.Zero, textScale * Configuration.menuScale, SpriteEffects.None, layerDepth);
                }
            }
            
        }

        private void EditText()
        {
            if (text != string.Empty)
            {
                rows.Clear();

                if (textSize > size.Y)
                {
                    return;
                }
                else if (font.MeasureString(text).X * textScale > size.X)
                {
                    if (!isMultiLine)
                    {
                        string subString;
                        int i = 1;
                        do
                        {
                            subString = text.Substring(0, text.Length - i);
                            i++;
                        }
                        while (font.MeasureString(subString).X * textScale > size.X);
                        rows.Add(subString);
                        return;
                    }
                    else
                    {                        
                        int allowRows = (int)(size.Y / textSize);
                        int rowCount = 0;
                        string[] words = text.Split(' ');
                        StringBuilder s = new StringBuilder();
                        foreach (string word in words)
                        {
                            if (font.MeasureString(word).X * textScale > size.X)
                            {
                                string subString;
                                int i = 1;
                                do
                                {
                                    subString = text.Substring(0, text.Length - i);
                                    i++;
                                }
                                while (font.MeasureString(subString).X * textScale > size.X);
                                rows.Add(subString);
                            }
                            else if (font.MeasureString(s.ToString()).X * textScale + font.MeasureString(word).X * textScale < size.X)
                            {
                                s.Append(word);
                                s.Append(' ');
                            }
                            else
                            {
                                rows.Add(s.ToString());
                                s.Clear();
                                s.Append(word);
                                s.Append(' ');
                                rowCount++;
                            }
                            if (rowCount == allowRows)
                                return;
                        }
                        rows.Add(s.ToString());
                        return;
                    }
                }
                rows.Add(text);
                if (isCenter)
                {
                    margin = new Vector2((size.X - font.MeasureString(rows[0]).X * textScale) / 2, (size.Y - font.MeasureString(rows[0]).Y * textScale) / 2);
                }
            }
        }

         
    }
}
