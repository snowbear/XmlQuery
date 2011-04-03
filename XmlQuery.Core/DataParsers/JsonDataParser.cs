using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using XmlQuery.Entities;

namespace XmlQuery.DataParsers
{
    public class JsonDataParser
    {
        private static readonly Dictionary<JTokenType, Func<JValue, Data>> AttributeCreators;

        static JsonDataParser()
        {
            AttributeCreators = new Dictionary<JTokenType, Func<JValue, Data>>
                                    {
                                        {JTokenType.String, o => new Attribute<string> {Value = (string) o.Value}},
                                        {JTokenType.Integer, o => new Attribute<int> {Value = (int) (long) o.Value}},
                                    };
        }

        public Data Parse(string input)
        {
            var jObject = JObject.Parse(input);
            var result = new Node {Name = "Data"};
            var children = ReadNodeChildren(jObject);
            foreach (var child in children)
                result.Children.Add(child);
            return result;
        }

        private static IEnumerable<Data> ReadNodeChildren(JObject jObject)
        {
            var children = new List<Data>();
            foreach (var child in jObject)
            {
                if (child.Value is JValue)
                {
                    var attribute = CreateAttribute(child.Key, (JValue) child.Value);
                    children.Add(attribute);
                }
                else if (child.Value is JObject)
                {
                    var nestedChildren = ReadNodeChildren((JObject) child.Value);
                    var node = new Node {Name = child.Key};
                    foreach (var nestedChild in nestedChildren)
                        node.Children.Add(nestedChild);
                    children.Add(node);
                }
                else
                    throw new Exception("Unexpected type: " + child.Value.GetType());
            }
            return children;
        }

        private static Data CreateAttribute(string name, JValue jValue)
        {
            Func<JValue, Data> attributeCreator;
            if (!AttributeCreators.TryGetValue(jValue.Type, out attributeCreator))
                throw new Exception(string.Format("Attribute name: {0}; Unexpected attribute type: {1}", name, jValue.Type.GetType()));
            var attribute = attributeCreator(jValue);
            attribute.Name = name;
            return attribute;
        }
    }
}