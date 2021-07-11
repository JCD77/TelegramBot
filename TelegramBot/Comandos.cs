using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;

namespace TelegramBot
{

  public    enum comandoType
    {
        PING,
        PING2,
   
    }

    public static class Comandos
    {
        public static List<Comando> listaComandos;

        public static void CargarComandos()
        {


            Comandos.listaComandos = Config.config.GetSection("Comandos").Get<List<Comando>>();




        }
    }
    

    public class Comando
    {
        public string Nombre { get; set; }
 
        public comandoType Tipo {set;get;}
        public string LLamada { get; set; }
        public string Descripcion { get; set; }

        public string Fichero { get; set; }

        public string TextoOK { get; set; }
        public string TextoKO { get; set; }

        public bool Demonio { get; set; }

        public bool? Estado { get; set; }
        public string respuesta { get; set; }

        public  bool ObtenerComando(Message message, ref Comando comando)
        {
            bool salida = false;
            if (comando==null) { comando = new Comando(); }

            if (message.Text.StartsWith("/"))
            {

                var firstSpaceIndex = message.Text.IndexOf(" ");
                string textoComando = "";
                //    string  secondString = "";
                if (firstSpaceIndex > 0)
                {
                    textoComando = message.Text.Substring(1, firstSpaceIndex - 1); // INAGX4
                }
                else
                {
                    textoComando = message.Text.Substring(1); // INAGX4
                }

                //     secondString = message.Text.Substring(firstSpaceIndex + 1);
                //}
                //    else
                //    {
                //        textoComando = message.Text.Substring(1);
                //    }


                comando = Comandos.listaComandos.Where(x => x.LLamada.Equals(textoComando)).FirstOrDefault();


              
                

                

            }

            return salida=(comando!=null);
        }



        public   string EjecutarComando(Comando comando)
        {

            PowerShellHelper ps = new PowerShellHelper();
            var script = System.IO.File.ReadAllText("Scripts/" + comando.Fichero);
            comando.respuesta = ps.Execute(script);
            comando.Estado = EstadoComando(comando);
           

          

            return   IconoRespuesta(comando)+comando.Nombre + "\n" + comando.respuesta;
        }

        private  string IconoRespuesta(Comando comando)
        {
            string salida;
            if (comando.Estado == null)
            {

                salida = "\U00002753";


            }
            else if (comando.Estado == true)
            {
               
                salida = "\U00002714";
            }
            else
            {
               
                salida = "\U00002716";
            }

            return salida;
        }
        private  bool? EstadoComando(Comando comando)
        {

            bool? salida = null;
            var ok = comando.respuesta.Contains(comando.TextoOK);
            var ko = comando.respuesta.Contains(comando.TextoKO);
          

            if ((ok && ko) || (!ok && !ko))
            {
                salida = null;
                


            }
            else if (ok)
            {
                salida = true;
               
            }
            else
            {
                salida = false;
               
            }
            return salida;
        }
    }
}
