
var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblArticulos").DataTable({
        //CUERPO DEL DATATABLE
        "ajax": {
            "url": "/admin/Articulos/GetAll",//RUTA DE DONDE OBTIENE LOS DATOS
            "type": "GET",//COMO VAMOS A CONSULTAR DATOS ES DE TIPO GET
            "datatype": "json"
        },
        "columns": [
            //CAMPOS DE LA ENTIDAD
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "25%" },
            { "data": "categoria.nombre", "width": "25%" },
            { "data": "fechaCreacion", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    //CONCATENAR DE UNA FORMA MAS SIMPLE - template literals `COMILLAS INVERTIDAS` 
                    return `<div class="text-center">
                            <a href='/Admin/Articulos/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-edit'></i> Editar
                            </a>
                            &nbsp;
                            <a onclick=Delete("/Admin/Articulos/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                            <i class='fas fa-trash-alt'></i> Borrar
                            </a>
                            `;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}

//UTILIZANDO SWEETALERT
function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#FF1D10",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {//VALIDAMOS QUE SI SE BORRO
                if (data.success) {
                    toastr.success(data.message);//UTILIZANDO TOASTR
                    dataTable.ajax.reload();//RECARGAR
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
