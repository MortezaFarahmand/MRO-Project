var SinglePage = {};

SinglePage.LoadModal = function () {
    var url = window.location.hash.toLowerCase();
    if (!url.startsWith("#showmodal")) {
        return;
    }
    url = url.split("showmodal=")[1];
    if (!url) return;

    $.get(url, null, function (htmlPage) {
        $("#ModalContent").html(htmlPage);

        try {
            const container = document.getElementById("ModalContent");
            const forms = container.getElementsByTagName("form");
            if (forms.length > 0 && $.validator && $.validator.unobtrusive) {
                $.validator.unobtrusive.parse(forms[forms.length - 1]);
            }
        } catch (e) {
            console.warn("unobtrusive parse", e);
        }

        try {
            $("#ModalContent").find("script").each(function () {
                $.globalEval(this.text || this.textContent || this.innerHTML || "");
            });
        } catch (e) {
            console.warn("modal script", e);
        }

        showModal();
    }).fail(function () {
        alert("خطایی رخ داده، لطفا با مدیر سیستم تماس بگیرید.");
    });
};

function showModal() {
    $("#MainModal").modal("show");
}

function hideModal() {
    $("#MainModal").modal("hide");
}

// =========================
// Tom Select – RTL/LTR
// =========================
function isRtlText(text) {
    if (!text) return false;
    return /[\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF]/.test(String(text));
}

function applyDirToTomSelect(ts) {
    if (!ts) return;

    function setWrapperDir(rtl) {
        ts.wrapper.classList.toggle("ts-rtl", rtl);
        ts.wrapper.classList.toggle("ts-ltr", !rtl);
        if (ts.control) {
            ts.control.classList.toggle("rtl", rtl);
        }
    }

    function updateControlDir() {
        var text = "";
        try {
            var v = ts.getValue();
            if (v !== null && v !== undefined && v !== "") {
                var opt = ts.options[v];
                text = (opt && (opt.text || opt.label)) || "";
            } else {
                text = (ts.settings && ts.settings.placeholder) ||
                    (ts.input && ts.input.querySelector("option[value='']"))?.textContent ||
                    ts.control_input?.getAttribute("placeholder") ||
                    ts.wrapper.querySelector(".ts-control")?.textContent ||
                    "";
            }
        } catch (e) { text = ""; }
        setWrapperDir(isRtlText(text));
    }

    function markOptionsDir() {
        if (!ts.dropdown) return;
        ts.dropdown.querySelectorAll(".option").forEach(function (el) {
            var t = (el.textContent || "").trim();
            var rtl = isRtlText(t);
            el.classList.toggle("ts-rtl", rtl);
            el.classList.toggle("ts-ltr", !rtl);
        });
    }

    ts.on("change", updateControlDir);
    ts.on("type", function (str) {
        setWrapperDir(isRtlText(str));
    });
    ts.on("dropdown_open", markOptionsDir);
    ts.on("load", function () {
        setTimeout(markOptionsDir, 30);
    });

    updateControlDir();
}

function destroyTomSelect(el) {
    if (el && el.tomselect) {
        try { el.tomselect.destroy(); } catch (e) { }
    }
}

function positionDropdown(ts) {
    var dropdown = ts.dropdown;
    var control = ts.control;
    if (!dropdown || !control) return;

    var rect = control.getBoundingClientRect();
    dropdown.style.width = rect.width + "px";
    dropdown.style.minWidth = rect.width + "px";
    dropdown.style.left = rect.left + "px";
    dropdown.style.top = (rect.bottom + window.scrollY) + "px";
    dropdown.style.position = "absolute";
    dropdown.style.display = "block";
    dropdown.style.zIndex = "10060";
}

function initTomSelectStatic($root) {
    if (typeof TomSelect === "undefined") return;
    $root = $root && $root.length ? $root : $(document);

    $root.find("select.tom-select").each(function () {
        var el = this;
        if (el.tomselect) return;

        var fixedDir = el.getAttribute("data-dir");
        var noSearch = el.getAttribute("data-no-search") === "true";

        destroyTomSelect(el);

        var settings = {
            allowEmptyOption: true,
            create: false,
            maxOptions: null,
            dropdownParent: document.body,
            closeAfterSelect: true,
            hideSelected: false,
            openOnFocus: true,
        };

        // فقط وقتی می‌خواهیم سرچ نداشته باشیم
        if (noSearch) {
            settings.controlInput = null;
        }

        var ts = new TomSelect(el, settings);

        ts.on("dropdown_open", function () {
            positionDropdown(ts);
        });

        if (fixedDir === "rtl" || fixedDir === "ltr") {
            ts.wrapper.classList.add(fixedDir === "rtl" ? "ts-rtl" : "ts-ltr");
            if (ts.control) ts.control.classList.toggle("rtl", fixedDir === "rtl");
        } else {
            applyDirToTomSelect(ts);
        }
    });
}

