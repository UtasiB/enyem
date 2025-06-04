using System;
using AFA_szamolo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Vizilabda
{

    public partial class Form1 : Form
    {
        dbClass db = new dbClass("localhost", "root", "", "13a_vizilabda");    

        public Form1()
        {
            InitializeComponent();
            ComboBoxFeltoltes();
            ListBoxToltes();
            EuropaBajnoksagos();
        }

        private void EuropaBajnoksagos()
        {
            //A feladat azt írta hogy a "bajnokság tábla minden adata szerepeljen" ezért úgy gondolotam hogy nem csinálom meg azt hogy a nevét írja ki
            //Lécci ne nagyon bántson ez miatt 😖🙏
            listBox2.Items.Clear();
            listBox2.Items.Add("Id - Év - Helyszín - Helyezés - Kapitány ID - Verseny");
            db.selectAll("bajnoksag");
            while (db.results.Read()) 
            {
                if (db.results["verseny"].ToString() == "Európa-bajnokság" && db.results["helyezes"].ToString() == "1") {
                    listBox2.Items.Add(db.results["id"].ToString() + " - " + db.results["ev"].ToString() + " - " + db.results["helyszin"].ToString() + " - " + db.results["helyezes"].ToString()+ " - " + db.results["kapitanyid"].ToString() + " - " + db.results["verseny"].ToString());
                }
            }
        }

        private void ListBoxToltes()
        {
            listBox1.Items.Clear();
            db.selectAll("kapitany");
            while (db.results.Read())
            {
                listBox1.Items.Add(db.results["neve"].ToString());
            }
        }

        private void ComboBoxFeltoltes()
        {
            try
            {
                db.selectAll("kapitany");
                while (db.results.Read()) 
                {
                    comboBox1.Items.Add(db.results["neve"].ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Adatbázis hiba!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0 || textBox1.Text == "" || numericUpDown2.Value == 0 || textBox2.Text == "" || comboBox1.Text == "") 
            {
                MessageBox.Show("Hiányzó adatok!");
            }
           else 
            {
                try
                {
                    string[] fields = { "ev", "helyszin", "helyezes", "kapitanyid", "verseny" };
                    string[] values = { numericUpDown1.Value.ToString(), textBox1.Text, numericUpDown2.Value.ToString(), (comboBox1.SelectedIndex + 1).ToString(), textBox2.Text };

                    db.insert("bajnoksag", fields, values);
                    MessageBox.Show("Sikeres felvétel!");

                    numericUpDown1.Value = 1;
                    textBox1.Text = "";
                    numericUpDown2.Value = 1;
                    textBox2.Text = "";
                    comboBox1.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Hatalmas hiba van insertnél!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericUpDown3.Value == 0 || textBox3.Text == "")
            {
                MessageBox.Show("Hiányzó adatok!");
            }
            else
            {
                if (numericUpDown4.Value != 0)
                {
                    if (numericUpDown3.Value > numericUpDown4.Value)
                    {
                        MessageBox.Show("Itt nem adta ki a matek!");
                    }
                }
                else
                {
                    try
                    {
                        string[] fields = { "neve", "szuletett", "meghalt", };
                        string[] values = { textBox3.Text, numericUpDown3.Value.ToString(), numericUpDown4.Value.ToString() };

                        db.insert("kapitany", fields, values);
                        MessageBox.Show("Sikeres felvétel!");
                        ComboBoxFeltoltes();

                        numericUpDown4.Value = 1;
                        textBox3.Text = "";
                        numericUpDown3.Value = 1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Hatalmas hiba van insertnél!");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int szulEv = Convert.ToInt32(numericUpDown5.Value);

            listBox1.Items.Clear();
            db.selectAll("kapitany");
            while (db.results.Read())
            {
                if (Convert.ToInt32(db.results["szuletett"]) == szulEv)
                {
                    listBox1.Items.Add(db.results["neve"]);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListBoxToltes();
        }
    }
}