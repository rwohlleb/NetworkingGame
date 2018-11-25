using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.
using Assets;
public class OnClickBehavior : MonoBehaviour {

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        EventManager.TargetClicked();
        Destroy(gameObject);
        
    }
}
