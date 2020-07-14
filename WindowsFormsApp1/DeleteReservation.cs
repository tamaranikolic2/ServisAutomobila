using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DeleteReservation : Form
    {
        private ReservationBusiness reservationBusiness;
        private UserBusiness userBusiness;
        public DeleteReservation()
        {
            InitializeComponent();
        }

        private void DeleteUpdateReservation_Load(object sender, EventArgs e)
        {
            this.reservationBusiness = new ReservationBusiness();
            this.userBusiness = new UserBusiness();
            
            fillReservations();
        }
        private void fillReservations()
        {
            var reservations = this.reservationBusiness.GetAllReservations();
            foreach (Reservation r in reservations)
            {
                this.user_list.Items.Add(r.code+"  -  " + r.username + " - " + r.date + "  " + r.time + " " +r.service_type );
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tb_code.Text))
            {
                MessageBox.Show("Dodajte datum!");
            }
            else
            {
                var confirmResult = MessageBox.Show("Da li ste sigurni da zelite da obrisete rezervaciju?",
                                     "Brisanje!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    Reservation r = new Reservation();
                    r.code = Int32.Parse(tb_code.Text);
                    this.reservationBusiness.DeleteReservation(r);
                    

                    MessageBox.Show("Uspesno izbrisano!");
                    user_list.Items.Clear();
                    fillReservations();
                }
                else
                {
                    return;
                }
            }
        }
        

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reservations res = new Reservations();
            res.Show();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tb_code.Text))
            {
                MessageBox.Show("Popunite sva potrebna polja!");
            }
            else
            {
                if (!Regex.Match(tb_code.Text, @"^\d+$").Success)
                {
                    MessageBox.Show("Sifra mora biti broj!");
                    return;
                }
                else
                {
                    Reservation r = new Reservation();
                    r.code = Convert.ToInt32(tb_code.Text);
                    

                    this.reservationBusiness.UpdateReservation(r);
                    MessageBox.Show("Uspesno izmenjeno!");
                    user_list.Items.Clear();
                    fillReservations();
                }
            }
        }
    }
}
