using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;
    public TextMeshProUGUI textcomponent;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        else 
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void setandshowtooltip(string message)
    {
        gameObject.SetActive (true);
        textcomponent.text = message;
    }

    public void hidetooltip()
    {
        gameObject.SetActive (false);
        textcomponent.text = string.Empty;
    }
}
