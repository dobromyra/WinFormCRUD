using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForBD
{
    public partial class FormNewStudent : Form
    {
        public FormNewStudent()
        {
            InitializeComponent();
            using (SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-76T88UVI\SQLEXPRESS;Initial Catalog=NewNewBase;Integrated Security=True"))
            {
                Con.Open();
                string query = "SELECT [ID] = g.ID, [Name] = sp.ShortName + '-' + SUBSTRING(g.Year, 3, 2) + '.'" +
                    "+ g.Number FROM [Group] as g INNER JOIN [Specialty] as sp ON sp.ID = g.IDSpec";
                SqlCommand command = new SqlCommand(query, Con);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();

                dt.Load(reader);

                ddlGroup.DataSource = dt;
                ddlGroup.ValueMember = "ID";
                ddlGroup.DisplayMember = "Name";

                Con.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLName.Text) || string.IsNullOrEmpty(txtFName.Text))
            {
                lblError.Text = "Заполните фамилию и имя";
                lblError.Visible = true; 
            }
            else
            {
                if (dateTimePicker.Value > DateTime.Now)
                {
                    lblError.Text = "Дата рождения должна быть реальной";
                    lblError.Visible = true;
                }
                else
                {
                    using (SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-76T88UVI\SQLEXPRESS;Initial Catalog=NewNewBase;Integrated Security=True"))
                    {
                        Con.Open();
                        string query = "INSERT INTO Student (LName, FName, MName, BirthDate, IDGroup) VALUES " +
                            "('" + txtLName.Text + "', '" + txtFName.Text + "', '" + txtMName.Text + "', '" +
                            dateTimePicker.Value + "', " + ddlGroup.SelectedValue + ")";
                        SqlCommand command = new SqlCommand(query, Con);
                        command.ExecuteNonQuery();

                        Con.Close();
                        this.Close();
                    }
                }
            }
           
        }

        private void FormNewStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form ifrm = Application.OpenForms[0];
            ifrm.Show();
        }
    }
}
