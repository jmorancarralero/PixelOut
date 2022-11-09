using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour{

    [SerializeField] private TMP_Text Title;

    private float elapsedTime = 0f;

    private bool glowActive = false;

    // Start is called before the first frame update
    public void Start(){
        
    }

    // Update is called once per frame
    public void Update(){

        elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) {

            SceneManager.LoadScene("MainMenu");

        } 

        Title.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, glowActive?elapsedTime:1-elapsedTime);

        if (elapsedTime >= 1f) {

            elapsedTime = elapsedTime % 1f;
            glowActive = !glowActive;

        } 

    }
}
