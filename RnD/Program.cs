using System;
using System.Reflection;
using System.Collections.Generic;
using RnD.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RnD
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempData = string.Empty;
            string tempMethod = string.Empty;

            var jsonOpenAPITemplate = File.ReadAllText(@"C:\Users\Working Folder\Projects\RnD\RnD\OpeAPITemplate.json");
            JObject objOpenAPITemplate = JObject.Parse(jsonOpenAPITemplate);

            objOpenAPITemplate["id"] = "lalit.nailwal@rsystems.com";
            objOpenAPITemplate["userid"] = "lalit.nailwal@rsystems.com";
            objOpenAPITemplate["APIEndPoint"] = "REST";
            objOpenAPITemplate["ConnectorName"] = "Calculator";
            objOpenAPITemplate["HostName"] = "www.dneonline.com/calculator.asmx";
            File.WriteAllText(@"C:\Users\Working Folder\Projects\RnD\RnD\OpenAPIOutput.json", objOpenAPITemplate.ToString());


            Type type = typeof(CalculatorSoap);
            PropertyInfo[] propertyInfo = type.GetProperties();

            MethodInfo[] methodInfo = type.GetMethods();

            foreach (MethodInfo method in methodInfo)
            {
                tempMethod = method.Name.ToString();
                
                JObject tempActionJobject = new JObject();
                tempActionJobject.Add("Verbs", "POST");
                tempActionJobject.Add("OperationId", method.Name);
                tempActionJobject.Add("APIName", method.Name);
                tempActionJobject.Add("Header", "[\"text/plain\",\"application/json\",\"text/json\"]");
                tempActionJobject.Add("Body", "[{\"in\": \"path\",\"name\": \"q\",\"required\": true,\"type\": \"string\"}]");
               
                AddActionToJson(tempMethod, tempActionJobject.ToString());
            }

            var jsonOpenAPITemplate1 = File.ReadAllText(@"C:\Users\Working Folder\Projects\RnD\RnD\OpenAPIOutput.json");
            Console.WriteLine("\n\n" + jsonOpenAPITemplate1);
        }

        public static void AddActionToJson(string Action, string data)
        {
            var jsonOpenAPIOutput = File.ReadAllText(@"C:\Users\Working Folder\Projects\RnD\RnD\OpenAPIOutput.json");

            JObject objOpenAPIOutput = JObject.Parse(jsonOpenAPIOutput);
            JObject paths = (JObject)objOpenAPIOutput["actions"];
            JToken t1 = JToken.Parse(data);
            paths.Add(Action, t1);
            var jsonText = objOpenAPIOutput.ToString();
            File.WriteAllText(@"C:\Users\Working Folder\Projects\RnD\RnD\OpenAPIOutput.json", jsonText);

           
        }
    }
}
