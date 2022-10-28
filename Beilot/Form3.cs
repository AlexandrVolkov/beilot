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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            form4 = new Form4();
            
        }
        Form4 form4;
        static public string a = "";
        private void button1_Click(object sender, EventArgs e)
        {
            form4.ShowDialog();
            listBox1.Items.Add(a);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            Form1.name = listBox1.SelectedItem.ToString();
            StreamWriter output = new StreamWriter("log.txt");
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                output.Write(listBox1.Items[i].ToString());
                output.WriteLine();
            }
            output.Write(listBox1.SelectedIndex.ToString());
            output.WriteLine();
            output.Write(Form1.rub.ToString());
            output.WriteLine();
            output.Write(Form1.rub1.ToString());
            output.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            Form1.name = listBox1.SelectedItem.ToString();
            StreamWriter output = new StreamWriter("log.txt");
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                output.Write(listBox1.Items[i].ToString());
                output.WriteLine();
            }
            output.Write(listBox1.SelectedIndex.ToString());
            output.WriteLine();
            output.Write(Form1.rub.ToString());
            output.WriteLine();
            output.Write(Form1.rub1.ToString());
            output.Close();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(listBox1.Items.Count == 0)
            for (int i = 0; i < Form1.li - 3; i++)
            {
                listBox1.Items.Add(Form1.list[i].ToString());
            }
        }
    }
}
