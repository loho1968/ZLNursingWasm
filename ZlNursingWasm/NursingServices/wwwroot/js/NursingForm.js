/**
 * 表单项目初始化
 * */
//单选组合项目的选项值
let ComboItemValue = { "是": "373066001", "否": "373067005" };
let FormScoreList = {};
let TaskType = "TFORM";
let FormLayout = [];
let phraseType = [];
let phraseList = [];
let lastGetPhraseTime;
function InitFormItem() {

    //todo:修改表单是观察时间需要设置

    Date.prototype.Format = function (fmt) {
        //author: meizz
        var o = {
            "M+": this.getMonth() + 1, //月份
            "d+": this.getDate(), //日
            "h+": this.getHours(), //小时
            "m+": this.getMinutes(), //分
            "s+": this.getSeconds(), //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            S: this.getMilliseconds() //毫秒
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(
                RegExp.$1,
                (this.getFullYear() + "").substr(4 - RegExp.$1.length)
            );
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt))
                fmt = fmt.replace(
                    RegExp.$1,
                    RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length)
                );
        return fmt;
    };

    Date.prototype.AddDays = function (offset) {
        return new Date(
            new Date(this.getTime() + offset * 24 * 60 * 60 * 1000).Format(
                "yyyy-MM-dd hh:mm:ss"
            )
        );
    };


    $("body").on("click", ".itemLov:radio", function (e) {
        return RadioClick(e.target);
    });



    //病情观察词句本地存储 
    SavePhrase()

    if (
        formInfo.EditType === "insert" && (ValueIsNull(formInfo.Pid) || ValueIsNull(formInfo.Pvid))
    ) {
        layer.msg("病人ID【" +
            formInfo.Pid +
            "】不正确，或者病人就诊ID【" +
            formInfo.Pvid +
            "】不正确", { icon: 5, time: 3000, offset: 'rt' });

        //禁止编辑 
        $("input").attr("disabled", true);

        $(".saveout").attr("disabled", true);
        $(".savecontinue").attr("disabled", true);
    }


    //非新增，隐藏连续增加按钮

    if (formInfo.EditType != "insert") {
        $(".savecontinue").hide();
    }
    //样式调整：label

    $(".form_label").each(function () {
        let height = $(this).parent().height();
        $(this).height(height - 8);
        $(this).css("line-height", height - 8 + "px");
    })

    //标签、选项文字长了了，用tooltip显示完整内容

    $('[data-toggle="tooltip"]').tooltip()
    //日期显示

    $(".flatpickr").flatpickr({
        defaultDate: new Date()//默认当前时间
    });


    $(".selectpicker").selectpicker({
        noneSelectedText: '请选择:' //默认显示内容  
    });

    //点击其他 
    $(".choosing").click(function () {
        ClickOtherLov(this);
    })


    //显示历史值
    $("body").on("click", ".history", function () {
        let str = $(this).attr("data-his");
        if (str == undefined || str == "null") {
            return;
        }
        let data = eval('(' + str + ')');
        let html = "";
        html = '<table class="table table-striped table-bordered">';
        html = html + "<thead><tr><th>项目值</th><th style='width:35%'>变动时间</th><th style='width:16%'>修改者</th></tr></thead>";
        html = html + "<tbody>";
        for (let i = 0; i < data.length; i++) {
            html += "<tr>";
            html += "<td>" + data[i].Value + "</td>";
            html += "<td>" + data[i].EditTime + "</td>";
            html += "<td>" + data[i].RecorderName + "</td>";
            html += "</tr>";
        }

        html = html + "</tbody></table>";
        layer.tips(html, this, {
            area: ['500px', 'auto'],
            tips: [1, '#e5f6f5']
        });
    })


    SetLastValueCount();

    $("body").on("click", ".lastValues", function () {
        ShowItemToDayList($(this));
    });

}

/**
 * 点击其他选项
 * @param {object} e
 */
function ClickOtherLov(e) {
    let item = $(e);
    //其他
    if (item.attr("data-other") == "1" || item.val().indexOf("其他") >= 0) {
        if (item.prop('checked') == true) {
            item.parent().find(".other").show();
        }
        else {
            item.parent().find(".other").show();
            item.parent().find(".other").val("");
        }
    }
    else {
        //单选选中其他选项
        if (item.attr("type") == "radio") {
            item.parent().parent().find(".other").val("");
            item.parent().parent().find(".other").hide();
        }
    }
}
/**
 * 点击上次值后，给项目赋值，护理记录单中使用
 * @param {object} e
 */
function ClickShowToDayValue(e) {

    //todo:组合项目的值赋值需要单独处理
    let item = $(e);
    let data = item.attr("data-lastvalue");
    if (data == undefined || data == "null")
        return;
    let obj = eval('(' + data + ')');
    item = item.parent().find(".itemLov");
    let type = item.attr("data-type");


    switch (type) {
        case "文字输入": {
            item.val(obj.Value);
            break;
        }
        case "数字输入": {
            item.val(obj.Value);
            break;
        }
        case "文本框输入": {
            item.val(obj.Value);
            break;
        }
        case "日期选择": {
            item.val(obj.Value);
            break;
        }
        case "时间选择": {
            item.val(obj.Value);
            break;
        }
        case "平铺多选": {
            item.each(function () {
                if (obj.Value.indexOf(item.val()) >= 0) {
                    item.attr("checked", "checked")
                }
                return false;
            })
            break;
        }
        case "按钮单选": {
            item.each(function () {
                if (item.val() == obj.Value) {
                    item.attr("checked", "checked")
                }
                return false;
            })
            break;
        }
        case "平铺单选": {
            item.each(function () {
                if (item.val() == obj.Value) {
                    item.attr("checked", "checked")
                }
                return false;
            })
            break;
        }
        case "下拉单选": {
            item.selectpicker('val', obj.Value);
            break;
        }
        case "下拉多选": {
            let arr = obj.Value.split(":");//暂且多选使用“:”分割
            item.selectpicker('val', arr);
            break;
        }
        case "单选组合": {
            //todo:上次值，单选组合赋值
            console.log("还未处理")
        }
    }
}
/**
 * @description 计算评分表分数
 * @pata {string} itemId：项目id, {string} formId表id
 */
