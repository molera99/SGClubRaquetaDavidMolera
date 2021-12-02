using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGClubRaquetaDavidMolera
{
    public partial class formPistas : Form
    {
        public formPistas()
        {
            InitializeComponent();
        }

        private void pistasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pistasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void pistasBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.pistasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void pistasBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.pistasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void formPistas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'crDB.pistas' Puede moverla o quitarla según sea necesario.
            this.pistasTableAdapter.Fill(this.crDB.pistas);

        }

        private void btNueva_Click(object sender, EventArgs e)
        {
            ofdExaminar.Filter = "image files|*.jpg;*.png;*.gif;*.ico;.*;";

            DialogResult dres = ofdExaminar.ShowDialog();

            if (dres == DialogResult.Abort)
                return;
            if (dres == DialogResult.Cancel)
                return;

            picFoto.Image = Image.FromFile(ofdExaminar.FileName);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Estas seguro de eliminar la pista?", "ELIMINACION PISTA", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                crDB ds = new crDB();

                crDBTableAdapters.reservasTableAdapter tbReservas = new crDBTableAdapters.reservasTableAdapter();
                tbReservas.FillByPista(ds.reservas,int.Parse(idPistaLabel1.Text));
                if (ds.reservas.Count > 0)
                {
                    MessageBox.Show("No se puede eliminar porque la pista esta alquilada");
                    DialogResult drAlquileres = MessageBox.Show("¿Quieres eliminar todos los datos de la pista?", "ELIMINACION PISTA", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (drAlquileres == DialogResult.OK)
                    {
                        tbReservas.Delete(int.Parse(idPistaLabel1.Text));
                        this.pistasTableAdapter.Delete(int.Parse(idPistaLabel1.Text));
                        MessageBox.Show("Pista eliminada");
                        this.pistasTableAdapter.Fill(this.crDB.pistas);
                        
                    }
                }
                else
                {
                    this.pistasTableAdapter.Delete(int.Parse(idPistaLabel1.Text));
                    MessageBox.Show("Pista eliminada");
                    this.pistasTableAdapter.Fill(this.crDB.pistas);
                }

            }
        }
    }
}
