namespace NaiQiu.Framework.View
{
    public interface IView
    {
        void OnCreate(UIManager uiManager);

        void OnStart();

        void OnStop();

        void OnDestroy();
    }
}
