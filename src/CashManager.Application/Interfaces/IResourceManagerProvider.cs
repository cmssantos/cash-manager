using System.Resources;

namespace CashManager.Application.Interfaces;

public interface IResourceManagerProvider
{
    ResourceManager GetResourceManager();
}
