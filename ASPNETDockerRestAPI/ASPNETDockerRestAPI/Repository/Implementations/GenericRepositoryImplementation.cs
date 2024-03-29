﻿using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Models.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ASPNETDockerRestAPI.Repository.Generic.Implementations
{
    public class GenericRepositoryImplementation<T>(MySqlContext dbContext) : IGenericRepository<T> where T : BaseModel
    {
        private readonly DbSet<T> _dataset = dbContext.Set<T>();

        public T? Create(T? entity)
        {
            if (entity is null)
            {
                return null;
            }

            try
            {
                _dataset.Add(entity);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                Log.Error($"There was an error creating entity: {entity}");
                throw;
            }

            return entity;
        }

        public List<T> FindAll()
        {
            return [.. _dataset];
        }

        public T? FindById(long id) => dbContext.Find<T>(id);

        public T? Update(T? entity)
        {
            if (entity is null)
            {
                return null;
            }

            try
            {
                _dataset.Update(entity);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                Log.Error($"There was an error updating entity: {entity}");
                throw;
            }

            return entity;
        }

        public void Delete(long id)
        {
            try
            {
                var entity = FindById(id)!;

                dbContext.Remove(entity);
            }
            catch (Exception)
            {
                Log.Error($"There was an error deleting entity with id {id}");
                throw;
            }
        }

        public List<T> FindAllPaged(string query)
        {
            return [.. _dataset.FromSqlRaw(query)];
        }

        public int GetCount(string query)
        {
            using var connection = dbContext.Database.GetDbConnection();
            using var command = connection.CreateCommand();

            connection.Open();
            command.CommandText = query;
            var result = command.ExecuteScalar()?.ToString();

            return string.IsNullOrWhiteSpace(result) ? 0 : int.Parse(result);
        }
    }
}
