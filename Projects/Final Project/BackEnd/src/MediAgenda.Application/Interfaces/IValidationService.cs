namespace MediAgenda.Application.Interfaces
{
    public interface IValidationService
    {
        Task<bool> ExistsEqualDateAndTime<T>(DateOnly date, TimeOnly start, TimeOnly end) where T : class;
        Task<bool> ExistsEqualDateAndTimeInDiferentId<T, TIdType>(DateOnly date, TimeOnly start, TimeOnly end, TIdType id) where T : class;
        Task<bool> ExistsProperty<T, TProperty, TIdType>(string nameproperty, TProperty property, TIdType id)
            where T : class;
        Task<bool> ExistsProperty<T, TProperty>(string nameproperty, TProperty property)
            where T : class;
        Task<bool> ExitsPropertyInSameId<T, TProperty, TIdType>(string nameproperty, TProperty property, string IdName, TIdType id)
            where T : class;
        Task<bool> ExistsPropertyAndOtherProperty<T, TProperty1, TProperty2>(string property1name, string property2name, TProperty1 P1, TProperty2 P2)
            where T : class;
        Task<TProperty?> GetPropertyById<T, IdType, TProperty>(string nameProperty, IdType id, string idPropertyName = "Id")
            where T : class;
        Task<bool> ExistsPropertyAndOtherPropertyExcludingId<T, TProperty1, TProperty2, TIdType>(string property1Name, string property2Name, TProperty1 P1, TProperty2 P2, TIdType idToExclude)
        where T : class;
    }
}