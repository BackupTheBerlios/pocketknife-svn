using System;
using System.Collections.Generic;
using System.Text;
using de.christianleberfinger.dotnet.pocketknife.configuration;

namespace TestApp
{
    public class Config : Configuration<Config>
    {
        public Config() { }

        public int myInteger = 5555;
        public string myString = "Test-String";

        //public string MyString
        //{
        //    get { return myString; }
        //    set { myString = value; }
        //}

        //public int MyInteger
        //{
        //    get { return myInteger; }
        //    set { myInteger = value; }
        //}
    }
}
