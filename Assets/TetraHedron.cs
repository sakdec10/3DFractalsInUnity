using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraHedron : MonoBehaviour
{   private Vector3 _targetZoom = new Vector3 (1f,1f,1f);
    
    [SerializeField]
    [Range(1, 10)]
    public int _iterations = 6;

    public class STetrahedron
    {
        static float s8_9 = Mathf.Sqrt(8f/9f);
        static float s2_9 = Mathf.Sqrt(2f / 9f);
        static float s2_3 = Mathf.Sqrt(2f / 3f);
        static float f1_3 = 1f / 3f;
        public float Size = 50;
        public List<Vector3> centers = new List<Vector3>();
        
        
        public STetrahedron Subdivide()
        {
            var result = new STetrahedron();
            float s = result.Size = Size*0.5f;
            if (centers.Count == 0)
                centers.Add(Vector3.zero);
            foreach(var c in centers)
            {
                result.centers.Add(c + new Vector3(0, s, 0));
                result.centers.Add(c + new Vector3(-s2_3*s, -f1_3 * s, -s2_9*s));
                result.centers.Add(c + new Vector3( s2_3*s, -f1_3 * s, -s2_9*s));
                result.centers.Add(c + new Vector3(0, -f1_3 * s, s8_9*s));
            }
            return result;
        }
        public STetrahedron Subdivide(int aCount)
        {
            var res = this;
            for (int i = 0; i < aCount; i++)
                res = res.Subdivide();
            return res;
        }
        public Mesh CreateMesh()
        {
            var m = new Mesh();
            m.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            Vector3[] vertices = new Vector3[centers.Count*12];
            Vector3[] normals = new Vector3[vertices.Length];
            float s = Size;
            int i = 0;
            foreach (var c in centers)
            {
                var v0 = c + new Vector3(0, s, 0);
                var v1 = c + new Vector3(-s2_3 * s, -f1_3 * s, -s2_9 * s);
                var v2 = c + new Vector3(s2_3 * s, -f1_3 * s, -s2_9 * s);
                var v3 = c + new Vector3(0, -f1_3 * s, s8_9 * s);
    
                normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v2 - v0, v1 - v0).normalized;
                vertices[i++] = v0; vertices[i++] = v2; vertices[i++] = v1;
    
                normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v1 - v0, v3 - v0).normalized;
                vertices[i++] = v0; vertices[i++] = v1; vertices[i++] = v3;
    
                normals[i] = normals[i + 1] = normals[i + 2] = Vector3.Cross(v3 - v0, v2 - v0).normalized;
                vertices[i++] = v0; vertices[i++] = v3; vertices[i++] = v2;
    
                normals[i] = normals[i + 1] = normals[i + 2] = Vector3.down;
                vertices[i++] = v1; vertices[i++] = v2; vertices[i++] = v3;
            }
            int[] triangles = new int[vertices.Length];
            for (int n = 0; n < triangles.Length; n++)
                triangles[n] = n;

            m.vertices = vertices;
            m.normals = normals;
            m.triangles = triangles;
            m.RecalculateBounds();
            return m;
        }
    }

    private MeshFilter meshFilter;

    private STetrahedron sibl;   

    float time;
    float timeDelay;

    // Start is called before the first frame update
    void Start(){ 
        time = 0f;
        timeDelay = 3f;
         sibl = new STetrahedron().Subdivide(_iterations);
         meshFilter = GetComponent<MeshFilter>();
         GetComponent<MeshRenderer>().material.color =
			Color.Lerp(Color.red, Color.blue, (float)_iterations /10);
         meshFilter.mesh = sibl.CreateMesh();
    }

    // Update is called once per frame
    void Update(){
        time = time + 1f * Time.deltaTime;

        if (time >= timeDelay) {
            time = 0f;
            sibl = new STetrahedron().Subdivide(_iterations);
            meshFilter = GetComponent<MeshFilter>();
            GetComponent<MeshRenderer>().material.color =
			    Color.Lerp(Color.red, Color.blue, (float)_iterations/10);
            meshFilter.mesh = sibl.CreateMesh();
        }

        HandleZoom();

    }

     private void HandleZoom() {
        if(Input.mouseScrollDelta.y > 0) _targetZoom += new Vector3(1.2f,1.2f,1.2f);
        if(Input.mouseScrollDelta.y < 0) _targetZoom -= new Vector3(1.2f,1.2f,1.2f);

        if (_targetZoom.x < 0) _targetZoom = new Vector3 (1f,1f,1f);

        float zoomSpeed = 5f;


        meshFilter.transform.localScale = Vector3.Lerp(meshFilter.transform.localScale,_targetZoom,Time.deltaTime * zoomSpeed); 
        //meshFilter.transform.localScale = Vector3.Lerp(meshFilter.transform.localScale,_targetZoom,Time.deltaTime * zoomSpeed);
        
        
    }

    public void resetZoom(){
        _targetZoom = new Vector3 (1f,1f,1f);
    }
}

