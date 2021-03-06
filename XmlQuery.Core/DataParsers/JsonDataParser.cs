using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Newtonsoft.Json.Linq;
using XmlQuery.Entities;

namespace XmlQuery.DataParsers
{
    public class JsonDataParser
    {
        private static readonly Dictionary<JTokenType, Func<string, JValue, Data>> AttributeCreators;

        static JsonDataParser()
        {
            AttributeCreators = new Dictionary<JTokenType, Func<string, JValue, Data>>
                                    {
                                        {JTokenType.String, (name, o) => new DataAttribute<string>(name, (string) o.Value)},
                                        {JTokenType.Integer, (name, o) => new DataAttribute<int>(name, (int) (long) o.Value)},
                                    };
        }

        public Data Parse(string input)
        {
            Contract.Requires(input != null);

            var jObject = JObject.Parse(input);
            Contract.Assume(jObject != null);

            return ConvertNode("Data", jObject);
        }

        private static Data ConvertNode(string name, JToken jtoken)
        {
            Contract.Requires(name != null);
            Contract.Requires(jtoken != null);

            if (jtoken is JValue)
            {
                return CreateAttribute(name, (JValue)jtoken);
            }
            if (jtoken is JObject)
            {
                var jobject = (JObject) jtoken;
                return new DataNode(name, jobject.Cast<KeyValuePair<string, JToken>>().Select(j => ConvertNode(j.Key, j.Value)));
            }
            if (jtoken is JArray)
            {
                var jarray = (JArray)jtoken;
                return new DataNode(name, jarray.Select(j => ConvertNode("Item", j)));
            }
            throw new Exception("Unexpected type: " + jtoken.GetType());
        }

        private static Data CreateAttribute(string name, JValue jValue)
        {
            Contract.Requires(name != null);
            Contract.Requires(jValue != null);

            Func<string, JValue, Data> attributeCreator;
            if (!AttributeCreators.TryGetValue(jValue.Type, out attributeCreator))
                throw new Exception(string.Format("DataAttribute name: {0}; Unexpected attribute type: {1}", name, jValue.Type.GetType()));

            Contract.Assume(attributeCreator != null);

            var attribute = attributeCreator(name, jValue);
            return attribute;
        }
    }
}