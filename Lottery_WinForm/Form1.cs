using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lottery_WinForm
{
    public partial class ThisGo : Form
    {
        public ThisGo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_Btn_Click(object sender, EventArgs e)
        {
            string cookie = Cookie_TxtBox.Text;
            if (!string.IsNullOrWhiteSpace(cookie))
            {

            }
            else
            {
                MessageBox.Show("请输入Cookie值!", "提示");
            }
        }

    }
}
