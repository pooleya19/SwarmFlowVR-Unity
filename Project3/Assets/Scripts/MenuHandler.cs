using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{    

    public GameObject[] GO_RC_Buttons;
    public delegate void Function(string outRCName);
    public Function outButtonClick = null;

    public void onButtonClick(string buttonName){
        if(outButtonClick != null) {
            outButtonClick(buttonName);
        }
    }

}
