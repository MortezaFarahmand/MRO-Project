$(document).ready(function () {
    $("#org").attr('disabled', true);
    $("#orgDepart").attr('disabled', true);

    LoadOrganizations();

    $('#org').change(function () {
        var organizationId = $(this).val();
        if (organizationId > 0) {
            LoadDepartments(organizationId);
        }
        else {
            alert("Select Organization");
            $('#org').empty();
            $("#org").attr('disabled', true);
            $("#orgDepart").attr('disabled', true);
            $('#org').append('<option>--Select Organization--</option>');
            $('#orgDepart').append('<option>--Select Department--</option>');
        }
    });
});

function LoadOrganizations() {
    $('#org').empty();
    $.ajax({
        url: '/IndexModel/GetOrganizations',
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#org').attr('disabled', true);
                $('#org').append('<option>--Select Organization--</option>');
                $('#orgDepart').append('<option>--Select Department--</option>');
                $.each(response, function (i, data) {
                    $('#org').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
            else {
                $("#org").attr('disabled', true);
                $("#orgDepart").attr('disabled', true);
                $('#org').append('<option>--Organizations not available--</option>');
                $('#orgDepart').append('<option>--Departments  not available--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    })
}

function LoadDepartments(organizationId) {
    $('#orgDepart').empty();
    $.ajax({
        url: '/IndexModel/GetDepartments?Id=' + organizationId,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#orgDepart').attr('disabled', true);
                $('#orgDepart').append('<option>--Select Department--</option>');
                $.each(response, function (i, data) {
                    $('#orgDepart').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
            else {
                $("#orgDepart").attr('disabled', true);
                $('#orgDepart').append('<option>--Departments  not available--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    })
}