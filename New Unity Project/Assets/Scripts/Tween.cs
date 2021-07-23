using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tween : MonoBehaviour
{
    public float tweenTime;
    public float timer;
    public GameObject[] array;
  
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            TweenObject();
        }
    }
    public void TweenObject()
    {
        foreach (var x in array)
        {
            LeanTween.cancel(x);
            transform.localScale = Vector3.one;
            LeanTween.scale(x, Vector3.one * 1.1f, tweenTime).setEaseShake();
        }
    }

}
