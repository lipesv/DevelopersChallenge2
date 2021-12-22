using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OFX.CrossCutting.DI.Interfaces
{
    public interface IContextService
    {
        IContextService ConfigureContext();
        IContextService ApplyMigration();
    }
}
