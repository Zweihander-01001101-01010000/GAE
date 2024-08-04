<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuEmpleados.aspx.cs" Inherits="GAE.Vistas.MenuEmpleados" %>

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
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
               <header>
    <div class="cabecera-izquierda">
        <h1>Empleado</h1>
        <p><asp:Label ID="lblNombre" runat="server" Text="Josue David Masis"></asp:Label></p>
    </div>
         <nav>
    <ul>
        <li class="has-submenu">
            <a href="#">Visualizar tus archivos subidos</a>
        </li>
        <li class="has-submenu">
            <a href="#">Visualizar todos los archivos subidos</a>
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
                    <a href="VisualizarArchivos.aspx"><img src="../../Imagenes/icono-vision.png" alt="Subir un nuevo archivo" /></a>
                    <h3>Visualizar todos los archivos</h3>
                    <p>Puede visualizar todos los archivos que se han implementado entre todos los miembros de este software.</p>
                </div>
                <div class="option-menu full-width">
                    <a href="VisualizarArchivos.aspx"><img src="../../Imagenes/icono-nube.png" alt="Visualizar Respaldos" /></a>
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
