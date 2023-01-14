using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using Cinemachine;

public class GameController : MonoBehaviour{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject StatsPanel;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject SavedPanel;
    [SerializeField] private GameObject TransitionPanel;
    [SerializeField] private GameObject TransitionToGamePanel;
    [SerializeField] private GameObject combatMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSword;
    [SerializeField] private GameObject playerSwordBody;
    [SerializeField] private GameObject playerSwordWeapon;
    [SerializeField] private GameObject playerSwordArm;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject playerGunBody;
    [SerializeField] private GameObject playerGunWeapon;
    [SerializeField] private GameObject playerGunArm;
    [SerializeField] private GameObject playerGunBullet;
    [SerializeField] private GameObject playerHammer;
    [SerializeField] private GameObject playerHammerBody;
    [SerializeField] private GameObject playerHammerWeapon;
    [SerializeField] private GameObject playerHammerArm;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject playerOptions;
    [SerializeField] private GameObject scenePanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject knownPanel;
    [SerializeField] private GameObject known1Panel;
    [SerializeField] private GameObject known2Panel;
    [SerializeField] private GameObject known3Panel;
    [SerializeField] private GameObject unknownPanel;
    [SerializeField] private GameObject unknown2Panel;
    [SerializeField] private GameObject unknown3Panel;
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private GameObject homeGrid;
    [SerializeField] private GameObject cityGrid;
    [SerializeField] private GameObject labMainGrid;
    [SerializeField] private GameObject labOutsideGrid;
    [SerializeField] private GameObject labCenterGrid;
    [SerializeField] private GameObject labLeftGrid;
    [SerializeField] private GameObject labRightGrid;
    [SerializeField] private GameObject labTopGrid;
    [SerializeField] private GameObject labMainDoorGrid;
    [SerializeField] private GameObject labCenterDoorGrid;
    [SerializeField] private GameObject leftButtonGrid;
    [SerializeField] private GameObject rightButtonGrid;
    [SerializeField] private GameObject tv;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject statsButton;

    [SerializeField] private PolygonCollider2D borderHome;
    [SerializeField] private PolygonCollider2D borderCity;
    [SerializeField] private PolygonCollider2D borderLab;
    [SerializeField] private PolygonCollider2D borderLabOutside;
    [SerializeField] private PolygonCollider2D borderLabCenter;
    [SerializeField] private PolygonCollider2D borderLabLeft;
    [SerializeField] private PolygonCollider2D borderLabRight;
    [SerializeField] private PolygonCollider2D borderLabTop;

    [SerializeField] private  CinemachineConfiner confiner;

    [SerializeField] private AudioSource AudioBackground;

    [SerializeField] public static Base.PlayerInfo game;

    [SerializeField] private static Base.EnemyInfo enemy;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Toggle fullScreenToggle;  

    [SerializeField] private Slider audioVolumeSlider; 
    [SerializeField] private Slider expSlider; 

    [SerializeField] private TMP_Dropdown qualityDropdown; 

    [SerializeField] private TMP_Text Attack;
    [SerializeField] private TMP_Text Defend;
    [SerializeField] private TMP_Text Run;
    [SerializeField] private TMP_Text BattleText;
    [SerializeField] private TMP_Text EnemyLife;
    [SerializeField] private TMP_Text PlayerLife;
    [SerializeField] private TMP_Text known1Text;
    [SerializeField] private TMP_Text known2Text;
    [SerializeField] private TMP_Text known3Text;
    [SerializeField] private TMP_Text unknownText;
    [SerializeField] private TMP_Text unknown2Text;
    [SerializeField] private TMP_Text unknown3Text;
    [SerializeField] private TMP_Text HPText;
    [SerializeField] private TMP_Text AttackLabelText;
    [SerializeField] private TMP_Text DefenseText;
    [SerializeField] private TMP_Text SpeedText;
    [SerializeField] private TMP_Text CritText;
    [SerializeField] private TMP_Text FailText;
    [SerializeField] private TMP_Text LVLText;
    [SerializeField] private TMP_Text UPText;
    [SerializeField] private TMP_Text ExpText;    
    [SerializeField] private TMP_Text TipText; 

    public static int state = 0;
    public static int battleState = 0;

    private static int sceneState = -1;

    private const int exitTransitionState = 0;
    private const int startTransitionState = 1;
    private const int initialSceneState = 2;
    private const int normalState = 3;
    private const int pauseState = 4;
    private const int combatState = 5;
    private const int sceneModeState = 6;
    private const int startBattle = 0;
    private const int showInitialText = 1;
    private const int showText = 2;
    private const int playerTurn = 3;
    private const int playerAttack = 4;
    private const int enemyTurn = 5;
    private const int runState = 6;
    private const int winState = 7;
    private const int expState = 8;
    private const int defeatState = 9;
    private const int finishGameState = 10;
    private const int lvlState = 11;

    public static string gameName;

    private string lastObject; 

    private string[,] scene;
    private string[] tipText;

    private const string AttackText = "Attack";
    private const string DefendText = "Defend";
    private const string RunText = "Run";
    private const string ContainerText = "CombatMenu";
    private const string SwordText = "sword";
    private const string GunText = "gun";
    private const string HammerText = "hammer";

    public static float elapsedTime = 0f;

    private bool glowActive = false;
    private bool AttackActive = false;
    private bool DefendActive = false;
    private bool RunActive = false;
    private bool ShowE = false;
    private bool isInGame = false;
    private bool showSelection = false;
    private bool selectedOption = false;
    private bool finalCombatActive = false;
    private bool isDefending = false;
    private bool finishCombat = false;
    private bool isSaving = false;
    private bool startingTransition = false;
    private bool finishingTransition = false;
    private bool startingTransitionToGame = false;
    private bool finishingTransitionToGame = false;
    private bool showingCredits = false;
    private bool showingGameOver = false;
    private bool showingMainMenu = false;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    private void Start(){   

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        if(game is null){

           game = new Base.PlayerInfo();

        }

        tipText = Base.TipText();

        loadData();

    }

