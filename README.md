# Golpea Topos · App de escritorio (Topohit)

Panel para educadoras y administradores del juego Topohit. Windows Forms (.NET Framework 4.8) que se conecta
a la Web API ([ASP.NET-Core-Web-API-Topohit](https://github.com/TankistyT80/ASP.NET-Core-Web-API-Topohit)) con `HttpClient`.

## Pantallas
- **Login**: solo entran los roles `admin` y `educador`. Si la API no responde, ofrece un modo demo con datos de ejemplo.
- **Jugadores**: lista de usuarios. El admin puede crear, editar y eliminar; el educador solo puede ver.
- **Dificultades**: vidas, aceleración y puntos para desbloquear cada dificultad de Infinito.
- **Estadísticas**: General (aciertos, errores y tiempo de reacción por modo y nivel) y Seguimiento (avisos por niño).
- **Detalle del jugador**: tarjetas, gráfico e historial de partidas de cada usuario.
- **Métricas**: métricas de todos los jugadores, gráficas y modelo de abandono, calculados con Python.

## Cómo arrancarla
1. Arranca la API (perfil http, puerto 5110). La dirección está en `Servicios/Api.cs` (`DIRECCION`).
2. Abre `TopohitEscritorio.sln` en Visual Studio y ejecuta.

## Cambios del 24/11/2026 (2) · pantalla Métricas (reto «Mètriques dels jocs»)

- **Barra lateral → Métricas** (`FormMetricas`). Enseña las métricas de **todos** los jugadores de Infinito y el modelo
  que predice si un niño volverá a jugar. Las de cada niño están en su tablet (Perfil → Mis estadísticas).
- Cómo funciona (`Servicios/Metricas.cs`):
  1. Pide las partidas a la API (`GET api/sesiones/todas`) y las guarda en un JSON en la carpeta temporal.
  2. Ejecuta `Python/metricas_escritorio.py`, que se copia junto al `.exe`.
  3. El script deja 11 gráficas en PNG y `resultado.json` con 15 tarjetas.
  4. Haciendo clic en una gráfica se ve en grande.
- `Python/analisis.py` es una **copia** de `Topohit-Android-/metricas/03_analisis.py`: si se cambia uno, hay que
  cambiar el otro.
- **Datos del modelo:** las métricas y gráficas salen de las partidas **reales**. El modelo necesita muchos jugadores:
  mientras haya menos de 50 reales, se entrena con las 400 sesiones simuladas (`Python/sesiones_simuladas.csv`).
  La tarjeta «Modelo entrenado con» dice cuáles se han usado.
- **Hace falta Python** en el ordenador, con las librerías del proyecto:

  ```
  cd TopohitEscritorio/Python
  python -m pip install -r requirements.txt
  ```

  Si en vuestro PC Python no se llama `python` (por ejemplo `py`), cambiad `PYTHON` en `Servicios/Metricas.cs`.
  Si falta Python o alguna librería, la pantalla lo dice.
- Archivos nuevos:
  - `Pantallas/FormMetricas.cs` y `FormMetricas.Designer.cs`;
  - `Servicios/Metricas.cs`;
  - `Modelos/ResultadoMetricas.cs`;
  - la carpeta `Python/`.

  También cambian `Api.GetTodasSesionesJson` y el botón **Métricas** de `FormPrincipal`.

## Cambios del 24/11/2026 · solo modo Infinito, puntos de desbloqueo y bloqueos del tutor
- El juego **solo tiene Infinito**. La pantalla «Modos y niveles» pasa a ser **Dificultades** (`FormModos`): una tabla
  con Fácil, Normal y Difícil donde se cambian las **vidas**, la **aceleración** y los **puntos para abrir** cada una
  (los que hay que conseguir en la anterior). La columna «Se abre con» lo explica. Fácil siempre está abierta.
  Guardar llama a `PUT api/modos/infinito/{nombre}` solo con las que han cambiado.
- **Ficha del jugador → «Bloquear dificultades»** (`FormBloqueos`, con `ShowDialog`): una casilla por dificultad,
  el motivo que verá el niño y cómo está ahora (abierta, le faltan X puntos, bloqueada por el tutor).
  Usa `GET/PUT/DELETE api/jugadores/{id}/bloqueos/{dificultad}`.
- Necesita la API del 24/11 y `07_solo_infinito.sql`. Las estadísticas siguen enseñando las partidas antiguas de
  Historia y Contrarreloj (están en la base de datos).
- Archivos nuevos: `Pantallas/FormBloqueos.cs` (+ `.Designer.cs`), `Modelos/EstadoDificultad.cs`, `Modelos/BloqueoPeticion.cs`.

## Cambios del 19/11/2026 · avatar de cada usuario
- **Cada usuario tiene su avatar** (tabla `AVATAR`, columna `USUARIO.avatar_id`). Antes la imagen salía de la temática.
  Necesita la API nueva (`GET api/modos/avatares` y `avatarId` en `api/usuarios`) y `06_avatares.sql` en la BD.
- **Jugadores** y **Detalle del jugador** enseñan su avatar (`Ui.Avatar(avatarId)`).
- **Crear / editar usuario**: debajo de la contraseña está la fila con los 12 avatares; el elegido se marca en naranja
  y se envía como `avatarId` en el POST o el PUT. Las imágenes se crean desde el código (`CrearAvatares`), porque la lista viene de la API.
- Los 12 PNG están en `Imagenes/Avatares/` (copia de los de Android, con el mismo nombre que `AVATAR.recurso`)
  y se copian junto al .exe al compilar. Si falta alguno, sale el topo normal.
- El avatar **no depende de la temática** del usuario: cualquiera puede elegir cualquiera (solo cambia el estilo del dibujo).
- Si la API todavía no tiene `api/modos/avatares`, o en el modo demo, se usan los 12 de siempre (`Datos.AvataresPorDefecto`).

## Cambios del 17/11/2026 y por qué
- **Las pantallas son formularios en vez de UserControls** (`FormJugadores`, `FormModos`, `FormEstadisticas`, `FormDetalleJugador`).
  Se ven igual: `FormPrincipal.MostrarPantalla` mete cada formulario en el panel de la derecha con `TopLevel = false`,
  `FormBorderStyle = None` y `Dock = Fill`, y lo abre con `Show()`. Los UserControls no los hemos visto en clase; los formularios sí.
- **Educadoras y admins también juegan, y sus partidas cuentan en las estadísticas** igual que las de un niño.
  Si no han jugado nunca, no salen en Seguimiento (`Calculos.SaleEnEstadisticas`).
- **Modos y niveles: Guardar solo envía los niveles que han cambiado** y, si algo falla, dice qué nivel.
- **Archivos borrados**: pantallas antiguas que ya no se usaban (`FormPartidas`, `FormEditarNivel`, `FormEditarContrarreloj`,
  `FormEditarInfinito`, `UcPanel`, `UcPartidas` y los UserControls antiguos) y `Modelos/FilaModo`, `FilaNivel` y `FilaRevision`.
- Las imágenes de `Imagenes/` (topo normal, Halloween y Navidad) eran los avatares según la temática de cada niño.
  Ahora solo se usan si falta un PNG de `Imagenes/Avatares/`. Las usa `Properties/Resources.resx`: si se borran, el proyecto no compila.
