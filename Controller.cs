using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Headman
{
    public class Controller
    {
        private static ITelegramBotClient _botClient;
        private static ReplyKeyboardMarkup _replyKeyboard = new ReplyKeyboardMarkup
        (
            new[]
            {
                new[]
                {
                    new KeyboardButton("Случайный человек"),
                },
                new[]
                {
                    new KeyboardButton("Список группы"),
                },
                new[]
                {
                    new KeyboardButton("Случайный порядок"),
                }
            },
            resizeKeyboard: true
        );

        public Controller(string token)
        {
            _botClient = new TelegramBotClient(token);
            
            _botClient.OnMessage += BotOnMessage;
            
            var bot = _botClient.GetMeAsync().Result;
            Console.WriteLine($"Logged in as {bot.Id} ({bot.FirstName}).");
        }

        public void StartBot() => _botClient.StartReceiving();
        
        public void StopBot() => _botClient.StopReceiving();
        
        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine(
                    $"Received a text message in chat {e.Message.Chat.Id} ({e.Message.Chat.FirstName}): {e.Message.Text}");
                
                var result = await SendResponse(e);
                Console.WriteLine($"Successfully replied in chat {result.Chat.Id}");
                
                await Task.Delay(40);
            }
        }

        private static async Task<Message> SendResponse(MessageEventArgs e)
        {
            var response = e.Message.Text switch
            {
                "/start" => $"Привет, {e.Message.Chat.FirstName}!\nЯ {_botClient.GetMeAsync().Result.FirstName}.",
                "Случайный человек" => $"{Group.GetRandomName()}",
                "Список группы" => $"{Group.GetGroup()}",
                "Случайный порядок" => $"{Group.GetRandomOrder()}",
                _ => $"Сам ты {e.Message.Text}..."
            };

            return await _botClient.SendTextMessageAsync
            (
                chatId: e.Message.Chat,
                text: response,
                replyMarkup: _replyKeyboard
            );
        }
    }
}