using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextLevel : MonoBehaviour {
    [SerializeField] string triggeringTag;
    
    [Tooltip("Name of scene to move to when triggering the given tag")] 
    [SerializeField] string sceneName;
    private NumberField ammoField;
    private bool isGameOverScene ;
    private void Start(){
        isGameOverScene = (sceneName == "level-game-over") ;
        Transform ammoText = transform.Find("AmmoField") ;
        ammoField = ammoText.GetComponent<NumberField>() ;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == triggeringTag) {
            if(!isGameOverScene){
                ammoField.Reset() ;
            }
            other.transform.position = Vector3.zero;
            SceneManager.LoadScene(sceneName);    // Input can either be a serial number or a name; here we use name.
        }
    }
}
