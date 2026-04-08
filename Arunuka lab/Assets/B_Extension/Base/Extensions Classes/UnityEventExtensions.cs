using UnityEngine.Events;


namespace B_Extensions.ClassesExtensions
{
    public static class UnityEventExtensions
    {
        public static UnityAction[] GetPersistentEventListeners(this UnityEvent unityEvent)
        {
            var listeners = new UnityAction[unityEvent.GetPersistentEventCount()];
            for (int i = 0; i < listeners.Length; i++)
            {
                listeners[i] = (UnityAction)unityEvent.GetPersistentTarget(i).GetType()
                    .GetMethod(unityEvent.GetPersistentMethodName(i))
                    .CreateDelegate(typeof(UnityAction), unityEvent.GetPersistentTarget(i));
            }
            return listeners;
        }
    }
}