using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject ground2Prefab;
    public GameObject goalPrefab;
    public int spawnAmount;
    public Vector2 step = new Vector2(4.25f, 7.5f);
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnNewWave()
    {
        Vector2 spawnPos = Vector2.zero;
        for(int i = 0; i < spawnAmount; i++)
        {
            int randomDir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
            int chooseground = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
            spawnPos += new Vector2(step.x * randomDir, step.y);

            GameObject ground = new GameObject();

            if(i!=spawnAmount-1)//不是终点
            {   if(chooseground==1)
                    ground = Instantiate(groundPrefab, spawnPos-Vector2.up*2f, Quaternion.identity);
                else if(chooseground==-1)
                    ground = Instantiate(ground2Prefab, spawnPos - Vector2.up * 2f, Quaternion.identity);

            }
            else//终点
            {
                ground = Instantiate(goalPrefab, spawnPos - Vector2.up * 2f, Quaternion.identity);
            }
            ground.transform.parent = transform;//把地板全都放在gamemanager底下
            ground.transform.DOMove(ground.transform.position + Vector3.up * 2f, 0.5f).SetDelay(i*0.1f);//动画往上走
        }
    }
}
