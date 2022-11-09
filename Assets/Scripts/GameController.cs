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

    [SerializeField] private TMP_Text Attack;
    [SerializeField] private TMP_Text Defend;

    public static int state = 0;
    private static int battleState = 0;
    private static int enemyLife = 10;
    private static int playerLife = 10;

    private const int normalState = 0;
    private const int pauseState = 1;
    private const int combatState = 2;
    private const int playerTurn = 0;
    private const int enemyTurn = 1;
    private const int winState = 4;
    private const int defeatState = 5;

    private string lastObject; 

    private const string AttackText = "Attack";
    private const string DefendText = "Defend";
    private const string ContainerText = "CombatMenu";

    private float elapsedTime = 0f;

    private bool glowActive = false;
    private bool AttackActive = false;
    private bool DefendActive = false;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    private void Start(){   

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

    }

    // Update is called once per frame
    private void Update(){

        switch(state){

            case normalState:

                NormalGame();
                break;

            case pauseState:

                PauseGame();
                break;

            case combatState:

                CombatGame();
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

    private void NormalGame(){

        if(Input.GetKeyDown(KeyCode.Escape)) {

            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            player.SetActive(false);
            state = pauseState;

        }

        float number = Random.value;

        if((Topdown_mov.movX != 0 || Topdown_mov.movY != 0) && number < 0.8 && number >= 0.795){

            combatMenu.SetActive(true);
            player.SetActive(false);
            state = combatState;

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

            Attack.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);

        }else if (DefendActive) {

            Defend.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);

        }else{

            Attack.color = new Color(255,255,255,255);
            Defend.color = new Color(255,255,255,255);

        }

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;

        } 

    }


    public void AttackSelected(){

        enemyLife -= 5;
        battleState = 1; 

    }

    public void DefendSelected(){

        playerLife += 1;
        battleState = 1; 

    }

    private void EnemyAttack(){

        playerLife -= 5;
        battleState = 0; 

    }

    private void ShowCredits(){

        SceneManager.LoadScene("Credits");

    }

    private void ShowGameOver(){

        SceneManager.LoadScene("GameOver");

    }

    private void CombatGame(){

        if(playerLife <= 0){

            battleState = 5;

        }    
        if(enemyLife <= 0){

            battleState = 4;

        }    

        switch(battleState){

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

    public static bool isInCombatMode(){

        return state == combatState;

    }

    public static bool isInPauseMode(){

        return state == pauseState;

    }
}

