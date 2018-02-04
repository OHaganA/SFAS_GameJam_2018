using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    private InputField input;
    void Start()
    {
        input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);  // This also work
    }
    private void SubmitName(string arg0)
    {
        Debug.Log("Setting Name");
        if(input.name == "P1InputField")
        {
            Debug.Log("P1 Name Set to" + arg0);
        }

        else
        {
            Debug.Log("P2 Name Set to" + arg0);
        }

    }
}