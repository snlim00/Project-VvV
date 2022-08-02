using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPref;
    [SerializeField] private float[] spawnDelay;
    private float[] firstSpawnDelay;
    [SerializeField] private float[] lastSpawnDelay;
    [SerializeField] private CoronaController corona;

    public bool doSpawn = true;

    private void Awake()
    {
        Init();
    }
    void Init()
    {
        firstSpawnDelay = new float[spawnDelay.Length];

        for(int i = 0; i < spawnDelay.Length; ++i)
        {
            firstSpawnDelay[i] = spawnDelay[i];

            StartCoroutine(enemyGeneration(i));
        }

        StartCoroutine(DecDelay());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            i += 30;
        }
    }

    IEnumerator enemyGeneration(int enemyIndex)
    {
        while(doSpawn == true)
        {
            yield return new WaitForSeconds(spawnDelay[enemyIndex]);

            Instantiate(enemyPref[enemyIndex]);
        }
    }

    float i = 0;
    IEnumerator DecDelay()
    {
        i = 0;
        WaitForSeconds wfs = new WaitForSeconds(10f);

        while(i < 19)
        {
            for(int j = 0; j < spawnDelay.Length; ++j)
            {
                spawnDelay[j] = Mathf.Lerp(firstSpawnDelay[j], lastSpawnDelay[j], i / 19f);
            }
            ++i;
            Debug.Log(i);

            yield return wfs;
        }
        Debug.Log("coronaspawn");
        StartCoroutine(corona.Appare());
    }
}
