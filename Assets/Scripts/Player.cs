using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHp = 100f;
    public float hp = 100f;
    public int damage;
    public float attackSpeed;
    [SerializeField] private GameObject textEffectPrefab;
    [SerializeField] private ParticleSystem AttackEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    
    private Queue<GameObject> Enemy;
    private GameObject currentTarget;
    private PlayerMove playerMoveScript;
    private GameManager gameManager;

    private float timer;
    private bool toNextPoint;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMoveScript = gameObject.GetComponent<PlayerMove>();
        Enemy = new Queue<GameObject>();
        
        gameManager.ChangeHpSliderValue(hp, maxHp);
    }

    void Update()
    {
        if (!toNextPoint)
        {
            if (Enemy.Count > 0 && currentTarget == null)
            {
                SearchTarget();
            }
            else if (Enemy.Count > 0 || currentTarget != null)
            {
                playerMoveScript.currentState = SlimeAnimationState.Attack;
            }
            else if (playerMoveScript.isReached)
            {
                toNextPoint = true;
            }
        }
        else
        {
            playerMoveScript.WalkToNextDestination();
            toNextPoint = false;
        }
        
        HelthRegen();
    }

    private void HelthRegen()
    {
        if (hp<maxHp)
        {
            hp += Time.deltaTime * 1f;
            gameManager.ChangeHpSliderValue(hp,maxHp);
        }
    }
    private void SearchTarget()
    {
        currentTarget = Enemy.Dequeue();
    }

    public void AddEnemy(GameObject enemy)
    {
       Enemy.Enqueue(enemy);
       
    }
    
    public void Attack()
    {
        AttackEffect.Play();
        if (currentTarget != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().target = currentTarget;
            bullet.GetComponent<Bullet>().damage = damage;
        }
        
    }
    
    public void GetDamage(int damage)
    {
        if (hp > 0)
        {

            textEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + damage;
            textEffectPrefab.GetComponent<Animation>().Play();
            hp -= damage;
            gameManager.ChangeHpSliderValue(hp, maxHp);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeAttackSpeed(float value)
    {
        attackSpeed += value;
        
        animator.SetFloat("AttackSpeed", attackSpeed);
    }
}
