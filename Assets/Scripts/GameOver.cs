using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour{

    [SerializeField] private TMP_Text Title;

    [SerializeField] private GameObject TransitionPanel;

    private float elapsedTime = 0f;

    private bool glowActive = false;

    public static int state = 0;

    private const int transitionState = 0;
    private const int introState = 1;
    private const int startTransitionState = 2;


    // Start is called before the first frame update
    public void Start(){
        
    }

    // Update is called once per frame
    public void Update(){

         switch(state){

            case transitionState:

                Transition();

                break;

            case introState:

                Intro();

                break;

            case startTransitionState:

                StartTransition();

                break;

        }

    }

    private void Intro(){

        elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) {

            state = startTransitionState;

        } 

        Title.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;

        }         

    }

    private void StartTransition(){

        elapsedTime += 0.005f;

        TransitionPanel.GetComponent<CanvasGroup>().alpha = elapsedTime;

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            TransitionPanel.GetComponent<CanvasGroup>().interactable = false;

            MainMenu.state = 0;

            SceneManager.LoadScene("MainMenu");

        } 

    }

    private void Transition(){

        elapsedTime += Time.deltaTime;

        TransitionPanel.GetComponent<CanvasGroup>().alpha = 1-elapsedTime;

        print(elapsedTime);

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;

            TransitionPanel.GetComponent<CanvasGroup>().interactable = true;

            TransitionPanel.SetActive(false);

            state = introState;

        } 

    }
}
