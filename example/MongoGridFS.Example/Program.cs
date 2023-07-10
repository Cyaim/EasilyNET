using EasilyNET.AutoDependencyInjection.Extensions;
using MongoGridFS.Example;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// �Զ�ע�����ģ��
builder.Services.AddApplication<AppWebModule>();

//���Serilog����
_ = builder.Host.UseSerilog((hbc, lc) =>
{
    const LogEventLevel logLevel = LogEventLevel.Information;
    _ = lc.ReadFrom.Configuration(hbc.Configuration)
          .MinimumLevel.Override("Microsoft", logLevel)
          .MinimumLevel.Override("System", logLevel)
          .Enrich.FromLogContext()
          .WriteTo.Async(wt =>
          {
              wt.Console();
              wt.Debug();
          });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) _ = app.UseDeveloperExceptionPage();

// ����Զ���ע���һЩ�м��.
app.InitializeApplication();
app.MapControllers();
app.Run();