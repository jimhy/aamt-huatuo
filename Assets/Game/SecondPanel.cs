using AAMT;
using UnityEngine;
using UnityEngine.UI;

public class SecondPanel : MonoBehaviour
{
    public Button BackButton;

    void Start()
    {
        BackButton.onClick.AddListener(OnBack);
    }

    private void OnBack()
    {
        AAMTManager.GetAssetsAsync(PathTools.MainPanelPath, (GameObject o) =>
        {
            Instantiate(o, transform.parent);
            Destroy(gameObject);
        });
    }
}