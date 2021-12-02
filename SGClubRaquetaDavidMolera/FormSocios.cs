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
    public partial class FormSocios : Form
    {
        public FormSocios()
        {
            InitializeComponent();
        }

        private void btMostrar_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objBD = new clubraquetaEntities())
            {
                var qSocios = from s in objBD.socios
                              orderby s.apellidos, s.nombre
                                  select new { s.DNI, s.nombre, s.apellidos, s.telefono, s.email };
                var listaSoc = qSocios.ToList();
                dgvSocios.DataSource = listaSoc;


            }
        }

        private void btAñadir_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objBD = new clubraquetaEntities())
            {
                //Creamos el objeto categoria
                socios objCat = new socios();
                objCat.DNI = txtDni.Text;
                objCat.nombre = txtNombre.Text;
                objCat.apellidos = txtApellidos.Text;
                objCat.domicilio = txtDireccion.Text;
                objCat.telefono = mtbTelefono.Text;
                objCat.email = txtEmail.Text;
                objCat.cuentaCorriente = mtbC.Text;

                //Se añade el objeto a la tabla, para incluirlo como nuevo
                objBD.socios.Add(objCat);

                //Se guardan los cambios
                objBD.SaveChanges();
                MessageBox.Show("Socio añadido correctamente");
            }
        }

        private void btModificar_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objBD = new clubraquetaEntities())
            {
                socios objCat = objBD.socios.Find(txtDni.Text);

                if (objCat != null)
                {
                    if (txtNombre.Text!="")
                    {
                        objCat.nombre = txtNombre.Text;
                    }
                    if (txtApellidos.Text != "")
                    {
                        objCat.apellidos = txtApellidos.Text;

                    }
                    if (txtDireccion.Text != "")
                    {
                        objCat.domicilio = txtDireccion.Text;

                    }
                    if (mtbTelefono.Text != "")
                    {
                        objCat.telefono = mtbTelefono.Text;

                    }
                    if (txtEmail.Text != "")
                    {
                        objCat.email = txtEmail.Text;

                    }
                    if (mtbC.Text != "ES")
                    {
                        objCat.cuentaCorriente = mtbC.Text;

                    }
                    //Se guardan los cambios
                    objBD.SaveChanges();
                    MessageBox.Show("Se ha modificado correctamente el socio con dni: "+txtDni.Text.ToString());
                }
                else
                {
                    MessageBox.Show("No se ha encontrado al socio");
                }


            }
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            using (clubraquetaEntities objBD = new clubraquetaEntities())
            {
                var qRes = from r in objBD.reservas
                           where r.socio == txtDni.Text
                           select new { r.socio };
                var listaPre = qRes.Distinct().ToList();
                socios objCat = objBD.socios.Find(txtDni.Text);
                if (objCat != null)
                {
                    if (listaPre.Count > 0)
                    {
                        MessageBox.Show("El socio tiene una reserva");

                    }
                    else
                    {
                        objBD.socios.Remove(objCat);
                        //Se guardan los cambios
                        objBD.SaveChanges();
                        MessageBox.Show("Socio eliminado correctamente");
                    }

                }
                else
                {
                    MessageBox.Show("No se ha encontrado al socio");
                }


            }
        }

        private void btProbar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
