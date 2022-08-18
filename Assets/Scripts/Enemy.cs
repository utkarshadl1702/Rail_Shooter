using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathvfx;
    [SerializeField] GameObject BonusVfx;
    [SerializeField] GameObject DeathSound;
    [SerializeField] float EnemyHitPoint = 4;
    float comparer;

    [SerializeField] int DeathScore = 500;


    ScoreBoard ScoreHandler;
    GameObject ParentGameObject;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    [SerializeField] int ScorePerHit = 100;
    void Start()
    {
        ScoreHandler = FindObjectOfType<ScoreBoard>();
        ParentGameObject = GameObject.FindWithTag("Spawn At Runtime");
        comparer = EnemyHitPoint - 4;
    }

    void OnParticleCollision(GameObject other)
    {
        EnemyHitPoint--;

        if (EnemyHitPoint <= 0)
        {
            ProcessHit();
        }
        Handlescore();

        bonusVfxPlay();


    }
    void Handlescore()
    {
        ScoreHandler.IncreaseScore(ScorePerHit);
    }

    void ProcessHit()
    {
        


        GameObject vfx = Instantiate(deathvfx, transform.position, Quaternion.identity);
        vfx.transform.parent = ParentGameObject.transform;
        Instantiate(DeathSound, transform.position, Quaternion.identity);
        ScoreHandler.IncreaseScore(DeathScore);
        Destroy(gameObject);
    }

    void bonusVfxPlay()
    {
        GameObject vfx = Instantiate(BonusVfx, transform.position, Quaternion.identity);
    }

}
