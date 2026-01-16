var SinglePage = {};

SinglePage.LoadModal = function () {
    var url = window.location.hash.toLowerCase();
    if (!url.startsWith("#showmodal")) {
        return;
    }
    url = url.split("showmodal=")[1];
    $.get(url,
        null,
        function (htmlPage) {
            $("#ModalContent").html(htmlPage);
            const container = document.getElementById("ModalContent");
            const forms = container.getElementsByTagName("form");
            const newForm = forms[forms.length - 1];
            $.validator.unobtrusive.parse(newForm);
            showModal();
            
        }).fail(function (error) {
            alert("خطایی رخ داده، لطفا با مدیر سیستم تماس بگیرید.");
        });
};

function showModal() {
    $("#MainModal").modal("show");
}

function hideModal() {
    $("#MainModal").modal("hide");
}

$(document).ready(function () {
    window.onhashchange = function () {
        SinglePage.LoadModal();
    };
    $("#MainModal").on("shown.bs.modal",
        function () {
            window.location.hash = "##";

            $('.persianDateInput').persianDatepicker({
                format: 'DD/MM/YYYY',
                calendarType: 'gregorian',
                autoClose: true
            });

            $('.select22').select2({
                width: '100%'
            });


        });

    $(document).on("submit",
        'form[data-ajax="true"]',
        function (e) {
            e.preventDefault();
            var form = $(this);
            const method = form.attr("method").toLocaleLowerCase();
            const url = form.attr("action");
            var action = form.attr("data-action");

            if (method === "get") {
                const data = form.serializeArray();
                $.get(url,
                    data,
                    function (data) {
                        CallBackHandler(data, action, form);
                    });
            } else {
                var formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    data: formData,
                    enctype: "multipart/form-data",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        CallBackHandler(data, action, form);
                    },
                    error: function (data) {
                        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
                    }
                });
            }
            return false;
        });
});

function CallBackHandler(data, action, form) {
    switch (action) {
        case "Message":
            alert(data.Message);
            break;
        case "Refresh":
            if (data.isSucceeded) {
                window.location.reload();
            } else {
                alert(data.message);
            }
            break;
        case "RefereshList":
            {
                hideModal();
                const refereshUrl = form.attr("data-refereshurl");
                const refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                const element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

function get(url, refereshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refereshDiv).html(result);
        });
}

function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};

function checkSlugDuplication(url, dist) {
    const slug = $('#' + dist).val();
    const id = convertToSlug(slug);
    $.get({
        url: url + '/' + id,
        success: function (data) {
            if (data) {
                sendNotification('error', 'top right', "خطا", "اسلاگ نمی تواند تکراری باشد");
            }
        }
    });
}

function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}

//var limitPara = $('#your_div_id');
//limitPara.text(limitPara.text().substring(0,300))

$(document).on("click",
    'button[data-ajax="true"]',
    function () {
        const button = $(this);
        const form = button.data("request-form");
        const data = $(`#${form}`).serialize();
        let url = button.data("request-url");
        const method = button.data("request-method");
        const field = button.data("request-field-id");
        if (field !== undefined) {
            const fieldValue = $(`#${field}`).val();
            url = url + "/" + fieldValue;
        }
        if (button.data("request-confirm") == true) {
            if (confirm("آیا از انجام این عملیات اطمینان دارید؟")) {
                handleAjaxCall(method, url, data);
            }
        } else {
            handleAjaxCall(method, url, data);
        }
    });

function handleAjaxCall(method, url, data) {
    if (method === "post") {
        $.post(url,
            data,
            "application/json; charset=utf-8",
            "json",
            function (data) {

            }).fail(function (error) {
                alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
            });
    }
}

jQuery.validator.addMethod("maxFileSize",
    function (value, element, params) {
        var size = element.files[0].size;
        var maxSize = 3 * 1024 * 1024;
        if (size > maxSize)
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");

jQuery.validator.addMethod("validExtentions",
    function (value, element, params) {
        var AllowExtensions = ["jpeg", "jpg", "png", "svg"];
        debugger;
        var extension = (/[.]/.exec(value)) ? /[^.]+$/.exec(value) : undefined;
        if (extension != undefined) {
            extension = extension[0];
        }
        extension = extension;
        var validExtension = $.inArray(extension, AllowExtensions) !== -1;
        return validExtension;
    
    });
jQuery.validator.unobtrusive.adapters.addBool("validExtentions");

//function getFileExtension(fileName) {
//    var extension = (/[.]/.exec(fileName)) ? /[^.]+$/.exec(fileName) : undefined;
//    if (extension != undefined) {
//        return extension[0];
//    }
//    return extension;
//};
// (jQuery);


//function validateFileType() {
//    var selectedFile = document.getElementById('fileInput').files[0];
//    var allowedTypes = ['image/jpeg', 'image/png', 'application/pdf'];

//    if (!allowedTypes.includes(selectedFile.type)) {
//        alert('Invalid file type. Please upload a JPEG, PNG, or PDF file.');
//        document.getElementById('fileInput').value = '';
//    }
//}


//jQuery.validator.addMethod("selectnic", function (value, element) {
//    if (/^[0-9]{9}[vVxX]$/.test(value)) {
//        return false;  // FAIL validation when REGEX matches
//    } else {
//        return true;   // PASS validation otherwise
//    };
//}, "wrong nic number"); 

//$('#basicDetails').validate({ // initialize the Plugin

//    rules: {
//        fname: {
//            required: true,
//            lettersonly: true,
//        },
//        lname: {
//            required: true,
//            lettersonly: true,
//        },

//        nicnumber: {
//            // other rules,
//            selectnic: true // <-  declare the rule someplace!
//        }


//    },


//    messages: {

//        fname: {
//            required: "Please enter your first name",
//            lettersonly: "Login format not valid",

//        },
//        lname: {
//            required: "Please enter your last name",
//            lettersonly: "Login format not valid",

//        },
//    },


//    submitHandler: function (form) { // for demo
//        alert('valid form submitted'); // for demo
//        return false; // for demo
//    }
//});

