//Index Menu

$(document).ready(function () {

    //顶部菜单切换
    $.each($('#nav li'), function (i) {
        $(this).click(function () {
            $(this).addClass('actived').siblings('li').removeClass('actived');
            $('#mainMenu #sort_' + i + '').show().siblings('div').hide().find('li a').removeClass('selected');
            $("#workspace").attr("src", $(this).find("a").attr("hr"));
        })
    });

    //左侧菜单切换
    $("#mainMenu dl li").click(function () {
        $(this).parents("div").find("a").removeClass("selected");
        $(this).find("a").addClass("selected")
        $("#workspace").attr("src", $(this).find("a").attr("hr"));
    });

    $("#mainMenu dt").click(function () {
        $(this).toggleClass("fold");
        $(this).next("dd").children("ul").toggle();
    });

    $(".tn-user-name").hover(function () {
        $(this).find(".tn-sub").toggle();
    })

});