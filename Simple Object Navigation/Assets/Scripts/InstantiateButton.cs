using UnityEngine;
using UnityEngine.UI;

public class InstantiateButton : MonoBehaviour
{
    public Object prefab;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ActivatePrefab);
    }

    void ActivatePrefab(){
        Player.instance.activePrefab = prefab;
    }
}
