using CRUD_Dapper2.CRUDService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace CRUD_Dapper2
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        EntityState objState = EntityState.Unchanged;

        public Form1()
        {
            InitializeComponent();
        }

        //*** btn SALVAR
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Validações...
            validarEmail(txtEmail, errorProvider1);
            validarCPF(txtCPF, errorProvider1);

            try
            {
                Contato obj = contatoBindingSource.Current as Contato;
                if (obj != null)
                {               
                    ContatoServiceSoapClient client = new ContatoServiceSoapClient();                                       
                    if (objState == EntityState.Added)
                    {
                        obj.ContatoID = client.Inserir(obj);
                    }
                    else if (objState == EntityState.Changed)
                    {
                        client.Update(obj);
                    }
                    gdContatos.Refresh();
                    pContainer.Enabled = false;
                    objState = EntityState.Unchanged;
                    }      
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        //*** Valida CPF
        private void validarCPF(MetroTextBox textBox, ErrorProvider errorProvider)
        {
            string cpf = txtCPF.Text;
            string modelo = @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)";
            if (Regex.IsMatch(cpf, modelo))
            {
                errorProvider1.SetError(txtCPF, "");
            }
            else
            {
                errorProvider1.SetError(txtCPF, "CPF inválido.");
            }
        }

        //*** Valida E-MAIL
        private void validarEmail(MetroTextBox textBox, ErrorProvider errorProvider1)
        {
            string email = txtEmail.Text;
            string modelo = @"[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+";
            if (Regex.IsMatch(email, modelo))
            {
                errorProvider1.SetError(txtEmail, "");
            }
            else
            {
                errorProvider1.SetError(txtEmail, "E-mail inválido.");
            }
        }

        //*** btn PROCURAR
        private void btnProcurar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg|PNG|*.png", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picFoto.Image = Image.FromFile(ofd.FileName);
                    Contato obj = contatoBindingSource.Current as Contato;
                    if (obj != null)
                    {
                        obj.ImagemUrl = ofd.FileName;
                    }
                }
            }
        }

        //*** Limpa Campos
        void LimpaControles()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtFixo.Text = "";
            txtID.Text = "";
            txtCelular.Text = "";
            txtCPF.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {              
                CRUDService.ContatoServiceSoapClient client = new ContatoServiceSoapClient();
                contatoBindingSource.DataSource = client.GetAll();
                pContainer.Enabled = false;                                       
                Contato obj = contatoBindingSource.Current as Contato;
                if (obj != null)
                {
                    if (!string.IsNullOrEmpty(obj.ImagemUrl))                       
                        picFoto.Image = Image.FromFile(obj.ImagemUrl);                     
                }                                 
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //*** btn ADICIONAR
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                objState = EntityState.Added;
                picFoto.Image = null;
                pContainer.Enabled = true;
                List<Contato> list = ((IEnumerable<Contato>)contatoBindingSource.DataSource).ToList();                       
                list.Add(new Contato());             
                contatoBindingSource.DataSource = list.AsEnumerable();
                contatoBindingSource.MoveLast();
                txtNome.Focus();
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //*** btn CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pContainer.Enabled = false;
            contatoBindingSource.ResetBindings(false);
            //LimpaControles();
            this.Form1_Load(sender, e);
        }

        //*** btn EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            objState = EntityState.Changed;
            pContainer.Enabled = true;
            txtNome.Focus();
        }

        //*** btn CELLCLICK
        private void gdContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Contato obj = contatoBindingSource.Current as Contato;
                if (obj != null)
                {
                    if (!string.IsNullOrEmpty(obj.ImagemUrl))
                    {
                        picFoto.Image = Image.FromFile(obj.ImagemUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //*** btn DELETAR
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            objState = EntityState.Deleted;
            if (MetroFramework.MetroMessageBox.Show(this, "Tem certeza que deseja Excluir esta registro", "Excluir ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Contato obj = contatoBindingSource.Current as Contato;
                    if (obj != null)
                    {
                        ContatoServiceSoapClient client = new ContatoServiceSoapClient();
                        bool result = client.Delete(obj.ContatoID);
                        if (result)
                        {
                            contatoBindingSource.RemoveCurrent();
                            pContainer.Enabled = false;
                            picFoto.Image = null;
                            objState = EntityState.Unchanged;
                        }                                                  
                     }                                 
                }              
                catch (Exception ex)
                {
                    MetroFramework.MetroMessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
