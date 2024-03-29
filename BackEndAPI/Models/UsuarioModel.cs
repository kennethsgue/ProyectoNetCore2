﻿using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Models
{
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int? Codigo { get; set; }
        public string Clave { get; set; }
        public byte[] Cv { get; set; }
    }
}