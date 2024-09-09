namespace HogWarp.Lib.System
{
    public class Logger
    {
        private string _pluglinName;

        public Logger(string name)
        {
            _pluglinName = name;
        }
        private string GetLoggerLine(string message)
        {
            string currentTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return $"[{currentTimestamp}][{_pluglinName}] {message}";
        }
        public void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(GetLoggerLine(message));
            Console.ResetColor();
        }
        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(GetLoggerLine(message));
            Console.ResetColor();
        }
        public void Successful(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(GetLoggerLine(message));
            Console.ResetColor();
        }
        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GetLoggerLine(message));
            Console.ResetColor();
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(GetLoggerLine(message));
            Console.ResetColor();
        }
    }
}