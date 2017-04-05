/*
添加到收藏夹
*/
function addFavorite(url, title) {
    window.external.AddFavorite(url, title);
}
// 对Date的扩展，将 Date 转化为指定格式的String   
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
// 例子：   
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
Date.prototype.format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
return fmt;
}
/*
添加天数
示例：
new Date().addDays(5);
new Date().addDays(-4);
*/
Date.prototype.addDays = function (days) {
    var dateValue = this.valueOf();
    dateValue += days * 24 * 60 * 60 * 1000;
    return new Date(dateValue);
}
/*
添加月
示例：
new Date().addMonths(5);
new Date().addMonths(-4);
*/
Date.prototype.addMonths = function (months) {
    var currentMonth = this.getMonth();
    var year = this.getFullYear() + parseInt((currentMonth + months) / 12);
    var month = (this.getMonth() + months) % 12;
    var dayInMonth = new Date(year, month + 1, 0).getDate();
    var day = this.getDate();
    if (day > dayInMonth) {
        day = dayInMonth;
    }
    var resultDate = new Date(year, month, day);
    return resultDate;
}
/*
删除指定位置的元素
*/
Array.prototype.remove = function (index) {
    if (isNaN(index) || index > this.length) {
        return false;
    }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[index]) {
            this[n++] = this[i];
        }
    }
    this.length -= 1
}