function clearValue(txt, digi, dec)
{
    while(txt.indexOf(' ') != -1)
        txt = txt.replace(' ','');
    while(txt.indexOf(digi) != -1)
        txt = txt.replace(digi,'');
    txt = txt.replace(dec,'.');
    if (txt == '')
        txt = '0';
    return txt;
}

function jsmoneyformat(val, digi, dec, len) {
    var Sign = '';
    if (digi == null) digi = ',';
    if (dec == null) dec = '.';
    if (len == null) len = 0;

    if (val < 0) {
        Sign = '-';
        val = val * -1;
    }
    var iNum = parseInt(val);
    var decstr = '';
    if (len > 0) {
        var fDec = val - iNum;
        fDec = fDec.toFixed(len);
        fDec = parseFloat(fDec);

        var iDec = Math.round(fDec * Math.pow(10, len));
        iDec = iDec.toFixed(len);
        iDec = parseFloat(iDec);

        if (iDec >= Math.pow(10, len)) {
            iNum = iNum + (iDec / Math.pow(10, len));
            iDec = iDec - Math.pow(10, len);
        }


        var decstr = iDec.toString();
        while (decstr.length < len)
            decstr = '0' + decstr;
        if (decstr.length > len) decstr = decstr.substr(0, len);
        decstr = dec + decstr;
    }
    var num = iNum.toString();
    var numstr = '';
    while (num.length > 3) {
        numstr = digi + num.substr(num.length - 3, 3) + numstr;
        num = num.substr(0, num.length - 3);
    }
    numstr = num + numstr;
    return Sign + numstr + decstr;
}

function cekpct(obj)
{
    lastval = obj.value;
    curval = '';
    if (event.keyCode >= 48 && event.keyCode <= 57)
        curval = event.keyCode - 48;
    nextval = parseFloat(clearValue(lastval + curval,',','.'));
    
    if (nextval > 100)
    {
        return false;
    }
    
    return true;
}
