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
        _rb.velocity = mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
