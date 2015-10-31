$(function () {
    $(".PhoneModel").change(function () {
        var phoneModelId = $(this).val();
        var phoneConditionId = $(this).closest('tr').find('.PhoneCondition').val();
        var purchaseAmount = $(this).closest('tr').find('.PurchaseAmount');
        UpdatePhoneConditionPrice(phoneModelId, phoneConditionId, purchaseAmount);
    });
});

$(function () {
    $(".PhoneCondition").change(function () {
        var phoneModelId = $(this).closest('tr').find('.PhoneModel').val();
        var phoneConditionId = $(this).val();
        var purchaseAmount = $(this).closest('tr').find('.PurchaseAmount');
        UpdatePhoneConditionPrice(phoneModelId, phoneConditionId, purchaseAmount);
    });
});

$(function () {
    $(".PhoneMake").change(function () {
        var phoneMakeId = $(this).val();
        var phoneModelId = $(this).closest('tr').find('.PhoneModel');
        UpdatePhoneModels(phoneMakeId, phoneModelId);
    });
});

var UpdatePhoneConditionPrice = function (PhoneModelId, PhoneConditionId, txtBoxToUpdate) {
    $.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/PhoneConditionPrices/GetPhoneConditionPrice",
        data: { "PhoneModelId": PhoneModelId, "PhoneConditionId": PhoneConditionId },
        success: function (data) {
            txtBoxToUpdate.val('');
            txtBoxToUpdate.val(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve Phone Price');
        }
    });
};

var UpdatePhoneModels = function (PhoneMakeId, selectList) {
    $.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/PhoneModels/GetPhoneModelsByMakeId",
        data: { "phoneMakeId": PhoneMakeId },
        success: function (data) {
            var listToUpdate = $(selectList);
            listToUpdate.html('');
            $.each(data, function (id, option) {
                listToUpdate.append($('<option></option>').val(option.id).html(option.name));
            });
            listToUpdate.change();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve Phone Price');
        }
    });
};

$(function () {
    $("#AddPhoneBtn").click(function () {
        AddPhoneToQuote();
    });
});

var AddPhoneToQuote = function () {
    var phone = {};
    phone.PhoneMakeId = $('#PhoneMakeId').val();
    phone.PhoneModelId = $('#PhoneModelId').val();
    phone.PhoneConditionId = $('#PhoneConditionId').val();

    var result = AddPhoneToQuoteInSession(phone);
};

var AddPhoneToQuoteInSession = function (phone) {
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        contentType: 'application/json',
        dataType: 'json',
        url: "/Quotes/AddPhoneToQuoteInSession",
        data: JSON.stringify({ "phone": phone }),
        success: function (data) {
            var result = data;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to add Phone to Quote');
        }
    });
};

