using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour
{
    public struct LineSegment
    {
        public Vector3 StartPosition {get; set;}
        public Vector3 EndPosition {get; set;}
        public Vector3 Direction {get; set;}
        public float Length {get; set;}
    }


    private Vector3[] _intiaterPoint;
    private Vector3 _rotateVector;
    [SerializeField]
    protected float _initiatorSize;
    
    private Vector3 _rotateAxis;

    protected Vector3[] _position;
    protected Vector3[] _targetPosition;
    private List<LineSegment> _lineSegment;
    [SerializeField]
    protected AnimationCurve generator;
    protected Keyframe[] _keys;


    protected int _generationCount;

    void Awake(){
        _position = new Vector3[4];
        _targetPosition = new Vector3[4];
        _keys = generator.keys;
        _lineSegment = new List<LineSegment>();
        _rotateVector = new Vector3(0, 1 ,0);
        _rotateAxis = new Vector3(0, 0, 1);

        for (int i = 0;i<3;++i){
            _position[i] = _rotateVector*_initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360/3,_rotateAxis)*_rotateVector;
        }

        _position[3] = _position[0];
        _targetPosition = _position;
    }

    protected void KochGenerate(Vector3[] positions, bool outwards, float generatorMultiplier){
        
        //creating line segments
        _lineSegment.Clear();

        for (int i=0; i<positions.Length-1;++i){
            LineSegment line = new LineSegment();
            line.StartPosition = positions[i];
            if (i == positions.Length-1){
                line.EndPosition = positions[0];
            }
            else{
                line.EndPosition = positions[i+1];
            }

            line.Direction = (line.EndPosition - line.StartPosition).normalized;
            line.Length = Vector3.Distance(line.EndPosition,line.StartPosition);
            _lineSegment.Add(line);
        }

        //add the line segment points to a point array
        List<Vector3> newPos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();

        for(int i = 0;i<_lineSegment.Count;++i){

            newPos.Add(_lineSegment[i].StartPosition);
            targetPos.Add(_lineSegment[i].StartPosition);

            for(int j = 1;j<_keys.Length-1;++j){

                float moveAmount = _lineSegment[i].Length * _keys[j].time;
                float heightAmount = (_lineSegment[i].Length * _keys[j].value) * generatorMultiplier;
                Vector3 movePos = _lineSegment[i].StartPosition + (_lineSegment[i].Direction * moveAmount);
                Vector3 Dir;

                if(outwards){
                    Dir = Quaternion.AngleAxis(-90,_rotateAxis) * _lineSegment[i].Direction;
                }
                else{
                    Dir = Quaternion.AngleAxis(90,_rotateAxis) * _lineSegment[i].Direction;
                }

                newPos.Add(movePos);
                targetPos.Add(movePos+(Dir* heightAmount));
            }
        }

        newPos.Add(_lineSegment[0].StartPosition);
        targetPos.Add(_lineSegment[0].StartPosition);
        _position = new Vector3[newPos.Count];
        _targetPosition = new Vector3[targetPos.Count];
        _position = newPos.ToArray();
        _targetPosition = targetPos.ToArray();

        _generationCount++;
    }

    private void OnDrawGizmos(){
        _intiaterPoint = new Vector3[3];
        _rotateVector = new Vector3(0, 1 ,0);
        _rotateAxis = new Vector3(0, 0, 1);

        for (int i = 0;i<3;++i){
            _intiaterPoint[i] = _rotateVector*_initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360/3,_rotateAxis)*_rotateVector;
        }

        for (int i = 0;i<3;++i){
            Gizmos.color = Color.white;
            Matrix4x4 transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.matrix = transformMatrix;
            if(i<3-1){
                Gizmos.DrawLine(_intiaterPoint[i], _intiaterPoint[i+1]);
            }
            else{
                Gizmos.DrawLine(_intiaterPoint[i], _intiaterPoint[0]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.DrawLine(new Vector3(0,0), new Vector3(50,50),Color.white,100f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
