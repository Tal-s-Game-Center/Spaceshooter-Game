using TMPro;
using UnityEngine;

/**
 * This component should be attached to a TextMeshPro object.
 * It allows to feed an integer number to the text field.
 */
[RequireComponent(typeof(TextMeshPro))]
public class NumberField : MonoBehaviour {
    private int number;
    private int defaultNumber ;
    public int GetNumber() {
        return this.number;
    }

    public void SetNumber(int newNumber) {
        this.number = newNumber;
        Debug.Log("Set to: " + newNumber) ;
        GetComponent<TextMeshPro>().text = newNumber.ToString();
    }

    public void AddNumber(int toAdd) {
        SetNumber(this.number + toAdd);
    }
    public void SetDefaultNumber(int number){
        this.defaultNumber = number ;
    }
    public void Reset(){
        SetNumber(defaultNumber) ;
    }
}
