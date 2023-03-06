using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class HealthHandler : IHealthHandler
{
    private IHealthHandler _nextHandler;

    public virtual IHealthHandler SetNext(IHealthHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public IHealthHandler GetNext()
    {
        return _nextHandler;
    }

    public virtual int AddHealth(int health)
    {
        return _nextHandler != null ? _nextHandler.AddHealth(health) : health;
    }

    public virtual int DeductHealth(int health)
    {
        return _nextHandler != null ? _nextHandler.DeductHealth(health) : health;
    }
}