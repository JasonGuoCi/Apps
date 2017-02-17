using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class JsonHandler
    {
        public class JsonMessage
        {
            public int type { get; set; }
            public string message { get; set; }
            public string value { get; set; }
        }

        public static JsonMessage CreateMessage(int pType, string pMessage, string pValue)
        {
            JsonMessage json = new JsonMessage()
            {
                type = pType,
                message = pMessage,
                value = pValue
            };
            return json;
        }

        public static JsonMessage CreateMessage(int pType, string pMessage)
        {
            JsonMessage json = new JsonMessage()
            {
                type = pType,
                message = pMessage
            };

            return json;
        }
    }
}
