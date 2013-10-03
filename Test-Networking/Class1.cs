using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using RusticiSoftware.TinCanAPILibrary;
using RusticiSoftware.TinCanAPILibrary.Model;


namespace Test_Networking
{
    public class SynchronousSocketClient
    {

        public static void JSObject()
        {
            Console.WriteLine("Start\n\n");

            //TCAPI tincan = new TCAPI(new Uri("http://35.9.22.105:8000/xapi"), new BasicHTTPAuth("Dave", "12345"));
            Statement[] statements = new Statement[1];
            Activity newAct = new Activity("http://www.google.com");
            newAct.Definition = new ActivityDefinition();
            newAct.Definition.Name = new LanguageMap();
            newAct.Definition.Name.Add("en-US", "Testing");
            statements[0] = new Statement(new Actor("Guess Who", "mailto:guesswho"), new StatementVerb(PredefinedVerbs.Created), newAct);
            TCAPI tincan = new TCAPI(new Uri("http://35.9.22.105:8000/xapi"), new BasicHTTPAuth("Joe", "123"), RusticiSoftware.TinCanAPILibrary.Helper.TCAPIVersion.TinCan1p0p0);
            tincan.StoreStatements(statements);

            Console.WriteLine("End\n");

        }
    }
}
