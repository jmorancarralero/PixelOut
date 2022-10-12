using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Play;
    [SerializeField] private TMP_Text Options;
    [SerializeField] private TMP_Text Exit;
    [SerializeField] private UnityEngine.Video.VideoPlayer VideoBackground;
    [SerializeField] private AudioSource AudioBackground;
    private string lastObject; 
    private float elapsedTime = 0f;
    private bool glowActive = false;
    private bool PlayActive = false;
    private bool OptionsActive = false;
    private bool ExitActive = false;
    public static long frameVideo;
    public static float timeAudio;  
    private const string PlayText = "Play";
    private const string OptionsText = "Options";
    private const string ExitText = "Exit";
    private const string ContainerText = "Container";
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    // Start is called before the first frame update
    void Start(){
        if (OptionsMenu.OptionsActive) {
            VideoBackground.frame = OptionsMenu.frameVideo;
            AudioBackground.time = OptionsMenu.timeAudio;
        }else{
            VideoBackground.frame = Intro.frameVideo;
            AudioBackground.time = Intro.timeAudio;
        }


        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update(){

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
                default:
                    if (lastObject == ContainerText) {
                        PlayActive = false;
                        OptionsActive = false;
                        ExitActive = false;
                    }
                    break;
            }
            lastObject = result.gameObject.name;
        }


        elapsedTime += Time.deltaTime;

        Title.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);

        if (PlayActive) {
            Play.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);
        }else if (OptionsActive) {
            Options.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);
        }else if (ExitActive) {
            Exit.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);
        }else{
            Play.color = new Color(255,255,255,255);
            Options.color = new Color(255,255,255,255);
            Exit.color = new Color(255,255,255,255);
        }

        if (elapsedTime >= 1f) {
            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;
        } 

    }

    public void GoToSelectionMenu(){
        SceneManager.LoadScene("SelectionMenu");
    }

    public void OpenOptions(){
        frameVideo = VideoBackground.frame;
        timeAudio = AudioBackground.time;
        SceneManager.LoadScene("Options");
    }

    public void ExitGame(){
        Application.Quit();
    }
}
