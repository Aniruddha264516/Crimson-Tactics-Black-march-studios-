using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        TooltipManager._instance.setandshowtooltip(message);

    }

    private void OnMouseExit()
    {
        TooltipManager._instance.hidetooltip();
    }
}
