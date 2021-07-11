using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot
{
     public static class TelegramHelper
    {


        internal static async Task EnviarMensajeAsync(ITelegramBotClient botClient, long id, string text)
        {
            await botClient.SendTextMessageAsync(
           chatId: id,
           text: "You said:\n" + text);
        }
    }
}
