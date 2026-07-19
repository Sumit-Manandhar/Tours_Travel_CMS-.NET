(function ($) {
    "use strict";
    $(".imglazyload").lazyload();

    let dynamicyearElm = $(".dynamic-year");
    if (dynamicyearElm.length) {
        let currentYear = new Date().getFullYear();
        dynamicyearElm.html(currentYear);
    }
    
    //Fact Counter + Text Count
    if ($(".count-box").length) {
        $(".count-box").appear(
            function () {
                var $t = $(this),
                    n = $t.find(".count-text").attr("data-stop"),
                    r = parseInt($t.find(".count-text").attr("data-speed"), 10);

                if (!$t.hasClass("counted")) {
                    $t.addClass("counted");
                    $({
                        countNum: $t.find(".count-text").text()
                    }).animate({
                        countNum: n
                    }, {
                        duration: r,
                        easing: "linear",
                        step: function () {
                            $t.find(".count-text").text(Math.floor(this.countNum));
                        },
                        complete: function () {
                            $t.find(".count-text").text(this.countNum);
                        }
                    });
                }
            }, {
            accY: 0
        }
        );
    }


    if ($(".video-popup").length) {
        $(".video-popup").magnificPopup({
            type: "iframe",
            mainClass: "mfp-fade",
            removalDelay: 160,
            preloader: true,

            fixedContentPos: false
        });
    }



    if ($(".img-popup").length) {
        var groups = {};
        $(".img-popup").each(function () {
            var id = parseInt($(this).attr("data-group"), 10);

            if (!groups[id]) {
                groups[id] = [];
            }

            groups[id].push(this);
        });

        $.each(groups, function () {
            $(this).magnificPopup({
                type: "image",
                closeOnContentClick: true,
                closeBtnInside: false,
                gallery: {
                    enabled: true
                }
            });
        });
    }

    let vietjetImagePopupGallery = $(".vietjet-image-popup");
    vietjetImagePopupGallery.each(function () {
        let elm = $(this);
        let options = elm.data("gallery-options");
        let imageGallery = elm.magnificPopup(
            "object" === typeof options ? options : JSON.parse(options)
        );
    });


    function dynamicCurrentMenuClass(selector) {
        let FileName = window.location.href.split("/").reverse()[0];

        selector.find("li").each(function () {
            let anchor = $(this).find("a");
            if ($(anchor).attr("href") == FileName) {
                $(this).addClass("current");
            }
        });
        // if any li has .current elmnt add class
        selector.children("li").each(function () {
            if ($(this).find(".current").length) {
                $(this).addClass("current");
            }
        });
        // if no file name return
        if ("" == FileName) {
            selector.find("li").eq(0).addClass("current");
        }
    }

    if ($(".main-menu__list").length) {
        // dynamic current class
        let mainNavUL = $(".main-menu__list");
        dynamicCurrentMenuClass(mainNavUL);
    }

    if ($(".service-sidebar__nav").length) {
        // dynamic current class
        let mainNavUL = $(".service-sidebar__nav");
        dynamicCurrentMenuClass(mainNavUL);
    }

    if ($(".main-menu").length && $(".mobile-nav__container").length) {
        let navContent = document.querySelector(".main-menu").innerHTML;
        let mobileNavContainer = document.querySelector(".mobile-nav__container");
        mobileNavContainer.innerHTML = navContent;
    }

    if ($(".sticky-header").length) {
        $(".sticky-header")
            .clone()
            .insertAfter(".sticky-header")
            .addClass("sticky-header--cloned");
    }

    if ($(".mobile-nav__container .main-menu__list").length) {
        let dropdownAnchor = $(
            ".mobile-nav__container .main-menu__list .dropdown > a"
        );
        dropdownAnchor.each(function () {
            let self = $(this);
            let toggleBtn = document.createElement("BUTTON");
            toggleBtn.setAttribute("aria-label", "dropdown toggler");
            toggleBtn.innerHTML = "<i class='fa fa-angle-down'></i>";
            self.append(function () {
                return toggleBtn;
            });
            self.find("button").on("click", function (e) {
                e.preventDefault();
                let self = $(this);
                self.toggleClass("expanded");
                self.parent().toggleClass("expanded");
                self.parent().parent().children("ul").slideToggle();
            });
        });
    }

    //Show Popup menu
    $(document).on("click", ".megamenu-clickable--toggler > a", function (e) {
        $("body").toggleClass("megamenu-popup-active");
        $(this).parent().find("ul").toggleClass("megamenu-clickable--active");
        e.preventDefault();
    });
    $(document).on("click", ".megamenu-clickable--close", function (e) {
        $("body").removeClass("megamenu-popup-active");
        $(".megamenu-clickable--active").removeClass("megamenu-clickable--active");
        e.preventDefault();
    });

    if ($(".mobile-nav__toggler").length) {
        $(".mobile-nav__toggler").on("click", function (e) {
            e.preventDefault();
            $(".mobile-nav__wrapper").toggleClass("expanded");
            $("body").toggleClass("locked");
        });
    }



    if ($(".wow").length) {
        var wow = new WOW({
            boxClass: "wow", // animated element css class (default is wow)
            animateClass: "animated", // animation css class (default is animated)
            mobile: true, // trigger animations on mobile devices (default is true)
            live: true // act on asynchronously loaded content (default is true)
        });
        wow.init();
    }

    function vietjetPara() {
        let vietjetParaElm = $(".vietjet-splax");
        if (vietjetParaElm.length) {
            vietjetParaElm.each(function () {
                let self = $(this);
                let className = self.attr("class");
                var image = document.getElementsByClassName(className);
                let options = self.data("para-options");
                let vietjetPara = new simpleParallax(
                    image,
                    "object" === typeof options ? options : JSON.parse(options)
                );
            });
        }
    }



    //accrodion
    accordionInitializer();

    if ($(".tabs-box").length) {
        $(".tabs-box .tab-buttons .tab-btn").on("click", function (e) {
            e.preventDefault();
            var target = $($(this).attr("data-tab"));

            if ($(target).is(":visible")) {
                return false;
            } else {
                target
                    .parents(".tabs-box")
                    .find(".tab-buttons")
                    .find(".tab-btn")
                    .removeClass("active-btn");
                $(this).addClass("active-btn");
                target
                    .parents(".tabs-box")
                    .find(".tabs-content")
                    .find(".tab")
                    .fadeOut(0);
                target
                    .parents(".tabs-box")
                    .find(".tabs-content")
                    .find(".tab")
                    .removeClass("active-tab");
                $(target).fadeIn(300);
                $(target).addClass("active-tab");
            }
        });
    }



    function thmOwlInit() {
        // owl slider
        let vietjetowlCarousel = $(".vietjet-owl__carousel");
        if (vietjetowlCarousel.length) {
            vietjetowlCarousel.each(function () {
                let elm = $(this);
                let options = elm.data("owl-options");
                let thmOwlCarousel = elm.owlCarousel(
                    "object" === typeof options ? options : JSON.parse(options)
                );
                elm.find("button").each(function () {
                    $(this).attr("aria-label", "carousel button");
                });
            });
        }

        let vietjetowlCarouselWithFilter = $(".vietjet-owl__carousel--filter");
        if (vietjetowlCarouselWithFilter.length) {
            vietjetowlCarouselWithFilter.each(function () {
                let elm = $(this);
                let options = elm.data("owl-options");
                let filtersDiv = elm.data("owl-filters-div");
                let thmOwlCarousel = elm.owlCarousel(
                    "object" === typeof options ? options : JSON.parse(options)
                );
                elm.find("button").each(function () {
                    $(this).attr("aria-label", "carousel button");
                });
                $(filtersDiv).on('click', '.item', function () {
                    var $item = $(this);
                    $(filtersDiv).find(".item").removeClass("active");
                    $item.addClass("active");
                    var filter = $item.data('owl-filter')
                    thmOwlCarousel.owlcarousel2_filter(filter);
                })

            });
        }

        let vietjetowlCarouselNav = $(".vietjet-owl__carousel--custom-nav");
        if (vietjetowlCarouselNav.length) {
            vietjetowlCarouselNav.each(function () {
                let elm = $(this);
                let owlNavPrev = elm.data("owl-nav-prev");
                let owlNavNext = elm.data("owl-nav-next");
                $(owlNavPrev).on("click", function (e) {
                    elm.trigger("prev.owl.carousel");
                    e.preventDefault();
                });

                $(owlNavNext).on("click", function (e) {
                    elm.trigger("next.owl.carousel");
                    e.preventDefault();
                });
            });
        }
    }



    /*-- Handle Scrollbar --*/
    function handleScrollbar() {
        const bodyHeight = $("body").height();
        const scrollPos = $(window).innerHeight() + $(window).scrollTop();
        let percentage = (scrollPos / bodyHeight) * 100;
        if (percentage > 100) {
            percentage = 100;
        }
        $(".scroll-to-top .scroll-to-top__inner").css("width", percentage + "%");
    }

    /*-- One Page Menu --*/
    function SmoothMenuScroll() {
        var anchor = $(".scrollToLink");
        if (anchor.length) {
            anchor.children("a").bind("click", function (event) {
                if ($(window).scrollTop() > 10) {
                    var headerH = "0";
                } else {
                    var headerH = "0";
                }
                var target = $(this);
                $("html, body")
                    .stop()
                    .animate({
                        scrollTop: $(target.attr("href")).offset().top - headerH + "px"
                    },
                        900,
                        "easeInOutExpo"
                    );
                anchor.removeClass("current");
                anchor.removeClass("current-menu-ancestor");
                anchor.removeClass("current_page_item");
                anchor.removeClass("current-menu-parent");
                target.parent().addClass("current");
                event.preventDefault();
            });
        }
    }
    SmoothMenuScroll();

    function OnePageMenuScroll() {
        var windscroll = $(window).scrollTop();
        if (windscroll >= 117) {
            var menuAnchor = $(".one-page-scroll-menu .scrollToLink").children("a");
            menuAnchor.each(function () {
                var sections = $(this).attr("href");
                $(sections).each(function () {
                    if ($(this).offset().top <= windscroll + 100) {
                        var Sectionid = $(sections).attr("id");
                        $(".one-page-scroll-menu").find("li").removeClass("current");
                        $(".one-page-scroll-menu")
                            .find("li")
                            .removeClass("current-menu-ancestor");
                        $(".one-page-scroll-menu")
                            .find("li")
                            .removeClass("current_page_item");
                        $(".one-page-scroll-menu")
                            .find("li")
                            .removeClass("current-menu-parent");
                        $(".one-page-scroll-menu")
                            .find("a[href*=\\#" + Sectionid + "]")
                            .parent()
                            .addClass("current");
                    }
                });
            });
        } else {
            $(".one-page-scroll-menu li.current").removeClass("current");
            $(".one-page-scroll-menu li:first").addClass("current");
        }
    }

    // window scroll event
    function stickyMenuUpScroll($targetMenu, $toggleClass) {
        var lastScrollTop = 0;
        window.addEventListener(
            "scroll",
            function () {
                var st = window.pageYOffset || document.documentElement.scrollTop;
                if (st > 500) {
                    if (st > lastScrollTop) {
                        // downscroll code
                        $targetMenu.removeClass($toggleClass);
                        // console.log("down");
                    } else {
                        // upscroll code
                        $targetMenu.addClass($toggleClass);
                        // console.log("up");
                    }
                } else {
                    $targetMenu.removeClass($toggleClass);
                }
                lastScrollTop = st;
            },
            false
        );
    }
    stickyMenuUpScroll($(".sticky-header--normal"), "active");

    //Strech Column
    function vietjet_stretch() {
        var i = $(window).width();
        $(".row .vietjet-stretch-element-inside-column").each(function () {
            var $this = $(this),
                row = $this.closest(".row"),
                cols = $this.closest('[class^="col-"]'),
                colsheight = $this.closest('[class^="col-"]').height(),
                rect = this.getBoundingClientRect(),
                l = row[0].getBoundingClientRect(),
                s = cols[0].getBoundingClientRect(),
                r = rect.left,
                d = i - rect.right,
                c = l.left + (parseFloat(row.css("padding-left")) || 0),
                u = i - l.right + (parseFloat(row.css("padding-right")) || 0),
                p = s.left,
                f = i - s.right,
                styles = {
                    "margin-left": 0,
                    "margin-right": 0
                };
            if (Math.round(c) === Math.round(p)) {
                var h = parseFloat($this.css("margin-left") || 0);
                styles["margin-left"] = h - r;
            }
            if (Math.round(u) === Math.round(f)) {
                var w = parseFloat($this.css("margin-right") || 0);
                styles["margin-right"] = w - d;
            }
            $this.css(styles);
        });
    }
    vietjet_stretch();

    function vietjet_cuved_circle() {
        let circleTypeElm = $(".curved-circle--item");
        if (circleTypeElm.length) {
            circleTypeElm.each(function () {
                let elm = $(this);
                let options = elm.data("circle-text-options");
                elm.circleType(
                    "object" === typeof options ? options : JSON.parse(options)
                );
            });
        }
    }

    let reviewStarElm = $(".vietjet-ratings-two");
    if (reviewStarElm.length) {
        reviewStarElm.find('i').on('mouseover', function () {
            var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on

            // Now highlight all the stars that's not after the current hovered star
            $(this).parent().children('i').each(function (e) {
                if (e < onStar) {
                    $(this).addClass('hover');
                } else {
                    $(this).removeClass('hover');
                }
            });

        }).on('mouseout', function () {
            $(this).parent().children('i').each(function (e) {
                $(this).removeClass('hover');
            });
        });

        reviewStarElm.find('i').on('click', function () {
            var onStar = parseInt($(this).data('value'), 10); // The star currently selected
            var stars = $(this).parent().children('i');

            for (let i = 0; i < stars.length; i++) {
                $(stars[i]).removeClass('active');
            }

            for (let i = 0; i < onStar; i++) {
                $(stars[i]).addClass('active');
            }

            // JUST RESPONSE (Not needed)
            var ratingValue = parseInt(reviewStarElm.find('i.active').last().data('value'), 10);
            var msg = 0;
            if (ratingValue > 1) {
                msg = ratingValue;
            } else {
                msg = ratingValue;
            }

            reviewStarElm.find('input[name=rating]').val(msg);

        });

    }





    // window load event
    $(window).on("load", function () {
        if ($(".preloader").length) {
            $(".preloader").fadeOut();
        }
        thmOwlInit();
        // thmTinyInit();
        // priceFilter();
        // vietjetPara();

        if ($(".circle-progress").length) {
            $(".circle-progress").appear(function () {
                let circleProgress = $(".circle-progress");
                circleProgress.each(function () {
                    let progress = $(this);
                    let progressOptions = progress.data("options");
                    progress.circleProgress(progressOptions);
                });
            });
        }
        if ($(".masonry-layout").length) {
            $(".masonry-layout").imagesLoaded(function () {
                $(".masonry-layout").isotope({
                    layoutMode: "masonry"
                });
            });
        }
        if ($(".fitRow-layout").length) {
            $(".fitRow-layout").imagesLoaded(function () {
                $(".fitRow-layout").isotope({
                    layoutMode: "fitRows"
                });
            });
        }

        if ($(".post-filter").length) {
            var postFilterList = $(".post-filter li");
            // for first init
            $(".filter-layout").isotope({
                filter: ".filter-item",
                animationOptions: {
                    duration: 500,
                    easing: "linear",
                    queue: false
                }
            });
            // on click filter links
            postFilterList.on("click", function () {
                var Self = $(this);
                var selector = Self.attr("data-filter");
                postFilterList.removeClass("active");
                Self.addClass("active");

                $(".filter-layout").isotope({
                    filter: selector,
                    animationOptions: {
                        duration: 500,
                        easing: "linear",
                        queue: false
                    }
                });
                return false;
            });
        }

        if ($(".post-filter.has-dynamic-filter-counter").length) {
            // var allItem = $('.single-filter-item').length;

            var activeFilterItem = $(".post-filter.has-dynamic-filter-counter").find(
                "li"
            );

            activeFilterItem.each(function () {
                var filterElement = $(this).data("filter");
                var count = $(".filter-layout").find(filterElement).length;
                $(this).append("<sup>[" + count + "]</sup>");
            });
        }

        vietjet_cuved_circle();
    });

    $(window).on("scroll", function () {
        OnePageMenuScroll();
        handleScrollbar();
        if ($(".sticky-header--one-page").length) {
            var headerScrollPos = 130;
            var stricky = $(".sticky-header--one-page");
            if ($(window).scrollTop() > headerScrollPos) {
                stricky.addClass("active");
            } else if ($(this).scrollTop() <= headerScrollPos) {
                stricky.removeClass("active");
            }
        }

        var scrollToTopBtn = ".scroll-to-top";
        if (scrollToTopBtn.length) {
            if ($(window).scrollTop() > 500) {
                $(scrollToTopBtn).addClass("show");
            } else {
                $(scrollToTopBtn).removeClass("show");
            }
        }
    });

    $(window).on("resize", function () {
        vietjet_stretch();
    });
})(jQuery);
