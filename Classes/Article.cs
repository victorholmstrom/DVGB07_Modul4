using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVGB07_Modul4.Classes
{
    public class Article
    {
        //Article properties as a Dictonairy (MAP) datatype
        public Dictionary<string, string> attribute { get; set; }

        public Article()
        {
            attribute = new Dictionary<string, string>();
        }
        //Method to add/change a property
        public void setAttributeValue(string key, string value)
        {
            if (key != "")
                attribute[key] = value;
        }
        //Method to retrieve a property value
        public string getAttributeValue(string key)
        {
            return attribute[key];
        }
    }

}
