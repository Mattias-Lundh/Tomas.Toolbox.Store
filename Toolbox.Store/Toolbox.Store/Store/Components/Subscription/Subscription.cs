using Toolbox.Store;

public class Subscription
{
    private Store.Callback _callback;    
    public Subscription(Store.Callback callback)
    {
        _callback = callback;
    }

    public Guid EntityId { get; set; }    
    public string PropName { get; set; }

    public void Invoke(object preUpdate, object postUpdate)
    {
        _callback(preUpdate, postUpdate);
    }
}