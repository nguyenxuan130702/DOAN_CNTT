using API_DOAN_Core.DTOs;
using API_DOAN_Core.Entities;
using API_DOAN_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace API_DOAN_Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        IBaseRepository<T> _repository;
        
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        private void SetNewId(T entity)
        {
            var className = typeof(T).Name;
            var prop = typeof(T).GetProperty($"{className}Id");
            if (prop != null && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid)))
            {
                prop.SetValue(entity, Guid.NewGuid());
            }
        }
        private void SetNewCode(T entity)
        {
            AutoGenerateCode g = new AutoGenerateCode();
            var className = typeof(T).Name;
            var prop = typeof(T).GetProperty($"{className}Code");
            string code = g.GenerateCode(5); 
            if (prop != null && prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, code);
            }
        }
        public DOANServiceResult DeleteService(string code)
        {
            //xu ly nghiep vu 
            ValidateUpdateDelete(code);

            var res = _repository.Delete(code);
            return new DOANServiceResult { Success = true, Data = res };
            //throw new NotImplementedException();
        }

        public DOANServiceResult InsertService(T service)
        {
            //Tự sinh id mới cho đối tượng 
            SetNewId(service);
            SetNewCode(service);    
            //xu ly nghiep vu 
            ValidateObject(service);

            ////sau khi da hop lệ thì thêm mới vào database. 
            var res = _repository.Insert(service);
            ProcessAfterSave(service);
            return new DOANServiceResult
            {
                Success = true,
                Data = res
            };
            //throw new NotImplementedException();
        }

        public DOANServiceResult UpdateService(T service)
        {
            //xu ly nghiep vu 
            //kiem tra xu ly khong ton tai ma khach hang 
            var props = service.GetType().GetProperties();
            var value = props[1].GetValue(service);

            ValidateUpdateDelete(value.ToString());
            var res = _repository.Update(service);
            return new DOANServiceResult { Success = true, Data = res };
            //throw new NotImplementedException();
        }


        protected virtual void ValidateUpdateDelete(string code) { }

        protected virtual void ValidateObject(T entity) { }
        protected virtual void ProcessAfterSave(T entity) { }

        public DOANServiceResult DeleteAnyService(string[] codes)
        {
            //xu ly nghiep vu 
            foreach (var code in codes)
            {
                ValidateUpdateDelete(code);
            }

            var res = _repository.DeleteAny(codes);
            return new DOANServiceResult { Success = true, Data = res };
        }

        public DOANServiceResult Page(int page, int pageSize)
        {
            var res = _repository.GetPage(page, pageSize);
            return new DOANServiceResult { Success = true, Data = res };
        }
    }
}
