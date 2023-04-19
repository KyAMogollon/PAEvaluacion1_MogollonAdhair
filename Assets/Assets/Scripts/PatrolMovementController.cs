using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] HealthBarController healthBarControllerEnemy1;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    public event Action<PatrolMovementController> OnCollision;
    [SerializeField] LayerMask layermask;
    int vidaEnemy = 100;
    int Puntuacion = 10;

    public int damage = 20;

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, (currentPositionTarget.position - transform.position).normalized, 3f, layermask);

        Debug.DrawRay(transform.position, (currentPositionTarget.position - transform.position).normalized * 3f, Color.blue);
        if (hit2D)
        {
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * 6;
        }
        else
        {
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
        }
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnCollision.Invoke(this);
        }
        if(collision.gameObject.tag == "Bala")
        {
            healthBarControllerEnemy1.UpdateHealth(-10);
            vidaEnemy = vidaEnemy - 10;
        }
        if (vidaEnemy <= 0)
        {
            controller.score=controller.score+Puntuacion;
            Destroy(gameObject);
        }
    }
}
