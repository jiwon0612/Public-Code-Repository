namespace Code.Core.EventSystem
{
    public static class SceneEvents
    {
        public static readonly SceneStartEvent SceneStart = new SceneStartEvent();
        public static readonly SceneChangeEvent SceneChange = new SceneChangeEvent();
    }
    
    public class SceneStartEvent : GameEvent {}

    public class SceneChangeEvent : GameEvent
    {
        public int sceneIndex;
        public SceneChangeEvent Init(int sceneIndex)
        {
            this.sceneIndex = sceneIndex;
            return this;
        }
    }
}