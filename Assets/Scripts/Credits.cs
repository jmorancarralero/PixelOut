using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour{

    [SerializeField] private TMP_Text text;

    [SerializeField] private GameObject TransitionPanel;

    private float elapsedTime = 0f;
    private float fontSize = 0f;

    private Vector2 sizeDelta;
    private Vector2 position;

    private int cont = 0;

    private const int stateMusic = 1;
    private const int stateArt = 2;
    private const int stateThanks = 3;
    private const int stateFinish = 4;

    public static int state = 0;

    private const int transitionState = 0;
    private const int introState = 1;
    private const int startTransitionState = 2;

    // Start is called before the first frame update
    private void Start(){

        fontSize = text.fontSize;

        sizeDelta = text.rectTransform.sizeDelta;

        position = text.transform.position;

    }

    // Update is called once per frame
    private void Update(){

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

        elapsedTime += Time.deltaTime / 100;

        if(elapsedTime < 0.01f){

            text.color = new Color(text.color.r,text.color.g,text.color.b, elapsedTime * 100);

        }else if(elapsedTime >= 0.01f && elapsedTime <= 0.09f){

            text.fontSize = text.fontSize + elapsedTime;
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x + (elapsedTime * 20),text.rectTransform.sizeDelta.y);
            text.transform.position = new Vector2(text.transform.position.x + (elapsedTime * 3) ,text.transform.position.y - elapsedTime);

        }else if(elapsedTime > 0.09f){

            text.color = new Color(text.color.r,text.color.g,text.color.b, 9f - (elapsedTime * 100));

        }

        if (elapsedTime >= 0.1f) {

            elapsedTime = 0f;
            cont += 1;

            text.fontSize = fontSize;
            text.rectTransform.sizeDelta = sizeDelta;
            text.transform.position = position;

            switch(cont){

                case stateMusic:

                    text.text = "music by J.MORAN";

                    break;
                
                case stateArt:

                    text.text = "art by J.MORAN";

                    break;

                case stateThanks:

                    text.text = "Thanks for play";

                    break;

                case stateFinish:

                    state = startTransitionState;

                    break;              
                                
            }

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
