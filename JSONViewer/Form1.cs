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
                txtInput.Text = JsonConvert.SerializeObject(input, Formatting.Indented);
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

        }
        #endregion Event Handlers

        #region Helpers
        void LoadJsonToTreeView(TreeView treeView, string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            var @object = JObject.Parse(json);
            AddObjectNodes(@object, "JSON", treeView.Nodes);
        }

        void AddObjectNodes(JObject @object, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name);
            parent.Add(node);

            foreach (var property in @object.Properties())
            {
                AddTokenNodes(property.Value, property.Name, node.Nodes);
            }
        }

        void AddArrayNodes(JArray array, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name);
            parent.Add(node);

            for (var i = 0; i < array.Count; i++)
            {
                AddTokenNodes(array[i], string.Format("[{0}]", i), node.Nodes);
            }
        }

        void AddTokenNodes(JToken token, string name, TreeNodeCollection parent)
        {
            if (token is JValue)
            {
                parent.Add(new TreeNode(string.Format("{0}: {1}", name, ((JValue)token).Value)));
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
        #endregion Helpers

    }
}
