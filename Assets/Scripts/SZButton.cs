using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SZButton : MonoBehaviour
{
    public SolarZellen solarZellen;
   public void SZClick()
   {
        solarZellen = FindObjectOfType<SolarZellen>();
        solarZellen.SZWillK();
   }
}
