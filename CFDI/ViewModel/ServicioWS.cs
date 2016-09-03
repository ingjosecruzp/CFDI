using CFDI.Model;
using CFDI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CFDI.ViewModel
{
    public class ServicioWS
    {
        //private string RequestUrl;
        private object JSONRequest;
        private string JSONmethod;
        private string JSONContentType;
        private Type JSONResponseType;
        private string ObjWS;
        private string Servicie;
        private string Method;
        private string Url;
        private string requestUrl
        {
            get
            {
                if (this.Url == null)
                    //return "http://10.10.1.86/WsCfdi/" + this.Servicie + "/" + this.Method;
                    return "http://200.52.220.238:89/WsCfdi/" + this.Servicie + "/" + this.Method;
                else
                    return this.Url;

            }
        }
        public string url
        {
            get
            {
                return this.Url;
            }
            set
            {
                this.Url = value;
            }
        }
        public string Servicio
        {
            get
            {
                return this.Servicie;
            }
            set
            {
                this.Servicie = value;
            }
        }
        public string Metodo
        {
            get
            {
                return this.Method;
            }

            set
            {
                this.Method = value;
            }
        }
        public Object ObjetoEnvio
        {
            set
            {
                this.JSONRequest = value;
            }
        }
        public Type TipoObjetoRespuesta
        {
            set
            {
                this.JSONResponseType = value;
            }
        }
        public string Parametros
        {
            set
            {
                this.ObjWS = value;
            }
        }
        public ServicioWS(string url, Object ObjetoEnvio, Type TipoObjetoRespuesta, string Parametros)
        {
            Url = url;
            JSONRequest = ObjetoEnvio;
            JSONmethod = "POST";
            JSONContentType = "application/json";
            JSONResponseType = TipoObjetoRespuesta;
            ObjWS = Parametros;
        }
        public ServicioWS(string Servicio, string Metodo, Object ObjetoEnvio, Type TipoObjetoRespuesta, string Parametros)
        {
            //requestUrl = @"http://10.10.1.86/WsCfdi/"+Servicio+"/"+Metodo;
            Servicie = Servicio;
            Method = Metodo;
            JSONRequest = ObjetoEnvio;
            JSONmethod = "POST";
            JSONContentType = "application/json";
            JSONResponseType = TipoObjetoRespuesta;
            ObjWS = Parametros;
        }
        public async Task<object> PostAsync()
        {
            dynamic content = null;
            using (var client = new HttpClient())
            {
                string text = JsonConvert.SerializeObject(JSONRequest);
                text = string.Concat(new string[]{"{\"",
                    ObjWS,
                    "\":",
                    text,
                    "}"
                });

                var response = await client.PostAsync(requestUrl,
                    new StringContent(text,
                        Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                     content = JsonConvert.DeserializeObject(
                        response.Content.ReadAsStringAsync()
                        .Result,JSONResponseType);

                    // Access variables from the returned JSON object
                    var appHref = content.links.applications.href;
                }
            }
            return content;
        }

        public object Peticion()
        {
            Thread newWindowThread;
            object result=null;
            try
            {
                ProgressBarView Barra = new ProgressBarView();
                GlobalsVariables.Peticion = true;
                // When the window closes, shut down the dispatcher
                Barra.Topmost = true;
                Barra.Show();
                newWindowThread = new Thread(new ThreadStart(() =>
                {
                    // Create our context, and install it:
                    SynchronizationContext.SetSynchronizationContext(
                        new DispatcherSynchronizationContext(
                            Dispatcher.CurrentDispatcher));

                    HttpWebRequest httpWebRequest = WebRequest.Create(requestUrl) as HttpWebRequest;
                    //Formatea la fecha a formato json /Date(21423423)/
                    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    };
                    string text = JsonConvert.SerializeObject(JSONRequest, microsoftDateFormatSettings);
                    text = string.Concat(new string[]{"{\"",
                    ObjWS,
                    "\":",
                    text,
                    "}"
                });
                    httpWebRequest.Method = JSONmethod;
                    httpWebRequest.ContentType = JSONContentType;
                    httpWebRequest.Headers.Add("Token", TokenModel.Nombre);
                    byte[] bytes = Encoding.UTF8.GetBytes(text);
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                    using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                    {
                        if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(string.Format("Server error (HTTP {0}: {1}).", httpWebResponse.StatusCode, httpWebResponse.StatusDescription));
                        }
                        Stream responseStream = httpWebResponse.GetResponseStream();
                        StreamReader streamReader = new StreamReader(responseStream);
                        string text2 = streamReader.ReadToEnd();

                        object obj = JsonConvert.DeserializeObject(text2, JSONResponseType);
                        result = obj;
                    }

                    // Start the Dispatcher Processing
                    GlobalsVariables.Peticion = false;
                    System.Windows.Threading.Dispatcher.Run();
                }));
                
                newWindowThread.SetApartmentState(ApartmentState.STA);
                newWindowThread.IsBackground = true;
                newWindowThread.Start();

                //Dispatcher.FromThread(newWindowThread).InvokeShutdown();
                while(GlobalsVariables.Peticion==true)
                {
                        Console.WriteLine(GlobalsVariables.Peticion);
                        Barra.Close();
                }
            }
            catch (Exception ex)
            {
                //Dispatcher.FromThread(newWindowThread).InvokeShutdown();
                MessageBox.Show(ex.Message);
                result = null;
            }
            return result;
        }
    }
}
