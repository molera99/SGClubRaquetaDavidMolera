using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGClubRaquetaDavidMolera
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formPistas frmPista = new formPistas();
            frmPista.ShowDialog();
        }

        private void rESERVASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formReservas Frm = new formReservas();
            Frm.TopLevel = false;
            panel1.Controls.Add(Frm);
            Frm.Show();

        }

        private void sociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSocios frm = new FormSocios();
            frm.ShowDialog();

        }
    }
}
