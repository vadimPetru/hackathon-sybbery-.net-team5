using System.ComponentModel.Design;
using Telegram.Bot.Types;
using Telegram.Bot;
using TgBot.Enums;

namespace TgBot.Handler
{

  public  class BankHandler
  {
    List<string> banks = new List<string>() { "Belinvest" ,"Bnb", "alfa"};
    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      
    }
  }
}
