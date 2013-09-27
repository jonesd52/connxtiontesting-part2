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

        public static void StartClient() {
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];

        // Connect to a remote device.
        try {
            // Establish the remote endpoint for the socket.
            // This example uses port 11000 on the local computer.
//            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("35.9.22.105");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress,8000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp );

            // Connect the socket to the remote endpoint. Catch any errors.
            try {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}\n",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);

                Console.WriteLine("Hey Hey Hey Hey");

                //JSObject();

                Console.WriteLine("Did I get here?");

                // Receive the response from the remote device.
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes,0,bytesRec));

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                
            } catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
            } catch (SocketException se) {
                Console.WriteLine("SocketException : {0}",se.ToString());
            } catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        } catch (Exception e) {
            Console.WriteLine( e.ToString());
        }
    }

        public static int Startup(String[] args)
        {
            StartClient();
            return 0;
        }

        public static void JSObject()
        {
            Console.WriteLine("Hey Hey!\n\n");

            //TCAPI tincan = new TCAPI(new Uri("http://35.9.22.105:8000/xapi"), new BasicHTTPAuth("Dave", "12345"));
            Statement[] statements = new Statement[1];
            Activity newAct = new Activity("http://www.google.com");
            newAct.Definition = new ActivityDefinition();
            newAct.Definition.Name = new LanguageMap();
            newAct.Definition.Name.Add("en-US", "Testing");
            statements[0] = new Statement(new Actor("Guess Who", "mailto:guesswho@who.com"), new StatementVerb(PredefinedVerbs.Created), newAct);
            TCAPI tincan = new TCAPI(new Uri("http://35.9.22.105:8000/xapi"), new BasicHTTPAuth("Dave", "12345"));
            tincan.StoreStatements(statements);

            Console.WriteLine("Fucking Douchenozzle\n");


    
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://35.9.22.105:8000/xapi");
/*            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cloud.scorm.com/ScormEngineInterface/TCAPI/public/");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            Console.WriteLine(httpWebRequest.Address);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "Whatwhatwhat";
                string json =
"{\n" +
"    \"actor\": {\n" +
"        \"mbox\": \"mailto:anyong@anyong.net\",\n" +
"        \"name\": \"Anyong\",\n" +
"        \"objectType\": \"Agent\"\n" +
"    },\n" +
"    \"verb\": {\n" +
"        \"id\": \"http://adlnet.gov/expapi/verbs/experienced\",\n" +
"        \"display\": {\n" +
"            \"en-US\": \"experienced\"\n" +
"        }\n" +
"    },\n" +
"    \"object\": {\n" +
"        \"id\": \"http://www.example.com/tincan/activities/EvoKnRYP\",\n" +
"        \"objectType\": \"Activity\",\n" +
"        \"definition\": {\n" +
"            \"name\": {\n" +
"                \"en-US\": \"Example Activity\"\n" +
"            },\n" +
"            \"description\": {\n" +
"                \"en-US\": \"Example activity definition\"\n" +
"            }\n" +
"        }\n" +
"    }\n" +
"}\n";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                Console.WriteLine(json);

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

            
            }*/
        }
    }
}
