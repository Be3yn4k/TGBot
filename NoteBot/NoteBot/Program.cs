using BackendApi.Contracts.Book_status;
using BackendApi.Contracts.Genre;
using BackendApi.Contracts.Role;
using BackendApi.Contracts.Users;
using Newtonsoft.Json;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;

namespace noteBot
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello There");

            var noteBot = new TelegramBotClient("6726783823:AAH-ExPwrOebEfHOkpBI7-6dFrGkI15vFZM");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            noteBot.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );


            var me = await noteBot.GetMeAsync();


            Console.WriteLine($"{me} is working");

            Console.ReadLine();

            cts.Cancel();
        }
        static Task HandlePollingErrorAsync(ITelegramBotClient noteBot, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;

        }


        static async Task HandleUpdateAsync(ITelegramBotClient noteBot, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.Message)
            {

                var message = update.Message;
                var messageText = message.Text;

                var chatId = message.Chat.Id;

                Console.WriteLine($"Recieved a '{messageText}' message in chat {chatId}.");

                Message sentMessage = await noteBot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said: \n" + messageText,
                    cancellationToken: cancellationToken);

                if (message.Text == "Проверка")
                {
                    await noteBot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Проверка: ОК!",
                    cancellationToken: cancellationToken);
                }



                if (message.Text == "Привет")
                {
                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Здраствуй, Monkey",
                        cancellationToken: cancellationToken);
                }



                if (message.Text == "Картинка")
                {
                    await noteBot.SendPhotoAsync(
                        chatId: chatId,
                        photo: InputFile.FromUri("https://www.meme-arsenal.com/memes/a38614de07a2e23e7cf4e84d0c83deff.jpg"),
                        cancellationToken: cancellationToken);
                }


                if (message.Text == "Видео")
                {
                    await noteBot.SendVideoAsync(
                        chatId: chatId,
                        video: InputFile.FromUri("https://rr5---sn-p5qddn7d.googlevideo.com/videoplayback?expire=1706031339&ei=i6SvZZr5E7GM_9EPnIypqAM&ip=102.165.16.211&id=o-AC3OY4iEfJuv7voZGXfHMxG2X8da5dTR6dtHq-_irWZ-&itag=22&source=youtube&requiressl=yes&xpc=EgVo2aDSNQ%3D%3D&mh=u-&mm=31%2C26&mn=sn-p5qddn7d%2Csn-vgqsknek&ms=au%2Conr&mv=m&mvi=5&pl=25&initcwndbps=910000&vprv=1&svpuc=1&mime=video%2Fmp4&cnr=14&ratebypass=yes&dur=8.544&lmt=1693479966076255&mt=1706009598&fvip=2&fexp=24007246&c=ANDROID&txp=5318224&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cxpc%2Cvprv%2Csvpuc%2Cmime%2Ccnr%2Cratebypass%2Cdur%2Clmt&sig=AJfQdSswRgIhAKzqWxp5zXtbgHvu03sYvWgrUxHxDfqFgGYBUzmJpHoIAiEA7tgAFOX_YMoKooMraCiLdbWYreq6cFOYKP-GpHH4S38%3D&lsparams=mh%2Cmm%2Cmn%2Cms%2Cmv%2Cmvi%2Cpl%2Cinitcwndbps&lsig=AAO5W4owRAIgLXCXya47FMj9zusfUxDKrhKPr2cXfazK1IBelq9hrgICIGPYIcGvPn4wvDJkxq7w193bRJoULKiJ7VuUXekePaDH&title=%D0%9E%D0%B1%D0%B5%D0%B7%D1%8C%D1%8F%D0%BD%D1%83%20%D0%BA%D1%80%D1%83%D1%82%D1%8F%D1%82%20%D0%BC%D0%B5%D0%BC"),
                        cancellationToken: cancellationToken);
                }



                if (message.Text == "Стикер")
                {
                    await noteBot.SendStickerAsync(
                        chatId: chatId,
                        sticker: InputFile.FromString("CAACAgIAAxkBAAI7gmWvrIhM32yYS54rE001D_bFMDhBAAJsJQACA2FpSUjC0S6WTK3zNAQ"),
                        cancellationToken: cancellationToken);
                }

                if (message.Text == "Кнопка")
                {
                    var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                          new KeyboardButton("1"),
                          new KeyboardButton("2"),
                          new KeyboardButton("3")

                        }
                    });
                    await noteBot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите одну кнопку:",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                }
                if (message.Text == "1" || message.Text == "2" || message.Text == "3")
                {
                    var replyKeyboardRemove = new ReplyKeyboardRemove();

                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выбор сделан",
                        replyMarkup: replyKeyboardRemove,
                        cancellationToken: cancellationToken);
                }


                if (message.Text == "База Данных")
                {
                    var keyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("Пользователи") },
                        new[] { new KeyboardButton("Роли") }
                    })
                    {
                        ResizeKeyboard = true
                    };

                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите действие:",
                        replyMarkup: keyboard,
                        cancellationToken: cancellationToken);
                }
                else if (message.Text == "Пользователи")
                {
                    await GetUsersAsync(noteBot, chatId, cancellationToken);
                }
                else if (message.Text == "Роли")
                {
                    await GetRoleAsync(noteBot, chatId, cancellationToken);
                }

                if (message.Text == "Пользователи" || message.Text == "Роли")
                {
                    var replyKeyboardRemove = new ReplyKeyboardRemove();

                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Monkeys love bananas",
                        replyMarkup: replyKeyboardRemove,
                        cancellationToken: cancellationToken);
                }




                static async Task GetUsersAsync(ITelegramBotClient noteBot, long chatId, CancellationToken cancellationToken)
                {
                    HttpClient client = new HttpClient();

                    var response = await client.GetAsync("https://localhost:7195/api/Users", cancellationToken);
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<GetUserResponse[]>(content);

                    var messageText = "";
                    foreach (var user in users)
                    {
                        messageText += $"\n\nДанные пользователя:\nUsername: {user.Role_id}\nEmail: {user.Email}\nPassword: {user.Password}";
                    }

                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: messageText,
                        cancellationToken: cancellationToken);
                }
                static async Task GetRoleAsync(ITelegramBotClient noteBot, long chatId, CancellationToken cancellationToken)
                {
                    HttpClient client = new HttpClient();

                    var response = await client.GetAsync("https://localhost:7195/api/Role", cancellationToken);
                    var content = await response.Content.ReadAsStringAsync();
                    var roles = JsonConvert.DeserializeObject<GetRoleResponse[]>(content);

                    var messageText = "";
                    foreach (var role in roles)
                    {
                        messageText += $"\n\nНазвание роли: {role.roleName} ";
                    }

                    await noteBot.SendTextMessageAsync(
                        chatId: chatId,
                        text: messageText,
                        cancellationToken: cancellationToken);
                }

            }
        }
    }
}