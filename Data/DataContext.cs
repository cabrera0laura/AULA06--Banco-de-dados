using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using RpgApi.Utils;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        // *** no diretorio -->(configurações para utilizar)

        
        //informações de dos dados
        //ctor + Enter --> Atalho para criar o construtor  ------herança--->(:) utilizando outra coisa de outra classe(base)

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
             
        }
        public DbSet<Personagem> TB_PERSONAGENS {get; set;}//Nome a ser chamado no terminal

        public DbSet<Usuario> TB_USUARIOS { get; set; } 
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS"); //Nome a ser chmado la no banco de dados
            modelBuilder.Entity<Arma>().ToTable("TB_ARMAS");
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");

           
            
            //Relaciona
            modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Personagens)
            .WithOne(e => e.Usuario)
            .HasForeignKey(e => e.UsuarioId)
            .IsRequired(false);
            
            modelBuilder.Entity<Usuario>().HasData
            ( 
                //Colar a linha dos sete personagens da 
                // controller PersonagensExemplo aqui:
                new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro, UsuarioId =1},
                new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro, UsuarioId =1},
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo, UsuarioId =1 },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago, UsuarioId =1 },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro, UsuarioId =1 },
                new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo, UsuarioId =1 },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago, UsuarioId =1 }
            );

             modelBuilder.Entity<Arma>().HasData
            (
              new Arma() { Id = 1, Nome = "Arco e Flecha", Dano = 35},
              new Arma() { Id = 2, Nome = "Espada", Dano = 33},
              new Arma() { Id = 3, Nome = "Machado", Dano = 31},
              new Arma() { Id = 4, Nome = "Punho", Dano = 30},
              new Arma() { Id = 5, Nome = "Chicote", Dano = 34},
              new Arma() { Id = 6, Nome = "Foice", Dano = 33 },
              new Arma() { Id = 7, Nome = "Cajado", Dano = 32}
            );
            
            //**falta a tabela de Armas**

            // Inicio da criação do usuario padrão.
            Usuario user = new Usuario();
            Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[] salt);
            user.Id=1;
            User.Username= "UsuarioAdmin";
            user.PasswordString = string.Empty;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.Perfil = "Admin";
            user.Email = "lucas56hoseok@gmail.com";
            user.Latitude = -23.5200241;
            user.Longitude = -46.596498;
            modelBuilder.Entity<Usuario>().HasData(user);
            //Fim da criação do usuario padrão.

            //Define que se o perfil não for informado, o valor padrão será jogador
            modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("jogador");


        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveColumnType("varchar").HaveMaxLength(200);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings
            .Ignore(RelationalEventId.PendingModelChangesWarning));
        }


        

        
    }
}