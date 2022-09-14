using System.Reflection;

namespace MessengerCoreAPI
{
    /// <summary>
    /// Пути к директориям
    /// </summary>
    public static class DirectoryPaths
    {
        /// <summary>
        /// Директория с запускаемым файлом
        /// </summary>
        public static string WorkingDirectory { get; private set; }

        /// <summary>
        /// Директория сохранения логов
        /// </summary>
        public static string LogsDirectory { get; private set; }

        static DirectoryPaths()
        {
            WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogsDirectory = Path.Combine(WorkingDirectory, "Logs");
        }
    }
}
