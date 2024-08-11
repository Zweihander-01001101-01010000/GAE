<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarRoles.aspx.cs" Inherits="GAE.Vistas.agregarRoles" %>

<!DOCTYPE html>

<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Subir respaldo</title>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap" rel="stylesheet">
    <link href="~/Estilos/app.css" type="text/css" rel="stylesheet"/>
           <style>
        .divisor-forma-personalizado {
            position: relative;
            width: 100%;
            overflow: hidden;
            line-height: 0;
            transform: rotate(180deg);
            z-index: 0;
            margin-top: 20px;
        }
        .divisor-forma-personalizado svg {
            display: block;
            width: calc(100% + 1.3px);
            height: 405px;
        }
        .divisor-forma-personalizado .relleno-forma {
            fill: #55bef5;
        }
        .table-container {
            padding: 20px;
            margin: 20px;
            border: 1px solid #ccc;
            background-color: #fff;
            position: relative;
            z-index: 1;
        }
        .table-container table {
            width: 100%;
            border-collapse: collapse;
        }
        .table-container th, .table-container td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: left;
        }
        .table-container th {
            background-color: #f2f2f2;
        }
        header nav ul {
            list-style-type: none;
            padding: 0;
            margin: 0;
            display: flex;
            gap: 20px;
        }
        header nav ul li {
            position: relative;
        }
        header nav ul li a {
            text-decoration: none;
            padding: 10px;
            display: block;
            color: #000;
        }
        header nav ul li.has-submenu .submenu {
            display: none;
            position: absolute;
            top: 100%;
            left: 0;
            background-color: white;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
            z-index: 1;
            min-width: 200px;
        }
        header nav ul li.has-submenu:hover .submenu {
            display: block;
            color: #5E58F8;
        }
        header nav ul li.has-submenu .submenu li {
            padding: 0;
        }
        header nav ul li.has-submenu .submenu li a {
            padding: 10px;
            color: black;
            white-space: nowrap;
            display: block;
        }
        header nav ul li.has-submenu .submenu li a:hover {
            color: #55bef5;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <header>
            <div class="cabecera-izquierda">
                <h1><a href="MenuJefatura.aspx">Jefatura</a></h1>
                <p><asp:Label ID="lblNombre" runat="server" Text="Esteban Mata Mena"></asp:Label></p>
            </div>
             <nav>
    <ul>
        <li class="has-submenu">
            <a href="SubirArchivo.aspx">Subir un nuevo archivo</a>
        </li>
        <li class="has-submenu">
            <a href="VisualizarArchivos.aspx">Visualizar tus archivos subidos</a>
        </li>
        <li class="has-submenu">
            <a href="SubirRespaldo.aspx">Respaldar un archivo.</a>
        </li>
        <li class="has-submenu">
            <a href="VistaRespaldos.aspx">Visualizar los archivos respaldado.</a>
        </li>
        <li class="has-submenu">
   <a href="#">Empleado</a>
   <ul class="submenu">
      <li><a href="agregarDepartamento.aspx">Añadir Departamento</a></li>
      <li><a href="agregarRoles.aspx">Añadir Role</a></li>
      <li><a href="agregarUsuario.aspx">Añadir Usuario</a></li>
   </ul>
</li>
        </ul>
</nav>
        </header>

            <section class="seccion-formulario">
       <div class="tarjeta-formulario-login" style="max-width: 700px; margin: 0 auto;">
          <h2><span class="fuente-gruesa">Crea un</span><br><span class="fuente-delgada">Role</span></h2>
          <p></p>
          <br />
          <div>
             <label class="fuente-delgada" for="cedula">Role</label>
             <asp:TextBox CssClass="inicioSesion" ID="Role" runat="server" placeholder="Ej.Jefatura"></asp:TextBox>
          </div>
          <br />
          <br />
          <br />
          <asp:Button ID="ingresar" runat="server" Text="Ingresar" CssClass="button" OnClick="ingresar_Click" />
          <br />
         
</div>
    </section>
    <div class="divisor-forma-personalizado">
        <svg data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1200 120" preserveAspectRatio="none">
            <path d="M321.39,56.44c58-10.79,114.16-30.13,172-41.86,82.39-16.72,168.19-17.73,250.45-.39C823.78,31,906.67,72,985.66,92.83c70.05,18.48,146.53,26.09,214.34,3V0H0V27.35A600.21,600.21,0,0,0,321.39,56.44Z" class="relleno-forma"></path>
        </svg>
    </div>
    <footer class="footer">
        <div>
            <h3>Sobre Nosotros</h3>
            <p>"Este gestor de archivos nos ofrece la gestión eficiente de archivos dentro de la empresa. Este software está diseñado para optimizar y mejorar la organización empresarial."</p>
        </div>
        <div>
            <h3>Tags</h3>
            <ul class="tag-list"></ul>
        </div>
        <div>
            <p>Contactenos:</p>
            <ul>
                <li>Email: email@gmail.com</li>
                <li>Phone: +1-800-123-4567</li>
            </ul>
        </div>
    </footer>
</form>
</body>
</html>