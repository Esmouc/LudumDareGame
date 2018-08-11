using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  public static GameManager instance = null;
  public AudioManager AudioManager = null;

  public enum GameState
  {
    MainMenu,
    InGame,
    PauseMenu,
    EndMenu
  };
  public GameState game_state;

  public float corruption_level;
  public float corruption_limit;
  public int corrupted_data; // Number of elements accumulated below

  public float chance_of_glitching;
  public float free_memory_chance;

  public int score;

  public bool started_game;
  public bool exit_game;
  public bool continued_game;
  public bool restart_game;


  private void Awake()
  {
    if (instance == null){
      instance = this;
    }
    else if (instance != this){
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  // Use this for initialization
  void Start () {
    AudioManager = FindObjectOfType<AudioManager>();
    if(AudioManager == null) Debug.Log("Audio Manager not found");

		game_state = GameState.MainMenu;

    started_game = false;
    exit_game = false;
    continued_game = false;
    restart_game = false;

    score = 0;
	}

  void Restart()
  {
    started_game = false;
    exit_game = false;
    continued_game = false;
    restart_game = false;

    score = 0;
  }
	
	// Update is called once per frame
	void Update () {
    switch(game_state) {
      case GameState.MainMenu: MainMenuUpdate();
      return;
      case GameState.InGame: InGameUpdate();
      return;
      case GameState.PauseMenu: PauseMenuUpdate();
      return;
      case GameState.EndMenu: EndMenuUpdate();
      return;
    }

    if(exit_game) Application.Quit();
	}

  void MainMenuUpdate()
  {
    if(started_game) {
      game_state = GameState.InGame;
      SceneManager.LoadScene(1);
    }
  }

  void InGameUpdate()
  {
    if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
      game_state = GameState.PauseMenu;
      Time.timeScale = 0.0f;
    }

    if(corruption_level >= corruption_limit) {
      game_state = GameState.EndMenu;
    }
  }

  void PauseMenuUpdate()
  {
    if(continued_game) {
      continued_game = false;
      game_state = GameState.InGame;
      Time.timeScale = 1.0f;
    }
  }

  void EndMenuUpdate()
  {
    if(restart_game) {
      Restart();
      game_state = GameState.InGame;
      SceneManager.LoadScene(1);
    }
  }


}
