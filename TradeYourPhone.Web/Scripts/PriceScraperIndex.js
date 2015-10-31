function toggleChecked(status) {
    $("input:checkbox").each(function () {
        // Set the checked status of each to match the 
        // checked status of the check all checkbox:
        $(this).prop("checked", status);
    });
}

$(document).ready(function () {

    // Grab a reference to the check all box:
    var checkAllBox = $("#checkall");

    //Set the default value of the global checkbox to true:
    checkAllBox.prop('checked', false);

    // Attach the call to toggleChecked to the
    // click event of the global checkbox:
    checkAllBox.click(function () {
        var status = checkAllBox.prop('checked');
        toggleChecked(status);
    });
});