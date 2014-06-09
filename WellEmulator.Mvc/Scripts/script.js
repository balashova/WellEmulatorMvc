
var modalShown = false;
var delayedMessage = undefined;

var showMessage = function (message) {
    if (modalShown) {
        delayedMessage = message;
        return;
    }

    if (!message) {
        message = "Пожалуйста подождите...";
        $("button#modal-ok-button").hide();
    } else {
        $("button#modal-ok-button").show();
    }

    $("span#modal-body").html(message);
    $("#wait-dialog").modal("show");
};

var hideMessage = function () {
    $("#wait-dialog").modal("hide");
};

$("#wait-dialog").on("show.bs.modal", function () {
    modalShown = true;
});

$("#wait-dialog").on("hidden.bs.modal", function() {
    modalShown = false;
    if (delayedMessage) {
        showMessage(delayedMessage);
        delayedMessage = undefined;
    }
});

var validateFields = function () {
    var allOkay = true;
    $(".need-validation").each(function () {
        var element = $(this);

        var addClass = function () {
            element.addClass("field-error");
        };

        var removeClass = function () {
            element.removeClass("field-error");
        };

        var checkFields = function (el) {
            if (!el.val()) {
                if (el.hasClass("field-error")) {
                    removeClass();
                    setTimeout(addClass, 200);
                    setTimeout(removeClass, 300);
                    setTimeout(addClass, 400);
                } else {
                    addClass();
                    setTimeout(removeClass, 100);
                    setTimeout(addClass, 200);
                }
                allOkay = false;
            }
        };

        var findFields = function (el) {
            var elementName = el.tagName.toLowerCase();
            if (elementName == "select" || elementName == "input") {
                checkFields($(el));
            } /*else { //рекурсивный поиск дочерних элементов
                        element.find("input, select").each(function () {
                            findFields(this);
                        });
                    }*/
        };

        findFields(this);
    });
    return allOkay;
};

$(".need-validation").focusin(function () {
    $(this).removeClass("field-error");
});