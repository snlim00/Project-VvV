using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneMgr : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BackgroundController[] bg;
    [SerializeField] private Image cover;

    [SerializeField] private Material[] stageMat;

    [SerializeField] private GameObject[] GuideImage;
 
    // Start is called before the first frame update
    void Start()
    {
        bg[GameInfo.stage - 1].transform.position = new Vector3(0, 0, 99);

        cover.color = Color.black;

        if(GameInfo.stage == 2)
        {
            Touch();
            Touch();
        }
    }

    void Touch()
    {
        switch (startCount)
        {
            case 0:
                GuideImage[0].SetActive(false);
                GuideImage[1].SetActive(true);
                break;

            case 1:
                GuideImage[0].SetActive(false);
                GuideImage[1].SetActive(false);

                StartCoroutine(Scene());
                break;
        }

        startCount += 1;
    }

    int startCount = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && startCount <= 1)
        {
            Touch();
        }
    }

    IEnumerator Scene()
    {
        yield return new WaitForSeconds(0.5f);

        float t = 0;

        Vector2 pos = player.transform.position;

        while (t < 1)
        {
            Debug.Log(t);
            t += Time.deltaTime;

            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, Mathf.Lerp(1, 0, t));
            Debug.Log(Mathf.Lerp(0, 255, t));

            yield return null;
        }

        t = 0;
        pos = player.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / 5;

            player.transform.position = Vector2.Lerp(pos, new Vector2(0, -1), t);
            bg[GameInfo.stage - 1].scrollSpeed = Mathf.Lerp(0.5f, 2, t);

            yield return null;
        }

        t = 0;
        pos = player.transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / 1.5f;

            player.transform.position = Vector2.Lerp(pos, new Vector2(0, -1.5f), t);

            yield return null;
        }

        t = 0;
        pos = player.transform.position;
        while(t < 1)
        {
            t += Time.deltaTime / 2;

            player.transform.position = Vector2.Lerp(pos, new Vector2(0, 6.5f), t);
            bg[GameInfo.stage - 1].scrollSpeed = Mathf.Lerp(2, 6, t * 0.67f);

            if(t > 0.5f)
            {
                cover.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, t);
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if(GameInfo.stage == 1)
            SceneManager.LoadScene(SCENE.STAGE_1);
        else if(GameInfo.stage == 2)
            SceneManager.LoadScene(SCENE.STAGE_2);
    }
}
