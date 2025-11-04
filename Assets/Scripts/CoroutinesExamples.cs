using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutinesExamples : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(ExampleCoroutine());
    }
    
    IEnumerator ExampleCoroutine()  // using System.Collections;
        // Ett tidsschema ("tidslinje i kod"), används åt sekvenser
        // "Co-" för att den fungerar ihop med annan kod
    {
        float waitTime = 2f;

        print("Starting Coroutine...");

        yield return new WaitForSeconds(waitTime);

        print("Two seconds went by... Waiting for 'F'-key");  // Testkör och printa "Ending Coroutine..."

        yield return new WaitUntil(IsFKeyPressed);  // Tar en funktion

        print("'F'-key was pressed...");  // Testkör

        // Lite mer dynamiskt
        KeyCode[] keysToCheck = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

        foreach (KeyCode key in keysToCheck)
        {
            print($"Waiting for '{key}'-key");
            yield return new WaitUntil(() => Input.GetKeyDown(key));  // (parametrar) => funktionens kod
        }

        print("All keys checked... Coroutine finished!");
    }

    bool IsFKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
