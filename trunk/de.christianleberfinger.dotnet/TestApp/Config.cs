using System;
using System.Collections.Generic;
using System.Text;
using de.christianleberfinger.dotnet.configuration;

namespace TestApp
{
    public class Config : Configuration<Config>
    {
        int myInteger;
        string myString;

        public string MyString
        {
            get { return myString; }
            set { myString = value; }
        }

        public int MyInteger
        {
            get { return myInteger; }
            set { myInteger = value; }
        }
    }
}
