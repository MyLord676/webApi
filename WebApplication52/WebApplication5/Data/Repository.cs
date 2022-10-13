using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication5.Data
{
    internal class Repository
    {
        public static async Task<IEnumerable<TValue>> GetAllValues<TValue>(DbContext dataBase)
            where TValue : class
        {
            // Entity не отслеживает изменение объекта
            dataBase.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var TValueProperty = findProperty(typeof(DataBaseContext), typeof(DbSet<TValue>));
            var dbSet = TValueProperty.GetValue(dataBase) as DbSet<TValue>;
            if (dbSet == null)
                throw new Exception(string.Format("property DbSet<{0}> was null.", typeof(TValue).Name));
            return await dbSet.ToListAsync();

        }
        public static async Task<TValue?> GetValueById<TValue>(DbContext dataBase, params object[] keyValue)
            where TValue : class
        {
            // Entity не отслеживает изменение объекта 
            dataBase.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var TValueProperty = findProperty(typeof(DataBaseContext), typeof(DbSet<TValue>));
            var dbSet = TValueProperty.GetValue(dataBase) as DbSet<TValue>;
            if (dbSet == null)
                throw new Exception(string.Format("property DbSet<{0}> was null.", typeof(TValue).Name));
            return await dbSet.FindAsync(keyValue);
        }
        public static async Task DeleteValueById<TValue>(DbContext dataBase, params object[] keyValue)
            where TValue : class
        {
            var TValueProperty = findProperty(typeof(DataBaseContext), typeof(DbSet<TValue>));
            var dbSet = TValueProperty.GetValue(dataBase) as DbSet<TValue>;
            if (dbSet == null)
                throw new Exception(string.Format("property DbSet<{0}> was null.", typeof(TValue).Name));
            var obj = await dbSet.FindAsync(keyValue);
            dbSet.Remove(obj);
            await dataBase.SaveChangesAsync();
        }
        public static async Task DeleteValue<TValue>(DbContext dataBase, TValue value)
            where TValue : class
        {
            var TValueProperty = findProperty(typeof(DataBaseContext), typeof(DbSet<TValue>));
            var dbSet = TValueProperty.GetValue(dataBase) as DbSet<TValue>;
            if (dbSet == null)
                throw new Exception(string.Format("property DbSet<{0}> was null.", typeof(TValue).Name));
            dbSet.Remove(value);
            await dataBase.SaveChangesAsync();
        }
        public static async Task AddValue<TValue>(DbContext dataBase, TValue value)
            where TValue : class
        {
            var TValueProperty = findProperty(typeof(DataBaseContext), typeof(DbSet<TValue>));

            var dbSet = TValueProperty.GetValue(dataBase) as DbSet<TValue>;
            if (dbSet == null)
                throw new Exception(string.Format("property DbSet<{0}> was null.", typeof(TValue).Name));
            dbSet.Add(value);
            await dataBase.SaveChangesAsync();
        }
        public static async Task UpdateValue<TValue>(DbContext dataBase, TValue value)
            where TValue : class
        {
            dataBase.Entry(value).State = EntityState.Modified;
            await dataBase.SaveChangesAsync();
        }
        private static PropertyInfo findProperty(Type objType, Type propertyType)
        {
            PropertyInfo[] properties = objType.GetProperties();
            foreach (var property in properties)
                if (property.PropertyType == propertyType)
                    return property;
            throw new Exception(string.Format("property {0} not found.", propertyType.Name));
        }
    }
}