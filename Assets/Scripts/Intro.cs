using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Intro : MonoBehaviour{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private UnityEngine.Video.VideoPlayer VideoBackground;
    [SerializeField] private AudioSource AudioBackground;
    private float elapsedTime = 0f;
    private bool glowActive = false;
    public static long frameVideo;
    public static float timeAudio;    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) {
            frameVideo = VideoBackground.frame;
            timeAudio = AudioBackground.time;
            SceneManager.LoadScene("MainMenu");
        } 
        Title.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);
        if (elapsedTime >= 1f) {
            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;
        } 
    }
}
