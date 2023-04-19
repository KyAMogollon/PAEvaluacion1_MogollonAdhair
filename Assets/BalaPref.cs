using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPref : MonoBehaviour
{
    public int speed = 6;
    [SerializeField] Vector2 mousePos;
    [SerializeField] GameObject bala;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _rb.velocity = mousePos*speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemigo")
        {
            Destroy(this.gameObject);
        }
    }
}
