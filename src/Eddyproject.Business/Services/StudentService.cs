﻿using AutoMapper;
using Eddyproject.Business.Exceptions;
using Eddyproject.Business.Validation;
using Eddyproject.Common.Dtos.Student;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace Eddyproject.Business.Services;

public class StudentService : IStudentService
{
    private IGenericRepository<Student> StudentRepository { get; }
    public IGenericRepository<Budget> BudgetRepository { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    private IMapper Mapper { get; }
    public StudentCreateValidator StudentCreateValidator { get; }
    public StudentUpdateValidator StudentUpdateValidator { get; }

    public StudentService(IGenericRepository<Student> studentRepository,IGenericRepository<Budget> budgetRepository,
        IGenericRepository<Address> addressRepository,IMapper mapper,
        StudentCreateValidator studentCreateValidator, StudentUpdateValidator studentUpdateValidator)
    {
        StudentRepository = studentRepository;
        BudgetRepository = budgetRepository;
        AddressRepository = addressRepository;
        Mapper = mapper;
        StudentCreateValidator = studentCreateValidator;
        StudentUpdateValidator = studentUpdateValidator;
    }

    

    public async Task<int> CreateStudentAsync(StudentCreate studentCreate)
    {
        await StudentCreateValidator.ValidateAndThrowAsync(studentCreate);

        var address = await AddressRepository.GetByIdAsync(studentCreate.AddressId);

        if(address == null)
            throw new AddressNotFoundException(studentCreate.AddressId);

        var budget = await BudgetRepository.GetByIdAsync(studentCreate.BudgetId);
        if(budget == null)
            throw new BudgetNotFoundException(studentCreate.BudgetId);

        var entity = Mapper.Map<Student>(studentCreate);
        entity.Address = address;
        entity.Budget = budget;
        await StudentRepository.InsertAsync(entity);
        await StudentRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteStudentAsync(StudentDelete studentDelete)
    {
        var entity = await StudentRepository.GetByIdAsync(studentDelete.Id);

        if(entity == null)
            throw new StudentNotFoundException(studentDelete.Id);
        StudentRepository.Delete(entity);
        await StudentRepository.SaveChangesAsync();
    }

    public async Task<StudentDetails> GetStudentAsync(int id)
    {
        var entity = StudentRepository.GetByIdAsync(id, (student) => student.Address, (student) => student.Budget, (student) => student.Courses).Result;
        if (entity == null)
            throw new StudentNotFoundException(id);
        return Mapper.Map<StudentDetails>(entity);
    }

    public async Task<List<StudentList>> GetStudentsAsync(StudentFilter studentFilter)
    {
        Expression<Func<Student, bool>> firstNameFilter = (student) => studentFilter.FirstName == null ? true :
        student.FirstName.StartsWith(studentFilter.FirstName);

        Expression<Func<Student, bool>> lastNameFilter = (student) => studentFilter.LastName == null ? true :
        student.LastName.StartsWith(studentFilter.LastName);

        Expression<Func<Student, bool>> budgetFilter = (student) => studentFilter.Budget == null ? true :
        student.Budget.Amount.StartsWith(studentFilter.Budget);

        Expression<Func<Student, bool>> courseFilter = (student) => studentFilter.Course == null ? true :
        student.Courses.Any(course => course.Name.StartsWith(studentFilter.Course));

        var entities = await StudentRepository.GetFilteredAsync(new Expression<Func<Student, bool>>[]
        {
            firstNameFilter, lastNameFilter, budgetFilter, courseFilter
        }, studentFilter.Skip, studentFilter.Take,
        (student) => student.Address, (student) => student.Budget, (student) => student.Courses);

        return Mapper.Map<List<StudentList>>(entities);
    }

    public async Task UpdateStudentAsync(StudentUpdate studentUpdate)
    {
        await StudentUpdateValidator.ValidateAsync(studentUpdate);

        var address = await AddressRepository.GetByIdAsync(studentUpdate.AddressId);

        if (address == null)
            throw new AddressNotFoundException(studentUpdate.AddressId);

        var budget = await BudgetRepository.GetByIdAsync(studentUpdate.BudgetId);
        if (budget == null)
            throw new BudgetNotFoundException(studentUpdate.BudgetId);

        var existingStudent = StudentRepository.GetByIdAsync(studentUpdate.Id);

        if (existingStudent == null)
            throw new StudentNotFoundException(studentUpdate.Id);


        var entity = Mapper.Map<Student>(studentUpdate);
        entity.Address = address;
        entity.Budget = budget;
        StudentRepository.Update(entity);
        await StudentRepository.SaveChangesAsync();
    }
}
