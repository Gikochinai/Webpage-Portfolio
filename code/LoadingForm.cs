using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace EDP_Project
{
    public partial class LoadingForm : Form
    {
        public int loadGauge = 0;
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        void LoadingProcess() {
            while (loadGauge <= 100) {
                Thread.Sleep(500);
                Invoke(new Action(() =>
                {
                    Loading_lbl.Text = loadGauge.ToString();
                    if (loadGauge == 100)
                    {
                        Close();
                    }
                }));
                Console.Write(loadGauge + " ");
                loadGauge = loadGauge + 5;               
            }
        }

        private void Loading_lbl_TextChanged(object sender, EventArgs e)
        {
        }

        private void Proceedbtn_Click(object sender, EventArgs e)
        {
            Thread threadA = new Thread(LoadingProcess);
            threadA.Start();
            //threadA.Join();
        }
    }
}
