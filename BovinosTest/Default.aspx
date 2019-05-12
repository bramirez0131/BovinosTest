<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BovinosTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bovinos Prueba</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="jumbotron jumbotron-fluid">
            <div class="container">
                <div class="row justify-content-md-center">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Filtro</label>
                            <asp:TextBox  TabIndex="1" runat="server" CssClass="form-control" ID="TxtFiltroBovino" placeholder="Ingrese el delimitador de los Bovinos" Text="B" />
                        </div>
                
                
                        <div class="form-group">
                            <label>Archivo Adjunto</label>
                            <asp:FileUpload CssClass="form-control-file" ID="FUArchivoPlano" AllowMultiple="false" runat="server" TabIndex="2" />
                        </div>
                
                
                        <asp:Button TabIndex="3" CssClass="btn btn-outline-success btn-lg" Text="Cargar" ID="btnCargarArchivo" OnClick="btnCargarArchivo_Click" runat="server" />    
                    </div>
                </div>           
            </div>
        </div>

        <!-- The Modal -->
        <div class="modal fade" id="ModalGeneral">
          <div class="modal-dialog">
            <div class="modal-content">

              <!-- Modal Header -->
              <div class="modal-header">
                <h4 class="modal-title">Mensaje del sistema de Información</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
              </div>

              <!-- Modal body -->
              <div class="modal-body">
                  <p class="Mensaje"></p>
              </div>

              <!-- Modal footer -->
              <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
              </div>

            </div>
          </div>
        </div>

        <script type="text/javascript">

            $(function () {
                $("#ModalGeneral").on('hidden.bs.modal', function () {
                    location.href = "/";
                });
            });

            function ModalGeneral(Mensaje){
                $(".Mensaje").text(Mensaje);
                $("#ModalGeneral").modal("show");
            }
        </script>

    </form>
</body>
</html>
