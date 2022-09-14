using MessengerCoreAPI.Services.RGDialogsClients;
using Microsoft.AspNetCore.Mvc;

namespace MessengerCoreAPI.Api.Controllers.RGDialogsClientsController
{
    /// <summary>
    /// Контроллер работы с поиском диалогов
    /// </summary>
    public class RGDialogsClientsController : Controller
    {
        private readonly IRGDialogsClientsService _dialogsClientsService;

        /// <summary>
        /// .ctor
        /// </summary>
        public RGDialogsClientsController(IRGDialogsClientsService dialogsClientsService)
        {
            _dialogsClientsService = dialogsClientsService;
        }

        /// <summary>
        /// Поиска диалога с переданными идентификаторами клиентов
        /// </summary>
        /// <param name="clientsId">Идентификаторы клиентов для которых необходимо найти диалог</param>
        /// <returns>Набор id диалогов содержащих переданных клиентов</returns>
        [HttpGet("~/api/v1/dialogs-with-clients")]
        [ProducesResponseType(typeof(IEnumerable<Guid>), 200)]
        public IActionResult GetDialogWithClients(IEnumerable<Guid> clientsId)
        {
            var dialogs = _dialogsClientsService.GetDialogWithClients(clientsId);

            return Ok(dialogs);
        }
    }
}
