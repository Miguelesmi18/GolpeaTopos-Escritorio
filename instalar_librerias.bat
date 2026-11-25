@echo off
chcp 65001 >nul
setlocal
rem =====================================================================
rem  TOPOHIT · Escritorio · Instalar las librerías de Python
rem  La pantalla Métricas ejecuta Python con pandas, numpy, matplotlib y scikit-learn.
rem  Doble clic en este archivo y esperad a que diga «Listo».
rem =====================================================================

set "REQ=%~dp0TopohitEscritorio\Python\requirements.txt"

echo.
echo === 1. Comprobando que Python está instalado ===
python --version >nul 2>&1
if errorlevel 1 (
    echo No se encuentra Python. Instalando Python 3.13 con winget...
    winget install --id Python.Python.3.13 -e --accept-package-agreements --accept-source-agreements
    echo.
    echo Python instalado. CERRAD esta ventana y volved a abrir el .bat
    echo para que Windows encuentre el comando python.
    pause
    exit /b 1
)
python --version

echo.
echo === 2. Instalando las librerías (requirements.txt) ===
python -m pip install --upgrade pip
python -m pip install -r "%REQ%"
if errorlevel 1 (
    echo.
    echo ERROR: no se han podido instalar las librerías. Mirad el mensaje de arriba.
    pause
    exit /b 1
)

echo.
echo === 3. Comprobando que se cargan ===
python -c "import pandas, numpy, matplotlib, sklearn; print('pandas', pandas.__version__, '- matplotlib', matplotlib.__version__, '- scikit-learn', sklearn.__version__)"
if errorlevel 1 (
    echo.
    echo ERROR: alguna librería no se carga. Mirad el mensaje de arriba.
    pause
    exit /b 1
)

echo.
echo Listo: la pantalla Métricas del escritorio ya puede ejecutar Python.
pause
