using AAMT;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    public Button loadSecondPanelButton;
    public Button loadMap1Button;
    public Button loadMap2Button;
    public Text text;

    private GameObject map1;
    private GameObject map2;

    void Start()
    {
        loadSecondPanelButton.onClick.AddListener(OnLoadSecondPanel);
        loadMap1Button.onClick.AddListener(OnLoadMap1);
        loadMap2Button.onClick.AddListener(OnLoadMap2);
    }

    private void OnLoadMap1()
    {
        if(map1 != null) return;
        if (map2 != null)
        {
            Destroy(map2);
            map2 = null;
        }
        AAMTManager.GetAssetsAsync(PathTools.Map1Path, (GameObject o) => { map1 = Instantiate(o); });
    }

    private void OnLoadMap2()
    {
        if(map2 != null) return;
        if (map1 != null)
        {
            Destroy(map1);
            map1 = null;
        }
        AAMTManager.GetAssetsAsync(PathTools.Map2Path, (GameObject o) => { map2 = Instantiate(o); });
    }

    private void OnLoadSecondPanel()
    {
        
        AAMTManager.GetAssetsAsync(PathTools.SecondPanel, (GameObject o) =>
        {
            Instantiate(o, transform.parent);
            if (map1 != null) Destroy(map1);
            if (map2 != null) Destroy(map2);
            map1 = null;
            map2 = null;
            Destroy(gameObject);
        });
    }
}