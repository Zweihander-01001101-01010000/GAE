<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuJefatura.aspx.cs" Inherits="GAE.Vistas.MenuJefatura" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ActivitySync</title>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap" rel="stylesheet"/>
    <link href="../../Estilos/app.css" rel="stylesheet"/>
    <style>
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
            color:#55bef5;
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
            color: #55bef5; /*color texto*/
        }
        .options-menu {
            display: flex;
            flex-wrap: wrap;
            justify-content: center; 
            gap: 20px; 
            max-width: 1200px; 
            margin: 0 auto; 
        }

        .option-menu {
            width: calc(50% - 20px); 
            margin-bottom: 20px; 
            text-align: center; 
        }

        .option-menu img {
            max-width: 100%; 
            height: auto;
        }

        @media (max-width: 768px) {
            .option-menu {
                width: 100%; 
            }
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
               <header>
    <div class="cabecera-izquierda">
        <h1>Jefatura</h1>
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
        </ul>
             <br /><br />
        <ul>
            <li class="has-submenu">
                <a href="agregarDepartamento.aspx">Agregar Departamento</a>
            </li>
            <li class="has-submenu">
                <a href="agregarRoles.aspx">Agregar Roles</a>
            </li>
            <li class="has-submenu">
                <a href="agregarUsuario.aspx">Agregar Usuario</a>
            </li>
        </ul>

</nav>
</header>
        <div class="container-menu">
            <div class="header-menu">
                <h1>Revisa nuestras opciones de gestión para el usuario</h1>
                <p>Ofrecemos varias opciones para nuestro gestor de archivos empresariales.</p>
            </div>
            <div class="options-menu">
                <div class="option-menu">
                <a href="SubirArchivo.aspx"><img src="../../Imagenes/icono-estadisticas.png" alt="Subir un nuevo archivo" /></a>
                <h3>Subir un nuevo archivo</h3>
                <p>Puede subir un nuevo archivo cual va ser guardado en las carpetas cuales tiene acceso.</p>
            </div>
                <div class="option-menu">
                    <a href="VisualizarArchivos.aspx"><img src="../../Imagenes/icono-documento.png" alt="Control de actividades" /></a>
                    <h3>Visualizar todos los archivos</h3>
                    <p>Puede visualizar todos los archivos que se han implementado entre todos los miembros de este software.</p>
                </div>
                <div class="option-menu full-width">
                    <a href="SubirRespaldo.aspx"><img src="../../Imagenes/icono-llave.png" alt="Subir Respaldo" /></a>
                    <h3>Puede subir un respaldo de un archivo</h3>
                    <p>Puede realizar un respaldo de un archivo deseado.</p>
                </div>
                <div class="option-menu full-width">
                    <a href="VistaRespaldos.aspx"><img src="../../Imagenes/icono-nube.png" alt="Visualizar Respaldos" /></a>
                    <h3>Visualizar Respaldos</h3>
                    <p>Puede ver los respaldos creados en el departamento.</p>
                </div>
                
            </div>
        </div>
<footer class="footer">
    <div>
        <h3>Sobre Nosotros</h3>
        <p>"Este gestor de archivos nos ofrece la gestión eficiente de archivos dentro de la empresa. Este software está 
            diseñado para optimizar y mejorar la organización empresarial."</p>
    </div>
    <div>
        <h3></h3>
        <ul class="tag-list">
        </ul>
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
