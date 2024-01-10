using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
   
    public partial class Form1 : Form
    {
        
        private Field field;
        private Score currentScore;
        public Form1()
        {
            this.Load += (sender, e) => Form1_Load(sender, e);
            this.KeyDown += new KeyEventHandler(Form_KeyDown);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int fieldSize = Cell.SizeValue * 4 + Cell.MarginValue * (4 + 1);

            int headerHeight = 70;

            int width = fieldSize + 25* 2;
            int height = fieldSize + 25 * 3 + headerHeight;
            int x = Screen.PrimaryScreen.Bounds.Width / 2 - width / 2;
            int y = Screen.PrimaryScreen.Bounds.Height / 2 - height / 2;

            Name = "2048";
            Text = "2048";
            MaximizeBox = false;
            ClientSize = new Size(width, height);
            Location = new Point(x, y);
            BackColor = Color.FromArgb(251, 249, 239);

            Panel header = new Panel()
            {
                Location = new Point(25, 25),
                Width = width - 25 * 2,
                Height = headerHeight
            };

            currentScore = new Score("Score")
            {
                Location = new Point(header.Width - 90*5-((25*2)-10), 0)
            };
            header.Controls.Add(currentScore);

            field = new Field()
            {
                Location = new Point(25, 25 * 2 + header.Height),
                Size = new Size(fieldSize, fieldSize)
            };
            Controls.Add(header);
            Controls.Add(field);

            for (int i = 0; i < 2; i++)
            {
                field.AddRandomItem();
            }

            field.UpdateUI();
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            bool isMove = false;
            int score=0;
            
            switch (e.KeyCode)
            {
                case Keys.Up:
                    isMove = field.ChangeByDirection(EDirection.UP, out score); ;
                    break;
                case Keys.Right:
                    isMove = field.ChangeByDirection(EDirection.RIGHT,out score);
                    break;
                case Keys.Down:
                    isMove = field.ChangeByDirection(EDirection.DOWN,out score);
                    break;
                case Keys.Left:
                    isMove = field.ChangeByDirection(EDirection.LEFT,out score);
                    break;
            }
            currentScore.Increase(score);
            if (isMove)
            {
                field.AddRandomItem();
            }
            field.UpdateUI();
            if (field.IsGameOver())
            {
                MessageBox.Show("You lose!!!");
                ResetState();
            }
            if (field.IsWin())
            {
                MessageBox.Show("You win!!!");
                ResetState();
            }
        }

        private void ResetState()
        {
            currentScore.Reset();
            field.Reset();
            for (int i = 0; i < 2; i++)
            {
                field.AddRandomItem();
            }
            field.UpdateUI();
        }

       
    }

}

