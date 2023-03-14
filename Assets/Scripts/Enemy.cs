using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float hp;
    
    [SerializeField] private float attackRange;
    [SerializeField] private int coinsForKill;

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject TextEffect;
    [SerializeField] private ParticleSystem GetDamageEffect;
    [SerializeField] private Slider slider;

    private GameManager gameManager;
    private EnemySpawner enemySpawner;
    private Player player;
    
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        
        slider.maxValue = hp;
        slider.value = hp;
    }

    private void Update()
    {
        Move();
    }
    
    
    private void Move()
    {

        if (DistanceToPlayer() > attackRange)
        {
            agent.SetDestination(player.transform.position + new Vector3(attackRange,0,0));
            transform.LookAt(player.transform);
            agent.isStopped = false;
            
            animator.SetFloat("Speed", agent.velocity.magnitude);
            
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetTrigger("Attack");
        }
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void Attack()
    {
        player.GetDamage(damage);
        
    }

    public void GetDamage(float damage)
    {
        TextEffect.GetComponentInChildren<TextMeshPro>().text = "-" + damage;
        TextEffect.GetComponent<Animation>().Play();
        hp -= damage;
        slider.value = hp;
        GetDamageEffect.Play();
        if (hp <= 0)
        {
            gameManager.ChangeCoinsValue(coinsForKill);
            Destroy(gameObject);
        }
    }
}
