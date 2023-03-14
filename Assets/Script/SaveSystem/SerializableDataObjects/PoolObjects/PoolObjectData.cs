using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PoolObjectData
{
    public PooledObjectName PooledObjectName { get; set; }

    public void SelfImport()
    {
        var exportable = GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName)
            .GetComponent<ExportablePoolObject>();
        exportable.Import(this);
    }
}
