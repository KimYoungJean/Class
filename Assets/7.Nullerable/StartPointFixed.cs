using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartPointFixed : MonoBehaviour
{
    internal Vector3? startPoint; // ¿œ¡æ¿« Boxed Type (Nullable)
    
    public void DisplayPoint()
    {
        
        if(startPoint != null) print(startPoint);
        else Debug.LogError("Not Initialized");

        if(startPoint.HasValue) print(startPoint.Value);
        else Debug.LogError("Not Initialized");

        print(startPoint?.ToString() ?? "Not Initialized");
    }

    private void Start()
    {
        DisplayPoint();
        
    }
}

