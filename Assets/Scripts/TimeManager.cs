using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject panel;
    public float slowdownFactor = 0.05f;
    public float slowdownLenght = 2f;

    public IEnumerator DoSlowMotion()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(0.01f);
        panel.SetActive(true);
        

    }
}
