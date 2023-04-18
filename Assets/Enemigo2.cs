using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    [SerializeField] Transform transformPlayer;
    public int speed = 3;
    bool b = false;
    Vector2 savePosition;
    public int damage = 10;
    public event Action<Enemigo2> OnCollision;
    // Start is called before the first frame update
    void Start()
    {
        savePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (b == true){
            transform.position = new Vector2(transformPlayer.position.x + speed * Time.deltaTime, transform.position.y + speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            b = true;
        }
        else
        {
            b = false;
            transform.position = new Vector2(savePosition.x, savePosition.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnCollision.Invoke(this);
        }
    }
}
