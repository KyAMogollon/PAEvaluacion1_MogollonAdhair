using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private int scoreLife = 100;
    public int score = 0;
    [SerializeField] HealthBarController healthBarController;
    [SerializeField] private List<Enemigo2> enemigo2OnMap;
    [SerializeField] private List<PatrolMovementController> enemy1OnMap;
    [SerializeField] UnityEvent OnPlayerDie;
    [SerializeField] UnityEvent OnWin;
    private List<Enemigo2> enemigo2Remaining;
    private List<PatrolMovementController> enemy1Remaining;
    // Start is called before the first frame update
    void Start()
    {
        enemigo2Remaining = new List<Enemigo2>(enemigo2OnMap);
        foreach(Enemigo2 enemigo2 in enemigo2Remaining)
        {
            enemigo2.OnCollision += PlayerDamage;
        }
        enemy1Remaining = new List<PatrolMovementController>(enemy1OnMap);
        foreach(PatrolMovementController enemy1 in enemy1Remaining)
        {
            enemy1.OnCollision += PlayerDamageEnemy1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 30)
        {
            OnWin.Invoke();
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(250, 40, 500, 20), string.Format("Total Score: {0}", score));
    }
    private void PlayerDamage(Enemigo2 enemigo2)
    {
        scoreLife -= enemigo2.damage;
        healthBarController.UpdateHealth(-enemigo2.damage);
        if (scoreLife <= 0)
        {
            OnPlayerDie.Invoke();
        }
    }
    private void PlayerDamageEnemy1(PatrolMovementController enemy1)
    {
        scoreLife-=enemy1.damage;
        healthBarController.UpdateHealth(-enemy1.damage);
        if (scoreLife <= 0)
        {
            OnPlayerDie.Invoke();
        }
    }
}
