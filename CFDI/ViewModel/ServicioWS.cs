using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                if(this.Url==null)
                    return "http://10.10.1.86/WsCfdi/" + this.Servicie + "/" + this.Method;
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
        public ServicioWS(string url,Object ObjetoEnvio, Type TipoObjetoRespuesta, string Parametros)
        {
            Url = url;
            JSONRequest = ObjetoEnvio;
            JSONmethod = "POST";
            JSONContentType = "application/json";
            JSONResponseType = TipoObjetoRespuesta;
            ObjWS = Parametros;
        }
        public ServicioWS(string Servicio,string Metodo,Object ObjetoEnvio,Type TipoObjetoRespuesta,string Parametros)
        {
            //requestUrl = @"http://10.10.1.86/WsCfdi/"+Servicio+"/"+Metodo;
            Servicie = Servicio;
            Method = Metodo;
            JSONRequest = ObjetoEnvio;
            JSONmethod = "POST";
            JSONContentType= "application/json";
            JSONResponseType = TipoObjetoRespuesta;
            ObjWS = Parametros;
        }
        public object Peticion()
        {
            object result;
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(requestUrl) as HttpWebRequest;
                string text = JsonConvert.SerializeObject(JSONRequest);
                text = string.Concat(new string[]{"{\"",
                    ObjWS,
                    "\":",
                    text,
                    "}"
                });
                httpWebRequest.Method = JSONmethod;
                httpWebRequest.ContentType = JSONContentType;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                /*result = new Respuesta
                {
                    status = "error",
                    msj = ex.Message
                };*/
                result = null;
            }
            return result;
        }
    }
}
