using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace Game2048
{
    class Cell : Label
    {
        public readonly static int SizeValue = 110;

        public readonly static int MarginValue = 10;

        public Cell(int x, int y)
        {
            Size = new Size(SizeValue, SizeValue);
            Location = new Point(x, y);
            Font = new Font("Arial", 24, FontStyle.Bold);
            TextAlign = ContentAlignment.MiddleCenter;
        }

        public CellColor Style
        {
            set
            {
                ForeColor = value.Foreground;
                BackColor = value.Background;
            }
        }


    }
}
