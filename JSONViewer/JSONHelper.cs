using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONViewer
{
    public class JSONHelper
    {
        /// <summary>
        /// Forms the Tree Nodes from JSON string by calling AddObjectNode method
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="json"></param>
        public void LoadJsonToTreeView(TreeView treeView, string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }
            if (IsJObject(json))
            {
                var @object = JObject.Parse(json);
                AddObjectNodes(@object, "JSON", treeView.Nodes);
            }
            else if (IsJArray(json))
            {
                var @array = JArray.Parse(json);                
                AddArrayNodes(@array, "JSON", treeView.Nodes);
            }
            else
            {
                throw new Exception("Invalid String");
            }
        }
        /// <summary>
        /// Checks whether string is JArray
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool IsJArray(string json)
        {
            try
            {
                JArray.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether string is JObject
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool IsJObject(string json)
        {
            try
            {
                JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Forms Object Nodes from given string and adds the nodes to the parent TreeNodeCollection
        /// </summary>
        /// <param name="@object"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
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

        /// <summary>
        /// Forms Array Nodes from given string and adds the nodes to the parent TreeNodeCollection
        /// </summary>
        /// <param name="array"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        private void AddArrayNodes(JArray array, string name, TreeNodeCollection parent)
        {
            var node = new TreeNode(name, 1, 1);
            parent.Add(node);
            for (var i = 0; i < array.Count; i++)
            {
                AddTokenNodes(array[i], string.Format("[{0}]", i), node.Nodes);
            }
        }

        /// <summary>
        /// Forms Token Nodes from given string and adds the nodes to the parent TreeNodeCollection
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <param name="parent"></param>
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

        /// <summary>
        /// Converts the properties of JSON from String to native types
        /// Considered int/Double/Boolean/DateTime 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JToken ConvertTypeJSON(JToken obj)
        {
            JObject objCopy = new JObject();
            if (obj is JValue)
            {
                int num; Double decimalValue; DateTime dt; Boolean flag;
                if (int.TryParse(obj.ToString(), out num))
                {
                    obj = num;
                }
                else if (Double.TryParse(obj.ToString(), out decimalValue))
                {
                    obj = decimalValue;
                }
                else if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    obj = dt;
                }
                else if (Boolean.TryParse(obj.ToString(), out flag))
                {
                    obj = flag;
                }
                return obj;
            }
            foreach (var x in obj)
            {
                if (x is JProperty)
                {
                    int num; Double decimalValue; DateTime dt; Boolean flag;
                    JProperty newProp = (JProperty)x;
                    //Converting string to Int
                    if (int.TryParse(((JProperty)x).Value.ToString(), out num))
                    {
                        newProp = new JProperty(((JProperty)x).Name, num);
                    }
                    //Converting string to Double
                    else if (Double.TryParse(((JProperty)x).Value.ToString(), out decimalValue))
                    {
                        newProp = new JProperty(((JProperty)x).Name, decimalValue);
                    }
                    //Converting string to DateTime
                    else if (DateTime.TryParse(((JProperty)x).Value.ToString(), out dt))
                    {
                        newProp = new JProperty(((JProperty)x).Name, dt);
                    }
                    //Converting string to Boolean
                    else if (Boolean.TryParse(((JProperty)x).Value.ToString(), out flag))
                    {
                        newProp = new JProperty(((JProperty)x).Name, flag);
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
                            if (!string.IsNullOrEmpty(item.ToString()) || item.HasValues)
                                newJArray.Add(ConvertTypeJSON(item));
                            newProp = new JProperty(((JProperty)x).Name, newJArray);
                        }
                    }
                    objCopy.Add(newProp);
                }
            }
            return objCopy;
        }
    }
}
