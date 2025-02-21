using UnityEngine;

public class AnimationPlayer : Information_Player
{
    private Animator anim;  //Animatorをanimという変数で定義する
    private Vector3 before_position;
    private Vector3 after_position;
    //===== 初期処理 =====
    void Start()
    {
        //変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {

    }


    public void JumpAnimation()
    {
        anim.SetTrigger("jump");
    }

    public void MoveAnimation(float moveSpeed)
    {
        if (moveSpeed == playerSpeed)
        {
            anim.SetBool("walking", true);
            anim.SetBool("running", false);
        }
        else if(moveSpeed == playerDashSpeed)
        {
            anim.SetBool("running", true);
        }
    }
    public void StopMoveAnimation()
    {
        anim.SetBool("running", false);
        anim.SetBool("walking", false);
    }
    public void DamageAnimation()
    {
        anim.SetTrigger("damage");
    }
    public void DeathAnimation()
    {
        anim.SetBool("bldeath", true);
    }

}
