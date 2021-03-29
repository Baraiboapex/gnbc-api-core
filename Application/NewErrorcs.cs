using System;
using System.Collections;

namespace Application
{
    public class NewError : Exception
    {
        private Hashtable Error;

        public NewError()
        {
            Error = new Hashtable()
            {
                {"Code",0},
                {"Message",""}
            };
        }

        public void AddValue(int code, string message)
        {
            Error["Code"] = code;
            Error["Message"]  = message;
        }

        public Hashtable GetError()
        {
            return Error;
        }
    }
}