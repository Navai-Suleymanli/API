namespace Eddyproject.Common.Dtos.Student;

public record StudentFilter(string? FirstName, string? LastName, string? Budget, string? Course, int? Skip, int? Take);