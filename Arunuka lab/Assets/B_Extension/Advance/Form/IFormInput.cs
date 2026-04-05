using System;

public interface IFormInput
{
    public event Action<object> OnUpdateState;
    public event Action<string> OnError;
    public bool CheckComplete();
    public object GetValue();
}