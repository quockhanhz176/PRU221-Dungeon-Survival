using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHealthHandler
{
    IHealthHandler SetNext(IHealthHandler healthHandler);
    IHealthHandler GetNext();
    public int AddHealth(int health);
    public int DeductHealth(int health);
}

