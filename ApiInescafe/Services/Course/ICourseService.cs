using ApiInescafe.DTOs.Course;
using ApiInescafe.Models;

namespace ApiInescafe.Services.Course;

public interface ICourseService
{
    Task<ResponseModel<List<CourseModel>>> GetAllCoursesAsync();
    Task<ResponseModel<CourseModel>> GetCourseByIdAsync(int id);
    Task<ResponseModel<List<CourseModel>>> GetCourseBySubstringAsync(string substring);
    Task<ResponseModel<List<CourseModel>>> CreateCourseAsync(CourseCreateDto Course);
    Task<ResponseModel<List<CourseModel>>> UpdateCourseAsync(CourseEditDto Course);
    Task<ResponseModel<List<CourseModel>>> DeleteCourseAsync(int id);
    
}