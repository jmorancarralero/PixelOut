using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;


public class MainMenu : MonoBehaviour{

    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text TitleMainMenu;
    [SerializeField] private TMP_Text Play;
    [SerializeField] private TMP_Text Options;
    [SerializeField] private TMP_Text Exit;
    [SerializeField] private TMP_Text Return;
    [SerializeField] private TMP_Text Save;
    [SerializeField] private TMP_Text ReturnSelection;
    [SerializeField] private TMP_Text Selection1;
    [SerializeField] private TMP_Text Selection2;
    [SerializeField] private TMP_Text Selection3;

    [SerializeField] private Image bgSelection1;
    [SerializeField] private Image bgSelection2; 
    [SerializeField] private Image bgSelection3;
    [SerializeField] private Image frameSelection1;   
    [SerializeField] private Image frameSelection2;   
    [SerializeField] private Image frameSelection3;   

    [SerializeField] private GameObject IntroMenu;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject SelectionMenu;

    [SerializeField] private Base.PlayerInfo game1;
    [SerializeField] private Base.PlayerInfo game2;
    [SerializeField] private Base.PlayerInfo game3;

    [SerializeField] private Toggle fullScreenToggle;  

    [SerializeField] private Slider audioVolumeSlider; 

    [SerializeField] private TMP_Dropdown qualityDropdown; 

    [SerializeField] private AudioMixer audioMixer;

    private float elapsedTime = 0f;

    private bool glowActive = false;
    private bool PlayActive = false;
    private bool OptionsActive = false;
    private bool ExitActive = false;
    private bool ReturnActive = false;
    private bool SaveActive = false;
    private bool ReturnSelectionActive = false;
    private bool Selection1Active = false;
    private bool Selection2Active = false;
    private bool Selection3Active = false;

    private string lastObject; 

    public static int state = 0;

    private const int introState = 0;
    private const int menuState = 1;
    private const int selectionState = 2;
    private const int optionsState = 3;

    private const string PlayText = "Play";
    private const string OptionsText = "Options";
    private const string ExitText = "Exit";
    private const string ReturnText = "Return";
    private const string SaveText = "Save";
    private const string Selection1Text = "Selection1Text";
    private const string Selection2Text = "Selection2Text";
    private const string Selection3Text = "Selection3Text";
    private const string ReturnSelectionText = "ReturnSelection";
    private const string ContainerMain = "MainMenu";
    private const string ContainerOptions = "OptionsMenu";
    private const string ContainerSelection = "SelectionMenu";

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    private void Start(){

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        CheckOptions();
        CheckGames();

    }

    // Update is called once per frame
    private void Update(){

        switch(state){

            case introState:

                Intro();
                break;

            case menuState:

                Menu();
                break;

            case optionsState:
                OptionsFunction();
                break;

            case selectionState:
                Selection();
                break;
        }

    }

    private void Intro(){

        ChangeGlowByTime();
        GlowTitle();

        if (Input.GetMouseButtonDown(0)) {

            state = menuState;

            IntroMenu.SetActive(false);
            MainMenuPanel.SetActive(true);

        } 

    }

    private void Menu(){

        ChangeGlowByTime();
        GlowTitle();
        MarkText();

    }

    private void OptionsFunction(){

        ChangeGlowByTime();
        MarkText();

    }

    private void Selection(){

        ChangeGlowByTime();
        MarkText();
        OpenGame();

    }

