﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;
using Computer.Model.Models;

namespace Computer.Service
{
    public interface IPermissionService
    {
        ICollection<Permission> GetByFunctionId(string functionId);

        ICollection<Permission> GetByUserId(string userId);

        void Add(Permission permission);

        void DeleteAll(string functionId);

        void SaveChange();
    }

    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            this._permissionRepository = permissionRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Permission permission)
        {
            _permissionRepository.Add(permission);
        }

        public void DeleteAll(string functionId)
        {
            _permissionRepository.DeleteMulti(x => x.FunctionId == functionId);
        }

        public ICollection<Permission> GetByFunctionId(string functionId)
        {
            return _permissionRepository
                .GetMulti(x => x.FunctionId == functionId, new string[] { "AppRole", "AppRole" }).ToList();
        }

        public ICollection<Permission> GetByUserId(string userId)
        {
            return _permissionRepository.GetByUserId(userId);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
