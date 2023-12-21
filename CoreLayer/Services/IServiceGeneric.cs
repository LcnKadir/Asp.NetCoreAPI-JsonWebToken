using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IServiceGeneric<TEntitiy, TDto> where TEntitiy : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntitiy, bool>> predicate);

        Task<Response<TDto>> AddAsync(TEntitiy entitiy);

        Task<Response<NoDataDto>> Remove(TEntitiy entitiy);
        Task<Response<NoDataDto>> Update(TEntitiy entitiy);


    }
}
