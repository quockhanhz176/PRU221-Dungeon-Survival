using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface PoolObject
{
    /// <summary>
    /// Initialize pool object the first time they are created (initiated from prefab).
    /// </summary>
    public void Initialize();

    /// <summary>
    /// Startup the object before being sent out from the object pool. 
    /// The gameObject this script is attached to is inactive at this point and should be set to active if necessary.
    /// </summary>
    public void StartUp();

    public PooledObjectName GetPoolObjectName();
}
