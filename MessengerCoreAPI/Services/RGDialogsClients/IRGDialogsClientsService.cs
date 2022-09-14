namespace MessengerCoreAPI.Services.RGDialogsClients
{
    /// <summary>
    /// Сервис работы с диалогами
    /// </summary>
    public interface IRGDialogsClientsService
    {
        /// <summary>
        /// Поиска диалога с переданными идентификаторами клиентов
        /// </summary>
        /// <param name="clientsId">Идентификаторы клиентов для которых необходимо найти диалог</param>
        /// <returns>Набор id диалогов содержащих переданных клиентов</returns>
        IEnumerable<Guid> GetDialogWithClients(IEnumerable<Guid> clientsId);
    }
}
