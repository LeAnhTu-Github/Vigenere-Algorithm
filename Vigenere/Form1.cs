using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vigenere
{
    public partial class Form1 : Form
    {
        StringBuilder key = new StringBuilder();
        char[] ASCII= {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
        'w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z',' ','~','`','!','@','#','$','%','^','&','*','(',')','-',
        '_','+','=','|','\\','\"','\n','\t','{','[',']','}',':',';','\'','<',',','.','>','?',
        '/','0','1','2','3','4','5','6','7','8','9'
    };
        public Form1()
        {
            InitializeComponent();

        }
        private void randomKey(object sender, EventArgs e)
        {
            if (txtLength.Text.Length == 0)
            {
                MessageBox.Show("Nếu bạn muốn random khóa.Vui lòng cung cấp độ dài khóa.", "Lỗi");
                return;
            }
            int doDai = Convert.ToInt32(txtLength.Text);
            Random rand = new Random();
            key.Clear();
            for (int i = 0; i < doDai; i++)
            {
                int randomNumber = rand.Next(ASCII.Length);    
                char randomChar = ASCII[randomNumber];    
                key.Append(randomChar);  
            }
            txtkey.Text = key.ToString();  
        }

        private StringBuilder GetKey()
        {
            return key;
        }
        private void btnEncrypt(object sender, EventArgs e)
        {
            // Kiem tra neu khong co khoa
            if (txtkey.Text.Length == 0)
            {
                MessageBox.Show("Khóa không được trống. Vui lòng tạo khóa trước khi mã hóa.", "Lỗi");
                return;
            }

            // Kiểm tra xem khóa có hợp lệ không
            foreach (char c in txtkey.Text)
            {
                // Nếu ký tự không nằm trong mảng ASCII, trả về false
                if (!ASCII.Contains(c))
                {
                    MessageBox.Show("Khóa chứa ký tự không hợp lệ. Vui lòng chỉ sử dụng các ký tự hợp lệ.", "Lỗi");
                    return;
                }
            }
            key.Clear();
            key.Append(txtkey.Text);

            string vanBanRo = txtInput.Text;

            StringBuilder vanBanMaHoa = new StringBuilder();


            for (int i = 0; i < vanBanRo.Length; i++)
            {
                char kiTuVanBan = vanBanRo[i];
                char kiTuKhoa = key[i % key.Length];


                int indexVanBan = Array.IndexOf(ASCII, kiTuVanBan);
                int indexKhoa = Array.IndexOf(ASCII, kiTuKhoa);

                // Nếu ký tự không nằm trong mảng, bỏ qua
                if (indexVanBan == -1 || indexKhoa == -1)
                {
                    vanBanMaHoa.Append(kiTuVanBan);  // Giữ nguyên ký tự không mã hóa
                }
                else
                {
                    // Mã hóa: tính chỉ số mới trong mảng ASCII
                    int indexMaHoa = (indexVanBan + indexKhoa) % 97;
                    vanBanMaHoa.Append(ASCII[indexMaHoa]);  // Thêm ký tự mã hóa vào chuỗi kết quả
                }
            }

            txtOutput.Text = vanBanMaHoa.ToString();  // Xuất kết quả mã hóa
        }

        private void button5_Click(object sender, EventArgs e) { 
                if (key.Length == 0)
                {
                    MessageBox.Show("Khóa không được trống. Vui lòng tạo khóa trước khi giải mã.", "Lỗi");
                    return; 
                }
                // Kiểm tra xem khóa có hợp lệ không
                foreach (char c in txtkey.Text)
                {
                    // Nếu ký tự không nằm trong mảng ASCII, trả về false
                    if (!ASCII.Contains(c))
                    {
                        MessageBox.Show("Khóa chứa ký tự không hợp lệ. Vui lòng chỉ sử dụng các ký tự hợp lệ.", "Lỗi");
                        return;
                    }
                }
                key.Clear();
                key.Append(txtkey.Text);

            string vanBanMaHoa = txtOutput.Text; 

                StringBuilder vanBanGiaiMa = new StringBuilder();

     
                for (int i = 0; i < vanBanMaHoa.Length; i++)
                {
                    char kiTuMaHoa = vanBanMaHoa[i];  
                    char kiTuKhoa = key[i % key.Length];  // Ký tự của khóa tương ứng (lặp lại khóa nếu cần)
                    int indexMaHoa = Array.IndexOf(ASCII, kiTuMaHoa);
                    int indexKhoa = Array.IndexOf(ASCII, kiTuKhoa);


                    if (indexMaHoa == -1 || indexKhoa == -1)
                    {
                        vanBanGiaiMa.Append(kiTuMaHoa); 
                    }
                    else
                    {
                        // Giải mã: tính chỉ số mới trong mảng ASCII
                        int indexGiaiMa = (indexMaHoa - indexKhoa + 97) % 97;
                        vanBanGiaiMa.Append(ASCII[indexGiaiMa]);  // Thêm ký tự đã giải mã vào chuỗi kết quả
                    }
                }

                txtInput.Text = vanBanGiaiMa.ToString();  // Xuất kết quả giải mã
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClearPlantext(object sender, EventArgs e)
        {
            txtInput.Clear();
        }

        private void btnClearCodetext(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        
    }
}

