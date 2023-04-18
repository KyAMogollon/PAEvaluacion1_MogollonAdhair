using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private int scoreLife = 50;
    [SerializeField] private List<Enemigo2> enemigo2OnMap;
    [SerializeField] UnityEvent OnPlayerDie;
    private List<Enemigo2> enemigo2Remaining;
    // Start is called before the first frame update
    void Start()
    {
        enemigo2Remaining = new List<Enemigo2>(enemigo2OnMap);
        foreach(Enemigo2 enemigo2 in enemigo2Remaining)
        {
            enemigo2.OnCollision += PlayerDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(7, 3.79f, 500, 20), string.Format("Life: {0}", scoreLife));
    }
    private void PlayerDamage(Enemigo2 enemigo2)
    {
        scoreLife -= enemigo2.damage;
        if (scoreLife <= 0)
        {
            OnPlayerDie.Invoke();
        }
    }
}
