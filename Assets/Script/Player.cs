using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    public Joystick Joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }
}
