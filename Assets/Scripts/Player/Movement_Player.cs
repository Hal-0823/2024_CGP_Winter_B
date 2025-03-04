using UnityEngine;
using System.Collections.Generic;
public class Movement_Player : Information_Player
{
    Rigidbody rb;
    GameObject player;
    public GameObject jumpSencor;
    public GameObject rollSencor;
    private bool isMoving = false;
    protected float moveSpeed;
    public bool cantOperate = false;

    bool canJump = true;
    public bool canRoll = true;
    public static bool isGrounded;  
    GameObject sensor;
    AnimationPlayer animationPlayer;

    StatementPlayer hitAreaScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        animationPlayer = this.GetComponent<AnimationPlayer>();
        hitAreaScript = GetComponentInChildren<StatementPlayer>();
        player = this.gameObject;
        moveSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        int moveX = 0;
        int moveZ = 0;
        
        if (Input.GetKey(KeyCode.W)) moveZ += 1;
        if (Input.GetKey(KeyCode.S)) moveZ -= 1;
        if (Input.GetKey(KeyCode.A)) moveX -= 1;
        if (Input.GetKey(KeyCode.D)) moveX += 1;
        

        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;
        if(isGrounded&&!cantOperate)
        {
            isMoving = direction != Vector3.zero;

            //シフトでローリング
            if((Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.Mouse3))&&!cantOperate&&canRoll)
            {
                cantOperate = true;
                canRoll = false;
                animationPlayer.RollingAnimation();
                hitAreaScript.noDamage = true;
                isMoving = true;
                Invoke("Rolling",0.20f);
                Invoke("EndRolling",0.8f);
                
            }
        }
        

        if(!isMoving||cantOperate)
        {
            animationPlayer.StopMoveAnimation();
        }

        if (isMoving&&isGrounded&&!cantOperate)
        {
            rb.rotation = Quaternion.LookRotation(direction);
        }


        // ジャンプ処理
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Mouse4)) && isGrounded&&canJump&&!cantOperate)
        {
            animationPlayer.JumpAnimation();
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            animationPlayer.MoveAnimation(moveSpeed);
            rb.MovePosition(rb.position + transform.forward * moveSpeed);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void Rolling()
    {
        if(isMoving)
        {
            AudioManager.I.PlaySE(SE.Name.Rolling);
            moveSpeed = playerRollSpeed;
            sensor = Instantiate(rollSencor,this.transform);
            Destroy(sensor,0.5f);
        }
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground"))
        {
            if(isGrounded==false)
            {
                AudioManager.I.PlaySE(SE.Name.Jump);
                isGrounded = true;
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;  // 地面に接している場合
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;  // 地面から離れた場合
            
        }
    }

    public void GotDamage()
    {
        cantOperate = true;
        isMoving = false;
        if(sensor!=null)
        {
            sensor.GetComponent<SensorParent>().isBanned = true;
            Destroy(sensor);
        }
    }


    
    void Jump()
    {
        // 上方向に力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
        sensor = Instantiate(jumpSencor,this.transform);
        Destroy(sensor,0.7f);
        Invoke("ResetJumpCoolTime",0.9f);
    }
    void EndRolling()
    {
        moveSpeed = playerSpeed;
        hitAreaScript.noDamage = false;
        if(isMoving)
        {
            cantOperate = false;
        }
    }
    void ResetJumpCoolTime()
    {
        canJump = true;
    }
    
    public void LookAtEnemy(GameObject enemy)
    {
        Debug.Log("LookAtEnemyが呼び出されたよん");
        Vector3 relativePos = enemy.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = rotation;
    }
}
