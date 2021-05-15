using UnityEngine;
using System.IO;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Object activePrefab;
    public RaycastHit hitinfo;
    public bool isTransformable;
    public Ray ray;
    public GameObject activeGameObject;
    public bool isRotable;
    GameObject[] iterableCubeObject;
    GameObject[] iterableSphereObject;
    private string saveCubeDataPath;
    private string saveSphereDataPath;
    public Vector3 prefabSize;
    


    void Awake(){
        if(instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        instance = this;

        saveCubeDataPath = Path.Combine(Application.persistentDataPath, "cubeDataFile");
        saveSphereDataPath = Path.Combine(Application.persistentDataPath, "sphereDataFile");
    }




    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitinfo, 100f) && !isRotable)
        {
            if(Input.GetMouseButtonDown(0))
            {
                InstantiateObject();
            }
        }

        
        if(Input.GetKeyDown(KeyCode.R) && activeGameObject != null)
        {
            InvokeRotationLogic();
        }        
    }




    void InstantiateObject()
    {
        if(hitinfo.transform.gameObject.tag == "Ground" && activePrefab)
        {
            GameObject resizableObject = Instantiate(activePrefab, hitinfo.point, Quaternion.identity) as GameObject;
            resizableObject.transform.localScale = prefabSize;
            isTransformable = false;
            if(activeGameObject != null){
                activeGameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
            }
        }        
    }




    void InvokeRotationLogic()
    {    
        if(isRotable)
        {
            isRotable = false;
            activeGameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else 
        {
            isRotable = true;
            activeGameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        
    }




    void OnApplicationQuit()
    {
        iterableCubeObject = GameObject.FindGameObjectsWithTag("EditableCube");
        iterableSphereObject = GameObject.FindGameObjectsWithTag("EditableSphere");

        Save();        
    }




    void Save()
    {
        var cubeBinaryWriter = new BinaryWriter(File.Open(saveCubeDataPath, FileMode.Create));
        var spherBinaryeWriter = new BinaryWriter(File.Open(saveSphereDataPath, FileMode.Create));

        GameDataWriter cubeDataWriter = new GameDataWriter(cubeBinaryWriter);
        GameDataWriter sphereDataWriter = new GameDataWriter(spherBinaryeWriter);

        cubeDataWriter.Write(iterableCubeObject.Length);
        for(int i = 0; i < iterableCubeObject.Length; i++)
        {
            cubeDataWriter.Write(iterableCubeObject[i].transform.localPosition);
            cubeDataWriter.Write(iterableCubeObject[i].transform.localRotation);
            cubeDataWriter.Write(iterableCubeObject[i].transform.localScale);
        }


        sphereDataWriter.Write(iterableSphereObject.Length);
        for(int i = 0; i < iterableSphereObject.Length; i++)
        {
            sphereDataWriter.Write(iterableSphereObject[i].transform.localPosition);
            sphereDataWriter.Write(iterableSphereObject[i].transform.localRotation);
            sphereDataWriter.Write(iterableSphereObject[i].transform.localScale);
        }

    }

}
