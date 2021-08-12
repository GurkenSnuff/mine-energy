using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterStart : MonoBehaviour
{
    
    private WindGenerator windGenerator;
    private SolarZellen solarZellen;
    private KohleGenerator kohleGenerator;
    public Ressourcen ressourcen;

    void Awake()
    {
        windGenerator = FindObjectOfType<WindGenerator>();
        solarZellen = FindObjectOfType<SolarZellen>();
        kohleGenerator = FindObjectOfType<KohleGenerator>();
        ressourcen = FindObjectOfType<Ressourcen>();
    }
    
    void Start()
    {
        StartCoroutine(Wait());
        
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        solarZellen.CounterStart();
        kohleGenerator.CounterStart();
        windGenerator.CounterStart();
        ressourcen.CounterStart();
        
    }
    
}
