var builder = WebApplication.CreateBuilder(args);

// เพิ่มบริการสำหรับ Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ใช้งาน Swagger เมื่ออยู่ในโหมด Development หรือ Production 
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
