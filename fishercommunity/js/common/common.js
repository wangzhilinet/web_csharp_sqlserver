//common脚本
//创建网页cookie 其中第一个值为 cookie名称，第二个为 cookie值 ，expires：9999（期限：天） path：'/' 全局目录，所有网页共享这个cookie
        $.cookie("cookieName", "cookieValue", { expires: 9999, path: '/' });
//        $(function () {
//            alert($.cookie("cookieName"));
//        })
        $.cookie("theme", "light", { expires: 9999, path: '/' });
        $("#LinkThemeCss").attr("href", "../css/dark/" + theme + "/wordstudy.css");
