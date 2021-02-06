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

namespace SpaceX_Starlink_project
{
    public partial class SpaceX : Form
    {
        public SpaceX()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        private void SpaceX_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'spacexDataSet.Status' table. You can move, or remove it, as needed.
            this.statusTableAdapter.Fill(this.spacexDataSet.Status);
            cn = new SqlConnection(@"Data Source=MRSEMICOLON;Initial Catalog=spacex;Integrated Security=True");
            cn.Open();
            //bind data in data grid view  
            GetAllSatelliteRecord();

            //disable delete and update button on load  
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void GetAllSatelliteRecord()
        {
            cmd = new SqlCommand("spacex_pro", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SatelliteID", 0);
            cmd.Parameters.AddWithValue("@SatelliteName", " ");
            cmd.Parameters.AddWithValue("@Longitude", 0);
            cmd.Parameters.AddWithValue("@Latitude", 0);
            cmd.Parameters.AddWithValue("@Elevation", 0);
            cmd.Parameters.AddWithValue("@HealthStatus", " ");
            cmd.Parameters.AddWithValue("@OperationType", "5");
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != string.Empty && txt_sat_name.Text != string.Empty && txt_long.Text != string.Empty && txt_lat.Text != string.Empty && txt_ele.Text != string.Empty)
            {
                cmd = new SqlCommand("spacex_pro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SatelliteID", txt_id.Text);
                cmd.Parameters.AddWithValue("@SatelliteName", txt_sat_name.Text);
                cmd.Parameters.AddWithValue("@Longitude", txt_long.Text);
                cmd.Parameters.AddWithValue("@Latitude", txt_lat.Text);
                cmd.Parameters.AddWithValue("@Elevation", txt_ele.Text);
                cmd.Parameters.AddWithValue("@HealthStatus", comboBox1.Text);
                cmd.Parameters.AddWithValue("@OperationType", "2");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record update successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAllSatelliteRecord();
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_sat_name.Text != string.Empty && txt_long.Text != string.Empty && txt_lat.Text != string.Empty && txt_ele.Text != string.Empty)
            {
                cmd = new SqlCommand("spacex_pro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SatelliteID", 0);
                cmd.Parameters.AddWithValue("@SatelliteName", txt_sat_name.Text);
                cmd.Parameters.AddWithValue("@Longitude", txt_long.Text);
                cmd.Parameters.AddWithValue("@Latitude", txt_lat.Text);
                cmd.Parameters.AddWithValue("@Elevation", txt_ele.Text);
                cmd.Parameters.AddWithValue("@HealthStatus", comboBox1.Text);
                cmd.Parameters.AddWithValue("@OperationType", "1");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record inserted successfully.", "Record Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAllSatelliteRecord();
                txt_sat_name.Text = "";
                txt_long.Text = "";
                txt_lat.Text = "";
                txt_ele.Text = "";
                comboBox1.Text = default;
            }
            else
            {
                MessageBox.Show("Please enter value in all fields", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != string.Empty)
            {

                cmd = new SqlCommand("spacex_pro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SatelliteID", txt_id.Text);
                cmd.Parameters.AddWithValue("@SatelliteName", "");
                cmd.Parameters.AddWithValue("@Longitude", 0);
                cmd.Parameters.AddWithValue("@Latitude", 0);
                cmd.Parameters.AddWithValue("@Elevation", 0);
                cmd.Parameters.AddWithValue("@HealthStatus", "");
                cmd.Parameters.AddWithValue("@OperationType", "4");
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txt_sat_name.Text = dr["SatelliteName"].ToString();
                    txt_long.Text = dr["Longitude"].ToString();
                    txt_lat.Text = dr["Latitude"].ToString();
                    txt_ele.Text = dr["Elevation"].ToString();
                    comboBox1.Text = dr["HealthStatus"].ToString();
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No record found with this id", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dr.Close();
            }
            else
            {
                MessageBox.Show("Please enter employee id ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this employee ? ", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.Yes)
                {

                    cmd = new SqlCommand("spacex_pro", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SatelliteID", txt_id.Text);
                    cmd.Parameters.AddWithValue("@SatelliteName", "");
                    cmd.Parameters.AddWithValue("@Longitude", 0);
                    cmd.Parameters.AddWithValue("@Latitude", 0);
                    cmd.Parameters.AddWithValue("@Elevation", 0);
                    cmd.Parameters.AddWithValue("@HealthStatus", "");
                    cmd.Parameters.AddWithValue("@OperationType", "3");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetAllSatelliteRecord();
                    txt_id.Text = "";
                    txt_sat_name.Text = "";
                    txt_long.Text = "";
                    txt_lat.Text = "";
                    txt_ele.Text = "";
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter employee id", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != string.Empty || txt_sat_name.Text != string.Empty || txt_long.Text != string.Empty || txt_lat.Text != string.Empty || txt_ele.Text != string.Empty)
            {
                txt_id.Text = "";
                txt_sat_name.Text = "";
                txt_long.Text = "";
                txt_lat.Text = "";
                txt_ele.Text = "";
                comboBox1.Text = default;
            }
        }
    }
}
