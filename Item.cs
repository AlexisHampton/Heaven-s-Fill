using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemText;

    private void OnTriggerStay2D(Collider2D collision)
    {
        UIManager.Instance.eButton.SetActive(true);
        if (Input.GetKey(KeyCode.E))
            UIManager.Instance.FillObject(itemText); 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UIManager.Instance.eButton.SetActive(false);
    }
}
