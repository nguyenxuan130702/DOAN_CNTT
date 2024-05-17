using API_DOAN_Core.Interface;
using API_DOAN_Core.Services;
using API_DOAN_Infrastructure.Interface;
using API_DOAN_Infrastructure.Repository;
using API_DOAN_Infrastructure.DOANDatabaseContext;
using API_DOAN_Core.Exception;
using API_DOAN_Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Features;
using API_DOAN_Core.AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();

builder.Services.AddScoped<INewRepository, NewRepository>();
builder.Services.AddScoped<INewService, NewService>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<IRadioRepository, RadioRepository>();
builder.Services.AddScoped<IRadioService, RadioService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
builder.Services.AddScoped<IStudentAnswerService, StudentAnswerServicecs>();

builder.Services.AddScoped<IVAccoutNameRepository, ViewAccoutNameRepository>();
builder.Services.AddScoped<IVAccoutNameService, AccoutNameService>();

builder.Services.AddScoped<ICourseDaiSoRepository, CourseDaiSoRepository>();
builder.Services.AddScoped<ICourseDaiSoService, CourseDaiSoService>();

builder.Services.AddScoped<ILessonCourseRepository, LessonCourseRepository>();
builder.Services.AddScoped<ILessonCourseService, LessonCourseService>();

builder.Services.AddScoped<IQuestionLessonRepository, QuestionLessonRepository>();
builder.Services.AddScoped<IQuestionLessonService, QuestionLessonService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IThreadsRepository, ThreadsRepository>();
builder.Services.AddScoped<IThreadsService, ThreadsService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ICategoryThreadRepository, CategoryThreadRepository>();
builder.Services.AddScoped<ICategoryThreadService, CategoryThreadService>();

builder.Services.AddScoped<IThreadsCommentRepository, ThreadsCommentRepository>();
builder.Services.AddScoped<IThreadsCommentService, ThreadsCommentService>();

builder.Services.AddScoped<ICommentUserRepository, CommentUserRepository>();
builder.Services.AddScoped<ICommentUserService, CommentUserService>();

builder.Services.AddScoped<IThreadUserRepository, ThreadUserRepository>();
builder.Services.AddScoped<IThreadUserService, ThreadUserService>();

builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
builder.Services.AddScoped<IScoreService, ScoreService>();

builder.Services.AddScoped<IVideo_CourseRepository, Video_CourseRepository>();
builder.Services.AddScoped<IVideo_CourseService, Video_CourseService>();

builder.Services.AddScoped<IVideo_QuestionRepository, Video_QuestionRepository>();
builder.Services.AddScoped<IVideo_QuestionService, Video_QuestionService>();

builder.Services.AddScoped<ILearningRepository, LearningRepository>();
builder.Services.AddScoped<ILearningService, LearningService>();

builder.Services.AddScoped<IVideoQuestionRepository, VideoQuestionRepository>();
builder.Services.AddScoped<IVideoQuestionService, VideoQuestionService>();

builder.Services.AddScoped<IVideoCourseRepository, VideoCourseRepository>();

builder.Services.AddScoped<IScoreViewRepository, ScoreViewRepository>();
builder.Services.AddScoped<IScoreViewService, ScoreViewService>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

builder.Services.AddScoped<ITeacher_CourseRepository, Teacher_CourseRepository>();
builder.Services.AddScoped<ITeacher_CourseService, Teacher_CourseService>();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();


builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();


builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<ISubmitRepository, SubmitRepository>();
builder.Services.AddScoped<ISubmitService, SubmitService>();

builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IResultService, ResultService>();

builder.Services.AddScoped<IViewTeacherCourseRepository, TeacherCourseRepository>();

builder.Services.AddScoped<IViewUserEnrollmentRepository, UserEnrollmentRepository>();

builder.Services.AddScoped<IViewClassAssignmentRepository, Class_AssignmentRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IDOANDbContext, MariaDbContext>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
//cau hinh lai bo nho request body asp.net 
//builder.Services.Configure<FormOptions>(x =>
//{
//    x.ValueLengthLimit = int.MaxValue;
//    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<HandleExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
