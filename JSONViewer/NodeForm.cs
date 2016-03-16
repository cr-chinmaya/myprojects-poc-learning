using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONViewer
{
    public partial class NodeForm : Form
    {
        private string mNewNodeName;
        private string mNewNodeText;

        public string NewNodeName
        {
            get
            {
                return mNewNodeName;
            }
            set
            {
                mNewNodeName = value;
            }
        }

        public string NewNodeText
        {
            get
            {
                return mNewNodeText;
            }
            set
            {
                mNewNodeText = value;
            }
        }
        public NodeForm()
        {
            InitializeComponent();
        }

        private void btnNewNodeSubmit_Click(object sender, EventArgs e)
        {
            if (txtNodeName.Text != string.Empty)
            {
                NewNodeName = txtNodeName.Text;
            }
            else
            {
                MessageBox.Show("Name the node.");
                return;
            }

            NewNodeText = txtNodeText.Text;

            this.Close();
        }
    }
}
