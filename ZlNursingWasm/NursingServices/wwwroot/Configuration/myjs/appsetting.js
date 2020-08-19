function GetfatherKey(){
    $.ajax({
        url: const_host + "/api/Config/GetNodeKeys",
        data: "[]",
        type: "post",
        contentType: "application/json",
        success: function (res) {
            var html = '';
            for (let i = 0; i < res.length; i++) {
                var arr = [];
                arr.push(res[i]);
                html += '<div class="list-group-item list-item-lg setitme" data-res=' + JSON.stringify(arr) + ' data-title="' + res[i] + '">' + res[i] + '</div>';
            }
            $('.fatherKey').html(html);
        },
        error: function (e) {
        }
    });
}

function GeChildrenKey(arrobj,title){
    $.ajax({
        url: const_host + "/api/Config/GetNodeKeys",
        data: JSON.stringify(arrobj),
        type: "post",
        contentType: "application/json",
        success: function (res) {
            if (res.length == 0)
            {
                GetNodeKeyValue(arrobj,title);
                //查询值
                return;
            }
            var html = `<div class="col-md-2 col-lg-2">
            <div class="panel">
                <div class="panel-heading">
                    <h3 class="panel-title">${title}</h3>
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        `;
            for (let i = 0; i < res.length; i++) {
                var arr = arrobj.concat();
                arr.push(res[i]);
                html += '<div class="list-group-item list-item-lg setitme" data-res=' + JSON.stringify(arr) + ' data-title="' + res[i] + '">' + res[i] + '</div>';
            }
            html +=
                `
                    </div>
                </div>
            </div>
        </div>`;
        $('.eq-height').append(html);
        },
        error: function (e) {
        }
    })
}

function GetNodeKeyValue(arrobj,title) {
    $.ajax({
        url: const_host + "/api/Config/GetNodeKeyValue",
        data: JSON.stringify(arrobj),
        type: "post",
        contentType: "application/json",
        success: function (res) {
            if (!res.start) return;
            var value = res.msg;
            var html = `<div class="col-md-2 col-lg-2">
            <div class="panel">
                <div class="panel-heading">
                    <h3 class="panel-title">${title}</h3>
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <div class="input-group mar-btm">
                            <textarea class="updatetxt" type="text" data-res=${JSON.stringify(arrobj)} rows="5" placeholder="" class="form-control" style="resize:vertical" >${value}</textarea>
                            <span class="input-group-btn">
                                <button class="btn btn-mint btn-update" type="button">修改</button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
        $('.eq-height').append(html);
        },
        error: function (e) {
        }
    });
}

//启动事件开始
$(function(){
    GetfatherKey();
    $('body').on('click','.setitme',function(){
        $(this).parent().find('.setitme').removeClass('active');
        $(this).addClass('active');
        var arr = JSON.parse($(this).attr('data-res'));
        var res = $(this).attr('data-title');
        var col = $(this).parents('.panel').parent();
        col.nextAll().remove();
        GeChildrenKey(arr,res);
    });

    $('body').on('click','.btn-update',function(){
        var txtobj = $(this).parent().parent().find('.updatetxt');
        var txt = txtobj.val();
        var strJson = txtobj.attr('data-res');
        $.ajax({
            url: const_host + "/api/Config/SetNodeKeyValue?value=" + encodeURI(txt),
            data: strJson,
            type: "post",
            contentType: "application/json",
            success: function (res) {
                if (res.start){
                    
                }
            },
            error: function (e) {
            }
        });
        debugger
    });
});
//启动事件结束