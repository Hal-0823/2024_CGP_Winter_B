using UnityEngine;
using UnityEngine.UI; 

public class StatementPlayer : Information_Player
{
    public bool noDamage;
    Movement_Player movement_Player;
    AnimationPlayer animationPlayer;
    ScoreManager scoreManager;
    float rollCoolTime = 15f;
    float downTime;
    public Image progressImage;    // 進捗バー（円形）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationPlayer = this.GetComponentInParent<AnimationPlayer>();
        movement_Player = this.GetComponentInParent<Movement_Player>();
        downTime=0f;
        progressImage = GameObject.Find("RollStamina").GetComponent<Image>();
        progressImage.fillAmount = 1;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!movement_Player.canRoll)
        {
            progressImage.fillAmount = downTime / rollCoolTime;
            downTime += Time.deltaTime;
            if(downTime>=rollCoolTime)
            {
                movement_Player.canRoll = true;
                downTime=0f;
            }
            
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy")&&!noDamage)
        {
            movement_Player.GotDamage();
            HP -= 1;
            scoreManager.GotDamageEffectForScore();
            Debug.Log("HP: " + HP);
            animationPlayer.DamageAnimation();
            noDamage = true;
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
            noDamage = false;
            movement_Player.cantOperate = false;
        }
    }
}
