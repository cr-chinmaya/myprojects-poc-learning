using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JSONViewer
{
    public partial class Form1 : Form
    {
        string inputString = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void myMenuItemAdd_Click(object sender, EventArgs e)
        {
            NodeForm n = new NodeForm();
            n.ShowDialog();
            TreeNode nod;
            if (!string.IsNullOrEmpty(n.NewNodeName))
            {
                int imageIndex = 4;
                if (!string.IsNullOrEmpty(n.NewNodeText))
                {
                    imageIndex = 2;
                    int numberInt; double numberDouble;
                    if (int.TryParse(n.NewNodeText, out numberInt) || (double.TryParse(n.NewNodeText, out numberDouble)))
                    {
                        imageIndex = 3;
                    }
                    else if (n.NewNodeName.StartsWith("["))
                    {
                        imageIndex = 5;
                    }
                    nod = new TreeNode(string.Format("{0}={1}", n.NewNodeName.ToString(), n.NewNodeText.ToString()), imageIndex, imageIndex);
                    nod.Name = n.NewNodeName.ToString();
                    treeViewOutput.SelectedNode.Nodes.Add(nod);
                }
                else
                {
                    nod = new TreeNode(string.Format("{0}", n.NewNodeName.ToString()), imageIndex, imageIndex);
                    nod.Name = n.NewNodeName.ToString();
                    treeViewOutput.SelectedNode.Nodes.Add(nod);
                }
                treeViewOutput.SelectedNode.ExpandAll();
            }
            n.Close();
        }

        private void myMenuItemRemove_Click(object sender, EventArgs e)
        {
            if (treeViewOutput.SelectedNode != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove the node " + treeViewOutput.SelectedNode.ToString() + "?", "Remove Node", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    treeViewOutput.SelectedNode.Remove();
                }
            }
        }

        private void treeViewOutput_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                treeViewOutput.SelectedNode = treeViewOutput.GetNodeAt(e.X, e.Y);
                contextMenuStrip.Show(treeViewOutput, e.Location);
            }
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            inputString = txtInput.Text;
            try
            {
                JSONHelper jsonHelper = new JSONHelper();
                if (jsonHelper.IsJObject(inputString))
                {
                    JObject input = JObject.Parse(inputString);
                    txtInput.Text = JsonConvert.SerializeObject(input, Newtonsoft.Json.Formatting.Indented);
                }
                else if (jsonHelper.IsJArray(inputString))
                {
                    JArray input = JArray.Parse(inputString);
                    txtInput.Text = JsonConvert.SerializeObject(input, Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    throw new Exception("Invalid String");
                }
            }
            catch
            {
                MessageBox.Show("Invalid JSON String");
            }
        }

        private void btnRemoveWhiteSpace_Click(object sender, EventArgs e)
        {
            inputString = txtInput.Text;
            try
            {
                JSONHelper jsonHelper = new JSONHelper();
                if (jsonHelper.IsJObject(inputString))
                {
                    JObject input = JObject.Parse(inputString);
                    txtInput.Text = JsonConvert.SerializeObject(input);
                }
                else if (jsonHelper.IsJArray(inputString))
                {
                    JArray input = JArray.Parse(inputString);
                    txtInput.Text = JsonConvert.SerializeObject(input);
                }
                else
                {
                    throw new Exception("Invalid String");
                }                
            }
            catch
            {
                MessageBox.Show("Invalid JSON String");
            }
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            treeViewOutput.ExpandAll();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            treeViewOutput.CollapseAll();
        }

        private void btnJsonToTree_Click(object sender, EventArgs e)
        {
            try
            {
                inputString = txtInput.Text;
                treeViewOutput.Nodes.Clear();
                JSONHelper jsonHelper = new JSONHelper();
                jsonHelper.LoadJsonToTreeView(treeViewOutput, inputString);
                treeViewOutput.ExpandAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid JSON String");
            }
        }

        private void btnTreeToJson_Click(object sender, EventArgs e)
        {
            try
            {
                XMLHelper xmlHelper = new XMLHelper();
                var rootElement = new XElement("JSON", xmlHelper.CreateXmlElement(treeViewOutput.Nodes));
                var document = new XDocument(rootElement);
                if (document.Root != null)
                {
                    string tempJson = JsonConvert.SerializeXNode(document.Root, Newtonsoft.Json.Formatting.Indented, true);
                    JObject obj = JObject.Parse(tempJson);
                    JSONHelper jsonHelper = new JSONHelper();
                    var objCopy = jsonHelper.ConvertTypeJSON(obj);
                    int indexOfRoot = (objCopy.ToString()).IndexOf("JSON");
                    txtInput.Text = (objCopy.ToString()).Substring(indexOfRoot + 7).TrimEnd('}');
                }
            }
            catch
            {
                MessageBox.Show("Failed to Parse TreeView!");
            } 
            
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Undo();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.SelectedText = string.Empty;
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.SelectAll();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewOutput.SelectedNode.Expand();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewOutput.SelectedNode.ExpandAll();
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewOutput.SelectedNode.Collapse();
        }

        private void copyNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(treeViewOutput.SelectedNode.Text);
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (treeViewOutput.SelectedNode.IsExpanded)
            {
                expandToolStripMenuItem.Enabled = false;
                expandAllToolStripMenuItem.Enabled = false;
                collapseToolStripMenuItem.Enabled = true;
            }
            else
            {
                expandToolStripMenuItem.Enabled = true;
                expandAllToolStripMenuItem.Enabled = true;
                collapseToolStripMenuItem.Enabled = false;
            }
            if(treeViewOutput.SelectedNode.FirstNode == null)
            {
                expandToolStripMenuItem.Enabled = false;
                expandAllToolStripMenuItem.Enabled = false;
                collapseToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStripForTxtBox_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.SelectedText))
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            else
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
        }
        #endregion Event Handlers       
    }
}
