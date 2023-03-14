using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ExportablePoolObject : MonoBehaviour, IExportable<PoolObjectData>
{
    [SerializeField]
    protected PooledObjectName _pooledObjectName;

    public PoolObjectData Export()
    {
        var dataObject = GetData();
        dataObject.PooledObjectName = _pooledObjectName;
        return dataObject;
    }

    public virtual void Import(PoolObjectData data)
    {
        if (data.PooledObjectName != _pooledObjectName)
        {
            throw new Exception($"{_pooledObjectName} expected but {data.PooledObjectName} was provided");
        }

        SetData(data);
    }

    protected abstract PoolObjectData GetData();

    protected abstract void SetData(PoolObjectData dataObject);
}

