using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool boolSwitcher = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            boolSwitcher = !boolSwitcher;
            gameObject.transform.GetChild(0).gameObject.SetActive(boolSwitcher);
        }

    }
}
