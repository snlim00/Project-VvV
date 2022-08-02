using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cover;

    public void GameStart()
    {
        StartCoroutine(_GameStart());
    }

    IEnumerator _GameStart()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, Mathf.Lerp(0, 1, t));

            yield return null;
        }

        SceneManager.LoadScene(SCENE.START_SCENE);
    }
}
