using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DisintegrationGun : PoolObject
{
    [Tooltip("The game object that contains all the shapes that form the gun")]
    public GameObject Shapes;
    [SerializeField]
    private GameObject Ray;
    public bool RayActive
    {
        get => Ray.gameObject.activeInHierarchy;
        set => Ray.gameObject.SetActive(value);
    }

    private SpriteRenderer[] _gunShapeRenderers;
    private float _rayWidth;
    private Quaternion _originalRotation;

    public override PooledObjectName GetPoolObjectName() => PooledObjectName.DisintegrationGun;
    private void Awake()
    {
        Debug.Log("start up");
        _gunShapeRenderers = Shapes.GetComponentsInChildren<SpriteRenderer>();
        _rayWidth = Ray.transform.localScale.y;
        _originalRotation = transform.rotation;
    }

    public void SetAlpha(float alpha)
    {
        foreach (var sr in _gunShapeRenderers)
        {
            var color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }

    /// <summary>
    /// Set the width of the ray
    /// </summary>
    /// <param name="scale">The scale of the ray, maximum 1, minimum 0</param>
    public void SetRayWidth(float scale)
    {
        if (scale > 1) scale = 1;
        if (scale < 0) scale = 0;
        Ray.transform.localScale = new Vector2(Ray.transform.localScale.x, scale * _rayWidth);
    }

    public override void StartUp()
    {
        gameObject.SetActive(true);
        Ray.gameObject.SetActive(false);
        transform.rotation = _originalRotation;
    }
}
