using MediAgenda.Application.Interfaces;
using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MediAgenda.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly MediContext context;

        public ValidationService(MediContext context)
        {
            this.context = context;
        }

        public async Task<TProperty?> GetPropertyById<T, IdType, TProperty>(string nameProperty, IdType id, string idPropertyName = "Id")
            where T : class
        {
            var query = context.Set<T>();
            var propertyValue = await query
                .Where(x => EF.Property<IdType>(x, idPropertyName).Equals(id))
                .AsNoTracking()
                .Select(x => EF.Property<TProperty>(x, nameProperty))
                .FirstOrDefaultAsync();

            return propertyValue;
        }

        public async Task<bool> ExistsProperty<T, TProperty>(string nameproperty, TProperty property)
            where T : class
        {
            var query = context.Set<T>();

            if (typeof(TProperty) == typeof(string))
            {
                string stringValue = ((string)(object)property).Trim().ToLower();
                return await query.AnyAsync(x =>
                    EF.Property<string>(x, nameproperty).Trim().ToLower() == stringValue
                );
            }

            var exist = await query.AnyAsync(x => EF.Property<TProperty>(x, nameproperty).Equals(property));
            return exist;
        }

        public async Task<bool> ExistsProperty<T, TProperty, TIdType>(string nameproperty, TProperty property, TIdType id)
            where T : class
        {
            var query = context.Set<T>();

            if (typeof(TProperty) == typeof(string))
            {
                string stringValue = ((string)(object)property).Trim().ToLower();

                return await query.AnyAsync(x =>
                    EF.Property<string>(x, nameproperty).Trim().ToLower() == stringValue
                    && !EF.Property<TIdType>(x, "Id").Equals(id)
                );
            }

            var exist = await query.AnyAsync(x => EF.Property<TProperty>(x, nameproperty).Equals(property) && !EF.Property<TIdType>(x, "Id").Equals(id));
            return exist;
        }

        public async Task<bool> ExistsPropertyAndOtherProperty<T, TProperty1, TProperty2>(string property1name, string property2name, TProperty1 P1, TProperty2 P2) 
            where T : class
        {
            var query = context.Set<T>();

            var exist = await query.AnyAsync(x => EF.Property<TProperty1>(x, property1name).Equals(P1) && EF.Property<TProperty2>(x, property2name).Equals(P2));
            return exist;
        }

        public async Task<bool> ExitsPropertyInSameId<T, TProperty, TIdType>(string nameproperty, TProperty property, string IdName, TIdType id)
            where T : class
        {
            var query = context.Set<T>();

            if (typeof(TProperty) == typeof(string))
            {
                string stringValue = ((string)(object)property).Trim().ToLower();
                return await query.AnyAsync(x =>
                    EF.Property<string>(x, nameproperty).Trim().ToLower() == stringValue
                    && EF.Property<TIdType>(x, IdName).Equals(id)
                );
            }

            var exist = await query.AnyAsync(x => EF.Property<TProperty>(x, nameproperty).Equals(property) && EF.Property<TIdType>(x, IdName).Equals(id));
            return exist;
        }

        public async Task<bool> ExistsEqualDateAndTime<T>(DateOnly date, TimeOnly start, TimeOnly end) where T : class
        {
            var query = context.Set<T>();
            var exist = await query.AnyAsync(x =>
                EF.Property<DateOnly>(x, "Date") == date &&
                (start <= EF.Property<TimeOnly>(x, "EndTime") && EF.Property<TimeOnly>(x, "StartTime") >= start)
            );

            return exist;
        }

        public async Task<bool> ExistsEqualDateAndTimeInDiferentId<T,TIdType>(DateOnly date, TimeOnly start, TimeOnly end, TIdType id) where T : class
        {
            var query = context.Set<T>();
            var exist = await query.AnyAsync(x =>
                EF.Property<DateOnly>(x, "Date") == date &&
                (start <= EF.Property<TimeOnly>(x, "EndTime") && EF.Property<TimeOnly>(x, "StartTime") >= start && !EF.Property<TIdType>(x, "Id").Equals(id))
            );

            return exist;
        }

        public async Task<bool> ExistsPropertyAndOtherPropertyExcludingId<T, TProperty1, TProperty2, TIdType>(string property1Name, string property2Name, TProperty1 P1, TProperty2 P2, TIdType idToExclude)
        where T : class
        {
            var query = context.Set<T>();

            if (typeof(TProperty1) == typeof(string))
            {
                string stringValue = ((string)(object)P1!).Trim().ToLower();
                return await query.AnyAsync(x =>
                    EF.Property<string>(x, property1Name).Trim().ToLower() == stringValue
                    && EF.Property<TProperty2>(x, property2Name).Equals(P2)
                    && !EF.Property<TIdType>(x, "Id").Equals(idToExclude)
                );
            }

            return await query.AnyAsync(x =>
                EF.Property<TProperty1>(x, property1Name).Equals(P1)
                && EF.Property<TProperty2>(x, property2Name).Equals(P2)
                && !EF.Property<TIdType>(x, "Id").Equals(idToExclude)
            );
        }

    }
}
