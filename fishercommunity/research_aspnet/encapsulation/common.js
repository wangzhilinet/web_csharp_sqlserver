/*
打开对话框
url:页面地址
dialogWidth:对话框宽度
dialogHeight:对话框高度
arguments:参数
refresh:是否刷新
*/
function openDialog(url, dialogWidth, dialogHeight, arguments, refresh) {
    //添加时间戳
    if (url.indexOf('?') < 0) {
        url += "?ts=" + Math.random();
    }
    else {
        url += "&ts=" + Math.random();
    }
    var sFeatures = "dialogWidth:" + dialogWidth + "px;dialogHeight=" + dialogHeight + "px;status:no;help:no;resizable:yes;center:yes;unadorned:no;scroll:no";
    var returnValue = window.showModalDialog(url, arguments, sFeatures);
    if (returnValue == undefined || returnValue == null) {
        returnValue = window.returnValue;
    }
    if (refresh) {
        window.location = '#';
    }
    return returnValue;
}
/*
打开框架对话框
*/
function openFrameDialog(url, dialogWidth, dialogHeight, arguments, refresh) {
    //添加时间戳
    if (url.indexOf('?') < 0) {
        url += "?ts=" + Math.random();
    }
    else {
        url += "&ts=" + Math.random();
    }
    if (arguments == null) {
        arguments = new Object();
    }
    arguments.url = url;
    var sFeatures = "dialogWidth:" + dialogWidth + "px;dialogHeight=" + dialogHeight + "px;status:no;help:no;resizable:yes;center:yes;unadorned:no;scroll:no";
    var returnValue = window.showModalDialog("../Common/FrameDialog.aspx", arguments, sFeatures);
    if (refresh) {
        window.location = '#';
    }
    return returnValue;
}
/*
打开文件上传对话框
*/
function openUploadDialog(uploadDirectory, fileNameElementID, filePathElementID) {
    var url = "../Common/UploadFile.aspx";
    var uploadFile = openDialog(url, 500, 200);
    if (uploadFile != null) {
        if (fileNameElementID != null) {
            document.getElementById(fileNameElementID).value = uploadFile.FileName;
        }
        if (filePathElementID != null) {
            document.getElementById(filePathElementID).value = uploadFile.FilePath;
        }
    }
    else {
        if (fileNameElementID != null) {
            document.getElementById(fileNameElementID).value = "";
        }
        if (filePathElementID != null) {
            document.getElementById(filePathElementID).value = "";
        }
    }
    return uploadFile;
}
/*
打开文本输入框
*/
function openPrompt(message, defaultContent) {
    var sFeatures = "dialogWidth:500px;dialogHeight=170px;status:no;help:no;resizable:yes;center:yes;unadorned:no;scroll:no";
    var url = "../Common/Prompt.aspx?Message=" + escape(message) + "&DefaultContent=" + escape(defaultContent) + "&ts=" + Math.random();
    var returnValue = window.showModalDialog(url, null, sFeatures);
    return returnValue;
}
/*
打开窗口
*/
function openWindow(url, width, height, replace) {
    if (url.indexOf('?') < 0) {
        url += "?ts=" + Math.random();
    }
    else {
        url += "&ts=" + Math.random();
    }
    var sFeatures = "location=no,menubar=no,status=no,scrollbars=yes, resizable=yes,width=" + width + ",height=" + height;
    return window.open(url, "", sFeatures, replace);
}
/*
关闭对话框
*/
function closeDialog() {
    top.close();
}
/*
取消对话框
*/
function cancelDialog() {
    window.returnValue = false;
    window.close();
    top.close();
    return false;
}
/*
打印页面
*/
function printPage(pageUrl) {
    //打开一个空页面
    var win = openWindow(pageUrl, 1024, 600);
    //移动到指定位置
    win.moveTo(100, 100);
    //打印页面
    win.print();
}

/*
取两位小数
*/
function formatAmount(amount) {
    var formatAmount = Math.round(parseFloat(amount) * 100) / 100.0;
    return formatAmount;
}


//****************************************************************
//* 名　　称：DataLength
//* 功    能：计算数据的长度
//* 输入参数：fData：需要计算的数据
//* 输出参数：返回fData的长度(Unicode长度为2，非Unicode长度为1)
//*****************************************************************
function DataLength(fData) {
    var intLength = 0
    for (var i = 0; i < fData.length; i++) {
        if ((fData.charCodeAt(i) < 0) || (fData.charCodeAt(i) > 255))
            intLength = intLength + 2
        else
            intLength = intLength + 1
    }
    return intLength
}

//****************************************************************
//* 名　　称：IsEmpty
//* 功    能：判断是否为空
//* 输入参数：fData：要检查的数据
//* 输出参数：True：空                              
//*           False：非空
//*****************************************************************
function IsEmpty(fData) {
    return ((fData == null) || (fData.length == 0))
}


