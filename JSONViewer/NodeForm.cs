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
        private bool canRemove;

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
        public bool CanRemove
        {
            get
            {
                return canRemove;
            }
            set
            {
                canRemove = value;
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

            if (txtNodeText.Text != string.Empty)
            {
                NewNodeText = txtNodeText.Text;
            }
            else
            {
                MessageBox.Show("Provide the new node's text");
                return;
            }
            CanRemove = false;

            this.Close();
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            panelAddNode.Visible = true;
        }

        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            CanRemove = true;
            this.Close();
        }
    }
}
