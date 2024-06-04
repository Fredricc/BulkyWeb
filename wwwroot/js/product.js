$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    DataTable = $('#tblData').DataTable({
        "ajax": url: '/admin/product/getall'},
        "columns": [
            { data: 'title', "width" : "25%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'price50', "width": "15%" },
            { data: 'price100', "width": "15%" }

    ]
    });
}