using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SwapSqares : MonoBehaviour
{
        public void OnMouseDown()
        {
            //Output the name of the GameObject that is being clicked
            Debug.Log(name + "Game Object Click in Progress");
        }

        public void OnMouseUp()
        {
            Debug.Log(name+ "Stoped the click.");
        }
}
 