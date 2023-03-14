using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExportablePickup : ExportablePoolObject
{
    protected override PoolObjectData GetData()
    {
        return new PickupData
        {
            Position = transform.position,
            Rotation = transform.rotation
        };
    }

    protected override void SetData(PoolObjectData data)
    {
        var dataObject = (PickupData)data;
        transform.position = dataObject.Position;
        transform.rotation = dataObject.Rotation;
    }
}
