using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    // public delegate void Attack();

    public Action UseAttack;
    public Func<Action, bool> IsMeleeSelected;
    bool isMelee = false;
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {   
        IsMeleeSelected = (UseAttack) => {return UseAttack == UseMelee;};
        UseAttack = () => {Debug.Log("No Attack Selected");};        //using lambda exp to assign a default
        // gameManager.OnLMBDown += gameManager.LMBDown;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UseAttack = UseMelee;
            isMelee = IsMeleeSelected(UseAttack);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseAttack = UseGun;
            isMelee = IsMeleeSelected(UseAttack);
        }
        if(Input.GetMouseButtonDown(0))
        {
            UseAttack?.Invoke();
            Debug.Log("Melee? "+ isMelee);
        }
        if(Input.GetMouseButtonDown(1))
        {
            gameManager.SetTimer(3f, Explode);
            Debug.Log("Explosion will occur in 3 seconds!");
        }
    }
    void UseMelee()
    {
        Debug.Log("Using Melee Attack");
    }
    void UseGun()
    {
        Debug.Log("Using Gun Attack");
    }
    void Explode()
    {
        Debug.Log("Boom!");
    }
}
