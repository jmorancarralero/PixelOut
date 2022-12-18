using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour{

    // Start is called before the first frame update
    private void Start(){
        
    }

    // Update is called once per frame
    private void Update(){
        
    }

    public class DataSaver{

        //Save Data
        public static void saveData<T>(T dataToSave, string dataFileName){
            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, dataFileName + ".txt");

            //Convert To Json then to bytes
            string jsonData = JsonUtility.ToJson(dataToSave, true);
            byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);

            //Create Directory if it does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath))){
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
            }
            //Debug.Log(path);

            try{
                File.WriteAllBytes(tempPath, jsonByte);
                Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
            }catch (Exception e){
                Debug.LogWarning("Failed To PlayerInfo Data to: " + tempPath.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }
        }

        //Load Data
        public static T loadData<T>(string dataFileName){
            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, dataFileName + ".txt");

            //Exit if Directory or File does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath))){
                Debug.LogWarning("Directory does not exist");
                return default(T);
            }

            if (!File.Exists(tempPath)){
                Debug.Log("File does not exist");
                return default(T);
            }

            //Load saved Json
            byte[] jsonByte = null;
            try{
                jsonByte = File.ReadAllBytes(tempPath);
                Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
            }catch (Exception e){
                Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }

            //Convert to json string
            string jsonData = Encoding.ASCII.GetString(jsonByte);

            //Convert to Object
            object resultValue = JsonUtility.FromJson<T>(jsonData);
            return (T)Convert.ChangeType(resultValue, typeof(T));
        }

        public static bool deleteData(string dataFileName){
            bool success = false;

            //Load Data
            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, dataFileName + ".txt");

            //Exit if Directory or File does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath))){
                Debug.LogWarning("Directory does not exist");
                return false;
            }

            if (!File.Exists(tempPath)){
                Debug.Log("File does not exist");
                return false;
            }

            try{
                File.Delete(tempPath);
                Debug.Log("Data deleted from: " + tempPath.Replace("/", "\\"));
                success = true;
            }catch (Exception e){
                Debug.LogWarning("Failed To Delete Data: " + e.Message);
            }

            return success;
        }
    }

    [Serializable]
    public class PlayerInfo{

        public int level = 1;
        public int life = 100;
        public int actualLife = 100;

        public float posX = -7.212492f;
        public float posY = 5.554957f;

        public bool homeActive = true;
        public bool cityActive = false;
        public bool labMainActive = false;
        public bool labOutsideActive = false;
        public bool labCenterActive = false;
        public bool labLeftActive = false;
        public bool labRightActive = false;
        public bool labTopActive = false;
        public bool labCenterDoorActive = true;
        public bool labMainDoorActive = true;
        public bool initialSceneActive = true;
        public bool momTextActivated = false;
        public bool shopTextActivated = false;
        public bool unknownTextActivated = false;
        public bool unknownText2Activated = false;
        public bool consoleTextActivated = false;
        public bool pclabTextActivated = false;
        public bool leftButtonActivated = false;
        public bool righttButtonActivated = false;
        public bool combatActive = false;

        public string selectedWeapon;
    

    }

    [Serializable]
    public class OptionsInfo{

        public bool fullScreen = false;

        public float audioVolume = 0;

        public int indexQuality = 0;

    }

    public static string[,] initialText(){

        return  new string[,] { {"¡This is PixelOut!","¡The new vide ogame of the moment!","The most intrepid and futuristic video game","That has ever existed",
        "Can you resist playing it?", "Should I buy this vide ogame"}, 
        {"unknown","unknown","unknown","unknown","unknown","me"} };

    }

    public static string[,] momText(){

        return  new string[,] { {"Mom, I want to buy a new video game","Is called PixelOut","¡¡I need it!!",
        "Son, you bought too many video games last month","Why do you need another game?","Please, mom","Ok son, here you have money to buy","Thanks mom"}, 
        {"me","me","me","known_female","known_female","me","known_female","me"} };

    }

    public static string[,] momText2(){

        return  new string[,] { {"I hope you enjoy this video game"}, 
        {"known_female"} };

    }

    public static string[,] shopText(){

        return  new string[,] { {"I do not have enough money","I should ask mom for money"}, 
        {"me","me"} };

    }

    public static string[,] shopText2(){

        return  new string[,] { {"Only games left for the next 3 customers","The rest will have to wait",
        "Until back in stock","It can't be real, I'm late","I won't be able to play the game"}, 
        {"unknown","unknown","unknown","me","me"} };

    }

    public static string[,] shopText3(){

        return  new string[,] { {"It seems I'm late"}, 
        {"me"} };

    }

    public static string[,] unknownText(){

        return  new string[,] { {"It seems that he will not say anything"}, 
        {"me"} };

    }

    public static string[,] unknownText2(){

        return  new string[,] { {"Hey, little boy","Do you want a PixelOut?",
        "It just so happens that I have an exclusive copy","But I think I'm not going to use it","Do you want it?","I suspect this man","What do you want in exchange?","Nothing, ha ha, take it as a gift","I don't trust anything, but a gift is a gift","I hope you like it ha ha ha"}, 
        {"known_male","known_male","known_male","known_male","known_male","me","me","known_male","me","known_male"} };

    }

    public static string[,] unknownText3(){

        return  new string[,] { {"I hope you like it ha ha ha"}, 
        {"known_male"} };

    }


    public static string[,] consoleText(){

        return  new string[,] { {"I need to buy PixelOut","To play on my Switch"}, 
        {"me","me"} };

    }

    public static string[,] consoleText2(){

        return  new string[,] { {"Finally I can try it","The game seems to give off a strange image"}, 
        {"me","me"} };

    }

    public static string[,] labText(){

        return  new string[,] { {"Where am I?","what is this place?","I think I must find a way out as soon as possible","On the table there seems to be weapons","It seems like a dangerous place, I should take one"}, 
        {"me","me","me","me","me"} };

    }

    public static string[,] pcText(){

        return  new string[,] { {"Looks like a computer","Touching it, I feel invigorated","Your health points have been restored"}, 
        {"me","me","unknown"} };

    }

    public static string[,] pcText2(){

        return  new string[,] { {"Your health points have been restored"}, 
        {"unknown"} };

    }

    public static string[,] labDoorText(){

        return  new string[,] { {"It seems to be closed, I can't open it"}, 
        {"me"} };

    }

    public static string[,] labDoor2Text(){

        return  new string[,] { {"The door suddenly opened"}, 
        {"me"} };

    }

    public static string[,] finalText(){

        return  new string[,] { {"You do not know who I am","But this is your end","Die"}, 
        {"unknown","unknown","unknown"} };

    }


    public static string[,] buttonActiveText(){

        return  new string[,] { {"It seems that something was activated"}, 
        {"unknown"} };

    }

    public static string[,] swordText(){

        return  new string[,] { {"Looks like a light sword, balanced"}, 
        {"me"} };

    }

    public static string[,] gunText(){

        return  new string[,] { {"It looks like a short pistol, accurate, but less damaging"}, 
        {"me"} };

    }

    public static string[,] hammerText(){

        return  new string[,] { {"It looks like a big hammer, with a lot of damage but less accurate"}, 
        {"me"} };

    }


    public static string[,] selectedText(){

        return  new string[,] { {"There is nothing, I have already chosen a weapon"}, 
        {"me"} };

    }

    public static string[,] notSelectedText(){

        return  new string[,] { {"You cannot carry more than one weapon"}, 
        {"unknown"} };

    }

}