function initTomSelectAjax($root) {
    if (typeof TomSelect === "undefined") return;
    $root = $root && $root.length ? $root : $(document);

    $root.find("select.tom-select-ajax").each(function () {
        var el = this;
        if (el.tomselect) return;

        var url = el.getAttribute("data-url");
        var minLen = parseInt(el.getAttribute("data-min-length") || "1", 10);
        var fixedDir = el.getAttribute("data-dir");
        if (!url) return;

        destroyTomSelect(el);

        var settings = {
            valueField: "id",
            labelField: "text",
            searchField: "text",
            allowEmptyOption: true,
            create: false,
            maxOptions: 30,
            loadThrottle: 250,
            dropdownParent: document.body,
            closeAfterSelect: true,
            openOnFocus: true,
            shouldLoad: function (query) {
                return query && query.length >= minLen;
            },
            load: function (query, callback) {
                if (!query || query.length < minLen) {
                    callback();
                    return;
                }
                var sep = url.indexOf("?") >= 0 ? "&" : "?";
                fetch(url + sep + "term=" + encodeURIComponent(query))
                    .then(function (r) { return r.json(); })
                    .then(function (json) { callback(json || []); })
                    .catch(function () { callback(); });
            },
            render: {
                option: function (data, escape) {
                    return "<div>" + escape(data.text || "") + "</div>";
                },
                item: function (data, escape) {
                    return "<div>" + escape(data.text || data.id || "") + "</div>";
                }
            }
        };

        var ts = new TomSelect(el, settings);

        ts.on("dropdown_open", function () {
            positionDropdown(ts);
        });

        if (fixedDir === "rtl" || fixedDir === "ltr") {
            ts.wrapper.classList.add(fixedDir === "rtl" ? "ts-rtl" : "ts-ltr");
            if (ts.control) ts.control.classList.toggle("rtl", fixedDir === "rtl");
        } else {
            applyDirToTomSelect(ts);
        }
    });
}

function initAllTomSelect($root) {
    var $scope = $root && $root.length ? $root : $(document);

    // نابود کردن نمونه‌های قبلی
    $scope.find("select.tom-select, select.tom-select-ajax").each(function () {
        destroyTomSelect(this);
    });

    initTomSelectStatic($scope);
    initTomSelectAjax($scope);
}

// =========================
// Select2 (مودال)
// =========================
function initModalSelect2($modal) {
    if (typeof $.fn.select2 !== "function") return;

    $modal = $modal && $modal.length ? $modal : $("#MainModal");

    $modal.find("select.select2-search, select.select22").each(function () {
        var $el = $(this);

        if ($el.data("select2")) {
            try { $el.select2("destroy"); } catch (e) { }
        }

        $el.select2({
            width: "100%",
            minimumResultsForSearch: 0,
            allowClear: true,
            dropdownParent: $modal
        });
    });
}

// =========================
// Document ready
// =========================
$(document).ready(function () {
    window.onhashchange = function () {
        SinglePage.LoadModal();
    };

    $("#MainModal").on("shown.bs.modal", function () {
        window.location.hash = "##";

        try {
            $(".persianDateInput").persianDatepicker({
                format: "DD/MM/YYYY",
                calendarType: "gregorian",
                autoClose: true
            });
        } catch (e) { console.warn(e); }

        try { initModalSelect2($("#MainModal")); } catch (e) { console.warn(e); }
        try { initAllTomSelect($("#MainModal")); } catch (e) { console.warn(e); }

        setTimeout(function () {
            $("#MainModal").removeAttr("aria-hidden");
        }, 10);
    });

    // صفحات عادی
    try {
        initAllTomSelect($(document));
    } catch (e) {
        console.warn("initAllTomSelect page", e);
    }

    $(document).on("submit", 'form[data-ajax="true"]', function (e) {
        e.preventDefault();
        var form = $(this);
        const method = form.attr("method").toLocaleLowerCase();
        const url = form.attr("action");
        var action = form.attr("data-action");

        if (method === "get") {
            const data = form.serializeArray();
            $.get(url, data, function (data) {
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
                error: function () {
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
                $("#" + element).html(data);
            }
            break;
        default:
    }
}

function get(url, refereshDiv) {
    const searchModel = window.location.search;
    $.get(url, searchModel, function (result) {
        $("#" + refereshDiv).html(result);
    });
}

function makeSlug(source, dist) {
    const value = $("#" + source).val();
    $("#" + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = "";
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, "-").replace(/-+/g, "-").replace(/^-|-$/g, "");
    return $slug.toLowerCase();
};

function checkSlugDuplication(url, dist) {
    const slug = $("#" + dist).val();
    const id = convertToSlug(slug);
    $.get({
        url: url + "/" + id,
        success: function (data) {
            if (data) {
                sendNotification("error", "top right", "خطا", "اسلاگ نمی تواند تکراری باشد");
            }
        }
    });
}

function fillField(source, dist) {
    const value = $("#" + source).val();
    $("#" + dist).val(value);
}

$(document).on("click", 'button[data-ajax="true"]', function () {
    const button = $(this);
    const form = button.data("request-form");
    const data = $("#" + form).serialize();
    let url = button.data("request-url");
    const method = button.data("request-method");
    const field = button.data("request-field-id");
    if (field !== undefined) {
        const fieldValue = $("#" + field).val();
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
        $.post(url, data, "application/json; charset=utf-8", "json", function () {
        }).fail(function () {
            alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
        });
    }
}

jQuery.validator.addMethod("maxFileSize", function (value, element) {
    if (!element.files || !element.files[0]) return true;
    var size = element.files[0].size;
    var maxSize = 3 * 1024 * 1024;
    return size <= maxSize;
});
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");

jQuery.validator.addMethod("validExtentions", function (value) {
    var AllowExtensions = ["jpeg", "jpg", "png", "svg"];
    var extension = (/[.]/.exec(value)) ? /[^.]+$/.exec(value) : undefined;
    if (extension != undefined) {
        extension = extension[0];
    }
    return $.inArray(extension, AllowExtensions) !== -1;
});
jQuery.validator.unobtrusive.adapters.addBool("validExtentions");