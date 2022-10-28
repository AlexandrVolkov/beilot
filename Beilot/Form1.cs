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
    
    public partial class Form1 : Form
    {

        PictureBox[,] pmass = new PictureBox[4, 6];
        PictureBox[] pcard = new PictureBox[4];
        int i = 0, j, n, s, razd = 0, x, g, temp, kon, kon1, razd1, koz, rez, player, level, sdach, play = -1;
        int y1, y2, y3, y4, y5, y6, z1, z2, z3, z4, z5, z6, k1, k2, k3, k4, k5, k6;
        int yy1, yy2, yy3, yy4, zz4, zz3, zz2, zz1, kk1, kk2, kk3, kk4;
        bool a=true, b=true, hod = false;
        int[] cards = new int[24];
        int[,] imass = new int[4, 6];
        int[] bella = new int[4];
        int[] hits = new int[4];
        int[] bolt = new int[4];
        bool[] xb1 = new bool[4];
        int[] prem = new int[4];
        int[,] terz = new int[4, 4];
        int[] hitsTemp = new int[4];
        int[] count = new int[24];
        int[] card4 = new int[4]; 
        int[] col =  new int[4];
        int[] takenHits = new int[4];
        int[] count1 = new int[4];
        int[] count2 = new int[4];
        int[,] ccard = new int[4, 6];
        Form2 form2;
        Form3 form3;
        bool open = false, canMod = false;
        static public int rub = 0;
        static public int rub1 = 0;
        static public int li = 0;
        static public string name = "Новый игрок";
        static public string[] list = new string[20];
        bool myText = false;
              

        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();
            form3 = new Form3();
        }
        
        public void Ref()
        {
            for (i = 0; i < 4; i++)
                for (j = 0; j < 6; j++)
                {
                    if ((imass[i, j] != 110) && canMod)
                    {
                        if ((open || i == 0) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                        if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8 + rub1 * 24 + imass[i, j]];
                        if (!open && i != 0) pmass[i, j].Image = imageList1.Images[152 + rub];
                    }
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pmass[0, 0] = pictureBox1;
            pmass[0, 1] = pictureBox2;
            pmass[0, 2] = pictureBox3;
            pmass[0, 3] = pictureBox4;
            pmass[0, 4] = pictureBox5;
            pmass[0, 5] = pictureBox6;
            pmass[1, 0] = pictureBox7;
            pmass[1, 1] = pictureBox8;
            pmass[1, 2] = pictureBox9;
            pmass[1, 3] = pictureBox10;
            pmass[1, 4] = pictureBox11;
            pmass[1, 5] = pictureBox12;
            pmass[2, 0] = pictureBox13;
            pmass[2, 1] = pictureBox14;
            pmass[2, 2] = pictureBox15;
            pmass[2, 3] = pictureBox16;
            pmass[2, 4] = pictureBox17;
            pmass[2, 5] = pictureBox18;
            pmass[3, 0] = pictureBox19;
            pmass[3, 1] = pictureBox20;
            pmass[3, 2] = pictureBox21;
            pmass[3, 3] = pictureBox22;
            pmass[3, 4] = pictureBox23;
            pmass[3, 5] = pictureBox24;
            ImageSpades.Image = imageList1.Images[25];
            ImageClubs.Image = imageList1.Images[26];
            ImageDiamonds.Image = imageList1.Images[27];
            ImageHearts.Image = imageList1.Images[28];
            Image1p1.Image = imageList1.Images[29];
            Image1p2.Image = imageList1.Images[29];
            Image2p1.Image = imageList1.Images[29];
            Image2p2.Image = imageList1.Images[29];
            Image3p1.Image = imageList1.Images[29];
            Image3p2.Image = imageList1.Images[29];
            Image4p1.Image = imageList1.Images[29];
            Image4p2.Image = imageList1.Images[29];
            Image1Play.Image = imageList1.Images[30];
            Image2Play.Image = imageList1.Images[30];
            Image3Play.Image = imageList1.Images[30];
            Image4Play.Image = imageList1.Images[30];
            Image1.Visible = false;
            Image2.Visible = false;
            Image3.Visible = false;
            Image4.Visible = false;
            timer1.Enabled = false;
            for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
            level = 0;
            sdach = 0;
            razd = 0;
            for (i = 0; i < 4; i++)
                bella[i] = 0;
            li=0;
           StreamReader input = new StreamReader("log.txt");
           while (true)
            {
                string S = input.ReadLine();
                if (S == null) break;
                list[li++] = S;
            }
            input.Close();
            if (li > 0)
            {
                name = list[Convert.ToInt32(list[li - 3])];
                rub = Convert.ToInt32(list[li - 2]);
                rub1 = Convert.ToInt32(list[li - 1]);
            }
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int max = 0, num = -1;
            for (i = 0; i < 4; i++)
            {
                prem[i] = Check(i);
                Check2(i);
            }
            button1.Visible = false;
            button1.Enabled = false;
            for (i = 0; i < 4; i++)
            {
                if (prem[i] == 200) { hits[i] += 200; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +200"); }
                if (prem[i] == 140) { hits[i] += 140; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +140"); }
                if (prem[i] == 110) { hits[i] += 110; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +110"); }
                if (prem[i] == 100) { hits[i] += 100; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +100"); }
                if (prem[i] == 40) { hits[i] += 40; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +40"); }
                if (prem[i] == 30) { hits[i] += 30; MessageBox.Show("У Игрока " + Convert.ToString(i + 1) + " премия +30"); }
            }
            for (i = 0; i < 4; i++)
                if (terz[i,0] > max)
                {
                    max = terz[i,0];
                    num = i;
                }
            button2.Visible = true;
            button2.Enabled = true;
            if (num == -1) return;
            string t1 = "";
            int match = 0;
            if (num == 0) if ((terz[0, 0] == terz[1, 0]) || (terz[0, 0] == terz[2, 0]) || (terz[0, 0] == terz[3, 0])) match = 1;
            if (num == 1) if ((terz[1, 0] == terz[2, 0]) || (terz[1, 0] == terz[3, 0]) || (terz[0, 0] == terz[1, 0])) match = 1;
            if (num == 2) if ((terz[2, 0] == terz[3, 0]) || (terz[1, 0] == terz[2, 0]) || (terz[0, 0] == terz[2, 0])) match = 1;
            if (num == 3) if ((terz[0, 0] == terz[3, 0]) || (terz[1, 0] == terz[3, 0]) || (terz[2, 0] == terz[3, 0])) match = 1;
            if (terz[num,2] == 0) t1 = " по пикам ";
            if (terz[num,2] == 1) t1 = " по крестям ";
            if (terz[num,2] == 2) t1 = " по бубнам ";
            if (terz[num,2] == 3) t1 = " по червям ";
            if (terz[num,0] == 32) { hits[num] = 1005; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырный бейлот."); }
            if (terz[num, 0] == 31) if (match == 1) MessageBox.Show("У двух игроков 150. Никому не засчитывается.");
                else { hitsTemp[num] = 150; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " 150 " + t1); }
            if (terz[num,0] == 23) { hitsTemp[num] = 100; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырная 100 с Туза"); }
            if (terz[num, 0] == 22) if (match == 1) MessageBox.Show("У двух игроков 100 с Туза. Никому не засчитывается.");
                else { hitsTemp[num] = 100; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " 100 " + t1 + " c Туза"); }
            if (terz[num,0] == 21) { hitsTemp[num] = 100; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырная 100 с Короля"); }
            if (terz[num, 0] == 20) if (match == 1) MessageBox.Show("У двух игроков 100 с Короля. Никому не засчитывается.");
                else { hitsTemp[num] = 100; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " 100 " + t1 + "с Короля"); }
            if (terz[num,0] == 15) { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырная полтина с Туза"); }
            if (terz[num, 0] == 14) if (match == 1) MessageBox.Show("У двух игроков полтина с Туза. Никому не засчитывается.");
                else { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " полтина " + t1 + "с Туза"); }
            if (terz[num,0] == 13) { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырная полтина с Короля"); }
            if (terz[num, 0] == 12) if (match == 1) MessageBox.Show("У двух игроков полтина с Короля. Никому не засчитывается.");
                else { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " полтина " + t1 + "с Короля"); }
            if (terz[num,0] == 11) { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырная полтина с Дамы"); }
            if (terz[num, 0] == 10) if (match == 1) MessageBox.Show("У двух игроков полтина с Дамы. Никому не засчитывается.");
                else { hitsTemp[num] = 50; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " полтина " + t1 + "с Дамы"); }
            if (terz[num,0] == 8) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырный терц " + t1 + "с Туза"); }
            if (terz[num, 0] == 7) if (match == 1) MessageBox.Show("У двух игроков терц с Туза. Никому не засчитывается.");
                else { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " терц " + t1 + "с Туза"); }
            if (terz[num,0] == 6) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырный терц " + t1 + "с Короля"); }
            if (terz[num, 0] == 5) if (match == 1) MessageBox.Show("У двух игроков терц с Короля. Никому не засчитывается.");
                else { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " терц " + t1 + "с Короля"); }
            if (terz[num,0] == 4) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырный терц " + t1 + "с Дамы"); }
            if (terz[num, 0] == 3) if (match == 1) MessageBox.Show("У двух игроков терц с Дамы. Никому не засчитывается.");
                else { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " терц " + t1 + "с Дамы"); }
            if (terz[num,0] == 2) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " козырный терц " + t1 + "с Валета"); }
            if (terz[num, 0] == 1) if (match == 1) MessageBox.Show("У двух игроков терц с Валета. Никому не засчитывается.");
                else { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " терц " + t1 + "с Валета"); }
            if (terz[num,1] == 0) return;
            if (terz[num,3] == 0) t1 = " по пикам ";
            if (terz[num,3] == 1) t1 = " по крестям ";
            if (terz[num,3] == 2) t1 = " по бубнам ";
            if (terz[num,3] == 3) t1 = " по червям ";
            if (terz[num,1] == 7) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " ещё один терц " + t1 + "с Туза"); }
            if (terz[num,1] == 5) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " ещё один терц " + t1 + "с Короля"); }
            if (terz[num,1] == 3) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " ещё один терц " + t1 + "с Дамы"); }
            if (terz[num,1] == 1) { hitsTemp[num] = 20; MessageBox.Show("У игрока " + Convert.ToString(num + 1) + " ещё один терц " + t1 + "с Валета"); }
        }

        public int Check(int t)
        {
            y1 = imass[t,0];
            y2 = imass[t,1];
            y3 = imass[t,2];
            y4 = imass[t,3];
            y5 = imass[t,4];
            y6 = imass[t,5];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            k1 = k2 = k3 = k4 = k5 = k6 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            if ((z1 == 0) && (z2 == 0) && (z3 == 0) && (z4 == 0) ||
                (z1 == 0) && (z5 == 0) && (z3 == 0) && (z4 == 0) ||
                (z1 == 0) && (z6 == 0) && (z5 == 0) && (z4 == 0) ||
                (z1 == 0) && (z2 == 0) && (z5 == 0) && (z4 == 0) ||
                (z1 == 0) && (z2 == 0) && (z5 == 0) && (z6 == 0) ||
                (z1 == 0) && (z2 == 0) && (z3 == 0) && (z5 == 0) ||
                (z1 == 0) && (z2 == 0) && (z3 == 0) && (z6 == 0) ||
                (z1 == 0) && (z2 == 0) && (z6 == 0) && (z4 == 0) ||
                (z1 == 0) && (z6 == 0) && (z3 == 0) && (z4 == 0) ||
                (z1 == 0) && (z5 == 0) && (z3 == 0) && (z6 == 0) ||
                (z5 == 0) && (z2 == 0) && (z3 == 0) && (z4 == 0) ||
                (z6 == 0) && (z2 == 0) && (z3 == 0) && (z4 == 0) ||
                (z6 == 0) && (z2 == 0) && (z5 == 0) && (z4 == 0) ||
                (z6 == 0) && (z2 == 0) && (z3 == 0) && (z5 == 0) ||
                (z5 == 0) && (z6 == 0) && (z3 == 0) && (z4 == 0)) return 110; //4 туза  премия
            if ((z1 == 1) && (z2 == 1) && (z3 == 1) && (z4 == 1) ||
                (z1 == 1) && (z5 == 1) && (z3 == 1) && (z4 == 1) ||
                (z1 == 1) && (z6 == 1) && (z5 == 1) && (z4 == 1) ||
                (z1 == 1) && (z2 == 1) && (z5 == 1) && (z4 == 1) ||
                (z1 == 1) && (z2 == 1) && (z5 == 1) && (z6 == 1) ||
                (z1 == 1) && (z2 == 1) && (z3 == 1) && (z5 == 1) ||
                (z1 == 1) && (z2 == 1) && (z3 == 1) && (z6 == 1) ||
                (z1 == 1) && (z2 == 1) && (z6 == 1) && (z4 == 1) ||
                (z1 == 1) && (z6 == 1) && (z3 == 1) && (z4 == 1) ||
                (z1 == 1) && (z5 == 1) && (z3 == 1) && (z6 == 1) ||
                (z5 == 1) && (z2 == 1) && (z3 == 1) && (z4 == 1) ||
                (z6 == 1) && (z2 == 1) && (z3 == 1) && (z4 == 1) ||
                (z6 == 1) && (z2 == 1) && (z5 == 1) && (z4 == 1) ||
                (z6 == 1) && (z2 == 1) && (z3 == 1) && (z5 == 1) ||
                (z5 == 1) && (z6 == 1) && (z3 == 1) && (z4 == 1)) return 40; //4 короля  премия
            if ((z1 == 2) && (z2 == 2) && (z3 == 2) && (z4 == 2) ||
                (z1 == 2) && (z5 == 2) && (z3 == 2) && (z4 == 2) ||
                (z1 == 2) && (z6 == 2) && (z5 == 2) && (z4 == 2) ||
                (z1 == 2) && (z2 == 2) && (z5 == 2) && (z4 == 2) ||
                (z1 == 2) && (z2 == 2) && (z5 == 2) && (z6 == 2) ||
                (z1 == 2) && (z2 == 2) && (z3 == 2) && (z5 == 2) ||
                (z1 == 2) && (z2 == 2) && (z3 == 2) && (z6 == 2) ||
                (z1 == 2) && (z2 == 2) && (z6 == 2) && (z4 == 2) ||
                (z1 == 2) && (z6 == 2) && (z3 == 2) && (z4 == 2) ||
                (z1 == 2) && (z5 == 2) && (z3 == 2) && (z6 == 2) ||
                (z5 == 2) && (z2 == 2) && (z3 == 2) && (z4 == 2) ||
                (z6 == 2) && (z2 == 2) && (z3 == 2) && (z4 == 2) ||
                (z6 == 2) && (z2 == 2) && (z5 == 2) && (z4 == 2) ||
                (z6 == 2) && (z2 == 2) && (z3 == 2) && (z5 == 2) ||
                (z5 == 2) && (z6 == 2) && (z3 == 2) && (z4 == 2)) return 30; //4 дамы  премия
            if ((z1 == 3) && (z2 == 3) && (z3 == 3) && (z4 == 3) ||
                (z1 == 3) && (z5 == 3) && (z3 == 3) && (z4 == 3) ||
                (z1 == 3) && (z6 == 3) && (z5 == 3) && (z4 == 3) ||
                (z1 == 3) && (z2 == 3) && (z5 == 3) && (z4 == 3) ||
                (z1 == 3) && (z2 == 3) && (z5 == 3) && (z6 == 3) ||
                (z1 == 3) && (z2 == 3) && (z3 == 3) && (z5 == 3) ||
                (z1 == 3) && (z2 == 3) && (z3 == 3) && (z6 == 3) ||
                (z1 == 3) && (z2 == 3) && (z6 == 3) && (z4 == 3) ||
                (z1 == 3) && (z6 == 3) && (z3 == 3) && (z4 == 3) ||
                (z1 == 3) && (z5 == 3) && (z3 == 3) && (z6 == 3) ||
                (z5 == 3) && (z2 == 3) && (z3 == 3) && (z4 == 3) ||
                (z6 == 3) && (z2 == 3) && (z3 == 3) && (z4 == 3) ||
                (z6 == 3) && (z2 == 3) && (z5 == 3) && (z4 == 3) ||
                (z6 == 3) && (z2 == 3) && (z3 == 3) && (z5 == 3) ||
                (z5 == 3) && (z6 == 3) && (z3 == 3) && (z4 == 3)) return 200; //4 вальта  премия
            if ((z1 == 4) && (z2 == 4) && (z3 == 4) && (z4 == 4) ||
                (z1 == 4) && (z5 == 4) && (z3 == 4) && (z4 == 4) ||
                (z1 == 4) && (z6 == 4) && (z5 == 4) && (z4 == 4) ||
                (z1 == 4) && (z2 == 4) && (z5 == 4) && (z4 == 4) ||
                (z1 == 4) && (z2 == 4) && (z5 == 4) && (z6 == 4) ||
                (z1 == 4) && (z2 == 4) && (z3 == 4) && (z5 == 4) ||
                (z1 == 4) && (z2 == 4) && (z3 == 4) && (z6 == 4) ||
                (z1 == 4) && (z2 == 4) && (z6 == 4) && (z4 == 4) ||
                (z1 == 4) && (z6 == 4) && (z3 == 4) && (z4 == 4) ||
                (z1 == 4) && (z5 == 4) && (z3 == 4) && (z6 == 4) ||
                (z5 == 4) && (z2 == 4) && (z3 == 4) && (z4 == 4) ||
                (z6 == 4) && (z2 == 4) && (z3 == 4) && (z4 == 4) ||
                (z6 == 4) && (z2 == 4) && (z5 == 4) && (z4 == 4) ||
                (z6 == 4) && (z2 == 4) && (z3 == 4) && (z5 == 4) ||
                (z5 == 4) && (z6 == 4) && (z3 == 4) && (z4 == 4)) return 100; //4 десятки  премия
            if ((z1 == 5) && (z2 == 5) && (z3 == 5) && (z4 == 5) ||
                (z1 == 5) && (z5 == 5) && (z3 == 5) && (z4 == 5) ||
                (z1 == 5) && (z6 == 5) && (z5 == 5) && (z4 == 5) ||
                (z1 == 5) && (z2 == 5) && (z5 == 5) && (z4 == 5) ||
                (z1 == 5) && (z2 == 5) && (z5 == 5) && (z6 == 5) ||
                (z1 == 5) && (z2 == 5) && (z3 == 5) && (z5 == 5) ||
                (z1 == 5) && (z2 == 5) && (z3 == 5) && (z6 == 5) ||
                (z1 == 5) && (z2 == 5) && (z6 == 5) && (z4 == 5) ||
                (z1 == 5) && (z6 == 5) && (z3 == 5) && (z4 == 5) ||
                (z1 == 5) && (z5 == 5) && (z3 == 5) && (z6 == 5) ||
                (z5 == 5) && (z2 == 5) && (z3 == 5) && (z4 == 5) ||
                (z6 == 5) && (z2 == 5) && (z3 == 5) && (z4 == 5) ||
                (z6 == 5) && (z2 == 5) && (z5 == 5) && (z4 == 5) ||
                (z6 == 5) && (z2 == 5) && (z3 == 5) && (z5 == 5) ||
                (z5 == 5) && (z6 == 5) && (z3 == 5) && (z4 == 5)) return 140; //4 девятки  премия
            else return 10;
        }

        public void Check2(int t)
        {
            int temp = 0, temp1 = 0;
            y1 = imass[t,0];
            y2 = imass[t,1];
            y3 = imass[t,2];
            y4 = imass[t,3];
            y5 = imass[t,4];
            y6 = imass[t,5];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            k1 = k2 = k3 = k4 = k5 = k6 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            if ((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (y3 - 2 == y4 - 3) && (y4 - 3 == y5 - 4) && (y5 - 4 == y6 - 5) &&
                (k1 == k2) && (k2 == k3) && (k3 == k4) && (k4 == k5) && (k5 == k6))
                if (k1 == koz) { terz[t,0] = 32; terz[t,2] = k1; return; } // козырный бейлот
                else { terz[t,0] = 31; terz[t,2] = k1; return; } //150
            if ((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (y3 - 2 == y4 - 3) && (y4 - 3 == y5 - 4) &&
                (k1 == k2) && (k2 == k3) && (k3 == k4) && (k4 == k5))
            {
                if ((k3 == koz) && (z1 == 0)) { terz[t,0] = 23; terz[t,2] = k3; return; } //100 T koz
                if ((k3 != koz) && (z1 == 0)) { terz[t,0] = 22; terz[t,2] = k3; return; } //100 T
                if ((k3 == koz) && (z1 == 1)) { terz[t,0] = 21; terz[t,2] = k3; return; } //100 K koz
                if ((k3 != koz) && (z1 == 1)) { terz[t,0] = 20; terz[t,2] = k3; return; } //100 K
            }
            if ((y2 == y3 - 1) && (y3 - 1 == y4 - 2) && (y4 - 2 == y5 - 3) && (y5 - 3 == y6 - 4) &&
                (k6 == k2) && (k2 == k3) && (k3 == k4) && (k4 == k5))
            {
                if ((k3 == koz) && (z2 == 0)) { terz[t,0] = 23; terz[t,2] = k3; return; } //100 T koz
                if ((k3 != koz) && (z2 == 0)) { terz[t,0] = 22; terz[t,2] = k3; return; } //100 T
                if ((k3 == koz) && (z2 == 1)) { terz[t,0] = 21; terz[t,2] = k3; return; } //100 K koz
                if ((k3 != koz) && (z2 == 1)) { terz[t,0] = 20; terz[t,2] = k3; return; } //100 K
            }
            if ((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (y3 - 2 == y4 - 3) && (k1 == k2) && (k2 == k3) && (k3 == k4))
            {
                if ((k3 == koz) && (z1 == 0)) { terz[t,0] = 15; terz[t,2] = k3; return; } //50 T koz
                if ((k3 != koz) && (z1 == 0)) { terz[t,0] = 14; terz[t,2] = k3; return; } //50 T
                if ((k3 == koz) && (z1 == 1)) { terz[t,0] = 13; terz[t,2] = k3; return; } //50 K koz
                if ((k3 != koz) && (z1 == 1)) { terz[t,0] = 12; terz[t,2] = k3; return; } //50 K
                if ((k3 == koz) && (z1 == 2)) { terz[t,0] = 11; terz[t,2] = k3; return; } //50 Д koz
                if ((k3 != koz) && (z1 == 2)) { terz[t,0] = 10; terz[t,2] = k3; return; } //50 Д
            }
            if ((y2 == y3 - 1) && (y3 - 1 == y4 - 2) && (y4 - 2 == y5 - 3) && (k5 == k2) && (k2 == k3) && (k3 == k4))
            {
                if ((k3 == koz) && (z2 == 0)) { terz[t,0] = 15; terz[t,2] = k3; return; } //50 T koz
                if ((k3 != koz) && (z2 == 0)) { terz[t,0] = 14; terz[t,2] = k3; return; } //50 T
                if ((k3 == koz) && (z2 == 1)) { terz[t,0] = 13; terz[t,2] = k3; return; } //50 K koz
                if ((k3 != koz) && (z2 == 1)) { terz[t,0] = 12; terz[t,2] = k3; return; } //50 K
                if ((k3 == koz) && (z2 == 2)) { terz[t,0] = 11; terz[t,2] = k3; return; } //50 Д koz
                if ((k3 != koz) && (z2 == 2)) { terz[t,0] = 10; terz[t,2] = k3; return; } //50 Д
            }
            if ((y3 == y4 - 1) && (y4 - 1 == y5 - 2) && (y5 - 2 == y6 - 3) && (k5 == k6) && (k4 == k5) && (k3 == k4))
            {
                if ((k3 == koz) && (z3 == 0)) { terz[t,0] = 15; terz[t,2] = k3; return; } //50 T koz
                if ((k3 != koz) && (z3 == 0)) { terz[t,0] = 14; terz[t,2] = k3; return; } //50 T
                if ((k3 == koz) && (z3 == 1)) { terz[t,0] = 13; terz[t,2] = k3; return; } //50 K koz
                if ((k3 != koz) && (z3 == 1)) { terz[t,0] = 12; terz[t,2] = k3; return; } //50 K
                if ((k3 == koz) && (z3 == 2)) { terz[t,0] = 11; terz[t,2] = k3; return; } //50 Д koz
                if ((k3 != koz) && (z3 == 2)) { terz[t,0] = 10; terz[t,2] = k3; return; } //50 Д
            }
            if ((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (k1 == k2) && (k2 == k3))
            {
                if ((k3 == koz) && (z1 == 0)) if (8 > temp) { terz[i,0] = 8; terz[t,2] = k3; temp = 8; } //20 T koz
                if ((k3 != koz) && (z1 == 0)) if (7 > temp) { terz[i,0] = 7; terz[t,2] = k3; temp = 7; } //20 T
                if ((k3 == koz) && (z1 == 1)) if (6 > temp) { terz[i,0] = 6; terz[t,2] = k3; temp = 6; } //20 K koz
                if ((k3 != koz) && (z1 == 1)) if (5 > temp) { terz[i,0] = 5; terz[t,2] = k3; temp = 5; } //20 K
                if ((k3 == koz) && (z1 == 2)) if (4 > temp) { terz[i,0] = 4; terz[t,2] = k3; temp = 4; } //20 Д koz
                if ((k3 != koz) && (z1 == 2)) if (3 > temp) { terz[i,0] = 3; terz[t,2] = k3; temp = 3; } //20 Д
                if ((k3 == koz) && (z1 == 3)) if (2 > temp) { terz[i,0] = 2; terz[t,2] = k3; temp = 2; } //20 B koz
                if ((k3 != koz) && (z1 == 4)) if (1 > temp) { terz[i,0] = 1; terz[t,2] = k3; temp = 1; } //20 B
                if ((y4 == y5 - 1) && (y5 - 1 == y6 - 2) && (k4 == k5) && (k6 == k5))
                {
                    if ((k4 == koz) && (z4 == 0)) if (8 > temp1) { terz[i,1] = 8; terz[t,3] = k4; temp1 = 8; } //20 T koz
                    if ((k4 != koz) && (z4 == 0)) if (7 > temp1) { terz[i,1] = 7; terz[t,3] = k4; temp1 = 7; } //20 T
                    if ((k4 == koz) && (z4 == 1)) if (6 > temp1) { terz[i,1] = 6; terz[t,3] = k4; temp1 = 6; } //20 K koz
                    if ((k4 != koz) && (z4 == 1)) if (5 > temp1) { terz[i,1] = 5; terz[t,3] = k4; temp1 = 5; } //20 K
                    if ((k4 == koz) && (z4 == 2)) if (4 > temp1) { terz[i,1] = 4; terz[t,3] = k4; temp1 = 4; } //20 Д koz
                    if ((k4 != koz) && (z4 == 2)) if (3 > temp1) { terz[i,1] = 3; terz[t,3] = k4; temp1 = 3; } //20 Д
                    if ((k4 == koz) && (z4 == 3)) if (2 > temp1) { terz[i,1] = 2; terz[t,3] = k4; temp1 = 2; } //20 B koz
                    if ((k4 != koz) && (z4 == 3)) if (1 > temp1) { terz[i,1] = 1; terz[t,3] = k4; temp1 = 1; } //20 B
                }
            }
            if ((y2 == y3 - 1) && (y3 - 1 == y4 - 2) && (k4 == k2) && (k2 == k3))
            {
                if ((k3 == koz) && (z2 == 0)) if (8 > temp) { terz[i,0] = 8; terz[t,2] = k3; temp = 8; } //20 T koz
                if ((k3 != koz) && (z2 == 0)) if (7 > temp) { terz[i,0] = 7; terz[t,2] = k3; temp = 7; } //20 T
                if ((k3 == koz) && (z2 == 1)) if (6 > temp) { terz[i,0] = 6; terz[t,2] = k3; temp = 6; } //20 K koz
                if ((k3 != koz) && (z2 == 1)) if (5 > temp) { terz[i,0] = 5; terz[t,2] = k3; temp = 5; } //20 K
                if ((k3 == koz) && (z2 == 2)) if (4 > temp) { terz[i,0] = 4; terz[t,2] = k3; temp = 4; } //20 Д koz
                if ((k3 != koz) && (z2 == 2)) if (3 > temp) { terz[i,0] = 3; terz[t,2] = k3; temp = 3; } //20 Д
                if ((k3 == koz) && (z2 == 3)) if (2 > temp) { terz[i,0] = 2; terz[t,2] = k3; temp = 2; } //20 B koz
                if ((k3 != koz) && (z2 == 3)) if (1 > temp) { terz[i,0] = 1; terz[t,2] = k3; temp = 1; } //20 B
            }
            if ((y3 == y4 - 1) && (y4 - 1 == y5 - 2) && (k4 == k3) && (k4 == k5))
            {
                if ((k3 == koz) && (z3 == 0)) if (8 > temp) { terz[i,0] = 8; terz[t,2] = k3; temp = 8; } //20 T koz
                if ((k3 != koz) && (z3 == 0)) if (7 > temp) { terz[i,0] = 7; terz[t,2] = k3; temp = 7; } //20 T
                if ((k3 == koz) && (z3 == 1)) if (6 > temp) { terz[i,0] = 6; terz[t,2] = k3; temp = 6; } //20 K koz
                if ((k3 != koz) && (z3 == 1)) if (5 > temp) { terz[i,0] = 5; terz[t,2] = k3; temp = 5; } //20 K
                if ((k3 == koz) && (z3 == 2)) if (4 > temp) { terz[i,0] = 4; terz[t,2] = k3; temp = 4; } //20 Д koz
                if ((k3 != koz) && (z3 == 2)) if (3 > temp) { terz[i,0] = 3; terz[t,2] = k3; temp = 3; } //20 Д
                if ((k3 == koz) && (z3 == 3)) if (2 > temp) { terz[i,0] = 2; terz[t,2] = k3; temp = 2; } //20 B koz
                if ((k3 != koz) && (z3 == 3)) if (1 > temp) { terz[i,0] = 1; terz[t,2] = k3; temp = 1; } //20 B
            }
            if ((y4 == y5 - 1) && (y5 - 1 == y6 - 2) && (k4 == k5) && (k6 == k5))
            {
                if ((k4 == koz) && (z4 == 0)) if (8 >= temp) { terz[i,0] = 8; terz[t,2] = k4; temp = 8; } //20 T koz
                if ((k4 != koz) && (z4 == 0)) if (7 >= temp) { terz[i,0] = 7; terz[t,2] = k4; temp = 7; } //20 T
                if ((k4 == koz) && (z4 == 1)) if (6 >= temp) { terz[i,0] = 6; terz[t,2] = k4; temp = 6; } //20 K koz
                if ((k4 != koz) && (z4 == 1)) if (5 >= temp) { terz[i,0] = 5; terz[t,2] = k4; temp = 5; } //20 K
                if ((k4 == koz) && (z4 == 2)) if (4 >= temp) { terz[i,0] = 4; terz[t,2] = k4; temp = 4; } //20 Д koz
                if ((k4 != koz) && (z4 == 2)) if (3 >= temp) { terz[i,0] = 3; terz[t,2] = k4; temp = 3; } //20 Д
                if ((k4 == koz) && (z4 == 3)) if (2 >= temp) { terz[i,0] = 2; terz[t,2] = k4; temp = 2; } //20 B koz
                if ((k4 != koz) && (z4 == 3)) if (1 >= temp) { terz[i,0] = 1; terz[t,2] = k4; } //20 B
                if ((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (k1 == k2) && (k2 == k3))
                {
                    if ((k1 == koz) && (z1 == 0)) if ((8 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 8; terz[t,3] = k1; temp1 = 8; } //20 T koz
                    if ((k1 != koz) && (z1 == 0)) if ((7 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 7; terz[t,3] = k1; temp1 = 7; } //20 T
                    if ((k1 == koz) && (z1 == 1)) if ((6 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 6; terz[t,3] = k1; temp1 = 6; } //20 K koz
                    if ((k1 != koz) && (z1 == 1)) if ((5 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 5; terz[t,3] = k1; temp1 = 5; } //20 K
                    if ((k1 == koz) && (z1 == 2)) if ((4 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 4; terz[t,3] = k1; temp1 = 4; } //20 Д koz
                    if ((k1 != koz) && (z1 == 2)) if ((3 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 3; terz[t,3] = k1; temp1 = 3; } //20 Д
                    if ((k1 == koz) && (z1 == 3)) if ((2 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 2; terz[t,3] = k1; temp1 = 2; } //20 B koz
                    if ((k1 != koz) && (z1 == 3)) if ((1 > temp1) && (terz[i,0] >= terz[i,1])) { terz[i,1] = 1; terz[t,3] = k1; } //20 B
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void начатьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
            level = 0;
            sdach = 0;
            razd = 0;
            for (i = 0; i < 4; i++)
                bella[i] = 0;
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            StartButton.Visible = true;
        }

        private void AskComp()
        {
            if (a == false) return;/*first ask*/
            if(razd1==0)
                if (MessageBox.Show("Вы будете играть?", "1-й круг", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    a = false;
                    Image1Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    End();
                    return;
                }
                else
                {
                    Image1p1.Visible = true;
                    return;
                }
            if(razd1==1)
                if (Ask(1) == 0)
                {
                    a = false;
                    Image2Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    End();
                    return;
                }
                else
                {
                    Image2p1.Visible = true;
                    return;
                }
            if (razd1 == 2)
                if (Ask(2) == 0)
                {
                    a = false;
                    Image3Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    End();
                    return;
                }
                else
                {
                    Image3p1.Visible = true;
                    return;
                }
            if (razd1 == 3)
                if (Ask(3) == 0)
                {
                    a = false;
                    Image4Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    End();
                    return;
                }
                else
                {
                    Image4p1.Visible = true;
                    return;
                }

        }

        private int Ask(int t)
        {
            y1 = imass[t,0];
            y2 = imass[t,1];
            y3 = imass[t,2];
            y4 = imass[t,3];
            y5 = cards[16];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            k1 = k2 = k3 = k4 = k5 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            if ((k1 == koz) && (k2 == koz) && (k3 == koz) && (k4 == koz)) return 0; // 4 козыря
            if ((k1 == koz) && (k2 == koz) && (k3 == koz) ||
                (k4 == koz) && (k2 == koz) && (k3 == koz)) return 0; //3 козыря
            if (((z1 == 3) && (k1 == koz) && (z2 == 5) && (k2 == koz)) ||
                ((z2 == 3) && (k2 == koz) && (z3 == 5) && (k3 == koz)) ||
                ((z3 == 3) && (k3 == koz) && (z4 == 5) && (k4 == koz))) return 0; //В коз и 9 коз
            if (((z1 == 3) && (k1 == koz) && (z2 == 4) && (k2 == koz)) ||
                ((z2 == 3) && (k2 == koz) && (z3 == 4) && (k3 == koz)) ||
                ((z3 == 3) && (k3 == koz) && (z4 == 4) && (k4 == koz))) return 0; //В коз и 10 коз
            if (((z2 == 3) && (k1 == koz) && (z1 == 1) && (k2 == koz)) ||
                ((z3 == 3) && (k2 == koz) && (z2 == 1) && (k3 == koz)) ||
                ((z4 == 3) && (k4 == koz) && (z3 == 1) && (k3 == koz))) return 0; //В коз и К коз
            if (((z2 == 3) && (k1 == koz) && (z1 == 2) && (k2 == koz)) ||
                ((z3 == 3) && (k2 == koz) && (z2 == 2) && (k3 == koz)) ||
                ((z4 == 3) && (k4 == koz) && (z3 == 2) && (k3 == koz))) return 0; //В коз и Д коз
            if (((z2 == 2) && (k1 == koz) && (z1 == 1) && (k2 == koz)) ||
                ((z3 == 2) && (k2 == koz) && (z2 == 1) && (k3 == koz)) ||
                ((z4 == 2) && (k4 == koz) && (z3 == 1) && (k3 == koz))) return 0; //Д коз и К коз
            if (((z2 == 3) && (k1 == koz) && (z1 == 0) && (k2 == koz)) ||
                ((z3 == 3) && (k2 == koz) && (z2 == 0) && (k3 == koz)) ||
                ((z4 == 3) && (k4 == koz) && (z3 == 0) && (k3 == koz))) return 0; //В коз и Т коз
            if ((z1 == 0) && (z2 == 0) && (z3 == 0) && (z4 == 0)) return 0; //4 туза
            if ((((z1 == 0) && (z2 == 0) && (z3 == 0)) && (z4 == 3) && (k4 == koz)) ||
                (((z1 == 0) && (z4 == 0) && (z3 == 0)) && (z2 == 3) && (k2 == koz)) ||
                (((z4 == 0) && (z2 == 0) && (z3 == 0)) && (z1 == 3) && (k1 == koz)) ||
                (((z1 == 0) && (z2 == 0) && (z4 == 0)) && (z3 == 3) && (k3 == koz))) return 0; //3 туза + В коз
            if ((((z1 == 0) && (z2 == 0) && (z3 == 0)) && (z4 == 2) && (k4 == koz)) ||
                (((z1 == 0) && (z4 == 0) && (z3 == 0)) && (z2 == 2) && (k2 == koz)) ||
                (((z4 == 0) && (z2 == 0) && (z3 == 0)) && (z1 == 2) && (k1 == koz)) ||
                (((z1 == 0) && (z2 == 0) && (z4 == 0)) && (z3 == 2) && (k3 == koz))) return 0; //3 туза + Д коз
            if ((((z1 == 0) && (z2 == 0) && (z3 == 0)) && (z4 == 1) && (k4 == koz)) ||
                (((z1 == 0) && (z4 == 0) && (z3 == 0)) && (z2 == 1) && (k2 == koz)) ||
                (((z4 == 0) && (z2 == 0) && (z3 == 0)) && (z1 == 1) && (k1 == koz)) ||
                (((z1 == 0) && (z2 == 0) && (z4 == 0)) && (z3 == 1) && (k3 == koz))) return 0; //3 туза + К коз
            if ((((z1 == 0) && (z2 == 0) && (z3 == 0)) && (z4 == 5) && (k4 == koz)) ||
                (((z1 == 0) && (z4 == 0) && (z3 == 0)) && (z2 == 5) && (k2 == koz)) ||
                (((z4 == 0) && (z2 == 0) && (z3 == 0)) && (z1 == 5) && (k1 == koz)) ||
                (((z1 == 0) && (z2 == 0) && (z4 == 0)) && (z3 == 5) && (k3 == koz))) return 0; //3 туза + 9 коз
            if ((((z1 == 0) && (z2 == 0) && (z3 == 0)) && (z4 == 4) && (k4 == koz)) ||
                (((z1 == 0) && (z4 == 0) && (z3 == 0)) && (z2 == 4) && (k2 == koz)) ||
                (((z4 == 0) && (z2 == 0) && (z3 == 0)) && (z1 == 4) && (k1 == koz)) ||
                (((z1 == 0) && (z2 == 0) && (z4 == 0)) && (z3 == 4) && (k3 == koz))) return 0; //3 туза + 10 коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 3) && (k3 == koz)) ||
                ((z1 == 0) && (z2 == 0) && (z4 == 3) && (k4 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z2 == 3) && (k2 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z4 == 3) && (k4 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z3 == 3) && (k3 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z2 == 3) && (k2 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z1 == 3) && (k1 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z4 == 3) && (k3 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z3 == 3) && (k3 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z1 == 3) && (k1 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z2 == 3) && (k2 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z1 == 3) && (k1 == koz))) return 0; //2 туза + В коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 5) && (k3 == koz)) ||
                ((z1 == 0) && (z2 == 0) && (z4 == 5) && (k4 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z2 == 5) && (k2 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z4 == 5) && (k4 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z3 == 5) && (k3 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z2 == 5) && (k2 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z1 == 5) && (k1 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z4 == 5) && (k3 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z3 == 5) && (k3 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z1 == 5) && (k1 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z2 == 5) && (k2 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z1 == 5) && (k1 == koz))) return 0; //2 туза + 9 коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 1) && (k3 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z2 == 1) && (k2 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z3 == 1) && (k3 == koz) && (z2 == 2) && (k2 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z1 == 1) && (k1 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z3 == 1) && (k3 == koz) && (z1 == 2) && (k1 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z2 == 1) && (k2 == koz) && (z1 == 2) && (k1 == koz))) return 0; //2 туза + Д коз + К коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 4) && (k3 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z2 == 4) && (k2 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z3 == 4) && (k3 == koz) && (z2 == 2) && (k2 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z1 == 4) && (k1 == koz) && (z4 == 2) && (k4 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z3 == 4) && (k3 == koz) && (z1 == 2) && (k1 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z2 == 4) && (k2 == koz) && (z1 == 2) && (k1 == koz))) return 0; //2 туза + Д коз + 10 коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 1) && (k3 == koz) && (z4 == 4) && (k4 == koz)) ||
                ((z1 == 0) && (z3 == 0) && (z2 == 1) && (k2 == koz) && (z4 == 4) && (k4 == koz)) ||
                ((z1 == 0) && (z4 == 0) && (z3 == 1) && (k3 == koz) && (z2 == 4) && (k2 == koz)) ||
                ((z2 == 0) && (z3 == 0) && (z1 == 1) && (k1 == koz) && (z4 == 4) && (k4 == koz)) ||
                ((z4 == 0) && (z2 == 0) && (z3 == 1) && (k3 == koz) && (z1 == 4) && (k1 == koz)) ||
                ((z4 == 0) && (z3 == 0) && (z2 == 1) && (k2 == koz) && (z1 == 4) && (k1 == koz))) return 0; //2 туза + 10 коз + К коз
            if (((y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (k1 == k2) && (k2 == k3) && (z4 == 3) && (k4 == koz)) ||
                ((y2 == y3 - 1) && (y3 - 1 == y4 - 2) && (k2 == k3) && (k3 == k4) && (z1 == 3) && (k1 == koz))) return 0; //терц и В коз
            if (((z1 == 0) && (y1 == y2 - 1) && (y2 - 1 == y3 - 4) && (k1 == k2) && (k2 == k3) && (z4 == 3) && (k4 == koz)) ||
                ((z2 == 0) && (y2 == y3 - 1) && (y3 - 1 == y4 - 4) && (k2 == k3) && (k3 == k4) && (z1 == 3) && (k1 == koz))) return 0; //Т,К,10 и В коз
            if (((z1 == 0) && (y1 == y2 - 1) && (y2 - 1 == y3 - 2) && (k1 == k2) && (k2 == k3) && (z4 == 3) && (k4 == koz)) ||
                ((z2 == 0) && (y2 == y3 - 1) && (y3 - 1 == y4 - 2) && (k2 == k3) && (k3 == k4) && (z1 == 3) && (k1 == koz))) return 0; //Т,Д,10 и В коз
            if (((z1 == 0) && (z2 == 0) && (z3 == 4) && (k2 == koz) && (k3 == koz) && (z5 == 5)) ||
                ((z1 == 0) && (z3 == 0) && (z4 == 4) && (k3 == koz) && (k4 == koz) && (z5 == 5)) ||
                ((z2 == 0) && (z3 == 0) && (z4 == 4) && (k3 == koz) && (k4 == koz) && (z5 == 5)) ||
                ((z1 == 0) && (z2 == 0) && (z4 == 4) && (k2 == koz) && (k4 == koz) && (z5 == 5)) ||
                ((z1 == 0) && (z2 == 4) && (z3 == 0) && (k1 == koz) && (k2 == koz) && (z5 == 5)) ||
                ((z1 == 0) && (z2 == 4) && (z4 == 0) && (k1 == koz) && (k2 == koz) && (z5 == 5)) ||
                ((z1 == 0) && (z3 == 4) && (z4 == 0) && (k1 == koz) && (k3 == koz) && (z5 == 5)) ||
                ((z2 == 0) && (z3 == 4) && (z4 == 0) && (k2 == koz) && (k3 == koz) && (z5 == 5))) return 0; //Т + Т коз + 10 коз + 9 коз на кону.

            else return 1;
        }

        private void AskComp2()
        {
            /*second ask*/
            if (b == false) return;
            if(razd1==0)
                if (MessageBox.Show("Вы будете играть?", "2-й круг", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    b = false;
                    Image1Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    if(koz == 0)
                    {
                        ImageClubs.Enabled=true;
                        ImageDiamonds.Enabled=true;
                        ImageHearts.Enabled=true;
                        ImageClubs.Visible=true;
                        ImageDiamonds.Visible=true;
                        ImageHearts.Visible=true;
                     }
                     if(koz==1)
                     {
                        ImageSpades.Enabled=true;
                        ImageDiamonds.Enabled=true;
                        ImageHearts.Enabled=true;
                        ImageSpades.Visible=true;
                        ImageDiamonds.Visible=true;
                        ImageHearts.Visible=true;
                     }
                     if(koz==2)
                     {
                        ImageSpades.Enabled=true;
                        ImageClubs.Enabled=true;
                        ImageHearts.Enabled=true;
                        ImageSpades.Visible=true;
                        ImageClubs.Visible=true;
                        ImageHearts.Visible=true;
                     }
                     if(koz==3)
                     {
                        ImageSpades.Enabled=true;
                        ImageClubs.Enabled=true;
                        ImageDiamonds.Enabled=true;
                        ImageSpades.Visible=true;
                        ImageClubs.Visible=true;
                        ImageDiamonds.Visible=true;
                     }
                     return;
                  }
                else
                {
                    Image1p2.Visible = true;
                    return;
                }
            if (razd1 == 1)
            {
                rez = Ask2(1);
                if (rez != 100)
                {
                    b = false;
                    if (rez == 0) koz = 0;
                    if (rez == 1) koz = 1;
                    if (rez == 2) koz = 2;
                    if (rez == 3) koz = 3;
                    Image2Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    End2();
                    return;
                }
                else
                {
                    Image2p2.Visible = true;
                    return;
                }
            }
            if (razd1 == 2)
            {
                rez = Ask2(2);
                if (rez != 100)
                {
                    b = false;
                    if (rez == 0) koz = 0;
                    if (rez == 1) koz = 1;
                    if (rez == 2) koz = 2;
                    if (rez == 3) koz = 3;
                    Image3Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    End2();
                    return;
                }
                else
                {
                    Image3p2.Visible = true;
                    return;
                }
            }
            if (razd1 == 3)
            {
                rez = Ask2(3);
                if (rez != 100)
                {
                    b = false;
                    if (rez == 0) koz = 0;
                    if (rez == 1) koz = 1;
                    if (rez == 2) koz = 2;
                    if (rez == 3) koz = 3;
                    Image4Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    End2();
                    return;
                }
                else
                {
                    Image4p2.Visible = true;
                    return;
                }
            }
        }

        public int Ask2(int t)
        {
            y1 = imass[t,0];
            y2 = imass[t,1];
            y3 = imass[t,2];
            y4 = imass[t,3];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            k1 = k2 = k3 = k4 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            if ((k1 == k2) && (k2 == k3) && (k3 == k4)) if (koz != k1) return k1; // 4 same
            if ((z1 == 3) && (k1 == k2) && (z2 == 5) || (z1 == 3) && (k1 == k3) && (z3 == 5)) if (koz != k1) return k1;
            if ((z2 == 3) && (k2 == k3) && (z3 == 5) || (z2 == 3) && (k2 == k4) && (z4 == 5)) if (koz != k2) return k2;
            if ((z3 == 3) && (k3 == k4) && (z4 == 5)) if (koz != k3) return k3; //В и 9 одной масти
            if ((k1 == k2) && (k2 == k3) || (k2 == k3) && (k3 == k4)) if (koz != k2) return k2; // 3 same
            if ((z1 == 3) && (z2 == 0) && (z3 == 0) && (z4 == 0) && (k1 != k2) && (k1 != k3) && (k1 != k4)) if (koz != k1) return k1;
            if ((z2 == 3) && (z1 == 0) && (z3 == 0) && (z4 == 0) && (k2 != k1) && (k2 != k3) && (k2 != k4)) if (koz != k2) return k2;
            if ((z3 == 3) && (z2 == 0) && (z1 == 0) && (z4 == 0) && (k3 != k2) && (k3 != k1) && (k3 != k4)) if (koz != k3) return k3;
            if ((z4 == 3) && (z2 == 0) && (z3 == 0) && (z1 == 0) && (k4 != k3) && (k4 != k2) && (k4 != k1)) if (koz != k4) return k4; // 3T + B др. масти
            if ((z1 == 0) && (z2 == 3) && (k1 == k2) && (z3 == 0) && (k3 != k1)) if (koz != k1) return k1;
            if ((z1 == 0) && (z2 == 3) && (k1 == k2) && (z4 == 0) && (k4 != k1)) if (koz != k1) return k1;
            if ((z2 == 0) && (z3 == 3) && (k2 == k3) && (z1 == 0) && (k1 != k2)) if (koz != k2) return k2;
            if ((z2 == 0) && (z3 == 3) && (k2 == k3) && (z4 == 0) && (k4 != k2)) if (koz != k2) return k2;
            if ((z3 == 0) && (z4 == 3) && (k3 == k4) && (z1 == 0) && (k1 != k3)) if (koz != k3) return k3;
            if ((z3 == 0) && (z4 == 3) && (k3 == k4) && (z2 == 0) && (k2 != k3)) if (koz != k3) return k3; // В + Т одной масти и Т др.
            return 100;
        }

        public void LastAsk(int t)
        {
            y1 = imass[t,0];
            y2 = imass[t,1];
            y3 = imass[t,2];
            y4 = imass[t,3];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            k1 = k2 = k3 = k4 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            if ((k1 == k2) && (k2 == k3) && (k3 == k4)) if (koz != k1) { koz = k1; return; } // 4 same
            if ((z1 == 3) && (k1 == k2) && (z2 == 5) || (z1 == 3) && (k1 == k3) && (z3 == 5)) if (koz != k1) { koz = k1; return; }
            if ((z2 == 3) && (k2 == k3) && (z3 == 5) || (z2 == 3) && (k2 == k4) && (z4 == 5)) if (koz != k2) { koz = k2; return; }
            if ((z3 == 3) && (k3 == k4) && (z4 == 5)) if (koz != k3) { koz = k3; return; } //В и 9 одной масти
            if ((k1 == k2) && (k2 == k3) || (k2 == k3) && (k3 == k4)) if (koz != k2) { koz = k2; return; } // 3 same
            if ((z1 == 3) && (z2 == 0) && (z3 == 0) && (z4 == 0) && (k1 != k2) && (k1 != k3) && (k1 != k4)) if (koz != k1) { koz = k1; return; }
            if ((z2 == 3) && (z1 == 0) && (z3 == 0) && (z4 == 0) && (k2 != k1) && (k2 != k3) && (k2 != k4)) if (koz != k2) { koz = k2; return; }
            if ((z3 == 3) && (z2 == 0) && (z1 == 0) && (z4 == 0) && (k3 != k2) && (k3 != k1) && (k3 != k4)) if (koz != k3) { koz = k3; return; }
            if ((z4 == 3) && (z2 == 0) && (z3 == 0) && (z1 == 0) && (k4 != k3) && (k4 != k2) && (k4 != k1)) if (koz != k4) { koz = k4; return; } // 3T + B др. масти
            if ((z1 == 0) && (z2 == 3) && (k1 == k2) && (z3 == 0) && (k3 != k1)) if (koz != k1) { koz = k1; return; }
            if ((z1 == 0) && (z2 == 3) && (k1 == k2) && (z4 == 0) && (k4 != k1)) if (koz != k1) { koz = k1; return; }
            if ((z2 == 0) && (z3 == 3) && (k2 == k3) && (z1 == 0) && (k1 != k2)) if (koz != k2) { koz = k2; return; }
            if ((z2 == 0) && (z3 == 3) && (k2 == k3) && (z4 == 0) && (k4 != k2)) if (koz != k2) { koz = k2; return; }
            if ((z3 == 0) && (z4 == 3) && (k3 == k4) && (z1 == 0) && (k1 != k3)) if (koz != k3) { koz = k3; return; }
            if ((z3 == 0) && (z4 == 3) && (k3 == k4) && (z2 == 0) && (k2 != k3)) if (koz != k3) { koz = k3; return; } // В + Т одной масти и Т др.
            if ((((k1 == k2) || (k2 == k3)) && (koz != k2))) { koz = k2; return; }
            if ((k3 == k4) && (k3 != koz)) { koz = k3; return; } // 2 same
            if (z1 == 3) if (koz != k1) { koz = k1; return; }
            if (z2 == 3) if (koz != k2) { koz = k2; return; }
            if (z3 == 3) if (koz != k3) { koz = k3; return; }
            if (z4 == 3) if (koz != k4) { koz = k4; return; }
            if (k1 == k2) if ((z1 <= z3) && (z1 <= z4)) if (koz != k1) { koz = k1; return; }
            if (k2 == k3) if (z2 <= z4) if (koz != k2) { koz = k2; return; }
            if (k3 == k4) if (koz != k1) { koz = k3; return; }
            if (z1 != 0) if (koz != k1) { koz = k1; return; }
            if (z2 != 0) if (koz != k2) { koz = k2; return; }
            if (z3 != 0) if (koz != k3) { koz = k3; return; }
            if (z4 != 0) if (koz != k4) { koz = k4; return; }
        }

        private void End()
        {
            imass[razd1++,4] = cards[n++];
            if (razd1 == 4) razd1 = 0;
            imass[razd1,4] = cards[n++];
            imass[razd1++,5] = cards[n++];
            if (razd1 == 4) razd1 = 0;
            imass[razd1,4] = cards[n++];
            imass[razd1++,5] = cards[n++];
            if (razd1 == 4) razd1 = 0;
            imass[razd1,4] = cards[n++];
            imass[razd1++,5] = cards[n++];
            if (razd1 == 4) razd1 = 0;
            imass[razd1,5] = cards[n];
            pictureBox25.Visible = false;
            pictureBox26.Visible = false;
            razd++;
            if (razd == 4) razd = 0;
            for (i = 0; i < 4; i++)
                for (x = 0; x < 6; x++)
                    for (j = 1; j < 6; j++)
                        for (g = 0; g < 6; g++)
                            if (imass[i, j - 1] > imass[i, j])
                            {
                                temp = imass[i, j - 1];
                                imass[i, j - 1] = imass[i, j];
                                imass[i, j] = temp;
                            }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 6; j++)
                {
                    if ((open || i ==0 ) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                    if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8+rub1*24+imass[i, j]];
                    if(!open && i != 0) pmass[i, j].Image = imageList1.Images[152 + rub];
                    pmass[i, j].Visible = true;
                }
            ImageTrump.Image = imageList1.Images[koz + 25];
            ImageTrump.Visible = true;
            button1.Visible = true;
            button1.Enabled = true;
            player = play;
            for (i = 0; i < 4; i++)
            {
                k1 = k2 = k3 = k4 = k5 = k6 = 0;
                y1 = imass[i,0];
                y2 = imass[i,1];
                y3 = imass[i,2];
                y4 = imass[i,3];
                y5 = imass[i,4];
                y6 = imass[i,5];
                z1 = y1;
                z2 = y2;
                z3 = y3;
                z4 = y4;
                z5 = y5;
                z6 = y6;
                while (z1 > 5)
                {
                    z1 -= 6;
                    k1++;
                }
                while (z2 > 5)
                {
                    z2 -= 6;
                    k2++;
                }
                while (z3 > 5)
                {
                    z3 -= 6;
                    k3++;
                }
                while (z4 > 5)
                {
                    z4 -= 6;
                    k4++;
                }
                while (z5 > 5)
                {
                    z5 -= 6;
                    k5++;
                }
                while (z6 > 5)
                {
                    z6 -= 6;
                    k6++;
                }
                if (((z1 == 1) && (z1 == z2 + 1) && (k1 == k2) && (k1 == koz)) ||
                 ((z2 == 1) && (z2 == z3 + 1) && (k3 == k2) && (k2 == koz)) ||
                 ((z3 == 1) && (z3 == z4 + 1) && (k3 == k4) && (k3 == koz)) ||
                 ((z4 == 1) && (z4 == z5 + 1) && (k5 == k4) && (k4 == koz)) ||
                 ((z5 == 1) && (z5 == z6 + 1) && (k5 == k6) && (k5 == koz))) bella[i]++;
            }

        }

        public void End2()
        {
             imass[razd,4] = cards[n++];
            imass[razd++,5] = cards[n++];
            if (razd == 4) razd = 0;
            imass[razd,4] = cards[n++];
            imass[razd++,5] = cards[n++];
            if (razd == 4) razd = 0;
            imass[razd,4] = cards[n++];
            imass[razd++,5] = cards[n++];
            if (razd == 4) razd = 0;
            imass[razd,4] = cards[n++];
            imass[razd++,5] = cards[n++];
            if (razd == 4) razd = 0;
            pictureBox26.Visible = false;
            pictureBox25.Visible = false;
            razd++;
            if (razd == 4) razd = 0;
            for (i = 0; i < 4; i++)
                for (x = 0; x < 6; x++)
                    for (j = 1; j < 6; j++)
                        for (g = 0; g < 6; g++)
                            if (imass[i, j - 1] > imass[i, j])
                            {
                                temp = imass[i, j - 1];
                                imass[i, j - 1] = imass[i, j];
                                imass[i, j] = temp;
                            }
            for ( i = 0; i < 4; i++)
                for (j = 0; j < 6; j++)
                {
                    if ((open || i == 0) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                    if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8 + rub1 * 24 + imass[i, j]];
                    if (!open && i != 0) pmass[i, j].Image = imageList1.Images[152 + rub];
                    pmass[i, j].Visible = true;
                }
            ImageTrump.Image = imageList1.Images[koz + 25];
            ImageTrump.Visible = true;
            button1.Visible = true;
            button1.Enabled = true;
            player = play;
            for (i = 0; i < 4; i++)
            {
                k1 = k2 = k3 = k4 = k5 = k6 = 0;
                y1 = imass[i,0];
                y2 = imass[i,1];
                y3 = imass[i,2];
                y4 = imass[i,3];
                y5 = imass[i,4];
                y6 = imass[i,5];
                z1 = y1;
                z2 = y2;
                z3 = y3;
                z4 = y4;
                z5 = y5;
                z6 = y6;
                while (z1 > 5)
                {
                    z1 -= 6;
                    k1++;
                }
                while (z2 > 5)
                {
                    z2 -= 6;
                    k2++;
                }
                while (z3 > 5)
                {
                    z3 -= 6;
                    k3++;
                }
                while (z4 > 5)
                {
                    z4 -= 6;
                    k4++;
                }
                while (z5 > 5)
                {
                    z5 -= 6;
                    k5++;
                }
                while (z6 > 5)
                {
                    z6 -= 6;
                    k6++;
                }
                if (((z1 == 1) && (z1 == z2 + 1) && (k1 == k2) && (k1 == koz)) ||
                 ((z2 == 1) && (z2 == z3 + 1) && (k3 == k2) && (k2 == koz)) ||
                 ((z3 == 1) && (z3 == z4 + 1) && (k3 == k4) && (k3 == koz)) ||
                 ((z4 == 1) && (z4 == z5 + 1) && (k5 == k4) && (k4 == koz)) ||
                 ((z5 == 1) && (z5 == z6 + 1) && (k5 == k6) && (k5 == koz))) bella[i]++;
            }
        
        }

        private void image3Play_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (hod == false) return;
            if(card4[0]==-1)
            {
               Image1.Visible=true;
               if(rub1==0)
                   Image1.Image = imageList1.Images[imass[0,0]];
               else
                   Image1.Image = imageList1.Images[8+rub1*24+imass[0, 0]];
               pictureBox1.Visible = false;
               pictureBox1.Enabled=false;
               card4[0]=imass[0,0];
               imass[0,0]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
               int y1,y2,y3,y4,y5,y6,y7,z1,z2,z3,z4,z5,z6,z7,k1,k2,k3,k4,k5,k6,k7,kozir=0,karta=0;
               k1=k2=k3=k4=k5=k6=k7=0;
               y1=imass[0,0];if(y1==110){y1=-1;k1=-1;}
               y2=imass[0,1];if(y2==110){y2=-1;k2=-1;}
               y3=imass[0,2];if(y3==110){y3=-1;k3=-1;}
               y4=imass[0,3];if(y4==110){y4=-1;k4=-1;}
               y5=imass[0,4];if(y5==110){y5=-1;k5=-1;}
               y6=imass[0,5];if(y6==110){y6=-1;k6=-1;}
               y7=card4[0];
               z1=y1;
               z2=y2;
               z3=y3;
               z4=y4;
               z5=y5;
               z6=y6;
               z7=y7;
               while(z1>5)
               {
                  z1-=6;
                  k1++;
               }
               while(z2>5)
               {
                  z2-=6;
                  k2++;
               }
               while(z3>5)
               {
                  z3-=6;
                  k3++;
               }
               while(z4>5)
               {
                  z4-=6;
                  k4++;
               }
               while(z5>5)
               {
                  z5-=6;
                  k5++;
               }
               while(z6>5)
               {
                  z6-=6;
                  k6++;
               }
               while(z7>5)
               {
                  z7-=6;
                  k7++;
               }
               if(k1==koz) kozir++;
               if(k2==koz) kozir++;
               if(k3==koz) kozir++;
               if(k4==koz) kozir++;
               if(k5==koz) kozir++;
               if(k6==koz) kozir++;
               if(k1==k7) karta++;
               if(k2==k7) karta++;
               if(k3==k7) karta++;
               if(k4==k7) karta++;
               if(k5==k7) karta++;
               if(k6==k7) karta++;
               if(k1==k7) {i++;i--;}
               else{if((k1!=k7)&&(karta>0)) return;
               else if((k1!=koz)&&(kozir>0)) return; }
            if(card4[1]==-1)
            {
               Image2.Visible=true;
               if (rub1 == 0)
                   Image2.Image = imageList1.Images[imass[0, 0]];
               else
                   Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 0]]; 
               pictureBox1.Visible = false;
               pictureBox1.Enabled=false;
               card4[1]=imass[0,0];
               imass[0,0]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
            if(card4[2]==-1)
            {
               Image3.Visible=true;
               if (rub1 == 0)
                   Image3.Image = imageList1.Images[imass[0, 0]];
               else
                   Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 0]];
               pictureBox1.Visible = false;
               pictureBox1.Enabled=false;
               card4[2]=imass[0,0];
               imass[0,0]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 0]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 0]];
                pictureBox1.Visible = false;
                pictureBox1.Enabled = false;
                card4[3] = imass[0,0];
                imass[0,0] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
             if (hod == false) return;
            if(card4[0]==-1)
            {
               Image1.Visible=true;
               if (rub1 == 0)
                   Image1.Image = imageList1.Images[imass[0, 1]];
               else
                   Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 1]];
               pictureBox2.Visible = false;
               pictureBox2.Enabled=false;
               card4[0]=imass[0,1];
               imass[0,1]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
               int y1,y2,y3,y4,y5,y6,y7,z1,z2,z3,z4,z5,z6,z7,k1,k2,k3,k4,k5,k6,k7,kozir=0,karta=0;
               k1=k2=k3=k4=k5=k6=k7=0;
               y1=imass[0,0];if(y1==110){y1=-1;k1=-1;}
               y2=imass[0,1];if(y2==110){y2=-1;k2=-1;}
               y3=imass[0,2];if(y3==110){y3=-1;k3=-1;}
               y4=imass[0,3];if(y4==110){y4=-1;k4=-1;}
               y5=imass[0,4];if(y5==110){y5=-1;k5=-1;}
               y6=imass[0,5];if(y6==110){y6=-1;k6=-1;}
               y7=card4[0];
               z1=y1;
               z2=y2;
               z3=y3;
               z4=y4;
               z5=y5;
               z6=y6;
               z7=y7;
               while(z1>5)
               {
                  z1-=6;
                  k1++;
               }
               while(z2>5)
               {
                  z2-=6;
                  k2++;
               }
               while(z3>5)
               {
                  z3-=6;
                  k3++;
               }
               while(z4>5)
               {
                  z4-=6;
                  k4++;
               }
               while(z5>5)
               {
                  z5-=6;
                  k5++;
               }
               while(z6>5)
               {
                  z6-=6;
                  k6++;
               }
               while(z7>5)
               {
                  z7-=6;
                  k7++;
               }
               if(k1==koz) kozir++;
               if(k2==koz) kozir++;
               if(k3==koz) kozir++;
               if(k4==koz) kozir++;
               if(k5==koz) kozir++;
               if(k6==koz) kozir++;
               if(k1==k7) karta++;
               if(k2==k7) karta++;
               if(k3==k7) karta++;
               if(k4==k7) karta++;
               if(k5==k7) karta++;
               if(k6==k7) karta++;
               if(k2==k7) {i++;i--;}
               else{if((k2!=k7)&&(karta>0)) return;
               else if((k2!=koz)&&(kozir>0)) return; }
            if(card4[1]==-1)
            {
               Image2.Visible=true;
               if (rub1 == 0)
                   Image2.Image = imageList1.Images[imass[0, 1]];
               else
                   Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 1]];
               pictureBox2.Visible = false;
               pictureBox2.Enabled=false;
               card4[1]=imass[0,1];
               imass[0,1]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
            if(card4[2]==-1)
            {
               Image3.Visible=true;
               if (rub1 == 0)
                   Image3.Image = imageList1.Images[imass[0, 1]];
               else
                   Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 1]];
               pictureBox2.Visible = false;
               pictureBox2.Enabled=false;
               card4[2]=imass[0,1];
               imass[0,1]=110;
               play++;
               hod=false;
               timer1.Enabled = true;
               return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 1]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 1]];
                pictureBox2.Visible = false;
                pictureBox2.Enabled = false;
                card4[3] = imass[0,1];
                imass[0,1] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (hod == false) return;
            if (card4[0] == -1)
            {
                Image1.Visible = true;
                if (rub1 == 0)
                    Image1.Image = imageList1.Images[imass[0, 2]];
                else
                    Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 2]];
                pictureBox3.Visible = false;
                pictureBox3.Enabled = false;
                card4[0] = imass[0, 2];
                imass[0, 2] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            int y1, y2, y3, y4, y5, y6, y7, z1, z2, z3, z4, z5, z6, z7, k1, k2, k3, k4, k5, k6, k7, kozir = 0, karta = 0;
            k1 = k2 = k3 = k4 = k5 = k6 = k7 = 0;
            y1 = imass[0, 0]; if (y1 == 110) { y1 = -1; k1 = -1; }
            y2 = imass[0, 1]; if (y2 == 110) { y2 = -1; k2 = -1; }
            y3 = imass[0, 2]; if (y3 == 110) { y3 = -1; k3 = -1; }
            y4 = imass[0, 3]; if (y4 == 110) { y4 = -1; k4 = -1; }
            y5 = imass[0, 4]; if (y5 == 110) { y5 = -1; k5 = -1; }
            y6 = imass[0, 5]; if (y6 == 110) { y6 = -1; k6 = -1; }
            y7 = card4[0];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            z7 = y7;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            while (z7 > 5)
            {
                z7 -= 6;
                k7++;
            }
            if (k1 == koz) kozir++;
            if (k2 == koz) kozir++;
            if (k3 == koz) kozir++;
            if (k4 == koz) kozir++;
            if (k5 == koz) kozir++;
            if (k6 == koz) kozir++;
            if (k1 == k7) karta++;
            if (k2 == k7) karta++;
            if (k3 == k7) karta++;
            if (k4 == k7) karta++;
            if (k5 == k7) karta++;
            if (k6 == k7) karta++;
            if (k3 == k7) { i++; i--; }
            else
            {
                if ((k3 != k7) && (karta > 0)) return;
                else if ((k3 != koz) && (kozir > 0)) return;
            }
            if (card4[1] == -1)
            {
                Image2.Visible = true;
                if (rub1 == 0)
                    Image2.Image = imageList1.Images[imass[0, 2]];
                else
                    Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 2]];
                pictureBox3.Visible = false;
                pictureBox3.Enabled = false;
                card4[1] = imass[0,2];
                imass[0,2] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[2] == -1)
            {
                Image3.Visible = true;
                if (rub1 == 0)
                    Image3.Image = imageList1.Images[imass[0, 2]];
                else
                    Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 2]];
                pictureBox3.Visible = false;
                pictureBox3.Enabled = false;
                card4[2] = imass[0,2];
                imass[0,2] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 2]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 2]];
                pictureBox3.Visible = false;
                pictureBox3.Enabled = false;
                card4[3] = imass[0,2];
                imass[0,2] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (hod == false) return;
            if (card4[0] == -1)
            {
                Image1.Visible = true;
                if (rub1 == 0)
                    Image1.Image = imageList1.Images[imass[0, 3]];
                else
                    Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 3]];
                pictureBox4.Visible = false;
                pictureBox4.Enabled = false;
                card4[0] = imass[0, 3];
                imass[0, 3] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            int y1, y2, y3, y4, y5, y6, y7, z1, z2, z3, z4, z5, z6, z7, k1, k2, k3, k4, k5, k6, k7, kozir = 0, karta = 0;
            k1 = k2 = k3 = k4 = k5 = k6 = k7 = 0;
            y1 = imass[0, 0]; if (y1 == 110) { y1 = -1; k1 = -1; }
            y2 = imass[0, 1]; if (y2 == 110) { y2 = -1; k2 = -1; }
            y3 = imass[0, 2]; if (y3 == 110) { y3 = -1; k3 = -1; }
            y4 = imass[0, 3]; if (y4 == 110) { y4 = -1; k4 = -1; }
            y5 = imass[0, 4]; if (y5 == 110) { y5 = -1; k5 = -1; }
            y6 = imass[0, 5]; if (y6 == 110) { y6 = -1; k6 = -1; }
            y7 = card4[0];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            z7 = y7;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            while (z7 > 5)
            {
                z7 -= 6;
                k7++;
            }
            if (k1 == koz) kozir++;
            if (k2 == koz) kozir++;
            if (k3 == koz) kozir++;
            if (k4 == koz) kozir++;
            if (k5 == koz) kozir++;
            if (k6 == koz) kozir++;
            if (k1 == k7) karta++;
            if (k2 == k7) karta++;
            if (k3 == k7) karta++;
            if (k4 == k7) karta++;
            if (k5 == k7) karta++;
            if (k6 == k7) karta++;
            if (k4 == k7) { i++; i--; }
            else
            {
                if ((k4 != k7) && (karta > 0)) return;
                else if ((k4 != koz) && (kozir > 0)) return;
            }
            if (card4[1] == -1)
            {
                Image2.Visible = true;
                if (rub1 == 0)
                    Image2.Image = imageList1.Images[imass[0, 3]];
                else
                    Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 3]];
                pictureBox4.Visible = false;
                pictureBox4.Enabled = false;
                card4[1] = imass[0,3];
                imass[0,3] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[2] == -1)
            {
                Image3.Visible = true;
                if (rub1 == 0)
                    Image3.Image = imageList1.Images[imass[0, 3]];
                else
                    Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 3]];
                pictureBox4.Visible = false;
                pictureBox4.Enabled = false;
                card4[2] = imass[0,3];
                imass[0,3] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 3]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 3]];
                pictureBox4.Visible = false;
                pictureBox4.Enabled = false;
                card4[3] = imass[0,3];
                imass[0,3] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (hod == false) return;
            if (card4[0] == -1)
            {
                Image1.Visible = true;
                if (rub1 == 0)
                    Image1.Image = imageList1.Images[imass[0, 4]];
                else
                    Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 4]];
                pictureBox5.Visible = false;
                pictureBox5.Enabled = false;
                card4[0] = imass[0, 4];
                imass[0, 4] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            int y1, y2, y3, y4, y5, y6, y7, z1, z2, z3, z4, z5, z6, z7, k1, k2, k3, k4, k5, k6, k7, kozir = 0, karta = 0;
            k1 = k2 = k3 = k4 = k5 = k6 = k7 = 0;
            y1 = imass[0, 0]; if (y1 == 110) { y1 = -1; k1 = -1; }
            y2 = imass[0, 1]; if (y2 == 110) { y2 = -1; k2 = -1; }
            y3 = imass[0, 2]; if (y3 == 110) { y3 = -1; k3 = -1; }
            y4 = imass[0, 3]; if (y4 == 110) { y4 = -1; k4 = -1; }
            y5 = imass[0, 4]; if (y5 == 110) { y5 = -1; k5 = -1; }
            y6 = imass[0, 5]; if (y6 == 110) { y6 = -1; k6 = -1; }
            y7 = card4[0];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            z7 = y7;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            while (z7 > 5)
            {
                z7 -= 6;
                k7++;
            }
            if (k1 == koz) kozir++;
            if (k2 == koz) kozir++;
            if (k3 == koz) kozir++;
            if (k4 == koz) kozir++;
            if (k5 == koz) kozir++;
            if (k6 == koz) kozir++;
            if (k1 == k7) karta++;
            if (k2 == k7) karta++;
            if (k3 == k7) karta++;
            if (k4 == k7) karta++;
            if (k5 == k7) karta++;
            if (k6 == k7) karta++;
            if (k5 == k7) { i++; i--; }
            else
            {
                if ((k5 != k7) && (karta > 0)) return;
                else if ((k5 != koz) && (kozir > 0)) return;
            }
            if (card4[1] == -1)
            {
                Image2.Visible = true;
                if (rub1 == 0)
                    Image2.Image = imageList1.Images[imass[0, 4]];
                else
                    Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 4]];
                pictureBox5.Visible = false;
                pictureBox5.Enabled = false;
                card4[1] = imass[0,4];
                imass[0,4] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[2] == -1)
            {
                Image3.Visible = true;
                if (rub1 == 0)
                    Image3.Image = imageList1.Images[imass[0, 4]];
                else
                    Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 4]];
                pictureBox5.Visible = false;
                pictureBox5.Enabled = false;
                card4[2] = imass[0,4];
                imass[0,4] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 4]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 4]];
                pictureBox5.Visible = false;
                pictureBox5.Enabled = false;
                card4[3] = imass[0,4];
                imass[0,4] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (hod == false) return;
            if (card4[0] == -1)
            {
                Image1.Visible = true;
                if (rub1 == 0)
                    Image1.Image = imageList1.Images[imass[0, 5]];
                else
                    Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 5]];
                pictureBox6.Visible = false;
                pictureBox6.Enabled = false;
                card4[0] = imass[0, 5];
                imass[0, 5] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            int y1, y2, y3, y4, y5, y6, y7, z1, z2, z3, z4, z5, z6, z7, k1, k2, k3, k4, k5, k6, k7, kozir = 0, karta = 0;
            k1 = k2 = k3 = k4 = k5 = k6 = k7 = 0;
            y1 = imass[0, 0]; if (y1 == 110) { y1 = -1; k1 = -1; }
            y2 = imass[0, 1]; if (y2 == 110) { y2 = -1; k2 = -1; }
            y3 = imass[0, 2]; if (y3 == 110) { y3 = -1; k3 = -1; }
            y4 = imass[0, 3]; if (y4 == 110) { y4 = -1; k4 = -1; }
            y5 = imass[0, 4]; if (y5 == 110) { y5 = -1; k5 = -1; }
            y6 = imass[0, 5]; if (y6 == 110) { y6 = -1; k6 = -1; }
            y7 = card4[0];
            z1 = y1;
            z2 = y2;
            z3 = y3;
            z4 = y4;
            z5 = y5;
            z6 = y6;
            z7 = y7;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            while (z5 > 5)
            {
                z5 -= 6;
                k5++;
            }
            while (z6 > 5)
            {
                z6 -= 6;
                k6++;
            }
            while (z7 > 5)
            {
                z7 -= 6;
                k7++;
            }
            if (k1 == koz) kozir++;
            if (k2 == koz) kozir++;
            if (k3 == koz) kozir++;
            if (k4 == koz) kozir++;
            if (k5 == koz) kozir++;
            if (k6 == koz) kozir++;
            if (k1 == k7) karta++;
            if (k2 == k7) karta++;
            if (k3 == k7) karta++;
            if (k4 == k7) karta++;
            if (k5 == k7) karta++;
            if (k6 == k7) karta++;
            if (k6 == k7) { i++; i--; }
            else
            {
                if ((k6 != k7) && (karta > 0)) return;
                else if ((k6 != koz) && (kozir > 0)) return;
            }
            if (card4[1] == -1)
            {
                Image2.Visible = true;
                if (rub1 == 0)
                    Image2.Image = imageList1.Images[imass[0, 5]];
                else
                    Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 5]];
                pictureBox6.Visible = false;
                pictureBox6.Enabled = false;
                card4[1] = imass[0,5];
                imass[0,5] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[2] == -1)
            {
                Image3.Visible = true;
                if (rub1 == 0)
                    Image3.Image = imageList1.Images[imass[0, 5]];
                else
                    Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 5]];
                pictureBox6.Visible = false;
                pictureBox6.Enabled = false;
                card4[2] = imass[0,5];
                imass[0,5] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
            if (card4[3] == -1)
            {
                Image4.Visible = true;
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[0, 5]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[0, 5]];
                pictureBox6.Visible = false;
                pictureBox6.Enabled = false;
                card4[3] = imass[0,5];
                imass[0,5] = 110;
                play++;
                hod = false;
                timer1.Enabled = true;
                return;
            }
        }

        private void ImageClubs_Click(object sender, EventArgs e)
        {
            koz = 1;
            ImageSpades.Enabled = false;
            ImageClubs.Enabled = false;
            ImageDiamonds.Enabled = false;
            ImageHearts.Enabled = false;
            ImageSpades.Visible = false;
            ImageClubs.Visible = false;
            ImageDiamonds.Visible = false;
            ImageHearts.Visible = false;
            End2();
        }

        private void ImageSpades_Click(object sender, EventArgs e)
        {
            koz = 0;
            ImageSpades.Enabled = false;
            ImageClubs.Enabled = false;
            ImageDiamonds.Enabled = false;
            ImageHearts.Enabled = false;
            ImageSpades.Visible = false;
            ImageClubs.Visible = false;
            ImageDiamonds.Visible = false;
            ImageHearts.Visible = false;
            End2();
        }

        private void ImageDiamonds_Click(object sender, EventArgs e)
        {
            koz = 2;
            ImageSpades.Enabled = false;
            ImageClubs.Enabled = false;
            ImageDiamonds.Enabled = false;
            ImageHearts.Enabled = false;
            ImageSpades.Visible = false;
            ImageClubs.Visible = false;
            ImageDiamonds.Visible = false;
            ImageHearts.Visible = false;
            End2();
        }

        private void ImageHearts_Click(object sender, EventArgs e)
        {
            koz = 3;
            ImageSpades.Enabled = false;
            ImageClubs.Enabled = false;
            ImageDiamonds.Enabled = false;
            ImageHearts.Enabled = false;
            ImageSpades.Visible = false;
            ImageClubs.Visible = false;
            ImageDiamonds.Visible = false;
            ImageHearts.Visible = false;
            End2();
        }

        public void NewGame()
        {
            Image1Play.Visible = false;
            Image2Play.Visible = false;
            Image3Play.Visible = false;
            Image4Play.Visible = false;
            ImageTrump.Visible = false;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 6; j++)
                {
                    pmass[i, j].Image = imageList1.Images[31];
                    ccard[i,j] = -1;
                    imass[i, j] = 110;
                }
            level = 0;
            koz = 0;
            play = -1;
            a = true;
            b = true;
            hod = false;
            timer1.Enabled = false;
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            Image1p1.Visible = false;
            Image2p1.Visible = false;
            Image3p1.Visible = false;
            Image4p1.Visible = false;
            Image1p2.Visible = false;
            Image2p2.Visible = false;
            Image3p2.Visible = false;
            Image4p2.Visible = false;
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox4.Enabled = true;
            pictureBox5.Enabled = true;
            pictureBox6.Enabled = true;
            Random rand = new Random();
            for (i = 0; i < 24; i++)
                cards[i] = 110;
            i = 0;
            while (i < 24)
            {
                j = rand.Next(0, 24);
                if (cards[j] != 110) continue;
                else
                {
                    cards[j] = i;
                    i++;
                }
            }
            n = 0;
            razd1 = razd;
            for (i = 0; i < 4; i++) { bella[i] = 0; col[i] = 0; takenHits[i] = 0; card4[i] = -1; prem[i] = 10; hitsTemp[i] = 0; xb1[i] = false; }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    razd1++;
                    if (razd1 == 4) razd1 = 0;
                    imass[razd1, i] = cards[n++];
                    terz[i, j] = 0;
                }
            for (i = 0; i < 4; i++)
                for (x = 0; x < 4; x++)
                    for (j = 1; j < 4; j++)
                        for (g = 0; g < 4; g++)
                            if (imass[i, j - 1] > imass[i, j])
                            {
                                temp = imass[i, j - 1];
                                imass[i, j - 1] = imass[i, j];
                                imass[i, j] = temp;
                            }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                {
                    if ((open || i == 0) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                    if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8 + rub1 * 24 + imass[i, j]];
                    if (!open && i != 0) pmass[i, j].Image = imageList1.Images[152 + rub];
                    pmass[i, j].Visible = true;
                }
            kon = cards[16];
            pictureBox25.Visible = true;
            pictureBox26.Visible = true;
            pictureBox26.Image = imageList1.Images[152+rub];
            if(rub1==0)
                pictureBox25.Image = imageList1.Images[cards[n]];
            else 
                pictureBox25.Image = imageList1.Images[8+rub1*24+cards[n]];
            this.Refresh();
            kon1 = kon;
            while (kon1 > 5)
            {
                kon1 -= 6;
                koz++;
            }
            if (kon1 == 3)
            {
                if (razd == 3) MessageBox.Show("Игроку 1 выпал Валет");
                else MessageBox.Show("Игроку " + Convert.ToString(razd + 2) + " выпал Валет");
                play = razd1 + 1;
                if (play == 4) play = 0;
                if (razd == 0) Image2Play.Visible = true;
                if (razd == 1) Image3Play.Visible = true;
                if (razd == 2) Image4Play.Visible = true;
                if (razd == 3) Image1Play.Visible = true;
                razd1++;
                if (razd1 == 4) razd1 = 0;
                a = false;
                b = false;
                End();
            }
            if (a == true)
                for (i = 0; i < 4; i++)
                {
                    razd1++;
                    if (razd1 == 4) razd1 = 0;
                    if (a == true)
                        AskComp();
                }
            if ((a == true) && (b == true))
                for (i = 0; i < 3; i++)
                {
                    razd1++;
                    if (razd1 == 4) razd1 = 0;
                    if (b == true)
                        AskComp2();
                }
            razd1++;
            if (razd1 == 4) razd1 = 0;
            if ((a == true) && (b == true))
            {
                if (razd1 == 0)
                {
                    MessageBox.Show(" Выбирайте масть! ");
                    Image1Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    if (koz == 0)
                    {
                        ImageClubs.Enabled = true;
                        ImageDiamonds.Enabled = true;
                        ImageHearts.Enabled = true;
                        ImageClubs.Visible = true;
                        ImageDiamonds.Visible = true;
                        ImageHearts.Visible = true;
                    }
                    if (koz == 1)
                    {
                        ImageSpades.Enabled = true;
                        ImageDiamonds.Enabled = true;
                        ImageHearts.Enabled = true;
                        ImageSpades.Visible = true;
                        ImageDiamonds.Visible = true;
                        ImageHearts.Visible = true;
                    }
                    if (koz == 2)
                    {
                        ImageSpades.Enabled = true;
                        ImageClubs.Enabled = true;
                        ImageHearts.Enabled = true;
                        ImageSpades.Visible = true;
                        ImageClubs.Visible = true;
                        ImageHearts.Visible = true;
                    }
                    if (koz == 3)
                    {
                        ImageSpades.Enabled = true;
                        ImageClubs.Enabled = true;
                        ImageDiamonds.Enabled = true;
                        ImageSpades.Visible = true;
                        ImageClubs.Visible = true;
                        ImageDiamonds.Visible = true;
                    }
                }
                if (razd1 == 1)
                {
                    Image2Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    LastAsk(1);
                    End2();
                }
                if (razd1 == 2)
                {
                    Image3Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    LastAsk(2);
                    End2();
                }
                if (razd1 == 3)
                {
                    Image4Play.Visible = true;
                    play = razd1;
                    if (play == 4) play = 0;
                    Image1p1.Visible = false;
                    Image2p1.Visible = false;
                    Image3p1.Visible = false;
                    Image4p1.Visible = false;
                    Image1p2.Visible = false;
                    Image2p2.Visible = false;
                    Image3p2.Visible = false;
                    Image4p2.Visible = false;
                    LastAsk(3);
                    End2();
                }

            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            Image1.Visible = false;
            Image2.Visible = false;
            Image3.Visible = false;
            Image4.Visible = false;
            canMod = true;
            NewGame();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button2.Enabled = false;
            for (i = 0; i < 24; i++)
                {
                    if (cards[i] == 0) { count[i] = 11; continue; }
                    if (cards[i] == 1) { count[i] = 4; continue; }
                    if (cards[i] == 2) { count[i] = 3; continue; }
                    if (cards[i] == 3) { count[i] = 2; continue; }
                    if (cards[i] == 4) { count[i] = 10; continue; }
                    if (cards[i] == 5) { count[i] = 0; continue; }
                    if (cards[i] == 6) { count[i] = 11; continue; }
                    if (cards[i] == 7) { count[i] = 4; continue; }
                    if (cards[i] == 8) { count[i] = 3; continue; }
                    if (cards[i] == 9) { count[i] = 2; continue; }
                    if (cards[i] == 10) { count[i] = 10; continue; }
                    if (cards[i] == 11) { count[i] = 0; continue; }
                    if (cards[i] == 12) { count[i] = 11; continue; }
                    if (cards[i] == 13) { count[i] = 4; continue; }
                    if (cards[i] == 14) { count[i] = 3; continue; }
                    if (cards[i] == 15) { count[i] = 2; continue; }
                    if (cards[i] == 16) { count[i] = 10; continue; }
                    if (cards[i] == 17) { count[i] = 0; continue; }
                    if (cards[i] == 18) { count[i] = 11; continue; }
                    if (cards[i] == 19) { count[i] = 4; continue; }
                    if (cards[i] == 20) { count[i] = 3; continue; }
                    if (cards[i] == 21) { count[i] = 2; continue; }
                    if (cards[i] == 22) { count[i] = 10; continue; }
                    if (cards[i] == 23) { count[i] = 0; continue; }
                }
            if (koz == 0)
                for (i = 0; i < 24; i++)
                {
                    if (cards[i] == 3) { count[i] = 20; continue; }
                    if (cards[i] == 5) { count[i] = 14; continue; }
                }
            if (koz == 1)
                for (i = 0; i < 24; i++)
                {
                    if (cards[i] == 9) { count[i] = 20; continue; }
                    if (cards[i] == 11) { count[i] = 14; continue; }
                }
            if (koz == 2)
                for (i = 0; i < 24; i++)
                {
                    if (cards[i] == 15) { count[i] = 20; continue; }
                    if (cards[i] == 17) { count[i] = 14; continue; }
                }
            if (koz == 3)
                for (i = 0; i < 24; i++)
                {
                    if (cards[i] == 21) { count[i] = 20; continue; }
                    if (cards[i] == 23) { count[i] = 14; continue; }
                }
               
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (card4[3] != -1) Refresh1();
            s = 0;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 6; j++)
                    if (imass[i, j] == 110)
                        s++;
            if (s == 24) timer1.Enabled = false;
            if ((level == 24)||(s == 24))
            {
                EndForever();
                if ((hits[0] >= 1005) && (hits[0] > hits[1]) && (hits[0] > hits[2]) && (hits[0] > hits[3]))
                {
                    MessageBox.Show("Игрок " + name +"  победил.");
                    StartButton.Visible = true;
                    for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
                    level = 0;
                    sdach = 0;
                    razd = 0;
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    for (i = 0; i < 4; i++)
                        bella[i] = 0;
                }
                if ((hits[1] >= 1005) && (hits[1] > hits[0]) && (hits[1] > hits[2]) && (hits[1] > hits[3]))
                {
                    MessageBox.Show("Игрок Балбес победил.");
                    StartButton.Visible = true;
                    for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
                    level = 0;
                    sdach = 0;
                    razd = 0;
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    for (i = 0; i < 4; i++)
                        bella[i] = 0;
                }
                if ((hits[2] >= 1005) && (hits[2] > hits[1]) && (hits[2] > hits[0]) && (hits[2] > hits[3]))
                {
                    MessageBox.Show("Игрок Бывалый победил.");
                    StartButton.Visible = true;
                    for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
                    level = 0;
                    sdach = 0;
                    razd = 0;
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    for (i = 0; i < 4; i++)
                        bella[i] = 0;
                }
                if ((hits[3] >= 1005) && (hits[3] > hits[1]) && (hits[3] > hits[2]) && (hits[3] > hits[0]))
                {
                    MessageBox.Show("Игрок Трус победил.");
                    StartButton.Visible = true;
                    for (i = 0; i < 4; i++) { hits[i] = 0; bolt[i] = 0; xb1[i] = false; }
                    level = 0;
                    sdach = 0;
                    razd = 0;
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    richTextBox4.Clear();
                    for (i = 0; i < 4; i++)
                        bella[i] = 0;
                }
            }
            if (play == 0) { hod = true; timer1.Enabled = false; return; }
            if (card4[3] != -1) Refresh1();
            if (play == 1) { PlayCard(1); play++; return; }
            if (card4[3] != -1) Refresh1();
            if (play == 2) { PlayCard(2); play++; return; }
            if (card4[3] != -1) Refresh1();
            if (play == 3) { PlayCard(3); play = 0; return; }
            if (card4[3] != -1) Refresh1();
        }

        public void Refresh1()
        {
            int temp = 0;
            int tempnum=0;
            y1 = card4[0];
            y2 = card4[1];
            y3 = card4[2];
            y4 = card4[3];
            z1 = y1; z2 = y2; z3 = y3; z4 = y4;
            k1 = k2 = k3 = k4 = 0;
            while (z1 > 5)
            {
                z1 -= 6;
                k1++;
            }
            while (z2 > 5)
            {
                z2 -= 6;
                k2++;
            }
            while (z3 > 5)
            {
                z3 -= 6;
                k3++;
            }
            while (z4 > 5)
            {
                z4 -= 6;
                k4++;
            }
            for (i = 0; i < 4; i++)
                if (k1 == i) { ccard[i,col[i]] = z1; col[i]++; }
            for (i = 0; i < 4; i++)
                if (k2 == i) { ccard[i,col[i]] = z2; col[i]++; }
            for (i = 0; i < 4; i++)
                if (k3 == i) { ccard[i,col[i]] = z3; col[i]++; }
            for (i = 0; i < 4; i++)
                if (k4 == i) { ccard[i,col[i]] = z4; col[i]++; }

            for (i = 0; i < 4; i++)
                count1[i] = 0;
            if (koz == 0)
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count1[i] = 110; continue; }
                    if (card4[i] == 1) { count1[i] = 40; continue; }
                    if (card4[i] == 2) { count1[i] = 30; continue; }
                    if (card4[i] == 3) { count1[i] = 200; continue; }
                    if (card4[i] == 4) { count1[i] = 100; continue; }
                    if (card4[i] == 5) { count1[i] = 140; continue; }
                }
            if (koz == 1)
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 6) { count1[i] = 110; continue; }
                    if (card4[i] == 7) { count1[i] = 40; continue; }
                    if (card4[i] == 8) { count1[i] = 30; continue; }
                    if (card4[i] == 9) { count1[i] = 200; continue; }
                    if (card4[i] == 10) { count1[i] = 100; continue; }
                    if (card4[i] == 11) { count1[i] = 140; continue; }
                }
            if (koz == 2)
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 12) { count1[i] = 110; continue; }
                    if (card4[i] == 13) { count1[i] = 40; continue; }
                    if (card4[i] == 14) { count1[i] = 30; continue; }
                    if (card4[i] == 15) { count1[i] = 200; continue; }
                    if (card4[i] == 16) { count1[i] = 100; continue; }
                    if (card4[i] == 17) { count1[i] = 140; continue; }
                }
            if (koz == 3)
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 18) { count1[i] = 110; continue; }
                    if (card4[i] == 19) { count1[i] = 40; continue; }
                    if (card4[i] == 20) { count1[i] = 30; continue; }
                    if (card4[i] == 21) { count1[i] = 200; continue; }
                    if (card4[i] == 22) { count1[i] = 100; continue; }
                    if (card4[i] == 23) { count1[i] = 140; continue; }
                }
            if ((k1 == 0) && (koz != 0))
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count2[i] = 11; continue; }
                    if (card4[i] == 1) { count2[i] = 4; continue; }
                    if (card4[i] == 2) { count2[i] = 3; continue; }
                    if (card4[i] == 3) { count2[i] = 2; continue; }
                    if (card4[i] == 4) { count2[i] = 10; continue; }
                    if (card4[i] == 5) { count2[i] = 1; continue; }
                    if (card4[i] == 6) { count2[i] = 0; continue; }
                    if (card4[i] == 7) { count2[i] = 0; continue; }
                    if (card4[i] == 8) { count2[i] = 0; continue; }
                    if (card4[i] == 9) { count2[i] = 0; continue; }
                    if (card4[i] == 10) { count2[i] = 0; continue; }
                    if (card4[i] == 11) { count2[i] = 0; continue; }
                    if (card4[i] == 12) { count2[i] = 0; continue; }
                    if (card4[i] == 13) { count2[i] = 0; continue; }
                    if (card4[i] == 14) { count2[i] = 0; continue; }
                    if (card4[i] == 15) { count2[i] = 0; continue; }
                    if (card4[i] == 16) { count2[i] = 0; continue; }
                    if (card4[i] == 17) { count2[i] = 0; continue; }
                    if (card4[i] == 18) { count2[i] = 0; continue; }
                    if (card4[i] == 19) { count2[i] = 0; continue; }
                    if (card4[i] == 20) { count2[i] = 0; continue; }
                    if (card4[i] == 21) { count2[i] = 0; continue; }
                    if (card4[i] == 22) { count2[i] = 0; continue; }
                    if (card4[i] == 23) { count2[i] = 0; continue; }
                }
            if ((k1 == 1) && (koz != 1))
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count2[i] = 0; continue; }
                    if (card4[i] == 1) { count2[i] = 0; continue; }
                    if (card4[i] == 2) { count2[i] = 0; continue; }
                    if (card4[i] == 3) { count2[i] = 0; continue; }
                    if (card4[i] == 4) { count2[i] = 0; continue; }
                    if (card4[i] == 5) { count2[i] = 0; continue; }
                    if (card4[i] == 6) { count2[i] = 11; continue; }
                    if (card4[i] == 7) { count2[i] = 4; continue; }
                    if (card4[i] == 8) { count2[i] = 3; continue; }
                    if (card4[i] == 9) { count2[i] = 2; continue; }
                    if (card4[i] == 10) { count2[i] = 10; continue; }
                    if (card4[i] == 11) { count2[i] = 1; continue; }
                    if (card4[i] == 12) { count2[i] = 0; continue; }
                    if (card4[i] == 13) { count2[i] = 0; continue; }
                    if (card4[i] == 14) { count2[i] = 0; continue; }
                    if (card4[i] == 15) { count2[i] = 0; continue; }
                    if (card4[i] == 16) { count2[i] = 0; continue; }
                    if (card4[i] == 17) { count2[i] = 0; continue; }
                    if (card4[i] == 18) { count2[i] = 0; continue; }
                    if (card4[i] == 19) { count2[i] = 0; continue; }
                    if (card4[i] == 20) { count2[i] = 0; continue; }
                    if (card4[i] == 21) { count2[i] = 0; continue; }
                    if (card4[i] == 22) { count2[i] = 0; continue; }
                    if (card4[i] == 23) { count2[i] = 0; continue; }
                }
            if ((k1 == 2) && (koz != 2))
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count2[i] = 0; continue; }
                    if (card4[i] == 1) { count2[i] = 0; continue; }
                    if (card4[i] == 2) { count2[i] = 0; continue; }
                    if (card4[i] == 3) { count2[i] = 0; continue; }
                    if (card4[i] == 4) { count2[i] = 0; continue; }
                    if (card4[i] == 5) { count2[i] = 0; continue; }
                    if (card4[i] == 6) { count2[i] = 0; continue; }
                    if (card4[i] == 7) { count2[i] = 0; continue; }
                    if (card4[i] == 8) { count2[i] = 0; continue; }
                    if (card4[i] == 9) { count2[i] = 0; continue; }
                    if (card4[i] == 10) { count2[i] = 0; continue; }
                    if (card4[i] == 11) { count2[i] = 0; continue; }
                    if (card4[i] == 12) { count2[i] = 11; continue; }
                    if (card4[i] == 13) { count2[i] = 4; continue; }
                    if (card4[i] == 14) { count2[i] = 3; continue; }
                    if (card4[i] == 15) { count2[i] = 2; continue; }
                    if (card4[i] == 16) { count2[i] = 10; continue; }
                    if (card4[i] == 17) { count2[i] = 1; continue; }
                    if (card4[i] == 18) { count2[i] = 0; continue; }
                    if (card4[i] == 19) { count2[i] = 0; continue; }
                    if (card4[i] == 20) { count2[i] = 0; continue; }
                    if (card4[i] == 21) { count2[i] = 0; continue; }
                    if (card4[i] == 22) { count2[i] = 0; continue; }
                    if (card4[i] == 23) { count2[i] = 0; continue; }
                }
            if ((k1 == 3) && (koz != 3))
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count2[i] = 0; continue; }
                    if (card4[i] == 1) { count2[i] = 0; continue; }
                    if (card4[i] == 2) { count2[i] = 0; continue; }
                    if (card4[i] == 3) { count2[i] = 0; continue; }
                    if (card4[i] == 4) { count2[i] = 0; continue; }
                    if (card4[i] == 5) { count2[i] = 0; continue; }
                    if (card4[i] == 6) { count2[i] = 0; continue; }
                    if (card4[i] == 7) { count2[i] = 0; continue; }
                    if (card4[i] == 8) { count2[i] = 0; continue; }
                    if (card4[i] == 9) { count2[i] = 0; continue; }
                    if (card4[i] == 10) { count2[i] = 0; continue; }
                    if (card4[i] == 11) { count2[i] = 0; continue; }
                    if (card4[i] == 12) { count2[i] = 0; continue; }
                    if (card4[i] == 13) { count2[i] = 0; continue; }
                    if (card4[i] == 14) { count2[i] = 0; continue; }
                    if (card4[i] == 15) { count2[i] = 0; continue; }
                    if (card4[i] == 16) { count2[i] = 0; continue; }
                    if (card4[i] == 17) { count2[i] = 0; continue; }
                    if (card4[i] == 18) { count2[i] = 11; continue; }
                    if (card4[i] == 19) { count2[i] = 4; continue; }
                    if (card4[i] == 20) { count2[i] = 3; continue; }
                    if (card4[i] == 21) { count2[i] = 2; continue; }
                    if (card4[i] == 22) { count2[i] = 10; continue; }
                    if (card4[i] == 23) { count2[i] = 1; continue; }
                }
            if ((k1 == 0) && (koz == 0))
                for (i = 0; i < 4; i++)
                {
                    if (card4[i] == 0) { count2[i] = 0; continue; }
                    if (card4[i] == 1) { count2[i] = 0; continue; }
                    if (card4[i] == 2) { count2[i] = 0; continue; }
                    if (card4[i] == 3) { count2[i] = 0; continue; }
                    if (card4[i] == 4) { count2[i] = 0; continue; }
                    if (card4[i] == 5) { count2[i] = 0; continue; }
                    if (card4[i] == 6) { count2[i] = 0; continue; }
                    if (card4[i] == 7) { count2[i] = 0; continue; }
                    if (card4[i] == 8) { count2[i] = 0; continue; }
                    if (card4[i] == 9) { count2[i] = 0; continue; }
                    if (card4[i] == 10) { count2[i] = 0; continue; }
                    if (card4[i] == 11) { count2[i] = 0; continue; }
                    if (card4[i] == 12) { count2[i] = 0; continue; }
                    if (card4[i] == 13) { count2[i] = 0; continue; }
                    if (card4[i] == 14) { count2[i] = 0; continue; }
                    if (card4[i] == 15) { count2[i] = 0; continue; }
                    if (card4[i] == 16) { count2[i] = 0; continue; }
                    if (card4[i] == 17) { count2[i] = 0; continue; }
                    if (card4[i] == 18) { count2[i] = 0; continue; }
                    if (card4[i] == 19) { count2[i] = 0; continue; }
                    if (card4[i] == 20) { count2[i] = 0; continue; }
                    if (card4[i] == 21) { count2[i] = 0; continue; }
                    if (card4[i] == 22) { count2[i] = 0; continue; }
                    if (card4[i] == 23) { count2[i] = 0; continue; }
                }
            Image1.Visible = false; //Image= imageList1.Images[31];
            Image2.Visible = false; //Image = imageList1.Images[31];
            Image3.Visible = false; //Image = imageList1.Images[31];
            Image4.Visible = false; //Image = imageList1.Images[31];
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 24; j++)
                    if (cards[j] == card4[i]) tempnum = j;
                temp += count[tempnum];
            }
            this.Refresh();
            for (i = 0; i < 4; i++) count1[i] += count2[i];
            int k;
            for (i = 0; i < 4; i++)
                card4[i] = -1;
            if ((((z1 == 1) || (z1 == 2)) && (k1 == koz)))
                if ((count1[0] > count1[1]) && (count1[0] > count1[2]) && (count1[0] > count1[3]))
                { takenHits[play] += temp; level += 4; bella[play]++; return; }
            if ((count1[0] > count1[1]) && (count1[0] > count1[2]) && (count1[0] > count1[3]))
            { takenHits[play] += temp; level += 4; return; }
            if ((((z2 == 1) || (z2 == 2)) && (k2 == koz)))
                if ((count1[1] > count1[0]) && (count1[1] > count1[2]) && (count1[1] > count1[3]))
                {
                    play += 1; if (play >= 4) play -= 4; takenHits[play] += temp; level += 4; k = play + 1;
                    if (k >= 4) k -= 4; bella[k]++; return;
                }
            if ((count1[1] > count1[0]) && (count1[1] > count1[2]) && (count1[1] > count1[3]))
            { play += 1; if (play >= 4)play -= 4; takenHits[play] += temp; level += 4; return; }
            if ((((z3 == 1) || (z3 == 2)) && (k3 == koz)))
                if ((count1[2] > count1[1]) && (count1[2] > count1[0]) && (count1[2] > count1[3]))
                {
                    play += 2; if (play >= 4) play -= 4; takenHits[play] += temp; level += 4; k = play + 2;
                    if (k >= 4) k -= 4; bella[k]++; return;
                }
            if ((count1[2] > count1[1]) && (count1[2] > count1[0]) && (count1[2] > count1[3]))
            { play += 2; if (play >= 4)play -= 4; takenHits[play] += temp; level += 4; return; }
            if ((((z4 == 1) || (z4 == 2)) && (k4 == koz)))
                if ((count1[3] > count1[1]) && (count1[3] > count1[2]) && (count1[3] > count1[0]))
                {
                    play += 3; if (play >= 4) play -= 4; if (play >= 4) play -= 4; takenHits[play] += temp; level += 4; k = play + 3;
                    if (k >= 4) k -= 4; bella[k]++; return;
                }
            if ((count1[3] > count1[1]) && (count1[3] > count1[2]) && (count1[3] > count1[0]))
            { play += 3; if (play >= 4)play -= 4; if (play >= 4)play -= 4; takenHits[play] += temp; level += 4; return; }
        }

        public void PlayCard(int t)
        {
            int colcard=6;
            int[] col1 = new int[4];
            int[] p = new int [6];
           for(i=0;i<4;i++)col1[i]=0;
            for(i=0;i<6;i++) p[i] = 0;
           k1=k2=k3=k4=k5=k6=kk1=kk2=kk3=kk4=0;
           y1 = imass[t,0]; if (y1 == 110) { y1 = -1; k1 = -1; p[0]=-1; colcard--; }
           y2 = imass[t,1]; if (y2 == 110) { y2 = -1; k2 = -1; p[1]=-1; colcard--; }
           y3 = imass[t,2]; if (y3 == 110) { y3 = -1; k3 = -1; p[2]=-1; colcard--; }
           y4 = imass[t,3]; if (y4 == 110) { y4 = -1; k4 = -1; p[3]=-1; colcard--; }
           y5 = imass[t,4]; if (y5 == 110) { y5 = -1; k5 = -1; p[4]=-1; colcard--; }
           y6 = imass[t,5]; if (y6 == 110) { y6 = -1; k6 = -1; p[5]=-1; colcard--; }
           yy1=card4[0];
           yy2=card4[1];
           yy3=card4[2];
           yy4=card4[3];
           z1=y1;
           z2=y2;
           z3=y3;
           z4=y4;
           z5=y5;
           z6=y6;
           zz1=yy1;
           zz2=yy2;
           zz3=yy3;
           zz4=yy4;
           while(z1>5)
           {
              z1-=6;
              k1++;
           }
           while(z2>5)
           {
              z2-=6;
              k2++;
           }
           while(z3>5)
           {
              z3-=6;
              k3++;
           }
           while(z4>5)
           {
              z4-=6;
              k4++;
           }
           while(z5>5)
           {
              z5-=6;
              k5++;
           }
           while(z6>5)
           {
              z6-=6;
              k6++;
           }
            while(zz1>5)
           {
              zz1-=6;
              kk1++;
           }
            while(zz2>5)
           {
              zz2-=6;
              kk2++;
           }
            while(zz3>5)
           {
              zz3-=6;
              kk3++;
           }
            while(zz4>5)
           {
              zz4-=6;
              kk4++;
           }
           for(i=0;i<4;i++)
           if(k1==i)col1[i]++;
           for(i=0;i<4;i++)
           if(k2==i)col1[i]++;
           for(i=0;i<4;i++)
           if(k3==i)col1[i]++;
           for(i=0;i<4;i++)
           if(k4==i)col1[i]++;
           for(i=0;i<4;i++)
           if(k5==i)col1[i]++;
           for(i=0;i<4;i++)
           if(k6==i)col1[i]++;
           int fs = 0;
           int nfs = -1;
           for (i = 0; i < 6; i++)
           {
               if (p[i] == -1) fs++;
               if (p[i] == 0) nfs = i;
           }

            if(fs==5) 
                for(i=0;i<4;i++)
                    if (card4[i] == -1) { card4[i] = imass[t,nfs]; Replace(i, imass[t,nfs]); return;}

           i = 0;//1-я карта
           if (card4[i] == -1)
           {
               if (col[koz] < 6 - col1[koz])
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //Валет коз
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //дама коз
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //король коз
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //10 коз
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //T коз
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //9 коз
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //туз
               if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //10
               if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //9
               if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //валет
               if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //дама
               if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //король
               if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
               if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
               if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
               if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
               if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }

               if ((col1[koz] == 6 - col[koz]) && (col1[koz] == level / 4))
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //Валет коз
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //дама коз
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //король коз
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //10 коз
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //9 коз
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //T коз
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if (col1[koz] == colcard)
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //Валет коз
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //дама коз
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //король коз
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //10 коз
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //9 коз
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; } //T коз
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
           }
           i++;//2-я карта
           if (card4[i] == -1)
           {
               if ((zz1 == 0) && (kk1 != koz)) //туз
               {
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 != koz))      //9
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 != koz))         //В
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 != koz))            //Д
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 != koz))     //К
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 != koz))          //10
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 == koz))   //В коз
               {
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 == koz))          //9 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 0) && (kk1 == koz))  //Т коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1) && (k3 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 == koz))         //10 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 == koz))    //Д коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 == koz))       //К коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
           }
           i++;  //3-я карта
           if (card4[i] == -1)
           {
               if ((zz1 == 0) && (kk1 != koz)) //туз
               {
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 != koz))      //9
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 != koz))         //В
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 != koz))            //Д
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 != koz))     //К
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 != koz))          //10
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 == koz))   //В коз
               {
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 == koz))          //9 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 0) && (kk1 == koz))  //Т коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1) && (k3 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 == koz))         //10 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 == koz))    //Д коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 == koz))       //К коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
           }
           i++;//4-я карта
           if (card4[i] == -1)
           {
               if ((zz1 == 0) && (kk1 != koz)) //туз
               {
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 != koz))      //9
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 != koz))         //В
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 != koz))            //Д
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 != koz))     //К
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 != koz))          //10
               {
                   if ((z1 == 0) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == kk1) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == kk1) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == kk1) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == kk1) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == kk1) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == kk1) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != kk1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != kk1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != kk1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != kk1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != kk1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != kk1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 3) && (kk1 == koz))   //В коз
               {
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 5) && (kk1 == koz))          //9 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 0) && (kk1 == koz))  //Т коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1) && (k3 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 4) && (kk1 == koz))         //10 коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 2) && (kk1 == koz))    //Д коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
               if ((zz1 == 1) && (kk1 == koz))       //К коз
               {
                   if ((z1 == 3) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 == koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 == koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 == koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 == koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 == koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 == koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 5) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 5) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 5) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 5) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 5) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 5) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 3) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 3) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 3) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 3) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 3) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 3) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 2) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 2) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 2) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 2) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 2) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 2) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 1) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 1) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 1) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 1) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 1) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 1) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 4) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 4) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 4) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 4) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 4) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 4) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
                   if ((z1 == 0) && (k1 != koz) && (k1 != -1)) { card4[i] = y1; Replace(i, y1); return; }
                   if ((z2 == 0) && (k2 != koz) && (k2 != -1)) { card4[i] = y2; Replace(i, y2); return; }
                   if ((z3 == 0) && (k3 != koz) && (k3 != -1)) { card4[i] = y3; Replace(i, y3); return; }
                   if ((z4 == 0) && (k4 != koz) && (k4 != -1)) { card4[i] = y4; Replace(i, y4); return; }
                   if ((z5 == 0) && (k5 != koz) && (k5 != -1)) { card4[i] = y5; Replace(i, y5); return; }
                   if ((z6 == 0) && (k6 != koz) && (k6 != -1)) { card4[i] = y6; Replace(i, y6); return; }
               }
           }
        }

        public void Replace(int n, int num)
        {
            int temp = 0;
            for (i = 0; i < 6; i++)
                if (imass[play,i] == num) { temp = i; continue; }
            if (n == 0)
            {
                if (rub1 == 0)
                    Image1.Image = imageList1.Images[imass[play, temp]];
                else
                    Image1.Image = imageList1.Images[8 + rub1 * 24 + imass[play, temp]];
                Image1.Visible = true;
                if (play == 1)
                {
                    if (temp == 0) pictureBox7.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox8.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox9.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox10.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox11.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox12.Image = imageList1.Images[31];
                }
                if (play == 2)
                {
                    if (temp == 0) pictureBox13.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox14.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox15.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox16.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox17.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox18.Image = imageList1.Images[31];
                }
                if (play == 3)
                {
                    if (temp == 0) pictureBox19.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox20.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox21.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox22.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox23.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox24.Image = imageList1.Images[31];
                }
                imass[play,temp] = 110;
                this.Refresh();
                return;
            }
            if (n == 1)
            {
                if (rub1 == 0)
                    Image2.Image = imageList1.Images[imass[play, temp]];
                else
                    Image2.Image = imageList1.Images[8 + rub1 * 24 + imass[play, temp]];
                Image2.Visible = true;
                if (play == 1)
                {
                    if (temp == 0) pictureBox7.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox8.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox9.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox10.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox11.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox12.Image = imageList1.Images[31];
                }
                if (play == 2)
                {
                    if (temp == 0) pictureBox13.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox14.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox15.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox16.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox17.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox18.Image = imageList1.Images[31];
                }
                if (play == 3)
                {
                    if (temp == 0) pictureBox19.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox20.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox21.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox22.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox23.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox24.Image = imageList1.Images[31];
                }
                imass[play,temp] = 110;
                this.Refresh();
                return;
            }
            if (n == 2)
            {
                if (rub1 == 0)
                    Image3.Image = imageList1.Images[imass[play, temp]];
                else
                    Image3.Image = imageList1.Images[8 + rub1 * 24 + imass[play, temp]];
                Image3.Visible = true;
                if (play == 1)
                {
                    if (temp == 0) pictureBox7.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox8.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox9.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox10.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox11.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox12.Image = imageList1.Images[31];
                }
                if (play == 2)
                {
                    if (temp == 0) pictureBox13.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox14.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox15.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox16.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox17.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox18.Image = imageList1.Images[31];
                }
                if (play == 3)
                {
                    if (temp == 0) pictureBox19.Image = imageList1.Images[31];
                    if (temp == 1) pictureBox20.Image = imageList1.Images[31];
                    if (temp == 2) pictureBox21.Image = imageList1.Images[31];
                    if (temp == 3) pictureBox22.Image = imageList1.Images[31];
                    if (temp == 4) pictureBox23.Image = imageList1.Images[31];
                    if (temp == 5) pictureBox24.Image = imageList1.Images[31];
                }
                imass[play,temp] = 110;
                this.Refresh();
                return;
            }
            if (n == 3)
            {
                if (rub1 == 0)
                    Image4.Image = imageList1.Images[imass[play, temp]];
                else
                    Image4.Image = imageList1.Images[8 + rub1 * 24 + imass[play, temp]];
                Image4.Visible = true;
                if (play == 1)
                {
                    if (temp == 0) pictureBox7.Visible = false;
                    if (temp == 1) pictureBox8.Visible = false;
                    if (temp == 2) pictureBox9.Visible = false;
                    if (temp == 3) pictureBox10.Visible = false;
                    if (temp == 4) pictureBox11.Visible = false;
                    if (temp == 5) pictureBox12.Visible = false;
                }
                if (play == 2)
                {
                    if (temp == 0) pictureBox13.Visible = false;
                    if (temp == 1) pictureBox14.Visible = false;
                    if (temp == 2) pictureBox15.Visible = false;
                    if (temp == 3) pictureBox16.Visible = false;
                    if (temp == 4) pictureBox17.Visible = false;
                    if (temp == 5) pictureBox18.Visible = false;
                }
                if (play == 3)
                {
                    if (temp == 0) pictureBox19.Visible = false;
                    if (temp == 1) pictureBox20.Visible = false;
                    if (temp == 2) pictureBox21.Visible = false;
                    if (temp == 3) pictureBox22.Visible = false;
                    if (temp == 4) pictureBox23.Visible = false;
                    if (temp == 5) pictureBox24.Visible = false;
                }
                imass[play,temp] = 110;
                this.Refresh();
                return;
            }
        }

        public void EndForever()
        {
            canMod = false;
            string xb = "";
            ImageTrump.Visible = false;
            Image1Play.Visible = false;
            Image2Play.Visible = false;
            Image3Play.Visible = false;
            Image4Play.Visible = false;
            sdach++;
            takenHits[play] += 10;
            for (i = 0; i < 4; i++)
            {
                if (takenHits[i] > 0) takenHits[i] += hitsTemp[i];
                if (bella[i] == 2) takenHits[i] += 20;
            }
            for (i = 0; i < 4; i++)
                if (takenHits[i] == 0) { xb1[i] = true; hits[i] -= 100; }
            if (player == 0)
            {
                if ((takenHits[player] <= takenHits[1]) || (takenHits[player] <= takenHits[2]) ||
                   (takenHits[player] <= takenHits[3]))
                {
                    if ((takenHits[1] >= takenHits[2]) && (takenHits[1] >= takenHits[player]) &&
                       (takenHits[1] >= takenHits[3]))
                        takenHits[1] += takenHits[player];
                    if ((takenHits[2] >= takenHits[player]) && (takenHits[2] >= takenHits[1]) &&
                       (takenHits[2] >= takenHits[3]))
                        takenHits[2] += takenHits[player];
                    if ((takenHits[3] >= takenHits[2]) && (takenHits[3] >= takenHits[player]) &&
                       (takenHits[3] >= takenHits[1]))
                        takenHits[3] += takenHits[player];
                    takenHits[player] = 0;
                    for (i = 0; i < 4; i++)
                        hits[i] += takenHits[i];
                    if (xb1[player] == true) xb = "хв";
                    bolt[player]++;
                    if (bolt[player] < 3)
                        richTextBox1.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    else
                    {
                        hits[player] -= 100;
                        richTextBox1.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    }
                    xb = "";
                    if (xb1[1] == true) xb = "хв";
                    richTextBox2.AppendText(xb + Convert.ToString(hits[1]) + "\r\n");
                    xb = "";
                    if (xb1[2] == true) xb = "хв";
                    richTextBox3.AppendText(xb + Convert.ToString(hits[2]) + "\r\n");
                    xb = "";
                    if (xb1[3] == true) xb = "хв";
                    richTextBox4.AppendText(xb + Convert.ToString(hits[3]) + "\r\n");
                    xb = ""; level = 0; StartButton.Visible = true;
                    richTextBox1.ScrollToCaret();
                    richTextBox2.ScrollToCaret();
                    richTextBox3.ScrollToCaret();
                    richTextBox4.ScrollToCaret();
                    return;
                }
            }
            if (player == 1)
            {
                if ((takenHits[player] <= takenHits[0]) || (takenHits[player] <= takenHits[2]) ||
                   (takenHits[player] <= takenHits[3]))
                {
                    if ((takenHits[0] >= takenHits[2]) && (takenHits[0] >= takenHits[player]) &&
                       (takenHits[0] >= takenHits[3]))
                        takenHits[0] += takenHits[player];
                    if ((takenHits[2] >= takenHits[player]) && (takenHits[2] >= takenHits[0]) &&
                       (takenHits[2] >= takenHits[3]))
                        takenHits[2] += takenHits[player];
                    if ((takenHits[3] >= takenHits[2]) && (takenHits[3] >= takenHits[player]) &&
                       (takenHits[3] >= takenHits[0]))
                        takenHits[3] += takenHits[player];
                    takenHits[player] = 0;
                    bolt[player]++;
                    for (i = 0; i < 4; i++)
                        hits[i] += takenHits[i];
                    if (xb1[player] == true) xb = "хв";
                    if (bolt[player] < 2)
                        richTextBox2.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    else
                    {
                        hits[player] -= 100;
                        richTextBox2.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    }
                    xb = "";
                    if (xb1[0] == true) xb = "хв";
                    richTextBox1.AppendText(xb + Convert.ToString(hits[0]) + "\r\n");
                    xb = "";
                    if (xb1[2] == true) xb = "хв";
                    richTextBox3.AppendText(xb + Convert.ToString(hits[2]) + "\r\n");
                    xb = "";
                    if (xb1[3] == true) xb = "хв";
                    richTextBox4.AppendText(xb + Convert.ToString(hits[3]) + "\r\n");
                    xb = "";
                    level = 0; StartButton.Visible = true;
                    richTextBox1.ScrollToCaret();
                    richTextBox2.ScrollToCaret();
                    richTextBox3.ScrollToCaret();
                    richTextBox4.ScrollToCaret();
                    return;
                }
            }
            if (player == 2)
            {
                if ((takenHits[player] <= takenHits[0]) || (takenHits[player] <= takenHits[1]) ||
                   (takenHits[player] <= takenHits[3]))
                {
                    if ((takenHits[0] >= takenHits[1]) && (takenHits[0] >= takenHits[player]) &&
                       (takenHits[0] >= takenHits[3]))
                        takenHits[0] += takenHits[player];
                    if ((takenHits[1] >= takenHits[player]) && (takenHits[1] >= takenHits[0]) &&
                       (takenHits[1] >= takenHits[3]))
                        takenHits[1] += takenHits[player];
                    if ((takenHits[3] >= takenHits[1]) && (takenHits[3] >= takenHits[player]) &&
                       (takenHits[3] >= takenHits[0]))
                        takenHits[3] += takenHits[player];
                    takenHits[player] = 0;
                    bolt[player]++;
                    for (i = 0; i < 4; i++)
                        hits[i] += takenHits[i];
                    if (xb1[player] == true) xb = "хв";
                    if (bolt[player] < 3)
                        richTextBox3.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    else
                    {
                        hits[player] -= 100;
                        richTextBox3.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    }
                    xb = "";
                    if (xb1[1] == true) xb = "хв";
                    richTextBox2.AppendText(xb + Convert.ToString(hits[1]) + "\r\n");
                    xb = "";
                    if (xb1[0] == true) xb = "хв";
                    richTextBox1.AppendText(xb + Convert.ToString(hits[0]) + "\r\n");
                    xb = "";
                    if (xb1[3] == true) xb = "хв";
                    richTextBox4.AppendText(xb + Convert.ToString(hits[3]) + "\r\n");
                    xb = "";
                    level = 0; StartButton.Visible = true;
                    richTextBox1.ScrollToCaret();
                    richTextBox2.ScrollToCaret();
                    richTextBox3.ScrollToCaret();
                    richTextBox4.ScrollToCaret();
                    return;
                }
            }
            if (player == 3)
            {
                if ((takenHits[player] <= takenHits[0]) || (takenHits[player] <= takenHits[1]) ||
                   (takenHits[player] <= takenHits[2]))
                {
                    if ((takenHits[0] >= takenHits[1]) && (takenHits[0] >= takenHits[player]) &&
                       (takenHits[0] >= takenHits[2]))
                        takenHits[0] += takenHits[player];
                    if ((takenHits[1] >= takenHits[player]) && (takenHits[1] >= takenHits[0]) &&
                       (takenHits[1] >= takenHits[2]))
                        takenHits[1] += takenHits[player];
                    if ((takenHits[2] >= takenHits[1]) && (takenHits[2] >= takenHits[player]) &&
                       (takenHits[2] >= takenHits[0]))
                        takenHits[2] += takenHits[player];
                    takenHits[player] = 0;
                    bolt[player]++;
                    for (i = 0; i < 4; i++)
                        hits[i] += takenHits[i];
                    if (xb1[player] == true) xb = "хв";
                    if (bolt[player] < 3)
                        richTextBox4.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    else
                    {
                        hits[player] -= 100;
                        richTextBox4.AppendText(xb + "Б" + Convert.ToString(bolt[player]) + " " + Convert.ToString(hits[player]) + "\r\n");
                    }
                    xb = "";
                    if (xb1[1] == true) xb = "хв";
                    richTextBox2.AppendText(xb + Convert.ToString(hits[1]) + "\r\n");
                    xb = "";
                    if (xb1[2] == true) xb = "хв";
                    richTextBox3.AppendText(xb + Convert.ToString(hits[2]) + "\r\n");
                    xb = "";
                    if (xb1[0] == true) xb = "хв";
                    richTextBox1.AppendText(xb + Convert.ToString(hits[0]) + "\r\n");
                    xb = "";
                    level = 0; StartButton.Visible = true;
                    richTextBox1.ScrollToCaret();
                    richTextBox2.ScrollToCaret();
                    richTextBox3.ScrollToCaret();
                    richTextBox4.ScrollToCaret();
                    return;
                }
            }
            for (i = 0; i < 4; i++)
                hits[i] += takenHits[i];
            xb = "";
            if (xb1[1] == true)
            {
                xb = "хв";
                richTextBox2.AppendText(xb + Convert.ToString(hits[1]) + "\r\n");
            }
            else richTextBox2.AppendText(Convert.ToString(hits[1]) + "\r\n");
            xb = "";
            if (xb1[2] == true)
            {
                xb = "хв";
                richTextBox3.AppendText(xb + Convert.ToString(hits[2]) + "\r\n");
            }
            else richTextBox3.AppendText(Convert.ToString(hits[2]) + "\r\n");
            xb = "";
            if (xb1[0] == true)
            {
                xb = "хв";
                richTextBox1.AppendText(xb + Convert.ToString(hits[0]) + "\r\n");
            }
            else richTextBox1.AppendText(Convert.ToString(hits[0]) + "\r\n");
            xb = "";
            if (xb1[3] == true)
            {
                xb = "хв";
                richTextBox4.AppendText(xb + Convert.ToString(hits[3]) + "\r\n");
            }
            else richTextBox4.AppendText(Convert.ToString(hits[3]) + "\r\n");
            xb = "";
            level = 0;
            StartButton.Visible = true;
            richTextBox1.ScrollToCaret();
            richTextBox2.ScrollToCaret();
            richTextBox3.ScrollToCaret();
            richTextBox4.ScrollToCaret();
            return;
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form2 frmSecond = new Form2();
            frmSecond.Show(this); 
        }

        private void вОткрытуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (open)
            {
                вОткрытуюToolStripMenuItem.Checked = false;
                open = false;
                for (i = 0; i < 4; i++)
                    for (j = 0; j < 6; j++)
                    {
                        if ((imass[i, j] != 110)&&canMod)
                        {
                            if ((open || i == 0) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                            if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8 + rub1 * 24 + imass[i, j]];
                            if (!open && i != 0) pmass[i, j].Image = imageList1.Images[128 + rub];
                        }
                    }
            }
            else
            {
                вОткрытуюToolStripMenuItem.Checked = true;
                open = true;
                for (i = 0; i < 4; i++)
                    for (j = 0; j < 6; j++)
                    {
                        if ((imass[i, j] != 110)&&canMod)
                        {
                            if ((open || i == 0) && rub1 == 0) pmass[i, j].Image = imageList1.Images[imass[i, j]];
                            if ((open || i == 0) && rub1 != 0) pmass[i, j].Image = imageList1.Images[8 + rub1 * 24 + imass[i, j]];
                            if (!open && i != 0) pmass[i, j].Image = imageList1.Images[128 + rub];
                        }
                    }
            }
        }

        private void игрокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
            label1.Text = name.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (myText == false)
            {
                this.Text = "Бейлот.";
                myText = true;
            }
            else
            {
                this.Text = "Бейлот с анимацией.";
                myText = false;
            }
        }
    }
}
