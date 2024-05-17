$(document).ready(function () {
    $.ajax({
        url: '@Url.Action("ViewChart", "PhieuDangKiController")',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var labels = [];
            var dataValues = [];
            $.each(data, function (index, item) {
                labels.push(item.City);
                dataValues.push(item.Count);
            });
            // Thực hiện tạo biểu đồ ở đây
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
});