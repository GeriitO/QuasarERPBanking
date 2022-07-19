function inhabilitartodo() {
    $(".limpiar").val("");
    $(".limpiar").prop('disabled', true);

}
function habilitartodo() {

    $(".limpiar").prop('disabled', false);
    $(".disabled").removeClass("disabled");
}

function Tab() {
    $('input').on("keypress", function (e) {
        /* ENTER PRESSED*/
        if (e.keyCode == 13) {
            /* FOCUS ELEMENT */
            var inputs = $(this).parents("form").eq(0).find(":input");
            var idx = inputs.index(this);

            if (idx == inputs.length - 1) {
                inputs[0].select()
            } else {
                inputs[idx + 1].focus(); //  handles submit buttons
                inputs[idx + 1].select();
            }
            return false;
        }
    });
}

function loadDatePicker() {
    $.datepicker.regional['pt-BR'] = {
        closeText: 'Fechar',
        prevText: '< Anterior',
        nextText: 'Próximo >',
        currentText: 'Hoje',
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
        'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
        'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        dayNames: ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: '',

    };
    $.datepicker.regional['es-VE'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.regional['en-US'] = {};

}


///////////// funcion para separadores de mil y decimales
function number_format(amount, decimals) {

    amount += ''; // por si pasan un numero en vez de un string
    amount = parseFloat(amount.replace(/[^0-9\.]/g, '')); // elimino cualquier cosa que no sea numero o punto (es el monto total con todo y decimales)

    decimals = decimals || 0; // por si la variable no fue fue pasada  (son la cantidad de decimales a mostrar)

    // si no es un numero o es igual a cero retorno el mismo cero
    if (isNaN(amount) || amount === 0)
        return parseFloat(0).toFixed(decimals);

    // si es mayor o menor que cero retorno el valor formateado como numero
    amount = '' + amount.toFixed(decimals);

    var amount_parts = amount.split('.'),
        regexp = /(\d+)(\d{3})/;

    while (regexp.test(amount_parts[0]))
        amount_parts[0] = amount_parts[0].replace(regexp, '$1' + '.' + '$2');             /////separador de mil

    return amount_parts.join(',');   ///////separador decimal

}

function number_format_js(number, decimals, dec_point, thousands_point) {

    if (number == null || !isFinite(number)) {
        throw new TypeError("number is not valid");
    }

    if (!decimals) {
        var len = number.toString().split('.').length;
        decimals = len > 1 ? len : 0;
    }

    if (!dec_point) {
        dec_point = '.';
    }

    if (!thousands_point) {
        thousands_point = ',';
    }

    number = parseFloat(number).toFixed(decimals);

    number = number.replace(".", dec_point);

    var splitNum = number.split(dec_point);
    splitNum[0] = splitNum[0].replace(/\B(?=(\d{3})+(?!\d))/g, thousands_point);
    number = splitNum.join(dec_point);

    return number;
}






function SeparadorDecimal2() {
    var SD = "";
    $.ajax({
        url: "/Funciones/IdentDecimal",
        success: function (data) {
            if (data != null) {
                SD = data;
                return SD;

            }
           

        }
    });

}



// para ordenamiento y paginador de tablas
//function loadDataTable_() {
//    $('#dataTable').DataTable({
//        searching: true,
//        ordering: true,
//        select: true,
//        language: {
//            processing: 'Procesando información...',
//            search: /*"Buscar:"*/,
//            lengthMenu: "Mostrar elementos _MENU_ ",
//            info: "Mostrando del elemento _START_ al _END_ de _TOTAL_ elementos",
//            infoEmpty: "Mostrando del elemento 0 al 0 de 0 elementos",
//            infoFiltered: "(_MAX_ elementos filtrados en total)",
//            infoPostFix: "",
//            loadingRecords: "Cargando...",
//            zeroRecords: "No hay datos disposibles",
//            emptyTable: "No hay datos disponibles en la tabla",
//            paginate: {
//                first: "Primero",
//                previous: "Anterior",
//                next: "Siguiente",
//                last: "Último"
//            },
//            aria: {
//                sortAscending: ": habilitado para ordenar la columna en orden ascendente",
//                sortDescending: ": habilitado para ordenar la columna en orden descendente"
//            }
//        }
//    });
//}

//$(document).ready(function () {
//    //loadDataTable();
//});



///// Funcion para permitir escribir solo numeros y un solo punto para decimales  
////forma de utilizar :  

//$("#positiveInt").numericInput();                                             SOLO NUMEROS POSITIVOS SIN DECIMALES                           
//$("#positiveNumber").numericInput({ allowFloat: true });                      SOLO NUMEROS POSITIVOS CON DECIMAL
//$("#anyInt").numericInput({ allowNegative: true });                           POSITIVOS O NEGATIVOS SIN DECIMAL
//$("#anyNumber").numericInput({ allowFloat: true, allowNegative: true });      NEGATIVOS CON DECIMAL
//me: www.transtatic.com
(function (b) { var c = { allowFloat: false, allowNegative: false }; b.fn.numericInput = function (e) { var f = b.extend({}, c, e); var d = f.allowFloat; var g = f.allowNegative; this.keypress(function (j) { var i = j.which; var h = b(this).val(); if (i > 0 && (i < 48 || i > 57)) { if (d == true && i == 46) { if (g == true && a(this) == 0 && h.charAt(0) == "-") { return false } if (h.match(/[.]/)) { return false } } else { if (g == true && i == 45) { if (h.charAt(0) == "-") { return false } if (a(this) != 0) { return false } } else { if (i == 8) { return true } else { return false } } } } else { if (i > 0 && (i >= 48 && i <= 57)) { if (g == true && h.charAt(0) == "-" && a(this) == 0) { return false } } } }); return this }; function a(d) { if (d.selectionStart) { return d.selectionStart } else { if (document.selection) { d.focus(); var f = document.selection.createRange(); if (f == null) { return 0 } var e = d.createTextRange(), g = e.duplicate(); e.moveToBookmark(f.getBookmark()); g.setEndPoint("EndToStart", e); return g.text.length } } return 0 } }(jQuery));