using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

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

  private bool started_game;
  private bool exit_game;
  private bool continued_game;
  private bool restart_game;

  private PostProcessingProfile camera_effects;


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
    corruption_level = 0.0f;
    corrupted_data = 0;

    camera_effects = null;
	}

  void Restart()
  {
    started_game = false;
    exit_game = false;
    continued_game = false;
    restart_game = false;

    score = 0;
    corruption_level = 0.0f;
    corrupted_data = 0;
  }

  void RestartGraphicProfile()
  {
    VignetteModel.Settings temp_vignette = camera_effects.vignette.settings;
    temp_vignette.intensity = 0.0f;
    camera_effects.vignette.settings = temp_vignette;
    GrainModel.Settings temp_grain = camera_effects.grain.settings;
    temp_grain.intensity = 0.0f;
    camera_effects.grain.settings = temp_grain;
    ChromaticAberrationModel.Settings temp_aberration = camera_effects.chromaticAberration.settings;
    temp_aberration.intensity = 0.0f;
    camera_effects.chromaticAberration.settings = temp_aberration;
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
      camera_effects = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
      RestartGraphicProfile();
    }
  }

  void InGameUpdate()
  {

    if(camera_effects == null) {
      camera_effects = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
      RestartGraphicProfile();
    }

    if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
      game_state = GameState.PauseMenu;
      Time.timeScale = 0.0f;
    }

    if(corruption_level >= corruption_limit) {
      game_state = GameState.EndMenu;
    }
 

    if(corruption_level > 1.0f) {
      // Glitches and some chromatic aberration
      ChromaticAberrationModel.Settings temp_aberration = camera_effects.chromaticAberration.settings;
      temp_aberration.intensity = 0.5f;
      camera_effects.chromaticAberration.settings = temp_aberration;
    }

    if(corruption_level > 3.0f) {
      // More glitches, dithering / grain
      GrainModel.Settings temp_grain = camera_effects.grain.settings;
      temp_grain.intensity = 0.3f;
      camera_effects.grain.settings = temp_grain;
    }

    if(corruption_level > 7.5f) {
      // Audio glitches, vignette, bloom
      VignetteModel.Settings temp_vignette = camera_effects.vignette.settings;
      temp_vignette.intensity = 0.3f;
      camera_effects.vignette.settings = temp_vignette;
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

  public void StartGame()
  {
    started_game = true;
  }

  public void ExitGame()
  {
    exit_game = true;
  }

  public void ContinueGame()
  {
    continued_game = true;
  }

  public void RestartGame()
  {
    restart_game = true;
  }

}
