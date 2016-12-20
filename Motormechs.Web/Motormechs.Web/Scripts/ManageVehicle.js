jQuery(document).ready(function () {
    jQuery(".DeleteVehicle").click(function () {
        var id = jQuery(this).attr("value");
        var userId = jQuery('#UserID').val();
        var ApiURL = jQuery('#ApiURL').val();
        var URL = ApiURL + "Vehicle/Delete?Id=" + id + "&UserId=" + userId;
        jQuery.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'GET',
            url: URL,
            data: null,
            success: function (response) {
                if (response.IsSuccess) {
                    jQuery("#Record-" + id).remove();
                    if (response.ResponseData == 0) {
                        var html = '<tr>\
                                        <td align="center" colspan="6">\
                                            No Vehicle Found\
                                        </td>\
                                    </tr>';
                        jQuery("tbody").html(html)
                    }
                }
            },
            failure: function (error) {
                alert(error);
            }
        });
    });
});
