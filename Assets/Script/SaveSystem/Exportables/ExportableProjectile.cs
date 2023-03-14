using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExportableProjectile : ExportablePoolObject
{
    [SerializeField]
    private Destroyable _destroyable;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    public void Start()
    {
        _destroyable = GetComponent<Destroyable>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected override PoolObjectData GetData()
    {
        return new ProjectileData
        {
            Position = transform.position,
            Rotation = transform.rotation,
            Velocity = _rigidbody2D.velocity,
            TimeLeft = _destroyable.TimeLeft
        };
    }

    protected override void SetData(PoolObjectData data)
    {
        var dataObject = (ProjectileData)data;
        transform.position = dataObject.Position;
        transform.rotation = dataObject.Rotation;
        _rigidbody2D.velocity = dataObject.Velocity;
        _destroyable.TimeLeft = dataObject.TimeLeft;
    }
}
