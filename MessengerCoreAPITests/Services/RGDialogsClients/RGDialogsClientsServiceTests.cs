using MessengerCoreAPI.Models.RGDialogsClients;
using MessengerCoreAPI.Models.RGDialogsClients.RGDialogsClientsFactory;
using MessengerCoreAPI.Services.RGDialogsClients;
using NUnit.Framework;

namespace MessengerCoreAPITests.Services.RGDialogsClients
{
    [TestFixture()]
    internal class RGDialogsClientsServiceTests
    {
        private IRGDialogsClientsService _service;
        private readonly IEnumerable<Guid> _goodClientsId = new[] { new Guid("4b6a6b9a-2303-402a-9970-6e71f4a47151"), new Guid("c72e5cb5-d6b4-4c0c-9992-d7ae1c53a820") };
        private readonly IEnumerable<Guid> _badClientsId = new[] { new Guid("4b6a6b9a-2303-402a-9970-6e71f4a47151"), new Guid("50454d55-a73c-4cbc-be25-3c5729dcb82b") };
        private readonly IEnumerable<Guid> _expectedDialogsId = new[] { new Guid("fcd6b112-1834-4420-bee6-70c9776f6378"), new Guid("19f6f751-7f8d-41fa-8261-709028650592") };

        [SetUp]
        public void SetUp()
        {
            IRGDialogsClientsFactory dialogsClientsFactory = new DefaultRGDialogsClientsFactory();
            var dialogs = new RGDialogsClientsCollection { RGDialogsClients = dialogsClientsFactory.CreateRGDialogsClients() };

            _service = new RGDialogsClientsService(dialogs);
        }

        [Test]
        public void GetDialogWithClients_GoodClientIds_DialogIds()
        {
            var actual = _service.GetDialogWithClients(_goodClientsId);

            var result = _expectedDialogsId.SequenceEqual(actual);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetDialogWithClients_BadClientIds_DialogIds()
        {
            var actual = _service.GetDialogWithClients(_badClientsId);

            Assert.That(actual, Is.Empty);
        }
    }
}
