using AAMT;
using UnityEngine;
public class Game : MonoBehaviour
{
    public Transform Canvas;
    private void Start()
    {
        AAMTManager.GetAssetsAsync<GameObject>(PathTools.MainPanelPath,OnLoadMainPanelComplete);
    }

    private void OnLoadMainPanelComplete(GameObject obj)
    {
        Instantiate(obj, Canvas);
    }
}
