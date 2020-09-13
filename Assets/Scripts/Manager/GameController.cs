using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject restartPanel;
    public SoundManager soundManager;
    public bool isPaused = false;
    public GameObject pausePanel;
    public Ghost ghost;

    //PRIVATE
    private Board board;
    private Spawner spawner;
    private Shape activeShape;
    private float dropInterval = 0.2f;
    private float timeToDrop;
    private bool isGameOver;
    private ScoreManager scoreManager;

    private float timeToNextKey = 0;
    private float keyRepeatRate = 0.001f;
    private float dropIntervalModded;

    void Start()
    {
        timeToNextKey = Time.time;
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        spawner.transform.position = Vectorf.Round(spawner.transform.position);
        activeShape = getSpawnShape(spawner);
        soundManager = FindObjectOfType<SoundManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        ghost = FindObjectOfType<Ghost>();

        if (pausePanel)
            pausePanel.SetActive(false);

        if (restartPanel)
            restartPanel.SetActive(false);

        dropIntervalModded = dropInterval;
    }

    private Shape getSpawnShape(Spawner spawner)
    {
        return spawner.GetSpawnShape().GetComponent<Shape>(); ;
    }

    void Update()
    {
        if (!board || !spawner || !activeShape || isGameOver || !soundManager || !scoreManager)
        { return; }

        FallingAction();

        if (Time.time > timeToNextKey)
        {
            timeToNextKey = Time.time + keyRepeatRate;
            InputListener();
        }


    }

    private void FallingAction()
    {
        if (Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropIntervalModded;

            if (activeShape)
            {
                activeShape.moveDown();

                if (!board.IsValidPosition(activeShape))
                {
                    activeShape.moveUp();
                    board.StoreShapeInGrid(activeShape);
                    ghost.ResetGhost();
                    if (board.IsOverLimit(activeShape))
                    {
                        GameOver();
                        return;
                    }

                    activeShape = getSpawnShape(spawner);
                    ClearRows();
                    PlaySoundAtOnce(soundManager.dropSound);
                }

            }

        }
    }

    private void GameOver()
    {
        isGameOver = true;
        PlaySoundAtOnce(soundManager.gameOverSound, 3f);
        PlaySoundAtOnce(soundManager.gameOverClip, 1.7f);
        if (restartPanel)
            restartPanel.SetActive(true);
    }

    private void InputListener()
    {
        if (Input.GetButtonDown("MoveRight"))
        {
            activeShape.moveRight();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.moveLeft();
                PlaySoundAtOnce(soundManager.errorSound, 0.5f);
            }
            else
            {
                PlaySoundAtOnce(soundManager.moveSound);
            }
        }
        else if (Input.GetButtonDown("MoveLeft"))
        {
            activeShape.moveLeft();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.moveRight();
                PlaySoundAtOnce(soundManager.errorSound, 0.5f);
            }
            else
            {
                PlaySoundAtOnce(soundManager.moveSound);
            }
        }
        else if (Input.GetButton("MoveDown"))
        {
            activeShape.moveDown();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.moveUp();
                ghost.ResetGhost();
                ClearRows();

                if (board.IsOverLimit(activeShape))
                {
                    GameOver();
                    return;
                }

            }
            else
            {
                PlaySoundAtOnce(soundManager.moveSound, 0.2f);
            }
        }
        else if (activeShape.isRotate && Input.GetButtonDown("Rotation"))
        {
            activeShape.rotateLeft();
            if (!board.IsValidPosition(activeShape))
            {
                activeShape.rotateRight();
                PlaySoundAtOnce(soundManager.errorSound, 0.5f);
            }
            else
            {
                PlaySoundAtOnce(soundManager.moveSound);
            }
        }
        else if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PlaySoundAtOnce(AudioClip audioClip, float volMultiplay = 0.5f)
    {
        if (soundManager.isFxEnable && audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, Mathf.Clamp(soundManager.fxVolume * volMultiplay, 0.05f, 1f));
        }
    }


    private void ClearRows()
    {
        board.ClearAllRow();
        if (board.clearedRowCountAtOnce > 0)
        {
            scoreManager.ScoreLines(board.clearedRowCountAtOnce);
            PlaySoundAtOnce(soundManager.clearSound, 1.5f);
            if (scoreManager.isLevelUp)
            {
                PlaySoundAtOnce(soundManager.levelUpClip);
                dropIntervalModded = Mathf.Clamp(dropInterval - ((float)scoreManager.level - 1) * 0.05f, 0.05f, 1f);
            }
            else
                PlaySoundAtOnce(soundManager.GetRandomAudioClip(soundManager.vocalSounds), 1.3f);
        }
    }

    public void TogglePause()
    {
        if (isGameOver)
            return;

        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);
        if (soundManager)
        {
            soundManager.musicSource.volume = isPaused ? soundManager.musicVolume * 0.2f : soundManager.musicVolume;

        }
        Time.timeScale = isPaused ? 0 : 1;


    }

    void LateUpdate()
    {
        if(ghost)
        {
            ghost.DrawGhost(activeShape,board);
        }
    }


}
