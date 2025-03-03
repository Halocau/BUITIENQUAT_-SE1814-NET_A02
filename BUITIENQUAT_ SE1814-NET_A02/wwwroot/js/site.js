

$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("LoadCustomer", function () {
        LoadProdData();
    });
});

function LoadProdData() {
    var tr = '';
    $.ajax({
        url: '/Customers',
        method: 'GET',
        success: (result) => {
            $.each(result, (i, v) => {
                tr += `<tr>
                    <td>${v.CustomerId}</td>
                    <td>${v.CustomerName}</td>
                    <td>${v.Address}</td>
                    <td>${v.Phone}</td>
                    <td>${v.DiscountRate}</td>
                    <td>
                        <a href="#" class="btn btn-sm btn-primary">Edit</a> |
                        <a href="#" class="btn btn-sm btn-info">Details</a> |
                        <a href="#" class="btn btn-sm btn-danger">Delete</a>
                    </td>
                </tr>`;
            });
            $(".table tbody").html(tr); // Cập nhật nội dung bảng
        },
        error: (error) => {
            console.log(error);
        }
    });
}