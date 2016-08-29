
//By  chaiyonglian
///加载课程类别
function DropBind(datatree, id, val) {
    //$("#myselect").prepend("<option value='0'>请选择</option>");
    //$("#myselect").val(0);
    // document.getElementById("Mmonth").options.add(new Option(1, 1));
    //加载type  tree
    var data = new Array();
    //var array = @Html.Raw(Json.Encode(@ViewBag.taxonomyTypes));
    var array = datatree;
    for(var i =0; i<array.length;i++){
        data[i] = array[i];
    } 
    function TreeSelector(item, data, rootId) {
        this._data = data;
        this._item = item;
        this._rootId = rootId;
    }
    TreeSelector.prototype.createTree = function () {
        var len = this._data.length;
        for (var i = 0; i < len; i++) {
            if (this._data[i].pid == this._rootId) {
                this._item.options.add(new Option("" + this._data[i].text, this._data[i].id));
                for (var j = 0; j < len; j++) {
                    this.createSubOption(len, this._data[i], this._data[j]);

                }
            }
        }
    }
    TreeSelector.prototype.createSubOption = function (len, current, next) {
        var blank = "";
        if (next.pid == current.id) {
            intLevel = 0;
            var intlvl = this.getLevel(this._data, this._rootId, current);
            for (a = 0; a < intlvl; a++)
                blank += "";
            blank += "　　├-";
            this._item.options.add(new Option(blank + next.text, next.id));

            for (var j = 0; j < len; j++) {
                this.createSubOption(len, next, this._data[j]);
            }
        }
    }
    TreeSelector.prototype.getLevel = function (datasources, topId, currentitem) {

        var pid = currentitem.pid;
        if (pid != topId) {
            for (var i = 0 ; i < datasources.length; i++) {
                if (datasources[i].id == pid) {
                    intLevel++;
                    this.getLevel(datasources, topId, datasources[i]);
                }
            }
        }
        return intLevel;
    }
    var ts = new TreeSelector(document.getElementById(id), data, 0);
    ts.createTree();
    //添加 默认选项
    document.getElementById(id).options.add(new Option("请选择", 0), 0);
    //默认选中项
    if (val=="" && val==null )
    {
        val = 0;
    }
    var all_options = document.getElementById(id).options;
    for (i = 0; i < all_options.length; i++) {
        if (all_options[i].value == val) // 根据option标签的value来进行判断 测试的代码这里是两个等号
        {
            all_options[i].selected = true;
        }
    }
}