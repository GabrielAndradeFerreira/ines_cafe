using ApiInescafe.Data;
using ApiInescafe.DTOs.Course;
using ApiInescafe.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInescafe.Services.Course;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;

    public CourseService(AppDbContext _context)
    {
        this._context = _context;
    }
    public async Task<ResponseModel<List<CourseModel>>> CreateCourseAsync(CourseCreateDto course)
    {
        var response = new ResponseModel<List<CourseModel>>();
        try
        {
            var Course = new CourseModel
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                ImageUrl = course.ImageUrl
            };
            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();
            var products = await _context.Courses.ToListAsync();
            response.Data = products;
            response.Message = "Curso criado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao criar os Cursos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<CourseModel>>> DeleteCourseAsync(int id)
    {
        var response = new ResponseModel<List<CourseModel>>();
        try
        {
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            if (string.IsNullOrEmpty(course?.Name))
            {
                response.Message = "Produto não encontrado.";
                return response;
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            var courses = await _context.Courses.ToListAsync();
            response.Data = courses;
            response.Message = "Curso deletado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao deletar os cursos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<CourseModel>>> GetAllCoursesAsync()
    {
        var response = new ResponseModel<List<CourseModel>>();
        try
        {
            var courses = await _context.Courses.ToListAsync();
            response.Data = courses;
            response.Message = "Todos os cursos foram recuperados com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os cursos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<CourseModel>> GetCourseByIdAsync(int id)
    {
        var response = new ResponseModel<CourseModel>();
        try
        {
            var course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            if (string.IsNullOrEmpty(course?.Name))
            {
                response.Message = "Curso não encontrado.";
                response.Status = false;
                return response;
            }
            response.Data = course;
            response.Message = "Curso recuperado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o curso: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<CourseModel>>> GetCourseBySubstringAsync(string substring)
    {
        var response = new ResponseModel<List<CourseModel>>();
        try
        {
            var courses = _context.Courses
                .Where(p => p.Name.Contains(substring))
                .ToListAsync();
            if (courses.Result.Count == 0)
            {
                response.Message = "Nenhum curso encontrado com o termo fornecido.";
                response.Status = true;
                return response;
            }
            response.Data = courses.Result;
            response.Message = "Cursos recuperados com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o curso: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<CourseModel>>> UpdateCourseAsync(CourseEditDto course)
    {
        var response = new ResponseModel<List<CourseModel>>();
        try
        {
            var existingCourse = await _context.Courses.FirstOrDefaultAsync(p => p.Id == course.Id);
            if (string.IsNullOrEmpty(existingCourse?.Name))
            {
                response.Message = "Produto não encontrado.";
                return response;
            }
            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Price = course.Price;
            existingCourse.ImageUrl = course.ImageUrl;
            _context.Courses.Update(existingCourse);
            await _context.SaveChangesAsync();
            var courses = await _context.Courses.ToListAsync();
            response.Data = courses;
            response.Message = "Curso atualizado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o curso: " + e.Message;
            response.Status = false;
            return response;
        }
    }
}