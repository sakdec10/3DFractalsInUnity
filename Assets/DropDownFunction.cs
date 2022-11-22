using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownFunction : MonoBehaviour
{   
    public GameObject _TraingleObject;
    public GameObject _KochObject;

    private TetraHedron _triangle;
    private KochLineGenerator _koch;


    // private int _traingleItr = 1;
    // private int _kochItr = 1;

    // Start is called before the first frame update
    void Start()
    {   _triangle = _TraingleObject.GetComponent<TetraHedron>();
        _koch = _KochObject.GetComponent<KochLineGenerator>();
         _TraingleObject.SetActive(false);
         _KochObject.SetActive(false);
    }

    public void dropValueBehaviour(int val){
        if(val == 0){
            _KochObject.SetActive(false);
            _TraingleObject.SetActive(false);
            
        }
        if(val == 1){
            _koch.GetComponent<KochLineGenerator>()._kochIterations = 1;
            _KochObject.SetActive(false);
            _TraingleObject.SetActive(true);
            
        }
        if(val == 2){
            _triangle.GetComponent<TetraHedron>()._iterations = 1;
            _TraingleObject.SetActive(false);
            _KochObject.SetActive(true);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.T)){
    //         _TraingleObject.SetActive(true);
    //         _KochObject.SetActive(false);
    //         // if(_traingleItr < 10 ) _traingleItr+=1;
    //         // _traingle.GetComponent<TetraHedron>()._iterations = _traingleItr;
    //     }
    //     // if(Input.GetKeyDown(KeyCode.O)){
    //     //     if(_traingleItr > 1 ) _traingleItr-=1;
    //     //     _traingle.GetComponent<TetraHedron>()._iterations = _traingleItr;
    //     // }
    //     if(Input.GetKeyDown(KeyCode.K)){
    //         _TraingleObject.SetActive(false);
    //         _KochObject.SetActive(true);
    //         //  if(_kochItr < 8 ) _kochItr+=1;
    //         // _koch.GetComponent<KochLineGenerator>()._kochIterations = _kochItr;
    //     }
    //     // if(Input.GetKeyDown(KeyCode.L)){
    //     //     if(_kochItr > 1 ) _kochItr-=1;
    //     //     _koch.GetComponent<KochLineGenerator>()._kochIterations = _kochItr;
    //     // }

    // }
}
