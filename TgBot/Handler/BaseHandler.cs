using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Enums;
using TgBot.Utils;

namespace TgBot.Handler
{
    public class BaseHandler
    {
        delegate Task<bool> MessageHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
        private static BankWorker defaultAction = BankWorker.Getbanks;

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if(!LastActionStorage.Storage.ContainsKey(update.Message.Chat.Id))
                LastActionStorage.Storage.Add(update.Message.Chat.Id, new(){Action = defaultAction});

            var clientAction = LastActionStorage.Storage[update.Message.Chat.Id];
            Console.WriteLine(clientAction.Action);

            if(update.Message.Text.ToLower() == "/start")
                clientAction.Action = defaultAction;
            
            MessageHandler handler = null;
            var messageHandled = false;

            while(!messageHandled)
            {
                switch(clientAction.Action)
                {
                    case BankWorker.Getbanks:
                        handler = BankHandler.HandleUpdateAsync;
                        break;
                    case BankWorker.GetListCurrency:
                        handler = ChooseCurrencyHandler.HandleUpdateAsync;
                        break;

                    default:
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Неверный запрос");
                        clientAction.Action = defaultAction;
                        return;
                }
                
                if(handler != null)
                    messageHandled = await handler.Invoke(botClient, update, cancellationToken);
            }
        }
    }
}