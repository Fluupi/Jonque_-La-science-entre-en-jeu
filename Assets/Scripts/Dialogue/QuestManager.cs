using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class QuestManager : MonoBehaviour
{
    float animIndex1, animIndex2, animIndex3, animIndex4;
    public AnimationCurve popAnimCurve;
    public float speed;
    public RectTransform Quest1, Quest2, Quest3, Quest4;
    public float timeIncr;
    float timer;
    int eventCounter;
    bool startEnd;
    public UnityEvent activateOlympieEvent;

    public void endQuests()
    {
        startEnd = true;
    }

    private void Update()
    {
        if (startEnd)
        {
            timer += Time.deltaTime;

            if(timer> timeIncr)
            {
                if(eventCounter == 0)
                {
                    StartCoroutine(popIn(new Vector2(-9, 0), new Vector2(310, 0), animIndex1, Quest1));
                    
                }
                else if(eventCounter == 1)
                {
                    StartCoroutine(popIn(new Vector2(-9, -110), new Vector2(310, -110), animIndex2, Quest2));
                }
                else if(eventCounter == 2)
                {
                    StartCoroutine(popIn(new Vector2(-9, -220), new Vector2(310, -220), animIndex3, Quest3));
                }
                else if(eventCounter == 3)
                {
                    StartCoroutine(popIn(new Vector2(360, -110), new Vector2(-50, -110), animIndex4, Quest4));

                }
                timer = 0;
                eventCounter++;
            }
        }
    }

    IEnumerator popIn(Vector2 startPos, Vector2 endPos, float index, RectTransform Quest)
    {
        index += Time.deltaTime * speed;

        Quest.anchoredPosition = Vector2.Lerp(startPos, endPos, popAnimCurve.Evaluate(index));

        if (index >= 1)
        {
            Quest.anchoredPosition = endPos;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(popIn(startPos, endPos, index, Quest));
        }
    }

}
