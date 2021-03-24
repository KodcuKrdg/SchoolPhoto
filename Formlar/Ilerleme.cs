using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shool_Photo.Formlar
{
    public partial class Ilerleme : Form
    {
        public Ilerleme()
        {
            InitializeComponent();
        }
        public void GostergeMax(int deger)
        {
            gosterge.Maximum = deger;
        }
        public void Gosterge(int deger)
        {
            gosterge.Value = deger;
        }
    }
}
