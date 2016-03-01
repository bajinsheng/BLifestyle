using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BLifestyle
{
    class JosnCommon
    {
        public static information SelectDictionary<information>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(information));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            information obj = (information)serializer.ReadObject(ms);
            ms.Dispose();
            return obj;
        }
    }
}