//****************************************************************
//* 名　　称：IsDigit
//* 功    能：判断是否为数字
//* 输入参数：fData：要检查的数据
//* 输出参数：True：是0到9的数字                              
//*           False：不是0到9的数字 
//*****************************************************************
function IsDigit(fData) {
    return ((fData >= "0") && (fData <= "9"))
}


//****************************************************************
//* 名　　称：IsInteger
//* 功    能：判断是否为正整数
//* 输入参数：fData：要检查的数据
//* 输出参数：True：是整数，或者数据是空的                            
//*           False：不是整数
//*****************************************************************
function IsInteger(fData) {
    //如果为空，返回true
    if (IsEmpty(fData))
        return true
    if ((isNaN(fData)) || (fData.indexOf(".") != -1) || (fData.indexOf("-") != -1))
        return false

    return true
}

//****************************************************************
//* 名　　称：IsEmail
//* 功    能：判断是否为正确的Email地址
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的Email地址，或者空                              
//*           False：错误的Email地址
//*****************************************************************
function IsEmail(fData) {
    if (IsEmpty(fData))
        return true
    if (fData.indexOf("@") == -1)
        return false
    var NameList = fData.split("@");
    if (NameList.length != 2)
        return false
    if (NameList[0].length < 1)
        return false
    if (NameList[1].indexOf(".") <= 0)
        return false
    if (fData.indexOf("@") > fData.indexOf("."))
        return false
    if (fData.indexOf(".") == fData.length - 1)
        return false

    return true
}

//****************************************************************
//* 名　　称：IsPhone
//* 功    能：判断是否为正确的电话号码（可以含"()"、"（）"、"+"、"-"和空格）
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的电话号码，或者空                              
//*           False：错误的电话号码
//* 错误信息：
//*****************************************************************
function IsPhone(fData) {
    var str;
    var fDatastr = "";
    if (IsEmpty(fData))
        return true
    for (var i = 0; i < fData.length; i++) {
        str = fData.substring(i, i + 1);
        if (str != "(" && str != ")" && str != "（" && str != "）" && str != "+" && str != "-" && str != " ")
            fDatastr = fDatastr + str;
    }
    //alert(fDatastr);  
    if (isNaN(fDatastr))
        return false
    return true
}

//****************************************************************
//* 名　　称：IsPositiveNumeric
//* 功    能：判断是否为正确的正数（可以含小数部分）
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的正数，或者空                              
//*           False：错误的正数
//* 错误信息：
//*****************************************************************
function IsPositiveNumeric(fData) {
    if (IsEmpty(fData))
        return true
    if ((isNaN(fData)) || (fData.indexOf("-") != -1))
        return false
    if (parseFloat(fData) <= 0) {
        return false;
    }
    return true
}
//****************************************************************
//* 名　　称：IsNegativeNumeric
//* 功    能：判断是否为正确的负数（可以含小数部分）
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的负数，或者空                              
//*           False：错误的负数
//* 错误信息：
//*****************************************************************
function IsNegativeNumeric(fData) {
    if (IsEmpty(fData))
        return true
    if ((isNaN(fData)) || (fData.indexOf("-") == -1))
        return false
    if (parseFloat(fData) >= 0) {
        return false;
    }
    return true
}
//****************************************************************
//* 名　　称：IsNotNegativeNumeric
//* 功    能：判断是否为正确的非负数（可以含小数部分）
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的负数，或者空                              
//*           False：错误的负数
//* 错误信息：
//*****************************************************************
function IsNotNegativeNumeric(fData) {
    if (IsEmpty(fData))
        return true
    if ((isNaN(fData)) || (fData.indexOf("-") != -1))
        return false
    if (parseFloat(fData) < 0) {
        return false;
    }
    return true
}

//****************************************************************
//* 名　　称：IsNumeric
//* 功    能：判断是否为正确的数字（可以为负数，小数）
//* 输入参数：fData：要检查的数据
//* 输出参数：True：正确的数字，或者空                              
//*           False：错误的数字
//* 错误信息：
//*****************************************************************
function IsNumeric(fData) {
    if (IsEmpty(fData))
        return true
    if (isNaN(fData))
        return false
    return true
}


//****************************************************************
//* 名　　称：IsIntegerInRange
//* 功    能：判断一个数字是否在指定的范围内
//* 输入参数：fInput：要检查的数据
//*           fLower：检查的范围下限，如果没有下限，请用null
//*           fHigh：检查的上限，如果没有上限，请用null
//* 输出参数：True：在指定的范围内                              
//*           False：超出指定范围
//*****************************************************************
function IsIntegerInRange(fInput, fLower, fHigh) {
    if (fLower == null)
        return (fInput <= fHigh)
    else if (fHigh == null)
        return (fInput >= fLower)
    else
        return ((fInput >= fLower) && (fInput <= fHigh))
}