using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] Transform playerTranform;
    [SerializeField] Detection detection;
    [SerializeField] HealthBarController healthBarControllerEnemy2;
    public float speed = 3;
    Vector2 initialPosition;
    public int damage = 10;
    public event Action<Enemigo2> OnCollision;
    Rigidbody2D _rb;
    int vidaEnemy2 = 100;
    int Puntuacion = 20;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        initialPosition = _rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (detection.checkDetection==1){
            Vector2 direction = ((Vector2)playerTranform.position - _rb.position).normalized;
            _rb.position+=direction*speed*Time.deltaTime;
        }else if (detection.checkDetection == 0)
        {
            Vector2 direction = ((Vector2)initialPosition - _rb.position).normalized;
            _rb.position += direction * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnCollision.Invoke(this);
        }
        if (collision.gameObject.tag == "Bala")
        {
            healthBarControllerEnemy2.UpdateHealth(-10);
            vidaEnemy2 = vidaEnemy2 - 10;
        }
        if(vidaEnemy2 <= 0)
        {
            controller.score = controller.score + Puntuacion;
            Destroy(gameObject);
        }
    }
}
