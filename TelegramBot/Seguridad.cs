using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;

namespace TelegramBot
{
   public static class Seguridad
    {






        public static List<long> listaChatId= new List<long>();

        public static void InicializarChat()
        {


            Seguridad.listaChatId = Config.config.GetSection("ChatId").Get<List<long>>();




        }
        public  static bool AutorizarMensaje(MessageEventArgs e)
        {
            bool salida = false;
            if (listaChatId.Count == 0) InicializarChat();

            return listaChatId.IndexOf(e.Message.Chat.Id)>=0;

            




        }

     
    }
}
