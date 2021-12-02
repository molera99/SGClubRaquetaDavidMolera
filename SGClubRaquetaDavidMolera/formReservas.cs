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
    public partial class formReservas : Form
    {
        public formReservas()
        {
            InitializeComponent();
        }

        private void formReservas_Load(object sender, EventArgs e)
        {
           

            panel1.Enabled = false;
            panel2.Enabled = false;
            panel3.Enabled = false;
            crDB cr = new crDB();
            crDBTableAdapters.sociosTableAdapter tbSocios = new crDBTableAdapters.sociosTableAdapter();
            tbSocios.Fill(cr.socios);
            for(int i = 0; i < cr.socios.Count(); i++)
            {
                cbNombreSocio.Items.Add(cr.socios[i].apellidos+" "+cr.socios[i].nombre);
            }
            dataGridViewTextBoxColumn1.Visible = false;
            dataGridViewTextBoxColumn5.Visible = false;
            dataGridViewTextBoxColumn7.HeaderText = "Euros";
            crDBTableAdapters.pistasTableAdapter tbPistas = new crDBTableAdapters.pistasTableAdapter();
            tbPistas.Fill(cr.pistas);
            for (int i=0;i<cr.pistas.Count();i++)
            {
                cbPista.Items.Add(cr.pistas[i].nombre);
            }
        }

        private void cbNombreSocio_SelectedIndexChanged(object sender, EventArgs e)
        {

            crDB cr = new crDB();
            crDBTableAdapters.sociosTableAdapter tbSocios = new crDBTableAdapters.sociosTableAdapter();
             String[] filtro = cbNombreSocio.Text.ToString().Split(' ');
            String apellidoSocio = filtro[0] + " " + filtro[1];
            tbSocios.FillByApellido(cr.socios, apellidoSocio);
            if (cr.socios.Count > 0)
            {
                lbDni.Text = cr.socios[0].DNI;
                txtNombre.Text = cr.socios[0].nombre;
                txtApellidos.Text = cr.socios[0].apellidos;
                txtDireccion.Text = cr.socios[0].domicilio;
                txtEmail.Text = cr.socios[0].email;
                mtbTelefono.Text = cr.socios[0].telefono;
                mtbC.Text = cr.socios[0].cuentaCorriente;
               this.reservasTableAdapter.FillBySocio(this.crDB.reservas, lbDni.Text.ToString());
            }
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reservasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.reservasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void reservasBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.reservasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void reservasBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.reservasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.crDB);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbPista_SelectedIndexChanged(object sender, EventArgs e)
        {
            crDB cr = new crDB();
            crDBTableAdapters.pistasTableAdapter tbPistas = new crDBTableAdapters.pistasTableAdapter();
            tbPistas.FillByPista(cr.pistas, cbPista.Text);
            if (cr.pistas.Rows.Count> 0)
            {
                DataRow myRow = cr.pistas.Rows[0];

                byte[] MyData = (byte[])myRow["foto"];
                MemoryStream stream = new MemoryStream(MyData);
                pbFoto.Image = Image.FromStream(stream);
            }
        }

        private void btReservar_Click(object sender, EventArgs e)
        {
            crDB cr = new crDB();
            crDBTableAdapters.reservasTableAdapter tbReservas = new crDBTableAdapters.reservasTableAdapter();
            crDBTableAdapters.pistasTableAdapter tbPistas = new crDBTableAdapters.pistasTableAdapter();
            tbPistas.FillByPista(cr.pistas, cbPista.Text);
            String strHora = nudHora.Value.ToString() + ":" + nudMinuto.Value.ToString()+":00";
            TimeSpan horaReserva = DateTime.Parse(strHora).TimeOfDay;
            Boolean control = true;
            tbReservas.FillBySocio(cr.reservas, lbDni.Text);
            if (cr.reservas.Rows.Count > 0)
            {
                for (int i=0;i<cr.reservas.Count();i++)
                {
                    if (cr.reservas[i].pagado.Equals("No"))
                    {
                        control = false;
                        MessageBox.Show("Debes pagar tus reservas para poder reservar una pista");
                    }
                }
            }
            tbReservas.Fill(cr.reservas);
            if (cr.reservas.Rows.Count>0)
            {
                for(int i = 0; i < cr.reservas.Count(); i++)
                {
                    if (cr.reservas[i].fecha.Equals(dtpFecha.Value))
                    {
                        tbReservas.FillByHora(cr.reservas,dtpFecha.Value.ToString());
                        TimeSpan horaDB = cr.reservas[0].hora;
                        TimeSpan intervalo = horaReserva - horaDB;
                        if (Math.Abs(intervalo.Hours)<1)
                        {
                            control = false;
                            MessageBox.Show("La pista esta ocupada");

                        }
                        else if (Math.Abs(intervalo.Hours) >= 2)
                        {
                            control = true;
                        }else if (Math.Abs(intervalo.Minutes) >= 30)
                        {
                            control = true;
                        }
                        else
                        {
                            control = false;
                        }
                    }
                }
            }
            tbPistas.FillByPista(cr.pistas, cbPista.Text);
            decimal precio = cr.pistas[0].precioHora + (cr.pistas[0].precioHora / 2);
            if (control == true)
            {
                DialogResult drAlquileres = MessageBox.Show("¿Deseas reserva la pista?", "RESERVA PISTA", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (drAlquileres == DialogResult.OK)
                {
                    tbReservas.Insert(dtpFecha.Value, horaReserva, cr.pistas[0].idPista, lbDni.Text, "No", precio);
                    MessageBox.Show("Pista reservada");
                    this.reservasTableAdapter.FillBySocio(this.crDB.reservas, lbDni.Text.ToString());

                }
                else
                {

                }
            }
            
                    
        }

        private void reservasDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btPagar_Click(object sender, EventArgs e)
        {
            if (reservasDataGridView.SelectedRows[0].Cells[5].Value==null)
            {
                MessageBox.Show("Selecciona una reserva valida");

            }
            else if (reservasDataGridView.SelectedRows[0].Cells[5].Value.ToString().Equals("Si"))
            {
                MessageBox.Show("Esta reserva ya esta pagada");

            }
            else
            {
                crDB cr = new crDB();
                crDBTableAdapters.reservasTableAdapter tbReservas = new crDBTableAdapters.reservasTableAdapter();
                tbReservas.FillByReserva(cr.reservas, int.Parse(reservasDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
                tbReservas.Update("Si", int.Parse(reservasDataGridView.SelectedRows[0].Cells[0].Value.ToString()), int.Parse(reservasDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
                this.reservasTableAdapter.FillBySocio(this.crDB.reservas, lbDni.Text.ToString());

            }
        }
    }
}
