using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button Button;

  
    public GameObject GameObject;



    private void Awake()
    {
        Button.onClick.AddListener(Respawn);
        
    }
    
    public void Respawn()
    {
        
        Instantiate(GameObject);
        
    }
}
