﻿using System;
using System.Collections.Generic;
using System.Linq;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IFunctionService
    {
        Function Create(Function function);

        IEnumerable<Function> GetAll(string filter);

        IEnumerable<Function> GetAllWithPermission(string userId);

        IEnumerable<Function> GetAllWithParentId(string parentId);

        Function Get(string id);

        void Update(Function function);

        void Delete(string id);

        void Save();

        bool CheckExistedId(string id);
    }

    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FunctionService(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
        }


        public bool CheckExistedId(string id)
        {
            return _functionRepository.CheckContains(x => x.ID == id);
        }

        public Function Create(Function function)
        {
            return _functionRepository.Add(function);
        }

        public void Delete(string id)
        {
            var function = _functionRepository.GetSingleByCondition(x => x.ID == id);
            _functionRepository.Delete(function);
        }

        public Function Get(string id)
        {
            return _functionRepository.GetSingleByCondition(x => x.ID == id);
        }

        public IEnumerable<Function> GetAll(string filter)
        {
            var query = _functionRepository.GetMulti(x => x.Status);
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));
            return query.OrderBy(x => x.ParentId);
        }

        public IEnumerable<Function> GetAllWithParentId(string parentId)
        {
            return _functionRepository.GetMulti(x => x.ParentId == parentId);
        }

        public IEnumerable<Function> GetAllWithPermission(string userId)
        {
            var query = _functionRepository.GetListFunctionWithPermission(userId);
            return query.OrderBy(x => x.ParentId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Function function)
        {
            _functionRepository.Update(function);
        }
    }
}