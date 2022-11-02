using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLineGenerator : KochGenerator
{   
    LineRenderer _lineRenderer;
    public float _lerpAmount = 1;
    Vector3[] _lerpPostion;
    public float _generateMultiplier = 1;
    
    public int _iteratorAmount = 3;
    public int j = 1; 

    // Start is called before the first frame update
    void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.positionCount = _position.Length;
        // lineRenderer.SetPositions(_position);
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = 2;
        _lineRenderer.startColor = randomColor();
        _lineRenderer.endColor = randomColor();
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);

        if(_generationCount!= 0){
            for (int i=0;i<_position.Length;++i){
                _lerpPostion[i] = Vector3.Lerp(_position[i],_targetPosition[i],_lerpAmount);
            }
            _lineRenderer.SetPositions(_lerpPostion);
        }

        // for(int j = 1; j<= _iteratorAmount; ++j){
        //     KochGenerate(_targetPosition,true,_generateMultiplier);
        //     _lerpPostion = new Vector3[_position.Length];
        //     _lineRenderer.positionCount = _position.Length;
        //     _lineRenderer.SetPositions(_position);
        //     _lerpAmount = 1;
        // }
    }

    // Update is called once per frame
    void Update()
    {   
        // if(_generationCount!= 0){
        //     for (int i=0;i<_position.Length;++i){
        //         _lerpPostion[i] = Vector3.Lerp(_position[i],_targetPosition[i],_lerpAmount);
        //     }
        //     _lineRenderer.SetPositions(_lerpPostion);
        // }

        while(j<= _iteratorAmount){
            KochGenerate(_targetPosition,true,_generateMultiplier);
            _lerpPostion = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 1;
            j++;
        }
        

        
        // if(Input.GetKeyUp(KeyCode.I)){
        //     KochGenerate(_targetPosition,false,_generateMultiplier);
        //     _lerpPostion = new Vector3[_position.Length];
        //     _lineRenderer.positionCount = _position.Length;
        //     _lineRenderer.SetPositions(_position);
        //     _lerpAmount = 1;
        // }
    }

    Color randomColor(){
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