//表单分数计算
function CalculationFormScore(itemId, formId) {
    let totalScore;
    let objForm;

    if (formId == undefined || formId == null) {
        formId = $("#a_" + itemId).attr("data-formId");
    }

    //非评分表，不计算
    if ($("#a_" + formId).attr("data-formtype") != 'r') {
        return;
    }
    for (let i = 0; i < formLists.length; i++) {
        if (formLists[i].FormId == formId) {
            objForm = formLists[i];
            break;
        }
    }
    if (objForm == undefined) {
        console.log('未找到表单：' + formId);
        return;
    }
    let formScore = objForm.FormScores;
    let objItems = objForm.FormItems;
    $("#a_" + formId)
        .find(".itemLov:checked:not(.niList)")
        .each(function () {
            let score = this.getAttribute("data-score");
            if (score != undefined) {
                if (isNaN(totalScore)) {
                    totalScore = Number(score);
                } else {
                    totalScore = totalScore + Number(score);
                }

            }
        });
    let objScore = {};
    let scoreDisplay;
    if (isNaN(totalScore)) {
        objScore["total_score"] = "";
        scoreDisplay = "未评！";
    } else {
        objScore["total_score"] = totalScore;
        scoreDisplay = "总分：" + totalScore;
    }
    objScore["scale_result_id"] = "";
    objScore["score_risk"] = "未评";
    objScore["result_exp"] = "";
    let riskClass = "normal";
    let scoreObj = document.getElementById("a_" + formId + "_score");
    scoreObj.parentNode.parentNode.classList.remove("highrisk");
    scoreObj.parentNode.parentNode.classList.remove("middleRisk");
    scoreObj.parentNode.parentNode.classList.remove("lowRisk");
    scoreObj.parentNode.parentNode.classList.add(riskClass);

    if (!isNaN(totalScore)) {
        for (let i = 0; i < formScore.length; i++) {
            let expr = formScore[i].Expr;
            if (expr != "" && expr != null) {
                let form_score = totalScore;
                let equql = eval(expr);
                if (equql) {
                    objScore["scale_result_id"] = UndefinedStringToSpace(formScore[i].ResultObservItemId);
                    objScore["score_risk"] = formScore[i].RiskLevel;
                    objScore["result_exp"] = UndefinedStringToSpace(formScore[i].ResultExp);

                    if (highRisk.indexOf(formScore[i].DetailCode) > -1) {
                        riskClass = "highrisk"
                    } else if (middleRisk.indexOf(formScore[i].DetailCode) > -1) {
                        riskClass = "middlerisk"
                    } else if (lowRisk.indexOf(formScore[i].DetailCode) > -1) {
                        riskClass = "lowrisk"
                    } else { riskClass = "normal" };
                    scoreObj.parentNode.parentNode.classList.add(riskClass);
                    scoreDisplay = scoreDisplay + "，风险：" + formScore[i].RiskLevel;
                    if (objScore["result_exp"] != null && objScore["result_exp"] != "") {
                        scoreDisplay += "：" + objScore["result_exp"];
                    }
                    if (formScore[i].ResultExp != null) {
                        scoreDisplay + "，评价：" + formScore[i].ResultExp;
                    }
                    break;
                }
            }
        }
    }
    FormScoreList[formId] = objScore;
    let scoreLabel = `<div id="${objForm.FormId}_score" style="font-size:18px;color: rgb(0, 0, 0);">评分结果【${scoreDisplay} 】</div>`;
    $("#a_" + formId + "_score")
        .html(" 【" + scoreDisplay + "】");
    HideFormNiList(totalScore, formId);
}

/**
 * @description 根据评分结果显示或者隐藏措施项目
 * @param {string} id 表单ID
 */
function HideFormNiList(form_score, formID) {
    let hideAll = true;
    $("#a_" + formID)
        .find(".item_niList")
        .each(function () {
            let expr = $(this).attr("expr");
            let isHide = expr == null ? false : true;
            let id = $(this).id;
            if (form_score !== undefined && eval(expr)) {
                isHide = false;
                hideAll = false;
            }
            if (isHide) {
                $(this).attr("disabled", true);
                $(this).prop("checked", false);
            } else {
                $(this).attr("disabled", false);
            }
        });
    if (hideAll) {
        if ($(".open").attr("data-open") == 1) {
            $(".open").click();
        }
    } else {
        if ($(".open").attr("data-open") == 0) {
            $(".open").click();
        }
    }
}


/**
 * 按钮单选点击，判断处理再次点击选中项目时，取消选中
 * @param {any} e
 */
function RadioClick(e) {
    let itemid = e.getAttribute("data-itemid");
    let dataId = e.id;
    if ($(e).parent().parent().data("checkedId") == dataId && e.checked) {
        e.checked = false;
        $(e).parent().parent().data("checkedId", "zlsfot");
    } else {
        $(e).parent().parent().data("checkedId", dataId);
    }
    return CalculationFormScore(itemid);
}
/**
 * @description 绑定到checkbox项目上的变动事件
 * @param {object} item 项目对象
 */
function ChangeCheckbox(e) {
    let itemId = $(e).attr("data-itemid");
    CalculationFormScore(itemId);
}

