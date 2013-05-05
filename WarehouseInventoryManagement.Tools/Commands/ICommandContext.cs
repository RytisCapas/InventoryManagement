using WarehouseInventoryManagement.Tools.Messages;

namespace WarehouseInventoryManagement.Tools.Commands
{
    public interface ICommandContext
    {
        IMessagesIndicator Messages { get; }
    }    
}
