$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url : '/admin/product/getall'},
        "columns": [
            { data: 'title', "width" : "20%" },
            { data: 'author', "width": "20%" },
            { data: 'category.name', "width": "10%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'price', "width": "10%" },
            { data: 'price50', "width": "10%" },
            { data: 'price100', "width": "10%" },
            {
                data: 'id',
                "render" : function (data) {
                    return `<div class="w-75 btn-group" role = "group" >
                    <a href = "/admin/product/upsert?id=${data}" class="btn btn-outline-primary mx-2" > <i class="bi bi-pencil-square"></i>Edit</a >
                    <a href = "" class="btn btn-outline-danger mx-2" > <i class="bi bi-trash-fill"></i>Delete</a ></div>`
                },
                "width": "10%"
            }

        ]
    });
}