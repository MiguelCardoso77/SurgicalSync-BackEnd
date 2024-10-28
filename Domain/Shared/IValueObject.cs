namespace DDDNetCore.Domain.Shared
{
    public interface IValueObject<T>
    {
        T Value { get; }
    }
}