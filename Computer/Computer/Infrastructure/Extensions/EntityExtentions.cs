using System;
using Computer.Common;
using Computer.Model.Models;
using Computer.Models.Computer;
using Computer.Models.System;

namespace Computer.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateComputer(this Model.Models.Computer computer, ComputerViewModel computerVm)
        {
            computer.ComputerId = computerVm.ComputerId;
            computer.ComputerCode = computerVm.ComputerCode;
            computer.ComputerName = computerVm.ComputerName;
            computer.ComputerDescription = computerVm.ComputerDescription;
            computer.ComputerTypeId = computerVm.ComputerTypeId;
            computer.ProducerTypeId = computerVm.ProducerTypeId;
            computer.DeparmentTypeId = computerVm.DeparmentTypeId;
            computer.IsBusyNow = computerVm.IsBusyNow;

            computer.CreatedDate = computerVm.CreatedDate;
            computer.CreatedBy = computerVm.CreatedBy;
            computer.UpdatedDate = computerVm.UpdatedDate;
            computer.UpdatedBy = computerVm.UpdatedBy;
            computer.MetaKeyword = computerVm.MetaKeyword;
            computer.MetaDescription = computerVm.MetaDescription;
            computer.Status = computerVm.Status;
        }

        public static void UpdateComputerType(this ComputerType computerType, ComputerTypeViewModel computerTypeVm)
        {
            computerType.ComputerTypeId = computerTypeVm.ComputerTypeId;
            computerType.ComputerTypeCode = computerTypeVm.ComputerTypeCode;
            computerType.ComputerTypeName = computerTypeVm.ComputerTypeName;
            computerType.ComputerTypeDescription = computerTypeVm.ComputerTypeDescription;

            computerType.CreatedDate = computerTypeVm.CreatedDate;
            computerType.CreatedBy = computerTypeVm.CreatedBy;
            computerType.UpdatedDate = computerTypeVm.UpdatedDate;
            computerType.UpdatedBy = computerTypeVm.UpdatedBy;
            computerType.MetaKeyword = computerTypeVm.MetaKeyword;
            computerType.MetaDescription = computerTypeVm.MetaDescription;
            computerType.Status = computerTypeVm.Status;
        }

        public static void UpdateDeparmentType(this DeparmentType computerType, DeparmentTypeViewModel computerTypeVm)
        {
            computerType.DeparmentTypeId = computerTypeVm.DeparmentTypeId;
            computerType.DeparmentTypeCode = computerTypeVm.DeparmentTypeCode;
            computerType.DeparmentTypeName = computerTypeVm.DeparmentTypeName;
            computerType.DeparmentTypeDescription = computerTypeVm.DeparmentTypeDescription;

            computerType.CreatedDate = computerTypeVm.CreatedDate;
            computerType.CreatedBy = computerTypeVm.CreatedBy;
            computerType.UpdatedDate = computerTypeVm.UpdatedDate;
            computerType.UpdatedBy = computerTypeVm.UpdatedBy;
            computerType.MetaKeyword = computerTypeVm.MetaKeyword;
            computerType.MetaDescription = computerTypeVm.MetaDescription;
            computerType.Status = computerTypeVm.Status;
        }

        public static void UpdateProducerType(this ProducerType computerType, ProducerTypeViewModel computerTypeVm)
        {
            computerType.ProducerTypeId = computerTypeVm.ProducerTypeId;
            computerType.ProducerTypeCode = computerTypeVm.ProducerTypeCode;
            computerType.ProducerTypeName = computerTypeVm.ProducerTypeName;
            computerType.ProducerTypeDescription = computerTypeVm.ProducerTypeDescription;

            computerType.CreatedDate = computerTypeVm.CreatedDate;
            computerType.CreatedBy = computerTypeVm.CreatedBy;
            computerType.UpdatedDate = computerTypeVm.UpdatedDate;
            computerType.UpdatedBy = computerTypeVm.UpdatedBy;
            computerType.MetaKeyword = computerTypeVm.MetaKeyword;
            computerType.MetaDescription = computerTypeVm.MetaDescription;
            computerType.Status = computerTypeVm.Status;
        }

        public static void UpdateComputerUsingHistory(this ComputerUsingHistory computerUsingHistory, ComputerUsingHistoryViewModel computerUsingHistoryVm)
        {
            computerUsingHistory.ComputerUsingHistoryId = computerUsingHistoryVm.ComputerUsingHistoryId;
            computerUsingHistory.ComputerId = computerUsingHistoryVm.ComputerId;
            computerUsingHistory.UserId = computerUsingHistoryVm.UserId;
            computerUsingHistory.StartTime = computerUsingHistoryVm.StartTime;
            computerUsingHistory.EndTime = computerUsingHistoryVm.EndTime;
            computerUsingHistory.StartTime = computerUsingHistoryVm.StartTime;
            computerUsingHistory.EndTime = computerUsingHistoryVm.EndTime;

            computerUsingHistory.CreatedDate = computerUsingHistoryVm.CreatedDate;
            computerUsingHistory.CreatedBy = computerUsingHistoryVm.CreatedBy;
            computerUsingHistory.UpdatedDate = computerUsingHistoryVm.UpdatedDate;
            computerUsingHistory.UpdatedBy = computerUsingHistoryVm.UpdatedBy;
            computerUsingHistory.MetaKeyword = computerUsingHistoryVm.MetaKeyword;
            computerUsingHistory.MetaDescription = computerUsingHistoryVm.MetaDescription;
            computerUsingHistory.Status = computerUsingHistoryVm.Status;
        }

        public static void UpdateFunction(this Function function, FunctionViewModel functionVm)
        {
            function.Name = functionVm.Name;
            function.DisplayOrder = functionVm.DisplayOrder;
            function.IconCss = functionVm.IconCss;
            function.Status = functionVm.Status;
            function.ParentId = functionVm.ParentId;
            function.Status = functionVm.Status;
            function.URL = functionVm.URL;
            function.ID = functionVm.ID;
        }
        public static void UpdatePermission(this Permission permission, PermissionViewModel permissionVm)
        {
            permission.RoleId = permissionVm.RoleId;
            permission.FunctionId = permissionVm.FunctionId;
            permission.CanCreate = permissionVm.CanCreate;
            permission.CanDelete = permissionVm.CanDelete;
            permission.CanRead = permissionVm.CanRead;
            permission.CanUpdate = permissionVm.CanUpdate;
        }

        public static void UpdateApplicationRole(this AppRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }

        public static void UpdateUser(this AppUser appUser, AppUserViewModel appUserViewModel, string action = "add")
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            if (!string.IsNullOrEmpty(appUserViewModel.BirthDay))
            {
                //DateTime dateTime = DateTime.ParseExact(appUserViewModel.BirthDay, "dd/MM/yyyy", new CultureInfo("vi-VN"));
                //appUser.BirthDay = dateTime;
                appUser.BirthDay = MethodHelper.StringToMonthDayYear(appUserViewModel.BirthDay);
            }

            appUser.Email = appUserViewModel.Email;
            appUser.Address = appUserViewModel.Address;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
            appUser.Gender = appUserViewModel.Gender == "True" ? true : false;
            appUser.Status = appUserViewModel.Status;
            appUser.Address = appUserViewModel.Address;
            appUser.Avatar = appUserViewModel.Avatar;
        }
    }
}