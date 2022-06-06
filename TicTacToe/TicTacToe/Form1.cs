using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Properties;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        int Igrac = 0; 
        bool Kompjuter=false;
        char[] xo = new char[2] { 'X', 'O' };
        string[] igraci = new string[2] { "X", "O" };
        char[,] Ploca=new char[3,3]{ {' ',' ',' '},{' ',' ',' '},{' ',' ',' '} };
        public Form1()
        {
            InitializeComponent();
        }
        bool Pobjeda()
        {//ako su ista 3 znaka horiznotalno ili veritkalo ili po diagonalama u istom redu onda je pobijedio igrac koji je zadnji upisao znak
            if ((Ploca[0, 0] == Ploca[0, 1] && Ploca[0, 0] == Ploca[0, 2] && Ploca[0, 0] != ' ')
             || (Ploca[1, 0] == Ploca[1, 1] && Ploca[1, 0] == Ploca[1, 2] && Ploca[1, 0] != ' ')
             || (Ploca[2, 0] == Ploca[2, 1] && Ploca[2, 0] == Ploca[2, 2] && Ploca[2, 0] != ' ')//horizontalno
             || (Ploca[0, 0] == Ploca[1, 0] && Ploca[1, 0] == Ploca[2, 0] && Ploca[0, 0] != ' ')
             || (Ploca[0, 1] == Ploca[1, 1] && Ploca[0, 1] == Ploca[2, 1] && Ploca[0, 1] != ' ')
             || (Ploca[0, 2] == Ploca[1, 2] && Ploca[0, 2] == Ploca[2, 2] && Ploca[0, 2] != ' ')//vertikalno
             || (Ploca[0, 0] == Ploca[1, 1] && Ploca[0, 0] == Ploca[2, 2] && Ploca[0, 0] != ' ')
             || (Ploca[0, 2] == Ploca[1, 1] && Ploca[0, 2] == Ploca[2, 0] && Ploca[0, 2] != ' ')//dijagonale
                ) return true;
            else return false;
        }
        bool Ima_Poteza(StringBuilder s)
        {
            for (int i = 0; i < 9; i++)
                if (s[i] == ' ') return true;
            return false;
        }
        int Procijeni(StringBuilder s)
        {
            for(int i=0;i<3;i++)
            {
                if(s[i*3]==s[i*3+1] && s[i*3]==s[i*3+2])//horizontalno
                {
                    if (s[i * 3] == xo[Igrac % 2]) return +10;
                    else if (s[i * 3] == xo[(Igrac + 1) % 2]) return -10;
                }
                if(s[i]==s[i+3] && s[i]==s[i+6])//verikalno
                {
                    if (s[i] == xo[Igrac % 2]) return +10;
                    else if (s[i] == xo[(Igrac + 1) % 2]) return -10;
                }
            }
            //dijagonale
            if(s[0]==s[4] && s[0]==s[8])
            {
                if (s[0] == xo[Igrac % 2]) return +10;
                else if (s[0] == xo[(Igrac + 1) % 2]) return -10;
            }
            if (s[2] == s[4] && s[2] == s[6])
            {
                if (s[2] == xo[Igrac % 2]) return +10;
                else if (s[2] == xo[(Igrac + 1) % 2]) return -10;
            }
            return 0;

        }
        int minimax(StringBuilder s,bool IsMax)
        {
            int score = Procijeni(s);
            if (score == 10 || score == -10) return score;
            if (Ima_Poteza(s) == false) return 0;
            
            if(IsMax)
            {
                int best = -1000;
                for(int i=0;i<9;i++)
                {
                    if(s[i]==' ')
                    {
                        s[i] = xo[Igrac % 2];
                        best = Math.Max(best, minimax(s, !IsMax));
                        s[i] = ' ';
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;
                for (int i = 0; i < 9; i++)
                {
                    if (s[i] == ' ')
                    {
                        s[i] = xo[(Igrac+1) % 2];
                        best = Math.Min(best, minimax(s, !IsMax));
                        s[i] = ' ';
                    }
                }
                return best;
            }
        }
        void OdigrajNajboljiPotez()
        {
            StringBuilder s = new StringBuilder();
            s.Clear();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    s.Append(Ploca[i, j]);
            int Najbojli = -1000;
            int NajboljiX = -1, NajboljiY = -1;
            for(int i=0;i<9;i++)
            {
                if(s[i]==' ')
                {
                    s[i] = xo[Igrac % 2];
                    int test = minimax(s, false);
                    s[i] = ' ';
                    if(test>Najbojli)
                    {
                        Najbojli = test;
                        NajboljiX = i / 3;
                        NajboljiY = i % 3;
                    }
                }
            }
            OdigrajPotez(NajboljiX, NajboljiY);
        }
        void OdigrajPotez(int i,int j)
        {
            if (i == -1) return;
            if(i==0 && j==0)
            {
                Ploca[0, 0] = xo[Igrac % 2]; 
                _00.Text = xo[Igrac % 2].ToString();
                _00.Enabled = false;
            }
            if (i == 0 && j == 1)
            {
                Ploca[0, 1] = xo[Igrac % 2];
                _01.Text = xo[Igrac % 2].ToString();
                _01.Enabled = false;
            }
            if (i == 0 && j == 2)
            {
                Ploca[0, 2] = xo[Igrac % 2];
                _02.Text = xo[Igrac % 2].ToString();
                _02.Enabled = false;
            }
            ///
            if (i == 1 && j == 0)
            {
                Ploca[1, 0] = xo[Igrac % 2];
                _10.Text = xo[Igrac % 2].ToString();
                _10.Enabled = false;
            }
            if (i == 1 && j == 1)
            {
                Ploca[1, 1] = xo[Igrac % 2];
                _11.Text = xo[Igrac % 2].ToString();
                _11.Enabled = false;
            }
            if (i == 1 && j == 2)
            {
                Ploca[1, 2] = xo[Igrac % 2];
                _12.Text = xo[Igrac % 2].ToString();
                _12.Enabled = false;
            }
            ///
            if (i == 2 && j == 0)
            {
                Ploca[2, 0] = xo[Igrac % 2];
                _20.Text = xo[Igrac % 2].ToString();
                _20.Enabled = false;
            }
            if (i == 2 && j == 1)
            {
                Ploca[2, 1] = xo[Igrac % 2];
                _21.Text = xo[Igrac % 2].ToString();
                _21.Enabled = false;
            }
            if (i == 2 && j == 2)
            {
                Ploca[2, 2] = xo[Igrac % 2];
                _22.Text = xo[Igrac % 2].ToString();
                _22.Enabled = false;
            }
            if (Pobjeda())
            {
                textBox1.Text = "Pobjednik je igrac " + igraci[Igrac % 2];
                DisableSvaDugmad();
                return;
            }
            else if (Igrac == 8) textBox1.Text = "Nerjeseno";

            Igrac++;
        }
        void DisableSvaDugmad()
        { //na kraju igre iskljucimo sva dugmad
            _00.Enabled = false;
            _01.Enabled = false;
            _02.Enabled = false;
            _10.Enabled = false;
            _11.Enabled = false;
            _12.Enabled = false;
            _20.Enabled = false;
            _21.Enabled = false;
            _22.Enabled = false;
        }
        private void _00_Click(object sender, EventArgs e)
        { 
            OdigrajPotez(0, 0);
            if(Kompjuter==true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _01_Click(object sender, EventArgs e)
        {
            OdigrajPotez(0, 1);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }

        }

        private void _02_Click(object sender, EventArgs e)
        {
            OdigrajPotez(0, 2);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _10_Click(object sender, EventArgs e)
        {
            OdigrajPotez(1, 0);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _11_Click(object sender, EventArgs e)
        {
            OdigrajPotez(1, 1);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _12_Click(object sender, EventArgs e)
        {
            OdigrajPotez(1, 2);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _20_Click(object sender, EventArgs e)
        {
            OdigrajPotez(2, 0);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _21_Click(object sender, EventArgs e)
        {
            OdigrajPotez(2, 1);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void _22_Click(object sender, EventArgs e)
        {
            OdigrajPotez(2, 2);
            if (Kompjuter == true)
            {
                OdigrajNajboljiPotez();
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {//kada restartujemo igru omogucimo sva dugmad
            _00.Enabled = true;
            _01.Enabled = true;
            _02.Enabled = true;
            _10.Enabled = true;
            _11.Enabled = true;
            _12.Enabled = true;
            _20.Enabled = true;
            _21.Enabled = true;
            _22.Enabled = true;
            //
            _00.Text = "";//obrisemo text iz svih dugmadi
            _01.Text = "";
            _02.Text = "";
            _10.Text = "";
            _11.Text = "";
            _12.Text = "";
            _20.Text = "";
            _21.Text = "";
            _22.Text = "";
            //
            textBox1.Text = ""; // i text boxa
            //
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Ploca[i, j] = ' ';
                }
            }
            //
            Igrac = 0;
            Kompjuter = false;
            if (comboBox1.SelectedIndex == 1)
            {
                Kompjuter = true;
                Random rnd = new Random();
                if (rnd.Next(0, 2) == 1)
                {
                    OdigrajNajboljiPotez();
                }
            }
            //
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                igraci[0] = Convert.ToString(textBox2.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                igraci[1] = Convert.ToString(textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Dva igraca";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Restart_Click(this, null);
        }
    }
}
