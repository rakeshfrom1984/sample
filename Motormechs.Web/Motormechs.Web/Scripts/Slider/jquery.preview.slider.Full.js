(function ($) {
    jps = function () {
        var self = this;
        this._init = function (element, options) {
            this.options = $.extend({}, this._settings, options);
            this.cssNames = {
                selected: this.options.cssprefix + "-selected",
                panel: this.options.cssprefix + "-panel",
                panels: this.options.cssprefix + "-panels",
                panelActive: this.options.cssprefix + "-panel-active",
                panelOld: this.options.cssprefix + "-panel-old",
                panelsWrapper: this.options.cssprefix + "-panels-wrapper",
                nav: this.options.cssprefix + "-nav",
                navClip: this.options.cssprefix + "-nav-clip",
                navBtn: this.options.cssprefix + "-nav-btn",
                navPrev: this.options.cssprefix + "-nav-prev",
                navNext: this.options.cssprefix + "-nav-next",
                btnDisable: this.options.cssprefix + "-btn-disable",
                btnPause: this.options.cssprefix + "-pause-btn",
                goPrev: this.options.cssprefix + "-go-prev",
                goNext: this.options.cssprefix + "-go-next",
                playBtn: this.options.cssprefix + "-play-btn",
                goBtns: this.options.cssprefix + "-go-btn"
            };
            this.domObj = $(element);
            this.panels = $("." + this.cssNames.panel, this.domObj);
            this.allItems = this.panels.size();
            this.nav = $("." + this.cssNames.nav, this.domObj);
            this.navClip = $("." + this.cssNames.navClip, this.nav);
            this.arePanels = this.allItems > 0 ? 1 : 0;
            this.isNavClip = this.navClip.size() > 0 ? 1 : 0;
            if (!this.arePanels && !this.isNavClip) {
                this._errorReport("Error #01", this.options.debug, 1)
            }
            this.domObjHeight = this.domObj.height();
            this.domObjWidth = this.domObj.width();
            if (!this.domObjHeight && !this.options.freeheight) {
                this.domObjHeight = this.options.height;
                this.domObj.css('height', this.domObjHeight);
                this._errorReport("Error #02", this.options.debug, 0)
            }
            if (!this.domObjWidth) {
                this.domObjWidth = this.options.width;
                this.domObj.css('width', this.domObjWidth);
                this._errorReport("Error #02", this.options.debug, 0)
            }
            this.domObj.css('display', 'block');
            this.currId = 0;
            this.prevId = 0;
            this.newId = 0;
            this.currPanel = null;
            this.prevPanel = 0;
            this.prevPanelStill = 0;
            this.firstTime = 1;
            this.scrollActive = 0;
            this.isPlaying = null;
            this.changeOngoing = false;
            this.currLine = 1;
            this.animating = false;
            this.panelAnteFns = new Array;
            this.panelPostFns = new Array;
            this.navAnteFns = new Array;
            this.navPostFns = new Array;
            this.runningScope = this.nav;
            if (this.isNavClip) {
                this._buildNav()
            }
            this._buildControls();
            if (this.arePanels) {
                this.panelsBag = $("." + this.cssNames.panels, this.domObj);
                if (this.options.panelfx == "sliding") {
                    this._wrapPanels()
                }
            }
            this.lineScrollDo = !this.arePanels ? 1 : 0;
            if (this.options.mousewheel) {
                this.domObj.mousewheel(function (event, delta) {
                    delta > 0 ? self.stepBackward() : self.stepForward();
                    return false
                })
            }
            if (this.options.keyboard) {
                this.domObj.keyup(function (event) {
                    if (event.keyCode == 37) {
                        self.stepBackward()
                    } else if (event.keyCode == 39) {
                        self.stepForward()
                    }
                })
            }
            if (this.options.panelclick && this.arePanels) {
                this.panelsBag.click(function () {
                    self.stepForward();
                    return false
                })
            }
            this.startId = this.options.start >= this.allItems ? this.allItems - 1 : this.options.start < 0 ? 0 : this.options.start;
            if (this.options.counter) {
                try {
                    this.Counter()
                } catch (err) {
                    this._errorReport(err, this.options.debug, 0)
                }
            }
            if (this.imageFx) {
                try {
                    this.imageFx()
                } catch (err) {
                    this._errorReport(err, this.options.debug, 0)
                }
            }
            if (this.options.delaycaptions) {
                try {
                    this.DelayCaptions(this.options.delaycaptions)
                } catch (err) {
                    this._errorReport(err, this.options.debug, 0)
                }
            }
            this.changeWithId(this.startId, null);
            if (this.options.auto) {
                this.autoScrollStart();
                this._autoScrollHoverStop()
            }
            if (this.options.timer) {
                try {
                    this.Timer(this.options.timer)
                } catch (err) {
                    this._errorReport(err, this.options.debug, 0)
                }
            }
            if (this.arePanels && !this.options.fastchange) {
                this.runningScope = this.domObj.find('.' + this.cssNames.panels, '.' + this.cssNames.nav)
            }
            return this
        };
        this._settings = {
            cssprefix: "jps",
            width: 500,
            height: 350,
            start: 0,
            auto: true,
            autospeed: 4000,
            autostill: false,
            mousewheel: false,
            keyboard: false,
            circular: false,
            shownavitems: 5,
            navitemshover: false,
            navclipcenter: false,
            navcontinuous: false,
            navscrollatend: false,
            navpanelautoswitch: true,
            navfx: "sliding",
            navfxbefore: function () { },
            navfxafter: function () { },
            scroll: null,
            scrollspeed: 600,
            scrolleasing: null,
            panelfx: "fading",
            panelfxspeed: 700,
            panelfxeasing: null,
            panelfxfirst: "none",
            panelfxbefore: function () { },
            panelfxafter: function () { },
            panelbtnshover: false,
            panelclick: false,
            verticalnav: false,
            verticalslide: false,
            tabs: false,
            freeheight: false,
            fastchange: true,
            counter: false,
            delaycaptions: false,
            timer: false,
            imagefx: false,
            debug: false
        };
        this._errorReport = function (errorCode, debug, stop) {
            if (debug) {
                alert("Slider Kit error!\nMessage = " + errorCode + " (see doc for details)\nElement id = " + this.domObj.attr("id") + "\nElement class = " + this.domObj.attr("class"))
            }
            if (stop) {
                return false
            }
        };
        this._autoScrollHoverStop = function () {
            if (!this.isPlayBtn && !this.options.autostill) {
                this.domObj.hover(function () {
                    if (self.isPlaying != null) {
                        //self.autoScrollStop()
                    }
                }, function () {
                    //self.autoScrollStart()
                })
            }
            if (this.options.autostill) {
                this.domObj.mouseleave(function () {
                    if (self.isPlaying == null) {
                        //self.autoScrollStart()
                    }
                })
            }
        };
        this._buildNav = function () {
            this.navUL = $("ul", this.navClip);
            this.navLI = $("li", this.navUL);
            this.navLINum = this.navLI.size();
            if (this.arePanels && (this.navLINum != this.allItems) && this.nav.size() == 1) {
                this._errorReport("Error #03", this.options.debug, 1)
            }
            if (this.options.tabs) {
                this.options.shownavitems = this.allItems
            } else {
                function getLImargin(attr) {
                    attrVal = self.navLI.css(attr);
                    if (attrVal != "auto" && attr != "" && attr != "0px") {
                        return parseInt(attrVal)
                    } else return 0
                }
                var navSize = this.options.verticalnav ? this.nav.height() : this.nav.width();
                var navLIWidth = this.navLI.outerWidth(true);
                var navLIHeight = this.navLI.outerHeight(true);
                var navLIextHMarg = getLImargin("margin-left") + getLImargin("margin-right");
                var navLIextVMarg = getLImargin("margin-top") + getLImargin("margin-bottom");
                this.allItems = this.navLINum;
                if (this.options.shownavitems > this.allItems) {
                    this.options.shownavitems = this.navLINum
                }
                this.navLIsize = this.options.verticalnav ? navLIHeight : navLIWidth;
                this.navULSize = this.navLIsize * this.navLINum;
                this.navClipSize = (this.options.shownavitems * this.navLIsize) - (this.options.verticalnav ? navLIextVMarg : navLIextHMarg);
                this.cssPosAttr = this.options.verticalnav ? "top" : "left";
                var cssSizeAttr = this.options.verticalnav ? "height" : "width";
                var cssSizeAttrr = this.options.verticalnav ? "width" : "height";
                this.navLI.css({
                    width: this.navLI.width(),
                    height: this.navLI.height()
                });
                this.navUL.css(cssSizeAttr, this.navULSize + "px");
                this.navClip.css({
                    width: this.options.verticalnav ? navLIWidth : this.navClipSize,
                    height: this.options.verticalnav ? this.navClipSize : navLIHeight
                });
                if (this.options.navclipcenter) {
                    this.navClip.css(this.cssPosAttr, (navSize - this.navClipSize) / 2).css("margin", "0")
                }
                if (this.allItems > this.options.shownavitems) {
                    this.scrollActive = true;
                    if (this.options.scroll == null || this.options.scroll < 0 || this.options.scroll > this.allItems) {
                        this.options.scroll = this.options.shownavitems
                    }
                }
                this.navBtns = $('.' + this.cssNames.navBtn, this.nav);
                if (this.navBtns.size() > 0) {
                    this._buildNavButtons()
                }
            }
            if (this.options.navitemshover && this.arePanels) {
                this.navLI.mouseover(function () {
                    self.changeWithId(getIndex(this, "li"), $(this))
                })
            } else if (this.arePanels || this.options.navscrollatend) {
                this.navLI.click(function () {
                    self.changeWithId(getIndex(this, "li"), $(this));
                    return false
                })
            }

            function getIndex(item, tag) {
                return $(tag, $(item).parent()).index(item)
            }
        };
        this._buildNavButtons = function () {
            if (this.scrollActive) {
                this.scrollBtns = true;
                this.navBtnPrev = $("." + this.cssNames.navPrev, this.nav);
                this.navBtnNext = $("." + this.cssNames.navNext, this.nav);
                this.navBtns.removeClass(this.cssNames.btnDisable);
                this.navBtnPrev.click(function () {
                    self.navPrev();
                    return false
                });
                this.navBtnNext.click(function () {
                    self.navNext();
                    return false
                });
                if (this.options.navcontinuous) {
                    this.navBtnPrev.mouseover(function () {
                        self.navPrev(true)
                    });
                    this.navBtnNext.mouseover(function () {
                        self.navNext(true)
                    });
                    this.navBtns.mouseout(function () {
                        self.navStopContinuous()
                    })
                }
                if (!this.options.circular) {
                    this.navBtnPrev.addClass(this.cssNames.btnDisable)
                }
            } else {
                this.navBtns.addClass(this.cssNames.btnDisable)
            }
        };
        this._getNavPos = function () {
            this.navPos = this.options.verticalnav ? this.navUL.position().top : this.navUL.position().left;
            this.LIbefore = Math.ceil(Math.abs(this.navPos) / this.navLIsize);
            this.LIafter = Math.floor((this.navULSize - Math.abs(this.navPos) - this.navClipSize) / this.navLIsize);
            if (this.LIafter < 0) {
                this.LIafter = 0
            }
        };
        this._buildControls = function () {
            this.playBtn = $("." + this.cssNames.playBtn, this.domObj);
            this.gBtns = $("." + this.cssNames.goBtns, this.domObj);
            this.isPlayBtn = this.playBtn.size() > 0 ? 1 : 0;
            this.goBtns = this.gBtns.size() > 0 ? 1 : 0;
            if (this.isPlayBtn) {
                if (this.options.auto) {
                    this.playBtn.addClass(this.cssNames.btnPause)
                }
                this.playBtn.click(function () {
                    if (self.playBtn.hasClass(self.cssNames.btnPause)) {
                        self.playBtnPause()
                    } else {
                        self.playBtnStart()
                    }
                    return false
                })
            }
            if (this.goBtns) {
                this.goBtnPrev = $("." + this.cssNames.goPrev, this.domObj);
                this.goBtnNext = $("." + this.cssNames.goNext, this.domObj);
                if (this.options.panelbtnshover) {
                    this.gBtns.hide();
                    $("." + this.cssNames.panels, this.domObj).hover(function () {
                        self.gBtns.fadeIn()
                    }, function () {
                        self.gBtns.fadeOut()
                    })
                }
                this.goBtnPrev.click(function () {
                    self.stepBackward($(this));
                    return false
                });
                this.goBtnNext.click(function () {
                    self.stepForward($(this));
                    return false
                })
            }
        };
        this._wrapPanels = function () {
            if ($('.' + this.cssNames.panelsWrapper, this.domObj).size() == 0) {
                this.panels.wrapAll('<div class="' + this.cssNames.panelsWrapper + '"></div>');
                this.panelsWrapper = $('.' + this.cssNames.panelsWrapper, this.panelsBag);
                this.panelsWrapper.css('position', 'relative')
            }
        };
        this._change = function (eventSrc, scrollWay, goToId, lineScrolling, stopAuto) {
            if (stopAuto && this.isPlaying != null) {
                if (this.isPlayBtn) {
                    this.playBtnPause()
                }
                if (this.options.autostill) {
                    self.autoScrollStop()
                }
            }
            if (eventSrc) {
                if (eventSrc.hasClass(this.cssNames.btnDisable)) {
                    return false
                }
            }
            var stopGoing = 0;
            var running = $(':animated', this.runningScope).size() > 0 ? 1 : 0;
            if (!running && !this.animating) {
                this.prevId = this.currId;
                if (goToId == null && !lineScrolling) {
                    this.currId = scrollWay == "-=" ? this.currId + 1 : this.currId - 1
                } else if (goToId != null) {
                    goToId = parseInt(goToId);
                    this.currId = goToId < 0 ? 0 : goToId > this.allItems - 1 ? this.allItems - 1 : goToId;
                    var checkIdRange = eventSrc ? eventSrc.parent().parent().hasClass(this.cssNames.navClip) ? false : true : true
                }
                if (this.goBtns) {
                    this.gBtns.removeClass(this.cssNames.btnDisable)
                }
                if (!this.options.circular) {
                    if (this.currId == -1) {
                        this.currId = 0;
                        stopGoing = 1
                    }
                    if (this.currId == 0 && this.goBtns) {
                        this.goBtnPrev.addClass(this.cssNames.btnDisable)
                    }
                    if (this.currId == this.allItems) {
                        this.currId = this.allItems - 1;
                        stopGoing = 1
                    }
                    if (this.currId == this.allItems - 1) {
                        if (this.options.auto) {
                            this.autoScrollStop()
                        }
                        if (this.goBtns) {
                            this.goBtnNext.addClass(this.cssNames.btnDisable)
                        }
                    }
                } else if (!this.scrollActive) {
                    if (this.currId == this.allItems) {
                        this.currId = 0
                    }
                    if (this.currId == -1) {
                        this.currId = this.allItems - 1
                    }
                }
                if (this.scrollActive && !stopGoing) {
                    this._setNavScroll(lineScrolling, scrollWay, checkIdRange)
                }
                if (this.isNavClip) {
                    this.selectThumbnail(this.currId)
                }
                if (!(lineScrolling && !this.options.navpanelautoswitch)) {
                    if (this.arePanels) {
                        this._animPanel(this.currId, scrollWay)
                    }
                }
                if (this.firstTime) {
                    this.firstTime = 0
                }
            }
        };
        this._setNavScroll = function (lineScrolling, scrollWay, checkIdRange) {
            this._getNavPos();
            var triggerLineScroll = lineScrolling ? true : false;
            var jumpToId = 0;
            if (!lineScrolling) {
                var idFromClipStart = Math.abs(this.currId + 1 - this.LIbefore);
                var idToClipEnd = this.options.shownavitems - idFromClipStart + 1;
                var currIdOnEdge = this.currId == 0 || this.currId == this.allItems - 1 ? 1 : 0;
                if ((this.options.navscrollatend && (idToClipEnd == 1 || idFromClipStart == 1)) && !this.firstTime && !currIdOnEdge) {
                    jumpToId = this.options.scroll - 1;
                    triggerLineScroll = true
                }
                if (idToClipEnd == 0 || idFromClipStart == 0) {
                    triggerLineScroll = true
                }
                if (checkIdRange) {
                    if (idToClipEnd < 0) {
                        idToClipEnd = 0
                    }
                    scrollWay = this.prevId < this.currId ? '-=' : '+=';
                    var idDiff = Math.abs(this.prevId - this.currId);
                    if ((idDiff - 1 > idToClipEnd && scrollWay == '-=') || (idDiff > idFromClipStart && scrollWay == '+=')) {
                        jumpToId = idDiff;
                        triggerLineScroll = true
                    }
                }
                if (scrollWay == "") {
                    if (this.prevId == this.currId && !currIdOnEdge) {
                        scrollWay = this.scrollWay == "-=" ? "+=" : "-="
                    } else {
                        scrollWay = this.prevId < this.currId ? "-=" : "+="
                    }
                }
                this.scrollWay = scrollWay
            }
            if (triggerLineScroll) {
                var scrollPower = jumpToId > 0 ? jumpToId : this.options.scroll;
                var LIremain = scrollWay == "-=" ? this.LIafter : this.LIbefore;
                var scrollto = LIremain < scrollPower ? LIremain : scrollPower;
                var scrollSize = scrollto * this.navLIsize;
                this.newId = scrollWay == "-=" ? this.LIbefore + scrollto : this.LIbefore - scrollto + this.options.shownavitems - 1;
                if ((scrollWay == "-=" && this.newId > this.currId) || (scrollWay == "+=" && this.newId < this.currId)) {
                    this.currId = this.newId
                }
                if (this.options.circular) {
                    if (this.LIbefore <= 0 && scrollWay == "+=") {
                        scrollWay = "-=";
                        this.currId = this.allItems - 1;
                        scrollSize = (this.LIafter / this.options.scroll) * (this.navLIsize * this.options.scroll)
                    } else if (this.LIafter == 0 && scrollWay == "-=") {
                        scrollWay = "+=";
                        this.currId = 0;
                        scrollSize = Math.abs(this.navPos)
                    }
                }
                this._animNav(scrollWay, scrollSize)
            }
        };
        this._animPanel = function (currId, scrollWay) {
            this.currPanel = this.panels.eq(currId);
            this.prevPanelStill = this.panels.eq(this.prevId);
            var panelComplete = function () {
                if ($.isFunction(self.options.panelfxafter)) {
                    self.options.panelfxafter()
                }
                self._runCallBacks(self.panelPostFns)
            };
            if (!this.currPanel.hasClass(this.cssNames.panelActive)) {
                if (this.firstTime) {
                    this.panelTransition = this.options.panelfxfirst;
                    var FirstTime = 1
                } else {
                    var freeheightfx = this.options.freeheight && this.options.panelfx == "fading" ? "tabsfading" : "sliding";
                    this.panelTransition = this.options.freeheight ? freeheightfx : this.options.panelfx
                }
                if ($.isFunction(self.options.panelfxbefore)) {
                    self.options.panelfxbefore()
                }
                this._runCallBacks(this.panelAnteFns);
                this._panelTransitions[this.panelTransition](scrollWay, FirstTime, panelComplete)
            }
        };
        this._animNav = function (scrollWay, scrollSize) {
            var navComplete = function () {
                if (!self.options.circular && self.scrollBtns) {
                    self.navBtns.removeClass(self.cssNames.btnDisable);
                    self._getNavPos();
                    if (self.LIbefore <= 0) {
                        self.navBtnPrev.addClass(self.cssNames.btnDisable)
                    } else if (self.LIafter <= 0) {
                        self.navBtnNext.addClass(self.cssNames.btnDisable)
                    }
                }
                if (self.scrollcontinue) {
                    setTimeout(function () {
                        self.scrollcontinue == "-=" ? self.navPrev() : self.navNext()
                    }, 0)
                } else if ($.isFunction(self.options.navfxafter)) {
                    self.options.navfxafter()
                }
                self._runCallBacks(self.navPostFns)
            };
            if ($.isFunction(self.options.navfxbefore)) {
                self.options.navfxbefore()
            }
            self._runCallBacks(self.navAnteFns);
            this._navTransitions[this.options.navfx](scrollWay, scrollSize, navComplete)
        };
        this._runCallBacks = function (fns) {
            $.each(fns, function (index, item) {
                if ($.isFunction(item)) {
                    item()
                }
            })
        };
        this._clearCallBacks = function (fns) {
            fns.length = 0
        };
        this._panelTransitions = {
            none: function (scrollWay, FirstTime, complete) {
                self.panels.removeClass(self.cssNames.panelActive).hide();
                self.currPanel.addClass(self.cssNames.panelActive).show();
                complete()
            },
            sliding: function (scrollWay, FirstTime, complete) {
                if (scrollWay == "") {
                    scrollWay = self.prevPanel < self.currId ? "-=" : "+="
                }
                self.prevPanel = self.currId;
                var cssPosValue = scrollWay == "-=" ? "+" : "-";
                var cssSlidePosAttr = self.options.verticalslide ? "top" : "left";
                var domObjSize = self.options.verticalnav ? self.domObjHeight : document.body.offsetWidth;

                function resize() {
                    var domObjSize = document.body.offsetWidth
                }
                var slideScrollValue = cssSlidePosAttr == "top" ? {
                    top: scrollWay + domObjSize
                } : {
                    left: scrollWay + domObjSize
                };
                self.oldPanel = $("." + self.cssNames.panelOld, self.domObj);
                self.activePanel = $("." + self.cssNames.panelActive, self.domObj);
                self.panels.css(cssSlidePosAttr, "0");
                self.oldPanel.removeClass(self.cssNames.panelOld).hide();
                self.activePanel.removeClass(self.cssNames.panelActive).addClass(self.cssNames.panelOld);
                self.currPanel.addClass(self.cssNames.panelActive).css(cssSlidePosAttr, cssPosValue + domObjSize + "px").show();
                self.panelsWrapper.stop(true, true).css(cssSlidePosAttr, "0").animate(slideScrollValue, self.options.panelfxspeed, self.options.panelfxeasing, function () {
                    complete()
                })
            },
            fading: function (scrollWay, FirstTime, complete) {
                if (FirstTime) {
                    self.panels.hide()
                } else {
                    self.currPanel.css("display", "none")
                }
                $("." + self.cssNames.panelOld, self.domObj).removeClass(self.cssNames.panelOld);
                $("." + self.cssNames.panelActive, self.domObj).stop(true, true).removeClass(self.cssNames.panelActive).addClass(self.cssNames.panelOld);
                self.currPanel.addClass(self.cssNames.panelActive).animate({
                    "opacity": "show"
                }, self.options.panelfxspeed, self.options.panelfxeasing, function () {
                    complete()
                })
            },
            tabsfading: function (scrollWay, FirstTime, complete) {
                self.panels.removeClass(self.cssNames.panelActive).hide();
                self.currPanel.fadeIn(self.options.panelfxspeed, function () {
                    complete()
                })
            }
        };
        this._navTransitions = {
            none: function (scrollWay, scrollSize, complete) {
                var newScrollSize = scrollWay == "-=" ? self.navPos - scrollSize : self.navPos + scrollSize;
                self.navUL.css(self.cssPosAttr, newScrollSize + "px");
                complete()
            },
            sliding: function (scrollWay, scrollSize, complete) {
                self.navUL.animate(self.cssPosAttr == "left" ? {
                    left: scrollWay + scrollSize
                } : {
                    top: scrollWay + scrollSize
                }, self.options.scrollspeed, self.options.scrolleasing, function () {
                    complete()
                })
            }
        };
        this.playBtnPause = function () {
            this.playBtn.removeClass(this.cssNames.btnPause);
            this.autoScrollStop()
        };
        this.playBtnStart = function () {
            this.playBtn.addClass(self.cssNames.btnPause);
            this.autoScrollStart()
        };
        this.autoScrollStart = function () {
            var self = this;
            this.isPlaying = setInterval(function () {
                self._change(null, "-=", null, self.lineScrollDo, null)
            }, self.options.autospeed)
        };
        this.autoScrollStop = function () {
            clearTimeout(this.isPlaying);
            this.isPlaying = null
        };
        this.changeWithId = function (id, eventSrc) {
            this._change(eventSrc, "", id, 0, 1)
        };
        this.stepBackward = function (eventSrc) {
            this._change(eventSrc, "+=", null, self.lineScrollDo, 1)
        };
        this.stepForward = function (eventSrc) {
            this._change(eventSrc, "-=", null, self.lineScrollDo, 1)
        };
        this.navPrev = function (c) {
            if (c) {
                self.scrollcontinue = "-="
            }
            this._change(this.navBtnPrev, "+=", null, 1, 1)
        };
        this.navNext = function (c) {
            if (c) {
                self.scrollcontinue = "+="
            }
            this._change(this.navBtnNext, "-=", null, 1, 1)
        };
        this.navStopContinuous = function () {
            self.scrollcontinue = ""
        };
        this.selectThumbnail = function (currId) {
            $("." + this.cssNames.selected, this.navUL).removeClass(this.cssNames.selected);
            this.navLI.eq(currId).addClass(this.cssNames.selected)
        }
    };
    $.fn.jps = function (options) {
        return this.each(function () {
            $(this).data("jps", new jps()._init(this, options))
        })
    }
})(jQuery);