using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Game2048
{
    class Score : Panel
    {
        public readonly static int widthValue = 120;

        public readonly static int heightValue = 70;

        private Label titleLabel;

        private Label valueLabel;

        private int _value;
        public Score(string title, int initialValue = 0)
        {
            BackColor = Color.FromArgb(188, 174, 159);
            Width = widthValue;
            Height = heightValue;

            _value = initialValue;

            titleLabel = new Label()
            {
                Text = title,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(widthValue, heightValue / 2),
                Location = new Point(0, 0),
                ForeColor = Color.FromArgb(100, Color.FromArgb(255, 246, 230)),
                TextAlign = ContentAlignment.MiddleCenter
            };

            valueLabel = new Label()
            {
                Text = $"{_value}",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Size = new Size(widthValue, heightValue / 2),
                Location = new Point(0, heightValue / 2),
                ForeColor = Color.FromArgb(255, 246, 230),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(titleLabel);
            Controls.Add(valueLabel);
        }

        public void SetValue(int value)
        {
            _value = value;
            valueLabel.Text = $"{_value}";
        }
        public void Increase(int value)
        {
            SetValue(_value + value);
        }

       
        public void Reset()
        {
            SetValue(0);
        }

        public int value
        {
            get
            {
                return _value;
            }
        }
    }
}
