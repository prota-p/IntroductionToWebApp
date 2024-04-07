namespace ChatGPT_API_Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("ChatGPTに話しかけてください（eixtで終了）:");
            while (true)
            {
                Console.Write("●あなたの質問：");
                string userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    break;
                }
                try
                {
                    var chatGPTService = new ChatGPTService();
                    var response = await chatGPTService.AskGPT4(userInput);
                    Console.WriteLine("■ChatGPTの回答：" + response);
                }
                catch (Exception e)
                {
                    Console.WriteLine("エラーが発生しました：" + e.Message);
                }
            }
        }
    }
}