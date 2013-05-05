namespace WarehouseInventoryManagement.Tools.Commands
{
    public abstract class CommandBase : ICommandBase
    {
        /// <summary>
        /// Gets or sets a command context.
        /// </summary>
        /// <value>
        /// A command executing context.
        /// </value>
        public ICommandContext Context { get; set; }
    }
}
