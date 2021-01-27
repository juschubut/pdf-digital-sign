function messageClear() {
    $.noty.closeAll();
}

function messageSuccess(text, options) {
    messageHelper(text, 'success', options);
}

function messageInformation(text, options) {
    messageHelper(text, 'information', options);
}

function messageWarning(text, options) {
    messageHelper(text, 'warning', options);
}

function messageErrorResult(errors) {
    var msg = "";
    var i = 0;

    for (i = 0; i < errors.length; i++) {
        msg += "> " + errors[i].ErrorMessage + "<br/>";
    }

    messageError(msg);
}

function messageConfirm(text, okFunction, options) {

    var ok = {
        className: 'btn btn-primary',
        text: 'Aceptar'
    };

    var cancel = {
        className: 'btn btn-danger',
        text: 'Cancelar'
    };

    var extra = null;

    if (options != null) {
        if (options.buttons != null) {
            if (options.buttons.ok != null)
                ok = $.extend(ok, options.buttons.ok);

            if (options.buttons.cancel != null)
                cancel = $.extend(cancel, options.buttons.cancel);

            if (options.buttons.extra != null)
                extra = $.extend({
                    className: 'btn btn-default',
                    text: 'Extra'
                }, options.buttons.extra);

            delete options['buttons'];
        }
    }


    var defaultOptions = {
        modal: true,
        layout: 'center',
        buttons: []
    };


    defaultOptions.buttons.push(
    {
        addClass: ok.className,
        text: ok.text,
        onClick: function ($noty) {
            $noty.close();

            okFunction.call();
        }
    });

    if (extra != null) {
        defaultOptions.buttons.push(
            {
                addClass: extra.className,
                text: extra.text,
                onClick: function ($noty) {
                    $noty.close();

                    if (extra.onClick != null)
                        extra.onClick.call();
                }
            });
    }

    defaultOptions.buttons.push(
    {
        addClass: cancel.className,
        text: cancel.text,
        onClick: function ($noty) {
            $noty.close();
        }
    });


    options = $.extend(defaultOptions, options);

    return messageHelper(text, 'alert', options);
}

function messageInput(titulo, text2, onOkFunction, extraOptions) {

    var options = $.extend({ inputText: '', layout: 'juschubutInputMessage', rows: 15 }, extraOptions);

    var html = "<span class=\"noty_confirm_title\">" + titulo + "</span><div style=\"margin-top:15px;\" class=\"form\"><label>" + text2 + "</label><textarea id=\"txtNotyInput\" rows=\"" + options.rows + "\" class=\"form-control\">" + options.inputText + "</textarea></div>";

    var n = messageConfirm(html,
        function () {
            var inputText = $("#txtNotyInput").val();

            onOkFunction(inputText);
        }, options);

    n.$message.find('textarea').focus();
}

function messageError(text, options) {

    messageHelper(text, 'error', options);
}

function messageHelper(text, type, options) {
    messageClear();

    if (options == undefined)
        options = {};

    var defaultOptions =
        {
            type: type,
            modal: false,
            timeout: 1000 * 10,
            layout: 'bottomCenter',
            theme: 'defaultTheme'
        };

    options['text'] = text;

    var notyOptions = $.extend({}, defaultOptions, options);

    return noty(notyOptions);
}