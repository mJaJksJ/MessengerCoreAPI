namespace MessengerCoreAPI.Models.RGDialogsClients.RGDialogsClientsFactory
{
    /// <inheritdoc cref="IRGDialogsClientsFactory"/>
    public class DefaultRGDialogsClientsFactory : IRGDialogsClientsFactory
    {
        private static RGDialogsClients Instace;

        /// <inheritdoc/>
        public List<RGDialogsClients> CreateRGDialogsClients()
        {
            Instace ??= new RGDialogsClients();
            return Instace.Init();
        }
    }
}