/**
*初始化计算表单，修改表单时为项目赋值，并计算评分表分数
*/
function InitForm() {


    for (let i = 0; i < formLists.length; i++) {
        let formId = formLists[0].FormId;
        //为项目赋值
        for (let j = 0; j < formLists[i].FormItems.length; j++) {
            let item = $("#a_" + formLists[i].FormItems[j].ItemId);
            let type = item.attr("data-type");

            SetItemValue(item, formLists[i].FormItems[j].DetailValue)

            //护理记录单中的病情观察
            if (formMajorType == 'nurse') {
                $("#nursingstep").val(UnEscape(formLists[i].NursingStep));
            }

            if (formInfo.EditType == 'update') {

                //修改表单时，才需要显示项目的修改痕迹   
                if (formLists[i].FormItems[j].HistoryValues != null && formLists[i].FormItems[j].HistoryValues.length > 0) {
                    let itemHistory = $("#a_" + formLists[i].FormItems[j].ItemId + "_history");
                    itemHistory.show();
                    //todo:修改为不把HistoryValues保存在Html中，而且从前端的对象中获取
                    itemHistory.attr("data-his", JSON.stringify(formLists[i].FormItems[j].HistoryValues));
                }
            }

            //处理原来是普通项目单选项目，后来调整设计变成单选组合项目，根据值，把单选组合中的项目选中   
            if (formLists[i].FormItems[j].InputType != null && formLists[i].FormItems[j].InputType.indexOf("单选") > -1 && formLists[i].FormItems[j].DetailValue == "是") {
                $("#a_" + formLists[i].FormItems[j].ItemId).attr("checked", true);
            }
            //处理单选组合项目
            if (formLists[i].FormItems[j].PatScaleFormComboItemDetails == null) {
                continue;
            }
            for (let p = 0; p < formLists[i].FormItems[j].PatScaleFormComboItemDetails.length; p++) {
                if (formLists[i].FormItems[j].PatScaleFormComboItemDetails[p].DetailSourceValue == ComboItemValue.是) {
                    $("#a_" + formLists[i].FormItems[j].PatScaleFormComboItemDetails[p].ScaleItemId).attr("checked", true);
                    //以前单选组合项目，现在可能是单独的项目了，所以进行一次赋值处理 
                    $("#a_" + formLists[i].FormItems[j].PatScaleFormComboItemDetails[p].DetailItemId).attr("checked", "checked");
                }
            }
        }
        if (formLists[i].FormType == 'r') {
            //评分表，计算分数  
            CalculationFormScore(null, formLists[i].FormId);
        }
        SetFormNiList(formLists[i].PatFormNiLists);
    }
}
/**
 * 修改或者查看表单时，设置病人表中的护理措施值
 * @param {any} formNiList
 */
function SetFormNiList(formNiList) {
    if (formNiList == null) return;
    for (let i = 0; i < formNiList.length; i++) {
        let item = $("#a_" + formNiList[i].NiListId);
        item.attr("checked", "checked");
    }
}
/**
 * 设置项目的值
 **/
function SetItemValue(item, value) {
    if (value == undefined) {
        return;
    }
    let itemId = item.attr("data-itemid");
    let type = item.attr("data-type");

    let otherValue = UnEscape(value.split('||')[1]);
    if (otherValue != undefined && otherValue != null) {
        value = UnEscape(value.split('||')[0]);
        $("#a_" + itemId + "_other").val(otherValue);
    } else {
        //有其他选项值的内容，把其他内容显示处理
    }

    switch (type) {
        case "文字输入": {
            item.find("input").val(value);
            break;
        }
        case "数字输入": {
            item.find("input").val(value);
            break;
        }
        case "文本框输入": {
            item.find("textarea").val(value);
            break;
        }
        case "时间选择": {
            item.find("input").val(new Date(value).Format('yyyy-MM-ddThh:mm'));
            break;
        }
        case "日期选择": {
            item.find("input").val(new Date(value).Format('yyyy-MM-ddThh:mm'));
            break;
        }
        case "平铺多选": {
            let itemLov = $(item.parent().find(".itemLov"));
            itemLov.each(function () {
                if ((":" + value + ":").indexOf(":" + item.val() + ":") >= 0) {
                    $(this).attr("checked", "checked")
                }
                return false;
            })
            break;
        }
        case "平铺单选": {
            let itemLov = $(item.parent().find(".itemLov"));

            itemLov.each(function (i) {
                if ($(this).val() == value) {
                    $(this).attr("checked", "checked")
                    $("#a_" + itemId).data("checkedId", this.id)
                }

            })
            break;
        }
        case "按钮单选": {
            let itemLov = $(item.parent().find(".itemLov"));

            itemLov.each(function (i) {
                if ($(this).val() == value) {
                    $(this).attr("checked", "checked")
                    $("#a_" + itemId).data("checkedId", this.id)
                }

            })
            break;
        }
        case "下拉单选": {
            item.find("input").selectpicker('val', value);
            break;
        }
        case "下拉多选": {
            let arr = value.split(":");//暂且多选使用“:”分割 
            item.find("input").selectpicker('val', arr);
            break;

        }
        case "单选组合": {
            //单选组合在后面处理
            break;
        }
        default: {
            console.log("不支持类型:" + type);
        }
    }

}
/**
* @description 提交后清除项目值
**/
function ClearItemValue() {

    $(".iteminput").val("");
    $(".itemLov").each(function () {
        $(this).attr("checked", "checked");
        $(this).removeAttr("checked");
    });
    $(".formrow").find(".itemselect").each(function () {
        $(this).selectpicker("val", "");
    });
    $(".form").each(function () {
        CalculationFormScore("", this.id)
    })
}
/**
 * @description 项目值转义处理，用于处理文字项目中的双引号
 * @param {string} value
 */
