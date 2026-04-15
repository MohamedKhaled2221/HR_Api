using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.Domain.Entities;

namespace HR.Application.Employees.DTOs
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            
            CreateMap<CreateEmployeeDto, Employee>();

           
            CreateMap<UpdateEmployeeDto, Employee>();

          
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
