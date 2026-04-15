using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Application;
using HR.Application.Employees.DTOs;
using HR.Domain.Entities;
using HR.Domain.Repositories;
using HR.Domain.Services;
using Microsoft.EntityFrameworkCore;


namespace HR.Infrastructure.Services
{

    #region Employee Service
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

       
        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

    
        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _unitOfWork.Employees.CreateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

       
        public async Task<EmployeeDto> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("الموظف غير موجود");

            _mapper.Map(dto, employee);
            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        
        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Employees.ExistsAsync(id))
                throw new KeyNotFoundException("الموظف غير موجود");

            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    } 
    #endregion
}



 