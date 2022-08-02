using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultCover : MonoBehaviour
{
    private Image img;

    [SerializeField] Text score1;
    [SerializeField] Text score2;
    [SerializeField] Text score;

    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();

        score1.text = "Stage 1   " + (int)GameInfo.firstStageScore;
        score2.text = "Stage 2   " + (int)GameInfo.secondStageScore;
        GameInfo.finalScore = GameInfo.firstStageScore + (int)GameInfo.secondStageScore;
        score.text = "Score   " + (int)GameInfo.finalScore;

        StartCoroutine(SetAlpha());
    }

    IEnumerator SetAlpha()
    {
        float t = 0;

        while (t < 1)
        {
            Debug.Log(t);
            t += Time.deltaTime;

            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(1, 0, t));
            Debug.Log(Mathf.Lerp(0, 255, t));

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(SCENE.MAIN_MENU);
    }
}