    // Update is called once per frame
    private void Update(){

        switch(state){

            case exitTransitionState:

                ExitTransition();

                break;

            case startTransitionState:

                StartTransition();

                break;

            case initialSceneState:

                InitialScene();

                break;

            case normalState:

                NormalGame();

                break;

            case pauseState:

                PauseGame();

                break;

            case combatState:

                CombatGame();

                break;

            case sceneModeState:

                SceneMode();

                break;

        }
    }

    private void ExitTransition(){

        elapsedTime += 0.005f;

        TransitionPanel.GetComponent<CanvasGroup>().alpha = 1-elapsedTime;

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            TransitionPanel.GetComponent<CanvasGroup>().interactable = true;

            TransitionPanel.SetActive(false);

            state = initialSceneState;

        } 

    }

    private void StartTransition(){

        elapsedTime += 0.005f;

        TransitionPanel.GetComponent<CanvasGroup>().alpha = elapsedTime;

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            TransitionPanel.GetComponent<CanvasGroup>().interactable = false;

            state = initialSceneState;

        } 

    }


    private void PauseGame(){

        CheckOptions();

        HPText.text = "HP "+game.actualLife+"/"+game.life;
        AttackLabelText.text = "Attack "+game.attack;
        DefenseText.text = "Defense "+game.defense;
        SpeedText.text = "Speed "+game.speed;
        CritText.text = "Crit. Chance "+game.critProb;        
        FailText.text = "Fail PROB. "+game.failProb;
        LVLText.text = "LVL "+game.level;
        UPText.text = "upgrades "+game.upgrades;
        ExpText.text = "EXP "+game.exp+"/"+game.level*1000;
        expSlider.value = game.exp/game.level;
        TipText.text = tipText[game.tip];

        if(isSaving){

            elapsedTime += 0.005f;

            SavedPanel.SetActive(true);

            if (elapsedTime >= 1f) {

                elapsedTime = elapsedTime % 1f;

                SavedPanel.SetActive(false);

                isSaving = false;

            } 

        }

        if(game.upgrades > 0){

            UpgradePanel.SetActive(true);

        }else{

            UpgradePanel.SetActive(false);

        }

        if(Input.GetKeyDown(KeyCode.Escape) && PausePanel.activeSelf) {

            Time.timeScale = 1f;

            pauseMenu.SetActive(false);
            player.SetActive(true);

            state = normalState;

        }

        if(Input.GetKeyDown(KeyCode.Escape) && OptionsPanel.activeSelf) {

            OptionsPanel.SetActive(false);
            PausePanel.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.Escape) && StatsPanel.activeSelf) {

            StatsPanel.SetActive(false);
            PausePanel.SetActive(true);

        }

    }

    private void InitialScene(){

        if(game.initialSceneActive){

            scene = Base.initialText();

            tv.SetActive(true);

            state = sceneModeState;

            game.initialSceneActive = false; 

        }else{

            state = normalState;

        }

    }

    private void showSelectionMode(){

        if(showSelection){

            Topdown_mov.stopMoving = false;

            selectionPanel.SetActive(true);

        }else{

            Topdown_mov.stopMoving = true;

            selectionPanel.SetActive(false);

        }

    }

    private void SceneMode(){

        if(sceneState == -1){

            scenePanel.SetActive(true);

            sceneState = 0;

            Topdown_mov.stopMoving = false;

        }

        if(sceneState < scene.GetLength(1)){

            switch(scene[1,sceneState]){
            
                case "unknown":

                    knownPanel.SetActive(false);
                    unknownPanel.SetActive(true);
                    unknown2Panel.SetActive(false);
                    unknown3Panel.SetActive(false);

                    unknownText.text = scene[0,sceneState];
                    break;

                case "unknown2":

                    knownPanel.SetActive(false);
                    unknownPanel.SetActive(false);
                    unknown2Panel.SetActive(true);
                    unknown3Panel.SetActive(false);

                    unknown2Text.text = scene[0,sceneState];
                    break;

                case "unknown3":

                    knownPanel.SetActive(false);
                    unknownPanel.SetActive(false);
                    unknown2Panel.SetActive(false);
                    unknown3Panel.SetActive(true);

                    unknown3Text.text = scene[0,sceneState];
                    break;

                case "known_female":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(true);
                    known2Panel.SetActive(false);
                    known3Panel.SetActive(false);
                    unknownPanel.SetActive(false);
                    unknown2Panel.SetActive(false);
                    unknown3Panel.SetActive(false);

                    known1Text.text = scene[0,sceneState];                     
                    break;

                case "me":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(false);
                    known2Panel.SetActive(true);
                    known3Panel.SetActive(false);
                    unknownPanel.SetActive(false);
                    unknown2Panel.SetActive(false);
                    unknown3Panel.SetActive(false);

                    known2Text.text = scene[0,sceneState];
                    break;

                case "known_male":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(false);
                    known2Panel.SetActive(false);
                    known3Panel.SetActive(true);
                    unknownPanel.SetActive(false);
                    unknown2Panel.SetActive(false);
                    unknown3Panel.SetActive(false);

                    known3Text.text = scene[0,sceneState];                     
                    break;

            } 

        }
        
        if(Input.GetMouseButtonDown(0)) {

            sceneState += 1;

        }

        if(sceneState == scene.GetLength(1)){

            scenePanel.SetActive(false);

            state = normalState;

            Topdown_mov.stopMoving = true;

            sceneState = -1;

        }

    }

    private void BecomeTransition(){

        if(startingTransition){

            finishingTransition = true;

        }

        if(!startingTransition){

            TransitionPanel.SetActive(true);

            state = startTransitionState;

            startingTransition = true;
            Topdown_mov.stopMoving = false;

        }

    }

    private void EndTransition(){

        startingTransition = false;
        finishingTransition = false;
        Topdown_mov.stopMoving = true;

        state = exitTransitionState;

    }

    private void NormalGame(){

        showSelectionMode();

        if(showingCredits){

            ShowCredits();

        }

        if(showingGameOver){

            ShowGameOver();

        }

        if(showingMainMenu){

            GoToMenu();

        }

        if(homeGrid.activeSelf){
            
            tv.SetActive(false);

        }

        if(Input.GetKeyDown(KeyCode.Escape)) {

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            player.SetActive(false);
            state = pauseState;

        }


        //print(player.transform.position.x+":"+player.transform.position.y);

        if(finishingTransitionToGame){

            startingTransitionToGame = false;
            finishingTransitionToGame = false;

            scene = Base.labText();

            state = sceneModeState; 

        }

        if(TransitionToGamePanel.activeSelf){

            if(startingTransitionToGame){

                BecomeTransition();

                if(finishingTransition){

                    labMainGrid.SetActive(true);
                    statsButton.SetActive(true);
                    TransitionToGamePanel.SetActive(false);

                    confiner.m_BoundingShape2D = borderLab; 

                    EndTransition();

                    finishingTransitionToGame = true;

                }

            }

            if(!startingTransitionToGame){

                elapsedTime += 0.005f;

                if (elapsedTime >= 1f) {

                    elapsedTime = elapsedTime % 1f;

                    startingTransitionToGame = true;

                } 

            }

        }

        if(homeGrid.activeSelf && isInGame){
            
            BecomeTransition();

            if(finishingTransition){

                homeGrid.SetActive(false);
                TransitionToGamePanel.SetActive(true);

                AudioBackground.clip = Resources.Load("Audios/lab") as AudioClip;
                AudioBackground.Play();   

                game.tip = 4; 

                EndTransition();

            }
  
        }

        if(homeGrid.activeSelf && player.transform.position.x > -0.6 && player.transform.position.x < -0.3 &&  player.transform.position.y < -5.2 && player.transform.position.y > -5.5){
            
            BecomeTransition();

            if(finishingTransition){

                homeGrid.SetActive(false);
                cityGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderCity;     

                AudioBackground.clip = Resources.Load("Audios/city") as AudioClip;
                AudioBackground.Play();

                EndTransition();

            }
            
        }

        if(cityGrid.activeSelf && player.transform.position.x > -0.58 && player.transform.position.x < -0.42 &&  player.transform.position.y < -4.7 && player.transform.position.y > -4.8){
            
            BecomeTransition();

            if(finishingTransition){            
            
                homeGrid.SetActive(true);
                cityGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderHome;     

                AudioBackground.clip = Resources.Load("Audios/home") as AudioClip;
                AudioBackground.Play();   

                EndTransition();

            }
        }

        if(labMainGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 1 && player.transform.position.y > 0.5){

            BecomeTransition();

            if(finishingTransition){            
            
                labOutsideGrid.SetActive(true);
                labMainGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderLabOutside;        

                game.combatActive = true;

                EndTransition();

            }

        }

        if(labOutsideGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 3.2 && player.transform.position.y > 2.8){

            BecomeTransition();

            if(finishingTransition){            
            
                labOutsideGrid.SetActive(false);
                labMainGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderLab;        

                game.combatActive = false;

                EndTransition();

            }

        }

        if(labOutsideGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 16.2 && player.transform.position.y > 15.8){

            BecomeTransition();

            if(finishingTransition){            
            
                labOutsideGrid.SetActive(false);
                labCenterGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderLabCenter;

                EndTransition();

            }

            if(game.labCenterDoorActive){

                game.tip = 5;

            }        

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 15.6 && player.transform.position.y > 15.4){

            BecomeTransition();

            if(finishingTransition){            
            
                labOutsideGrid.SetActive(true);
                labCenterGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderLabOutside;   

                EndTransition();

            }
     
        }

        if(labCenterGrid.activeSelf && player.transform.position.x > -2.6 && player.transform.position.x < -2.5 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            BecomeTransition();

            if(finishingTransition){            
            
                labLeftGrid.SetActive(true);
                labCenterGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderLabLeft;  

                EndTransition();

            }

        }

        if(labLeftGrid.activeSelf && player.transform.position.x > -2.5 && player.transform.position.x < -2.4 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            BecomeTransition();

            if(finishingTransition){            
            
                labLeftGrid.SetActive(false);
                labCenterGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderLabCenter;     

                EndTransition();

            }

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 18.5 && player.transform.position.x < 18.6 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            BecomeTransition();

            if(finishingTransition){            
                
                labRightGrid.SetActive(true);
                labCenterGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderLabRight;    

                EndTransition();

            }

        }

        if(labRightGrid.activeSelf && player.transform.position.x > 18.3 && player.transform.position.x < 18.5 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            BecomeTransition();

            if(finishingTransition){                        

                labRightGrid.SetActive(false);
                labCenterGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderLabCenter;        

                EndTransition();

            }

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 37 && player.transform.position.y > 36.5){

            BecomeTransition();

            if(finishingTransition){                        

                labTopGrid.SetActive(true);
                labCenterGrid.SetActive(false);

                confiner.m_BoundingShape2D = borderLabTop;            

                EndTransition();

            }

        }

        if(labTopGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 36 && player.transform.position.y > 35.5){

            BecomeTransition();

            if(finishingTransition){                        

                labTopGrid.SetActive(false);
                labCenterGrid.SetActive(true);

                confiner.m_BoundingShape2D = borderLabCenter;         

                EndTransition();

            }

        }


        ShowE = false;

        if(homeGrid.activeSelf && player.transform.position.x > 2.7 && player.transform.position.x < 5.3 &&  player.transform.position.y < 7.2 && player.transform.position.y > 6.4){

            ShowE = true;

        }

        if(homeGrid.activeSelf && player.transform.position.x > 2.7 && player.transform.position.x < 5.3 &&  player.transform.position.y < 7.2 && player.transform.position.y > 6.4 && Input.GetKeyDown(KeyCode.E)){

            if(game.consoleTextActivated){
              
                scene = Base.consoleText2();

                isInGame = true;

            }else{

                scene = Base.consoleText();

            }

            state = sceneModeState; 

            ShowE = false;

        }

        if(homeGrid.activeSelf && player.transform.position.x > 5.2 && player.transform.position.x < 6.6 &&  player.transform.position.y < -0.7 && player.transform.position.y > -1.3){

            ShowE = true;

        }

        if(homeGrid.activeSelf && player.transform.position.x > 5.2 && player.transform.position.x < 6.6 &&  player.transform.position.y < -0.7 && player.transform.position.y > -1.3 && Input.GetKeyDown(KeyCode.E)){

            if(game.momTextActivated){
              
                scene = Base.momText2();

            }else{

                scene = Base.momText();

                game.tip = 1;

                game.momTextActivated = true;

            }


            state = sceneModeState; 

            ShowE = false;

        }

        if(cityGrid.activeSelf && player.transform.position.x > 22 && player.transform.position.x < 25.5 &&  player.transform.position.y < -4.8 && player.transform.position.y > -5.2){

            ShowE = true;

        }

        if(cityGrid.activeSelf && player.transform.position.x > 22 && player.transform.position.x < 25.5 &&  player.transform.position.y < -4.8 && player.transform.position.y > -5.2 && Input.GetKeyDown(KeyCode.E)){

            if(game.shopTextActivated){
              
                scene = Base.shopText3();

            }else if(game.momTextActivated){

                scene = Base.shopText2();

                game.tip = 2;

                game.shopTextActivated = true;
                game.unknownTextActivated = true;

            }else{

                scene = Base.shopText();

            }

            state = sceneModeState; 

            ShowE = false;

        }

        if(cityGrid.activeSelf && player.transform.position.x > 20.5 && player.transform.position.x < 21.5 &&  player.transform.position.y < 1 && player.transform.position.y > -0.3){

            ShowE = true;

        }

        if(cityGrid.activeSelf && player.transform.position.x > 20.5 && player.transform.position.x < 21.5 &&  player.transform.position.y < 1 && player.transform.position.y > -0.3 && Input.GetKeyDown(KeyCode.E)){

            if(game.unknownText2Activated){
              
                scene = Base.unknownText3();

            }else if(game.unknownTextActivated){

                scene = Base.unknownText2();

                game.tip = 3;

                game.consoleTextActivated = true;
                game.unknownText2Activated = true;

            }else{

                scene = Base.unknownText();

            }

            state = sceneModeState; 

            ShowE = false;
        }

        if(labMainGrid.activeSelf && player.transform.position.x > 0.4 && player.transform.position.x < 0.8 &&  player.transform.position.y < 8.3 && player.transform.position.y > 7.9){

            if(selectedOption){

                sword.SetActive(false);
                labMainDoorGrid.SetActive(false);
                playerSword.SetActive(true);

                game.selectedWeapon = "sword";

                game.attack = 10;
                game.defense = 5;
                game.speed = 5;
                game.critProb = 50;
                game.failProb = 15;
                game.life = 15;
                game.actualLife = 15;

                selectedOption = false;

                state = sceneModeState;

                scene = Base.labDoor2Text();

            }else if(!showSelection){

                ShowE = true;

            }
        }

        if(labMainGrid.activeSelf && player.transform.position.x > 0.4 && player.transform.position.x < 0.8 &&  player.transform.position.y < 8.3 && player.transform.position.y > 7.9 && Input.GetKeyDown(KeyCode.E)){

            if(string.IsNullOrEmpty(game.selectedWeapon)){
            
                scene = Base.swordText();

            }else if(game.selectedWeapon == SwordText){

                scene = Base.selectedText();

            }else{

                scene = Base.notSelectedText();

            }
            
            state = sceneModeState; 

            ShowE = false;

            if(string.IsNullOrEmpty(game.selectedWeapon)){

                showSelection = true;

            }

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 1.5 && player.transform.position.x < 2.2 &&  player.transform.position.y < 8.6 && player.transform.position.y > 7.9){

            if(selectedOption){

                gun.SetActive(false);
                labMainDoorGrid.SetActive(false);
                playerGun.SetActive(true);

                game.selectedWeapon = "gun";

                game.attack = 5;
                game.defense = 10;
                game.speed = 10;
                game.critProb = 50;
                game.failProb = 10;
                game.life = 10;
                game.actualLife = 10;

                selectedOption = false;

                state = sceneModeState;

                scene = Base.labDoor2Text();

            }else if(!showSelection){

                ShowE = true;

            }

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 1.5 && player.transform.position.x < 2.2 &&  player.transform.position.y < 8.6 && player.transform.position.y > 7.9 && Input.GetKeyDown(KeyCode.E)){

            if(string.IsNullOrEmpty(game.selectedWeapon)){
            
                scene = Base.gunText();

            }else if(game.selectedWeapon == GunText){

                scene = Base.selectedText();

            }else{

                scene = Base.notSelectedText();

            }
            
            state = sceneModeState; 

            ShowE = false;

            if(string.IsNullOrEmpty(game.selectedWeapon)){

                showSelection = true;

            }

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 3 && player.transform.position.x < 3.8 &&  player.transform.position.y < 8.3 && player.transform.position.y > 7.9){

            if(selectedOption){

                hammer.SetActive(false);
                labMainDoorGrid.SetActive(false);
                playerHammer.SetActive(true);

                game.selectedWeapon = "hammer";

                game.attack = 20;
                game.defense = 5;
                game.speed = 2;
                game.critProb = 50;
                game.failProb = 20;
                game.life = 20;
                game.actualLife = 20;

                selectedOption = false;

                state = sceneModeState;

                scene = Base.labDoor2Text();


            }else if(!showSelection){

                ShowE = true;

            }

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 3 && player.transform.position.x < 3.8 &&  player.transform.position.y < 8.3 && player.transform.position.y > 7.9 && Input.GetKeyDown(KeyCode.E)){

            if(string.IsNullOrEmpty(game.selectedWeapon)){
            
                scene = Base.hammerText();

            }else if(game.selectedWeapon == HammerText){

                scene = Base.selectedText();

            }else{

                scene = Base.notSelectedText();

            }
            
            state = sceneModeState; 

            ShowE = false;

            if(string.IsNullOrEmpty(game.selectedWeapon)){

                showSelection = true;

            }


        }

        if(labMainGrid.activeSelf && player.transform.position.x > 6 && player.transform.position.x < 10 &&  player.transform.position.y < 9.3 && player.transform.position.y > 8.8){

            ShowE = true;

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 6 && player.transform.position.x < 10 &&  player.transform.position.y < 9.3 && player.transform.position.y > 8.8 && Input.GetKeyDown(KeyCode.E)){

            if(game.pclabTextActivated){
              
                scene = Base.pcText2();

            }else{

                scene = Base.pcText();

                game.pclabTextActivated = true;

            }

            game.actualLife = game.life;

            state = sceneModeState; 

            ShowE = false;

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 2.8 && player.transform.position.y > 2.5 && labMainDoorGrid.activeSelf){

            ShowE = true;

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 2.8 && player.transform.position.y > 2.5 && labMainDoorGrid.activeSelf && Input.GetKeyDown(KeyCode.E)){

            scene = Base.labDoorText();

            state = sceneModeState; 

            ShowE = false;

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6 && player.transform.position.x < 10 &&  player.transform.position.y < 35.3 && player.transform.position.y > 35 && labCenterDoorGrid.activeSelf){

            ShowE = true;

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6 && player.transform.position.x < 10 &&  player.transform.position.y < 35.3 && player.transform.position.y > 35 && labCenterDoorGrid.activeSelf && Input.GetKeyDown(KeyCode.E)){

            scene = Base.labDoorText();

            state = sceneModeState; 

            game.tip = 6;

            ShowE = false;

        }

        if(labTopGrid.activeSelf && player.transform.position.x > 7.5 && player.transform.position.x < 8.5 &&  player.transform.position.y < 42 && player.transform.position.y > 40){

            ShowE = true;

        }

        if(labTopGrid.activeSelf && player.transform.position.x > 7.5 && player.transform.position.x < 8.5 &&  player.transform.position.y < 42 && player.transform.position.y > 40 && Input.GetKeyDown(KeyCode.E)){

            scene = Base.finalText();

            state = sceneModeState; 

            ShowE = false;

            finalCombatActive = true;

        }


        if(!game.leftButtonActivated && player.transform.position.x > -22.4 && player.transform.position.x < -22.1 &&  player.transform.position.y < 31.3 && player.transform.position.y > 29.5){
            
            game.leftButtonActivated = !game.leftButtonActivated;

            leftButtonGrid.SetActive(true);

            scene = Base.buttonActiveText();

            state = sceneModeState; 

        }

        if(!game.righttButtonActivated && player.transform.position.x > 38.1 && player.transform.position.x < 38.4 &&  player.transform.position.y < 31.3 && player.transform.position.y > 29.5){
            
            game.righttButtonActivated = !game.righttButtonActivated;

            rightButtonGrid.SetActive(true);

            scene = Base.buttonActiveText();

            state = sceneModeState; 

        }

        if(game.leftButtonActivated && game.righttButtonActivated && labCenterDoorGrid.activeSelf){

            labCenterDoorGrid.SetActive(false);

            scene = Base.labDoor2Text();

            state = sceneModeState; 

        }



        if(ShowE){

            inGamePanel.SetActive(true);

        }else{

            inGamePanel.SetActive(false);

        }


        float number = Random.value;

        if(((Topdown_mov.movX != 0 || Topdown_mov.movY != 0) && number < 0.8 && number >= 0.79995 && game.combatActive) || finalCombatActive){

            combatMenu.SetActive(true);
            player.SetActive(false);
            playerHUD.SetActive(true);

            if(finalCombatActive){

                enemy1.SetActive(false);
                enemy2.SetActive(false);
                enemy3.SetActive(false);
                boss.SetActive(true);

                enemy = Base.ObjectBoss();

            }else{

                float aux = Random.value;
                if(aux > 0 && aux <= 0.5){

                    enemy1.SetActive(true);
                    enemy2.SetActive(false);
                    enemy3.SetActive(false);
                    boss.SetActive(false); 

                    enemy = Base.ObjectEnemy1();               

                }else if(aux > 0.5 && aux <= 0.8){

                    enemy1.SetActive(false);
                    enemy2.SetActive(true);
                    enemy3.SetActive(false);
                    boss.SetActive(false);

                    enemy = Base.ObjectEnemy2();                               

                }else{

                    enemy1.SetActive(false);
                    enemy2.SetActive(false);                    
                    enemy3.SetActive(true);
                    boss.SetActive(false);    

                    enemy = Base.ObjectEnemy3();             

                }

            }

            state = combatState;
            
            elapsedTime = elapsedTime % 1f;

            AudioBackground.clip = Resources.Load("Audios/combat") as AudioClip;
            AudioBackground.Play();
      
            switch(game.selectedWeapon){

                case "hammer":

                    playerHammerArm.GetComponent<Animator>().enabled = false;
                    playerHammerArm.GetComponent<Animator>().Rebind();
                    playerHammerArm.GetComponent<Animator>().Update(0f);
                    playerHammerBody.GetComponent<Animator>().enabled = false;
                    playerHammerBody.GetComponent<Animator>().Rebind();
                    playerHammerBody.GetComponent<Animator>().Update(0f);
                    playerHammerWeapon.GetComponent<Animator>().enabled = false;
                    playerHammerWeapon.GetComponent<Animator>().Rebind();
                    playerHammerWeapon.GetComponent<Animator>().Update(0f);


                    break;
                
                case "sword":

                    playerSwordArm.GetComponent<Animator>().enabled = false;
                    playerSwordArm.GetComponent<Animator>().Rebind();
                    playerSwordArm.GetComponent<Animator>().Update(0f);
                    playerSwordBody.GetComponent<Animator>().enabled = false;
                    playerSwordBody.GetComponent<Animator>().Rebind();
                    playerSwordBody.GetComponent<Animator>().Update(0f);                    
                    playerSwordWeapon.GetComponent<Animator>().enabled = false; 
                    playerSwordWeapon.GetComponent<Animator>().Rebind();
                    playerSwordWeapon.GetComponent<Animator>().Update(0f);                       

                    break;

                case "gun":

                    playerGunBullet.SetActive(false);
                    playerGunBullet.GetComponent<Animator>().Rebind();
                    playerGunBullet.GetComponent<Animator>().Update(0f);    

                    break;

            }

        }

    }

    private void CombatGame(){
        
        if(enemy.actualLife >= 0){

            EnemyLife.text = "HP: " + enemy.actualLife;

        }else{

            EnemyLife.text = "HP: " + 0;

        }

        if(game.actualLife >= 0){

            PlayerLife.text = "HP: " + game.actualLife;

        }else{

            PlayerLife.text = "HP: " + 0;

        }


        if(game.actualLife <= 0){

            battleState = defeatState;

        }  

        if(enemy.actualLife <= 0 && battleState == enemyTurn ){

            game.exp += enemy.exp;

            battleState = winState;

            if(finalCombatActive){

                battleState = finishGameState;                

            }

        }    

        switch(battleState){

            case startBattle:

                StartBattle();

                break;

            case showInitialText:
            
                ShowInitialText();

                break;

            case showText:
            
                ShowText();

                break;

            case playerTurn:

                WaitingAttack();

                break;

            case enemyTurn:

                EnemyAttack();

                break;

            case finishGameState:

                ShowCredits();

                break;

            case defeatState:

                ShowGameOver();

                break;

            case winState:

                ShowResults();

                break;

            case expState:

                ShowExp();

                break;

            case lvlState:

                ShowLvl();

                break;   

            case runState:

                RunTurn();

                break;           

        }

    }

    private void StartBattle(){

        elapsedTime += Time.deltaTime;

        playerHUD.GetComponent<Image>().color = new Color(1,1,1, elapsedTime);
        playerHUD.transform.Find("Panel").gameObject.GetComponent<Image>().color = new Color(0,0,0, elapsedTime);


        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            battleState = showInitialText;

        } 

    }

    private void ShowLvl(){

        elapsedTime += Time.deltaTime / 2;

        BattleText.text = "You leveled up";

        if (elapsedTime >= 1f) {

            game.upgrades += 1;
            game.exp -= 1000*game.level;
            game.level += 1;
            elapsedTime = elapsedTime % 1f;

            returnGame();

        } 

    }    

    private void ShowResults(){

        elapsedTime += Time.deltaTime / 2;

        BattleText.text = "The virus "+ enemy.name + " was defeated";

        switch(enemy.name){

            case "Herbaceum":

                enemy1.SetActive(false);

                break;

            case "Poisonus":

                enemy2.SetActive(false);

                break;

            case "Fangus":

                enemy3.SetActive(false);

                break;

            case "Final Boss":

                boss.SetActive(false);

                break;

        }   

        if (elapsedTime >= 1f) {
       
            battleState = expState;

            switch(game.selectedWeapon){

                case "hammer":

                    playerHammerArm.GetComponent<Animator>().enabled = false;
                    playerHammerArm.GetComponent<Animator>().Rebind();
                    playerHammerArm.GetComponent<Animator>().Update(0f);
                    playerHammerBody.GetComponent<Animator>().enabled = false;
                    playerHammerBody.GetComponent<Animator>().Rebind();
                    playerHammerBody.GetComponent<Animator>().Update(0f);
                    playerHammerWeapon.GetComponent<Animator>().enabled = false;
                    playerHammerWeapon.GetComponent<Animator>().Rebind();
                    playerHammerWeapon.GetComponent<Animator>().Update(0f);

                    break;
                
                case "sword":

                    playerSwordArm.GetComponent<Animator>().enabled = false;
                    playerSwordArm.GetComponent<Animator>().Rebind();
                    playerSwordArm.GetComponent<Animator>().Update(0f);
                    playerSwordBody.GetComponent<Animator>().enabled = false;
                    playerSwordBody.GetComponent<Animator>().Rebind();
                    playerSwordBody.GetComponent<Animator>().Update(0f);                    
                    playerSwordWeapon.GetComponent<Animator>().enabled = false; 
                    playerSwordWeapon.GetComponent<Animator>().Rebind();
                    playerSwordWeapon.GetComponent<Animator>().Update(0f);                       

                    break;

                case "gun":

                    playerGunBullet.SetActive(false);
                    playerGunBullet.GetComponent<Animator>().Rebind();
                    playerGunBullet.GetComponent<Animator>().Update(0f);    

                    break;

            }

            elapsedTime = elapsedTime % 1f;

        } 

    }

    private void returnGame(){

        battleState = startBattle;

        combatMenu.SetActive(false);
        player.SetActive(true);
        playerHUD.SetActive(false);

        state = normalState;

        BattleText.text = "";

        AudioBackground.clip = Resources.Load("Audios/lab") as AudioClip;
        AudioBackground.Play();

    }

    private void ShowExp(){

        elapsedTime += Time.deltaTime / 2;

        BattleText.text = "You won "+ enemy.exp + " EXP points";

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            if(game.exp >= 1000*game.level){

                battleState = lvlState;

            }else{

                returnGame(); 

            }

        }       

    }

    private void ShowInitialText(){

        BattleText.text = "the virus "+ enemy.name + " gets in your way";


        if(game.speed > enemy.speed){

            battleState = showText;

        }else{

            battleState = enemyTurn;

        }

    }

    private void ShowText(){

        elapsedTime += Time.deltaTime / 2;

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            playerOptions.SetActive(true);

            BattleText.text = "What do you want to do?";

            battleState = playerTurn;

        } 

    }

    private void WaitingAttack(){

        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (RaycastResult result in results){

            switch (result.gameObject.name){

                case AttackText:

                    AttackActive = true;
                    DefendActive = false;
                    RunActive = false;

                    break;

                case DefendText:

                    AttackActive = false;
                    DefendActive = true;
                    RunActive = false;

                    break;

                case RunText:

                    AttackActive = false;
                    DefendActive = false;
                    RunActive = true;

                    break;

                default:

                    if (lastObject == ContainerText) {

                        AttackActive = false;
                        DefendActive = false;
                        RunActive = false;

                    }

                    break;
                    
            }

            lastObject = result.gameObject.name;

        }

        elapsedTime += Time.deltaTime;

        if (AttackActive) {

            Attack.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

        }else if (DefendActive) {

            Defend.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

        }else if (RunActive) {

            Run.color = new Color(1,1,1, glowActive?elapsedTime:1-elapsedTime);

        }else{

            Attack.color = new Color(1,1,1,1);
            Defend.color = new Color(1,1,1,1);
            Run.color = new Color(1,1,1,1);

        }

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;

        } 

    }

    public void SelectedOptionYes(){

        selectedOption = true;

        showSelection = false;

    }

    public void SelectedOptionNo(){

        selectedOption = false;

        showSelection = false;

    }

    private void CheckOptions(){

        fullScreenToggle.isOn = Screen.fullScreen;
        float volume = 0f;
        audioMixer.GetFloat("Volume",out volume);

        if(volume == 0f){

            volume = -1f;

        }
        audioVolumeSlider.value = volume;
        qualityDropdown.value = QualitySettings.GetQualityLevel();

    }

    private void loadData(){

        player.transform.position = new Vector2(game.posX, game.posY);

        homeGrid.SetActive(game.homeActive);
        cityGrid.SetActive(game.cityActive);
        labMainGrid.SetActive(game.labMainActive);
        labOutsideGrid.SetActive(game.labOutsideActive);
        labCenterGrid.SetActive(game.labCenterActive);
        labLeftGrid.SetActive(game.labLeftActive);
        labRightGrid.SetActive(game.labRightActive);
        labTopGrid.SetActive(game.labTopActive);
        labMainDoorGrid.SetActive(game.labMainDoorActive);    
        labCenterDoorGrid.SetActive(game.labCenterDoorActive);

        switch (true){

            case true when game.labMainActive: case true when game.labOutsideActive: case true when game.labCenterActive: case true when game.labLeftActive: case true when game.labRightActive: case true when game.labTopActive:

                AudioBackground.clip = Resources.Load("Audios/lab") as AudioClip;
                AudioBackground.Play();

                statsButton.SetActive(true);

                break;

        }

        switch(true){

            case true when game.homeActive:

                statsButton.SetActive(false);

                break;

            case true when game.cityActive:

                AudioBackground.clip = Resources.Load("Audios/city") as AudioClip;
                AudioBackground.Play();

                confiner.m_BoundingShape2D = borderCity;  

                break;

            case true when game.labMainActive:

                confiner.m_BoundingShape2D = borderLab;  
 
                break;

            case true when game.labOutsideActive:

                confiner.m_BoundingShape2D = borderLabOutside;  
 
                break;             
            
            case true when game.labCenterActive:

                confiner.m_BoundingShape2D = borderLabCenter;  
 
                break;             
            
            case true when game.labLeftActive:

                confiner.m_BoundingShape2D = borderLabLeft;  
 
                break;             
            
            case true when game.labRightActive:

                confiner.m_BoundingShape2D = borderLabRight;  
 
                break; 
            
            case true when game.labTopActive:

                confiner.m_BoundingShape2D = borderLabTop;  
 
                break;

        }


        if(!string.IsNullOrEmpty(game.selectedWeapon)){

            if(game.selectedWeapon == HammerText){

                hammer.SetActive(false);
                playerHammer.SetActive(true);

            }else if(game.selectedWeapon == SwordText){

                sword.SetActive(false);
                playerSword.SetActive(true);

            }else{

                gun.SetActive(false);
                playerGun.SetActive(true);

            }

        }


    }

    public void GoToMenu(){

        if(!showingMainMenu){

            showingMainMenu = true;

            BecomeTransition();

        }else{

            showingMainMenu = false;

            game = new Base.PlayerInfo();

            MainMenu.state = 0;
            
            Time.timeScale = 1f;

            SceneManager.LoadScene("MainMenu");

        }

    }

    public void OpenOptions(){

        PausePanel.SetActive(false);
        OptionsPanel.SetActive(true);

    }

    public void OpenStats(){

        PausePanel.SetActive(false);
        StatsPanel.SetActive(true);

    }

    public void UpgradeHP(){

        game.life += 10;
        game.upgrades -= 1;

    }

    public void UpgradeAttack(){

        game.attack += 10;
        game.upgrades -= 1;

    }

    public void UpgradeDefense(){

        game.defense += 10;
        game.upgrades -= 1;

    }

    public void UpgradeSpeed(){

        game.speed += 5;
        game.upgrades -= 1;

    } 

    public void UpgradeCrit(){

        game.critProb += 5;
        game.upgrades -= 1;

    } 

    public void UpgradeFail(){

        if(game.failProb > 0){
            
            game.failProb -= 5;
            game.upgrades -= 1;

        }

    }        

    public void FullScreen(bool fullScreen){

        Base.FullScreen(fullScreen);

    }

    public void ChangeVolume(float volume){

        Base.ChangeVolume(volume, audioMixer);

    }

    public void ChangeQuality(int index){

        Base.ChangeQuality(index);

    }

    public void SaveOptions(){

        Base.SaveOptions(fullScreenToggle.isOn, audioVolumeSlider.value, qualityDropdown.value);

        isSaving = true;

    }

    public void SaveGame(){

        game.posX = player.transform.position.x;
        game.posY = player.transform.position.y;

        game.homeActive = homeGrid.activeSelf;
        game.cityActive = cityGrid.activeSelf;
        game.labMainActive = labMainGrid.activeSelf;
        game.labOutsideActive = labOutsideGrid.activeSelf;
        game.labCenterActive = labCenterGrid.activeSelf;
        game.labLeftActive = labLeftGrid.activeSelf;
        game.labRightActive = labRightGrid.activeSelf;
        game.labTopActive = labTopGrid.activeSelf;
        game.labMainDoorActive = labMainDoorGrid.activeSelf;
        game.labCenterDoorActive = labCenterDoorGrid.activeSelf;

        Base.DataSaver.saveData(game, gameName);

        isSaving = true;

    }

    public void AttackSelected(){

        float number = Random.value; 

        if(number > game.failProb/100){

            number = Random.value;

            int finalAttack = game.critProb > number ? game.attack * 2 : game.attack;

            int finalDefense = isDefending ? enemy.defense * 2 : enemy.defense;

            int finalDamage = finalAttack - finalDefense;

            if(finalDamage > 0){

                enemy.actualLife -= finalDamage;

            }

            BattleText.text = "you attack with all your energy";

            switch(game.selectedWeapon){

                case "hammer":

                    playerHammerArm.GetComponent<Animator>().enabled = true;
                    playerHammerBody.GetComponent<Animator>().enabled = true;
                    playerHammerWeapon.GetComponent<Animator>().enabled = true;

                    break;
                
                case "sword":

                    playerSwordArm.GetComponent<Animator>().enabled = true;
                    playerSwordBody.GetComponent<Animator>().enabled = true;
                    playerSwordWeapon.GetComponent<Animator>().enabled = true;    

                    break;

                case "gun":

                    playerGunBullet.SetActive(true);

                    break;

            }


        }else{

            BattleText.text = "you have missed your attack";

        }   

        battleState = enemyTurn; 

        playerOptions.SetActive(false);


    }

    public void DefendSelected(){

        BattleText.text = "you have blocked the attack";

        isDefending = true;

        battleState = enemyTurn; 

        playerOptions.SetActive(false);

    }

    private void RunTurn(){

        elapsedTime += Time.deltaTime / 2;  

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            if(finishCombat){

                finishCombat = false;

                returnGame();

            }else{

                battleState = enemyTurn; 

            }

        } 

    }

    public void RunSelected(){

        battleState = runState;

        finishCombat = game.speed > enemy.speed;

        playerOptions.SetActive(false);

        if(!finishCombat){

            float number = Random.value;

            finishCombat = number > 0.5;

        }

        if(finishCombat){

            BattleText.text = "fortunately you fled";

        }else{

            BattleText.text = "you have failed to try to escape";

        }

    }

    private void EnemyAttack(){

        elapsedTime += Time.deltaTime / 2;

        if (elapsedTime >= 1f) {

            switch(game.selectedWeapon){

                case "hammer":

                    playerHammerArm.GetComponent<Animator>().enabled = false;
                    playerHammerArm.GetComponent<Animator>().Rebind();
                    playerHammerArm.GetComponent<Animator>().Update(0f);
                    playerHammerBody.GetComponent<Animator>().enabled = false;
                    playerHammerBody.GetComponent<Animator>().Rebind();
                    playerHammerBody.GetComponent<Animator>().Update(0f);
                    playerHammerWeapon.GetComponent<Animator>().enabled = false;
                    playerHammerWeapon.GetComponent<Animator>().Rebind();
                    playerHammerWeapon.GetComponent<Animator>().Update(0f);

                    break;
                
                case "sword":

                    playerSwordArm.GetComponent<Animator>().enabled = false;
                    playerSwordArm.GetComponent<Animator>().Rebind();
                    playerSwordArm.GetComponent<Animator>().Update(0f);
                    playerSwordBody.GetComponent<Animator>().enabled = false;
                    playerSwordBody.GetComponent<Animator>().Rebind();
                    playerSwordBody.GetComponent<Animator>().Update(0f);                    
                    playerSwordWeapon.GetComponent<Animator>().enabled = false; 
                    playerSwordWeapon.GetComponent<Animator>().Rebind();
                    playerSwordWeapon.GetComponent<Animator>().Update(0f);                       

                    break;

                case "gun":

                    playerGunBullet.SetActive(false);
                    playerGunBullet.GetComponent<Animator>().Rebind();
                    playerGunBullet.GetComponent<Animator>().Update(0f);    

                    break;

            }

            float number = Random.value; 

            if(number > enemy.failProb/100){

                number = Random.value;

                int finalAttack = enemy.critProb > number ? enemy.attack * 2 : enemy.attack;

                int finalDefense = isDefending ? game.defense * 2 : game.defense;

                int finalDamage = finalAttack - finalDefense;

                if(finalDamage > 0){

                    game.actualLife -= finalDamage;

                }



                isDefending = false;

                BattleText.text = "the enemy attacks you mercilessly";

                switch(enemy.name){

                    case "Herbaceum":

                        enemy1.GetComponent<Animator>().SetBool("attack",true);

                        break;

                    case "Poisonus":

                        enemy2.GetComponent<Animator>().SetBool("attack",true);

                        break;

                    case "Fangus":

                        enemy3.GetComponent<Animator>().SetBool("attack",true);

                        break;

                    case "Final Boss":

                        boss.GetComponent<Animator>().SetBool("attack",true);

                        break;

                }   

            }else{

                BattleText.text = "the enemy missed the attack";

            }        

            elapsedTime = elapsedTime % 1f;

            battleState = showText; 

        } 

    }

    private void ShowCredits(){

        if(!showingCredits){

            BecomeTransition();

            showingCredits = true;

        }else{

            showingCredits = false;

            game = new Base.PlayerInfo();

            SceneManager.LoadScene("Credits");

        }

    }

    private void ShowGameOver(){

        if(!showingGameOver){

            BecomeTransition();

            showingGameOver = true;

        }else{

            showingCredits = false;

            game = new Base.PlayerInfo();
 
            SceneManager.LoadScene("GameOver");

        }

    }

    public static bool isInCombatMode(){

        return state == combatState;

    }

    public static bool isInPauseMode(){

        return state == pauseState;

    }
}

