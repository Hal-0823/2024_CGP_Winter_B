using UnityEngine;

public class StatementPlayer : Information_Player
{
    bool gotDamage;
    Movement_Player movement_Player;
    AnimationPlayer animationPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationPlayer = this.GetComponentInParent<AnimationPlayer>();
        movement_Player = this.GetComponentInParent<Movement_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy")&&!gotDamage)
        {
            movement_Player.GotDamage();
            HP -= 1;
            Debug.Log("HP: " + HP);
            animationPlayer.DamageAnimation();
            gotDamage = true;
            if (HP <= 0)
            {
                animationPlayer.DeathAnimation();
            }
            else
            {
                Invoke("endDamage", 1f);
            }
            
        }
    }
    void endDamage()
    {
        if(HP>0)
        {
            gotDamage = false;
            movement_Player.cantOperate = false;
        }
    }
}
