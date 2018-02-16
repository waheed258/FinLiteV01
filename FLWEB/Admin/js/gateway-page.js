$(window).scroll(floatingIcons);
var flagForbuttonAnimate = true;
$(function() {
    processStepAnimate();
    $("a.arrow-icn").click(function() {
        $("ul.mobiletab").fadeToggle("slow")
    });
    $(".selectpicker").selectpicker({
        style: "btn-info",
        size: 4
    });
});

function processStepAnimate() {
    var a = (function() {
        var p = $("#process-container > div.process-row"),
            k, l, o = $("#process-links > a"),
            b = $(window),
            j = {},
            g = false,
            e = 2000,
            n = "easeInOutExpo",
            f = false,
            c = f && Modernizr.csstransforms3d,
            r = function() {
                h();
                m();
                q();
                d();
                if (c) {
                    p.css({
                        "-webkit-perspective": 600,
                        "-webkit-perspective-origin": "50% 0%"
                    })
                }
                k.find("a.process-circle").addClass("process-circle-deco");
                i()
            },
            q = function() {
                $.extend($.expr[":"], {
                    inviewport: function(s) {
                        if ($(s).offset().top < j.height) {
                            return true
                        }
                        return false
                    }
                })
            },
            d = function() {
                k = p.filter(":inviewport");
                l = p.not(k)
            },
            h = function() {
                j.width = b.width();
                j.height = b.height()
            },
            m = function() {
                o.on("click.Scrolling", function(s) {
                    $("html, body").stop().animate({
                        scrollTop: $($(this).attr("href")).offset().top
                    }, e, n);
                    return false
                });
                $(window).on({
                    "resize.Scrolling": function(s) {
                        h();
                        d();
                        p.find("a.process-circle").removeClass("process-circle-deco");
                        k.each(function() {
                            $(this).find("div.process-left").css({
                                left: "0%"
                            }).end().find("div.process-right").css({
                                right: "0%"
                            }).end().find("a.process-circle").addClass("process-circle-deco")
                        })
                    },
                    "scroll.Scrolling": function(s) {
                        if (g) {
                            return false
                        }
                        g = true;
                        setTimeout(function() {
                            i();
                            g = false
                        }, 10)
                    }
                })
            },
            i = function() {
                var t = b.scrollTop(),
                    s = j.height / 2 + t;
                l.each(function(z) {
                    var B = $(this),
                        A = B.find("div.process-left"),
                        x = B.find("div.process-right"),
                        y = B.offset().top;
                    if (y > j.height + t) {
                        if (c) {
                            A.css({
                                "-webkit-transform": "translate3d(-75%, 0, 0) rotateY(-90deg) translate3d(-75%, 0, 0)",
                                opacity: 0
                            });
                            x.css({
                                "-webkit-transform": "translate3d(75%, 0, 0) rotateY(90deg) translate3d(75%, 0, 0)",
                                opacity: 0
                            })
                        } else {
                            A.css({
                                left: "-50%"
                            });
                            x.css({
                                right: "-50%"
                            })
                        }
                    } else {
                        var D = B.height(),
                            C = (((y + D / 2) - s) / (j.height / 2 + D / 2)),
                            w = Math.max(C * 50, 0);
                        if (w <= 0) {
                            if (!B.data("pointer")) {
                                B.data("pointer", true);
                                B.find(".process-circle").addClass("process-circle-deco")
                            }
                        } else {
                            if (B.data("pointer")) {
                                B.data("pointer", false);
                                B.find(".process-circle").removeClass("process-circle-deco")
                            }
                        } if (c) {
                            var E = Math.max(C * 75, 0),
                                u = Math.max(C * 90, 0),
                                v = Math.min(Math.abs(C - 1), 1);
                            A.css({
                                "-webkit-transform": "translate3d(-" + E + "%, 0, 0) rotateY(-" + u + "deg) translate3d(-" + E + "%, 0, 0)",
                                opacity: v
                            });
                            x.css({
                                "-webkit-transform": "translate3d(" + E + "%, 0, 0) rotateY(" + u + "deg) translate3d(" + E + "%, 0, 0)",
                                opacity: v
                            })
                        } else {
                            A.css({
                                left: -w + "%"
                            });
                            x.css({
                                right: -w + "%"
                            })
                        }
                    }
                })
            };
        return {
            init: r
        }
    })();
    a.init()
}

function getTargetedUrl() {
    var c = $(".actionItem").val();
    var b = $(".product").val();
    if (b != 0) {
        var a = redirectUrl[c][b];
        window.location = a
    }
}

function floatingIcons() {
    if ($(window).scrollTop() > 475 & $(window).scrollTop() < 3250) {
        $("#process-links").css("position", "fixed")
    } else {
        $("#process-links").css("position", "absolute")
    } if ($(window).scrollTop() > 3250 && $(window).scrollTop() < 5025) {
        if (flagForbuttonAnimate) {
            $(".we-hire-btn").addClass("we-hire-btn-animate");
            flagForbuttonAnimate = false
        }
    } else {
        $(".we-hire-btn").removeClass("we-hire-btn-animate");
        flagForbuttonAnimate = true
    }
}

