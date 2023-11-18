using EAgendaMedica.Aplicacao.ModuloAtividade;
using EAgendaMedica.Aplicacao.ModuloMedico;
using EAgendaMedica.Dominio.Compartilhado;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.Infra.Orm.Compartilhado;
using EAgendaMedica.Infra.Orm.ModuloAtividade;
using EAgendaMedica.Infra.Orm.ModuloMedico;
using EAgendaMedica.WebApi.Config;
using EAgendaMedica.WebApi.Config.AutoMapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace EAgendaMedica.WebApi {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<ApiBehaviorOptions>(config => {
                config.SuppressModelStateInvalidFilter = true;
            });

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, EAgendaDbContext>(optionsBuilder => {
                optionsBuilder.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<IRepositorioMedico, RepositorioMedicoOrm>();
            builder.Services.AddTransient<ServicoMedico>();

            builder.Services.AddTransient<IRepositorioAtividade, RepositorioAtividadeOrm>();
            builder.Services.AddTransient<ServicoAtividade>();

            builder.Services.AddTransient<ConfigurarMedicoMappingAction>();

            builder.Services.AddAutoMapper(config => {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<AtividadeProfile>();
            });




            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.MapType<TimeSpan>(() => new OpenApiSchema {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
                c.MapType<DateTime>(() => new OpenApiSchema {
                    Type = "string",
                    Example = new OpenApiString(DateTime.Now.ToString("23-11-2023"))

                });

            });

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

    public class TimeSpanToStringConverter : JsonConverter<TimeSpan> {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString());
        }
    }
}