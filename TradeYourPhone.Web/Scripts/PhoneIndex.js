$(function () {
    $("#PhoneMakeId").change(function () {
        UpdatePhoneModels($("#PhoneMakeId").val(), $('#PhoneModelId'));
    });
});

var UpdatePhoneModels = function (PhoneMakeId, selectList) {
    if (PhoneMakeId != "") {
        $.ajax({
            cache: false,
            async: false,
            type: "GET",
            url: "/PhoneModels/GetPhoneModelsByMakeId",
            data: { "phoneMakeId": PhoneMakeId },
            success: function (data) {
                var listToUpdate = $(selectList);
                listToUpdate.html('').append($('<option></option>').val('').html('All'));
                $.each(data, function (id, option) {
                    listToUpdate.append($('<option></option>').val(option.id).html(option.name));
                });
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve Phone Price');
            }
        });
    }
    else {
        $(selectList).html('').append($('<option></option>').val('').html('All'));
    }
};