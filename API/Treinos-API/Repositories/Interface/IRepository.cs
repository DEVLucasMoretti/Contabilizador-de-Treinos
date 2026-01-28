using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetByPeriodOfTime(DateTime InitialDate, DateTime lastDate);
        Task Add(T value);
        Task<bool> Update(T value);
        Task<bool> Delete(int id);
    }
}
