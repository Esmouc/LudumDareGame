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

  public Texture2D cursor;
  public Texture2D reticule;

  private bool started_game;
  private bool exit_game;
  private bool continued_game;
  private bool restart_game;

  private bool changed_music;

  private PostProcessingProfile camera_effects;
  private ChromaticAberrationModel.Settings aberration_sett;
  private GrainModel.Settings grain_sett;
  private VignetteModel.Settings vignette_sett;
  private BloomModel.Settings bloom_set;

  private GameObject pause_canvas;
	private GameObject corruptionObject;


  private void OnMouseEnter()
  {
    Cursor.SetCursor(cursor,new Vector2(35,11),CursorMode.Auto);
  }


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

    changed_music = false;

    score = 0;
    corruption_level = 0.0f;
    corrupted_data = 0;

    camera_effects = null;
    pause_canvas = null;
    Cursor.SetCursor(cursor,new Vector2(35,11),CursorMode.Auto);
	}

  void Restart()
  {
    game_state = GameState.MainMenu;

    started_game = false;
    exit_game = false;
    continued_game = false;
    restart_game = false;

    changed_music = false;

    score = 0;
    corruption_level = 0.0f;
    corrupted_data = 0;

    camera_effects = null;
    pause_canvas = null;
    Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
  }

  void RestartGraphicProfile()
  {
    vignette_sett = camera_effects.vignette.settings;
    vignette_sett.intensity = 0.0f;
    camera_effects.vignette.settings = vignette_sett;
    
    grain_sett = camera_effects.grain.settings;
    grain_sett.intensity = 0.0f;
    camera_effects.grain.settings = grain_sett;
    
    aberration_sett = camera_effects.chromaticAberration.settings;
    aberration_sett.intensity = 0.0f;
    camera_effects.chromaticAberration.settings = aberration_sett;
    
    bloom_set = camera_effects.bloom.settings;
    bloom_set.bloom.intensity = 0.0f;
    camera_effects.bloom.settings = bloom_set;
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
      AudioManager.PlayMusic("BGM");
      SceneManager.LoadScene(1);
      Cursor.SetCursor(reticule, new Vector2(29,29),CursorMode.Auto);
    }
  }

  void InGameUpdate()
  {

    if(pause_canvas == null) {
      if((pause_canvas = GameObject.Find("Canvas")) != null) {
      pause_canvas.SetActive(false);
      }
    }

	if(corruptionObject == null) {
		corruptionObject = GameObject.Find ("Corruption");
	}

    if(camera_effects == null) {
      if((camera_effects = Camera.main.GetComponent<PostProcessingBehaviour>().profile) != null)
        RestartGraphicProfile();
    }

    if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
      game_state = GameState.PauseMenu;
      pause_canvas.SetActive(true);
      Time.timeScale = 0.0f;
    }

    if(corruption_level >= corruption_limit) {
      game_state = GameState.EndMenu;
      RestartGraphicProfile();
		  AudioManager.instance.StopMusic ("BGM");
		  AudioManager.instance.PlaySFX ("BlueScreen");
		  SceneManager.LoadScene(2);
    }
 
    // Glitches and some chromatic aberration
    aberration_sett.intensity = (corruption_level / corruption_limit) * 0.5f;
    camera_effects.chromaticAberration.settings = aberration_sett;

    // More glitches, dithering / grain
    grain_sett.intensity = (corruption_level / corruption_limit) * 0.5f;
    camera_effects.grain.settings = grain_sett;

    // Audio glitches, vignette, bloom
    vignette_sett.intensity = (corruption_level / corruption_limit) * 0.5f;
    camera_effects.vignette.settings = vignette_sett;
    bloom_set.bloom.intensity = (corruption_level / corruption_limit) * 1.25f;
    camera_effects.bloom.settings = bloom_set;

    if((corruption_level / corruption_limit) > 0.75f && changed_music == false) {
      AudioManager.instance.StopMusic("BGM");
      AudioManager.instance.PlayMusic("BGM_Alternate");
      changed_music = true;
    }
	
	if((corruption_level / corruption_limit) > 0.9f ) {
		corruptionObject.GetComponent<Animator> ().SetInteger ("CorruptionAnim", 2);
	}

	if((corruption_level / corruption_limit) > 0.95f ) {
		corruptionObject.GetComponent<Animator> ().SetInteger ("CorruptionAnim", 3);
	}

	if((corruption_level / corruption_limit) > 0.98f ) {
		corruptionObject.GetComponent<Animator> ().SetInteger ("CorruptionAnim", 1);
	}


    // Graphical glitches
    /*if((corruption_level / corruption_limit) > 0.5f) {
      // Starts at 5%, then scales up to 9%
      if(Random.Range(0,100) < 10 * (int)(corruption_level / corruption_limit)) {
        Destroy(Instantiate(,new Vector3(Random.Range(0,1440),Random.Range(0,1080),0.0f), Quaternion.identity), 2.0f);
      }
    }*/
  }

  void PauseMenuUpdate()
  {
    if(continued_game) {
      continued_game = false;
      game_state = GameState.InGame;
      Time.timeScale = 1.0f;
      pause_canvas.SetActive(false);
    }
  }

  void EndMenuUpdate()
  {
	if(Input.GetKeyDown(KeyCode.F8)) {
      Restart();
      game_state = GameState.InGame;
	    AudioManager.PlayMusic ("BGM");
      SceneManager.LoadScene(0);
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
