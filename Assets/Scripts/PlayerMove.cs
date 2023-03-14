using UnityEngine;

using UnityEngine.AI;

public enum SlimeAnimationState { Idle,Walk,Jump,Attack,Damage}

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private Face faces;
    [SerializeField] private GameObject slimeBody;
    [SerializeField] public SlimeAnimationState currentState;

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private int damType;

    [SerializeField] private EnemySpawner spawner;

    private bool move;
    public bool isReached;
    private Material faceMaterial;
    
    void Start()
    {
        faceMaterial = slimeBody.GetComponent<Renderer>().materials[1];
        WalkToNextDestination();
    }

    public void WalkToNextDestination()
    {
        isReached = false;
        currentState = SlimeAnimationState.Walk;
        Vector3 nextWaypoint = gameObject.transform.position + new Vector3(10, 0, 0);
        agent.SetDestination(nextWaypoint);
        SetFace(faces.WalkFace);
    }
    
    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    void Update()
    {
        AnimationStates();
    }
   
    private void AnimationStates()
    {
        switch (currentState)
        {
            case SlimeAnimationState.Idle:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                StopAgent();
                SetFace(faces.Idleface);
                break;

            case SlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                agent.isStopped = false;
                agent.updateRotation = true;
                

                // agent reaches the destination
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    isReached = true;
                    currentState = SlimeAnimationState.Idle;
                    spawner.NextWave();
                }
                
                animator.SetFloat("Speed", agent.velocity.magnitude);

                break;

            
            case SlimeAnimationState.Attack:
            
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");
                
            
                break;
        }
    }

    void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
        agent.updateRotation = false;
    }

    
    
}
