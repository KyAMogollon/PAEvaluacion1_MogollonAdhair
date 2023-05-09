using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject balaPref;
    [SerializeField] private Transform posicionDisparo;
    Vector2 movementPlayer;
    Vector2 mouseInput;

    private void Start()
    {
    }
    private void Update() {
        
        myRBD2.velocity = movementPlayer * velocityModifier;

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        //mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseInput = mouseInput - transform.position;
        CheckFlip(mouseInput.x);
    
        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        /*if(Input.GetMouseButtonDown(0)){
            Instantiate(balaPref, transform.position, Quaternion.identity);
        }else if(Input.GetMouseButtonDown(1)){
            Instantiate(balaPref, transform.position, Quaternion.identity);
        }*/
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        movementPlayer = new Vector2(inputMovement.x, inputMovement.y);
    }
    public void OnAim(InputAction.CallbackContext value)
    {
        Vector2 inputMouse = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        mouseInput = inputMouse;
    }
    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.started) {
            Vector2 disparo = new Vector2((mouseInput.x - transform.position.x), (mouseInput.y - transform.position.y));
            Instantiate(balaPref, disparo, Quaternion.identity);
        }
    }
}
