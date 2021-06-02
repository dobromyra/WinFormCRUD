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
    public partial class MainForm : Form
    {
        public void GetStudent()
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-76T88UVI\SQLEXPRESS;Initial Catalog=NewNewBase;Integrated Security=True"))
            {
                Con.Open();
                string query = "SELECT 'Полное имя' = s.LName + ' ' + s.FName + ' ' + s.MName, 'Дата рождения' = s.BirthDate, " +
                    "'Группа' = sp.ShortName + '-' + SUBSTRING(g.Year, 3, 2) + '.' + g.Number " +
                    "FROM Student as s INNER JOIN [Group] as g ON g.ID = s.IDGroup " +
                    "INNER JOIN Specialty as sp ON sp.ID = g.IDSpec";
                SqlCommand command = new SqlCommand(query, Con);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                Con.Close();

                if (dt.Rows.Count != 0)
                {
                    gridStudent.Visible = true;
                    gridStudent.DataSource = dt;
                    gridStudent.Update();
                    gridStudent.Columns[0].Width = 351; //ФИО
                    gridStudent.Columns[1].Width = 150; //др
                    gridStudent.Columns[2].Width = 200; //группа
                }
                else
                {
                    gridStudent.Visible = false;
                }

            }
        }

        public MainForm()
        {
            InitializeComponent();
            GetStudent();
            
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            FormNewStudent form = new FormNewStudent();
            form.Show();
            this.Hide();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            GetStudent();
   
        }
    }
}
