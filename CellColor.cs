using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Game2048
{
    public struct CellColor
    {
        public Color Foreground;
        public Color Background;
        public CellColor(Color foreground, Color background)
        {
            Foreground = foreground;
            Background = background;
        }

    }
}
