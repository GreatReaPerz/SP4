using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 0;
    private float lifeTime = 0;

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
            if (Collided(theProjectile, enemy))
            {
                Destroy(theProjectile);
                enemy.health.addHealth(-projDmg);
                thisArcher.fireProj = false;
                return;
            }
            if (!thisArcher.activ)
            {
                Destroy(theProjectile);
                thisArcher.fireProj = false;
                return;
            }
            else if(!enemy.activ)
            {
                Destroy(theProjectile);
                thisArcher.fireProj = false;
                return;
            }

            theProjectile.transform.position += dir * Time.deltaTime * 500;
            lifeTime += Time.deltaTime;
            if(lifeTime > 1.5f)
            {
                Destroy(theProjectile);
                lifeTime = 0.0f;
                thisArcher.fireProj = false;

            }
        }


    }

    public void CreateProjectile(GameObject ProjectileOBJ, TroopAI theArcher, TroopAI theEnemy, float damage)
    {
        if (theProjectile != null)
            Destroy(theProjectile);
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

        GameObject theCanvas = GameObject.FindGameObjectWithTag("Canvas");
        Vector2 canvasLocalScale = theCanvas.transform.localScale;


        theProjectile.transform.SetParent(theCanvas.transform, true);
        theProjectile.transform.localScale = new Vector3(theProjectile.transform.localScale.x * canvasLocalScale.x, theProjectile.transform.localScale.y * canvasLocalScale.y, 0);
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
