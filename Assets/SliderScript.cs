using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{   
    [SerializeField]
    private Slider _slider;

    // [SerializeField]
    // private TextMeshProUGUI _dropDown;

    [SerializeField]
    private TextMeshProUGUI _sliderText;

    public GameObject _TraingleObject;
    public GameObject _KochObject;

    private TetraHedron _triangle;
    private KochLineGenerator _koch;

    // private int _traingleItr = 1;
    // private int _kochItr = 1;

    // private int _iterMax;

    //for resetting slider value, for the fractal in update. Prevents from calling again everyframe
    static int _sliderValueChange = -1;

    // Start is called before the first frame update
    void Start()
    {
        _triangle = _TraingleObject.GetComponent<TetraHedron>();
        _koch = _KochObject.GetComponent<KochLineGenerator>();
        // var _dropDown = transform.GetComponent<Dropdown>();
        _slider.onValueChanged.AddListener((v) =>{
            if(_TraingleObject.activeSelf){
            // _slider.interactable = true;
            // _slider.maxValue = 10;
            // if(_traingleItr < 10 ) _traingleItr+=1;
            _triangle.GetComponent<TetraHedron>()._iterations = (int)v;
        }
        if(_KochObject.activeSelf){
            // _slider.interactable = true;
            // _slider.maxValue = 8;
            _koch.GetComponent<KochLineGenerator>()._kochIterations = (int)v;
        }
        // if(!_TraingleObject.activeSelf && !_KochObject.activeSelf){
        //     // _slider.interactable = false;
        //     _slider.maxValue = 0;
        // }
            _sliderText.text = v.ToString("0");
        });
        
    }

    //Update is called once per frame
    void Update()
    {   
         if(_TraingleObject.activeSelf){
            _slider.interactable = true;
            _slider.maxValue = 10;
            _slider.minValue = 1;
            if(_sliderValueChange!=1) {
                _sliderValueChange = 1;
                _slider.value = 1;
                _triangle.GetComponent<TetraHedron>()._iterations = (int)_slider.value;
            }
            // if(_traingleItr < 10 ) _traingleItr+=1;
            // _triangle.GetComponent<TetraHedron>()._iterations = (int)v;
        }
        if(_KochObject.activeSelf){
            _slider.interactable = true;
            _slider.maxValue = 8;
            _slider.minValue = 1;
             if(_sliderValueChange!=2) {
                _sliderValueChange = 2;
                _slider.value = 1;
                _koch.GetComponent<KochLineGenerator>()._kochIterations = (int)_slider.value;
            }
            // _koch.GetComponent<KochLineGenerator>()._kochIterations = (int)v;
        }
        if(!_TraingleObject.activeSelf && !_KochObject.activeSelf){
            _slider.interactable = false;
            _slider.maxValue = 0;
            _slider.minValue = -1;
            if(_sliderValueChange!=0) {
                _sliderValueChange = 0;
                _slider.value = 0;
                _triangle.GetComponent<TetraHedron>()._iterations = 0;
                _koch.GetComponent<KochLineGenerator>()._kochIterations = 0;
            }
        }

    }
}
