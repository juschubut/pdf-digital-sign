(function ($) {
    $.juschubutSign = function (options) {

        // Plugin default options.
        var defaultOptions = {

            // Url donde se encuentra la aplicacion que firma ClickOnce
            urlFirmador: 'https://apprw/juschubutpdfsign/Juschubut.WinPdfDigitalSign.application',

            // Url del servidor que se utiliza como intermediario entre el cliente y el firmador.
            urlServer: 'https://apprw/PdfSignServer',

            // Url desde donde se debe tomar el pdf que se desea firmar
            urlArchivo: '',

            //{url:'localhost/client/pdf/1', nombre:'Acta', id:'identificador'},
            archivos: [],

            // Indica el layout de la firma (1, 2, 3, 4, 5, 6). Si es -1 indica un layout personalizado (debe especificar x, y, alto y ancho)
            layout: 1,

            // Para un layout distinto de 1, se debe especificar el firmante.
            numeroFirmante: 1,

            // Alto que ocupará cada firma
            altoFirma: 150,
            // Ancho que ocupará cada firma
            anchoFirma: 150,
            // Posicion x de la firma. 
            posicionXFirma: 0,
            // Posicion y de la firma. 
            posicionYFirma: 0,

            // Modo de funcionamiento (oculto, visible). En el modo visible se muestra un mensane con lo que esta sucediendo.
            modo: 'oculto',

            // Leyenda que se agrega antes del nombre del firmante.
            leyenda: 'Firmado digitalmente el {fecha-hora} por',

            // Leyenda que se agrega luego del nombre del firmante.
            cargo: '',

            // Se ejecuta si se produce algun error
            onError: function (error) { return; },

            // Se ejecuta antes de comenzar la firma
            onStart: function () { },

            // Se ejecuta con cada cambio de estado
            onProgress: function () { },

            // Se ejecuta cuando termina la firma en forma correcta
            onComplete: function () { }
        };

        // Extend default options.
        var settings = $.extend(true, defaultOptions, options);

        var signID = "";
        var lastStatus = "";

        var model = {
        };

        if (settings.archivos != null && settings.archivos.length > 0) {
            model.Archivos = [];

            for (var i = 0; i < settings.archivos.length; i++) {
                var f = settings.archivos[i];

                if (f != null) {

                    model.Archivos.push({
                        Nombre: f.nombre,
                        Url: f.url,
                        ClientID: f.id
                    });
                }
            }

            if (model.Archivos.length > 0)
                settings.urlArchivo = null;
        }

        post({
            url: settings.urlServer + '/Sign/Setup',
            data: model,
            onSuccess: function (rs) {
                signID = rs.signID;

                if (typeof (settings.onStart) == 'function') {
                    settings.onStart(signID);
                }

                status();

                var url = settings.urlFirmador;
                var archivo = "";

                if (settings.urlArchivo != null && settings.urlArchivo != '')
                    archivo = encodeURI(settings.urlArchivo);

                url += "?signID=" + rs.signID
                    + "&modo=" + settings.modo
                    + "&prefirma=" + settings.leyenda
                    + "&postfirma=" + settings.cargo
                    + "&layout=" + settings.layout
                    + "&altofirma=" + settings.altoFirma
                    + "&anchofirma=" + settings.anchoFirma
                    + "&pxfirma=" + settings.posicionXFirma
                    + "&pyfirma=" + settings.posicionYFirma
                    + "&firmante=" + settings.numeroFirmante
                    + "&urlServer=" + encodeURI(settings.urlServer)
                    + "&archivo=" + archivo

                    + "&debug=true";

                window.location = url;
            },
            onError: function (err) {
                if (typeof (settings.onError) == 'function') {
                    settings.onError(err);
                }
            }

        });

        function post(options) {

            $.ajax({
                type: "POST",
                async: true,
                url: options.url,
                crossDomain: true,
                traditional: true,
                data: JSON.stringify(options.data),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (options.onSuccess != null)
                        options.onSuccess(data);
                },
                error: function (err, err2) {
                    if (typeof (settings.onError) == 'function')
                        settings.onError(err2);
                    else
                        alert(err2);
                }

            });
        }

        function status() {

            post({
                url: settings.urlServer + '/Sign/Status/' + signID,
                onSuccess: function (rs) {

                    var lastStatus = rs.ultimoStatus;

                    if (typeof (settings.onProgress) == 'function') {
                        settings.onProgress(rs);
                    }

                    if (lastStatus.codigo.toUpperCase() == "COMPLETADO") {
                        if (typeof (settings.onComplete) == 'function') {

                            var url = '';

                            if (rs.archivos != null && rs.archivos.length > 0) {
                                var a = rs.archivos[rs.archivos.length - 1];
                                url = a.urlArchivoFirmado;
                            }

                            settings.onComplete(
                                {
                                    result: rs,
                                    url: url
                                });
                        }
                    }
                    else if (lastStatus.codigo.toUpperCase() == 'ERROR') {
                        if (typeof (settings.onError) == 'function') {
                            settings.onError(rs);
                        }
                    }
                    else {
                        window.setTimeout(function () {
                            status();
                        }, 1000);
                    }
                },
                onError: function (err) {
                    if (typeof (settings.onError) == 'function') {
                        settings.onError(err);
                    }
                }
            });
        }
    };
})(jQuery);