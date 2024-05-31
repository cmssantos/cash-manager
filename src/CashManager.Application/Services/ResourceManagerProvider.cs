using CashManager.Application.Interfaces;
using CashManager.Exception.ExceptionsBase;
using System.Resources;

namespace CashManager.Application.Services;

public class ResourceManagerProvider : IResourceManagerProvider
{
    public ResourceManager GetResourceManager()
        => new("CashManager.Exception.Resources.ResourceErrorMessages", typeof(CashManagerException).Assembly);
}
