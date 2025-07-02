using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RT7216_temperature_compensation_v8.Classes
{
    public partial class COMPORT : UserControl
    {
        public event EventHandler ClickHandler;
        public event EventHandler HoverHandler;

        public COMPORT()
        {
            InitializeComponent();

            tableLayoutPanel1.Click += COMPORT_Click;
            label1.Click += COMPORT_Click;
            label3.Click += COMPORT_Click;
            label5.Click += COMPORT_Click;
            label_DevID.Click += COMPORT_Click;
            label_Description.Click += COMPORT_Click;
            //label_PNPDevID.Click += COMPORT_Click;
                
        }

        private void COMPORT_Click(object sender, EventArgs e)
        {
            ClickHandler.Invoke(this, new EventArgs());
        }

        private void COMPORT_MouseHover(object sender, EventArgs e)
        {
            HoverHandler.Invoke(this, new EventArgs());
        }


    }
}
