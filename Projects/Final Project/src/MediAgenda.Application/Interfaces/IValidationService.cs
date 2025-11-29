namespace MediAgenda.Application.Interfaces
{
    public interface IValidationService
    {
        Task<bool> ExistsEqualDateAndTime<T>(DateOnly date, TimeOnly start, TimeOnly end) where T : class;
        Task<bool> ExistsProperty<T, TProperty, TIdType>(string nameproperty, TProperty property, TIdType id)
            where T : class;
        Task<bool> ExistsProperty<T, TProperty>(string nameproperty, TProperty property)
            where T : class;
        Task<bool> ExitsPropertyInSameId<T, TProperty, TIdType>(string nameproperty, TProperty property, string IdName, TIdType id)
            where T : class;
    }
}