using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruccion : MonoBehaviour
{
    public float speed = 3f;
    private int directionY = -1;
    private Animator _animator;
    private Rigidbody2D _rb;
    public AudioSource AS;
    public GameManagerControl gm;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
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
    public void SetGameManager(GameManagerControl gm)
    {
        this.gm = gm;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Bala"))
        {
            AS.Play();
            _animator.SetBool("Boom", true);
            Destroy(this.gameObject, 1.5f);
            gm.UpdatePuntaje();
        }
        if (other.gameObject.tag == ("Muro"))
        {
            AS.Play();
            _animator.SetBool("Boom", true);
            Destroy(this.gameObject, 1.5f);
        }
        if (other.gameObject.tag == ("NaveRebelde"))
        {
            AS.Play();
            _animator.SetBool("Boom", true);
            Destroy(this.gameObject, 1f);
        }
    }
    
}
