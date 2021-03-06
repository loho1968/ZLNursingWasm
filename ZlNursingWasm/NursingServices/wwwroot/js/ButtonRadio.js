﻿(function (l) {
    l.fn.richcheckbox = function (options) {
        var settings = l.extend({
            allBtn: false,
            checkAll: false,
            selected: ""
        }, options);
        var labHTML = "";
        var btnGroup = l(this);
        var _name = btnGroup.attr("name");
        var checkNum = 0;
        var _Browser = "";
        var selectArr = settings.selected.split(",");
        if ("IE8" == l.BrowserVersion())
            _Browser = "IE8";
        else
            _Browser = "otherBrowser";
        btnGroup.each(function (i) {
            var checkbox = l(this);
            if ("IE8" == _Browser) {
                checkbox.addClass("lowIE");
                if ("" != settings.selected) {
                    if (-1 != $.inArray(checkbox.attr("value"), selectArr)) {
                        labHTML = "<label class='richcheckbox IEChecked " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>";
                        checkNum++
                    } else {
                        labHTML = "<label class='richcheckbox " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>"
                    }
                } else {
                    if (checkbox.is(':checked') || settings.checkAll) {
                        labHTML = "<label class='richcheckbox IEChecked " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>";
                        checkNum++
                    } else {
                        labHTML = "<label class='richcheckbox " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>"
                    }
                }
                checkbox.wrap(labHTML);
                checkbox.click(function () {
                    var parent = l(this).closest("label");
                    if (l(this).is(':checked')) {
                        parent.addClass("IEChecked");
                        checkNum++;
                        if (checkNum == btnGroup.length)
                            l("#" + _name).addClass("IEChecked")
                    } else {
                        parent.removeClass("IEChecked");
                        $("#" + _name).removeClass("IEChecked");
                        checkNum--
                    }
                })
            } else {
                checkbox.addClass("otherBrowser");
                if ("" != settings.selected) {
                    if (-1 != l.inArray(checkbox.attr("value"), selectArr)) {
                        labHTML = "<label class='richcheckbox otherBrowserChecked " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>";
                        checkNum++
                    } else {
                        labHTML = "<label class='richcheckbox " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>"
                    }
                } else {
                    if (checkbox.is(':checked') || settings.checkAll) {
                        labHTML = "<label class='richcheckbox otherBrowserChecked " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>";
                        checkNum++
                    } else {
                        labHTML = "<label class='richcheckbox " + l(this).attr("name") + "'>" + l(this).attr("lab") + "</label>"
                    }
                }
                checkbox.wrap(labHTML);
                checkbox.click(function () {
                    var parent = l(this).closest("label");
                    if (l(this).is(':checked')) {
                        parent.addClass("otherBrowserChecked");
                        checkNum++;
                        if (checkNum == btnGroup.length)
                            l("#" + _name).addClass("otherBrowserChecked")
                    } else {
                        parent.removeClass("otherBrowserChecked");
                        $("#" + _name).removeClass("otherBrowserChecked");
                        checkNum--
                    }
                })
            }
        });
        if (settings.allBtn) {
            var allBtnHTML = "<label class='richcheckbox' id='" + _name + "'>全选</label>";
            btnGroup.eq(0).closest(".richcheckbox").before(allBtnHTML);
            if (checkNum == btnGroup.length) {
                if ("IE8" == _Browser)
                    l("#" + _name).addClass("IEChecked");
                else
                    l("#" + _name).addClass("otherBrowserChecked")
            }
        }
        l("#" + _name).click(function () {
            if ("IE8" == _Browser) {
                if (-1 != l(this).attr("class").indexOf("IEChecked")) {
                    l("." + _name).removeClass("IEChecked");
                    l(this).removeClass("IEChecked");
                    btnGroup.prop("checked", false);
                    checkNum = 0
                } else {
                    l("." + _name).addClass("IEChecked");
                    l(this).addClass("IEChecked");
                    btnGroup.prop("checked", true);
                    checkNum = btnGroup.length
                }
            } else {
                if (-1 != l(this).attr("class").indexOf("otherBrowserChecked")) {
                    l("." + _name).removeClass("otherBrowserChecked");
                    l(this).removeClass("otherBrowserChecked");
                    btnGroup.prop("checked", false);
                    checkNum = 0
                } else {
                    l("." + _name).addClass("otherBrowserChecked");
                    l(this).addClass("otherBrowserChecked");
                    btnGroup.prop("checked", true);
                    checkNum = btnGroup.length
                }
            }
        })
    }
        ;
    l.fn.richradio = function (options) {
        var settings = l.extend({
            selected: ""
        }, options);
        var selectArr = settings.selected.split(",");
        var labHTML = "";
        var btnGroup = l(this);
        var _Browser = "";
        if ("IE8" == l.BrowserVersion())
            _Browser = "IE8";
        else
            _Browser = "otherBrowser";
        btnGroup.each(function (i) {
            var radio = l(this);
            var _name = radio.attr("name");
            if (_Browser == "IE8") {
                radio.addClass("lowIE");
                if ("" != settings.selected) {
                    if (-1 != $.inArray(radio.attr("value"), selectArr)) {
                        labHTML = "<label class='richradio IEChecked " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    } else {
                        labHTML = "<label class='richradio " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    }
                } else {
                    if (radio.is(':checked')) {
                        labHTML = "<label class='richradio IEChecked " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    } else {
                        labHTML = "<label class='richradio " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    }
                }
                radio.prop("id", _name + "-" + i);
                radio.wrap(labHTML);
                radio.click(function () {
                    var _name = l(this).attr("name");
                    var parent = l(this).closest("label");
                    l("." + _name).removeClass("IEChecked");
                    parent.addClass("IEChecked")
                })
            } else {
                radio.addClass("otherBrowser");
                if ("" != settings.selected) {
                    if (-1 != $.inArray(radio.attr("value"), selectArr)) {
                        labHTML = "<label class='richradio otherBrowserChecked " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    } else {
                        labHTML = "<label class='richradio " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    }
                } else {
                    if (radio.is(':checked')) {
                        labHTML = "<label class='richradio otherBrowserChecked " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    } else {
                        labHTML = "<label class='richradio " + _name + "' for='" + _name + "-" + i + "'>" + radio.attr("lab") + "</label>"
                    }
                }
                radio.prop("id", _name + "-" + i);
                radio.wrap(labHTML);
                radio.click(function () {
                    var _name = l(this).attr("name");
                    var parent = l(this).closest("label");
                    l("." + _name).removeClass("otherBrowserChecked");
                    parent.addClass("otherBrowserChecked")
                })
            }
        })
    }
        ;
    l.fn.richselect = function (options) {
        var settings = l.extend({
            type: "single",
            selected: "",
            width: "220px",
            placeholder: "请选择"
        }, options);
        var select = l(this);
        var optGroup = select.find("option");
        if ("IE8" == l.BrowserVersion()) {
            select.addClass("lowIE")
        } else {
            select.addClass("otherBrowser")
        }
        var richselectHTML = "<div class='richselect' id='" + select.attr("id") + "-rich'><span class='richplaceholder'>" + settings.placeholder + "</span><ul>";
        optGroup.each(function () {
            var option = l(this);
            richselectHTML += "<li value='" + option.val() + "'>" + option.text() + "</li>"
        });
        richselectHTML += "</ul></div>";
        select.after(richselectHTML);
        var richselect = select.next(".richselect");
        richselect.css({
            "width": settings.width
        });
        richselect.find("ul>li").addClass("unchecked");
        if ("single" == settings.type) {
            select.find("option").prop("selected", false);
            select.prepend("<option disabled='disabled' selected='selected' value='null' ></option>");
            if ("" != settings.selected) {
                richselect.find("ul>li").each(function (i) {
                    if (settings.selected == l(this).attr("value")) {
                        richselect.find('.richplaceholder').text(l(this).text());
                        select.find("option[value='" + l(this).attr("value") + "']").prop("selected", true);
                        return false
                    }
                })
            }
            richselect.on('click', '.richplaceholder', function (e) {
                var parent = l(this).closest(".richselect");
                if (!parent.hasClass('is-open')) {
                    l(".richselect").removeClass('is-open');
                    parent.addClass('is-open');
                    l(this).find(".is-open").not(parent).removeClass('is-open')
                } else {
                    parent.removeClass('is-open')
                }
                e.stopPropagation()
            }).on('click', 'ul>li', function () {
                var parent = l(this).closest(".richselect");
                if (l(this).text() != parent.find('.richplaceholder').text()) {
                    parent.removeClass('is-open').find('.richplaceholder').text(l(this).text());
                    select.find("option").prop("selected", false);
                    select.find("option[value='" + l(this).attr("value") + "']").prop("selected", true)
                }
            })
        } else if ("multi" == settings.type) {
            var tArr = [];
            select.prop("multiple", "multiple");
            select.find("option").prop("selected", false);
            if ("" != settings.selected) {
                var arr = settings.selected.split(",");
                richselect.find("ul>li").each(function (i) {
                    for (var j = 0; j < arr.length; j++) {
                        if (arr[j] == l(this).attr("value")) {
                            l(this).removeClass("unchecked").addClass("checked");
                            tArr.push(l(this).text());
                            select.find("option[value='" + l(this).attr("value") + "']").prop("selected", true)
                        }
                    }
                });
                richselect.find('.richplaceholder').text(tArr)
            }
            richselect.on('click', '.richplaceholder', function (e) {
                var parent = l(this).closest(".richselect");
                if (!parent.hasClass('is-open')) {
                    l(".richselect").removeClass('is-open');
                    parent.addClass('is-open');
                    l(this).find(".is-open").not(parent).removeClass('is-open')
                } else {
                    parent.removeClass('is-open')
                }
                e.stopPropagation()
            }).on('click', 'ul>li', function () {
                var parent = l(this).closest(".richselect");
                if (-1 != l(this).attr("class").indexOf("unchecked")) {
                    l(this).removeClass("unchecked").addClass("checked");
                    tArr.push(l(this).text());
                    select.find("option[value='" + l(this).attr("value") + "']").prop("selected", true);
                    parent.find('.richplaceholder').text(tArr)
                } else if (-1 != l(this).attr("class").indexOf("checked")) {
                    l(this).removeClass("checked").addClass("unchecked");
                    tArr.removeByValue(l(this).text());
                    select.find("option[value='" + l(this).attr("value") + "']").prop("selected", false);
                    if (tArr.length > 0) {
                        parent.find('.richplaceholder').text(tArr)
                    } else {
                        parent.find('.richplaceholder').text(settings.placeholder)
                    }
                }
            })
        }
        $('body').on('click', function (event) {
            var evt = event.srcElement ? event.srcElement : event.target;
            var t = l(evt).closest(".richselect");
            if (undefined != l(t).attr("class") && -1 != l(t).attr("class").indexOf("richselect"))
                return;
            else
                l(".richselect").removeClass('is-open')
        })
    }
        ;
    Array.prototype.removeByValue = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) {
                this.splice(i, 1);
                break
            }
        }
    }
}
)(jQuery);
