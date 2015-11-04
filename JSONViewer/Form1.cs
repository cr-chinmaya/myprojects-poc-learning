using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
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
        private void btnFormat_Click(object sender, EventArgs e)
        {
            inputString = txtInput.Text;
            try
            {
                JObject input = JObject.Parse(inputString);
                txtInput.Text = JsonConvert.SerializeObject(input, Newtonsoft.Json.Formatting.Indented);
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
                JObject input = JObject.Parse(inputString);
                txtInput.Text = JsonConvert.SerializeObject(input);
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
                LoadJsonToTreeView(treeViewOutput, inputString);
            }
            catch
            {
                MessageBox.Show("Invalid JSON String");
            }
        }

        private void btnTreeToJson_Click(object sender, EventArgs e)
        {
            var rootElement = new XElement("JSON", CreateXmlElement(treeViewOutput.Nodes));
            var document = new XDocument(rootElement);
            txtInput.Text = JsonConvert.SerializeXNode(document.Root.LastNode, Newtonsoft.Json.Formatting.None, true);

        }

        private void treeViewOutput_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeForm n = new NodeForm();
            n.ShowDialog();
            if (n.CanRemove)
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
            else
            {
                TreeNode nod = new TreeNode();
                if (n.NewNodeName != null && n.NewNodeText != null)
                {
                    nod.Name = n.NewNodeName.ToString();
                    nod.Text = string.Format("{0}={1}", n.NewNodeName.ToString(), n.NewNodeText.ToString());
                    treeViewOutput.SelectedNode.Nodes.Add(nod);
                    treeViewOutput.SelectedNode.ExpandAll();
                }
            }            
            n.Close();
        }

        #endregion Event Handlers

        #region Helpers
        private void LoadJsonToTreeView(TreeView treeView, string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            var @object = JObject.Parse(json);
            AddObjectNodes(@object, "JSON", treeView.Nodes);
        }

        private void AddObjectNodes(JObject @object, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name);
            parent.Add(node);

            foreach (var property in @object.Properties())
            {
                AddTokenNodes(property.Value, property.Name, node.Nodes);
            }
        }

        private void AddArrayNodes(JArray array, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name);
            parent.Add(node);
            for (var i = 0; i < array.Count; i++)
            {
                AddTokenNodes(array[i], string.Format("[{0}]", i), node.Nodes);
            }
        }

        private void AddTokenNodes(JToken token, string name, TreeNodeCollection parent)
        {
            if (token is JValue)
            {
                parent.Add(new TreeNode(string.Format("{0}={1}", name, ((JValue)token).Value)));
            }
            else if (token is JArray)
            {
                AddArrayNodes((JArray)token, name, parent);
            }
            else if (token is JObject)
            {
                AddObjectNodes((JObject)token, name, parent);
            }
        }

        private List<XElement> CreateXmlElement(TreeNodeCollection treeViewNodes)
        {
            var elements = new List<XElement>();
            foreach (TreeNode treeViewNode in treeViewNodes)
            {
                try
                {
                    if (treeViewNode.Text.Split('=')[0].StartsWith("[") && treeViewNode.Text.Split('=').ToList().Count > 0)
                    {
                        var childElement = new XElement(treeViewNode.Parent.Text, CreateXmlElement(treeViewNode.Nodes));
                        if (treeViewNode.GetNodeCount(true) == 0 && treeViewNode.Text.Split('=').ToList().Count > 0)
                            childElement.Value = treeViewNode.Text.Split('=')[1];
                        elements.Add(childElement);
                    }
                    else if (treeViewNode.Text.Split('=')[0].StartsWith("[") && treeViewNode.Text.Split('=').ToList().Count == 0)
                    {
                        var childElement = new XElement(treeViewNode.Parent.Text);
                        childElement.Value = treeViewNode.Text.Split('=')[1];
                        elements.Add(childElement);
                    }
                    else
                    {
                        var element = new XElement(treeViewNode.Text.Split('=')[0]);
                        if (treeViewNode.GetNodeCount(true) == 0 && treeViewNode.Text.Split('=').ToList().Count > 0)
                            element.Value = treeViewNode.Text.Split('=')[1];
                        else
                            element.Add(CreateXmlElement(treeViewNode.Nodes));
                        if (element.FirstNode is XElement)
                        {
                            if (Convert.ToString(((XElement)element.FirstNode).Name) == element.Name.ToString())
                            {
                                foreach (var node in element.Nodes())
                                {
                                    elements.Add((XElement)node);
                                }
                            }
                            else
                            {
                                elements.Add(element);
                            }
                        }
                        else
                        {
                            elements.Add(element);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return elements;
        }
        #endregion Helpers    
    }
}
