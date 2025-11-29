using MediAgenda.Application.Interfaces;
using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly MediContext context;

        public ValidationService(MediContext context)
        {
            this.context = context;
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


    }
}
