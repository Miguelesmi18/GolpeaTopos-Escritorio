# Golpea Topos · App de escritorio (Topohit)

Panel para educadoras y administradores del juego Topohit. Windows Forms (.NET Framework 4.8) que se conecta
a la Web API ([ASP.NET-Core-Web-API-Topohit](https://github.com/TankistyT80/ASP.NET-Core-Web-API-Topohit)) con `HttpClient`.

## Pantallas
- **Login**: solo entran los roles `admin` y `educador`. Si la API no responde, ofrece un modo demo con datos de ejemplo.
- **Jugadores**: lista de usuarios. El admin puede crear, editar y eliminar; el educador solo puede ver.
- **Modos y niveles**: activar o desactivar los niveles del modo Historia.
- **Estadísticas**: General (aciertos, errores y tiempo de reacción por modo y nivel) y Seguimiento (avisos por niño).
- **Detalle del jugador**: tarjetas, gráfico e historial de partidas de cada usuario.

## Cómo arrancarla
1. Arranca la API (perfil http, puerto 5110). La dirección está en `Servicios/Api.cs` (`DIRECCION`).
2. Abre `TopohitEscritorio.sln` en Visual Studio y ejecuta.

## Cambios del 17/11/2026 y por qué
- **Las pantallas son formularios en vez de UserControls** (`FormJugadores`, `FormModos`, `FormEstadisticas`, `FormDetalleJugador`).
  Se ven igual: `FormPrincipal.MostrarPantalla` mete cada formulario en el panel de la derecha con `TopLevel = false`,
  `FormBorderStyle = None` y `Dock = Fill`, y lo abre con `Show()`. Los UserControls no los hemos visto en clase; los formularios sí.
- **Educadoras y admins también juegan, y sus partidas cuentan en las estadísticas** igual que las de un niño.
  Si no han jugado nunca, no salen en Seguimiento (`Calculos.SaleEnEstadisticas`).
- **Modos y niveles: Guardar solo envía los niveles que han cambiado** y, si algo falla, dice qué nivel.
- **Archivos borrados**: pantallas antiguas que ya no se usaban (`FormPartidas`, `FormEditarNivel`, `FormEditarContrarreloj`,
  `FormEditarInfinito`, `UcPanel`, `UcPartidas` y los UserControls antiguos) y `Modelos/FilaModo`, `FilaNivel` y `FilaRevision`.
- Las imágenes de `Imagenes/` (topo normal, Halloween y Navidad) son los avatares según la temática de cada niño.
  Las usa `Properties/Resources.resx`: si se borran, el proyecto no compila.
