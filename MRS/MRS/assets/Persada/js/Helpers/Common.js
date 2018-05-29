var pathApi = "http://localhost:45086/api/";
var fullOrigin = "http://localhost:52325";
var pathWeb = location.origin;

//var pathApi = "http://tourismpediaapi.professionalis.me/api/";
//var fullOrigin = "http://tourismpedia.professionalis.me";

function Refresh() {
    window.location.reload(true);
}

function GetCurrentFullDate() {
    var rs = new Date().toLocaleString();
    return rs;
}

function GetCurrentDate() {
    var rs = new Date().toLocaleDateString();
    return rs;
}

function GetCurrentTime() {
    var rs = new Date().toLocaleTimeString();
    return rs;
}

function ConvertDate(dt) {
    var split = dt.split('-');
    var rs = split[1] + '-' + split[0] + '-' + split[2]

    return rs;
}

function EraseAttImg(value) {
    var rs = value.replace("data:image/png;base64,", '');
    return rs;
}

function RegexDate(str) {
    var regexValid = 0;
    var m = str.match(/^(\d{1,2})-(\d{1,2})-(\d{4})$/);
    if (m == null) {
        regexValid = 1;
    }
    return regexValid;
}

function RegexNumber(str) {
    var regexValid = 0;
    var m = str.match(/^\d+$/);
    if (m == null) {
        regexValid = 1;
    }
    return regexValid;
}

function IsValidDate() {
    var fields = document.getElementsByClassName("dateText");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('required_' + fields[i].id);
        var str = fields[i].innerHTML;
        if (RegexDate(str) == 1) {
            field.innerHTML = 'format required dd-mm-yyyy';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
}


function IsValidLength() {
    var fields = $(".item-required").find("span");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        if (fields[i].id != "") {
            var maxlength = $("#" + fields[i].id).attr("maxlenght");
            var field = document.getElementById('required_' + fields[i].id);
            if (fields[i].innerText.length > maxlength) {
                field.innerHTML = 'max lenght ' + maxlength;
                field.style.color = 'red';
                valid = 1;
            } else {
                field.innerHTML = '';
            };
        }
    };

    return valid;
};

function IsValidLengthNonMandatory() {
    var fields = $(".freeText");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var maxlength = $("#" + fields[i].id).attr("maxlenght");
        var field = document.getElementById('required_' + fields[i].id);
        if (fields[i].innerHTML.length > maxlength) {
            field.innerHTML = 'max lenght ' + maxlength;
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function IsValidChecked() {
    var fields = document.getElementsByClassName("mandatoryChecked");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('mandatory_checked_' + fields[i].id);
        //var isChecked = fields[i].is(":checked");
        if (!fields[i].checked) {
            field.innerHTML = 'Input must be checked';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function IsOnlyNumberInput() {
    var fields = document.getElementsByClassName("onlyNumber");
    var valid = 0;

    for (i = 0; i < fields.length; ++i) {
        var field = document.getElementById('only_number_' + fields[i].id);
        var str = fields[i].innerHTML;
        if (RegexNumber(str) == 1) {
            field.innerHTML = 'Input only number';
            field.style.color = 'red';
            valid = 1;
        } else {
            field.innerHTML = '';
        };
    };

    return valid;
};

function EraseComma(nStr) {
    return nStr.replace(/,/g, "");
}

function Comma(nStr) {
    nStr = nStr.replace(/,/g, "")
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');

    var result = x1 + x2;
    return result;
}

function fnExcelReport(idButton, idTable, nmReport) {
    var tab_text = '<html xmlns:x="urn:schemas-microsoft-com:office:excel">';
    tab_text = tab_text + '<head><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>';

    tab_text = tab_text + '<x:Name>' + nmReport + '</x:Name>';

    tab_text = tab_text + '<x:WorksheetOptions><x:Panes></x:Panes></x:WorksheetOptions></x:ExcelWorksheet>';
    tab_text = tab_text + '</x:ExcelWorksheets></x:ExcelWorkbook></xml></head><body>';

    //tab_text = tab_text + "<table>";
    //tab_text = tab_text + $('#' + idTableHead).html();
    //tab_text = tab_text + '</table>';

    tab_text = tab_text + "<table>";
    tab_text = tab_text + $('#' + idTable).html();
    tab_text = tab_text + '</table></body></html>';

    var data_type = 'data:application/vnd.ms-excel';

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        if (window.navigator.msSaveBlob) {
            var blob = new Blob([tab_text], {
                type: "application/csv;charset=utf-8;"
            });
            navigator.msSaveBlob(blob, nmReport + '.xls');
        }
    } else {
        $('#' + idButton).attr('href', data_type + ', ' + encodeURIComponent(tab_text));
        $('#' + idButton).attr('download', nmReport + '.xls');
    }

};


$.extend({
    //Passing with primitif object
    xResponse: function (url, data) {
        var theResponse = null;
        $.ajax({
            url: url,
            cache: false,
            traditional: true,
            type: 'POST',
            data: data,
            dataType: "json",
            async: false,
            success: function (respText) {
                theResponse = respText;
            },
            error: function (jqXHR, textStatus, errorMessage) {
                console.log(errorMessage);
            }
        });
        return theResponse;
    },
    //Passing with modern object
    xResponseJson: function (url, data) {
        var theResponse = null;
        $.ajax({
            url: url,
            cache: false,
            traditional: true,
            type: "POST",
            data: data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (respText) {
                theResponse = respText;
            },
            error: function (jqXHR, textStatus, errorMessage) {
                console.log(errorMessage);
            }
        });
        return theResponse;
    },
})

function CleanInputText() {
    var fields = document.getElementsByTagName("input");

    for (i = 0; i < fields.length; ++i) {
        fields[i].value = '';
    };
}

function IsValidForm() {
    var fields = $(".item-required");
    var valid = true;

    for (i = 0; i < fields.length; ++i) {
        if (fields[i].id != "") {
            var field = document.getElementById('required_' + fields[i].id);
            if (fields[i].value == '') {
                field.innerHTML = 'must be filled';
                field.style.color = 'red';
                field.style.display = '';
                valid = false;
                break;
            } else {
                field.innerHTML = '';
                field.style.display = 'none';
            };
        }
    };

    return valid;
}

function encode(input) {
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var output = "";
    var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
    var i = 0;

    while (i < input.length) {
        chr1 = input[i++];
        chr2 = i < input.length ? input[i++] : Number.NaN; // Not sure if the index 
        chr3 = i < input.length ? input[i++] : Number.NaN; // checks are needed here

        enc1 = chr1 >> 2;
        enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
        enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
        enc4 = chr3 & 63;

        if (isNaN(chr2)) {
            enc3 = enc4 = 64;
        } else if (isNaN(chr3)) {
            enc4 = 64;
        }
        output += keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                  keyStr.charAt(enc3) + keyStr.charAt(enc4);
    }
    return output;
}

function GetFile() {
    $('#postedFile').trigger('click');
    var fileList = document.getElementById("postedFile").files;
    var file = fileList[0];
    var fileReader = new FileReader();
    if (fileReader && fileList && fileList.length) {
        fileReader.readAsArrayBuffer(fileList[0]);
        fileReader.onload = function () {
            var arrayBuffer = fileReader.result;
            var bytes = new Uint8Array(arrayBuffer);
            document.getElementById("txtImage").src = "data:image/png;base64," + encode(bytes);
            document.getElementById("txtImage").style.display = "";
        };
    }
}

