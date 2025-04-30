using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username  { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? Foto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DataAcesso { get; set; }
        
        // Significa que a propriedade não vai gerar uma coluna na tabela de dados de pois ficara como não mapeada
        [NotMapped]//DataAnnotations / System.ComponentModel.DataAnnotations.Schema

        public string PasswordString { get; set; } = string.Empty;

        public List<Personagem> Personagens { get; set; }
            = new List<Personagem>(); //using Systen.Collections,Generic;


        public string? Perfil { get; set; }
        public string? Email { get; set; } = string.Empty;
    }
}   // Criando uma lista de persomagens, um usuario podera ter mais de um personagem. 