using System;
using System.Collections.Generic;
using System.Linq;
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
            return ConvertNode("Data", jObject);
        }

        private static Data ConvertNode(string name, JToken jtoken)
        {
            if (jtoken is JValue)
            {
                return CreateAttribute(name, (JValue)jtoken);
            }
            if (jtoken is JObject)
            {
                var jobject = (JObject) jtoken;
                return new Node(name, jobject.Cast<KeyValuePair<string, JToken>>().Select(j => ConvertNode(j.Key, j.Value)));
            }
            if (jtoken is JArray)
            {
                var jarray = (JArray)jtoken;
                return new Node(name, jarray.Select(j => ConvertNode("Item", j)));
            }
            throw new Exception("Unexpected type: " + jtoken.GetType());
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