using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            catch (Exception)
            {
                MessageBox.Show("Invalid JSON String");
            }
        }

        private void btnTreeToJson_Click(object sender, EventArgs e)
        {
            var rootElement = new XElement("JSON", CreateXmlElement(treeViewOutput.Nodes));
            var document = new XDocument(rootElement);
            string tempJson = JsonConvert.SerializeXNode(document.Root.LastNode, Newtonsoft.Json.Formatting.Indented, true);
            JObject obj = JObject.Parse(tempJson);
            var objCopy = ConvertTypeJSON(obj);
            txtInput.Text = objCopy.ToString();
        }

        private JToken ConvertTypeJSON(JToken obj)
        {
            JObject objCopy = new JObject();
            if (obj is JValue)
            {
                int num; DateTime dt;
                if (int.TryParse(obj.ToString(), out num))
                {
                    obj = num;
                }
                else if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    obj = dt;
                }
                return obj;
            }
            foreach (var x in obj)
            {
                if (x is JProperty)
                {
                    int num; DateTime dt;
                    JProperty newProp = (JProperty)x;
                    if (int.TryParse(((JProperty)x).Value.ToString(), out num))
                    {
                        newProp = new JProperty(((JProperty)x).Name, num);
                    }
                    else if (DateTime.TryParse(((JProperty)x).Value.ToString(), out dt))
                    {
                        newProp = new JProperty(((JProperty)x).Name, dt);
                    }
                    else if (((JProperty)x).Value is JObject)
                    {
                        newProp = new JProperty(((JProperty)x).Name, ConvertTypeJSON(((JProperty)x).Value));
                    }
                    else if (((JProperty)x).Value is JArray)
                    {
                        JArray newJArray = new JArray();
                        foreach (var item in ((JProperty)x).Value)
                        {
                            newJArray.Add(ConvertTypeJSON(item));
                            newProp = new JProperty(((JProperty)x).Name, newJArray);
                        }
                    }
                    objCopy.Add(newProp);
                }
            }
            return objCopy;
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
            int imageIndex = 0;
            if (name.StartsWith("["))
                imageIndex = 5;
            var node = new TreeNode(name, imageIndex, imageIndex);
            parent.Add(node);

            foreach (var property in @object.Properties())
            {
                AddTokenNodes(property.Value, property.Name, node.Nodes);
            }
        }

        private void AddArrayNodes(JArray array, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name, 1, 1);
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
                int imageIndex = 4;
                if (((JValue)token).Value != null)
                {
                    imageIndex = 2;
                    if (((JValue)token).Value is Int64 || ((JValue)token).Value is Double)
                        imageIndex = 3;
                    else if (name.StartsWith("["))
                        imageIndex = 5;
                    parent.Add(new TreeNode(string.Format("{0}={1}", name, ((JValue)token).Value), imageIndex, imageIndex));
                }
                else
                    parent.Add(new TreeNode(string.Format("{0}", name), imageIndex, imageIndex));
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
                        {
                            element.Value = treeViewNode.Text.Split('=')[1];
                        }
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
                catch (Exception)
                {

                }
            }
            return elements;
        }
        #endregion Helpers                    
    }
}
