namespace TopohitEscritorio.Servicios
{
    // Cifrado de contraseñas con bcrypt (paquete NuGet BCrypt.Net-Next).
    // [No visto en clase]: lo pide el reto "Protecció dels usuaris".
    //
    // La API nueva compara el hash que enviamos con el que hay en la BD, letra a letra.
    // Para que salga EXACTAMENTE el mismo hash, todos (escritorio, tablet y BD) tienen que usar:
    //   - la versión $2a$
    //   - el coste 10 (el "10" del principio)
    //   - la misma sal (los 22 caracteres que van después de "$2a$10$")
    // Esa sal es la que tienen los usuarios de la BD: por ejemplo el hash de marc empieza por
    // "$2a$10$3s5iPBlBV39O4ZeUPrG23e". Si la cambiáis aquí, hay que cambiarla también en Android.
    public static class Cifrado
    {
        public const string SAL = "$2a$10$3s5iPBlBV39O4ZeUPrG23e";

        // Devuelve el hash de 60 caracteres que se guarda en USUARIO.password_hash
        public static string Hash(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena, SAL);
        }
    }
}
