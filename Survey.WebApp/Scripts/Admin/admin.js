var Util = {
    number: {
        format: function (e) {
            if (e == 0) {
                return '';
            }
            if (e > 0) {
                e.toFixed(1);
                e += "";
                var t = e.split(".");
                var n = t[0];
                var r = t.length > 1 ? "." + t[1] : "";
                var i = /(\d+)(\d{3})/;
                while (i.test(n)) {
                    n = n.replace(i, "$1" + "," + "$2");
                }
                return n + r;
            }
            if (e < 0) {
                var o = 0 - e;
                o.toFixed(1);
                o += "";
                var t1 = o.split(".");
                var n1 = t1[0];
                var r1 = t1.length > 1 ? "." + t1[1] : "";
                var i1 = /(\d+)(\d{3})/;
                while (i1.test(n1)) {
                    n1 = n1.replace(i1, "$1" + "," + "$2");
                }
                return '-' + n1 + r1;
            }
            return e;
        },
        format2: function (e) {
            if (e == 0) {
                return '0';
            }
            if (e > 0) {
                e.toFixed(1);
                e += "";
                var t = e.split(".");
                var n = t[0];
                var r = t.length > 1 ? "." + t[1] : "";
                var i = /(\d+)(\d{3})/;
                while (i.test(n)) {
                    n = n.replace(i, "$1" + "," + "$2");
                }
                return n + r;
            }
            if (e < 0) {
                var o = 0 - e;
                o.toFixed(1);
                o += "";
                var t1 = o.split(".");
                var n1 = t1[0];
                var r1 = t1.length > 1 ? "." + t1[1] : "";
                var i1 = /(\d+)(\d{3})/;
                while (i1.test(n1)) {
                    n1 = n1.replace(i1, "$1" + "," + "$2");
                }
                return '-' + n1 + r1;
            }
            return e;
        }
    },
    date: {
        baseDateTime: function () { return new Date(2010, 0, 1, 0, 0, 0, 0); },
        getSeconds: function (value) {
            if (value) return parseInt((value.getTime() - this.baseDateTime().getTime()) / 1000);
            return 0;
        },
        getTime: function (seconds) {
            if (seconds == 0) {
                return '';
            }
            var date = this.baseDateTime();
            date.setSeconds(seconds);
            return date;
        },
        formatDateTime: function (value) {
            if (value) {
                var date = new Date(2010, 0, 1, 0, 0, 0, 0);
                date.setSeconds(value);
                return paddingDisplay2Number(date.getHours()) +
                    ':' +
                    paddingDisplay2Number(date.getMinutes()) +
                    ':' +
                    paddingDisplay2Number(date.getSeconds()) +
                    ' ' +
                    paddingDisplay2Number(date.getDate()) +
                    '-' +
                    paddingDisplay2Number(date.getMonth() + 1) +
                    '-' +
                    date.getFullYear();
            }
            return '';
        },
        formatDate: function (value) {
            if (value) {
                var date = new Date(2010, 0, 1, 0, 0, 0, 0);
                date.setSeconds(value);
                return paddingDisplay2Number(date.getDate()) +
                    '-' +
                    paddingDisplay2Number(date.getMonth() + 1) +
                    '-' +
                    date.getFullYear();
            }
            return '';
        },
        formatDate5: function (value) {
            if (value) {
                var date = Util.date.getTime(value);
                return paddingDisplay2Number(date.getDate()) + '-' + paddingDisplay2Number(date.getMonth() + 1) + '-' + paddingDisplay2Number(date.getYear() + 1900);
            }
            return '';
        },
        formatDuration: function (value) {
            if (value > 0) {
                var results = '';
                var days = parseInt(value / 86400);
                var pad = value % 86400;
                if (days > 0) {
                    results = days + 'd ';
                }
                results = results + paddingDisplay2Number(parseInt(pad / 3600)) + ':' + paddingDisplay2Number(parseInt((pad % 3600) / 60)) + ':' + paddingDisplay2Number(parseInt((pad % 3600) % 60));
                return results;
            }
            return '';
        },
        formatDateTime3: function (value) {
            if (value) {
                return paddingDisplay2Number((value.getFullYear()) + '/' + paddingDisplay2Number(value.getMonth() + 1) + '/' + paddingDisplay2Number(value.getDate()) + ' ' + paddingDisplay2Number(value.getHours()) + ':' + paddingDisplay2Number(value.getMinutes()));
            }
            return '';
        }
    }
};
function showNotification(data) {
    if (data.length == 0) {
        return;
    }
    var icon = '';
    if (data.lv == 1) {
        icon = 'success';
    }
    else if (data.lv == 2) {
        icon = 'info';
    }
    else if (data.lv == 3) {
        icon = 'warning';
    }
    else if (data.lv == 4) {
        icon = 'danger';
    }
    $.notify({
        // options
        message: '<table cellpadding="3" cellspacing="3"><tr><td nowrap="nowrap">Cập nhật lúc :</td><td nowrap="nowrap"><i>' + Util.date.formatDateTime(new Date()) + '</i></td></tr>' + '<tr><td nowrap="nowrap">Nội dung :</td><td nowrap="nowrap">' + getMessage(data.msg) + '</td></tr></table>'
    }, {
            // settings
            type: icon,
            placement: {
                from: 'bottom',
                align: 'right',
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                }
            }
        });
}
function paddingDisplay2Number(value) {
    if (value < 10) {
        return '0' + value;
    }
    return value;
}
function getMessage(msg) {
    var obj = $.parseJSON(msg);
    var result = '';
    for (var i = 0; i < obj.length; i++) {
        result += obj[0] + "<br/>";
    }
    return result;
}
function formatValueNull(e) {
    if (e) {
        return e;
    }
    return '';
}