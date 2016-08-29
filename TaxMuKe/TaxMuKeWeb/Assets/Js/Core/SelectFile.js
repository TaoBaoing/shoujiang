//选择文件

var selectFileCallback = null;

function insetImages() {
  
    selectFileCallback = function (linkurl) {
        um.execCommand('insertHtml', "&nbsp;<img  src='/Upload/" + linkurl + "'/>")
    }
    showDialog('/Material/List?materialType=Images&viewName=SelectFilesNew&loadTaxonomys=true', '选择');
}
function insertAudio() {
    selectFileCallback = function (linkurl) {
        um.execCommand('insertHtml', "&nbsp;<embed  src='/Upload/" + linkurl + "'></embed>")
    }
    showDialog('/Material/List?materialType=Audio&viewName=SelectFilesNew&loadTaxonomys=true', '选择');
}
function insertVideo() {
    selectFileCallback = function (linkurl) {
        um.execCommand('insertHtml', "&nbsp;<video  src='/Upload/" + linkurl + "' controls='controls'></video>")
    }
    showDialog('/Material/List?materialType=Video&viewName=SelectFilesNew&loadTaxonomys=true', '选择');
}
function insertOtherFile() {

    selectFileCallback = function (linkurl) {
        um.execCommand('insertHtml', "&nbsp;<a  href='/Upload/" + linkurl + "'>附件</a>")
    }
    showDialog('/Material/List?materialType=OtherFile&viewName=SelectFilesNew&loadTaxonomys=true', '选择');

}
//实例化编辑器
var um;
$(function () {
    um = UM.getEditor('myEditor');
    // um.setContent('Html.Raw(Model.Content) ')

})

function doSubmit() {
    var content = um.getContent();
    $("#Content").val(content);
    return true;
}

function GetContent() {
    var content = _escape(um.getContent());
    document.getElementById("Content").value = content;
    return true;
}

// 编码字符串
function _escape(val) {
    return val.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
}

function showDialog(url, title) {

    var buttons = [
        {
            value: '关闭'
        }
    ]

    $.get(url, function (content) {
        var d = parent.dialog({
            title: title,
            content: content,
            button: buttons,
            width: 1000,
            height: 600
        });

        d.showModal();
    });


}



