using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public Text highScore;

    public int grados;
    public bool changeMove;
    public Vector3 offset;
    public Transform pivoteRotation;
    public GameObject player;
  
    //Start Menu Objects
    //public Animator playerAnimation;

    public GameObject title;
    public GameObject touchMessage;
    public Transform menuCamera;
    public Transform runCamera;
    public GameObject camera;
    public float speed;

    public TimeManager timeManager;
    public Text scoreText;
    public bool startAction;
    public bool startGame;
    public float view;

    Score score;

    public bool gameOver = false;

     Movement movement;

    #region Singlenton
    public static GameController Instance;

    private void Awake()
    {
        Instance = this;

        
    }
    #endregion

    private void Start()
    {
        highScore.text=PlayerPrefs.GetInt("HighScore", 0).ToString();
        changeMove = false;
        startAction = false;
        startGame = false;
        score = Score.Instance;
         view= camera.GetComponent<Camera>().fieldOfView;


        movement = Movement.Instance;
        
    }

    public bool getGameOver()
    {
        return gameOver;
    }
    private void Update()
    {

        if (Input.touchCount > 0 && startAction == false || Input.GetKey(KeyCode.Space))
        
            {

            startAction = true;
            }

        if(startAction==true)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, grados, 0), speed * Time.deltaTime);

            DisableStartScreen();
            if (changeMove == false)
            {
                if (Mathf.Abs(camera.transform.position.x - (player.transform.position.x + offset.x)) > 0.01)
                {
                    camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position + offset, speed * Time.deltaTime);
                    player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position - Vector3.forward * player.transform.position.z, 2000000 * Time.deltaTime);
                }
                else
                {

                    changeMove = true;
                    startGame = true;
                }
                camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, pivoteRotation.rotation, speed * Time.deltaTime * 0.9f);

            }
        }
    }

     void LateUpdate()
    {
        if (startGame == true && changeMove==true)
        {
            camera.transform.position = player.transform.position+offset;
           // player.GetComponent<Animator>().enabled = false;
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, pivoteRotation.rotation, speed * Time.deltaTime * 0.9f);
           // player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, grados, 0), speed * Time.deltaTime);
            if (view < 110)
            {
                camera.GetComponent<Camera>().fieldOfView = view + 1;
                view += 0.1f;
            }
        }
    }


    public void EndGame()
    {
       

        if (gameOver == false)
        {
            gameOver = true;
           
            StartCoroutine(timeManager.DoSlowMotion());
            
            ShowScore();
            
        }
    }
   public void   ShowScore()
    {

        scoreText.text = score.GetActualScore();
        if (int.Parse(scoreText.text) > int.Parse(highScore.text))
        {
            PlayerPrefs.SetInt("HighScore", int.Parse(scoreText.text));
            //highScore.text = scoreText.text + "m";
        }
        
            highScore.text += "m";
            scoreText.text += "m";

        score.DisableScore();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene("MainScene");
    }
    public void DisableStartScreen()
    {
       
        title.SetActive(false);
        touchMessage.SetActive(false);
    }


}
