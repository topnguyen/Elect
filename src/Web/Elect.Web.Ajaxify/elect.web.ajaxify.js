var elect = elect || {};

elect.ajaxify = {
    // Enable or Not
    isEnable: true, // Global enable
    preventAjaxifySelector: '.no-ajaxify', // No-Ajaxify for specific anchor tag

    // Document / Main Content
    bodySelector: '.document-body',
    scriptSelector: '.document-script',
    contentSelector: '#document-content,article:first,.article:first,.post:first',

    // Menu
    menuSelector: '#menu,#nav,nav:first,.nav:first',
    activeClass: 'active selected current',
    activeSelector: '.active,.selected,.current',
    menuChildrenSelector: '> li,> ul > li',

    // Callback Functions
    fnBeforeRedirect: undefined, // trigger before state change, if return false will not execute state change
    fnAfterRedirect: undefined // trigger after finish state change 
};

(function (window, undefined) {
    var History = window.History;
    var $ = window.jQuery;
    var document = window.document;

    // Check to see if History.js is enabled for our Browser
    if (!History.enabled) {
        return false;
    }

    // Wait for Document
    $(function () {
        var $content = $(elect.ajaxify.contentSelector).filter(':first');
        var contentNode = $content.get(0);
        var $menu = $(elect.ajaxify.menuSelector).filter(':first');
        var $window = $(window);
        var $body = $(document.body);
        var rootUrl = History.getRootUrl();

        // Ensure Content
        if ($content.length === 0) {
            $content = $body;
        }

        // Internal Helper
        $.expr[':'].internal = function (obj, index, meta, stack) {
            // Prepare
            var
                $this = $(obj),
                url = $this.attr('href') || '',
                isInternalLink;

            // Check link
            var isSameRoot = url.substring(0, rootUrl.length) === rootUrl;
            var isPartialUrl = url.indexOf(':') === -1;

            isInternalLink = isSameRoot || isPartialUrl;

            if (isPartialUrl === true) {
                if (url.startsWith("#")) {
                    return false;
                }
            }

            if (isSameRoot === true) {
                var path = url.replace(window.location.href, '');

                if (path.startsWith("#") || path.startsWith("/#")) {
                    return false;
                }
            }

            // Ignore or Keep
            return isInternalLink;
        };

        // HTML Helper
        var documentHtml = function (html) {
            // Prepare
            var result = String(html)
                .replace(/<\!DOCTYPE[^>]*>/i, '')
                .replace(/<(html|head|body|title|meta|script)([\s\>])/gi, '<div class="document-$1"$2')
                .replace(/<\/(html|head|body|title|meta|script)\>/gi, '</div>')
            ;

            // Return
            return $.trim(result);
        };

        var decodeHtml = function (value) {
            var txt = document.createElement('textarea');
            txt.innerHTML = value;
            return txt.value;
        };

        // Ajaxify Helper
        $.fn.ajaxify = function () {
            // Prepare
            var $this = $(this);

            // Ajaxify
            $this.find('a:internal:not(' + elect.ajaxify.preventAjaxifySelector + ')').click(function (event) {
                // Prepare
                var
                    $this = $(this),
                    url = $this.attr('href'),
                    title = $this.attr('title') || null;

                if (elect.ajaxify.isEnable !== true) {
                    return true;
                }

                // before redirect check
                var fnBeforeRedirect = elect.ajaxify.fnBeforeRedirect;
                if (fnBeforeRedirect && typeof fnBeforeRedirect === 'function') {
                    var result = fnBeforeRedirect($this, url);

                    // End if result is false
                    if (result === false) {
                        event.preventDefault();
                        return;
                    }
                }

                // Continue as normal for cmd clicks etc
                if (event.which == 2 || event.metaKey) {
                    return true;
                }

                // Ajaxify this link
                History.pushState(null, title, url);
                event.preventDefault();
                return false;
            });

            // Chain
            return $this;
        };

        // Ajaxify our Internal Links
        $body.ajaxify();

        // Hook into State Changes
        $window.bind('statechange', function () {
            // Prepare Variables
            var
                State = History.getState(),
                url = State.url,
                relativeUrl = url.replace(rootUrl, '');

            // Set Loading
            $body.addClass('loading');

            // Start Fade Out
            // Animating to opacity to 0 still keeps the element's height intact
            // Which prevents that annoying pop bang issue when loading in new content
            $content.animate({opacity: 0}, 800);

            // Ajax Request the Traditional Page
            $.ajax({
                url: url,
                method: 'GET',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("X-Requested-With", "Elect.Web.Ajaxify");
                },
                success: function (data, textStatus, jqXHR) {
                    // Prepare
                    var
                        dataHtml = documentHtml(data),
                        $data = $(dataHtml),
                        $dataBody = $data.find(elect.ajaxify.bodySelector + ':first'),
                        $dataContent = $dataBody.find(elect.ajaxify.contentSelector).filter(':first'),
                        $menuChildren, contentHtml, $scripts;

                    var iStartMainScriptBlock = dataHtml.indexOf("<div class=\"document-main-script-start\"></div>") + "<div class=\"document-main-script-start\"></div>".length + 1;
                    var iEndMainScriptBlock = dataHtml.lastIndexOf("<div class=\"document-main-script-end\"></div>");
                    var mainScriptBlock = dataHtml.substr(iStartMainScriptBlock, iEndMainScriptBlock - iStartMainScriptBlock).trim();
                    var mainScripts = mainScriptBlock.split("<div class=\"document-script\"");

                    for (var i = 0; i < mainScripts.length; i++) {
                        var mainScript = mainScripts[i];

                        var iStartMainScript = mainScript.indexOf(">") + 1;

                        var divStartContent = mainScript.substr(0, iStartMainScript);

                        if (divStartContent.includes("src")) {
                            mainScripts[i] = mainScript = "<div class=\"document-script\"" + mainScript;
                            continue;
                        }

                        var iEndMainScript = mainScript.lastIndexOf("</div>");
                        mainScripts[i] = mainScript = mainScript.substr(iStartMainScript, iEndMainScript - iStartMainScript).trim();
                    }

                    window.mainScripts = mainScripts;

                    // Fetch the scripts
                    $scripts = $dataContent.find(elect.ajaxify.scriptSelector);

                    if ($scripts.length) {
                        $scripts.detach();
                    }

                    // Fetch the content
                    contentHtml = $dataContent.html() || $data.html();

                    if (!contentHtml) {
                        document.location.href = url;
                        return false;
                    }

                    // Update the menu

                    $menuChildren = $menu.find(elect.ajaxify.menuChildrenSelector);

                    $menuChildren.filter(elect.ajaxify.activeSelector).removeClass(elect.ajaxify.activeClass);

                    $menuChildren = $menuChildren.has('a[href^="' + relativeUrl + '"],a[href^="/' + relativeUrl + '"],a[href^="' + url + '"]');

                    if ($menuChildren.length === 1) {
                        $menuChildren.addClass(elect.ajaxify.activeClass);
                    }

                    // Update the content

                    $content.stop(true, true);

                    $content.html(contentHtml).ajaxify().css('opacity', 100).show(); /* you could fade in here if you'd like */

                    // Update the title

                    document.title = $data.find('.document-title:first').text();
                    try {
                        document.getElementsByTagName('title')[0].innerHTML = document.title.replace('<', '&lt;').replace('>', '&gt;').replace(' & ', ' &amp; ');
                    } catch (Exception) {
                        // Ignore
                    }

                    window.onerror = function () {
                        location.reload();
                    };

                    // Add the scripts
                    for (var i = 0; i < $scripts.length; i++) {
                        var $script = $($scripts[i]);

                        var scriptHtml = decodeHtml($script.html());

                        var scriptNode = document.createElement('script');

                        if ($script.attr('src')) {
                            if (!$script[0].async) {
                                scriptNode.async = false;
                            }
                            scriptNode.src = $script.attr('src');
                        } else {
                            scriptHtml = mainScripts[i];
                        }

                        scriptNode.appendChild(document.createTextNode(scriptHtml));

                        contentNode.appendChild(scriptNode);
                    }

                    // Complete the change
                    window.scrollTo(0, 0);

                    $body.removeClass('loading');

                    var fnAfterRedirect = elect.ajaxify.fnAfterRedirect;
                    if (fnAfterRedirect && typeof fnAfterRedirect === 'function') {
                        fnAfterRedirect(url);
                    }

                    // Inform Google Analytics of the change
                    if (typeof window._gaq !== 'undefined') {
                        window._gaq.push(['_trackPageview', relativeUrl]);
                    }

                    // Inform ReInvigorate of a state change
                    if (typeof window.reinvigorate !== 'undefined' && typeof window.reinvigorate.ajax_track !== 'undefined') {
                        reinvigorate.ajax_track(url);
                        // ^ we use the full url here as that is what reinvigorate supports
                    }

                    window.onerror = function () {
                    };

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    document.location.href = url;
                    return false;
                }
            });
        });
    });
})(window);
