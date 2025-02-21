using UnityEngine;

public class StatementPlayer : Information_Player
{
    bool gotDamage;
    Movement_Player movement_Player;
    AnimationPlayer animationPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationPlayer = this.GetComponent<AnimationPlayer>();
        movement_Player = this.GetComponent<Movement_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy")&&!gotDamage)
        {
            HP -= 1;
            Debug.Log("HP: " + HP);
            animationPlayer.DamageAnimation();
            gotDamage = true;
            movement_Player.cantOperate = true;
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
        gotDamage = false;
        movement_Player.cantOperate = false;
    }
}