function Escape(value) {
    if (value != undefined) {
        return value.replace(/"/g, '"');
    }
}
/**
 * @description 项目值反转义
 * @param {string} value
 */
function UnEscape(value) {
    if (value != undefined) {
        return value.replace(/\"/g, '"');
    }
}

//todo:护理记录单中的血压项目需要单独处理
/**
 * @description 获取血压项目的值
 * @param {Object} itemID 项目id
 */

function GetBloodPressureItemValue(itemId) {
    //血压项目

    let systolic = $("#a_" + itemId + "_systolic").val();
    let diastolic = $("#a_" + itemId + "_diastolic").val();
    if (systolic == undefined) {
        systolic = "";
    }
    if (diastolic == undefined) {
        diastolic = "";
    }
    let itemValue = systolic + "/" + diastolic;
    let errorText = "";
    let result = {
        error: false
    };

    if (
        (systolic !== "" && diastolic === "") ||
        (systolic === "" && diastolic !== "")
    ) {
        let item;
        if (systolic == null) {
            item = $(".systolic");
        } else {
            item = $(".diastolic");
        }
        layer.tips('收缩压和舒张压必须同时记录', item, {
            tips: [3, "red"]
        });
        result["error"] = true;
        itemValue = "";
    }
    if (itemValue == "/") {
        itemValue = "";
    }
    result["value"] = itemValue;
    return result;
}


/**
 * @description 获取字符串长度
 * @param {string} val 字符串
 */
function GetByteLen(val) {
    let len = 0;

    if (val == null) {
        return 0;
    }
    for (let i = 0; i < val.length; i++) {
        let a = val.charAt(i);
        if (a.match(/[^\x00-\xff]/gi) != null) {
            len += 2;
        } else {
            len += 1;
        }
    }
    return len;
}


/**
 * @description  获取radio checbox项目的值
 * @param {string} itemID 项目id
 * @param {Object} objItem 项目对象
 */
function GetCheckBoxItemValue(objItem) {
    let result = {};
    let itemDataID;
    let objItemData;
    let detail_value_id = "";
    let itemValue = "";

    $("#" + objItem.ItemId)
        .find(".itemLov:checked")
        .each(function () {
            itemDataID = this.id;
            detail_value_id = itemDataID.substring(2);
            if (itemValue === "") {
                itemValue = $(this).val();
            } else {
                itemValue = itemValue + ":" + $(this).val();
            }
        });
    result["item_value"] = itemValue;
    result["detail_value_id"] = detail_value_id;

    return result;
}

/**
 * @description 把undefined变量转空串
 * @param {string} str 字符变量
 */
function UndefinedStringToSpace(str) {
    if (typeof str == undefined || str == null) {
        return "";
    }
    return str;
}


/**
 * @description 判断项目值是否为空
 * @param {string} itemValue 项目值
 */
function ValueIsNull(itemValue) {
    if (itemValue == "" || itemValue == null || itemValue == "undefined") {
        return true;
    }
    return false;
}

/**
 * @description 获取表单头信息
 * @param {string} observ_time 观察时间
 */
function GetFormsHeader(objForm, observtime, formsResult, formType) {
    formsResult["form_type"] = objForm.FormType;
    if (objForm.FormType != 's') {
        formsResult["form_id"] = objForm.FormId;
    } else {
        //管道评估表单

        formsResult["form_id"] = formInfo.RecId;
    }
    formsResult["version"] = objForm.FormVersion;

    formsResult["scale_score"] = null;

    formsResult["scale_result_id"] = "";
    formsResult["risk_level"] = "";
    formsResult["result_exp"] = "";
    //评分表，有评分结果
    if (objForm.FormType == "r") {
        let formscore = FormScoreList[objForm.FormId];
        if (formscore == undefined || IsNullObject(formscore.total_score) || formscore.total_score === "") {
            layer.msg("【" + objForm.FormName + "】未评分，该评分表未保存", { icon: 5, time: 3000, offset: 'rt' });
            formsResult["error"] = true;
        } else {
            formsResult["scale_score"] = formscore.total_score;
            formsResult["scale_result_id"] = UndefinedStringToSpace(
                formscore.scale_result_id
            );
            formsResult["risk_level"] = formscore.score_risk;
            formsResult["result_exp"] = formscore.result_exp;
        }
    }

    //formsResult["rec_status"] = "";
    formsResult["pat_id"] = formInfo.Pid;
    formsResult["enc_type"] = formInfo.EncType;
    formsResult["encounter"] = formInfo.Pvid;
    formsResult["nb_sno"] = formInfo.Baby;

    formsResult["ward_id"] = UndefinedStringToSpace(formInfo.WardId);
    formsResult["ward_name"] = UndefinedStringToSpace(formInfo.WardName);
    if (
        formType !== "s" &&
        formInfo.EditType === "insert" &&
        ValueIsNull(formsResult["ward_id"])) {
        layer.msg(
            "病人区ID【" +
            formsResult["ward_id"], { icon: 5, time: 3000, offset: 'rt' });
        return false;
    }

    formsResult["recorder"] = formInfo.Recorder;
    formsResult["recorder_name"] = "";
    if (
        ValueIsNull(formsResult["recorder"])
    ) {
        layer.msg(
            "记录者ID【" +
            formsResult["recorder"] +
            "】不正确", { icon: 5, time: 3000, offset: 'rt' });
        return false;
    }
    formsResult["recorder_name"] = UndefinedStringToSpace(formInfo.RecoderName);
    formsResult["form_rec_id"] = formInfo.RecId;
    formsResult["observ_time"] = observtime;
    return formsResult;
}
/**
 * @description 根据获取表单的措施
 * @param {string} formID 表单ID
 */
function GetFormNiResult(formID) {
    let result = [];
    $("#a_" + formID)
        .find(".item_niList")
        .each(function () {
            let id = $(this).val();
            let item = {};
            if ($(this).is(":checked")) {
                item["ni_list_id"] = id;
                result.push(item);
            }
        });
    return result;
}

/**
 * 保存表单
 * @param {any} afterSave
 */
function SaveForms(afterSave) {
    if ($(".observtime").val() === "") {
        layer.tips('必须录入观察时间', '.observtime', {
            tips: [3, "red"]
        });
        return false;
    }

    //禁止编辑 
    $("input").attr("disabled", true);
    layer.msg('保存中', {
        icon: 16
        , shade: 0.01
    });
    $("button").attr("disabled", true);

    //提交页面 
    let submit = SubmitForms();

    $("input").attr("disabled", false);
    $("button").attr("disabled", false);
    if (!submit) {

        return false;
    }

    //todo:保存继续后的处理
    if (afterSave === "notout") {
        layer.closeAll();
        ClearItemValue();
    } else {
        CallBackToCEFComponent("submit");
    }

}
/**
 * 执行表单保存
 * @description 保存表单
 * @param {Object} afterSave 保存后是否关闭页面
 */
function SubmitForms() {


    let obserTime = $(".observtime").val();
    //观察时间只记录到分
    if (GetByteLen(obserTime) == 16) {
        obserTime = obserTime + ":00";
    }

    let t1;
    try {
        t1 = new Date(obserTime);
    } catch (err) {

        layer.tips("观察时间不正确【" + $(".observtime").val() + "】", '.observtime', {
            tips: [3, "red"]
        });
        return false;
    }

    let datespan = t1 - new Date();
    if (Math.floor(datespan / (3600 * 1000)) >= 2) {
        layer.tips('观察时间不能早于当前时间2个小时:' + $(".observtime").val(), '.observtime', {
            tips: [3, "red"]
        });
        return false;
    }

    let result = GetFormsResult(obserTime);

    /*为执行护理任务获取填写表单ID：*/
    if (result.length != 0) {
        let formids = result[0]["form_id"];
        for (let i = 1; i < result.length; i++) {
            formids = formids + "," + result[i]["form_id"];
        }
    }

    /*完成评分表后生成任务提供入参：*/
    if (result.length != 0) {
        let score = "",
            formid = "";
        for (let i = 0, a = 0; i < result.length; i++) {
            if (result[i]["scale_score"] != null && result[i]["scale_score"] != '') {
                a++;
                if (a == 1) {
                    score = result[i]["scale_score"];
                    formid = result[i]["form_id"];
                } else {
                    score = score + "," + result[i]["scale_score"];
                    formid = formid + "," + result[i]["form_id"];
                }
            }
        }
    }

    let NursingStep = $("#NursingStep").find(".iteminput").val();
    if (formMajorType == 'nurse' && result.length === 0 && NursingStep === "") {
        layer.msg('没有记录任何内容', { icon: 5, time: 3000, offset: 'rt' });
        return false;
    }
    let apiName;
    switch (formMajorType) {

        case 'form':
            apiName = "SaveForms";
            break;
        case 'nurse':
            result[0]["NursingStep"] = NursingStep;
            apiName = "SaveNurseRec";
            break;
        case 'supdev':
            apiName = "SaveSupdev";
            break;
    }
    console.log(JSON.stringify(result));
    $.ajax({
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        url: "/api/NForm/" + apiName + "?editType=" + formInfo.EditType,
        data: JSON.stringify(result),
        async: false,
        success: function (result) {
            console.log(result);
        },
        //请求失败，包含具体的错误信息 
        error: function (e) {
            console.log(e.status);
            console.log(e.responseText);
            let error = eval('(' + e.responseText + ')');
            layer.msg(error.message, { icon: 5, time: 3000, offset: 'rt' });
        }
    });
    return true;
}
/**
 * @description 生成所有表单集结果JSON
 * @param {*} observtime 观察时间
 * @param {*} afterSave 保存后模式,noout-不退出;out-退出
 * @param {*} saveType 保存类型,insert-新增;update-修改
 */
function GetFormsResult(observtime) {
    let allFormResult = [];
    let hasValue = false;
    let formsResult = {};
    let objFormData,
        oneFormResults,
        objItem,
        itemValue,
        item_children,
        detail_value_id,
        itemDataID,
        objItemData,
        one_item_result,
        observ_itemID,
        item_score;

    for (let i = 0; i < formLists.length; i++) {
        let objForm = formLists[i];
        oneFormResults = [];
        formsResult = {};
        formsResult["error"] = false;

        formsResult = GetFormsHeader(
            objForm,
            observtime,
            formsResult,
            objForm.type
        );

        formsResult = GetSupdevInfo(objForm, formsResult);
        let oneFormNi = GetFormNiResult(objForm.FormId);


        formsResult["form_ni"] = oneFormNi;

        objFormData = objForm;

        oneFormResults = GetFormItemsResult(objForm);

        hasValue = oneFormResults.hasValue;
        if (!oneFormResults.error && !formsResult.error && hasValue) {
            formsResult["result"] = oneFormResults.data;
            formsResult["hasValue"] = hasValue;
            if (oneFormResults.layout.length > 0) {
                formsResult["layout"] = oneFormResults.layout;
            }
            delete formsResult.error;
            allFormResult.push(formsResult);
        }
    }
    return allFormResult;
}
/**
 * @description 获取单选组合项目的值
 * @param {string} itemID 项目id
 * @param {Object} objItem 项目对象
 */
function GetGroupRadioItemValue(objItem) {
    let selectitem = objItem.find(".itemLov");
    let detail_value_id = "";
    let itemValue = "";
    let result = {};
    let itemDataID;
    let objItemData;
    let item_children = [];
    for (let i = 0; i < selectitem.length; i++) {
        let a = selectitem[i];

        let b;
        let one_detail = {};

        itemDataID = objItem.attr("id").substring(2);
        one_detail["observ_itemid"] = itemDataID;

        one_detail["item_value"] = a.checked ? ComboItemValue.是 : ComboItemValue.否;
        item_children.push(one_detail);
        //组合项目的值：dataid;选择的是否结果,dataid;选择的是否结果
        if (itemValue == "") {
            itemValue = itemDataID + ";" + one_detail["item_value"];
            detail_value_id = itemDataID;
        } else {
            itemValue = itemValue + "," + itemDataID + ";" + one_detail["item_value"];
            detail_value_id = detail_value_id + "," + itemDataID;
        }
    }
    result["item_value"] = itemValue;
    result["detail_value_id"] = detail_value_id;
    result["item_children"] = item_children;
    return result;
}

/**
 * @description 获取表单的项目值
 * @param {object} objFormitem 表单定义数据对象
 * @param {object} objForm 表单对象
 */
function GetFormItemsResult(objForm) {
    let detail_value_id,
        itemValue,
        item_children,
        one_item_result,
        item_attachec_text,
        valueResult,
        oneFormResults,
        item_value,
        itemDataID,
        layout, radioCheckedItem,
        itemLayout,
        $item, itemId, observItemId,
        objItemData;

    let item_score = "0";
    oneFormResults = [];
    valueResult = [];
    itemLayout = {};
    layout = [];
    let result = {
        error: false
    };
    let hasValue = false;
    $("[data-formid=" + objForm.FormId + "]").each(function () {
        $item = $(this);
        itemId = $item.attr("data-itemid");
        observItemId = $item.attr("data-observitemid");
        itemValue = "";
        detail_value_id = "";
        item_children = [];
        one_item_result = {};
        item_attachec_text;
        item_attachec_text = $("#a_" + itemId + "_other").val();

        itemLayout["ItemId"] = itemId;
        //获取项目的label，超长的label完整内容在title中 
        if (formMajorType == 'nurse') {
            itemLayout["ItemLabel"] = $("#a_" + itemId).attr("title");
            if (itemLayout["ItemLabel"] == undefined || itemLayout["ItemLabel"] == "") {
                itemLayout["ItemLabel"] = $("#a_" + itemId + "_label").text().trim();
            }
            itemLayout["RowNumber"] = $item.attr("data-row");
            itemLayout["ColNumber"] = $item.attr("data-col");
            layout.push(itemLayout);
            itemLayout = {};
        }

        if (IsBloodpressureItem(itemId)) {
            itemValue = GetBloodPressureItemValue(itemId);
            if (itemValue.error) {
                result["error"] = true;
                //continue;
                return true;
            } else {
                itemValue = itemValue.value;
            }
        } else {

            let inputType = $item.attr("data-type");
            let tmp = {};
            switch (inputType) {
                case "分段":
                    return true;
                case "说明行":
                    return true;
                case "文本框输入":
                    tmp = {};
                    tmp.text = Escape($item.find(".iteminput").val());
                    itemValue = tmp.text;
                    break;
                case "平铺多选":
                    valueResult = GetCheckBoxItemValue($item);
                    itemValue = valueResult["item_value"];
                    detail_value_id = valueResult["detail_value_id"];
                    break;
                case "单选组合":
                    //单选组合，每个选项就是一个所见项，每个选项无论是否选择都要记录结果
                    detail_value_id = "";
                    item_children = [];
                    valueResult = GetGroupRadioItemValue($item, item_attachec_text);
                    itemValue = valueResult["item_value"];
                    detail_value_id = valueResult["detail_value_id"];
                    item_children = valueResult["item_children"];
                    break;
                case "文字输入":
                    tmp = {};
                    tmp.text = Escape($item.find(".iteminput").val());
                    itemValue = tmp.text;
                    break;
                case "数字输入":
                    tmp = {};
                    tmp.text = Escape($item.find(".iteminput").val());
                    itemValue = tmp.text;
                    break;
                case "下拉多选":
                    //todo:下拉多选获取值未实现 .find('option:selected')
                    //$item.find("select").selectpicker('val')
                    let selected = GetMultiSelectItemValue($item);
                    itemValue = selected.text;
                    detail_value_id = selected.id;
                    break;
                case "下拉单选":
                    tmp = {};
                    tmp.text = Escape($item.find("select").selectpicker('val'));
                    itemValue = tmp.text;
                    if (tmp.text != "") {
                        detail_value_id = $item.find("[value=" + tmp.text + "]").attr("id")
                    }
                    break;
                case "平铺单选":
                    //其他就都是单选
                    radioCheckedItem = $item.find(".itemLov:checked");
                    itemValue = radioCheckedItem.val();
                    itemDataID = radioCheckedItem.attr("data-itemid");
                    item_score = radioCheckedItem.attr("data-score");
                    detail_value_id = "";
                    if (item_score == undefined) {
                        item_score = "";
                    }
                    if (!ValueIsNull(itemDataID)) {
                        detail_value_id = itemDataID;
                    }
                    break;
                case "按钮单选":
                    //其他就都是单选
                    radioCheckedItem = $item.find(".itemLov:checked");
                    itemValue = radioCheckedItem.val();
                    itemDataID = radioCheckedItem.attr("data-itemid");
                    item_score = radioCheckedItem.attr("data-score");
                    detail_value_id = "";
                    if (item_score == undefined) {
                        item_score = "";
                    }
                    if (!ValueIsNull(itemDataID)) {
                        detail_value_id = itemDataID;
                    }
                    break;
                case "时间选择":
                    //todo:获取值时间选择未实现 
                    itemValue = $item.find(".itemdate").val()
                    break;
                default:
                    console.log("获取值未实现类型：" + inputType);
                    break;
            }

        }

        one_item_result["item_score"] = item_score;
        if ($item.attr("data-itemid") != null) {
            one_item_result["item_id"] = itemId;

        } else {
            console.log("未找到项目的id属性：" + $item);
        }
        one_item_result["item_name"] = UndefinedStringToSpace($item.attr("data-itemname"));
        //处理项目中输入的双引号符号
        if (!ValueIsNull(item_attachec_text)) {
            one_item_result["item_value"] = Escape(
                UndefinedStringToSpace(itemValue) + "||" + item_attachec_text
            );
        } else {
            one_item_result["item_value"] = Escape(UndefinedStringToSpace(itemValue));
        }
        if (!UndefinedStringToSpace(itemValue) == "" && itemValue != null) {
            hasValue = true;
        }

        one_item_result["detail_value_id"] = UndefinedStringToSpace(
            detail_value_id
        );

        one_item_result["observ_item_name"] = UndefinedStringToSpace($item.attr("data-observname"));
        one_item_result["observ_item_code"] = UndefinedStringToSpace($item.attr("data-observcode"));
        one_item_result["observ_item_id"] = UndefinedStringToSpace($item.attr("data-observitemid"));
        one_item_result["item_children"] = item_children;
        oneFormResults.push(one_item_result);
    });
    result["data"] = oneFormResults;
    result["hasValue"] = hasValue;
    result["layout"] = layout;
    return result;
}


/**
 *@description 获取Select2项目的id和text值
 * @param {string} item_id
 */
function GetMultiSelectItemValue($item) {
    let select_data = {};
    let text_data = "";
    let id_data = "";
    let b;
    let value = $item.find("select").selectpicker('val');
    for (var i = 0; i < value.length; i++) {

        if (text_data == "") {
            text_data = value[i];
            id_data = $item.find("[value=" + value[i] + "]").attr("id");
        } else {
            text_data = text_data + ":" + value[i];
            id_data = id_data + ":" + $item.find("[value=" + value[i] + "]").attr("id");
        }
    }
    select_data["text"] = text_data;
    select_data["id"] = id_data;
    return select_data;
}

/**
 * @description 获取项目是否必须输入的class
 * @param {string} itemRequire  项目是否必须输入属性
 */
function GetItemRequiredClass(itemRequire) {
    if (itemRequire) {
        return "is-required";
    } else {
        return "";
    }
}


/**
 * @description 获取病人附着物表单信息
 * @param {Object} objForm
 * @param {Object} formsResult
 */
function GetSupdevInfo(objForm, formsResult) {

    //病人管道

    let supdevTypeId = objForm.FormId.split("_")[0];
    let patsupdevId = objForm.FormId.split("_")[1];
    formsResult["supdev_type_id"] = UndefinedStringToSpace(
        objForm.SupdevTypeId
    );
    formsResult["supdev_type_name"] = UndefinedStringToSpace(
        objForm.SupdevTypeName
    );
    formsResult["pat_supdev_name"] = UndefinedStringToSpace(
        objForm.PatSupdevName
    );
    formsResult["pat_supdev_id"] = UndefinedStringToSpace(objForm.PatSupdevId);
    return formsResult;
}

/**
 * @description 判断是否血压项目
 * @param {string} itemID 项目id
 */
function IsBloodpressureItem(itemID) {
    if (bloodPressureItem.indexOf(itemID) > -1) {
        return true;
    } else {
        return false;
    }
}

/**
 * @description 判断对象是否空
 * @param {object} obj
 */
function IsNullObject(obj) {
    if (obj === null || JSON.stringify(obj) === "{}" || obj === null) {
        return true;
    }
    return false;
}




/**
 * 创建表单任务
 * @param {any} formIds
 */
function CreateFormtask(formIds) {
    let TaskIn = {
        "executor": formInfo.RecoderId,
        "executeTime": "",
        "taskId": 0,
        "createTime": "",
        "title": "",
        "exectueDescript": "",
        "beginTime": "",
        "requestFinishTime": "",
        "taskTypeCode": TaskType,
        "pid": formInfo.Pid,
        "pvid": formInfo.Pvid,
        "wardId": formInfo.WardId,
        "responsibleId": "",
        "eventCode": "",
        "eventObjectId": formInfo.eventObjectId,
        "taskObjectId": formIds,
        "taskEventCode": ""
    };

    $.ajax({
        url: GetServerPath() + "/api/Task/DoTaskByTaskObjectId",
        type: "post",
        dataType: "json",
        //同步
        async: false,
        data: JSON.stringify(TaskIn),
        headers: { 'Content-Type': 'application/json' },
        success: function (res) {
            layer.msg("任务执行成功!", { icon: 6, time: 3000, offset: 'rt' });
        },
        error: function (e) {
            layer.msg('评分评估表任务创建失败:' + e.responseText, { icon: 5, time: 3000, offset: 'rt' });
        }
    });
}

function FinishFormCreateTask(formId, formScore) {
    var FTaskIn = {
        "pid": formInfo.Pid,
        "pvid": formInfo.Pvid,
        "wardId": formInfo.WardId,
        "eventObjectId": formScore,
        "formScale": formScore
    };
    //调用服务 
    $.ajax({
        url: GetServerPath() + "/api/Task/FinishFormCreateTask",
        type: "post",
        dataType: "json",
        //同步
        async: false,
        data: JSON.stringify(FTaskIn),
        headers: { 'Content-Type': 'application/json' },
        success: function (res) {
            layer.msg("完成评分生成任务成功!", { icon: 6, time: 3000, offset: 'rt' });
        },
        error: function (e) {
            layer.msg('评分评估表任务创建失败:' + e.responseText, { icon: 5, time: 3000, offset: 'rt' });
            console.log(e.responseText);
        }
    });
}

function AddFormHeduTask(formIds, formScore) {
    var HeduTaskIn = {
        "pid": formInfo.Pid,
        "pvid": formInfo.Pvid,
        "baby": formInfo.Baby,
        "wardId": formInfo.WardId,
        "eventObjectIds": formId,
        "taskEventCode": "FORM_1",
        "time": "2020/7/20",
        "formIds": formIds,
        "formScales": formScore
    };
    //调用服务 
    $.ajax({
        url: GetServerPath() + "/api/HeduTask/AddFormHeduTask",
        type: "post",
        dataType: "json",
        //同步
        async: false,
        data: JSON.stringify(HeduTaskIn),
        headers: { 'Content-Type': 'application/json' },
        success: function (res) {
            layer.msg("完成评分生成任务成功!", { icon: 6, time: 3000, offset: 'rt' });
        },
        error: function (e) {
            layer.msg('评分评估表任务创建失败:' + e.responseText, { icon: 5, time: 3000, offset: 'rt' });
        }
    });
}

/**
 * @description 提交关闭页面后向CEF部件回调
 * @event {string} 回调参数 submit 保存提交;cancel 取消
 */
//todo:后续改为通过分析浏览器的meda，判断是否EF部件，是EF部件才执行这个 
function CallBackToCEFComponent(event) {
    //CEF部件的版本49
    if (getBrowser('v') <= 50) {
        try {
            CEFAPI.trigger(event, "");
        } catch (err) {
            console.log('非CEF部件');
        }
    }
}

/**
 * 获取浏览器的版本
 * @param {any} n
 */
function getBrowser(n) {    var ua = navigator.userAgent.toLowerCase(),        s,        name = '',        ver = 0;    //探测浏览器    (s = ua.match(/msie ([\d.]+)/)) ? _set("ie", _toFixedVersion(s[1])) :        (s = ua.match(/firefox\/([\d.]+)/)) ? _set("firefox", _toFixedVersion(s[1])) :            (s = ua.match(/chrome\/([\d.]+)/)) ? _set("chrome", _toFixedVersion(s[1])) :                (s = ua.match(/opera.([\d.]+)/)) ? _set("opera", _toFixedVersion(s[1])) :                    (s = ua.match(/version\/([\d.]+).*safari/)) ? _set("safari", _toFixedVersion(s[1])) : 0;    function _toFixedVersion(ver, floatLength) {        ver = ('' + ver).replace(/_/g, '.');        floatLength = floatLength || 1;        ver = String(ver).split('.');        ver = ver[0] + '.' + (ver[1] || '0');        ver = Number(ver).toFixed(floatLength);        return ver;    }    function _set(bname, bver) {        name = bname;        ver = bver;    }    return (n == 'n' ? name : (n == 'v' ? ver : name + ver));};/**
 *获取服务器ip
 * */
function GetServerPath() {
    let urlPath = window.document.location.href;
    let docPath = window.document.location.pathname;
    let index = urlPath.indexOf(docPath);
    let serverPath = urlPath.substring(0, index);
    return serverPath;
}

//设置护理记录单项目当然的记录次数

function SetLastValueCount() {
    if (formMajorType != "nurse") {
        return false;
    }
    for (let i = 0; i < toDayNurseRec.length; i++) {
        let itemId = toDayNurseRec[i].ObservItemId;
        let $item = $("[data-itemid=" + itemId + "]").find(".lastValues");
        $item.text(toDayNurseRec[i].ItemRecCount);
        $item.show();
    }
}
//缓存病情观察词句

function SavePhrase() {

    if (formMajorType != 'nurse') {
        return false;
    }
    let getTime = new Date().Format("yyyy-MM-dd hh:mm:ss");
    let lastGetPhraseTime = localStorage.getItem("lastGetPhraseTime");
    if (lastGetPhraseTime == undefined || lastGetPhraseTime == null) {
        lastGetPhraseTime = "";
    }

    //请求病情观察的词句
    //todo：后续加上判断，indexdb没有数据才请求  
    $.ajax({
        //请求方式
        type: "GET",
        //请求的媒体类型
        contentType: "application/json;charset=UTF-8",
        //请求地址 
        url: "/api/NForm/GetPhrases?lastTime=" + lastGetPhraseTime,
        async: false,
        success: function (result) {
            phraseType = result.Data.phrasesType;
            phraseList = result.Data.phrasesList;
        },
        //请求失败，包含具体的错误信息 
        error: function (e) {
            console.log(e.status);
            console.log(e.responseText);
            let error = {};
            error["message"] = e.status;
            if (e.responseText != "") {
                error = eval('(' + e.responseText + ')');
            }

            layer.msg(error.message, { icon: 5, time: 3000, offset: 'rt' });
        }
    });

    //更加上次时间查询，有新数据，就删除重新缓存 
    if (phraseType.length > 0) {
        localStorage.setItem("phraseType", phraseType);
        localStorage.setItem("lastGetPhraseTime", getTime);
    } else {
        phraseType = localStorage.getItem("phraseType");
    }

    //更加上次时间查询，有新数据，就删除重新缓存 
    if (phraseList.length > 0) {
        localStorage.setItem("phraseList", phraseList);
        localStorage.setItem("lastGetPhraseTime", getTime);
    } else {
        phraseList = localStorage.getItem("phraseList");
    }
}
//显示护理记录项目今日的值表格

function ShowItemToDayList($item) {
    let itemId = $item.parent().attr("data-itemid");

    let lastValues = toDayNurseRec.find(function (i, index) {
        if (i.ObservItemId == itemId) {
            return i;
        }
    });

    let html = "";
    html = '<table class="table table-striped table-bordered">';
    html = html + "<thead><tr><th>项目值</th><th style='width:35%'>记录时间</th><th style='width:16%'>记录者</th></tr></thead>";
    html = html + "<tbody>";
    for (let i = 0; i < lastValues.RecDetail.length; i++) {
        html += "<tr>";
        html += "<td>" + lastValues.RecDetail[i].Value + "</td>";
        var a = new Date(lastValues.RecDetail[i].ObservTime)
        html += "<td>" + a.Format("yyyy-MM-dd hh:mm:ss") + "</td>";
        html += "<td>" + lastValues.RecDetail[i].RecorderName + "</td>";
        html += "</tr>";
    }

    html = html + "</tbody></table>";
    layer.tips(html, $item, {
        area: ['500px', 'auto'],
        tips: [1, 'white'],
        time: 4000
    });

}

