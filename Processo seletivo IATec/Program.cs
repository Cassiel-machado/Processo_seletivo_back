using Microsoft.EntityFrameworkCore;
using Processo_seletivo_IATec.Aplicacao;
using Processo_seletivo_IATec.Infra;
using Processo_seletivo_IATec.Infra.Repositorio.PessoaRepositorio;
using Processo_seletivo_IATec.Infra.Repositorio.TelefoneRepositorio;

var builder = WebApplication.CreateBuilder(args);

//conecta no sqlServer pegando a classe de contexto
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer("Server=IATECBR-NB097\\SQLEXPRESS;Database=ProcessoSeletivo;Trusted_Connection=True;"));
//foi utilizado uma string de conexao para conectar no sqlserver

//Adiciona o servico de pessoa e telefone
builder.Services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
builder.Services.AddScoped<ITelefoneRepositorio, TelefoneRepositorio>();

//fazem parte do processo de injecao de dependencia    
builder.Services.AddScoped<PessoaServico>();
builder.Services.AddScoped<TelefoneServico>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