    private void ChangeGlowByTime(){

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;

        } 

    }

    private void GlowTitle(){

        Title.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);
        TitleMainMenu.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);

    }

    private void MarkText(){
        
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results){

            switch (result.gameObject.name){

                case PlayText:

                    PlayActive = true;
                    OptionsActive = false;
                    ExitActive = false;
                    break;

                case OptionsText:

                    PlayActive = false;
                    OptionsActive = true;
                    ExitActive = false;
                    break;

                case ExitText:

                    PlayActive = false;
                    OptionsActive = false;
                    ExitActive = true;
                    break;

                case ReturnText:

                    SaveActive = false;
                    ReturnActive = true;
                    break;

                case SaveText:

                    SaveActive = true;
                    ReturnActive = false;
                    break;

                case ReturnSelectionText:

                    ReturnSelectionActive = true;
                    Selection1Active = false;
                    Selection2Active = false;
                    Selection3Active = false;

                    break;

                case Selection1Text:

                    ReturnSelectionActive = false;
                    Selection1Active = true;
                    Selection2Active = false;
                    Selection3Active = false;

                    break;

                case Selection2Text:

                    ReturnSelectionActive = false;
                    Selection1Active = false;
                    Selection2Active = true;
                    Selection3Active = false;

                    break;

                case Selection3Text:

                    ReturnSelectionActive = false;
                    Selection1Active = false;
                    Selection2Active = false;
                    Selection3Active = true;

                    break;

                default:

                    if (lastObject == ContainerMain) {

                        PlayActive = false;
                        OptionsActive = false;
                        ExitActive = false;

                    }

                    if (lastObject == ContainerOptions) {

                        SaveActive = false;
                        ReturnActive = false;

                    }

                    if (lastObject == ContainerSelection) {

                        ReturnSelectionActive = false;
                        Selection1Active = false;
                        Selection2Active = false;
                        Selection3Active = false;                       

                    }

                    break;
            }

            lastObject = result.gameObject.name;

            if (PlayActive) {

                Play.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (OptionsActive) {

                Options.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (ExitActive) {

                Exit.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (ReturnActive) {

                Return.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (SaveActive) {

                Save.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (ReturnSelectionActive) {

                ReturnSelection.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

            }else if (Selection1Active) {

                Selection1.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);
                bgSelection1.color = new Color(1,1,1,1);
                frameSelection1.color = new Color(1,1,1,1);

            }else if (Selection2Active) {

                Selection2.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);
                bgSelection2.color = new Color(1,1,1,1);
                frameSelection2.color = new Color(1,1,1,1);                

            }else if (Selection3Active) {

                Selection3.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);
                bgSelection3.color = new Color(1,1,1,1);
                frameSelection3.color = new Color(1,1,1,1);                

            }else{

                Play.color = new Color(1,1,1,1);
                Options.color = new Color(1,1,1,1);
                Exit.color = new Color(1,1,1,1);
                Return.color = new Color(1,1,1,1);
                Save.color = new Color(1,1,1,1);
                ReturnSelection.color = new Color(1,1,1,1);
                Selection1.color = new Color(1,1,1,1);
                Selection2.color = new Color(1,1,1,1);
                Selection3.color = new Color(1,1,1,1);
                bgSelection1.color = new Color(1,1,1,0.5f);
                frameSelection1.color = new Color(1,1,1,0.5f);
                bgSelection2.color = new Color(1,1,1,0.5f);
                frameSelection2.color = new Color(1,1,1,0.5f);  
                bgSelection3.color = new Color(1,1,1,0.5f);
                frameSelection3.color = new Color(1,1,1,0.5f); 

            }

        }

    }

    private void OpenGame(){


        if (Input.GetMouseButtonDown(0)) {

            if(Selection1Active || Selection2Active || Selection3Active){

                state = introState;

                MainMenuPanel.SetActive(true);
                SelectionMenu.SetActive(false);

                if(Selection1Active){

                    GameController.gameName = "game1";

                    GameController.game = game1;

                }else if(Selection2Active){

                    GameController.gameName = "game2";

                    GameController.game = game2;

                }else{

                    GameController.gameName = "game3";

                    GameController.game = game3;

                }

                SceneManager.LoadScene("Game");

            }

        } 

    }

    public void GoToSelectionMenu(){

        state = selectionState;

        CheckGames();

        MainMenuPanel.SetActive(false);
        SelectionMenu.SetActive(true);

    }

    public void OpenOptions(){

        state = optionsState;

        MainMenuPanel.SetActive(false);
        OptionsMenu.SetActive(true);

    }

    public void ExitGame(){

        Application.Quit();

    }

    private void CheckOptions(){

        Base.OptionsInfo optionsData = Base.DataSaver.loadData<Base.OptionsInfo>("options");

        if( optionsData == null ){

            optionsData = new Base.OptionsInfo();

        }

        Screen.fullScreen = optionsData.fullScreen;
        audioMixer.SetFloat("Volume",optionsData.audioVolume);
        QualitySettings.SetQualityLevel(optionsData.indexQuality);

        fullScreenToggle.isOn = optionsData.fullScreen;
        audioVolumeSlider.value = optionsData.audioVolume;
        qualityDropdown.value = optionsData.indexQuality;

    }

    private void CheckGames(){

        game1 = Base.DataSaver.loadData<Base.PlayerInfo>("game1");

        if( game1 != null ){

            Selection1.text = "LVL " + game1.level;

        }

        game2 = Base.DataSaver.loadData<Base.PlayerInfo>("game2");

        if( game2 != null ){

            Selection2.text = "LVL " + game2.level;

        }

        game3 = Base.DataSaver.loadData<Base.PlayerInfo>("game3");

        if( game3 != null ){

            Selection3.text = "LVL " + game3.level;

        }        

    }

    public void FullScreen(bool fullScreen){

        Screen.fullScreen = fullScreen;

    }

    public void ChangeVolume(float volume){

        audioMixer.SetFloat("Volume",volume);

    }

    public void ChangeQuality(int index){

        QualitySettings.SetQualityLevel(index);

    }

    public void SaveOptions(){

        Base.OptionsInfo optionsData = new Base.OptionsInfo();
        optionsData.fullScreen = fullScreenToggle.isOn;
        optionsData.audioVolume = audioVolumeSlider.value;
        optionsData.indexQuality = qualityDropdown.value;

        Base.DataSaver.saveData(optionsData, "options");

    }

    public void ReturnToMainMenu(){

        state = menuState;

        MainMenuPanel.SetActive(true);
        OptionsMenu.SetActive(false);
        SelectionMenu.SetActive(false);

    }

}
