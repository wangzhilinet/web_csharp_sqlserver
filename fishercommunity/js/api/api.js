/*
调用接口
*/
function call(serviceName, data, successCallback,errorCallback) {
    var requestParameters = new Object();
    requestParameters.service_name = serviceName;
    requestParameters.data = JSON.stringify(data);
    url = "/Services/Proxy.ashx?r=" + Math.random();
    $.ajax({
        url: url,
        dataType: "json",
        data: requestParameters,
        success: function (result) {
            //showMessage(JSON.stringify(result));
            if (result.success) {
                successCallback(result);
            }
            else {
                if (errorCallback != null) {
                    errorCallback(result);
                }
                else {
                    showMessage(result.error_message);
                }
                //showMessage("调用服务【" + serviceName + "】出错:" + result.error_message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(errorThrown);
        }
    });
}
