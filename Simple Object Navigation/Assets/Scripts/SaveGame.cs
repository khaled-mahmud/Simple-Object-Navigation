using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class SaveGame : MonoBehaviour
{
    public GameObject cubePrefab, spherePrefab;
    private string saveCubeDataPath, saveSphereDataPath;


    void Start()
    {
        saveCubeDataPath = Path.Combine(Application.persistentDataPath, "cubeDataFile");
        saveSphereDataPath = Path.Combine(Application.persistentDataPath, "sphereDataFile");

        if(!Application.isPlaying){
            Load();
        }     
    }



    void Load()
    {
        GameObject[] iterableCubeObject = GameObject.FindGameObjectsWithTag("EditableCube");
        GameObject[] iterableSphereObject = GameObject.FindGameObjectsWithTag("EditableSphere");
        

        var cubeBinaryReader = new BinaryReader(File.Open(saveCubeDataPath, FileMode.Open));
        var sphereBinaryReader = new BinaryReader(File.Open(saveSphereDataPath, FileMode.Open));

        GameDataReader cubeDataReader = new GameDataReader(cubeBinaryReader);
        GameDataReader sphereDataReader = new GameDataReader(sphereBinaryReader);

        int cubeCount = cubeDataReader.ReadInt();
        for(int i = 0; i <iterableCubeObject.Length; i++)
        {
            DestroyImmediate(iterableCubeObject[i]);
        }
        for(int i = 0; i < cubeCount; i++)
        {
            Vector3 cubePosition = cubeDataReader.ReadVector3();
            Quaternion cubeRotation = cubeDataReader.ReadQuaternion();
            Vector3 cubeSize = cubeDataReader.ReadVector3();
            GameObject resizableCubeObject = Instantiate(cubePrefab, cubePosition, cubeRotation);
            resizableCubeObject.transform.localScale = cubeSize;
        }



        int sphereCount = sphereDataReader.ReadInt();
        for(int i = 0; i <iterableSphereObject.Length; i++)
        {
            DestroyImmediate(iterableSphereObject[i]);
        }
        for(int i = 0; i < sphereCount; i++)
        {
            Vector3 spherePosition = sphereDataReader.ReadVector3();
            Quaternion sphereRotation = sphereDataReader.ReadQuaternion();
            Vector3 sphereSize = sphereDataReader.ReadVector3();
            GameObject resizableObject = Instantiate(spherePrefab, spherePosition, sphereRotation);
            resizableObject.transform.localScale = sphereSize;
        }

    }
}
