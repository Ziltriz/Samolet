using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samolet
{
    public partial class Form2 : Form
    {
        
        int o1;
        int o2;
        int o3;
        int i = 0;
        public Form2(int _o1, int _o2, int _o3)
        {
            InitializeComponent();
            label1.Text = String.Format("Нажмите + , чтобы начать тест");
            timer1.Interval = 1000;
            o1 = _o1;
            o2 = _o2;
            o3 = _o3;
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            i++;

            label1.Text = String.Format("Тест в процессе " + i);
            if (i == 110)
            {
                timer1.Stop();
                button1.Enabled = false;
                Otk(this, e);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            timer1.Enabled = true;
            timer1.Start();
            button2.Visible = true;
        }

        private void Otk(object sender, EventArgs e)
        {
            switch (o1 + o2 + o3)
            {
                case 6:
                    label1.Text = String.Format("Сработали все отказы");
                    break;
                case 5:
                    label1.Text = String.Format("Сработали отказы номер 2 и 3");
                    break;
                case 4:
                    label1.Text = String.Format("Сработали отказы номер 1 и 3");
                    break;
                case 3:
                    label1.Text = String.Format("Сработал отказ номер 3");
                    break;
                case 2:
                    label1.Text = String.Format("Сработал отказ номер 1");
                    break;
                case 1:
                    label1.Text = String.Format("Сработал отказ номер 1");
                    break;
                case 0:
                    label1.Text = String.Format("Отказов нет");
                    break;
                
                                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button1.Enabled = false;
            Otk(this, e);
        }
    }
}
