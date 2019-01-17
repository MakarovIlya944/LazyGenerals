using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LazyGeneral
{
    public partial class IpConfig : Form
    {
        string Ip = "empty";

        public IpConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //на веру
            Ip = textBox1.Text;

            Close();
        }

        public void GetIp(out string s)
        {
            s = Ip;
        }
    }
}
