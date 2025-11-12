using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Coroutines : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
        // Co- "för att den sammarbetar med resten av koder"
        // Routine- Tidslinje i kod (används åt sekvenser)

    {
        print("Starting Coroutine...");

        float waitTime = 2f;

        yield return new WaitForSeconds(waitTime);

        print("Two seconds went by... Waiting for G");

        yield return new WaitUntil(IsGPressed);

        print("G has been pressed...");

        KeyCode[] keysToCheck = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

        foreach (KeyCode key in keysToCheck)
        {
            print($"Waiting for {key}");
            yield return new WaitUntil(() => Input.GetKeyDown(key));  // Anonym funktion
            // (parametrar) => funktionens kod
        }

        print("All keys checked... Coroutine finished!");

    }

    bool IsGPressed()
    {
        return Input.GetKeyDown(KeyCode.G);
    }
}
