using System;
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    public GameObject target;
    public float damage;

    
    private Tween tweener;


    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private int jumpCount;

    void Start()
    {
        tweener = transform.DOJump(target.transform.position, jumpHeight, jumpCount, jumpDuration);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            tweener.Kill();
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
