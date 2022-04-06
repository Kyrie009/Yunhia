using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to add a dialogue variable that can be easily editted in other script.
[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea(3,10)] public string[] sentences;


    
}
