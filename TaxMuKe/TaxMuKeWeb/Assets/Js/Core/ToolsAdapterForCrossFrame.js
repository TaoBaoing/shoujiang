//成功信息提示框（自动消失）  
function showSuccessMsg(msg) {
    parent.showSuccessMsg(msg);
}
//警告信息提示框（自动消失）  
function showWarningMsg(msg) {
    parent.showWarningMsg(msg)
}
//错误信息提示框（自动消失）  
function showErrorMsg(msg) {
    parent.showErrorMsg(msg)
}
//信息提示框（自动消失）  
function showInfoMsg(msg) {
    parent.showInfoMsg(msg)
}
//信息提示框
function alertMsg(msg) {
    parent.alertMsg(msg);
}
//确认
function confirmMsg(msg, callback) {
    parent.confirmMsg(msg, callback);
}
//确认后进行ajax操作
function confirmAfterDoAjax(msg, url, data, callback) {
    parent.confirmAfterDoAjax(msg, url, data, callback);
}
//删除确认框
function confirmDelete(url, data, callback) {
    parent.confirmDeleteParent(url, data, callback);
}
//修改确认框
function confirmUpdate(url, data, callback) {
    parent.confirmUpdate(url, data, callback);
}
//弹出模态框
function showModalDialog(title, content, buttons, width, height) {
    parent.showModalDialog(title, content, buttons, width, height);
}
//提交成功
function showSubmitResult(e) {
    parent.showSubmitResult(e);
}
//弹出数据模态框框
function showDataFormModalDialog(url, title, width, height) {
    parent.showDataFormModalDialog(url, title, width, height);
}
