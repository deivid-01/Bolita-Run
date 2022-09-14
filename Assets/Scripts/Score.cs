using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public GameObject scoreObject;
    public Text actualScore;
    GameController gameController;
    #region Singlenton
    public static Score Instance;

    private void Awake()
    {
        Instance = this;
        

    }
    #endregion
    void Start()
    {
        //  actualScore.text = "RUN!";
        gameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.startGame == true)
        {
            scoreObject.SetActive(true);
            if (Time.time > 5 && gameController.gameOver==false)
                actualScore.text = ((int)this.transform.position.x * -1).ToString()+"m";
        }
    }

    public string  GetActualScore()
    {
        return actualScore.text.Substring(0, actualScore.text.Length - 1);
    }

    public void DisableScore()
    {
        actualScore.text = "";
        scoreObject.SetActive(false);
       
        
    }
    
}
