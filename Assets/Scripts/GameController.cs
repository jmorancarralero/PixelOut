using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject combatMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSword;
    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject playerHammer;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject playerBattle;
    [SerializeField] private GameObject playerArmBattle;
    [SerializeField] private GameObject playerWeaponBattle;
    [SerializeField] private GameObject enemyBattle;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject playerOptions;
    [SerializeField] private GameObject scenePanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject knownPanel;
    [SerializeField] private GameObject known1Panel;
    [SerializeField] private GameObject known2Panel;
    [SerializeField] private GameObject known3Panel;
    [SerializeField] private GameObject unknownPanel;
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

    [SerializeField] public static Base.PlayerInfo game;

    [SerializeField] private TMP_Text Attack;
    [SerializeField] private TMP_Text Defend;
    [SerializeField] private TMP_Text BattleText;
    [SerializeField] private TMP_Text EnemyLife;
    [SerializeField] private TMP_Text PlayerLife;
    [SerializeField] private TMP_Text known1Text;
    [SerializeField] private TMP_Text known2Text;
    [SerializeField] private TMP_Text known3Text;
    [SerializeField] private TMP_Text unknownText;

    public static int state = 0;

    private static int battleState = 0;
    private static int enemyLife = 10;
    private static int sceneState = -1;

    private const int initialSceneState = 0;
    private const int normalState = 1;
    private const int pauseState = 2;
    private const int combatState = 3;
    private const int sceneModeState = 4;
    private const int startBattle = 0;
    private const int showInitialText = 1;
    private const int showText = 2;
    private const int playerTurn = 3;
    private const int playerAttack = 4;
    private const int enemyTurn = 5;
    private const int winState = 6;
    private const int defeatState = 7;

    public static string gameName;

    private string lastObject; 
    private string selectedWeapon; 

    private string[,] scene;

    private const string AttackText = "Attack";
    private const string DefendText = "Defend";
    private const string ContainerText = "CombatMenu";
    private const string SwordText = "sword";
    private const string GunText = "gun";
    private const string HammerText = "hammer";

    private float elapsedTime = 0f;

    private bool glowActive = false;
    private bool AttackActive = false;
    private bool DefendActive = false;
    private bool ShowE = false;
    private bool isInGame = false;
    private bool showSelection = false;
    private bool selectedOption = false;
    private bool finalCombatActive = false;


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

        loadData();

    }

    // Update is called once per frame
    private void Update(){

        switch(state){

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

    private void PauseGame(){

        if(Input.GetKeyDown(KeyCode.Escape)) {

            Time.timeScale = 1f;

            pauseMenu.SetActive(false);
            player.SetActive(true);

            state = normalState;

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

                    unknownText.text = scene[0,sceneState];
                    break;

                case "known_female":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(true);
                    known2Panel.SetActive(false);
                    known3Panel.SetActive(false);
                    unknownPanel.SetActive(false);

                    known1Text.text = scene[0,sceneState];                     
                    break;

                case "me":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(false);
                    known2Panel.SetActive(true);
                    known3Panel.SetActive(false);
                    unknownPanel.SetActive(false);

                    known2Text.text = scene[0,sceneState];
                    break;

                case "known_male":

                    knownPanel.SetActive(true);
                    known1Panel.SetActive(false);
                    known2Panel.SetActive(false);
                    known3Panel.SetActive(true);
                    unknownPanel.SetActive(false);

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

    private void NormalGame(){

        showSelectionMode();

        if(homeGrid.activeSelf){
            
            tv.SetActive(false);

        }

        if(Input.GetKeyDown(KeyCode.Escape)) {

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            player.SetActive(false);
            state = pauseState;

        }


        print(player.transform.position.x+":"+player.transform.position.y);

        if(homeGrid.activeSelf && isInGame){
            
            homeGrid.SetActive(false);
            labMainGrid.SetActive(true);

            scene = Base.labText();

            state = sceneModeState; 

        }


        if(homeGrid.activeSelf && player.transform.position.x > -0.6 && player.transform.position.x < -0.3 &&  player.transform.position.y < -5.2 && player.transform.position.y > -5.5){
            
            homeGrid.SetActive(false);
            cityGrid.SetActive(true);

        }

        if(cityGrid.activeSelf && player.transform.position.x > -0.58 && player.transform.position.x < -0.42 &&  player.transform.position.y < -4.7 && player.transform.position.y > -4.8){
            
            homeGrid.SetActive(true);
            cityGrid.SetActive(false);

        }

        if(labMainGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 1 && player.transform.position.y > 0.5){

            labOutsideGrid.SetActive(true);
            labMainGrid.SetActive(false);

            game.combatActive = true;

        }

        if(labOutsideGrid.activeSelf && player.transform.position.x > 7.4 && player.transform.position.x < 8.65 &&  player.transform.position.y < 3.2 && player.transform.position.y > 2.8){

            labOutsideGrid.SetActive(false);
            labMainGrid.SetActive(true);

            game.combatActive = false;

        }

        if(labOutsideGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 16.2 && player.transform.position.y > 15.8){

            labOutsideGrid.SetActive(false);
            labCenterGrid.SetActive(true);

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 15.6 && player.transform.position.y > 15.4){

            labOutsideGrid.SetActive(true);
            labCenterGrid.SetActive(false);

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > -2.6 && player.transform.position.x < -2.5 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            labLeftGrid.SetActive(true);
            labCenterGrid.SetActive(false);

        }

        if(labLeftGrid.activeSelf && player.transform.position.x > -2.5 && player.transform.position.x < -2.4 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            labLeftGrid.SetActive(false);
            labCenterGrid.SetActive(true);

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 18.5 && player.transform.position.x < 18.6 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            labRightGrid.SetActive(true);
            labCenterGrid.SetActive(false);

        }

        if(labRightGrid.activeSelf && player.transform.position.x > 18.3 && player.transform.position.x < 18.5 &&  player.transform.position.y < 27.5 && player.transform.position.y > 25){

            labRightGrid.SetActive(false);
            labCenterGrid.SetActive(true);

        }

        if(labCenterGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 37 && player.transform.position.y > 36.5){

            labTopGrid.SetActive(true);
            labCenterGrid.SetActive(false);

        }

        if(labTopGrid.activeSelf && player.transform.position.x > 6.4 && player.transform.position.x < 9.7 &&  player.transform.position.y < 36 && player.transform.position.y > 35.5){

            labTopGrid.SetActive(false);
            labCenterGrid.SetActive(true);

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


        if(!game.leftButtonActivated && player.transform.position.x > -10.3 && player.transform.position.x < -10.2 &&  player.transform.position.y < 31.3 && player.transform.position.y > 29.5){
            
            game.leftButtonActivated = !game.leftButtonActivated;

            leftButtonGrid.SetActive(true);

            scene = Base.buttonActiveText();

            state = sceneModeState; 

        }

        if(!game.righttButtonActivated && player.transform.position.x > 26 && player.transform.position.x < 26.2 &&  player.transform.position.y < 31.3 && player.transform.position.y > 29.5){
            
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

        if(((Topdown_mov.movX != 0 || Topdown_mov.movY != 0) && number < 0.8 && number >= 0.799 && game.combatActive) || finalCombatActive){

            combatMenu.SetActive(true);
            player.SetActive(false);
            playerHUD.SetActive(true);

            if(finalCombatActive){

                enemy1.SetActive(false);
                enemy2.SetActive(false);
                enemy3.SetActive(false);
                boss.SetActive(true);

                enemyLife = 50;

            }else{

                float aux = Random.value;
                print(aux);
                if(aux > 0 && aux <= 0.3){

                    enemy1.SetActive(true);
                    enemy2.SetActive(false);
                    enemy3.SetActive(false);
                    boss.SetActive(false); 

                    enemyLife = 10;                   

                }else if(aux > 0.3 && aux <= 0.6){

                    enemy1.SetActive(false);
                    enemy2.SetActive(true);
                    enemy3.SetActive(false);
                    boss.SetActive(false);     

                    enemyLife = 15;                                      

                }else{

                    enemy1.SetActive(false);
                    enemy2.SetActive(false);                    
                    enemy3.SetActive(true);
                    boss.SetActive(false);    

                    enemyLife = 5;                  

                }

            }

            state = combatState;
            
            elapsedTime = elapsedTime % 1f;
      
            // playerBattle.GetComponent<Animator>().enabled = false;
            // playerArmBattle.GetComponent<Animator>().enabled = false;
            // playerWeaponBattle.GetComponent<Animator>().enabled = false;
            // enemyBattle.GetComponent<Animator>().enabled = false;

        }

    }

    private void CombatGame(){
        
        EnemyLife.text = "HP: " + enemyLife;

        PlayerLife.text = "HP: " + game.actualLife;


        if(game.actualLife <= 0){

            battleState = defeatState;

        }    
        if(enemyLife <= 0){

            battleState = startBattle;

            combatMenu.SetActive(false);
            player.SetActive(true);
            playerHUD.SetActive(false);

            state = normalState;

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

            case winState:

                ShowCredits();
                break;

            case defeatState:

                ShowGameOver();
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

    private void ShowInitialText(){

        BattleText.text = "the virus "+ enemyBattle.name + " gets in your way";

        battleState = showText;

    }

    private void ShowText(){

        elapsedTime += Time.deltaTime / 2;

        if (elapsedTime >= 1f) {

            playerOptions.SetActive(true);

            BattleText.text = "What do you want to do?";

            elapsedTime = elapsedTime % 1f;

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
                    break;

                case DefendText:

                    AttackActive = false;
                    DefendActive = true;
                    break;

                default:

                    if (lastObject == ContainerText) {

                        AttackActive = false;
                        DefendActive = false;

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

        }else{

            Attack.color = new Color(1,1,1,1);
            Defend.color = new Color(1,1,1,1);

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

        SceneManager.LoadScene("MainMenu");

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

    }

    public void AttackSelected(){

        BattleText.text = "you attack with all your energy";

        enemyLife -= 5;

        battleState = enemyTurn; 

        playerOptions.SetActive(false);

    }

    public void DefendSelected(){

        BattleText.text = "you have blocked the attack";

        game.actualLife += 1;

        battleState = enemyTurn; 

        playerOptions.SetActive(false);

    }

    private void EnemyAttack(){

        elapsedTime += Time.deltaTime / 2;

        if (elapsedTime >= 1f) {

            BattleText.text = "the enemy attacks you mercilessly";

            elapsedTime = elapsedTime % 1f;

            game.actualLife -= 5;

            battleState = showText; 

        } 

    }

    private void ShowCredits(){

        SceneManager.LoadScene("Credits");

    }

    private void ShowGameOver(){

        state = normalState;
        battleState = startBattle;

        SceneManager.LoadScene("GameOver");

    }

    public static bool isInCombatMode(){

        return state == combatState;

    }

    public static bool isInPauseMode(){

        return state == pauseState;

    }
}

