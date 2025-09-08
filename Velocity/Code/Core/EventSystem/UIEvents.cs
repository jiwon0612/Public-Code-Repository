namespace Code.Core.EventSystem
{
    public static class UIEvents
    {
        public static readonly FadeCompleteEvent FadeComplete = new FadeCompleteEvent();
        public static readonly CurserEnableEvent CurserEnable = new CurserEnableEvent();
    }
    
    public class FadeCompleteEvent : GameEvent {}

    public class CurserEnableEvent : GameEvent
    {
        public bool enable;

        public CurserEnableEvent Init(bool enable)
        {
            this.enable = enable;
            return this;
        }
    }
}