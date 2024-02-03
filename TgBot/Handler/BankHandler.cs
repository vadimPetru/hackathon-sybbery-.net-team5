using System.ComponentModel.Design;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net.WebSockets;
using TgBot.Utils;

namespace TgBot.Handler
{

  public static class BankHandler
  {

    
    static ReplyKeyboardMarkup banks = new(new[]
    {
      new KeyboardButton[]
      {
        "БелИнвест", "Алфа", "БНБ"
      } })
    {

      ResizeKeyboard = true
    };



    public async static Task<bool> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "Выбирите банк", cancellationToken: cancellationToken, replyMarkup: banks);
      LastActionStorage.Storage[update.Message.Chat.Id].Action = BankWorker.GetListCurrency; 
      return true;
    } 

    

  }
}
