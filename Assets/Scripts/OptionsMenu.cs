using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenu : MonoBehaviour{
    [SerializeField] private TMP_Text Return;
    [SerializeField] private UnityEngine.Video.VideoPlayer VideoBackground;
    [SerializeField] private AudioSource AudioBackground;
    private string lastObject;
    private float elapsedTime = 0f;
    private bool glowActive = false; 
    private bool ReturnActive = false;
    public static bool OptionsActive = false;
    public static long frameVideo;
    public static float timeAudio;  
    private const string ReturnText = "Return";
    private const string ContainerText = "Container";
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    public void Start(){
        VideoBackground.frame = MainMenu_old.frameVideo;
        AudioBackground.time = MainMenu_old.timeAudio;

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        OptionsActive = true;
    }

    // Update is called once per frame
    public void Update(){

        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results){

            switch (result.gameObject.name){
                case ReturnText:
                    ReturnActive = true;
                    break;
                default:
                    if (lastObject == ContainerText) {
                        ReturnActive = false;
                    }
                    break;
            }
            lastObject = result.gameObject.name;
        }

        elapsedTime += Time.deltaTime;

        if (ReturnActive) {
            Return.color = new Color(255,255,255, glowActive?elapsedTime:1-elapsedTime);
        }else{
            Return.color = new Color(255,255,255,255);
        }

        if (elapsedTime >= 1f) {
            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;
        } 
    }

    public void ReturnToMenu(){
        frameVideo = VideoBackground.frame;
        timeAudio = AudioBackground.time;
        SceneManager.LoadScene("MainMenu");
    }
}
