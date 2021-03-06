﻿using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSRProject
{
    public partial class aCharity : MetroFramework.Forms.MetroForm
    {
        SqlConnection con = Сonnection.AzureConnection();

        public aCharity()
        {
            InitializeComponent();
            CharityLoadAsync();
            timer1.Start();
            FormClosing += new FormClosingEventHandler(AppClose.GoodBye);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var counter = new Сonnection(); // Создание экземпляра класса Connection
            timerLabel.Text = counter.GetTime(); // Для доступа к публичному методу возвращаемого типа string
        }

        private async void CharityLoadAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    metroGrid1.DataSource = null;
                    con.Open();
                    metroGrid1.Columns[0].DefaultCellStyle.NullValue = Properties.Resources.tile_Blago;
                    SqlDataAdapter ad = new SqlDataAdapter("SELECT CharityName, CharityDescription, CharityLogo FROM Charity", con);
                    ad.Fill(wSRDataSetCharity1, "Charity");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });

            metroGrid1.DataSource = charityBindingSource;
            LoaderPictureBox.Visible = false;
            LoaderPictureBox.Enabled = false;
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            var CharityAddForm = new aCharityAdd
            {
                Location = Location
            };
            CharityAddForm.Show();
            Hide();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            var AdminMenu = new AdminForm
            {
                Location = Location
            };
            AdminMenu.Show();
            Hide();
        }

    }
}
