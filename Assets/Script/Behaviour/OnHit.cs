using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private float timer;
    private OnHit onHit;

    public static OnHit Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _sprite = gameObject.GetComponentInParent<SpriteRenderer>();
        StartCoroutine(Flicker(timer));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Flicker(float time)
    {
        var originalColor = _sprite.color;
        _sprite.color = Color.red;
        yield return new WaitForSeconds(time);
        _sprite.color = originalColor;
        Destroy(gameObject);
    }
}