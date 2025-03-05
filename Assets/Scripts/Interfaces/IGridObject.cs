public interface IGridObject<T> where T : class
{
    public T Data { get; set; }
    public void Init(T data, GameManager gameManager, Holder holder);
}