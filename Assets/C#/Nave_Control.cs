using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave_Control : MonoBehaviour
{
    private SpriteRenderer _spriteR;
    private Animator _animator;
    private Rigidbody2D _rb2D;
    private float horizontal;
    public float speed = 6;
    public AudioSource _AS;
    public GameObject Laser;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        _animator.SetInteger("IntCaminar", (int)(Mathf.Ceil(horizontal)));
        if (horizontal < 0)
        {
            _spriteR.flipX = true;
        }
        else if (horizontal > 0)
        {
            _spriteR.flipX = false;
        }
        if(Input.GetKeyDown("space") == true) 
        {
            _AS.Play();
            Instantiate(Laser, new Vector2(transform.position.x + 0.95f,transform.position.y+ 1f), Laser.transform.rotation);
            Instantiate(Laser, new Vector2(transform.position.x + -1.01f, transform.position.y + 1f), Laser.transform.rotation);
        }
    }
    void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(speed * horizontal, 0);
    }

}
