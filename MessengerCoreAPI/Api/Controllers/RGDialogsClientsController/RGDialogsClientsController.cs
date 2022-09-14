using MessengerCoreAPI.Models.RGDialogsClients;
using Microsoft.AspNetCore.Mvc;

namespace MessengerCoreAPI.Api.Controllers.RGDialogsClientsController
{
    /// <summary>
    /// Контроллер работы с поиском диалогов
    /// </summary>
    public class RGDialogsClientsController : Controller
    {
        private readonly RGDialogsClientsCollection _dialogsClientsCollection;

        /// <summary>
        /// .ctor
        /// </summary>
        public RGDialogsClientsController(RGDialogsClientsCollection dialogsClientsCollection)
        {
            _dialogsClientsCollection = dialogsClientsCollection;
        }

        /// <summary>
        /// Поиска диалога с переданными идентификаторами клиентов
        /// </summary>
        /// <param name="clientsId">Идентификаторы клиентов для которых необходимо найти диалог</param>
        /// <returns></returns>
        [HttpGet("~/api/v1/dialogs-with-clients")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), 200)]
        public IActionResult GetDialogWithClients(IEnumerable<Guid> clientsId)
        {
            var dialogs = _dialogsClientsCollection.RGDialogsClients
                .GroupBy(_ => _.IDRGDialog, _ => _.IDClient)
                .Where(group => clientsId.All(cid => group.Any(dialogCid => dialogCid == cid)))
                .Select(dialog => dialog.Key);

            return Ok(dialogs);
        }
    }
}
