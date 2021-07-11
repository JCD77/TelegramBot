using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    class Program
    {
        static ITelegramBotClient botClient;
 
     
        static string TokenAccess = Config.config.GetSection("TokenBot").Get<String>();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            Comandos.CargarComandos();

       

            botClient = new TelegramBotClient(TokenAccess);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }









        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.From.Username}: { e.Message.Chat.Id}.");
                if (Seguridad.AutorizarMensaje(e))
                {
                   
                    Comando comando = new Comando();
                  if (comando.ObtenerComando(e.Message, ref comando)){

                        var salida = comando.EjecutarComando(comando);

                         await TelegramHelper.EnviarMensajeAsync(botClient, e.Message.Chat.Id,salida);



                    }
                



                }
            }
        }
    }
}
    

