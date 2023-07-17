using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace nhom._10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Model1 contex=new Model1();

        List<NHANVIEN> listnhanvien;

        private void Form1_Load(object sender, EventArgs e)
        {
            List<PHONGBAN> listphongban=contex.PHONGBANs.ToList();
            List<NHANVIEN>listnhanvien=contex.NHANVIENs.ToList();

           
            fillphongbancombobox(listphongban);
             adddsnhanvien(listnhanvien);
            dtngaysinh.Format = DateTimePickerFormat.Custom;
            dtngaysinh.CustomFormat = "yyyy-MM-dd";
            dtngaysinh.Value = DateTime.Now;

  

        }

        private void fillphongbancombobox(List<PHONGBAN> listphongban) 
        {
            this.cbphongban.DataSource = listphongban;
            this.cbphongban.DisplayMember = "TENPB";
            this.cbphongban.ValueMember = "MAPB";

        }

        private void adddsnhanvien(List<NHANVIEN> listnhanvien)
        {
            listView1.Items.Clear();
            foreach (NHANVIEN item in listnhanvien)
            {
                ListViewItem listitem = new ListViewItem(item.MANV);
                listitem.SubItems.Add(item.TENNV);
                listitem.SubItems.Add(item.NGAYSINH.ToString());
                if (item.PHONGBAN != null)
                {
                    listitem.SubItems.Add(item.PHONGBAN.TENPB);
                }
                listView1.Items.Add(listitem);

            }

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            NHANVIEN nv = new NHANVIEN();

            nv.MANV = txtmanhanvien.Text;
            nv.TENNV = txttennhanvien.Text;

            if (DateTime.TryParseExact(dtngaysinh.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
            {
                nv.NGAYSINH = ngaySinh;

                if (cbphongban.SelectedValue != null)
                {
                    nv.MAPB = cbphongban.SelectedValue.ToString();
                }
                else
                {
                    nv.MAPB = string.Empty;
                }

                contex.NHANVIENs.Add(nv);
                contex.SaveChanges();
                listnhanvien = contex.NHANVIENs.ToList();
                adddsnhanvien(listnhanvien);
            }
            else
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập lại theo định dạng yyyy-MM-dd.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        int itemselect = 0;
        private void btnsua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedRow = listView1.SelectedItems[0];

                // Lấy giá trị từ các SubItems của hàng được chọn
                string id = selectedRow.SubItems[0].Text;
                string tenNV = selectedRow.SubItems[1].Text;
                string ngaySinhStr = selectedRow.SubItems[2].ToString();
                string tenPB = selectedRow.SubItems[3].Text;

                // Gán giá trị từ SubItems vào các điều khiển tương ứng trong Form
                txtmanhanvien.Text = id;
                txttennhanvien.Text = tenNV;

                DateTime ngaySinh;
                if (DateTime.TryParseExact(ngaySinhStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
                {
                    dtngaysinh.Value = ngaySinh;
                }
                else
                {
                    // Xử lý lỗi không phân tích được giá trị ngày
                    MessageBox.Show("Giá trị ngày không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng xử lý nếu có lỗi
                }

                cbphongban.Text = tenPB;
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedRow = listView1.SelectedItems[0];
                string id = selectedRow.SubItems[0].Text;

                NHANVIEN delete = contex.NHANVIENs.FirstOrDefault(p => p.MANV == id);
                if (delete != null)
                {
                    contex.NHANVIENs.Remove(delete);
                    contex.SaveChanges();
                }

                listnhanvien = contex.NHANVIENs.ToList();
                adddsnhanvien(listnhanvien);
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat=MessageBox.Show("bạn có muốn thoát hay không !","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (thoat == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
