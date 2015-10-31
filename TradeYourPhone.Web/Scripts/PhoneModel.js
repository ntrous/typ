function loadImageFromURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#primaryImageElement')
                .attr('src', e.target.result)
                .width(100)
        };

        reader.readAsDataURL(input.files[0]);
    }
}