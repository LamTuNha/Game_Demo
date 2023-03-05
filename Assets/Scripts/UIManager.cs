using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefabs;
    public GameObject healthTextPrefabs;

    public Canvas gameCanvas;

    void Awake(){
        gameCanvas = FindObjectOfType<Canvas>();

    }

    private void OnEnable() {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed; 
    }

    private void OnDisable() {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed; 
    }

    public void CharacterTookDamage(GameObject character, int damageRecevied){
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmptext = Instantiate(damageTextPrefabs, spawnPosition, Quaternion.identity, gameCanvas.transform)
                            .GetComponent<TMP_Text>();

        tmptext.text = damageRecevied.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored){
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmptext = Instantiate(healthTextPrefabs, spawnPosition, Quaternion.identity, gameCanvas.transform)
                            .GetComponent<TMP_Text>();

        tmptext.text = healthRestored.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context){
        if(context.started){
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name+" : "+this.GetType()+" : "+System.Reflection.MethodBase.GetCurrentMethod().Name);
            #endif
            #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying =false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #elif (UNITY_WEBGL)
                SceneManager.LoadScene("QuitScene")
            #endif

        }
    }

}
