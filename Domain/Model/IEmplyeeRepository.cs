﻿using PrimeiraAPI.Domain.DTOs;

namespace PrimeiraAPI.Domain.Model
{
    public interface IEmplyeeRepository
    {
        void Add(Employee employee);

        List<EmployeeDTO> Get(int pageNumber, int pageQuantity);
        Employee? Get(int id);
    }
}
