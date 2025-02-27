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
    bool jumpCoolTime;
    public static bool isGrounded;  
    GameObject sensor;
    AnimationPlayer animationPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        animationPlayer = this.GetComponent<AnimationPlayer>();
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
            //シフトで移動速度を変更
            if((Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.Mouse3))&&!cantOperate)
            {
                cantOperate = true;
                animationPlayer.RollingAnimation();
                
                isMoving = true;
                Invoke("Rolling",0.21f);
                Invoke("EndRolling",0.85f);
                
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
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Mouse4)) && isGrounded&&!jumpCoolTime&&!cantOperate)
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
        moveSpeed = playerRollSpeed;
        sensor = Instantiate(rollSencor,this.transform);
        Destroy(sensor,0.3f);
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
        if(sensor!=null)
        sensor.GetComponent<SensorParent>().isBanned = true;
    }
    
    void Jump()
    {
        // 上方向に力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCoolTime = true;
        sensor = Instantiate(jumpSencor,this.transform);
        Destroy(sensor,0.3f);
        Invoke("ResetJumpCoolTime",1.2f);
    }
    void EndRolling()
    {
        moveSpeed = playerSpeed;
        if(isMoving)
        {
            cantOperate = false;
        }
    }
    void ResetJumpCoolTime()
    {
        jumpCoolTime = false;
    }
    
    public void LookAtEnemy(GameObject enemy)
    {
        Debug.Log("LookAtEnemyが呼び出されたよん");
        Vector3 relativePos = enemy.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = rotation;
    }
}
