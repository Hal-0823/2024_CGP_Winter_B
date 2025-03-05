using UnityEngine;
using UnityEngine.UI; 

public class StatementPlayer : Information_Player
{
    public bool noDamage;
    Movement_Player movement_Player;
    AnimationPlayer animationPlayer;
    ScoreManager scoreManager;
    float rollCoolTime = 10f;
    float downTime;
    public Image progressImage;    // 進捗バー（円形）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationPlayer = this.GetComponentInParent<AnimationPlayer>();
        movement_Player = this.GetComponentInParent<Movement_Player>();
        downTime=0f;
        progressImage = GameObject.Find("RollStaminaFill").GetComponent<Image>();
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
            AudioManager.I.PlaySE(SE.Name.BadReaction);

            
            if(scoreManager.GotDamageEffectForScore()<=-5000)
            {
                //ゲームオーバー処理はここに書く
                animationPlayer.DeathAnimation();
                noDamage = true;
                Destroy(scoreManager.gameObject,1.0f);
            }
            else
            {
                animationPlayer.DamageAnimation();
                AudioManager.I.PlaySE(SE.Name.Hit);
                noDamage = true;
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
