using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 0;

    private GameObject theProjectile;
    private TroopAI enemy;
    private TroopAI thisArcher;
    private float projDmg;
    Vector3 dir = new Vector3();
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (theProjectile != null)
        {
            if (thisArcher.health.getHealth() <= 0)
            {
                Debug.Log("HERE");
                Destroy(theProjectile);
                thisArcher.fireProj = false;
                return;
            }
            else if(enemy.health.getHealth() <= 0)
            {
                Debug.Log("HERE2");
                Destroy(theProjectile);
                thisArcher.fireProj = false;
                return;
            }
            Debug.Log("Projectile " + theProjectile.transform.position);
            Debug.Log("Enemy " + enemy.transform.position);
            if (Collided(theProjectile, enemy))
            {
                Debug.Log("Collide");
                Destroy(theProjectile);
                enemy.health.addHealth(-projDmg);
                thisArcher.fireProj = false;
                return;
            }
            theProjectile.transform.position += dir * Time.deltaTime * 300;
        }
    }

    public void CreateProjectile(GameObject ProjectileOBJ, TroopAI theArcher, TroopAI theEnemy, float damage)
    {
        enemy = theEnemy;
        thisArcher = theArcher;
        thisArcher.fireProj = true;
        projDmg = damage;
        dir = (theEnemy.transform.position - theArcher.transform.position).normalized;
        Vector3 offset = new Vector3(0, theArcher.range / 4, 0);
        if (dir.y < 0)
            theProjectile = Instantiate(ProjectileOBJ, theArcher.transform.position - offset, theArcher.transform.rotation);
        else
            theProjectile = Instantiate(ProjectileOBJ, theArcher.transform.position + offset, theArcher.transform.rotation);

        theProjectile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
    }
    bool Collided(GameObject projectile, TroopAI theEnemy)
    {
        if (projectile.transform.position.x < theEnemy.transform.position.x + (99 * 0.5f)
    && projectile.transform.position.x > theEnemy.transform.position.x - (99 * 0.5f)
    && projectile.transform.position.y < theEnemy.transform.position.y + (99 * 0.5f)
    && projectile.transform.position.y > theEnemy.transform.position.y - (99 * 0.5f))
            return true;

        return false;
    }

}
