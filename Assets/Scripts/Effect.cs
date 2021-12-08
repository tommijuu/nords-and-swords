using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    GameObject enemyHitEffect;
    public GameObject enemy;
    public GameObject player;

    public Vector3 enemyOffset;
    public Vector3 playerOffset;

    public GameObject golemHitEffect;
    public GameObject skullHitEffect;
    public GameObject hiisiHitEffect;

    public GameObject playerHitEffect;


    GameObject enemyBlockEffect;

    public GameObject golemBlockEffect;
    public GameObject playerBlockEffect;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");


        if (enemy.gameObject.name.Contains("Golem"))
        {
            enemyHitEffect = golemHitEffect;
            enemyBlockEffect = golemBlockEffect;

            enemyOffset = new Vector3(-0.5f, 2, -2);
            playerOffset = new Vector3(0.3f, 1, -1);
        }
        else if (enemy.gameObject.name.Contains("Skull"))
        {
            enemyHitEffect = skullHitEffect;

            enemyOffset = new Vector3(0, 1.75f, -0.5f);
            playerOffset = new Vector3(0, 1, -0.5f);
        }
        else if (enemy.gameObject.name.Contains("Leshen"))
        {
            enemyHitEffect = hiisiHitEffect;

            enemyOffset = new Vector3(0, 1.75f, -1);
            playerOffset = new Vector3(0, 0.4f, -1);
        }
        else if (enemy.gameObject.name.Contains("training"))
        {
            enemyHitEffect = playerHitEffect;
            enemyBlockEffect = playerBlockEffect;

            enemyOffset = new Vector3(0, 1.0f, -1);
            playerOffset = new Vector3(0, 1.0f, -1);
        }
    }

    //player attacks
    public void PlayEffectHeavy()
    {
        StartCoroutine(PlayPlayerEffect(1.3f));
    }

    public void PlayEffectMedium()
    {
        StartCoroutine(PlayPlayerEffect(0.5f));
    }

    public void PlayEffectQuick()
    {
        StartCoroutine(PlayPlayerEffect(0.5f));
    }


    //enemy attacks
    public void PlayEnemyHeavy()
    {
        StartCoroutine(PlayEnemyEffect(1.3f));
    }

    public void PlayEnemyMedium()
    {
        StartCoroutine(PlayEnemyEffect(0.5f));
    }

    public void PlayEnemyQuick()
    {
        StartCoroutine(PlayEnemyEffect(0.5f));
    }


    public IEnumerator PlayPlayerEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effect = Instantiate(playerHitEffect, enemy.transform.position + enemyOffset, Quaternion.identity);
        Destroy(effect, 1f);
    }

    public IEnumerator PlayEnemyEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effect = Instantiate(enemyHitEffect, player.transform.position + playerOffset, Quaternion.identity);
        Destroy(effect, 1f);
    }




    public IEnumerator PlayerBlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effect = Instantiate(playerBlockEffect, player.transform.position + playerOffset, Quaternion.identity);
        Destroy(effect, 1f);
    }

    public IEnumerator EnemyBlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject effect = Instantiate(enemyBlockEffect, enemy.transform.position + enemyOffset, Quaternion.identity);
        Destroy(effect, 1f);
    }


    public void PlayEnemyBlock()
    {
        StartCoroutine(EnemyBlock(0.3f));
    }

    public void PlayPlayerBlock()
    {
        StartCoroutine(PlayerBlock(0.3f));
    }

    public void PlayPlayerBlock(float f)
    {
        StartCoroutine(PlayerBlock(f));
    }


}
