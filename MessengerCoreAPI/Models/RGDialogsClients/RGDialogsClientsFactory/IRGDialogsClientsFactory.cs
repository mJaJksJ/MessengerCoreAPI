namespace MessengerCoreAPI.Models.RGDialogsClients.RGDialogsClientsFactory
{
    /// <summary>
    /// Фабрика создания RGDialogsClients
    /// </summary>
    public interface IRGDialogsClientsFactory
    {
        /// <summary>
        /// Создать RGDialogsClients
        /// </summary>
        /// <returns></returns>
        List<RGDialogsClients> CreateRGDialogsClients();
    }
}
