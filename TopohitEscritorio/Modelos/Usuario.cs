using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopohitEscritorio.Modelos
{
    public class Usuario
    {
        // Propiedades automáticas (no hay email: los niños no tienen)
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }              // jugador, educador o admin
        public string Tematica { get; set; }         // Normal, Halloween o Navidad
        public int AvatarId { get; set; } = 1;       // CAMBIO 19/11: imagen de perfil (tabla AVATAR). 1 = topo normal feliz
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int NumPartidas { get; set; }         // no se guarda: se calcula para enseñarlo en la tabla
        public DateTime? UltimaPartida { get; set; }   // fecha de su última partida (null = no ha jugado)
        public string UltimaPartidaTexto { get; set; } // "Hoy 10:42", "Ayer 17:05", "02/11 18:44" o "—"

        // Constructor vacío: para crear un usuario nuevo desde el formulario
        public Usuario()
        {
        }

        // Constructor con parámetros: para crear un usuario con sus datos de golpe
        public Usuario(int id, string nombreUsuario, string contrasena, string rol, string tematica)
        {
            this.Id = id;
            this.NombreUsuario = nombreUsuario;
            this.Contrasena = contrasena;
            this.Rol = rol;
            this.Tematica = tematica;
            this.Observaciones = "";
            this.Activo = true;
            this.FechaRegistro = DateTime.Now;
        }
    }
}
