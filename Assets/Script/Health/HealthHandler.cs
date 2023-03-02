using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class HealthHandler
{
    public HealthHandler NextHandler;

    public virtual HealthHandler SetNext(HealthHandler handler)
    {
        NextHandler = handler;
        return handler;
    }

    public virtual int AddHealth(int health)
    {
        return NextHandler != null ? NextHandler.AddHealth(health) : health;
    }

    public virtual int DeductHealth(int health)
    {
        return NextHandler != null ? NextHandler.DeductHealth(health) : health;
    }
}