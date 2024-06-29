using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{

    private int directionY = 1;
    private Rigidbody2D _rb;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0f, speed * directionY);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Imperio") 
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Muro") 
        {
            Destroy(this.gameObject);           
        }
    }
}
