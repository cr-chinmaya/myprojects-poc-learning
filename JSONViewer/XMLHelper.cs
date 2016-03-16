using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JSONViewer
{
    public class XMLHelper
    {
        /// <summary>
        /// Creates XML from TreeNodeCollection
        /// </summary>
        /// <param name="treeViewNodes"></param>
        /// <returns></returns>
        public List<XElement> CreateXmlElement(TreeNodeCollection treeViewNodes)
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
                        if (treeViewNode.Parent.GetNodeCount(false) == 1)
                            elements.Add(new XElement(treeViewNode.Parent.Text, ""));
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
                        if (treeViewNode.GetNodeCount(true) == 0 && treeViewNode.Text.Split('=').ToList().Count > 1)
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
                    //Do Nothing
                }
            }
            return elements;
        }
    }
}
