using NaiQiu.Framework.Resource;
using NaiQiu.Framework.View;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    private ResourceManager resourceManager;

    public static SpriteManager SpriteManager;
    public static UIManager UIManager;
    public static DataManager DataManager;

    protected override void SingletonAwakened()
    {
        resourceManager = new(OnResourceLoaded);
    }
    
    private void OnResourceLoaded()
    {
        Debug.Log("OnResourceLoadCompleted");

        SpriteManager = new(resourceManager.SpriteAtlas);
        UIManager = new(resourceManager.Views);
        DataManager = new(resourceManager.Jsons);

        ToStart();
    }

    private void ToStart()
    {
        UIManager.Open("FunView");
    }

    public void ClearResource()
    {
        SpriteManager.Clear();
        UIManager.Clear();
    }
}
