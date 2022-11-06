using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{   private int _traingleItr = 1;
    private int _kochItr = 1;
    public GameObject _traingle;
    public GameObject _koch;
    // Start is called before the first frame update
    void Start()
    {  
       _traingleItr =  _traingle.GetComponent<TetraHedron>()._iterations;
       _kochItr = _koch.GetComponent<KochLineGenerator>()._kochIterations;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            _koch.SetActive(false);
            _traingle.SetActive(true);
            // if(_traingleItr < 10 ) _traingleItr+=1;
            // _traingle.GetComponent<TetraHedron>()._iterations = _traingleItr;
        }
        // if(Input.GetKeyDown(KeyCode.O)){
        //     if(_traingleItr > 1 ) _traingleItr-=1;
        //     _traingle.GetComponent<TetraHedron>()._iterations = _traingleItr;
        // }
        if(Input.GetKeyDown(KeyCode.K)){
            _koch.SetActive(true);
            _traingle.SetActive(false);
            //  if(_kochItr < 8 ) _kochItr+=1;
            // _koch.GetComponent<KochLineGenerator>()._kochIterations = _kochItr;
        }
        // if(Input.GetKeyDown(KeyCode.L)){
        //     if(_kochItr > 1 ) _kochItr-=1;
        //     _koch.GetComponent<KochLineGenerator>()._kochIterations = _kochItr;
        // }

    }
}
