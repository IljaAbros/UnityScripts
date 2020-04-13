public interface ICallbackList<T> {
    event System.Action<T> OnEdit;
    event System.Action<T> OnRemoved;
    event System.Action OnClear;
}
