namespace core
{
    public interface IPuzzle<T,out TOut>
    {
        T LoadArgs();

        TOut Run(T input);
    }
}
