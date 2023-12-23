using CoreLayer.Repository;
using CoreLayer.Services;
using CoreLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ServiceGeneric<TEntitiy, TDto> : IServiceGeneric<TEntitiy, TDto> where TEntitiy : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntitiy> _genericRepository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntitiy> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entitiy)
        {
            var newEntit = ObjectMapper.Mapper.Map<TEntitiy>(entitiy);
            await _genericRepository.AddAsync(newEntit);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntit);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);

            if (product == null)
            {
                return Response<TDto>.Fail("id not found", 404, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntitiy = await _genericRepository.GetByIdAsync(id);

            if (isExistEntitiy == null)
            {
                return Response<NoDataDto>.Fail("id not found", 404, true);
            }

            _genericRepository.Remove(isExistEntitiy);

            await _unitOfWork.CommitAsync();
            // 204 Durum Kodu => No Content => Response body'sinde hiçbir data olmayacak.//
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto entitiy, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail("id not found", 404, true);
            }
            var updateEntity = ObjectMapper.Mapper.Map<TDto>(entitiy);
            await _unitOfWork.CommitAsync();
            // 204 Durum Kodu => No Content => Response body'sinde hiçbir data olmayacak.//
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntitiy, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),200);
        }
    }
}
