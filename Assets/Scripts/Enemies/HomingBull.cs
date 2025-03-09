using UnityEngine;

public class HomingBull : BullController
{
    const float WALK_SPEED = 1.0f;
    const float DUSH_SPEED = 12.0f;
    public float RotateSpeed = 60f;
    private enum State { Walk, Target, Dush, Slow };
    private State currentState = State.Walk;
    private Transform player;
    private Vector3 targetPos;
    private float timer = 0f;
    
    public override void Initialize(Vector3 start, Vector3 end, bool isSpawn)
    {
        base.Initialize(start, end);
        player = GameObject.FindWithTag("Player").transform;
        Speed = WALK_SPEED;
    }

    void Update()
    {
        switch(currentState)
        {
            case State.Walk:
                Walk();
                break;
            case State.Target:
                Target();
                break;
            case State.Dush:
                Dush();
                break;
            case State.Slow:
                Slow();
                break;
        }
    }

    private void Walk()
    {
        timer += Time.deltaTime;
        base.Move();

        if (timer > 1.0f)
        {
            currentState = State.Target;
            timer = 0f;
        }
    }

    private void Target()
    {
        timer += Time.deltaTime;
        targetPos = player.position;

        Vector3 targetDirection = targetPos - transform.position;
        targetDirection.y= 0;
        targetDirection = targetDirection.normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), RotateSpeed * Time.deltaTime);
        direction = targetDirection;

        if (timer > 1.8f)
        {
            currentState = State.Dush;
            timer = 0f;
        }
        base.Move();
    }
    
    private void Dush()
    {
        timer += Time.deltaTime;

        if (timer < 1.0f)
        {
            Speed = Mathf.Max(WALK_SPEED, DUSH_SPEED * ((timer+0.5f)*(timer+0.5f) / 2));
        }
        else if (timer > 2.0f)
        {
            currentState = State.Slow;
            timer = 0f;
        }

        base.Move();
    }

    private void Slow()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            Speed -= Time.deltaTime * DUSH_SPEED;
        }
        
        if (Speed < WALK_SPEED)
        {
            currentState = State.Target;
            timer = 0;
        }

        base.Move();
    }
}
