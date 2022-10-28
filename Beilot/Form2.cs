using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Beilot
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string[] list1 = new string[20];
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) Form1.rub = 0;
            if (radioButton2.Checked) Form1.rub = 1;
            if (radioButton3.Checked) Form1.rub = 2;
            if (radioButton4.Checked) Form1.rub = 3;
            if (radioButton5.Checked) Form1.rub = 4;
            if (radioButton6.Checked) Form1.rub = 5;
            if (radioButton7.Checked) Form1.rub = 6;
            if (radioButton8.Checked) Form1.rub = 7;
            if (radioButton9.Checked) Form1.rub = 8;
            if (radioButton10.Checked) Form1.rub = 9;
            if (radioButton11.Checked) Form1.rub = 10;
            if (radioButton12.Checked) Form1.rub = 11;
            if (radioButton13.Checked) Form1.rub = 12;
            if (radioButton14.Checked) Form1.rub = 13;
            if (radioButton15.Checked) Form1.rub = 14;
            if (radioButton16.Checked) Form1.rub = 15;
            if (radioButton17.Checked) Form1.rub = 16;
            if (radioButton18.Checked) Form1.rub = 17;
            if (radioButton19.Checked) Form1.rub = 18;
            if (radioButton20.Checked) Form1.rub = 19;
            if (radioButton21.Checked) Form1.rub = 20;
            if (radioButton22.Checked) Form1.rub = 21;
            if (radioButton23.Checked) Form1.rub = 22;
            if (radioButton24.Checked) Form1.rub = 23;
            if (radioButton25.Checked) Form1.rub = 24;
            if (radioButton26.Checked) Form1.rub1 = 0;
            if (radioButton27.Checked) Form1.rub1 = 1;
            if (radioButton28.Checked) Form1.rub1 = 2;
            if (radioButton29.Checked) Form1.rub1 = 3;
            if (radioButton30.Checked) Form1.rub1 = 4;
            if (radioButton31.Checked) Form1.rub1 = 5;
            int li = 0;
            StreamReader input = new StreamReader("log.txt");
            while (true)
            {
                string S = input.ReadLine();
                if (S == null) break;
                list1[li++] = S;
            }
            input.Close(); 
            StreamWriter output = new StreamWriter("log.txt");
            for (int i = 0; i < li-3; i++)
            {
                output.Write(list1[i].ToString());
                output.WriteLine();
            }
            output.Write(list1[li-3].ToString());
            output.WriteLine();
            output.Write(Form1.rub.ToString());
            output.WriteLine();
            output.Write(Form1.rub1.ToString());
            output.Close();
            
            Form1 frmFirst = this.Owner as Form1;
            if (frmFirst != null) frmFirst.Ref();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form1.rub == 0) radioButton1.Checked = true;
            if (Form1.rub == 1) radioButton2.Checked = true;
            if (Form1.rub == 2) radioButton3.Checked = true;
            if (Form1.rub == 3) radioButton4.Checked = true;
            if (Form1.rub == 4) radioButton5.Checked = true;
            if (Form1.rub == 5) radioButton6.Checked = true;
            if (Form1.rub == 6) radioButton7.Checked = true;
            if (Form1.rub == 7) radioButton8.Checked = true;
            if (Form1.rub == 8) radioButton9.Checked = true;
            if (Form1.rub == 9) radioButton10.Checked = true;
            if (Form1.rub == 10) radioButton11.Checked = true;
            if (Form1.rub == 11) radioButton12.Checked = true;
            if (Form1.rub == 12) radioButton13.Checked = true;
            if (Form1.rub == 13) radioButton14.Checked = true;
            if (Form1.rub == 14) radioButton15.Checked = true;
            if (Form1.rub == 15) radioButton16.Checked = true;
            if (Form1.rub == 16) radioButton17.Checked = true;
            if (Form1.rub == 17) radioButton18.Checked = true;
            if (Form1.rub == 18) radioButton19.Checked = true;
            if (Form1.rub == 19) radioButton20.Checked = true;
            if (Form1.rub == 20) radioButton21.Checked = true;
            if (Form1.rub == 21) radioButton22.Checked = true;
            if (Form1.rub == 22) radioButton23.Checked = true;
            if (Form1.rub == 23) radioButton24.Checked = true;
            if (Form1.rub == 24) radioButton25.Checked = true;
            if (Form1.rub1 == 0) radioButton26.Checked = true;
            if (Form1.rub1 == 1) radioButton27.Checked = true;
            if (Form1.rub1 == 2) radioButton28.Checked = true;
            if (Form1.rub1 == 3) radioButton29.Checked = true;
            if (Form1.rub1 == 4) radioButton30.Checked = true;
            if (Form1.rub1 == 5) radioButton31.Checked = true;
           
        }
    }
}
