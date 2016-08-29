//弹层自动消失
function showMsg(msg, type, callback) {
    var d = dialog({
        content: msg,
        onremove: callback
    });
    d.show();
    //var bgcolor = "#EBEBEB";
    //switch (type) {
    //    case 1:
    //        bgcolor = "#9AFF9A";
    //        break;
    //    case 2:
    //        bgcolor = "#EEEE00";
    //        break;
    //    case 3:
    //        bgcolor = "#FF4040";
    //        break;
    //    case 4:
    //        bgcolor = "#FFFACD";
    //        break;
    //}

    //$("div[i='dialog']").css("background-color", bgcolor);
    setTimeout(function () {
        d.close().remove();
    }, 2000);
}
//成功信息提示框（自动消失）  
function showSuccessMsg(msg, callback) {
    if (msg) {
        showMsg(msg, 1, callback)
    }
    else {
        showMsg("操作成功", 1, callback)
    }

}
//警告信息提示框（自动消失）  
function showWarningMsg(msg) {
    showMsg(msg, 2)
}
//错误信息提示框（自动消失）  
function showErrorMsg(msg, callback) {
    if (msg) {
        showMsg(msg, 3, callback)
    }
    else {
        showMsg("操作失败", 3, callback)
    }
}
//信息提示框（自动消失）  
function showInfoMsg(msg) {
    showMsg(msg, 4)
}

//信息提示框
function alertMsg(msg) {

    var d = dialog({
        title: '提示',
        content: msg
    });
    d.show();
}

//确认
function confirmMsg(msg, callback) {

    var d = dialog({
        title: '确认',
        content: msg,
        okValue: '确定',
        ok: callback,
        cancelValue: '取消',
        cancel: function () { }
    });
    d.show();
}
//确认后进行ajax操作
function confirmAfterDoAjax(msg, url, data, callback) {

    confirmMsg(msg, function() {
        $.ajax({
            url: url,
            data: data,
            type: "POST",
            dataType: "json"
        }).done(function(d) {
            if (callback) {
                callback(d);
            }
        }).fail(function() {
            showErrorMsg("网络繁忙，请稍后再试");
        });
    });
}
//删除确认框
function confirmDeleteParent(url, data, callback) {
    confirmAfterDoAjax("确定要删除此记录吗?", url, data, callback);
}
//修改确认框
function confirmUpdate(url, data, callback) {
    confirmAfterDoAjax("确定要保存更改吗?", url, data, callback);
}


//弹出模态框
function showModalDialog(title, content, buttons, width, height) {

    var d = dialog({
        title: title,
        content: content,
        button: buttons
        //width: width,
        //height: height

    });
    d.showModal();
}

//提交完成
function showSubmitResult(e, textStatus, jqXHR, callback) {
    if (e.IsSuccess) {
        showSuccessMsg("操作成功", function () {
            if (callback) {
                callback(e);
            }
            else {
                window.frames["workspace"].location.reload();
            }
        });
    }
    else {
        showErrorMsg(e.Message);
    }
}
//弹出数据模态框框
function showDataFormModalDialog(url, title, width, height) {

    var buttons = [
        {
            value: "确定",
            autofocus: true,
            callback: function () {
                $("#editForm").submit();
            }
        },
        {
            value: '取消'
        }
    ]

    $.get(url, function (content) {
        showModalDialog(title, content, buttons, width, height)
    });
}
