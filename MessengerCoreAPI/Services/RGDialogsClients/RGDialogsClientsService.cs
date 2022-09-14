using MessengerCoreAPI.Models.RGDialogsClients;

namespace MessengerCoreAPI.Services.RGDialogsClients
{
    /// <inheritdoc cref="IRGDialogsClientsService"/>
    public class RGDialogsClientsService : IRGDialogsClientsService
    {
        private readonly RGDialogsClientsCollection _dialogsClientsCollection;

        /// <summary>
        /// .ctor
        /// </summary>
        public RGDialogsClientsService(RGDialogsClientsCollection dialogsClientsCollection)
        {
            _dialogsClientsCollection = dialogsClientsCollection;
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> GetDialogWithClients(IEnumerable<Guid> clientsId)
        {
            var dialogs = _dialogsClientsCollection.RGDialogsClients
                .GroupBy(_ => _.IDRGDialog, _ => _.IDClient)
                .Where(group => clientsId.All(cid => group.Any(dialogCid => dialogCid == cid)))
                .Select(dialog => dialog.Key);

            return dialogs;
        }
    }
}
