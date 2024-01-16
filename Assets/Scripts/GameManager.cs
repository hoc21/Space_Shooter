using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject playerShip;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject scoreUITextGo;
    [SerializeField] GameObject traiDat;
    [SerializeField] GameObject backGround;
    [SerializeField] GameObject Pause;
    [SerializeField] GameObject Joystick;
    [SerializeField] bool paused;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    void Start()
    {
        GMState = GameManagerState.Opening;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }

    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:

                GameOver.SetActive(false);
                playButton.SetActive(true);
                traiDat.SetActive(true);
                backGround.SetActive(false);
                Joystick.SetActive(false);
                break;
            case GameManagerState.Gameplay:
            scoreUITextGo.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                traiDat.SetActive(false);
                backGround.SetActive(true);
                Joystick.SetActive(true);
                playerShip.GetComponent<Player>().Init();
                enemySpawner.GetComponent<EnemySpawn>().ScheduleEnemySpawner();
                break;
            case GameManagerState.GameOver:
                enemySpawner.GetComponent<EnemySpawn>().UnscheduleEnemySpawner();
                GameOver.SetActive(true);
                Joystick.SetActive(false);
                Invoke("ChangeToOpeningState",5f);
                break;
        }
    }
    public void SetGameManagerState(GameManagerState state) 
    {
            GMState = state;
            UpdateGameManagerState();
    }
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
    void ChangePause()
    {
        if (! paused)
            {
                paused = true;
                Pause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                Pause.SetActive(false);
                Time.timeScale = 1;
            }
    }
}
