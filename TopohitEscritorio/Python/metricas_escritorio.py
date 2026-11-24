# =====================================================================
#  TOPOHIT · Métricas · Pantalla «Métricas» del escritorio (todos los jugadores)
#  El escritorio pide las partidas a la API (GET api/sesiones/todas), las guarda en un JSON y
#  ejecuta este script:
#      python metricas_escritorio.py <partidas.json> <carpeta_de_salida>
#  En la carpeta de salida deja:
#   - las gráficas en PNG;
#   - resultado.json con las tarjetas (número y título) y la lista de gráficas.
#  Sin textos de conclusiones (lo pide el PDF de Métricas).
#
#  Las métricas y las gráficas salen de las partidas REALES de Infinito.
#  El modelo de abandono necesita muchos jugadores: si todavía hay menos de 50 reales (o casi
#  todos vuelven o casi ninguno), se entrena con las sesiones simuladas (sesiones_simuladas.csv,
#  hechas con 02_simulacion.py) y la tarjeta «Modelo entrenado con» lo dice.
#
#  analisis.py es una copia de Topohit-Android-/metricas/03_analisis.py: si se cambia uno,
#  hay que cambiar el otro.
# =====================================================================

import os
import sys
import json

# Paso 1: Pandas - Las partidas reales que manda el escritorio
import pandas as pd
import analisis

MINIMO_JUGADORES_REALES = 50
MINIMO_POR_CLASE = 5          # jugadores que vuelven y que no, como mínimo, para poder entrenar


def cargar_reales(ruta_json, carpeta):
    with open(ruta_json, encoding="utf-8") as archivo:
        df = pd.DataFrame(json.load(archivo))
    df = df[df["modeId"] == 3]           # solo Infinito: Historia y Contrarreloj ya no existen
    df = df.rename(columns={"sessionId": "session_id", "userId": "user_id", "dateTime": "date_time",
                            "sessionLength": "session_length", "errorsMade": "errors_made",
                            "finalScore": "final_score", "nAttempts": "n_attempts",
                            "molesEscaped": "moles_escaped", "maxCombo": "max_combo",
                            "goldenHits": "golden_hits", "avgReactionMs": "avg_reaction_ms",
                            "helpClicks": "help_clicks"})
    # Se guarda en CSV para cargarlo igual que las simuladas (analisis.cargar)
    ruta_csv = os.path.join(carpeta, "partidas_reales.csv")
    df.to_csv(ruta_csv, index=False)
    return analisis.cargar(ruta_csv)


# Paso 2: Scikit-learn - ¿Hay bastantes jugadores reales para el modelo?
def hay_bastantes(jugadores):
    vuelven = int(jugadores["returning_player"].sum())
    no_vuelven = len(jugadores) - vuelven
    return (len(jugadores) >= MINIMO_JUGADORES_REALES and vuelven >= MINIMO_POR_CLASE
            and no_vuelven >= MINIMO_POR_CLASE)


# Paso 3: Guardar las gráficas y las tarjetas para el escritorio
NOMBRES = {
    "duracion": "Duración de las partidas",
    "puntuaciones": "Puntuaciones por dificultad",
    "errores_dificultad": "Errores y abandonos por dificultad",
    "intentos_puntuacion": "Intentos y puntuación",
    "actividad_diaria": "Actividad diaria (DAU)",
    "abandono_ayudas": "Abandono según las ayudas",
    "abandono_errores": "Abandono según los errores por minuto",
    "matriz_confusion": "Modelo: matriz de confusión",
    "curva_roc": "Modelo: curva ROC",
    "importancias": "Modelo: importancia de las variables",
    "arbol": "Modelo: árbol de decisión",
}


def tarjetas(metricas, resultado, origen):
    return [
        {"titulo": "Jugadores", "valor": str(metricas["player_count"])},
        {"titulo": "Partidas", "valor": str(metricas["sesiones"])},
        {"titulo": "Vuelven a jugar (retention)", "valor": str(metricas["retention_rate"]) + " %"},
        {"titulo": "Solo juegan una (churn)", "valor": str(metricas["churn_rate"]) + " %"},
        {"titulo": "Duración media", "valor": str(metricas["average_session_length"]) + " s"},
        {"titulo": "DAU medio", "valor": str(metricas["dau_medio"])},
        {"titulo": "MAU medio", "valor": str(metricas["mau_medio"])},
        {"titulo": "DAU / MAU", "valor": str(metricas["dau_mau"])},
        {"titulo": "Partidas abandonadas", "valor": str(metricas["partidas_abandonadas"]) + " %"},
        {"titulo": "Ayudas por partida", "valor": str(metricas["ayudas_por_partida"])},
        {"titulo": "Modelo · accuracy", "valor": str(resultado["accuracy"])},
        {"titulo": "Modelo · precision", "valor": str(resultado["precision"])},
        {"titulo": "Modelo · recall", "valor": str(resultado["recall"])},
        {"titulo": "Modelo · ROC-AUC", "valor": str(resultado["roc_auc"])},
        {"titulo": "Modelo entrenado con", "valor": origen},
    ]


def main(ruta_json, carpeta):
    os.makedirs(carpeta, exist_ok=True)
    df = cargar_reales(ruta_json, carpeta)
    jugadores = analisis.tabla_jugadores(df)
    metricas = analisis.metricas_derivadas(df, jugadores)

    if hay_bastantes(jugadores):
        resultado = analisis.entrenar_modelo(jugadores)
        origen = str(len(jugadores)) + " jugadores reales"
    else:
        ruta_simuladas = os.path.join(os.path.dirname(os.path.abspath(__file__)), "sesiones_simuladas.csv")
        simuladas = analisis.tabla_jugadores(analisis.cargar(ruta_simuladas))
        resultado = analisis.entrenar_modelo(simuladas)
        origen = str(len(simuladas)) + " jugadores simulados"

    graficas = []
    for nombre, fig in analisis.crear_graficas(df, jugadores, resultado):
        archivo = nombre + ".png"
        fig.savefig(os.path.join(carpeta, archivo), dpi=90, bbox_inches="tight")
        analisis.plt.close(fig)
        graficas.append({"titulo": NOMBRES[nombre], "archivo": archivo})

    datos = {"tarjetas": tarjetas(metricas, resultado, origen), "graficas": graficas}
    with open(os.path.join(carpeta, "resultado.json"), "w", encoding="utf-8") as archivo:
        json.dump(datos, archivo, ensure_ascii=False, indent=2)
    print("Métricas listas en " + carpeta)


# Esta parte es de información externa que no sale en los vídeos
# (sys.argv: el escritorio le pasa al script la ruta del JSON y la carpeta donde dejar las gráficas)
if __name__ == "__main__":
    main(sys.argv[1], sys.argv[2])
