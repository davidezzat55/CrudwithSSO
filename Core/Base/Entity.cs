using LinkDev.Wasel.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Wasel.Core.Base
{
    public abstract class Entity<T>
    {
        public virtual T ID { get; protected set; }

    }
}
