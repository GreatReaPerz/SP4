using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]
    GameObject projectileObject;

    private GameObject theProjectile;
    private TroopAI enemy;
    private TroopAI thisArcher;
    private float projDmg;
    Vector3 dir = new Vector3();
    // Use this for initialization
    void Start()
    {
        Debug.Log(projectileObject.name);
    }

    // Update is called once per frame
    void Update()
    {

        if (theProjectile != null)
        {
            Debug.Log("Update 1");
            if (thisArcher.health.getHealth() <= 0)
            {
                Destroy(theProjectile);
                return;
            }
            if (Collided(theProjectile, enemy))
            {
                Destroy(theProjectile);
                enemy.health.addHealth(projDmg);
                //thisArcher.fireProj = false;
                return;
            }
            theProjectile.transform.position += dir * Time.deltaTime * 300;
        }
    }

    public void CreateProjectile(TroopAI theArcher, TroopAI theEnemy,float damage)
    {
        Debug.Log("Create");
        theProjectile = new GameObject();
        enemy = theEnemy;
        thisArcher = theArcher;
        //theArcher.fireProj = true;
        projDmg = damage;
        dir = (theEnemy.transform.position - theArcher.transform.position).normalized;
        Debug.Log(dir);
        Vector3 offset = new Vector3(0, theArcher.range / 4, 0);
        if (dir.y == -1)
            theProjectile = Instantiate(projectileObject, theArcher.transform.position - offset, theArcher.transform.rotation);
        else
            theProjectile = Instantiate(projectileObject, theArcher.transform.position + offset, theArcher.transform.rotation);

        theProjectile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        //Instantiate(projectile, position, rotation);
        //transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform.transform);
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
