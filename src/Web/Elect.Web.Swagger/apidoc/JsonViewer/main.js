var jsonViewerSessionStorageKey = "JsonViewer";
var defaultData = '{"message": "Welcome to Json Viewer","author": "Top Nguyen", "website": "http://topnguyen.net"}'

function trim(s, c) {
    if (c === "]") c = "\\]";
    if (c === "\\") c = "\\\\";
    return s.replace(new RegExp(
        "^[" + c + "]+|[" + c + "]+$", "g"
    ), "");
}

! function (t) {
    function e(r) { if (n[r]) return n[r].exports; var o = n[r] = { exports: {}, id: r, loaded: !1 }; return t[r].call(o.exports, o, o.exports, e), o.loaded = !0, o.exports }
    var n = {};
    return e.m = t, e.c = n, e.p = "/", e(0)
}(function (t) {
    var sessionStorageData = sessionStorage.getItem(jsonViewerSessionStorageKey);
    if (!sessionStorageData || sessionStorageData == "undefined") {
        sessionStorageData = defaultData;
    }

    try {
        // try parse to check data is actual json string
        if (sessionStorageData.startsWith("\"{")) {
            sessionStorageData = trim(sessionStorageData, "\"").replace(/\\"/g, '"');
        }
        var jsonData = JSON.parse(sessionStorageData.trim());
        sessionStorageData = JSON.stringify(jsonData, null, 4);
        sessionStorage.setItem(jsonViewerSessionStorageKey, sessionStorageData);
    } catch (exception) {
        var jsonData = { "data": sessionStorageData };
        sessionStorageData = JSON.stringify(jsonData, null, 4);
        sessionStorage.setItem(jsonViewerSessionStorageKey, sessionStorageData);
        console.log('The data is wrong json string format, Json Viewer try parse to new data.');
        console.log('JsonViewerData', jsonData);
    } finally {
        var sessionStorageData = sessionStorage.getItem(jsonViewerSessionStorageKey);
        var preElement = document.createElement("pre");
        preElement.innerHTML = sessionStorageData;
        document.body.insertBefore(preElement, document.body.firstChild)

        for (var e in t)
            if (Object.prototype.hasOwnProperty.call(t, e)) switch (typeof t[e]) {
                case "function":
                    break;
                case "object":
                    t[e] = function (e) {
                        var n = e.slice(1),
                            r = t[e[0]];
                        return function (t, e, o) { r.apply(this, [t, e, o].concat(n)) }
                    }(t[e]);
                    break;
                default:
                    t[e] = t[t[e]]
            }
        return t
    }
}([function (t, e, n) { n(276), t.exports = n(121) }, function (t, e, n) {
    "use strict";

    function r(t, e, n, r, i, a, u, s) {
        if (o(e), !t) {
            var c;
            if (void 0 === e) c = new Error("Minified exception occurred; use the non-minified dev environment for the full error message and additional helpful warnings.");
            else {
                var l = [n, r, i, a, u, s],
                    f = 0;
                c = new Error(e.replace(/%s/g, function () { return l[f++] })), c.name = "Invariant Violation"
            }
            throw c.framesToPop = 1, c
        }
    }
    var o = function (t) { };
    t.exports = r
}, function (t, e, n) {
    "use strict";
    var r = n(7),
        o = r;
    t.exports = o
}, function (t, e) {
    "use strict";

    function n(t) {
        for (var e = arguments.length - 1, n = "Minified React error #" + t + "; visit http://facebook.github.io/react/docs/error-decoder.html?invariant=" + t, r = 0; r < e; r++) n += "&args[]=" + encodeURIComponent(arguments[r + 1]);
        n += " for the full message or use the non-minified dev environment for full errors and additional helpful warnings.";
        var o = new Error(n);
        throw o.name = "Invariant Violation", o.framesToPop = 1, o
    }
    t.exports = n
}, function (t, e) {
    "use strict";

    function n(t) { if (null === t || void 0 === t) throw new TypeError("Object.assign cannot be called with null or undefined"); return Object(t) }

    function r() { try { if (!Object.assign) return !1; var t = new String("abc"); if (t[5] = "de", "5" === Object.getOwnPropertyNames(t)[0]) return !1; for (var e = {}, n = 0; n < 10; n++) e["_" + String.fromCharCode(n)] = n; var r = Object.getOwnPropertyNames(e).map(function (t) { return e[t] }); if ("0123456789" !== r.join("")) return !1; var o = {}; return "abcdefghijklmnopqrst".split("").forEach(function (t) { o[t] = t }), "abcdefghijklmnopqrst" === Object.keys(Object.assign({}, o)).join("") } catch (t) { return !1 } }
    var o = Object.getOwnPropertySymbols,
        i = Object.prototype.hasOwnProperty,
        a = Object.prototype.propertyIsEnumerable;
    t.exports = r() ? Object.assign : function (t, e) { for (var r, u, s = n(t), c = 1; c < arguments.length; c++) { r = Object(arguments[c]); for (var l in r) i.call(r, l) && (s[l] = r[l]); if (o) { u = o(r); for (var f = 0; f < u.length; f++) a.call(r, u[f]) && (s[u[f]] = r[u[f]]) } } return s }
}, function (t, e, n) {
    "use strict";

    function r(t, e) { return 1 === t.nodeType && t.getAttribute(d) === String(e) || 8 === t.nodeType && t.nodeValue === " react-text: " + e + " " || 8 === t.nodeType && t.nodeValue === " react-empty: " + e + " " }

    function o(t) { for (var e; e = t._renderedComponent;) t = e; return t }

    function i(t, e) {
        var n = o(t);
        n._hostNode = e, e[g] = n
    }

    function a(t) {
        var e = t._hostNode;
        e && (delete e[g], t._hostNode = null)
    }

    function u(t, e) {
        if (!(t._flags & v.hasCachedChildNodes)) {
            var n = t._renderedChildren,
                a = e.firstChild;
            t: for (var u in n)
                if (n.hasOwnProperty(u)) {
                    var s = n[u],
                        c = o(s)._domID;
                    if (0 !== c) {
                        for (; null !== a; a = a.nextSibling)
                            if (r(a, c)) { i(s, a); continue t }
                        f("32", c)
                    }
                }
            t._flags |= v.hasCachedChildNodes
        }
    }

    function s(t) {
        if (t[g]) return t[g];
        for (var e = []; !t[g];) {
            if (e.push(t), !t.parentNode) return null;
            t = t.parentNode
        }
        for (var n, r; t && (r = t[g]); t = e.pop()) n = r, e.length && u(r, t);
        return n
    }

    function c(t) { var e = s(t); return null != e && e._hostNode === t ? e : null }

    function l(t) { if (void 0 === t._hostNode ? f("33") : void 0, t._hostNode) return t._hostNode; for (var e = []; !t._hostNode;) e.push(t), t._hostParent ? void 0 : f("34"), t = t._hostParent; for (; e.length; t = e.pop()) u(t, t._hostNode); return t._hostNode }
    var f = n(3),
        p = n(18),
        h = n(93),
        d = (n(1), p.ID_ATTRIBUTE_NAME),
        v = h,
        g = "__reactInternalInstance$" + Math.random().toString(36).slice(2),
        y = { getClosestInstanceFromNode: s, getInstanceFromNode: c, getNodeFromInstance: l, precacheChildNodes: u, precacheNode: i, uncacheNode: a };
    t.exports = y
}, function (t, e) {
    "use strict";
    var n = !("undefined" == typeof window || !window.document || !window.document.createElement),
        r = { canUseDOM: n, canUseWorkers: "undefined" != typeof Worker, canUseEventListeners: n && !(!window.addEventListener && !window.attachEvent), canUseViewport: n && !!window.screen, isInWorker: !n };
    t.exports = r
}, function (t, e) {
    "use strict";

    function n(t) { return function () { return t } }
    var r = function () { };
    r.thatReturns = n, r.thatReturnsFalse = n(!1), r.thatReturnsTrue = n(!0), r.thatReturnsNull = n(null), r.thatReturnsThis = function () { return this }, r.thatReturnsArgument = function (t) { return t }, t.exports = r
}, function (t, e, n) {
    "use strict";
    var r = null;
    t.exports = { debugTool: r }
}, function (t, e, n) {
    "use strict";

    function r() { T.ReactReconcileTransaction && w ? void 0 : l("123") }

    function o() { this.reinitializeTransaction(), this.dirtyComponentsLength = null, this.callbackQueue = p.getPooled(), this.reconcileTransaction = T.ReactReconcileTransaction.getPooled(!0) }

    function i(t, e, n, o, i, a) { return r(), w.batchedUpdates(t, e, n, o, i, a) }

    function a(t, e) { return t._mountOrder - e._mountOrder }

    function u(t) {
        var e = t.dirtyComponentsLength;
        e !== y.length ? l("124", e, y.length) : void 0, y.sort(a), m++;
        for (var n = 0; n < e; n++) {
            var r = y[n],
                o = r._pendingCallbacks;
            r._pendingCallbacks = null;
            var i;
            if (d.logTopLevelRenders) {
                var u = r;
                r._currentElement.type.isReactTopLevelWrapper && (u = r._renderedComponent), i = "React update: " + u.getName(), console.time(i)
            }
            if (v.performUpdateIfNecessary(r, t.reconcileTransaction, m), i && console.timeEnd(i), o)
                for (var s = 0; s < o.length; s++) t.callbackQueue.enqueue(o[s], r.getPublicInstance())
        }
    }

    function s(t) { return r(), w.isBatchingUpdates ? (y.push(t), void (null == t._updateBatchNumber && (t._updateBatchNumber = m + 1))) : void w.batchedUpdates(s, t) }

    function c(t, e) { w.isBatchingUpdates ? void 0 : l("125"), b.enqueue(t, e), x = !0 }
    var l = n(3),
        f = n(4),
        p = n(91),
        h = n(14),
        d = n(96),
        v = n(19),
        g = n(36),
        y = (n(1), []),
        m = 0,
        b = p.getPooled(),
        x = !1,
        w = null,
        _ = { initialize: function () { this.dirtyComponentsLength = y.length }, close: function () { this.dirtyComponentsLength !== y.length ? (y.splice(0, this.dirtyComponentsLength), M()) : y.length = 0 } },
        C = { initialize: function () { this.callbackQueue.reset() }, close: function () { this.callbackQueue.notifyAll() } },
        E = [_, C];
    f(o.prototype, g, { getTransactionWrappers: function () { return E }, destructor: function () { this.dirtyComponentsLength = null, p.release(this.callbackQueue), this.callbackQueue = null, T.ReactReconcileTransaction.release(this.reconcileTransaction), this.reconcileTransaction = null }, perform: function (t, e, n) { return g.perform.call(this, this.reconcileTransaction.perform, this.reconcileTransaction, t, e, n) } }), h.addPoolingTo(o);
    var M = function () {
        for (; y.length || x;) {
            if (y.length) {
                var t = o.getPooled();
                t.perform(u, null, t), o.release(t)
            }
            if (x) {
                x = !1;
                var e = b;
                b = p.getPooled(), e.notifyAll(), p.release(e)
            }
        }
    },
        k = { injectReconcileTransaction: function (t) { t ? void 0 : l("126"), T.ReactReconcileTransaction = t }, injectBatchingStrategy: function (t) { t ? void 0 : l("127"), "function" != typeof t.batchedUpdates ? l("128") : void 0, "boolean" != typeof t.isBatchingUpdates ? l("129") : void 0, w = t } },
        T = { ReactReconcileTransaction: null, batchedUpdates: i, enqueueUpdate: s, flushBatchedUpdates: M, injection: k, asap: c };
    t.exports = T
}, function (t, e, n) {
    "use strict";

    function r(t, e, n, r) {
        this.dispatchConfig = t, this._targetInst = e, this.nativeEvent = n;
        var o = this.constructor.Interface;
        for (var i in o)
            if (o.hasOwnProperty(i)) {
                var u = o[i];
                u ? this[i] = u(n) : "target" === i ? this.target = r : this[i] = n[i]
            }
        var s = null != n.defaultPrevented ? n.defaultPrevented : n.returnValue === !1;
        return s ? this.isDefaultPrevented = a.thatReturnsTrue : this.isDefaultPrevented = a.thatReturnsFalse, this.isPropagationStopped = a.thatReturnsFalse, this
    }
    var o = n(4),
        i = n(14),
        a = n(7),
        u = (n(2), "function" == typeof Proxy, ["dispatchConfig", "_targetInst", "nativeEvent", "isDefaultPrevented", "isPropagationStopped", "_dispatchListeners", "_dispatchInstances"]),
        s = { type: null, target: null, currentTarget: a.thatReturnsNull, eventPhase: null, bubbles: null, cancelable: null, timeStamp: function (t) { return t.timeStamp || Date.now() }, defaultPrevented: null, isTrusted: null };
    o(r.prototype, {
        preventDefault: function () {
            this.defaultPrevented = !0;
            var t = this.nativeEvent;
            t && (t.preventDefault ? t.preventDefault() : "unknown" != typeof t.returnValue && (t.returnValue = !1), this.isDefaultPrevented = a.thatReturnsTrue)
        },
        stopPropagation: function () {
            var t = this.nativeEvent;
            t && (t.stopPropagation ? t.stopPropagation() : "unknown" != typeof t.cancelBubble && (t.cancelBubble = !0), this.isPropagationStopped = a.thatReturnsTrue)
        },
        persist: function () { this.isPersistent = a.thatReturnsTrue },
        isPersistent: a.thatReturnsFalse,
        destructor: function () { var t = this.constructor.Interface; for (var e in t) this[e] = null; for (var n = 0; n < u.length; n++) this[u[n]] = null }
    }), r.Interface = s, r.augmentClass = function (t, e) {
        var n = this,
            r = function () { };
        r.prototype = n.prototype;
        var a = new r;
        o(a, t.prototype), t.prototype = a, t.prototype.constructor = t, t.Interface = o({}, n.Interface, e), t.augmentClass = n.augmentClass, i.addPoolingTo(t, i.fourArgumentPooler)
    }, i.addPoolingTo(r, i.fourArgumentPooler), t.exports = r
}, function (t, e) {
    "use strict";
    var n = { current: null };
    t.exports = n
}, function (t, e) {
    var n = Array.isArray;
    t.exports = n
}, function (t, e) {
    function n(t) { return !!t && "object" == typeof t }
    t.exports = n
},
[291, 3],
function (t, e, n) {
    var r = n(40),
        o = n(32),
        i = r(o, "Map");
    t.exports = i
},
function (t, e, n) {
    (function () {
        "use strict";
        var e = { "@@functional/placeholder": !0 },
            n = function (t, e) {
                switch (t) {
                    case 0:
                        return function () { return e.apply(this, arguments) };
                    case 1:
                        return function (t) { return e.apply(this, arguments) };
                    case 2:
                        return function (t, n) { return e.apply(this, arguments) };
                    case 3:
                        return function (t, n, r) { return e.apply(this, arguments) };
                    case 4:
                        return function (t, n, r, o) { return e.apply(this, arguments) };
                    case 5:
                        return function (t, n, r, o, i) { return e.apply(this, arguments) };
                    case 6:
                        return function (t, n, r, o, i, a) { return e.apply(this, arguments) };
                    case 7:
                        return function (t, n, r, o, i, a, u) { return e.apply(this, arguments) };
                    case 8:
                        return function (t, n, r, o, i, a, u, s) { return e.apply(this, arguments) };
                    case 9:
                        return function (t, n, r, o, i, a, u, s, c) { return e.apply(this, arguments) };
                    case 10:
                        return function (t, n, r, o, i, a, u, s, c, l) { return e.apply(this, arguments) };
                    default:
                        throw new Error("First argument to _arity must be a non-negative integer no greater than ten")
                }
            },
            r = function (t) { return new RegExp(t.source, (t.global ? "g" : "") + (t.ignoreCase ? "i" : "") + (t.multiline ? "m" : "") + (t.sticky ? "y" : "") + (t.unicode ? "u" : "")) },
            o = function (t) { return function () { return !t.apply(this, arguments) } },
            i = function (t, e) {
                t = t || [], e = e || [];
                var n, r = t.length,
                    o = e.length,
                    i = [];
                for (n = 0; n < r;) i[i.length] = t[n], n += 1;
                for (n = 0; n < o;) i[i.length] = e[n], n += 1;
                return i
            },
            a = function (t, e, n) {
                for (var r = 0, o = n.length; r < o;) {
                    if (t(e, n[r])) return !0;
                    r += 1
                }
                return !1
            },
            u = function (t) { return function e(n) { return 0 === arguments.length ? e : null != n && n["@@functional/placeholder"] === !0 ? e : t.apply(this, arguments) } },
            s = function (t) { return function e(n, r) { var o = arguments.length; return 0 === o ? e : 1 === o && null != n && n["@@functional/placeholder"] === !0 ? e : 1 === o ? u(function (e) { return t(n, e) }) : 2 === o && null != n && n["@@functional/placeholder"] === !0 && null != r && r["@@functional/placeholder"] === !0 ? e : 2 === o && null != n && n["@@functional/placeholder"] === !0 ? u(function (e) { return t(e, r) }) : 2 === o && null != r && r["@@functional/placeholder"] === !0 ? u(function (e) { return t(n, e) }) : t(n, r) } },
            c = function (t) { return function e(n, r, o) { var i = arguments.length; return 0 === i ? e : 1 === i && null != n && n["@@functional/placeholder"] === !0 ? e : 1 === i ? s(function (e, r) { return t(n, e, r) }) : 2 === i && null != n && n["@@functional/placeholder"] === !0 && null != r && r["@@functional/placeholder"] === !0 ? e : 2 === i && null != n && n["@@functional/placeholder"] === !0 ? s(function (e, n) { return t(e, r, n) }) : 2 === i && null != r && r["@@functional/placeholder"] === !0 ? s(function (e, r) { return t(n, e, r) }) : 2 === i ? u(function (e) { return t(n, r, e) }) : 3 === i && null != n && n["@@functional/placeholder"] === !0 && null != r && r["@@functional/placeholder"] === !0 && null != o && o["@@functional/placeholder"] === !0 ? e : 3 === i && null != n && n["@@functional/placeholder"] === !0 && null != r && r["@@functional/placeholder"] === !0 ? s(function (e, n) { return t(e, n, o) }) : 3 === i && null != n && n["@@functional/placeholder"] === !0 && null != o && o["@@functional/placeholder"] === !0 ? s(function (e, n) { return t(e, r, n) }) : 3 === i && null != r && r["@@functional/placeholder"] === !0 && null != o && o["@@functional/placeholder"] === !0 ? s(function (e, r) { return t(n, e, r) }) : 3 === i && null != n && n["@@functional/placeholder"] === !0 ? u(function (e) { return t(e, r, o) }) : 3 === i && null != r && r["@@functional/placeholder"] === !0 ? u(function (e) { return t(n, e, o) }) : 3 === i && null != o && o["@@functional/placeholder"] === !0 ? u(function (e) { return t(n, r, e) }) : t(n, r, o) } },
            l = function t(e, r, o) {
                return function () {
                    for (var i = [], a = 0, u = e, s = 0; s < r.length || a < arguments.length;) {
                        var c;
                        s < r.length && (null == r[s] || r[s]["@@functional/placeholder"] !== !0 || a >= arguments.length) ? c = r[s] : (c = arguments[a], a += 1), i[s] = c, null != c && c["@@functional/placeholder"] === !0 || (u -= 1), s += 1
                    }
                    return u <= 0 ? o.apply(this, i) : n(u, t(e, i, o))
                }
            },
            f = function (t, e) { for (var n = 0, r = e.length, o = []; n < r;) t(e[n]) && (o[o.length] = e[n]), n += 1; return o },
            p = function (t) { return { "@@transducer/value": t, "@@transducer/reduced": !0 } },
            h = function (t) { return function (e) { return f(function (t) { return "function" == typeof e[t] }, t(e)) } },
            d = function (t, e) { return Object.prototype.hasOwnProperty.call(e, t) },
            v = function (t) { return t },
            g = Array.isArray || function (t) { return null != t && t.length >= 0 && "[object Array]" === Object.prototype.toString.call(t) },
            y = Number.isInteger || function (t) { return t << 0 === t },
            m = function (t) { return "[object Number]" === Object.prototype.toString.call(t) },
            b = function (t) { return "[object String]" === Object.prototype.toString.call(t) },
            x = function (t) { return "function" == typeof t["@@transducer/step"] },
            w = function (t, e) { for (var n = 0, r = e.length, o = Array(r); n < r;) o[n] = t(e[n]), n += 1; return o },
            _ = function (t, e) { return function () { return e.call(this, t.apply(this, arguments)) } },
            C = function (t, e) { return function () { var n = this; return t.apply(n, arguments).then(function (t) { return e.call(n, t) }) } },
            E = function (t) { return '"' + t.replace(/"/g, '\\"') + '"' },
            M = function (t) { return t && t["@@transducer/reduced"] ? t : { "@@transducer/value": t, "@@transducer/reduced": !0 } },
            k = function t(e, n, r) {
                switch (arguments.length) {
                    case 1:
                        return t(e, 0, e.length);
                    case 2:
                        return t(e, n, e.length);
                    default:
                        for (var o = [], i = 0, a = Math.max(0, Math.min(e.length, r) - n); i < a;) o[i] = e[n + i], i += 1;
                        return o
                }
            },
            T = function () { var t = function (t) { return (t < 10 ? "0" : "") + t }; return "function" == typeof Date.prototype.toISOString ? function (t) { return t.toISOString() } : function (e) { return e.getUTCFullYear() + "-" + t(e.getUTCMonth() + 1) + "-" + t(e.getUTCDate()) + "T" + t(e.getUTCHours()) + ":" + t(e.getUTCMinutes()) + ":" + t(e.getUTCSeconds()) + "." + (e.getUTCMilliseconds() / 1e3).toFixed(3).slice(2, 5) + "Z" } }(),
            S = function () {
                function t(t, e) { this.xf = e, this.pred = t, this.lastValue = void 0, this.seenFirstValue = !1 }
                return t.prototype["@@transducer/init"] = function () { return this.xf["@@transducer/init"]() }, t.prototype["@@transducer/result"] = function (t) { return this.xf["@@transducer/result"](t) }, t.prototype["@@transducer/step"] = function (t, e) { var n = !1; return this.seenFirstValue ? this.pred(this.lastValue, e) && (n = !0) : this.seenFirstValue = !0, this.lastValue = e, n ? t : this.xf["@@transducer/step"](t, e) }, s(function (e, n) { return new t(e, n) })
            }(),
            N = { init: function () { return this.xf["@@transducer/init"]() }, result: function (t) { return this.xf["@@transducer/result"](t) } },
            O = function () {
                function t(t, e) { this.xf = e, this.f = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) ? this.xf["@@transducer/step"](t, e) : t }, s(function (e, n) { return new t(e, n) })
            }(),
            A = function () {
                function t(t, e) { this.xf = e, this.f = t, this.found = !1 }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.found || (t = this.xf["@@transducer/step"](t, void 0)), this.xf["@@transducer/result"](t) }, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) && (this.found = !0, t = M(this.xf["@@transducer/step"](t, e))), t }, s(function (e, n) { return new t(e, n) })
            }(),
            P = function () {
                function t(t, e) { this.xf = e, this.f = t, this.idx = -1, this.found = !1 }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.found || (t = this.xf["@@transducer/step"](t, -1)), this.xf["@@transducer/result"](t) }, t.prototype["@@transducer/step"] = function (t, e) { return this.idx += 1, this.f(e) && (this.found = !0, t = M(this.xf["@@transducer/step"](t, this.idx))), t }, s(function (e, n) { return new t(e, n) })
            }(),
            j = function () {
                function t(t, e) { this.xf = e, this.f = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.xf["@@transducer/result"](this.xf["@@transducer/step"](t, this.last)) }, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) && (this.last = e), t }, s(function (e, n) { return new t(e, n) })
            }(),
            D = function () {
                function t(t, e) { this.xf = e, this.f = t, this.idx = -1, this.lastIdx = -1 }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.xf["@@transducer/result"](this.xf["@@transducer/step"](t, this.lastIdx)) }, t.prototype["@@transducer/step"] = function (t, e) { return this.idx += 1, this.f(e) && (this.lastIdx = this.idx), t }, s(function (e, n) { return new t(e, n) })
            }(),
            I = function () {
                function t(t, e) { this.xf = e, this.f = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) { return this.xf["@@transducer/step"](t, this.f(e)) }, s(function (e, n) { return new t(e, n) })
            }(),
            R = function () {
                function t(t, e) { this.xf = e, this.n = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) { return 0 === this.n ? M(t) : (this.n -= 1, this.xf["@@transducer/step"](t, e)) }, s(function (e, n) { return new t(e, n) })
            }(),
            L = function () {
                function t(t, e) { this.xf = e, this.f = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) ? this.xf["@@transducer/step"](t, e) : M(t) }, s(function (e, n) { return new t(e, n) })
            }(),
            F = function () {
                function t(t) { this.f = t }
                return t.prototype["@@transducer/init"] = function () { throw new Error("init not implemented on XWrap") }, t.prototype["@@transducer/result"] = function (t) { return t }, t.prototype["@@transducer/step"] = function (t, e) { return this.f(t, e) },
                    function (e) { return new t(e) }
            }(),
            U = s(function (t, e) { return t + e }),
            q = c(function (t, e, n) {
                if (e >= n.length || e < -n.length) return n;
                var r = e < 0 ? n.length : 0,
                    o = r + e,
                    a = i(n);
                return a[o] = t(n[o]), a
            }),
            H = u(function (t) { return function () { return t } }),
            B = s(function (t, e) { for (var n = 0, r = e.length - (t - 1), o = new Array(r >= 0 ? r : 0); n < r;) o[n] = k(e, n, n + t), n += 1; return o }),
            W = s(function (t, e) { return i(e, [t]) }),
            z = s(function (t, e) { return t.apply(this, e) }),
            V = c(function (t, e, n) { var r = {}; for (var o in n) r[o] = n[o]; return r[t] = e, r }),
            Y = c(function t(e, n, r) {
                switch (e.length) {
                    case 0:
                        return r;
                    case 1:
                        return V(e[0], n, r);
                    default:
                        return V(e[0], t(k(e, 1), n, Object(r[e[0]])), r)
                }
            }),
            $ = s(function (t, e) { return n(t.length, function () { return t.apply(e, arguments) }) }),
            K = s(function (t, e) { return function () { return t.apply(this, arguments) && e.apply(this, arguments) } }),
            X = u(function (t) { return function (e, n) { return t(e, n) ? -1 : t(n, e) ? 1 : 0 } }),
            G = u(o),
            J = u(function (t) {
                return function () {
                    for (var e = 0; e < t.length;) {
                        if (t[e][0].apply(this, arguments)) return t[e][1].apply(this, arguments);
                        e += 1
                    }
                }
            }),
            Q = c(a),
            Z = s(function (t, e) {
                for (var n = {}, r = e.length, o = 0; o < r;) {
                    var i = t(e[o]);
                    n[i] = (d(i, n) ? n[i] : 0) + 1, o += 1
                }
                return n
            }),
            tt = s(function (t, e) { var n = {}; return n[t] = e, n }),
            et = s(function (t, e) { return 1 === t ? u(e) : n(t, l(t, [], e)) }),
            nt = U(-1),
            rt = s(function (t, e) { return null == e ? t : e }),
            ot = c(function (t, e, n) { for (var r = [], o = 0, i = e.length, a = Q(t); o < i;) a(e[o], n) || a(e[o], r) || (r[r.length] = e[o]), o += 1; return r }),
            it = s(function (t, e) { var n = {}; for (var r in e) r !== t && (n[r] = e[r]); return n }),
            at = s(function t(e, n) {
                switch (e.length) {
                    case 0:
                        return n;
                    case 1:
                        return it(e[0], n);
                    default:
                        var r = e[0],
                            o = k(e, 1);
                        return null == n[r] ? n : V(r, t(o, n[r]), n)
                }
            }),
            ut = s(function (t, e) { return t / e }),
            st = s(function (t, e) { for (var n = e.length - 1; n >= 0 && t(e[n]);) n -= 1; return k(e, 0, n + 1) }),
            ct = s(function (t, e) { return function () { return t.apply(this, arguments) || e.apply(this, arguments) } }),
            lt = u(function (t) {
                if (null != t && "function" == typeof t.empty) return t.empty();
                if (null != t && null != typeof t.constructor && "function" == typeof t.constructor.empty) return t.constructor.empty();
                switch (Object.prototype.toString.call(t)) {
                    case "[object Array]":
                        return [];
                    case "[object Object]":
                        return {};
                    case "[object String]":
                        return ""
                }
            }),
            ft = s(function t(e, n) { var r, o, i, a = {}; for (o in n) r = e[o], i = typeof r, a[o] = "function" === i ? r(n[o]) : "object" === i ? t(e[o], n[o]) : n[o]; return a }),
            pt = u(function (t) { for (var e = 0, n = t.length, r = {}; e < n;) g(t[e]) && t[e].length && (r[t[e][0]] = t[e][1]), e += 1; return r }),
            ht = s(function (t, e) { return t > e }),
            dt = s(function (t, e) { return t >= e }),
            vt = s(d),
            gt = s(function (t, e) { return t in e }),
            yt = s(function (t, e) { return t === e ? 0 !== t || 1 / t === 1 / e : t !== t && e !== e }),
            mt = u(v),
            bt = c(function (t, e, n) { return et(Math.max(t.length, e.length, n.length), function () { return t.apply(this, arguments) ? e.apply(this, arguments) : n.apply(this, arguments) }) }),
            xt = U(1),
            wt = c(function (t, e, n) { t = t < n.length && t >= 0 ? t : n.length; var r = k(n); return r.splice(t, 0, e), r }),
            _t = c(function (t, e, n) { return t = t < n.length && t >= 0 ? t : n.length, i(i(k(n, 0, t), e), k(n, t)) }),
            Ct = s(function (t, e) { return null != e && e.constructor === t || e instanceof t }),
            Et = u(function (t) { return !!g(t) || !!t && ("object" == typeof t && (!(t instanceof String) && (1 === t.nodeType ? !!t.length : 0 === t.length || t.length > 0 && (t.hasOwnProperty(0) && t.hasOwnProperty(t.length - 1))))) }),
            Mt = u(function (t) { return 0 === Object(t).length }),
            kt = u(function (t) { return null == t }),
            Tt = function () {
                var t = !{ toString: null }.propertyIsEnumerable("toString"),
                    e = ["constructor", "valueOf", "isPrototypeOf", "toString", "propertyIsEnumerable", "hasOwnProperty", "toLocaleString"],
                    n = function (t, e) {
                        for (var n = 0; n < t.length;) {
                            if (t[n] === e) return !0;
                            n += 1
                        }
                        return !1
                    };
                return u("function" == typeof Object.keys ? function (t) { return Object(t) !== t ? [] : Object.keys(t) } : function (r) {
                    if (Object(r) !== r) return [];
                    var o, i, a = [];
                    for (o in r) d(o, r) && (a[a.length] = o);
                    if (t)
                        for (i = e.length - 1; i >= 0;) o = e[i], d(o, r) && !n(a, o) && (a[a.length] = o), i -= 1;
                    return a
                })
            }(),
            St = u(function (t) { var e, n = []; for (e in t) n[n.length] = e; return n }),
            Nt = u(function (t) { return null != t && Ct(Number, t.length) ? t.length : NaN }),
            Ot = s(function (t, e) { return t < e }),
            At = s(function (t, e) { return t <= e }),
            Pt = c(function (t, e, n) { for (var r = 0, o = n.length, i = [], a = [e]; r < o;) a = t(a[0], n[r]), i[r] = a[1], r += 1; return [a[0], i] }),
            jt = c(function (t, e, n) { for (var r = n.length - 1, o = [], i = [e]; r >= 0;) i = t(i[0], n[r]), o[r] = i[1], r -= 1; return [i[0], o] }),
            Dt = s(function (t, e) { return e.match(t) || [] }),
            It = s(function (t, e) { return y(t) ? !y(e) || e < 1 ? NaN : (t % e + e) % e : NaN }),
            Rt = s(function (t, e) { return e > t ? e : t }),
            Lt = c(function (t, e, n) { return t(n) > t(e) ? n : e }),
            Ft = s(function (t, e) { for (var n = {}, r = Tt(t), o = 0; o < r.length;) n[r[o]] = t[r[o]], o += 1; for (r = Tt(e), o = 0; o < r.length;) n[r[o]] = e[r[o]], o += 1; return n }),
            Ut = s(function (t, e) { return e < t ? e : t }),
            qt = c(function (t, e, n) { return t(n) < t(e) ? n : e }),
            Ht = s(function (t, e) { return t % e }),
            Bt = s(function (t, e) { return t * e }),
            Wt = s(function (t, e) {
                switch (t) {
                    case 0:
                        return function () { return e.call(this) };
                    case 1:
                        return function (t) { return e.call(this, t) };
                    case 2:
                        return function (t, n) { return e.call(this, t, n) };
                    case 3:
                        return function (t, n, r) { return e.call(this, t, n, r) };
                    case 4:
                        return function (t, n, r, o) { return e.call(this, t, n, r, o) };
                    case 5:
                        return function (t, n, r, o, i) { return e.call(this, t, n, r, o, i) };
                    case 6:
                        return function (t, n, r, o, i, a) { return e.call(this, t, n, r, o, i, a) };
                    case 7:
                        return function (t, n, r, o, i, a, u) { return e.call(this, t, n, r, o, i, a, u) };
                    case 8:
                        return function (t, n, r, o, i, a, u, s) { return e.call(this, t, n, r, o, i, a, u, s) };
                    case 9:
                        return function (t, n, r, o, i, a, u, s, c) { return e.call(this, t, n, r, o, i, a, u, s, c) };
                    case 10:
                        return function (t, n, r, o, i, a, u, s, c, l) { return e.call(this, t, n, r, o, i, a, u, s, c, l) };
                    default:
                        throw new Error("First argument to nAry must be a non-negative integer no greater than ten")
                }
            }),
            zt = u(function (t) { return -t }),
            Vt = u(function (t) { return !t }),
            Yt = s(function (t, e) { var n = t < 0 ? e.length + t : t; return b(e) ? e.charAt(n) : e[n] }),
            $t = u(function (t) { return function () { return Yt(t, arguments) } }),
            Kt = s(function (t, e) { return e.charAt(t < 0 ? e.length + t : t) }),
            Xt = s(function (t, e) { return e.charCodeAt(t < 0 ? e.length + t : t) }),
            Gt = u(function (t) { return [t] }),
            Jt = u(function (t) { var e, n = !1; return function () { return n ? e : (n = !0, e = t.apply(this, arguments)) } }),
            Qt = function () { var t = function (e) { return { value: e, map: function (n) { return t(n(e)) } } }; return c(function (e, n, r) { return e(function (e) { return t(n(e)) })(r).value }) }(),
            Zt = s(function (t, e) { if (null != e) { for (var n = e, r = 0, o = t.length; r < o && null != n; r += 1) n = n[t[r]]; return n } }),
            te = s(function (t, e) { for (var n = {}, r = 0; r < t.length;) t[r] in e && (n[t[r]] = e[t[r]]), r += 1; return n }),
            ee = s(function (t, e) {
                for (var n = {}, r = 0, o = t.length; r < o;) {
                    var i = t[r];
                    n[i] = e[i], r += 1
                }
                return n
            }),
            ne = s(function (t, e) { var n = {}; for (var r in e) t(e[r], r, e) && (n[r] = e[r]); return n }),
            re = s(function (t, e) { return i([t], e) }),
            oe = s(function (t, e) { return e[t] }),
            ie = c(function (t, e, n) { return null != n && d(e, n) ? n[e] : t }),
            ae = c(function (t, e, n) { return t(n[e]) }),
            ue = s(function (t, e) { for (var n = t.length, r = [], o = 0; o < n;) r[o] = e[t[o]], o += 1; return r }),
            se = s(function (t, e) { if (!m(t) || !m(e)) throw new TypeError("Both arguments to range must be numbers"); for (var n = [], r = t; r < e;) n.push(r), r += 1; return n }),
            ce = c(function (t, e, n) { for (var r = n.length - 1; r >= 0;) e = t(e, n[r]), r -= 1; return e }),
            le = u(M),
            fe = c(function (t, e, n) { return i(k(n, 0, Math.min(t, n.length)), k(n, Math.min(n.length, t + e))) }),
            pe = c(function (t, e, n) { return n.replace(t, e) }),
            he = u(function (t) { return k(t).reverse() }),
            de = c(function (t, e, n) { for (var r = 0, o = n.length, i = [e]; r < o;) e = t(e, n[r]), i[r + 1] = e, r += 1; return i }),
            ve = c(function (t, e, n) { return Qt(t, H(e), n) }),
            ge = s(function (t, e) { return k(e).sort(t) }),
            ye = s(function (t, e) {
                return k(e).sort(function (e, n) {
                    var r = t(e),
                        o = t(n);
                    return r < o ? -1 : r > o ? 1 : 0
                })
            }),
            me = s(function (t, e) { return t - e }),
            be = s(function (t, e) { for (var n = e.length - 1; n >= 0 && t(e[n]);) n -= 1; return k(e, n + 1, 1 / 0) }),
            xe = s(function (t, e) { return t(e), e }),
            we = s(function (t, e) { return r(t).test(e) }),
            _e = s(function (t, e) { for (var n = Number(e), r = new Array(n), o = 0; o < n;) r[o] = t(o), o += 1; return r }),
            Ce = u(function (t) { var e = []; for (var n in t) d(n, t) && (e[e.length] = [n, t[n]]); return e }),
            Ee = u(function (t) { var e = []; for (var n in t) e[e.length] = [n, t[n]]; return e }),
            Me = function () {
                var t = "\t\n\v\f\r   ᠎             　\u2028\u2029\ufeff",
                    e = "​",
                    n = "function" == typeof String.prototype.trim;
                return u(n && !t.trim() && e.trim() ? function (t) { return t.trim() } : function (e) {
                    var n = new RegExp("^[" + t + "][" + t + "]*"),
                        r = new RegExp("[" + t + "][" + t + "]*$");
                    return e.replace(n, "").replace(r, "")
                })
            }(),
            ke = u(function (t) { return null === t ? "Null" : void 0 === t ? "Undefined" : Object.prototype.toString.call(t).slice(8, -1) }),
            Te = u(function (t) { return function () { return t(k(arguments)) } }),
            Se = u(function (t) { return Wt(1, t) }),
            Ne = s(function (t, e) { return et(t, function () { for (var n, r = 1, o = e, i = 0; r <= t && "function" == typeof o;) n = r === t ? arguments.length : i + o.length, o = o.apply(this, k(arguments, i, n)), r += 1, i = n; return o }) }),
            Oe = s(function (t, e) { for (var n = t(e), r = []; n && n.length;) r[r.length] = n[0], n = t(n[1]); return r }),
            Ae = s(function (t, e) { for (var n, r = 0, o = e.length, i = []; r < o;) n = e[r], a(t, n, i) || (i[i.length] = n), r += 1; return i }),
            Pe = c(function (t, e, n) { return q(H(e), t, n) }),
            je = u(function (t) { for (var e = Tt(t), n = e.length, r = [], o = 0; o < n;) r[o] = t[e[o]], o += 1; return r }),
            De = u(function (t) { var e, n = []; for (e in t) n[n.length] = t[e]; return n }),
            Ie = function () { var t = function (t) { return { value: t, map: function () { return this } } }; return s(function (e, n) { return e(t)(n).value }) }(),
            Re = s(function (t, e) {
                for (var n in t)
                    if (d(n, t) && !t[n](e[n])) return !1;
                return !0
            }),
            Le = s(function (t, e) { return et(t.length, function () { return e.apply(this, i([t], arguments)) }) }),
            Fe = s(function (t, e) {
                for (var n, r = 0, o = t.length, i = e.length, a = []; r < o;) {
                    for (n = 0; n < i;) a[a.length] = [t[r], e[n]], n += 1;
                    r += 1
                }
                return a
            }),
            Ue = s(function (t, e) { for (var n = [], r = 0, o = Math.min(t.length, e.length); r < o;) n[r] = [t[r], e[r]], r += 1; return n }),
            qe = s(function (t, e) { for (var n = 0, r = t.length, o = {}; n < r;) o[t[n]] = e[n], n += 1; return o }),
            He = c(function (t, e, n) { for (var r = [], o = 0, i = Math.min(e.length, n.length); o < i;) r[o] = t(e[o], n[o]), o += 1; return r }),
            Be = H(!1),
            We = H(!0),
            ze = function (t, e) { return function () { var n = arguments.length; if (0 === n) return e(); var r = arguments[n - 1]; return g(r) || "function" != typeof r[t] ? e.apply(this, arguments) : r[t].apply(r, k(arguments, 0, n - 1)) } },
            Ve = function t(e, n, o) {
                var i = function (r) {
                    for (var i = n.length, a = 0; a < i;) {
                        if (e === n[a]) return o[a];
                        a += 1
                    }
                    n[a + 1] = e, o[a + 1] = r;
                    for (var u in e) r[u] = t(e[u], n, o);
                    return r
                };
                switch (ke(e)) {
                    case "Object":
                        return i({});
                    case "Array":
                        return i([]);
                    case "Date":
                        return new Date(e);
                    case "RegExp":
                        return r(e);
                    default:
                        return e
                }
            },
            Ye = function (t) { return function (e) { var r = k(arguments, 1); return n(Math.max(0, e.length - r.length), function () { return e.apply(this, t(r, arguments)) }) } },
            $e = function (t, e, n) { return function () { var r = arguments.length; if (0 === r) return n(); var o = arguments[r - 1]; if (!g(o)) { var i = k(arguments, 0, r - 1); if ("function" == typeof o[t]) return o[t].apply(o, i); if (x(o)) { var a = e.apply(null, i); return a(o) } } return n.apply(this, arguments) } },
            Ke = function t(e, n, r, o) {
                var i = ke(e);
                if (i !== ke(n)) return !1;
                if ("Boolean" === i || "Number" === i || "String" === i) return "object" == typeof e ? "object" == typeof n && yt(e.valueOf(), n.valueOf()) : yt(e, n);
                if (yt(e, n)) return !0;
                if ("RegExp" === i) return e.source === n.source && e.global === n.global && e.ignoreCase === n.ignoreCase && e.multiline === n.multiline && e.sticky === n.sticky && e.unicode === n.unicode;
                if (Object(e) === e) {
                    if ("Date" === i && e.getTime() !== n.getTime()) return !1;
                    var a = Tt(e);
                    if (a.length !== Tt(n).length) return !1;
                    for (var u = r.length - 1; u >= 0;) {
                        if (r[u] === e) return o[u] === n;
                        u -= 1
                    }
                    for (r[r.length] = e, o[o.length] = n, u = a.length - 1; u >= 0;) {
                        var s = a[u];
                        if (!d(s, n) || !t(n[s], e[s], r, o)) return !1;
                        u -= 1
                    }
                    return r.pop(), o.pop(), !0
                }
                return !1
            },
            Xe = function (t, e) { return null != e && !g(e) && "function" == typeof e[t] },
            Ge = function (t) {
                return function e(n) {
                    for (var r, o, i, a = [], u = 0, s = n.length; u < s;) {
                        if (Et(n[u]))
                            for (r = t ? e(n[u]) : n[u], o = 0, i = r.length; o < i;) a[a.length] = r[o], o += 1;
                        else a[a.length] = n[u];
                        u += 1
                    }
                    return a
                }
            },
            Je = function () {
                function t(t, e, n) {
                    for (var r = 0, o = n.length; r < o;) {
                        if (e = t["@@transducer/step"](e, n[r]), e && e["@@transducer/reduced"]) { e = e["@@transducer/value"]; break }
                        r += 1
                    }
                    return t["@@transducer/result"](e)
                }

                function e(t, e, n) {
                    for (var r = n.next(); !r.done;) {
                        if (e = t["@@transducer/step"](e, r.value), e && e["@@transducer/reduced"]) { e = e["@@transducer/value"]; break }
                        r = n.next()
                    }
                    return t["@@transducer/result"](e)
                }

                function n(t, e, n) { return t["@@transducer/result"](n.reduce($(t["@@transducer/step"], t), e)) }
                var r = "undefined" != typeof Symbol ? Symbol.iterator : "@@iterator";
                return function (o, i, a) { if ("function" == typeof o && (o = F(o)), Et(a)) return t(o, i, a); if ("function" == typeof a.reduce) return n(o, i, a); if (null != a[r]) return e(o, i, a[r]()); if ("function" == typeof a.next) return e(o, i, a); throw new TypeError("reduce: list must be array or iterable") }
            }(),
            Qe = function () {
                var t = { "@@transducer/init": Array, "@@transducer/step": function (t, e) { return i(t, [e]) }, "@@transducer/result": v },
                    e = { "@@transducer/init": String, "@@transducer/step": function (t, e) { return t + e }, "@@transducer/result": v },
                    n = { "@@transducer/init": Object, "@@transducer/step": function (t, e) { return Ft(t, Et(e) ? tt(e[0], e[1]) : e) }, "@@transducer/result": v };
                return function (r) { if (x(r)) return r; if (Et(r)) return t; if ("string" == typeof r) return e; if ("object" == typeof r) return n; throw new Error("Cannot create transformer for " + r) }
            }(),
            Ze = function () {
                function t(t, e) { this.xf = e, this.f = t, this.all = !0 }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.all && (t = this.xf["@@transducer/step"](t, !0)), this.xf["@@transducer/result"](t) }, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) || (this.all = !1, t = M(this.xf["@@transducer/step"](t, !1))), t }, s(function (e, n) { return new t(e, n) })
            }(),
            tn = function () {
                function t(t, e) { this.xf = e, this.f = t, this.any = !1 }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) { return this.any || (t = this.xf["@@transducer/step"](t, !1)), this.xf["@@transducer/result"](t) }, t.prototype["@@transducer/step"] = function (t, e) { return this.f(e) && (this.any = !0, t = M(this.xf["@@transducer/step"](t, !0))), t }, s(function (e, n) { return new t(e, n) })
            }(),
            en = function () {
                function t(t, e) { this.xf = e, this.n = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) { return this.n > 0 ? (this.n -= 1, t) : this.xf["@@transducer/step"](t, e) }, s(function (e, n) { return new t(e, n) })
            }(),
            nn = function () {
                function t(t, e) { this.xf = e, this.f = t }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = N.result, t.prototype["@@transducer/step"] = function (t, e) {
                    if (this.f) {
                        if (this.f(e)) return t;
                        this.f = null
                    }
                    return this.xf["@@transducer/step"](t, e)
                }, s(function (e, n) { return new t(e, n) })
            }(),
            rn = function () {
                function t(t, e) {
                    this.xf = e, this.f = t, this.inputs = {};
                }
                return t.prototype["@@transducer/init"] = N.init, t.prototype["@@transducer/result"] = function (t) {
                    var e;
                    for (e in this.inputs)
                        if (d(e, this.inputs) && (t = this.xf["@@transducer/step"](t, this.inputs[e]), t["@@transducer/reduced"])) { t = t["@@transducer/value"]; break }
                    return this.xf["@@transducer/result"](t)
                }, t.prototype["@@transducer/step"] = function (t, e) { var n = this.f(e); return this.inputs[n] = this.inputs[n] || [n, []], this.inputs[n][1] = W(e, this.inputs[n][1]), t }, s(function (e, n) { return new t(e, n) })
            }(),
            on = u(function (t) {
                return et(t.length, function () {
                    var e = 0,
                        n = arguments[0],
                        r = arguments[arguments.length - 1],
                        o = k(arguments);
                    return o[0] = function () { var t = n.apply(this, i(arguments, [e, r])); return e += 1, t }, t.apply(this, o)
                })
            }),
            an = s($e("all", Ze, function (t, e) {
                for (var n = 0; n < e.length;) {
                    if (!t(e[n])) return !1;
                    n += 1
                }
                return !0
            })),
            un = s(function (t, e) { return Xe("and", t) ? t.and(e) : t && e }),
            sn = s($e("any", tn, function (t, e) {
                for (var n = 0; n < e.length;) {
                    if (t(e[n])) return !0;
                    n += 1
                }
                return !1
            })),
            cn = u(function (t) { return Wt(2, t) }),
            ln = u(function (t) { return Ve(t, [], []) }),
            fn = s(function (t, e) { if (g(e)) return i(t, e); if (Xe("concat", t)) return t.concat(e); throw new TypeError("can't concat " + typeof t) }),
            pn = u(function (t) { return et(t.length, t) }),
            hn = s($e("dropWhile", nn, function (t, e) { for (var n = 0, r = e.length; n < r && t(e[n]);) n += 1; return k(e, n) })),
            dn = s(function (t, e) { return Xe("equals", t) ? t.equals(e) : Xe("equals", e) ? e.equals(t) : Ke(t, e, [], []) }),
            vn = s($e("filter", O, f)),
            gn = s($e("find", A, function (t, e) {
                for (var n = 0, r = e.length; n < r;) {
                    if (t(e[n])) return e[n];
                    n += 1
                }
            })),
            yn = s($e("findIndex", P, function (t, e) {
                for (var n = 0, r = e.length; n < r;) {
                    if (t(e[n])) return n;
                    n += 1
                }
                return -1
            })),
            mn = s($e("findLast", j, function (t, e) {
                for (var n = e.length - 1; n >= 0;) {
                    if (t(e[n])) return e[n];
                    n -= 1
                }
            })),
            bn = s($e("findLastIndex", D, function (t, e) {
                for (var n = e.length - 1; n >= 0;) {
                    if (t(e[n])) return n;
                    n -= 1
                }
                return -1
            })),
            xn = u(Ge(!0)),
            wn = u(function (t) { return pn(function (e, n) { var r = k(arguments); return r[0] = n, r[1] = e, t.apply(this, r) }) }),
            _n = s(ze("forEach", function (t, e) { for (var n = e.length, r = 0; r < n;) t(e[r]), r += 1; return e })),
            Cn = u(h(Tt)),
            En = u(h(St)),
            Mn = s($e("groupBy", rn, function (t, e) { return Je(function (e, n) { var r = t(n); return e[r] = W(n, e[r] || (e[r] = [])), e }, {}, e) })),
            kn = Yt(0),
            Tn = c(function (t, e, n) { for (var r = [], o = 0; o < e.length;) a(t, e[o], n) && (r[r.length] = e[o]), o += 1; return Ae(t, r) }),
            Sn = s(ze("intersperse", function (t, e) { for (var n = [], r = 0, o = e.length; r < o;) r === o - 1 ? n.push(e[r]) : n.push(e[r], t), r += 1; return n })),
            Nn = c(function (t, e, n) { return x(t) ? Je(e(t), t["@@transducer/init"](), n) : Je(e(Qe(t)), t, n) }),
            On = u(function (t) {
                for (var e = Tt(t), n = e.length, r = 0, o = {}; r < n;) {
                    var i = e[r],
                        a = t[i],
                        u = d(a, o) ? o[a] : o[a] = [];
                    u[u.length] = i, r += 1
                }
                return o
            }),
            An = u(function (t) {
                for (var e = Tt(t), n = e.length, r = 0, o = {}; r < n;) {
                    var i = e[r];
                    o[t[i]] = i, r += 1
                }
                return o
            }),
            Pn = Yt(-1),
            jn = s(function (t, e) {
                if (Xe("lastIndexOf", e)) return e.lastIndexOf(t);
                for (var n = e.length - 1; n >= 0;) {
                    if (dn(e[n], t)) return n;
                    n -= 1
                }
                return -1
            }),
            Dn = s($e("map", I, w)),
            In = s(function (t, e) { return Je(function (n, r) { return n[r] = t(e[r]), n }, {}, Tt(e)) }),
            Rn = s(function (t, e) { return Je(function (n, r) { return n[r] = t(e[r], r, e), n }, {}, Tt(e)) }),
            Ln = s(o($e("any", tn, sn))),
            Fn = s(function (t, e) { return Xe("or", t) ? t.or(e) : t || e }),
            Un = pn(Ye(i)),
            qn = pn(Ye(wn(i))),
            Hn = s(function (t, e) {
                return Je(function (e, n) { var r = e[t(n) ? 0 : 1]; return r[r.length] = n, e }, [
                    [],
                    []
                ], e)
            }),
            Bn = c(function (t, e, n) { return dn(Zt(t, n), e) }),
            Wn = s(function (t, e) { return Dn(oe(t), e) }),
            zn = c(function (t, e, n) { return ae(dn(e), t, n) }),
            Vn = c(function (t, e, n) { return ae(Ct(t), e, n) }),
            Yn = c(Je),
            $n = s(function (t, e) { return vn(o(t), e) }),
            Kn = s(function (t, e) { return _e(H(t), e) }),
            Xn = c(ze("slice", function (t, e, n) { return Array.prototype.slice.call(n, t, e) })),
            Gn = s(function (t, e) { if (t <= 0) throw new Error("First argument to splitEvery must be a positive integer"); for (var n = [], r = 0; r < e.length;) n.push(Xn(r, r += t, e)); return n }),
            Jn = Yn(U, 0),
            Qn = ze("tail", Xn(1, 1 / 0)),
            Zn = s($e("take", R, function (t, e) { return Xn(0, t < 0 ? 1 / 0 : t, e) })),
            tr = s($e("takeWhile", L, function (t, e) { for (var n = 0, r = e.length; n < r && t(e[n]);) n += 1; return k(e, 0, n) })),
            er = et(4, function (t, e, n, r) { return Je(t("function" == typeof e ? F(e) : e), n, r) }),
            nr = c(function (t, e, n) { return Ae(t, i(e, n)) }),
            rr = Ae(dn),
            or = u(Ge(!1)),
            ir = pn(function (t) {
                var e = k(arguments, 1),
                    r = e.length;
                return pn(n(r, function () { for (var n = [], o = 0; o < r;) n[o] = e[o](arguments[o]), o += 1; return t.apply(this, n.concat(k(arguments, r))) }))
            }),
            ar = s(function (t, e) { return Re(In(dn, t), e) }),
            ur = function () { var t = function (t) { return { "@@transducer/init": N.init, "@@transducer/result": function (e) { return t["@@transducer/result"](e) }, "@@transducer/step": function (e, n) { var r = t["@@transducer/step"](e, n); return r["@@transducer/reduced"] ? p(r) : r } } }; return function (e) { var n = t(e); return { "@@transducer/init": N.init, "@@transducer/result": function (t) { return n["@@transducer/result"](t) }, "@@transducer/step": function (t, e) { return Et(e) ? Je(n, t, e) : Je(n, t, [e]) } } } }(),
            sr = function (t, e, n) {
                for (var r = n; r < t.length;) {
                    if (dn(t[r], e)) return r;
                    r += 1
                }
                return -1
            },
            cr = function (t) { return function (e) { var r = function () { var n = arguments; return t(function (t) { return t.apply(null, n) }, e) }; return arguments.length > 1 ? r.apply(null, k(arguments, 1)) : n(Math.max.apply(Math, Wn("length", e)), r) } },
            lr = s(function (t, e) { return Dn(t, ur(e)) }),
            fr = u(cr(an)),
            pr = u(cr(sn)),
            hr = s(function (t, e) { return Xe("ap", t) ? t.ap(e) : Je(function (t, n) { return i(t, Dn(n, e)) }, [], t) }),
            dr = pn(function (t) { return t.apply(this, k(arguments, 1)) }),
            vr = s($e("chain", lr, function (t, e) { return or(Dn(t, e)) })),
            gr = c(function (t, e, n) {
                function r(e, n) { return hr(Dn(W, t(n)), e) }
                return Je(r, e([]), n)
            }),
            yr = s(function (t, e) {
                if (t > 10) throw new Error("Constructor with greater than ten arguments");
                return 0 === t ? function () { return new e } : pn(Wt(t, function (t, n, r, o, i, a, u, s, c, l) {
                    switch (arguments.length) {
                        case 1:
                            return new e(t);
                        case 2:
                            return new e(t, n);
                        case 3:
                            return new e(t, n, r);
                        case 4:
                            return new e(t, n, r, o);
                        case 5:
                            return new e(t, n, r, o, i);
                        case 6:
                            return new e(t, n, r, o, i, a);
                        case 7:
                            return new e(t, n, r, o, i, a, u);
                        case 8:
                            return new e(t, n, r, o, i, a, u, s);
                        case 9:
                            return new e(t, n, r, o, i, a, u, s, c);
                        case 10:
                            return new e(t, n, r, o, i, a, u, s, c, l)
                    }
                }))
            }),
            mr = et(3, function (t) {
                var e = k(arguments, 1);
                return et(Math.max.apply(Math, Wn("length", e)), function () {
                    var n = arguments,
                        r = this;
                    return t.apply(r, w(function (t) { return t.apply(r, n) }, e))
                })
            }),
            br = s($e("drop", en, function (t, e) { return Xn(Math.max(0, t), 1 / 0, e) })),
            xr = s(function (t, e) { return Zn(t < e.length ? e.length - t : 0, e) }),
            wr = s($e("dropRepeatsWith", S, function (t, e) {
                var n = [],
                    r = 1,
                    o = e.length;
                if (0 !== o)
                    for (n[0] = e[0]; r < o;) t(Pn(n), e[r]) || (n[n.length] = e[r]), r += 1;
                return n
            })),
            _r = c(function (t, e, n) { return dn(e[t], n[t]) }),
            Cr = s(function (t, e) { return Xe("indexOf", e) ? e.indexOf(t) : sr(e, t, 0) }),
            Er = Xn(0, -1),
            Mr = u(function (t) {
                for (var e = t.length, n = 0; n < e;) {
                    if (sr(t, t[n], n + 1) >= 0) return !1;
                    n += 1
                }
                return !0
            }),
            kr = s(function (t, e) { return function (n) { return function (r) { return Dn(function (t) { return e(t, r) }, n(t(r))) } } }),
            Tr = u(function (t) { return kr(Yt(t), Pe(t)) }),
            Sr = u(function (t) { return kr(oe(t), V(t)) }),
            Nr = s(function (t, e) { var n = et(t, e); return et(t, function () { return Je(hr, Dn(n, arguments[0]), k(arguments, 1)) }) }),
            Or = u(function (t) { return Jn(t) / t.length }),
            Ar = u(function (t) {
                var e = t.length;
                if (0 === e) return NaN;
                var n = 2 - e % 2,
                    r = (e - n) / 2;
                return Or(k(t).sort(function (t, e) { return t < e ? -1 : t > e ? 1 : 0 }).slice(r, r + n))
            }),
            Pr = u(function (t) { return Yn(Ft, {}, t) }),
            jr = function () { if (0 === arguments.length) throw new Error("pipe requires at least one argument"); return et(arguments[0].length, Yn(_, arguments[0], Qn(arguments))) },
            Dr = function () { if (0 === arguments.length) throw new Error("pipeP requires at least one argument"); return et(arguments[0].length, Yn(C, arguments[0], Qn(arguments))) },
            Ir = Yn(Bt, 1),
            Rr = ir(w, ee, mt),
            Lr = s(function (t, e) { return br(t >= 0 ? e.length - t : 0, e) }),
            Fr = function (t, e) { return sr(e, t, 0) >= 0 },
            Ur = function t(e, n) {
                var r = function (r) { var o = n.concat([e]); return Fr(r, o) ? "<Circular>" : t(r, o) },
                    o = function (t, e) { return w(function (e) { return E(e) + ": " + r(t[e]) }, e.slice().sort()) };
                switch (Object.prototype.toString.call(e)) {
                    case "[object Arguments]":
                        return "(function() { return arguments; }(" + w(r, e).join(", ") + "))";
                    case "[object Array]":
                        return "[" + w(r, e).concat(o(e, $n(we(/^\d+$/), Tt(e)))).join(", ") + "]";
                    case "[object Boolean]":
                        return "object" == typeof e ? "new Boolean(" + r(e.valueOf()) + ")" : e.toString();
                    case "[object Date]":
                        return "new Date(" + E(T(e)) + ")";
                    case "[object Null]":
                        return "null";
                    case "[object Number]":
                        return "object" == typeof e ? "new Number(" + r(e.valueOf()) + ")" : 1 / e === -(1 / 0) ? "-0" : e.toString(10);
                    case "[object String]":
                        return "object" == typeof e ? "new String(" + r(e.valueOf()) + ")" : E(e);
                    case "[object Undefined]":
                        return "undefined";
                    default:
                        return "function" == typeof e.constructor && "Object" !== e.constructor.name && "function" == typeof e.toString && "[object Object]" !== e.toString() ? e.toString() : "{" + o(e, Tt(e)).join(", ") + "}"
                }
            },
            qr = gr(mt),
            Hr = function () { if (0 === arguments.length) throw new Error("compose requires at least one argument"); return jr.apply(this, he(arguments)) },
            Br = function () { return 0 === arguments.length ? mt : Hr.apply(this, Dn(vr, arguments)) },
            Wr = function () { if (0 === arguments.length) throw new Error("composeP requires at least one argument"); return Dr.apply(this, he(arguments)) },
            zr = u(function (t) { return yr(t.length, t) }),
            Vr = s(Fr),
            Yr = s(function (t, e) { for (var n = [], r = 0, o = t.length; r < o;) Fr(t[r], e) || Fr(t[r], n) || (n[n.length] = t[r]), r += 1; return n }),
            $r = u($e("dropRepeats", S(dn), wr(dn))),
            Kr = s(function (t, e) { return rr(f(wn(Fr)(t), e)) }),
            Xr = u(function (t) { return Nr(t.length, t) }),
            Gr = s(function (t, e) { var n = {}; for (var r in e) Fr(r, t) || (n[r] = e[r]); return n }),
            Jr = function () { return Br.apply(this, he(arguments)) },
            Qr = u(function (t) { return Ur(t, []) }),
            Zr = s(Hr(rr, i)),
            to = s(function (t, e) { for (var n, r, o = 0, i = [], a = []; o < e.length;) r = e[o], n = t(r), Fr(n, i) || (a.push(r), i.push(n)), o += 1; return a }),
            eo = s(function (t, e) { return et(t + 1, function () { var n = arguments[t]; if (null != n && Ct(Function, n[e])) return n[e].apply(n, k(arguments, 0, t)); throw new TypeError(Qr(n) + ' does not have a method named "' + e + '"') }) }),
            no = eo(1, "join"),
            ro = u(function (t) { var e = {}; return function () { var n = Qr(arguments); return d(n, e) || (e[n] = t.apply(this, arguments)), e[n] } }),
            oo = eo(1, "split"),
            io = eo(0, "toLowerCase"),
            ao = eo(0, "toUpperCase"),
            uo = { F: Be, T: We, __: e, add: U, addIndex: on, adjust: q, all: an, allPass: fr, always: H, and: un, any: sn, anyPass: pr, ap: hr, aperture: B, append: W, apply: z, assoc: V, assocPath: Y, binary: cn, bind: $, both: K, call: dr, chain: vr, clone: ln, commute: qr, commuteMap: gr, comparator: X, complement: G, compose: Hr, composeK: Br, composeP: Wr, concat: fn, cond: J, construct: zr, constructN: yr, contains: Vr, containsWith: Q, converge: mr, countBy: Z, createMapEntry: tt, curry: pn, curryN: et, dec: nt, defaultTo: rt, difference: Yr, differenceWith: ot, dissoc: it, dissocPath: at, divide: ut, drop: br, dropLast: xr, dropLastWhile: st, dropRepeats: $r, dropRepeatsWith: wr, dropWhile: hn, either: ct, empty: lt, eqProps: _r, equals: dn, evolve: ft, filter: vn, find: gn, findIndex: yn, findLast: mn, findLastIndex: bn, flatten: xn, flip: wn, forEach: _n, fromPairs: pt, functions: Cn, functionsIn: En, groupBy: Mn, gt: ht, gte: dt, has: vt, hasIn: gt, head: kn, identical: yt, identity: mt, ifElse: bt, inc: xt, indexOf: Cr, init: Er, insert: wt, insertAll: _t, intersection: Kr, intersectionWith: Tn, intersperse: Sn, into: Nn, invert: On, invertObj: An, invoker: eo, is: Ct, isArrayLike: Et, isEmpty: Mt, isNil: kt, isSet: Mr, join: no, keys: Tt, keysIn: St, last: Pn, lastIndexOf: jn, length: Nt, lens: kr, lensIndex: Tr, lensProp: Sr, lift: Xr, liftN: Nr, lt: Ot, lte: At, map: Dn, mapAccum: Pt, mapAccumRight: jt, mapObj: In, mapObjIndexed: Rn, match: Dt, mathMod: It, max: Rt, maxBy: Lt, mean: Or, median: Ar, memoize: ro, merge: Ft, mergeAll: Pr, min: Ut, minBy: qt, modulo: Ht, multiply: Bt, nAry: Wt, negate: zt, none: Ln, not: Vt, nth: Yt, nthArg: $t, nthChar: Kt, nthCharCode: Xt, of: Gt, omit: Gr, once: Jt, or: Fn, over: Qt, partial: Un, partialRight: qn, partition: Hn, path: Zt, pathEq: Bn, pick: te, pickAll: ee, pickBy: ne, pipe: jr, pipeK: Jr, pipeP: Dr, pluck: Wn, prepend: re, product: Ir, project: Rr, prop: oe, propEq: zn, propIs: Vn, propOr: ie, propSatisfies: ae, props: ue, range: se, reduce: Yn, reduceRight: ce, reduced: le, reject: $n, remove: fe, repeat: Kn, replace: pe, reverse: he, scan: de, set: ve, slice: Xn, sort: ge, sortBy: ye, split: oo, splitEvery: Gn, subtract: me, sum: Jn, tail: Qn, take: Zn, takeLast: Lr, takeLastWhile: be, takeWhile: tr, tap: xe, test: we, times: _e, toLower: io, toPairs: Ce, toPairsIn: Ee, toString: Qr, toUpper: ao, transduce: er, trim: Me, type: ke, unapply: Te, unary: Se, uncurryN: Ne, unfold: Oe, union: Zr, unionWith: nr, uniq: rr, uniqBy: to, uniqWith: Ae, unnest: or, update: Pe, useWith: ir, values: je, valuesIn: De, view: Ie, where: Re, whereEq: ar, wrap: Le, xprod: Fe, zip: Ue, zipObj: qe, zipWith: He };
        t.exports = uo
    }).call(this)
},
function (t, e, n) {
    "use strict";

    function r(t) {
        if (g) {
            var e = t.node,
                n = t.children;
            if (n.length)
                for (var r = 0; r < n.length; r++) y(e, n[r], null);
            else null != t.html ? f(e, t.html) : null != t.text && h(e, t.text)
        }
    }

    function o(t, e) { t.parentNode.replaceChild(e.node, t), r(e) }

    function i(t, e) { g ? t.children.push(e) : t.node.appendChild(e.node) }

    function a(t, e) { g ? t.html = e : f(t.node, e) }

    function u(t, e) { g ? t.text = e : h(t.node, e) }

    function s() { return this.node.nodeName }

    function c(t) { return { node: t, children: [], html: null, text: null, toString: s } }
    var l = n(47),
        f = n(38),
        p = n(55),
        h = n(108),
        d = 1,
        v = 11,
        g = "undefined" != typeof document && "number" == typeof document.documentMode || "undefined" != typeof navigator && "string" == typeof navigator.userAgent && /\bEdge\/\d/.test(navigator.userAgent),
        y = p(function (t, e, n) { e.node.nodeType === v || e.node.nodeType === d && "object" === e.node.nodeName.toLowerCase() && (null == e.node.namespaceURI || e.node.namespaceURI === l.html) ? (r(e), t.insertBefore(e.node, n)) : (t.insertBefore(e.node, n), r(e)) });
    c.insertTreeBefore = y, c.replaceChildWithTree = o, c.queueChild = i, c.queueHTML = a, c.queueText = u, t.exports = c
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return (t & e) === e }
    var o = n(3),
        i = (n(1), {
            MUST_USE_PROPERTY: 1,
            HAS_BOOLEAN_VALUE: 4,
            HAS_NUMERIC_VALUE: 8,
            HAS_POSITIVE_NUMERIC_VALUE: 24,
            HAS_OVERLOADED_BOOLEAN_VALUE: 32,
            injectDOMPropertyConfig: function (t) {
                var e = i,
                    n = t.Properties || {},
                    a = t.DOMAttributeNamespaces || {},
                    s = t.DOMAttributeNames || {},
                    c = t.DOMPropertyNames || {},
                    l = t.DOMMutationMethods || {};
                t.isCustomAttribute && u._isCustomAttributeFunctions.push(t.isCustomAttribute);
                for (var f in n) {
                    u.properties.hasOwnProperty(f) ? o("48", f) : void 0;
                    var p = f.toLowerCase(),
                        h = n[f],
                        d = { attributeName: p, attributeNamespace: null, propertyName: f, mutationMethod: null, mustUseProperty: r(h, e.MUST_USE_PROPERTY), hasBooleanValue: r(h, e.HAS_BOOLEAN_VALUE), hasNumericValue: r(h, e.HAS_NUMERIC_VALUE), hasPositiveNumericValue: r(h, e.HAS_POSITIVE_NUMERIC_VALUE), hasOverloadedBooleanValue: r(h, e.HAS_OVERLOADED_BOOLEAN_VALUE) };
                    if (d.hasBooleanValue + d.hasNumericValue + d.hasOverloadedBooleanValue <= 1 ? void 0 : o("50", f), s.hasOwnProperty(f)) {
                        var v = s[f];
                        d.attributeName = v
                    }
                    a.hasOwnProperty(f) && (d.attributeNamespace = a[f]), c.hasOwnProperty(f) && (d.propertyName = c[f]), l.hasOwnProperty(f) && (d.mutationMethod = l[f]), u.properties[f] = d
                }
            }
        }),
        a = ":A-Z_a-z\\u00C0-\\u00D6\\u00D8-\\u00F6\\u00F8-\\u02FF\\u0370-\\u037D\\u037F-\\u1FFF\\u200C-\\u200D\\u2070-\\u218F\\u2C00-\\u2FEF\\u3001-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFFD",
        u = { ID_ATTRIBUTE_NAME: "data-reactid", ROOT_ATTRIBUTE_NAME: "data-reactroot", ATTRIBUTE_NAME_START_CHAR: a, ATTRIBUTE_NAME_CHAR: a + "\\-.0-9\\u00B7\\u0300-\\u036F\\u203F-\\u2040", properties: {}, getPossibleStandardName: null, _isCustomAttributeFunctions: [], isCustomAttribute: function (t) { for (var e = 0; e < u._isCustomAttributeFunctions.length; e++) { var n = u._isCustomAttributeFunctions[e]; if (n(t)) return !0 } return !1 }, injection: i };
    t.exports = u
},
function (t, e, n) {
    "use strict";

    function r() { o.attachRefs(this, this._currentElement) }
    var o = n(248),
        i = (n(8), n(2), {
            mountComponent: function (t, e, n, o, i, a) { var u = t.mountComponent(e, n, o, i, a); return t._currentElement && null != t._currentElement.ref && e.getReactMountReady().enqueue(r, t), u },
            getHostNode: function (t) { return t.getHostNode() },
            unmountComponent: function (t, e) { o.detachRefs(t, t._currentElement), t.unmountComponent(e) },
            receiveComponent: function (t, e, n, i) {
                var a = t._currentElement;
                if (e !== a || i !== t._context) {
                    var u = o.shouldUpdateRefs(a, e);
                    u && o.detachRefs(t, a), t.receiveComponent(e, n, i), u && t._currentElement && null != t._currentElement.ref && n.getReactMountReady().enqueue(r, t)
                }
            },
            performUpdateIfNecessary: function (t, e, n) { t._updateBatchNumber === n && t.performUpdateIfNecessary(e) }
        });
    t.exports = i
},
function (t, e, n) {
    "use strict";
    var r = n(4),
        o = n(280),
        i = n(62),
        a = n(285),
        u = n(281),
        s = n(282),
        c = n(21),
        l = n(283),
        f = n(286),
        p = n(287),
        h = (n(2), c.createElement),
        d = c.createFactory,
        v = c.cloneElement,
        g = r,
        y = { Children: { map: o.map, forEach: o.forEach, count: o.count, toArray: o.toArray, only: p }, Component: i, PureComponent: a, createElement: h, cloneElement: v, isValidElement: c.isValidElement, PropTypes: l, createClass: u.createClass, createFactory: d, createMixin: function (t) { return t }, DOM: s, version: f, __spread: g };
    t.exports = y
},
function (t, e, n) {
    "use strict";

    function r(t) { return void 0 !== t.ref }

    function o(t) { return void 0 !== t.key }
    var i = n(4),
        a = n(11),
        u = (n(2), n(113), Object.prototype.hasOwnProperty),
        s = n(111),
        c = { key: !0, ref: !0, __self: !0, __source: !0 },
        l = function (t, e, n, r, o, i, a) { var u = { $$typeof: s, type: t, key: e, ref: n, props: a, _owner: i }; return u };
    l.createElement = function (t, e, n) {
        var i, s = {},
            f = null,
            p = null,
            h = null,
            d = null;
        if (null != e) { r(e) && (p = e.ref), o(e) && (f = "" + e.key), h = void 0 === e.__self ? null : e.__self, d = void 0 === e.__source ? null : e.__source; for (i in e) u.call(e, i) && !c.hasOwnProperty(i) && (s[i] = e[i]) }
        var v = arguments.length - 2;
        if (1 === v) s.children = n;
        else if (v > 1) {
            for (var g = Array(v), y = 0; y < v; y++) g[y] = arguments[y + 2];
            s.children = g
        }
        if (t && t.defaultProps) { var m = t.defaultProps; for (i in m) void 0 === s[i] && (s[i] = m[i]) }
        return l(t, f, p, h, d, a.current, s)
    }, l.createFactory = function (t) { var e = l.createElement.bind(null, t); return e.type = t, e }, l.cloneAndReplaceKey = function (t, e) { var n = l(t.type, e, t.ref, t._self, t._source, t._owner, t.props); return n }, l.cloneElement = function (t, e, n) {
        var s, f = i({}, t.props),
            p = t.key,
            h = t.ref,
            d = t._self,
            v = t._source,
            g = t._owner;
        if (null != e) {
            r(e) && (h = e.ref, g = a.current), o(e) && (p = "" + e.key);
            var y;
            t.type && t.type.defaultProps && (y = t.type.defaultProps);
            for (s in e) u.call(e, s) && !c.hasOwnProperty(s) && (void 0 === e[s] && void 0 !== y ? f[s] = y[s] : f[s] = e[s])
        }
        var m = arguments.length - 2;
        if (1 === m) f.children = n;
        else if (m > 1) {
            for (var b = Array(m), x = 0; x < m; x++) b[x] = arguments[x + 2];
            f.children = b
        }
        return l(t.type, p, h, d, v, g, f)
    }, l.isValidElement = function (t) { return "object" == typeof t && null !== t && t.$$typeof === s }, t.exports = l
},
    3,
function (t, e, n) {
    "use strict";
    t.exports = n(20)
},
function (t, e, n) {
    "use strict";
    var r = {};
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t) { return "button" === t || "input" === t || "select" === t || "textarea" === t }

    function o(t, e, n) {
        switch (t) {
            case "onClick":
            case "onClickCapture":
            case "onDoubleClick":
            case "onDoubleClickCapture":
            case "onMouseDown":
            case "onMouseDownCapture":
            case "onMouseMove":
            case "onMouseMoveCapture":
            case "onMouseUp":
            case "onMouseUpCapture":
                return !(!n.disabled || !r(e));
            default:
                return !1
        }
    }
    var i = n(3),
        a = n(48),
        u = n(49),
        s = n(53),
        c = n(102),
        l = n(103),
        f = (n(1), {}),
        p = null,
        h = function (t, e) { t && (u.executeDispatchesInOrder(t, e), t.isPersistent() || t.constructor.release(t)) },
        d = function (t) { return h(t, !0) },
        v = function (t) { return h(t, !1) },
        g = function (t) { return "." + t._rootNodeID },
        y = {
            injection: { injectEventPluginOrder: a.injectEventPluginOrder, injectEventPluginsByName: a.injectEventPluginsByName },
            putListener: function (t, e, n) {
                "function" != typeof n ? i("94", e, typeof n) : void 0;
                var r = g(t),
                    o = f[e] || (f[e] = {});
                o[r] = n;
                var u = a.registrationNameModules[e];
                u && u.didPutListener && u.didPutListener(t, e, n)
            },
            getListener: function (t, e) { var n = f[e]; if (o(e, t._currentElement.type, t._currentElement.props)) return null; var r = g(t); return n && n[r] },
            deleteListener: function (t, e) {
                var n = a.registrationNameModules[e];
                n && n.willDeleteListener && n.willDeleteListener(t, e);
                var r = f[e];
                if (r) {
                    var o = g(t);
                    delete r[o]
                }
            },
            deleteAllListeners: function (t) {
                var e = g(t);
                for (var n in f)
                    if (f.hasOwnProperty(n) && f[n][e]) {
                        var r = a.registrationNameModules[n];
                        r && r.willDeleteListener && r.willDeleteListener(t, n), delete f[n][e]
                    }
            },
            extractEvents: function (t, e, n, r) {
                for (var o, i = a.plugins, u = 0; u < i.length; u++) {
                    var s = i[u];
                    if (s) {
                        var l = s.extractEvents(t, e, n, r);
                        l && (o = c(o, l))
                    }
                }
                return o
            },
            enqueueEvents: function (t) { t && (p = c(p, t)) },
            processEventQueue: function (t) {
                var e = p;
                p = null, t ? l(e, d) : l(e, v), p ? i("95") : void 0, s.rethrowCaughtError()
            },
            __purge: function () { f = {} },
            __getListenerBank: function () { return f }
        };
    t.exports = y
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { var r = e.dispatchConfig.phasedRegistrationNames[n]; return y(t, r) }

    function o(t, e, n) {
        var o = r(t, n, e);
        o && (n._dispatchListeners = v(n._dispatchListeners, o), n._dispatchInstances = v(n._dispatchInstances, t))
    }

    function i(t) { t && t.dispatchConfig.phasedRegistrationNames && d.traverseTwoPhase(t._targetInst, o, t) }

    function a(t) {
        if (t && t.dispatchConfig.phasedRegistrationNames) {
            var e = t._targetInst,
                n = e ? d.getParentInstance(e) : null;
            d.traverseTwoPhase(n, o, t)
        }
    }

    function u(t, e, n) {
        if (n && n.dispatchConfig.registrationName) {
            var r = n.dispatchConfig.registrationName,
                o = y(t, r);
            o && (n._dispatchListeners = v(n._dispatchListeners, o), n._dispatchInstances = v(n._dispatchInstances, t))
        }
    }

    function s(t) { t && t.dispatchConfig.registrationName && u(t._targetInst, null, t) }

    function c(t) { g(t, i) }

    function l(t) { g(t, a) }

    function f(t, e, n, r) { d.traverseEnterLeave(n, r, u, t, e) }

    function p(t) { g(t, s) }
    var h = n(25),
        d = n(49),
        v = n(102),
        g = n(103),
        y = (n(2), h.getListener),
        m = { accumulateTwoPhaseDispatches: c, accumulateTwoPhaseDispatchesSkipTarget: l, accumulateDirectDispatches: p, accumulateEnterLeaveDispatches: f };
    t.exports = m
},
function (t, e) {
    "use strict";
    var n = { remove: function (t) { t._reactInternalInstance = void 0 }, get: function (t) { return t._reactInternalInstance }, has: function (t) { return void 0 !== t._reactInternalInstance }, set: function (t, e) { t._reactInternalInstance = e } };
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = n(58),
        a = { view: function (t) { if (t.view) return t.view; var e = i(t); if (e.window === e) return e; var n = e.ownerDocument; return n ? n.defaultView || n.parentWindow : window }, detail: function (t) { return t.detail || 0 } };
    o.augmentClass(r, a), t.exports = r
},
function (t, e, n) {
    function r(t, e) {
        for (var n = t.length; n--;)
            if (o(t[n][0], e)) return n;
        return -1
    }
    var o = n(197);
    t.exports = r
},
function (t, e) {
    function n(t) { var e = typeof t; return "number" == e || "boolean" == e || "string" == e && "__proto__" !== t || null == t }
    t.exports = n
},
function (t, e, n) {
    var r = n(40),
        o = r(Object, "create");
    t.exports = o
},
function (t, e, n) {
    (function (t, r) {
        var o = n(168),
            i = { function: !0, object: !0 },
            a = i[typeof e] && e && !e.nodeType ? e : null,
            u = i[typeof t] && t && !t.nodeType ? t : null,
            s = o(a && u && "object" == typeof r && r),
            c = o(i[typeof self] && self),
            l = o(i[typeof window] && window),
            f = o(i[typeof this] && this),
            p = s || l !== (f && f.window) && l || c || f || Function("return this")();
        t.exports = p
    }).call(e, n(289)(t), function () { return this }())
},
function (t, e) {
    function n(t) { return "number" == typeof t && t > -1 && t % 1 == 0 && t <= r }
    var r = 9007199254740991;
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { return Object.prototype.hasOwnProperty.call(t, v) || (t[v] = h++ , f[t[v]] = {}), f[t[v]] }
    var o, i = n(4),
        a = n(48),
        u = n(240),
        s = n(101),
        c = n(273),
        l = n(59),
        f = {},
        p = !1,
        h = 0,
        d = { topAbort: "abort", topAnimationEnd: c("animationend") || "animationend", topAnimationIteration: c("animationiteration") || "animationiteration", topAnimationStart: c("animationstart") || "animationstart", topBlur: "blur", topCanPlay: "canplay", topCanPlayThrough: "canplaythrough", topChange: "change", topClick: "click", topCompositionEnd: "compositionend", topCompositionStart: "compositionstart", topCompositionUpdate: "compositionupdate", topContextMenu: "contextmenu", topCopy: "copy", topCut: "cut", topDoubleClick: "dblclick", topDrag: "drag", topDragEnd: "dragend", topDragEnter: "dragenter", topDragExit: "dragexit", topDragLeave: "dragleave", topDragOver: "dragover", topDragStart: "dragstart", topDrop: "drop", topDurationChange: "durationchange", topEmptied: "emptied", topEncrypted: "encrypted", topEnded: "ended", topError: "error", topFocus: "focus", topInput: "input", topKeyDown: "keydown", topKeyPress: "keypress", topKeyUp: "keyup", topLoadedData: "loadeddata", topLoadedMetadata: "loadedmetadata", topLoadStart: "loadstart", topMouseDown: "mousedown", topMouseMove: "mousemove", topMouseOut: "mouseout", topMouseOver: "mouseover", topMouseUp: "mouseup", topPaste: "paste", topPause: "pause", topPlay: "play", topPlaying: "playing", topProgress: "progress", topRateChange: "ratechange", topScroll: "scroll", topSeeked: "seeked", topSeeking: "seeking", topSelectionChange: "selectionchange", topStalled: "stalled", topSuspend: "suspend", topTextInput: "textInput", topTimeUpdate: "timeupdate", topTouchCancel: "touchcancel", topTouchEnd: "touchend", topTouchMove: "touchmove", topTouchStart: "touchstart", topTransitionEnd: c("transitionend") || "transitionend", topVolumeChange: "volumechange", topWaiting: "waiting", topWheel: "wheel" },
        v = "_reactListenersID" + String(Math.random()).slice(2),
        g = i({}, u, {
            ReactEventListener: null,
            injection: { injectReactEventListener: function (t) { t.setHandleTopLevel(g.handleTopLevel), g.ReactEventListener = t } },
            setEnabled: function (t) { g.ReactEventListener && g.ReactEventListener.setEnabled(t) },
            isEnabled: function () { return !(!g.ReactEventListener || !g.ReactEventListener.isEnabled()) },
            listenTo: function (t, e) {
                for (var n = e, o = r(n), i = a.registrationNameDependencies[t], u = 0; u < i.length; u++) {
                    var s = i[u];
                    o.hasOwnProperty(s) && o[s] || ("topWheel" === s ? l("wheel") ? g.ReactEventListener.trapBubbledEvent("topWheel", "wheel", n) : l("mousewheel") ? g.ReactEventListener.trapBubbledEvent("topWheel", "mousewheel", n) : g.ReactEventListener.trapBubbledEvent("topWheel", "DOMMouseScroll", n) : "topScroll" === s ? l("scroll", !0) ? g.ReactEventListener.trapCapturedEvent("topScroll", "scroll", n) : g.ReactEventListener.trapBubbledEvent("topScroll", "scroll", g.ReactEventListener.WINDOW_HANDLE) : "topFocus" === s || "topBlur" === s ? (l("focus", !0) ? (g.ReactEventListener.trapCapturedEvent("topFocus", "focus", n), g.ReactEventListener.trapCapturedEvent("topBlur", "blur", n)) : l("focusin") && (g.ReactEventListener.trapBubbledEvent("topFocus", "focusin", n), g.ReactEventListener.trapBubbledEvent("topBlur", "focusout", n)), o.topBlur = !0, o.topFocus = !0) : d.hasOwnProperty(s) && g.ReactEventListener.trapBubbledEvent(s, d[s], n), o[s] = !0)
                }
            },
            trapBubbledEvent: function (t, e, n) { return g.ReactEventListener.trapBubbledEvent(t, e, n) },
            trapCapturedEvent: function (t, e, n) { return g.ReactEventListener.trapCapturedEvent(t, e, n) },
            supportsEventPageXY: function () { if (!document.createEvent) return !1; var t = document.createEvent("MouseEvent"); return null != t && "pageX" in t },
            ensureScrollValueMonitoring: function () {
                if (void 0 === o && (o = g.supportsEventPageXY()), !o && !p) {
                    var t = s.refreshScrollValues;
                    g.ReactEventListener.monitorScrollValue(t), p = !0
                }
            }
        });
    t.exports = g
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(28),
        i = n(101),
        a = n(57),
        u = { screenX: null, screenY: null, clientX: null, clientY: null, ctrlKey: null, shiftKey: null, altKey: null, metaKey: null, getModifierState: a, button: function (t) { var e = t.button; return "which" in t ? e : 2 === e ? 2 : 4 === e ? 1 : 0 }, buttons: null, relatedTarget: function (t) { return t.relatedTarget || (t.fromElement === t.srcElement ? t.toElement : t.fromElement) }, pageX: function (t) { return "pageX" in t ? t.pageX : t.clientX + i.currentScrollLeft }, pageY: function (t) { return "pageY" in t ? t.pageY : t.clientY + i.currentScrollTop } };
    o.augmentClass(r, u), t.exports = r
},
function (t, e, n) {
    "use strict";
    var r = n(3),
        o = (n(1), {}),
        i = {
            reinitializeTransaction: function () { this.transactionWrappers = this.getTransactionWrappers(), this.wrapperInitData ? this.wrapperInitData.length = 0 : this.wrapperInitData = [], this._isInTransaction = !1 },
            _isInTransaction: !1,
            getTransactionWrappers: null,
            isInTransaction: function () { return !!this._isInTransaction },
            perform: function (t, e, n, o, i, a, u, s) { this.isInTransaction() ? r("27") : void 0; var c, l; try { this._isInTransaction = !0, c = !0, this.initializeAll(0), l = t.call(e, n, o, i, a, u, s), c = !1 } finally { try { if (c) try { this.closeAll(0) } catch (t) { } else this.closeAll(0) } finally { this._isInTransaction = !1 } } return l },
            initializeAll: function (t) { for (var e = this.transactionWrappers, n = t; n < e.length; n++) { var r = e[n]; try { this.wrapperInitData[n] = o, this.wrapperInitData[n] = r.initialize ? r.initialize.call(this) : null } finally { if (this.wrapperInitData[n] === o) try { this.initializeAll(n + 1) } catch (t) { } } } },
            closeAll: function (t) {
                this.isInTransaction() ? void 0 : r("28");
                for (var e = this.transactionWrappers, n = t; n < e.length; n++) {
                    var i, a = e[n],
                        u = this.wrapperInitData[n];
                    try { i = !0, u !== o && a.close && a.close.call(this, u), i = !1 } finally { if (i) try { this.closeAll(n + 1) } catch (t) { } }
                }
                this.wrapperInitData.length = 0
            }
        };
    t.exports = i
},
function (t, e) {
    "use strict";

    function n(t) {
        var e = "" + t,
            n = o.exec(e);
        if (!n) return e;
        var r, i = "",
            a = 0,
            u = 0;
        for (a = n.index; a < e.length; a++) {
            switch (e.charCodeAt(a)) {
                case 34:
                    r = "&quot;";
                    break;
                case 38:
                    r = "&amp;";
                    break;
                case 39:
                    r = "&#x27;";
                    break;
                case 60:
                    r = "&lt;";
                    break;
                case 62:
                    r = "&gt;";
                    break;
                default:
                    continue
            }
            u !== a && (i += e.substring(u, a)), u = a + 1, i += r
        }
        return u !== a ? i + e.substring(u, a) : i
    }

    function r(t) { return "boolean" == typeof t || "number" == typeof t ? "" + t : n(t) }
    var o = /["'&<>]/;
    t.exports = r
},
function (t, e, n) {
    "use strict";
    var r, o = n(6),
        i = n(47),
        a = /^[ \r\n\t\f]/,
        u = /<(!--|link|noscript|meta|script|style)[ \r\n\t\f\/>]/,
        s = n(55),
        c = s(function (t, e) {
            if (t.namespaceURI !== i.svg || "innerHTML" in t) t.innerHTML = e;
            else { r = r || document.createElement("div"), r.innerHTML = "<svg>" + e + "</svg>"; for (var n = r.firstChild; n.firstChild;) t.appendChild(n.firstChild) }
        });
    if (o.canUseDOM) {
        var l = document.createElement("div");
        l.innerHTML = " ", "" === l.innerHTML && (c = function (t, e) {
            if (t.parentNode && t.parentNode.replaceChild(t, t), a.test(e) || "<" === e[0] && u.test(e)) {
                t.innerHTML = String.fromCharCode(65279) + e;
                var n = t.firstChild;
                1 === n.data.length ? t.removeChild(n) : n.deleteData(0, 1)
            } else t.innerHTML = e
        }), l = null
    }
    t.exports = c
},
function (t, e) {
    "use strict";

    function n(t, e) { return t === e ? 0 !== t || 0 !== e || 1 / t === 1 / e : t !== t && e !== e }

    function r(t, e) {
        if (n(t, e)) return !0;
        if ("object" != typeof t || null === t || "object" != typeof e || null === e) return !1;
        var r = Object.keys(t),
            i = Object.keys(e);
        if (r.length !== i.length) return !1;
        for (var a = 0; a < r.length; a++)
            if (!o.call(e, r[a]) || !n(t[r[a]], e[r[a]])) return !1;
        return !0
    }
    var o = Object.prototype.hasOwnProperty;
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { var n = null == t ? void 0 : t[e]; return o(n) ? n : void 0 }
    var o = n(201);
    t.exports = r
},
function (t, e) {
    function n(t) {
        var e = !1;
        if (null != t && "function" != typeof t.toString) try { e = !!(t + "") } catch (t) { }
        return e
    }
    t.exports = n
},
function (t, e, n) {
    function r(t, e) { return "number" == typeof t || !o(t) && (a.test(t) || !i.test(t) || null != e && t in Object(e)) }
    var o = n(12),
        i = /\.|\[(?:[^[\]]*|(["'])(?:(?!\1)[^\\]|\\.)*?\1)\]/,
        a = /^\w*$/;
    t.exports = r
},
function (t, e) {
    function n(t) { var e = typeof t; return !!t && ("object" == e || "function" == e) }
    t.exports = n
},
function (t, e, n) {
    function r(t) {
        var e = c(t);
        if (!e && !u(t)) return i(t);
        var n = a(t),
            r = !!n,
            l = n || [],
            f = l.length;
        for (var p in t) !o(t, p) || r && ("length" == p || s(p, f)) || e && "constructor" == p || l.push(p);
        return l
    }
    var o = n(77),
        i = n(161),
        a = n(180),
        u = n(85),
        s = n(82),
        c = n(181);
    t.exports = r
},
function (t, e, n) {
    "use strict";
    t.exports = n(225)
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return Array.isArray(e) && (e = e[1]), e ? e.nextSibling : t.firstChild }

    function o(t, e, n) { l.insertTreeBefore(t, e, n) }

    function i(t, e, n) { Array.isArray(e) ? u(t, e[0], e[1], n) : v(t, e, n) }

    function a(t, e) {
        if (Array.isArray(e)) {
            var n = e[1];
            e = e[0], s(t, e, n), t.removeChild(n)
        }
        t.removeChild(e)
    }

    function u(t, e, n, r) {
        for (var o = e; ;) {
            var i = o.nextSibling;
            if (v(t, o, r), o === n) break;
            o = i
        }
    }

    function s(t, e, n) {
        for (; ;) {
            var r = e.nextSibling;
            if (r === n) break;
            t.removeChild(r)
        }
    }

    function c(t, e, n) {
        var r = t.parentNode,
            o = t.nextSibling;
        o === e ? n && v(r, document.createTextNode(n), o) : n ? (d(o, n), s(r, o, e)) : s(r, t, e)
    }
    var l = n(17),
        f = n(217),
        p = (n(5), n(8), n(55)),
        h = n(38),
        d = n(108),
        v = p(function (t, e, n) { t.insertBefore(e, n) }),
        g = f.dangerouslyReplaceNodeWithMarkup,
        y = {
            dangerouslyReplaceNodeWithMarkup: g,
            replaceDelimitedText: c,
            processUpdates: function (t, e) {
                for (var n = 0; n < e.length; n++) {
                    var u = e[n];
                    switch (u.type) {
                        case "INSERT_MARKUP":
                            o(t, u.content, r(t, u.afterNode));
                            break;
                        case "MOVE_EXISTING":
                            i(t, u.fromNode, r(t, u.afterNode));
                            break;
                        case "SET_MARKUP":
                            h(t, u.content);
                            break;
                        case "TEXT_CONTENT":
                            d(t, u.content);
                            break;
                        case "REMOVE_NODE":
                            a(t, u.fromNode)
                    }
                }
            }
        };
    t.exports = y
},
function (t, e) {
    "use strict";
    var n = { html: "http://www.w3.org/1999/xhtml", mathml: "http://www.w3.org/1998/Math/MathML", svg: "http://www.w3.org/2000/svg" };
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r() {
        if (u)
            for (var t in s) {
                var e = s[t],
                    n = u.indexOf(t);
                if (n > -1 ? void 0 : a("96", t), !c.plugins[n]) { e.extractEvents ? void 0 : a("97", t), c.plugins[n] = e; var r = e.eventTypes; for (var i in r) o(r[i], e, i) ? void 0 : a("98", i, t) }
            }
    }

    function o(t, e, n) {
        c.eventNameDispatchConfigs.hasOwnProperty(n) ? a("99", n) : void 0, c.eventNameDispatchConfigs[n] = t;
        var r = t.phasedRegistrationNames;
        if (r) {
            for (var o in r)
                if (r.hasOwnProperty(o)) {
                    var u = r[o];
                    i(u, e, n)
                }
            return !0
        }
        return !!t.registrationName && (i(t.registrationName, e, n), !0)
    }

    function i(t, e, n) { c.registrationNameModules[t] ? a("100", t) : void 0, c.registrationNameModules[t] = e, c.registrationNameDependencies[t] = e.eventTypes[n].dependencies }
    var a = n(3),
        u = (n(1), null),
        s = {},
        c = {
            plugins: [],
            eventNameDispatchConfigs: {},
            registrationNameModules: {},
            registrationNameDependencies: {},
            possibleRegistrationNames: null,
            injectEventPluginOrder: function (t) { u ? a("101") : void 0, u = Array.prototype.slice.call(t), r() },
            injectEventPluginsByName: function (t) {
                var e = !1;
                for (var n in t)
                    if (t.hasOwnProperty(n)) {
                        var o = t[n];
                        s.hasOwnProperty(n) && s[n] === o || (s[n] ? a("102", n) : void 0, s[n] = o, e = !0)
                    }
                e && r()
            },
            getPluginModuleForEvent: function (t) {
                var e = t.dispatchConfig;
                if (e.registrationName) return c.registrationNameModules[e.registrationName] || null;
                if (void 0 !== e.phasedRegistrationNames) {
                    var n = e.phasedRegistrationNames;
                    for (var r in n)
                        if (n.hasOwnProperty(r)) { var o = c.registrationNameModules[n[r]]; if (o) return o }
                }
                return null
            },
            _resetEventPlugins: function () {
                u = null;
                for (var t in s) s.hasOwnProperty(t) && delete s[t];
                c.plugins.length = 0;
                var e = c.eventNameDispatchConfigs;
                for (var n in e) e.hasOwnProperty(n) && delete e[n];
                var r = c.registrationNameModules;
                for (var o in r) r.hasOwnProperty(o) && delete r[o]
            }
        };
    t.exports = c
},
function (t, e, n) {
    "use strict";

    function r(t) { return "topMouseUp" === t || "topTouchEnd" === t || "topTouchCancel" === t }

    function o(t) { return "topMouseMove" === t || "topTouchMove" === t }

    function i(t) { return "topMouseDown" === t || "topTouchStart" === t }

    function a(t, e, n, r) {
        var o = t.type || "unknown-event";
        t.currentTarget = y.getNodeFromInstance(r), e ? v.invokeGuardedCallbackWithCatch(o, n, t) : v.invokeGuardedCallback(o, n, t), t.currentTarget = null
    }

    function u(t, e) {
        var n = t._dispatchListeners,
            r = t._dispatchInstances;
        if (Array.isArray(n))
            for (var o = 0; o < n.length && !t.isPropagationStopped(); o++) a(t, e, n[o], r[o]);
        else n && a(t, e, n, r);
        t._dispatchListeners = null, t._dispatchInstances = null
    }

    function s(t) {
        var e = t._dispatchListeners,
            n = t._dispatchInstances;
        if (Array.isArray(e)) {
            for (var r = 0; r < e.length && !t.isPropagationStopped(); r++)
                if (e[r](t, n[r])) return n[r]
        } else if (e && e(t, n)) return n;
        return null
    }

    function c(t) { var e = s(t); return t._dispatchInstances = null, t._dispatchListeners = null, e }

    function l(t) {
        var e = t._dispatchListeners,
            n = t._dispatchInstances;
        Array.isArray(e) ? d("103") : void 0, t.currentTarget = e ? y.getNodeFromInstance(n) : null;
        var r = e ? e(t) : null;
        return t.currentTarget = null, t._dispatchListeners = null, t._dispatchInstances = null, r
    }

    function f(t) { return !!t._dispatchListeners }
    var p, h, d = n(3),
        v = n(53),
        g = (n(1), n(2), { injectComponentTree: function (t) { p = t }, injectTreeTraversal: function (t) { h = t } }),
        y = { isEndish: r, isMoveish: o, isStartish: i, executeDirectDispatch: l, executeDispatchesInOrder: u, executeDispatchesInOrderStopAtTrue: c, hasDispatches: f, getInstanceFromNode: function (t) { return p.getInstanceFromNode(t) }, getNodeFromInstance: function (t) { return p.getNodeFromInstance(t) }, isAncestor: function (t, e) { return h.isAncestor(t, e) }, getLowestCommonAncestor: function (t, e) { return h.getLowestCommonAncestor(t, e) }, getParentInstance: function (t) { return h.getParentInstance(t) }, traverseTwoPhase: function (t, e, n) { return h.traverseTwoPhase(t, e, n) }, traverseEnterLeave: function (t, e, n, r, o) { return h.traverseEnterLeave(t, e, n, r, o) }, injection: g };
    t.exports = y
},
function (t, e) {
    "use strict";

    function n(t) {
        var e = /[=:]/g,
            n = { "=": "=0", ":": "=2" },
            r = ("" + t).replace(e, function (t) { return n[t] });
        return "$" + r
    }

    function r(t) {
        var e = /(=0|=2)/g,
            n = { "=0": "=", "=2": ":" },
            r = "." === t[0] && "$" === t[1] ? t.substring(2) : t.substring(1);
        return ("" + r).replace(e, function (t) { return n[t] })
    }
    var o = { escape: n, unescape: r };
    t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t) { null != t.checkedLink && null != t.valueLink ? u("87") : void 0 }

    function o(t) { r(t), null != t.value || null != t.onChange ? u("88") : void 0 }

    function i(t) { r(t), null != t.checked || null != t.onChange ? u("89") : void 0 }

    function a(t) { if (t) { var e = t.getName(); if (e) return " Check the render method of `" + e + "`." } return "" }
    var u = n(3),
        s = n(20),
        c = n(246),
        l = (n(1), n(2), { button: !0, checkbox: !0, image: !0, hidden: !0, radio: !0, reset: !0, submit: !0 }),
        f = { value: function (t, e, n) { return !t[e] || l[t.type] || t.onChange || t.readOnly || t.disabled ? null : new Error("You provided a `value` prop to a form field without an `onChange` handler. This will render a read-only field. If the field should be mutable use `defaultValue`. Otherwise, set either `onChange` or `readOnly`.") }, checked: function (t, e, n) { return !t[e] || t.onChange || t.readOnly || t.disabled ? null : new Error("You provided a `checked` prop to a form field without an `onChange` handler. This will render a read-only field. If the field should be mutable use `defaultChecked`. Otherwise, set either `onChange` or `readOnly`.") }, onChange: s.PropTypes.func },
        p = {},
        h = {
            checkPropTypes: function (t, e, n) {
                for (var r in f) {
                    if (f.hasOwnProperty(r)) var o = f[r](e, r, t, "prop", null, c);
                    if (o instanceof Error && !(o.message in p)) {
                        p[o.message] = !0;
                        a(n)
                    }
                }
            },
            getValue: function (t) { return t.valueLink ? (o(t), t.valueLink.value) : t.value },
            getChecked: function (t) { return t.checkedLink ? (i(t), t.checkedLink.value) : t.checked },
            executeOnChange: function (t, e) { return t.valueLink ? (o(t), t.valueLink.requestChange(e.target.value)) : t.checkedLink ? (i(t), t.checkedLink.requestChange(e.target.checked)) : t.onChange ? t.onChange.call(void 0, e) : void 0 }
        };
    t.exports = h
},
function (t, e, n) {
    "use strict";
    var r = n(3),
        o = (n(1), !1),
        i = { replaceNodeWithMarkup: null, processChildrenUpdates: null, injection: { injectEnvironment: function (t) { o ? r("104") : void 0, i.replaceNodeWithMarkup = t.replaceNodeWithMarkup, i.processChildrenUpdates = t.processChildrenUpdates, o = !0 } } };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { try { e(n) } catch (t) { null === o && (o = t) } }
    var o = null,
        i = { invokeGuardedCallback: r, invokeGuardedCallbackWithCatch: r, rethrowCaughtError: function () { if (o) { var t = o; throw o = null, t } } };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t) { s.enqueueUpdate(t) }

    function o(t) {
        var e = typeof t;
        if ("object" !== e) return e;
        var n = t.constructor && t.constructor.name || e,
            r = Object.keys(t);
        return r.length > 0 && r.length < 20 ? n + " (keys: " + r.join(", ") + ")" : n
    }

    function i(t, e) { var n = u.get(t); if (!n) { return null } return n }
    var a = n(3),
        u = (n(11), n(27)),
        s = (n(8), n(9)),
        c = (n(1), n(2), {
            isMounted: function (t) { var e = u.get(t); return !!e && !!e._renderedComponent },
            enqueueCallback: function (t, e, n) { c.validateCallback(e, n); var o = i(t); return o ? (o._pendingCallbacks ? o._pendingCallbacks.push(e) : o._pendingCallbacks = [e], void r(o)) : null },
            enqueueCallbackInternal: function (t, e) { t._pendingCallbacks ? t._pendingCallbacks.push(e) : t._pendingCallbacks = [e], r(t) },
            enqueueForceUpdate: function (t) {
                var e = i(t, "forceUpdate");
                e && (e._pendingForceUpdate = !0, r(e))
            },
            enqueueReplaceState: function (t, e) {
                var n = i(t, "replaceState");
                n && (n._pendingStateQueue = [e], n._pendingReplaceState = !0, r(n))
            },
            enqueueSetState: function (t, e) {
                var n = i(t, "setState");
                if (n) {
                    var o = n._pendingStateQueue || (n._pendingStateQueue = []);
                    o.push(e), r(n)
                }
            },
            enqueueElementInternal: function (t, e, n) { t._pendingElement = e, t._context = n, r(t) },
            validateCallback: function (t, e) { t && "function" != typeof t ? a("122", e, o(t)) : void 0 }
        });
    t.exports = c
},
function (t, e) {
    "use strict";
    var n = function (t) { return "undefined" != typeof MSApp && MSApp.execUnsafeLocalFunction ? function (e, n, r, o) { MSApp.execUnsafeLocalFunction(function () { return t(e, n, r, o) }) } : t };
    t.exports = n
},
function (t, e) {
    "use strict";

    function n(t) { var e, n = t.keyCode; return "charCode" in t ? (e = t.charCode, 0 === e && 13 === n && (e = 13)) : e = n, e >= 32 || 13 === e ? e : 0 }
    t.exports = n
},
function (t, e) {
    "use strict";

    function n(t) {
        var e = this,
            n = e.nativeEvent;
        if (n.getModifierState) return n.getModifierState(t);
        var r = o[t];
        return !!r && !!n[r]
    }

    function r(t) { return n }
    var o = { Alt: "altKey", Control: "ctrlKey", Meta: "metaKey", Shift: "shiftKey" };
    t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) { var e = t.target || t.srcElement || window; return e.correspondingUseElement && (e = e.correspondingUseElement), 3 === e.nodeType ? e.parentNode : e }
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t, e) {
        if (!i.canUseDOM || e && !("addEventListener" in document)) return !1;
        var n = "on" + t,
            r = n in document;
        if (!r) {
            var a = document.createElement("div");
            a.setAttribute(n, "return;"), r = "function" == typeof a[n]
        }
        return !r && o && "wheel" === t && (r = document.implementation.hasFeature("Events.wheel", "3.0")), r
    }
    var o, i = n(6);
    i.canUseDOM && (o = document.implementation && document.implementation.hasFeature && document.implementation.hasFeature("", "") !== !0), t.exports = r
},
function (t, e) {
    "use strict";

    function n(t, e) {
        var n = null === t || t === !1,
            r = null === e || e === !1;
        if (n || r) return n === r;
        var o = typeof t,
            i = typeof e;
        return "string" === o || "number" === o ? "string" === i || "number" === i : "object" === i && t.type === e.type && t.key === e.key
    }
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = (n(4), n(7)),
        o = (n(2), r);
    t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { this.props = t, this.context = e, this.refs = a, this.updater = n || i }
    var o = n(22),
        i = n(63),
        a = (n(113), n(24));
    n(1), n(2);
    r.prototype.isReactComponent = {}, r.prototype.setState = function (t, e) { "object" != typeof t && "function" != typeof t && null != t ? o("85") : void 0, this.updater.enqueueSetState(this, t), e && this.updater.enqueueCallback(this, e, "setState") }, r.prototype.forceUpdate = function (t) { this.updater.enqueueForceUpdate(this), t && this.updater.enqueueCallback(this, t, "forceUpdate") };
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e) { }
    var o = (n(2), { isMounted: function (t) { return !1 }, enqueueCallback: function (t, e) { }, enqueueForceUpdate: function (t) { r(t, "forceUpdate") }, enqueueReplaceState: function (t, e) { r(t, "replaceState") }, enqueueSetState: function (t, e) { r(t, "setState") } });
    t.exports = o
},
function (t, e) {
    "use strict";

    function n(t) { return t instanceof Object && Object.keys(t).length > 0 }

    function r(t) { var e = /^(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/; return e.test(t) }

    function o(t, e) {
        var a = "";
        if ("string" == typeof t) t = t.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"), a += r(t) ? '<a href="' + t + '" class="json-literal-string">"' + t + '"</a>' : '<span class="json-literal-string">"' + t + '"</span>';
        else if ("number" == typeof t) a += '<span class="json-literal-numeric">' + t + "</span>";
        else if ("boolean" == typeof t) a += '<span class="json-literal-boolean">' + t + "</span>";
        else if (null === t) a += '<span class="json-literal">null</span>';
        else if (t instanceof Array)
            if (t.length > 0) {
                a += '[<ol class="json-array">';
                for (var u = 0; u < t.length; ++u) a += "<li>", n(t[u]) && (a += '<a href class="json-toggle"></a>'), a += o(t[u], e), u < t.length - 1 && (a += ","), a += "</li>";
                a += "</ol>]"
            } else a += "[]";
        else if ("object" === ("undefined" == typeof t ? "undefined" : i(t))) {
            var s = Object.keys(t).length;
            if (s > 0) {
                a += '{<ul class="json-dict">';
                for (var c in t)
                    if (t.hasOwnProperty(c)) {
                        a += "<li>";
                        var l = e.withQuotes ? '<span class="property">"' + c + '"</span>' : c;
                        a += n(t[c]) ? '<a href class="json-toggle">' + l + "</a>" : l, a += ": " + o(t[c], e), --s > 0 && (a += ","), a += "</li>"
                    }
                a += "</ul>}"
            } else a += "{}"
        }
        return a
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var i = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) { return typeof t } : function (t) { return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t };
    e.initPlugin = function (t, e, r, i) {
        ! function (t, e, r, i) {
            return i = i || {}, t(e).each(function () {
                var e = o(r, i);
                n(r) && (e = '<a href class="json-toggle"></a>' + e), t(this).html(e), t(this).off("click"), t(this).on("click", "span.property", function (e) { t("li").removeClass("copyable"), t(this).parents("li").first().addClass("copyable") }), t(this).on("click", "a.json-placeholder", function () { return t(this).siblings("a.json-toggle").click(), !1 }), 1 == i.collapsed && t(this).find("a.json-toggle").click()
            })
        }(e, t, r, i)
    }
},
function (t, e, n) {
    "use strict";

    function r(t) { return (0, o.is)(Function, t) ? t : function () { return t } }
    e.__esModule = !0, e.default = r;
    var o = n(16)
},
function (t, e) { },
function (t, e, n) {
    "use strict";
    var r = n(7),
        o = { listen: function (t, e, n) { return t.addEventListener ? (t.addEventListener(e, n, !1), { remove: function () { t.removeEventListener(e, n, !1) } }) : t.attachEvent ? (t.attachEvent("on" + e, n), { remove: function () { t.detachEvent("on" + e, n) } }) : void 0 }, capture: function (t, e, n) { return t.addEventListener ? (t.addEventListener(e, n, !0), { remove: function () { t.removeEventListener(e, n, !0) } }) : { remove: r } }, registerDefault: function () { } };
    t.exports = o
},
function (t, e) {
    "use strict";

    function n(t) { try { t.focus() } catch (t) { } }
    t.exports = n
},
function (t, e) {
    "use strict";

    function n() { if ("undefined" == typeof document) return null; try { return document.activeElement || document.body } catch (t) { return document.body } }
    t.exports = n
},
function (t, e, n) {
    function r(t) {
        var e = -1,
            n = t ? t.length : 0;
        for (this.clear(); ++e < n;) {
            var r = t[e];
            this.set(r[0], r[1])
        }
    }
    var o = n(191),
        i = n(192),
        a = n(193),
        u = n(194),
        s = n(195);
    r.prototype.clear = o, r.prototype.delete = i, r.prototype.get = a, r.prototype.has = u, r.prototype.set = s, t.exports = r
},
function (t, e, n) {
    var r = n(32),
        o = r.Symbol;
    t.exports = o
},
function (t, e, n) {
    function r(t, e) { var n = o(t, e); if (n < 0) return !1; var r = t.length - 1; return n == r ? t.pop() : a.call(t, n, 1), !0 }
    var o = n(29),
        i = Array.prototype,
        a = i.splice;
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { var n = o(t, e); return n < 0 ? void 0 : t[n][1] }
    var o = n(29);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { return o(t, e) > -1 }
    var o = n(29);
    t.exports = r
},
function (t, e, n) {
    function r(t, e, n) {
        var r = o(t, e);
        r < 0 ? t.push([e, n]) : t[r][1] = n
    }
    var o = n(29);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { e = i(e, t) ? [e + ""] : o(e); for (var n = 0, r = e.length; null != t && n < r;) t = t[e[n++]]; return n && n == r ? t : void 0 }
    var o = n(80),
        i = n(42);
    t.exports = r
},
function (t, e) {
    function n(t, e) { return o.call(t, e) || "object" == typeof t && e in t && null === i(t) }
    var r = Object.prototype,
        o = r.hasOwnProperty,
        i = Object.getPrototypeOf;
    t.exports = n
},
function (t, e, n) {
    function r(t, e, n, u, s) { return t === e || (null == t || null == e || !i(t) && !a(e) ? t !== t && e !== e : o(t, e, r, n, u, s)) }
    var o = n(158),
        i = n(43),
        a = n(13);
    t.exports = r
},
function (t, e) {
    function n(t) { return function (e) { return null == e ? void 0 : e[t] } }
    t.exports = n
},
function (t, e, n) {
    function r(t) { return o(t) ? t : i(t) }
    var o = n(12),
        i = n(196);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { return o ? void 0 !== t[e] : a.call(t, e) }
    var o = n(31),
        i = Object.prototype,
        a = i.hasOwnProperty;
    t.exports = r
},
function (t, e) {
    function n(t, e) { return t = "number" == typeof t || o.test(t) ? +t : -1, e = null == e ? r : e, t > -1 && t % 1 == 0 && t < e }
    var r = 9007199254740991,
        o = /^(?:0|[1-9]\d*)$/;
    t.exports = n
},
function (t, e, n) {
    function r(t, e, n) { var r = null == t ? void 0 : o(t, e); return void 0 === r ? n : r }
    var o = n(76);
    t.exports = r
},
function (t, e, n) {
    function r(t) { return o(t) && u.call(t, "callee") && (!c.call(t, "callee") || s.call(t) == i) }
    var o = n(200),
        i = "[object Arguments]",
        a = Object.prototype,
        u = a.hasOwnProperty,
        s = a.toString,
        c = a.propertyIsEnumerable;
    t.exports = r
},
function (t, e, n) {
    function r(t) { return null != t && !("function" == typeof t && i(t)) && a(o(t)) }
    var o = n(173),
        i = n(86),
        a = n(33);
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = o(t) ? s.call(t) : ""; return e == i || e == a }
    var o = n(43),
        i = "[object Function]",
        a = "[object GeneratorFunction]",
        u = Object.prototype,
        s = u.toString;
    t.exports = r
},
function (t, e, n) {
    function r(t) { return "string" == typeof t || !o(t) && i(t) && s.call(t) == a }
    var o = n(12),
        i = n(13),
        a = "[object String]",
        u = Object.prototype,
        s = u.toString;
    t.exports = r
},
function (t, e) {
    function n() { throw new Error("setTimeout has not been defined") }

    function r() { throw new Error("clearTimeout has not been defined") }

    function o(t) { if (l === setTimeout) return setTimeout(t, 0); if ((l === n || !l) && setTimeout) return l = setTimeout, setTimeout(t, 0); try { return l(t, 0) } catch (e) { try { return l.call(null, t, 0) } catch (e) { return l.call(this, t, 0) } } }

    function i(t) { if (f === clearTimeout) return clearTimeout(t); if ((f === r || !f) && clearTimeout) return f = clearTimeout, clearTimeout(t); try { return f(t) } catch (e) { try { return f.call(null, t) } catch (e) { return f.call(this, t) } } }

    function a() { v && h && (v = !1, h.length ? d = h.concat(d) : g = -1, d.length && u()) }

    function u() {
        if (!v) {
            var t = o(a);
            v = !0;
            for (var e = d.length; e;) {
                for (h = d, d = []; ++g < e;) h && h[g].run();
                g = -1, e = d.length
            }
            h = null, v = !1, i(t)
        }
    }

    function s(t, e) { this.fun = t, this.array = e }

    function c() { }
    var l, f, p = t.exports = {};
    ! function () { try { l = "function" == typeof setTimeout ? setTimeout : n } catch (t) { l = n } try { f = "function" == typeof clearTimeout ? clearTimeout : r } catch (t) { f = r } }();
    var h, d = [],
        v = !1,
        g = -1;
    p.nextTick = function (t) {
        var e = new Array(arguments.length - 1);
        if (arguments.length > 1)
            for (var n = 1; n < arguments.length; n++) e[n - 1] = arguments[n];
        d.push(new s(t, e)), 1 !== d.length || v || o(u)
    }, s.prototype.run = function () { this.fun.apply(null, this.array) }, p.title = "browser", p.browser = !0, p.env = {}, p.argv = [], p.version = "", p.versions = {}, p.on = c, p.addListener = c, p.once = c, p.off = c, p.removeListener = c, p.removeAllListeners = c, p.emit = c, p.binding = function (t) { throw new Error("process.binding is not supported") }, p.cwd = function () { return "/" }, p.chdir = function (t) { throw new Error("process.chdir is not supported") }, p.umask = function () { return 0 }
},
function (t, e, n) {
    "use strict";

    function r() { }

    function o(t) { try { return t.then } catch (t) { return y = t, m } }

    function i(t, e) { try { return t(e) } catch (t) { return y = t, m } }

    function a(t, e, n) { try { t(e, n) } catch (t) { return y = t, m } }

    function u(t) {
        if ("object" != typeof this) throw new TypeError("Promises must be constructed via new");
        if ("function" != typeof t) throw new TypeError("not a function");
        this._45 = 0, this._81 = 0, this._65 = null, this._54 = null, t !== r && v(t, this)
    }

    function s(t, e, n) {
        return new t.constructor(function (o, i) {
            var a = new u(r);
            a.then(o, i), c(t, new d(e, n, a))
        })
    }

    function c(t, e) { for (; 3 === t._81;) t = t._65; return u._10 && u._10(t), 0 === t._81 ? 0 === t._45 ? (t._45 = 1, void (t._54 = e)) : 1 === t._45 ? (t._45 = 2, void (t._54 = [t._54, e])) : void t._54.push(e) : void l(t, e) }

    function l(t, e) {
        g(function () {
            var n = 1 === t._81 ? e.onFulfilled : e.onRejected;
            if (null === n) return void (1 === t._81 ? f(e.promise, t._65) : p(e.promise, t._65));
            var r = i(n, t._65);
            r === m ? p(e.promise, y) : f(e.promise, r)
        })
    }

    function f(t, e) {
        if (e === t) return p(t, new TypeError("A promise cannot be resolved with itself."));
        if (e && ("object" == typeof e || "function" == typeof e)) { var n = o(e); if (n === m) return p(t, y); if (n === t.then && e instanceof u) return t._81 = 3, t._65 = e, void h(t); if ("function" == typeof n) return void v(n.bind(e), t) }
        t._81 = 1, t._65 = e, h(t)
    }

    function p(t, e) { t._81 = 2, t._65 = e, u._97 && u._97(t, e), h(t) }

    function h(t) {
        if (1 === t._45 && (c(t, t._54), t._54 = null), 2 === t._45) {
            for (var e = 0; e < t._54.length; e++) c(t, t._54[e]);
            t._54 = null
        }
    }

    function d(t, e, n) { this.onFulfilled = "function" == typeof t ? t : null, this.onRejected = "function" == typeof e ? e : null, this.promise = n }

    function v(t, e) {
        var n = !1,
            r = a(t, function (t) { n || (n = !0, f(e, t)) }, function (t) { n || (n = !0, p(e, t)) });
        n || r !== m || (n = !0, p(e, y))
    }
    var g = n(115),
        y = null,
        m = {};
    t.exports = u, u._10 = null, u._97 = null, u._61 = r, u.prototype.then = function (t, e) { if (this.constructor !== u) return s(this, t, e); var n = new u(r); return c(this, new d(t, e, n)), n }
},
function (t, e) {
    "use strict";

    function n(t, e) { return t + e.charAt(0).toUpperCase() + e.substring(1) }
    var r = { animationIterationCount: !0, borderImageOutset: !0, borderImageSlice: !0, borderImageWidth: !0, boxFlex: !0, boxFlexGroup: !0, boxOrdinalGroup: !0, columnCount: !0, flex: !0, flexGrow: !0, flexPositive: !0, flexShrink: !0, flexNegative: !0, flexOrder: !0, gridRow: !0, gridColumn: !0, fontWeight: !0, lineClamp: !0, lineHeight: !0, opacity: !0, order: !0, orphans: !0, tabSize: !0, widows: !0, zIndex: !0, zoom: !0, fillOpacity: !0, floodOpacity: !0, stopOpacity: !0, strokeDasharray: !0, strokeDashoffset: !0, strokeMiterlimit: !0, strokeOpacity: !0, strokeWidth: !0 },
        o = ["Webkit", "ms", "Moz", "O"];
    Object.keys(r).forEach(function (t) { o.forEach(function (e) { r[n(e, t)] = r[t] }) });
    var i = { background: { backgroundAttachment: !0, backgroundColor: !0, backgroundImage: !0, backgroundPositionX: !0, backgroundPositionY: !0, backgroundRepeat: !0 }, backgroundPosition: { backgroundPositionX: !0, backgroundPositionY: !0 }, border: { borderWidth: !0, borderStyle: !0, borderColor: !0 }, borderBottom: { borderBottomWidth: !0, borderBottomStyle: !0, borderBottomColor: !0 }, borderLeft: { borderLeftWidth: !0, borderLeftStyle: !0, borderLeftColor: !0 }, borderRight: { borderRightWidth: !0, borderRightStyle: !0, borderRightColor: !0 }, borderTop: { borderTopWidth: !0, borderTopStyle: !0, borderTopColor: !0 }, font: { fontStyle: !0, fontVariant: !0, fontWeight: !0, fontSize: !0, lineHeight: !0, fontFamily: !0 }, outline: { outlineWidth: !0, outlineStyle: !0, outlineColor: !0 } },
        a = { isUnitlessNumber: r, shorthandPropertyExpansions: i };
    t.exports = a
},
function (t, e, n) {
    "use strict";

    function r(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }
    var o = n(3),
        i = n(14),
        a = (n(1), function () {
            function t(e) { r(this, t), this._callbacks = null, this._contexts = null, this._arg = e }
            return t.prototype.enqueue = function (t, e) { this._callbacks = this._callbacks || [], this._callbacks.push(t), this._contexts = this._contexts || [], this._contexts.push(e) }, t.prototype.notifyAll = function () {
                var t = this._callbacks,
                    e = this._contexts,
                    n = this._arg;
                if (t && e) {
                    t.length !== e.length ? o("24") : void 0, this._callbacks = null, this._contexts = null;
                    for (var r = 0; r < t.length; r++) t[r].call(e[r], n);
                    t.length = 0, e.length = 0
                }
            }, t.prototype.checkpoint = function () { return this._callbacks ? this._callbacks.length : 0 }, t.prototype.rollback = function (t) { this._callbacks && this._contexts && (this._callbacks.length = t, this._contexts.length = t) }, t.prototype.reset = function () { this._callbacks = null, this._contexts = null }, t.prototype.destructor = function () { this.reset() }, t
        }());
    t.exports = i.addPoolingTo(a)
},
function (t, e, n) {
    "use strict";

    function r(t) { return !!c.hasOwnProperty(t) || !s.hasOwnProperty(t) && (u.test(t) ? (c[t] = !0, !0) : (s[t] = !0, !1)) }

    function o(t, e) { return null == e || t.hasBooleanValue && !e || t.hasNumericValue && isNaN(e) || t.hasPositiveNumericValue && e < 1 || t.hasOverloadedBooleanValue && e === !1 }
    var i = n(18),
        a = (n(5), n(8), n(274)),
        u = (n(2), new RegExp("^[" + i.ATTRIBUTE_NAME_START_CHAR + "][" + i.ATTRIBUTE_NAME_CHAR + "]*$")),
        s = {},
        c = {},
        l = {
            createMarkupForID: function (t) { return i.ID_ATTRIBUTE_NAME + "=" + a(t) },
            setAttributeForID: function (t, e) { t.setAttribute(i.ID_ATTRIBUTE_NAME, e) },
            createMarkupForRoot: function () { return i.ROOT_ATTRIBUTE_NAME + '=""' },
            setAttributeForRoot: function (t) { t.setAttribute(i.ROOT_ATTRIBUTE_NAME, "") },
            createMarkupForProperty: function (t, e) { var n = i.properties.hasOwnProperty(t) ? i.properties[t] : null; if (n) { if (o(n, e)) return ""; var r = n.attributeName; return n.hasBooleanValue || n.hasOverloadedBooleanValue && e === !0 ? r + '=""' : r + "=" + a(e) } return i.isCustomAttribute(t) ? null == e ? "" : t + "=" + a(e) : null },
            createMarkupForCustomAttribute: function (t, e) { return r(t) && null != e ? t + "=" + a(e) : "" },
            setValueForProperty: function (t, e, n) {
                var r = i.properties.hasOwnProperty(e) ? i.properties[e] : null;
                if (r) {
                    var a = r.mutationMethod;
                    if (a) a(t, n);
                    else {
                        if (o(r, n)) return void this.deleteValueForProperty(t, e);
                        if (r.mustUseProperty) t[r.propertyName] = n;
                        else {
                            var u = r.attributeName,
                                s = r.attributeNamespace;
                            s ? t.setAttributeNS(s, u, "" + n) : r.hasBooleanValue || r.hasOverloadedBooleanValue && n === !0 ? t.setAttribute(u, "") : t.setAttribute(u, "" + n)
                        }
                    }
                } else if (i.isCustomAttribute(e)) return void l.setValueForAttribute(t, e, n)
            },
            setValueForAttribute: function (t, e, n) { if (r(e)) { null == n ? t.removeAttribute(e) : t.setAttribute(e, "" + n) } },
            deleteValueForAttribute: function (t, e) { t.removeAttribute(e) },
            deleteValueForProperty: function (t, e) {
                var n = i.properties.hasOwnProperty(e) ? i.properties[e] : null;
                if (n) {
                    var r = n.mutationMethod;
                    if (r) r(t, void 0);
                    else if (n.mustUseProperty) {
                        var o = n.propertyName;
                        n.hasBooleanValue ? t[o] = !1 : t[o] = ""
                    } else t.removeAttribute(n.attributeName)
                } else i.isCustomAttribute(e) && t.removeAttribute(e)
            }
        };
    t.exports = l
},
function (t, e) {
    "use strict";
    var n = { hasCachedChildNodes: 1 };
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r() {
        if (this._rootNodeID && this._wrapperState.pendingUpdate) {
            this._wrapperState.pendingUpdate = !1;
            var t = this._currentElement.props,
                e = u.getValue(t);
            null != e && o(this, Boolean(t.multiple), e)
        }
    }

    function o(t, e, n) {
        var r, o, i = s.getNodeFromInstance(t).options;
        if (e) {
            for (r = {}, o = 0; o < n.length; o++) r["" + n[o]] = !0;
            for (o = 0; o < i.length; o++) {
                var a = r.hasOwnProperty(i[o].value);
                i[o].selected !== a && (i[o].selected = a)
            }
        } else {
            for (r = "" + n, o = 0; o < i.length; o++)
                if (i[o].value === r) return void (i[o].selected = !0);
            i.length && (i[0].selected = !0)
        }
    }

    function i(t) {
        var e = this._currentElement.props,
            n = u.executeOnChange(e, t);
        return this._rootNodeID && (this._wrapperState.pendingUpdate = !0), c.asap(r, this), n
    }
    var a = n(4),
        u = n(51),
        s = n(5),
        c = n(9),
        l = (n(2), !1),
        f = {
            getHostProps: function (t, e) { return a({}, e, { onChange: t._wrapperState.onChange, value: void 0 }) },
            mountWrapper: function (t, e) {
                var n = u.getValue(e);
                t._wrapperState = { pendingUpdate: !1, initialValue: null != n ? n : e.defaultValue, listeners: null, onChange: i.bind(t), wasMultiple: Boolean(e.multiple) }, void 0 === e.value || void 0 === e.defaultValue || l || (l = !0)
            },
            getSelectValueContext: function (t) { return t._wrapperState.initialValue },
            postUpdateWrapper: function (t) {
                var e = t._currentElement.props;
                t._wrapperState.initialValue = void 0;
                var n = t._wrapperState.wasMultiple;
                t._wrapperState.wasMultiple = Boolean(e.multiple);
                var r = u.getValue(e);
                null != r ? (t._wrapperState.pendingUpdate = !1, o(t, Boolean(e.multiple), r)) : n !== Boolean(e.multiple) && (null != e.defaultValue ? o(t, Boolean(e.multiple), e.defaultValue) : o(t, Boolean(e.multiple), e.multiple ? [] : ""))
            }
        };
    t.exports = f
},
function (t, e) {
    "use strict";
    var n, r = { injectEmptyComponentFactory: function (t) { n = t } },
        o = { create: function (t) { return n(t) } };
    o.injection = r, t.exports = o
},
function (t, e) {
    "use strict";
    var n = { logTopLevelRenders: !1 };
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { return u ? void 0 : a("111", t.type), new u(t) }

    function o(t) { return new s(t) }

    function i(t) { return t instanceof s }
    var a = n(3),
        u = (n(1), null),
        s = null,
        c = { injectGenericComponentClass: function (t) { u = t }, injectTextComponentClass: function (t) { s = t } },
        l = { createInternalComponent: r, createInstanceForText: o, isTextComponent: i, injection: c };
    t.exports = l
},
function (t, e, n) {
    "use strict";

    function r(t) { return i(document.documentElement, t) }
    var o = n(233),
        i = n(137),
        a = n(68),
        u = n(69),
        s = {
            hasSelectionCapabilities: function (t) { var e = t && t.nodeName && t.nodeName.toLowerCase(); return e && ("input" === e && "text" === t.type || "textarea" === e || "true" === t.contentEditable) },
            getSelectionInformation: function () { var t = u(); return { focusedElem: t, selectionRange: s.hasSelectionCapabilities(t) ? s.getSelection(t) : null } },
            restoreSelection: function (t) {
                var e = u(),
                    n = t.focusedElem,
                    o = t.selectionRange;
                e !== n && r(n) && (s.hasSelectionCapabilities(n) && s.setSelection(n, o), a(n))
            },
            getSelection: function (t) {
                var e;
                if ("selectionStart" in t) e = { start: t.selectionStart, end: t.selectionEnd };
                else if (document.selection && t.nodeName && "input" === t.nodeName.toLowerCase()) {
                    var n = document.selection.createRange();
                    n.parentElement() === t && (e = { start: -n.moveStart("character", -t.value.length), end: -n.moveEnd("character", -t.value.length) })
                } else e = o.getOffsets(t);
                return e || { start: 0, end: 0 }
            },
            setSelection: function (t, e) {
                var n = e.start,
                    r = e.end;
                if (void 0 === r && (r = n), "selectionStart" in t) t.selectionStart = n, t.selectionEnd = Math.min(r, t.value.length);
                else if (document.selection && t.nodeName && "input" === t.nodeName.toLowerCase()) {
                    var i = t.createTextRange();
                    i.collapse(!0), i.moveStart("character", n), i.moveEnd("character", r - n), i.select()
                } else o.setOffsets(t, e)
            }
        };
    t.exports = s
},
function (t, e, n) {
    "use strict";

    function r(t, e) {
        for (var n = Math.min(t.length, e.length), r = 0; r < n; r++)
            if (t.charAt(r) !== e.charAt(r)) return r;
        return t.length === e.length ? -1 : n
    }

    function o(t) { return t ? t.nodeType === D ? t.documentElement : t.firstChild : null }

    function i(t) { return t.getAttribute && t.getAttribute(A) || "" }

    function a(t, e, n, r, o) {
        var i;
        if (w.logTopLevelRenders) {
            var a = t._currentElement.props.child,
                u = a.type;
            i = "React mount: " + ("string" == typeof u ? u : u.displayName || u.name), console.time(i)
        }
        var s = E.mountComponent(t, n, null, b(t, e), o, 0);
        i && console.timeEnd(i), t._renderedComponent._topLevelWrapper = t, U._mountImageIntoNode(s, e, t, r, n)
    }

    function u(t, e, n, r) {
        var o = k.ReactReconcileTransaction.getPooled(!n && x.useCreateElement);
        o.perform(a, null, t, e, o, n, r), k.ReactReconcileTransaction.release(o)
    }

    function s(t, e, n) { for (E.unmountComponent(t, n), e.nodeType === D && (e = e.documentElement); e.lastChild;) e.removeChild(e.lastChild) }

    function c(t) { var e = o(t); if (e) { var n = m.getInstanceFromNode(e); return !(!n || !n._hostParent) } }

    function l(t) { return !(!t || t.nodeType !== j && t.nodeType !== D && t.nodeType !== I) }

    function f(t) {
        var e = o(t),
            n = e && m.getInstanceFromNode(e);
        return n && !n._hostParent ? n : null
    }

    function p(t) { var e = f(t); return e ? e._hostContainerInfo._topLevelWrapper : null }
    var h = n(3),
        d = n(17),
        v = n(18),
        g = n(20),
        y = n(34),
        m = (n(11), n(5)),
        b = n(227),
        x = n(229),
        w = n(96),
        _ = n(27),
        C = (n(8), n(243)),
        E = n(19),
        M = n(54),
        k = n(9),
        T = n(24),
        S = n(106),
        N = (n(1), n(38)),
        O = n(60),
        A = (n(2), v.ID_ATTRIBUTE_NAME),
        P = v.ROOT_ATTRIBUTE_NAME,
        j = 1,
        D = 9,
        I = 11,
        R = {},
        L = 1,
        F = function () { this.rootID = L++ };
    F.prototype.isReactComponent = {}, F.prototype.render = function () { return this.props.child }, F.isReactTopLevelWrapper = !0;
    var U = {
        TopLevelWrapper: F,
        _instancesByReactRootID: R,
        scrollMonitor: function (t, e) { e() },
        _updateRootComponent: function (t, e, n, r, o) { return U.scrollMonitor(r, function () { M.enqueueElementInternal(t, e, n), o && M.enqueueCallbackInternal(t, o) }), t },
        _renderNewRootComponent: function (t, e, n, r) {
            l(e) ? void 0 : h("37"), y.ensureScrollValueMonitoring();
            var o = S(t, !1);
            k.batchedUpdates(u, o, e, n, r);
            var i = o._instance.rootID;
            return R[i] = o, o
        },
        renderSubtreeIntoContainer: function (t, e, n, r) { return null != t && _.has(t) ? void 0 : h("38"), U._renderSubtreeIntoContainer(t, e, n, r) },
        _renderSubtreeIntoContainer: function (t, e, n, r) {
            M.validateCallback(r, "ReactDOM.render"), g.isValidElement(e) ? void 0 : h("39", "string" == typeof e ? " Instead of passing a string like 'div', pass React.createElement('div') or <div />." : "function" == typeof e ? " Instead of passing a class like Foo, pass React.createElement(Foo) or <Foo />." : null != e && void 0 !== e.props ? " This may be caused by unintentionally loading two independent copies of React." : "");
            var a, u = g.createElement(F, { child: e });
            if (t) {
                var s = _.get(t);
                a = s._processChildContext(s._context)
            } else a = T;
            var l = p(n);
            if (l) {
                var f = l._currentElement,
                    d = f.props.child;
                if (O(d, e)) {
                    var v = l._renderedComponent.getPublicInstance(),
                        y = r && function () { r.call(v) };
                    return U._updateRootComponent(l, u, a, n, y), v
                }
                U.unmountComponentAtNode(n)
            }
            var m = o(n),
                b = m && !!i(m),
                x = c(n),
                w = b && !l && !x,
                C = U._renderNewRootComponent(u, n, w, a)._renderedComponent.getPublicInstance();
            return r && r.call(C), C
        },
        render: function (t, e, n) { return U._renderSubtreeIntoContainer(null, t, e, n) },
        unmountComponentAtNode: function (t) { l(t) ? void 0 : h("40"); var e = p(t); if (!e) { c(t), 1 === t.nodeType && t.hasAttribute(P); return !1 } return delete R[e._instance.rootID], k.batchedUpdates(s, e, t, !1), !0 },
        _mountImageIntoNode: function (t, e, n, i, a) {
            if (l(e) ? void 0 : h("41"), i) {
                var u = o(e);
                if (C.canReuseMarkup(t, u)) return void m.precacheNode(n, u);
                var s = u.getAttribute(C.CHECKSUM_ATTR_NAME);
                u.removeAttribute(C.CHECKSUM_ATTR_NAME);
                var c = u.outerHTML;
                u.setAttribute(C.CHECKSUM_ATTR_NAME, s);
                var f = t,
                    p = r(f, c),
                    v = " (client) " + f.substring(p - 20, p + 20) + "\n (server) " + c.substring(p - 20, p + 20);
                e.nodeType === D ? h("42", v) : void 0
            }
            if (e.nodeType === D ? h("43") : void 0, a.useCreateElement) {
                for (; e.lastChild;) e.removeChild(e.lastChild);
                d.insertTreeBefore(e, t, null)
            } else N(e, t), m.precacheNode(n, e.firstChild)
        }
    };
    t.exports = U
},
function (t, e, n) {
    "use strict";
    var r = n(3),
        o = n(20),
        i = (n(1), { HOST: 0, COMPOSITE: 1, EMPTY: 2, getType: function (t) { return null === t || t === !1 ? i.EMPTY : o.isValidElement(t) ? "function" == typeof t.type ? i.COMPOSITE : i.HOST : void r("26", t) } });
    t.exports = i
},
function (t, e) {
    "use strict";
    var n = { currentScrollLeft: 0, currentScrollTop: 0, refreshScrollValues: function (t) { n.currentScrollLeft = t.x, n.currentScrollTop = t.y } };
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return null == e ? o("30") : void 0, null == t ? e : Array.isArray(t) ? Array.isArray(e) ? (t.push.apply(t, e), t) : (t.push(e), t) : Array.isArray(e) ? [t].concat(e) : [t, e] }
    var o = n(3);
    n(1);
    t.exports = r
},
function (t, e) {
    "use strict";

    function n(t, e, n) { Array.isArray(t) ? t.forEach(e, n) : t && e.call(n, t) }
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) {
        for (var e;
            (e = t._renderedNodeType) === o.COMPOSITE;) t = t._renderedComponent;
        return e === o.HOST ? t._renderedComponent : e === o.EMPTY ? null : void 0
    }
    var o = n(100);
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r() { return !i && o.canUseDOM && (i = "textContent" in document.documentElement ? "textContent" : "innerText"), i }
    var o = n(6),
        i = null;
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t) { if (t) { var e = t.getName(); if (e) return " Check the render method of `" + e + "`." } return "" }

    function o(t) { return "function" == typeof t && "undefined" != typeof t.prototype && "function" == typeof t.prototype.mountComponent && "function" == typeof t.prototype.receiveComponent }

    function i(t, e) {
        var n;
        if (null === t || t === !1) n = c.create(i);
        else if ("object" == typeof t) {
            var u = t,
                s = u.type;
            if ("function" != typeof s && "string" != typeof s) {
                var p = "";
                p += r(u._owner), a("130", null == s ? s : typeof s, p)
            }
            "string" == typeof u.type ? n = l.createInternalComponent(u) : o(u.type) ? (n = new u.type(u), n.getHostNode || (n.getHostNode = n.getNativeNode)) : n = new f(u)
        } else "string" == typeof t || "number" == typeof t ? n = l.createInstanceForText(t) : a("131", typeof t);
        return n._mountIndex = 0, n._mountImage = null, n
    }
    var a = n(3),
        u = n(4),
        s = n(224),
        c = n(95),
        l = n(97),
        f = (n(271), n(1), n(2), function (t) { this.construct(t) });
    u(f.prototype, s, { _instantiateReactComponent: i }), t.exports = i
},
function (t, e) {
    "use strict";

    function n(t) { var e = t && t.nodeName && t.nodeName.toLowerCase(); return "input" === e ? !!r[t.type] : "textarea" === e }
    var r = {
        color: !0,
        date: !0,
        datetime: !0,
        "datetime-local": !0,
        email: !0,
        month: !0,
        number: !0,
        password: !0,
        range: !0,
        search: !0,
        tel: !0,
        text: !0,
        time: !0,
        url: !0,
        week: !0
    };
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = n(6),
        o = n(37),
        i = n(38),
        a = function (t, e) {
            if (e) { var n = t.firstChild; if (n && n === t.lastChild && 3 === n.nodeType) return void (n.nodeValue = e) }
            t.textContent = e
        };
    r.canUseDOM && ("textContent" in document.documentElement || (a = function (t, e) { return 3 === t.nodeType ? void (t.nodeValue = e) : void i(t, o(e)) })), t.exports = a
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return t && "object" == typeof t && null != t.key ? c.escape(t.key) : e.toString(36) }

    function o(t, e, n, i) {
        var p = typeof t;
        if ("undefined" !== p && "boolean" !== p || (t = null), null === t || "string" === p || "number" === p || "object" === p && t.$$typeof === u) return n(i, t, "" === e ? l + r(t, 0) : e), 1;
        var h, d, v = 0,
            g = "" === e ? l : e + f;
        if (Array.isArray(t))
            for (var y = 0; y < t.length; y++) h = t[y], d = g + r(h, y), v += o(h, d, n, i);
        else {
            var m = s(t);
            if (m) {
                var b, x = m.call(t);
                if (m !== t.entries)
                    for (var w = 0; !(b = x.next()).done;) h = b.value, d = g + r(h, w++), v += o(h, d, n, i);
                else
                    for (; !(b = x.next()).done;) {
                        var _ = b.value;
                        _ && (h = _[1], d = g + c.escape(_[0]) + f + r(h, 0), v += o(h, d, n, i))
                    }
            } else if ("object" === p) {
                var C = "",
                    E = String(t);
                a("31", "[object Object]" === E ? "object with keys {" + Object.keys(t).join(", ") + "}" : E, C)
            }
        }
        return v
    }

    function i(t, e, n) { return null == t ? 0 : o(t, "", e, n) }
    var a = n(3),
        u = (n(11), n(239)),
        s = n(270),
        c = (n(1), n(50)),
        l = (n(2), "."),
        f = ":";
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t) {
        var e = Function.prototype.toString,
            n = Object.prototype.hasOwnProperty,
            r = RegExp("^" + e.call(n).replace(/[\\^$.*+?()[\]{}|]/g, "\\$&").replace(/hasOwnProperty|(function).*?(?=\\\()| for .+?(?=\\\])/g, "$1.*?") + "$");
        try { var o = e.call(t); return r.test(o) } catch (t) { return !1 }
    }

    function o(t) {
        var e = c(t);
        if (e) {
            var n = e.childIDs;
            l(t), n.forEach(o)
        }
    }

    function i(t, e, n) { return "\n    in " + (t || "Unknown") + (e ? " (at " + e.fileName.replace(/^.*[\\\/]/, "") + ":" + e.lineNumber + ")" : n ? " (created by " + n + ")" : "") }

    function a(t) { return null == t ? "#empty" : "string" == typeof t || "number" == typeof t ? "#text" : "string" == typeof t.type ? t.type : t.type.displayName || t.type.name || "Unknown" }

    function u(t) {
        var e, n = M.getDisplayName(t),
            r = M.getElement(t),
            o = M.getOwnerID(t);
        return o && (e = M.getDisplayName(o)), i(n, r && r._source, e)
    }
    var s, c, l, f, p, h, d, v = n(22),
        g = n(11),
        y = (n(1), n(2), "function" == typeof Array.from && "function" == typeof Map && r(Map) && null != Map.prototype && "function" == typeof Map.prototype.keys && r(Map.prototype.keys) && "function" == typeof Set && r(Set) && null != Set.prototype && "function" == typeof Set.prototype.keys && r(Set.prototype.keys));
    if (y) {
        var m = new Map,
            b = new Set;
        s = function (t, e) { m.set(t, e) }, c = function (t) { return m.get(t) }, l = function (t) { m.delete(t) }, f = function () { return Array.from(m.keys()) }, p = function (t) { b.add(t) }, h = function (t) { b.delete(t) }, d = function () { return Array.from(b.keys()) }
    } else {
        var x = {},
            w = {},
            _ = function (t) { return "." + t },
            C = function (t) { return parseInt(t.substr(1), 10) };
        s = function (t, e) {
            var n = _(t);
            x[n] = e
        }, c = function (t) { var e = _(t); return x[e] }, l = function (t) {
            var e = _(t);
            delete x[e]
        }, f = function () { return Object.keys(x).map(C) }, p = function (t) {
            var e = _(t);
            w[e] = !0
        }, h = function (t) {
            var e = _(t);
            delete w[e]
        }, d = function () { return Object.keys(w).map(C) }
    }
    var E = [],
        M = {
            onSetChildren: function (t, e) {
                var n = c(t);
                n ? void 0 : v("144"), n.childIDs = e;
                for (var r = 0; r < e.length; r++) {
                    var o = e[r],
                        i = c(o);
                    i ? void 0 : v("140"), null == i.childIDs && "object" == typeof i.element && null != i.element ? v("141") : void 0, i.isMounted ? void 0 : v("71"), null == i.parentID && (i.parentID = t), i.parentID !== t ? v("142", o, i.parentID, t) : void 0
                }
            },
            onBeforeMountComponent: function (t, e, n) {
                var r = { element: e, parentID: n, text: null, childIDs: [], isMounted: !1, updateCount: 0 };
                s(t, r)
            },
            onBeforeUpdateComponent: function (t, e) {
                var n = c(t);
                n && n.isMounted && (n.element = e)
            },
            onMountComponent: function (t) {
                var e = c(t);
                e ? void 0 : v("144"), e.isMounted = !0;
                var n = 0 === e.parentID;
                n && p(t)
            },
            onUpdateComponent: function (t) {
                var e = c(t);
                e && e.isMounted && e.updateCount++
            },
            onUnmountComponent: function (t) {
                var e = c(t);
                if (e) {
                    e.isMounted = !1;
                    var n = 0 === e.parentID;
                    n && h(t)
                }
                E.push(t)
            },
            purgeUnmountedComponents: function () {
                if (!M._preventPurging) {
                    for (var t = 0; t < E.length; t++) {
                        var e = E[t];
                        o(e)
                    }
                    E.length = 0
                }
            },
            isMounted: function (t) { var e = c(t); return !!e && e.isMounted },
            getCurrentStackAddendum: function (t) {
                var e = "";
                if (t) {
                    var n = a(t),
                        r = t._owner;
                    e += i(n, t._source, r && r.getName())
                }
                var o = g.current,
                    u = o && o._debugID;
                return e += M.getStackAddendumByID(u)
            },
            getStackAddendumByID: function (t) { for (var e = ""; t;) e += u(t), t = M.getParentID(t); return e },
            getChildIDs: function (t) { var e = c(t); return e ? e.childIDs : [] },
            getDisplayName: function (t) { var e = M.getElement(t); return e ? a(e) : null },
            getElement: function (t) { var e = c(t); return e ? e.element : null },
            getOwnerID: function (t) { var e = M.getElement(t); return e && e._owner ? e._owner._debugID : null },
            getParentID: function (t) { var e = c(t); return e ? e.parentID : null },
            getSource: function (t) {
                var e = c(t),
                    n = e ? e.element : null,
                    r = null != n ? n._source : null;
                return r
            },
            getText: function (t) { var e = M.getElement(t); return "string" == typeof e ? e : "number" == typeof e ? "" + e : null },
            getUpdateCount: function (t) { var e = c(t); return e ? e.updateCount : 0 },
            getRootIDs: d,
            getRegisteredIDs: f
        };
    t.exports = M
},
function (t, e) {
    "use strict";
    var n = "function" == typeof Symbol && Symbol.for && Symbol.for("react.element") || 60103;
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = {};
    t.exports = r
},
function (t, e, n) {
    "use strict";
    var r = !1;
    t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) { var e = t && (r && t[r] || t[o]); if ("function" == typeof e) return e }
    var r = "function" == typeof Symbol && Symbol.iterator,
        o = "@@iterator";
    t.exports = n
},
function (t, e) {
    (function (e) {
        "use strict";

        function n(t) { u.length || (a(), s = !0), u[u.length] = t }

        function r() {
            for (; c < u.length;) {
                var t = c;
                if (c += 1, u[t].call(), c > l) {
                    for (var e = 0, n = u.length - c; e < n; e++) u[e] = u[e + c];
                    u.length -= c, c = 0
                }
            }
            u.length = 0, c = 0, s = !1
        }

        function o(t) {
            var e = 1,
                n = new p(t),
                r = document.createTextNode("");
            return n.observe(r, { characterData: !0 }),
                function () { e = -e, r.data = e }
        }

        function i(t) {
            return function () {
                function e() { clearTimeout(n), clearInterval(r), t() }
                var n = setTimeout(e, 0),
                    r = setInterval(e, 50)
            }
        }
        t.exports = n;
        var a, u = [],
            s = !1,
            c = 0,
            l = 1024,
            f = "undefined" != typeof e ? e : self,
            p = f.MutationObserver || f.WebKitMutationObserver;
        a = "function" == typeof p ? o(r) : i(r), n.requestFlush = a, n.makeRequestCallFromTimer = i
    }).call(e, function () { return this }())
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function i(t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }

    function a(t, e) {
        if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e);
        t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var u = function () {
        function t(t, e) {
            for (var n = 0; n < e.length; n++) {
                var r = e[n];
                r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(t, r.key, r)
            }
        }
        return function (e, n, r) { return n && t(e.prototype, n), r && t(e, r), e }
    }(),
        s = n(23),
        c = r(s),
        l = n(119),
        f = r(l),
        p = n(120),
        h = r(p),
        d = n(117),
        v = r(d),
        g = n(118),
        y = r(g);
    n(132), n(66);
    var m = function (t) {
        function e(t) { o(this, e); var n = i(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, t)); return window.json = t.json, n.state = { selectedTab: "tree", json: t.json, selectedJSON: t.json }, n }
        return a(e, t), u(e, [{ key: "changeTabSelection", value: function (t) { this.setState({ selectedTab: t }) } }, {
            key: "changeJSON",
            value: function (t) {
                var e = this;
                this.setState({ json: t, selectedJSON: t }, function () { window.json = t, e.changeTabSelection("tree") })
            }
        }, { key: "changeTargetNodeOnChart", value: function (t) { this.setState({ selectedJSON: t }) } }, { key: "render", value: function () { return window.json = this.state.json, c.default.createElement("div", { className: "App" }, c.default.createElement(f.default, { changeTabSelection: this.changeTabSelection.bind(this), selectedTab: this.state.selectedTab }), c.default.createElement("div", { className: "tab-container" }, "tree" === this.state.selectedTab && c.default.createElement(h.default, { data: this.state.json }), "chart" === this.state.selectedTab && c.default.createElement(v.default, { rootData: this.state.json, data: this.state.selectedJSON, changeTargetNodeOnChart: this.changeTargetNodeOnChart.bind(this) }), "jsonInput" === this.state.selectedTab && c.default.createElement(y.default, { json: this.state.json, changeJSON: this.changeJSON.bind(this) }))) } }]), e
    }(s.Component);
    e.default = m
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t) { if (Array.isArray(t)) { for (var e = 0, n = Array(t.length); e < t.length; e++) n[e] = t[e]; return n } return Array.from(t) }

    function i(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function a(t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }

    function u(t, e) {
        if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e);
        t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var s = function () {
        function t(t, e) {
            for (var n = 0; n < e.length; n++) {
                var r = e[n];
                r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(t, r.key, r)
            }
        }
        return function (e, n, r) { return n && t(e.prototype, n), r && t(e, r), e }
    }(),
        c = n(23),
        l = r(c),
        f = n(45),
        p = n(126),
        h = function (t) {
            function e(t) { i(this, e); var n = a(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, t)); return n.state = { breadcrumbs: ["response"], rootState: t.rootData, chartData: t.data, positionTop: window.innerHeight / 2 }, n }
            return u(e, t), s(e, [{
                key: "createValidPath",
                value: function (t) {
                    var e = t.lastIndexOf("["),
                        n = t.lastIndexOf("]");
                    if (e > -1) { var r = t.substring(e + 1, n); return r }
                    return t
                }
            }, {
                key: "createNewNodeValue",
                value: function (t) {
                    var e = this.props.data,
                        n = [].concat(o(t));
                    return 1 == n.length ? this.state.rootState : (n.reverse().splice(0, 1), n.forEach(function (t, n) {
                        var r = t.lastIndexOf("["),
                            o = t.lastIndexOf("]");
                        if (r > -1) { var i = t.substring(r + 1, o); return e = e[i] }
                        e = e[t]
                    }), e)
                }
            }, { key: "generateDataFromBreadcumb", value: function (t) { var e = this.state.rootState; return t.forEach(function (t, n) { e = e[t] }), e } }, {
                key: "gotToChart",
                value: function (t) {
                    var e = this.state.breadcrumbs.slice(1, t + 1),
                        n = this.generateDataFromBreadcumb(e),
                        r = {};
                    0 === t ? r = Object.assign({}, n) : r[e[e.length - 1]] = n, this.setState({ breadcrumbs: this.state.breadcrumbs.slice(0, t + 1) }), this.props.changeTargetNodeOnChart(r)
                }
            }, {
                key: "renderIngChart",
                value: function () {
                    var t = this,
                        e = {
                            state: this.props.data,
                            rootKeyName: "response",
                            onClickText: function (e) {
                                var n = e,
                                    r = void 0,
                                    o = !1,
                                    i = t.createValidPath(e.name);
                                if (t.state.breadcrumbs[t.state.breadcrumbs.length - 1] !== n.name && 1 !== n.depth || t.state.breadcrumbs[t.state.breadcrumbs.length - 1] !== n.name && 1 == n.depth)
                                    for (o = !0, r = [e.name]; e.hasOwnProperty("parent") && e.parent.hasOwnProperty("name");) r.push(t.createValidPath(e.parent.name)), e = e.parent;
                                else r = t.state.breadcrumbs;
                                var a = r,
                                    u = t.createNewNodeValue(a),
                                    s = {};
                                0 == n.depth ? s = Object.assign({}, u) : s[i] = u, t.props.changeTargetNodeOnChart(s), o && (0 === n.depth ? t.setState({ breadcrumbs: ["response"] }) : ! function () {
                                    var e = t.state.breadcrumbs,
                                        n = [];
                                    a.forEach(function (r, o) {
                                        if (e.indexOf(r) === -1) {
                                            var i = t.createValidPath(r);
                                            n.push(i)
                                        }
                                    }), t.setState({ breadcrumbs: t.state.breadcrumbs.concat(n.reverse()) })
                                }())
                            },
                            id: "jsonTree",
                            size: window.innerWidth - 100,
                            aspectRatio: .8,
                            isSorted: !1,
                            margin: { top: 50, left: 100 },
                            widthBetweenNodesCoeff: 1.5,
                            heightBetweenNodesCoeff: 2,
                            style: { node: { colors: { collapsed: "#e2777a", parent: "#00fcd4", default: "#44c7f4" }, stroke: "white" }, text: { colors: { default: "#ccc", hover: "#f08d49" }, "font-size": "12px" }, link: { stroke: "#188E3F", fill: "none" } },
                            tooltipOptions: { offset: { left: 50, top: 10 }, indentationSize: 2, style: { background: "#222", padding: "8px", color: "#2f2442", "border-radius": "2px", "box-shadow": "0 7px 7px 0 #111", "font-size": "13px", "line-height": "1.3" } }
                        };
                    this.renderChart = (0, p.tree)((0, f.findDOMNode)(this), e), this.renderChart()
                }
            }, { key: "componentWillMount", value: function () { this.prepareComponentState(this.props) } }, { key: "componentWillReceiveProps", value: function (t) { this.prepareComponentState(t), this.renderChart(t.data || t.state) } }, { key: "prepareComponentState", value: function (t) { this.setState({ chartData: t.data }) } }, { key: "componentDidMount", value: function () { this.renderIngChart() } }, { key: "render", value: function () { var t = this; return l.default.createElement("div", null, l.default.createElement("div", { className: "breadcumb" }, l.default.createElement("ul", null, this.state.breadcrumbs.map(function (e, n) { return l.default.createElement("li", { key: n }, l.default.createElement("a", { href: "#", onClick: t.gotToChart.bind(t, n) }, " ", e, " ")) }))), l.default.createElement("div", { className: "chart-holder" })) } }]), e
        }(c.Component);
    e.default = h
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function i(t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }

    function a(t, e) {
        if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e);
        t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var u = function () {
        function t(t, e) {
            for (var n = 0; n < e.length; n++) {
                var r = e[n];
                r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(t, r.key, r)
            }
        }
        return function (e, n, r) { return n && t(e.prototype, n), r && t(e, r), e }
    }(),
        s = n(23),
        c = r(s),
        l = (n(64), function (t) {
            function e(t) { o(this, e); var n = i(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, t)); return n.state = { errors: { jsonParseFailed: { status: !1, message: "Failed to parse invalid JSON format" }, rawJSON: { status: !1, message: "Field shouldn't be empty" } }, json: JSON.stringify(t.json, null, 4) }, n }
            return a(e, t), u(e, [{
                key: "parseJSON",
                value: function () {
                    var t = this.refs.rawJSON.value.trim();
                    if (t.startsWith("\"{")) {
                        t = trim(t, "\"").replace(/\\"/g, '"');
                    }
                    if (this.resetErrors(), !t) return void this.setState({ errors: Object.assign({}, this.state.errors, Object.assign({}, this.state.errors, { rawJSON: Object.assign({}, this.state.errors.rawJSON, { status: !0 }) })) });
                    try {
                        // try parse to check data is actual json string
                        var jsonData = JSON.parse(t);
                        var sessionStorageData = JSON.stringify(jsonData, null, 4);
                        sessionStorage.setItem(jsonViewerSessionStorageKey, sessionStorageData);
                        this.props.changeJSON(jsonData)
                    } catch (t) { this.setState({ errors: Object.assign({}, this.state.errors, Object.assign({}, this.state.errors, { jsonParseFailed: Object.assign({}, this.state.errors.jsonParseFailed, { status: !0 }) })) }) }
                }
            }, { key: "resetErrors", value: function () { this.setState({ errors: Object.assign({}, this.state.errors, Object.assign({}, this.state.errors, { jsonParseFailed: Object.assign({}, this.state.errors.jsonParseFailed, { status: !1 }), rawJSON: Object.assign({}, this.state.errors.rawJSON, { status: !1 }) })) }) } }, {
                key: "render", value: function () {
                    return c.default.createElement("div", { className: "json-input-section" }, c.default.createElement("div", { className: "json-logo" }, c.default.createElement("img", { src: faviconUrl, style: { marginTop: "8px", width: "60px" } }, null)), c.default.createElement("h1", null, "Json Viewer"), this.state.errors.jsonParseFailed.status && c.default.createElement("div", { className: "json-input-error-msg" }, this.state.errors.jsonParseFailed.message), this.state.errors.rawJSON.status && c.default.createElement("div", { className: "json-input-error-msg" }, this.state.errors.rawJSON.message), c.default.createElement("div", { className: "form-input" }, c.default.createElement("textarea", { ref: "rawJSON", defaultValue: this.state.json, className: "json-input" })), c.default.createElement("div", { className: "form-input save-btn-area" }, c.default.createElement("button", {
                        type: "button", className: "btn btn-big btn-white", onClick: this.parseJSON
                            .bind(this)
                    }, "Parse JSON")))
                }
            }]), e
        }(s.Component));
    e.default = l
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function i(t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }

    function a(t, e) {
        if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e);
        t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var u = function () {
        function t(t, e) {
            for (var n = 0; n < e.length; n++) {
                var r = e[n];
                r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(t, r.key, r)
            }
        }
        return function (e, n, r) { return n && t(e.prototype, n), r && t(e, r), e }
    }(),
        s = n(23),
        c = r(s);
    n(66);
    var l = function (t) {
        function e(t) { o(this, e); var n = i(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, t)); return n.state = { selectedPan: t.selectedTab }, n }
        return a(e, t), u(e, [{ key: "setActive", value: function (t) { this.setState({ selectedPan: t }), this.props.changeTabSelection(t) } }, { key: "componentWillMount", value: function () { this.prepareComponentState(this.props) } }, { key: "componentWillReceiveProps", value: function (t) { this.prepareComponentState(t) } }, { key: "prepareComponentState", value: function (t) { this.setState({ selectedPan: t.selectedTab }) } }, { key: "render", value: function () { return c.default.createElement("div", { className: "action-area" }, c.default.createElement("ul", { className: "menus" }, c.default.createElement("li", { className: "tree" === this.state.selectedPan ? "active" : "" }, c.default.createElement("a", { href: "#", onClick: this.setActive.bind(this, "tree") }, "Tree"), " "), c.default.createElement("li", { className: "chart" === this.state.selectedPan ? "active" : "" }, c.default.createElement("a", { href: "#", onClick: this.setActive.bind(this, "chart") }, "Chart"), " "), c.default.createElement("li", { className: "jsonInput" === this.state.selectedPan ? "active" : "" }, c.default.createElement("a", { href: "#", onClick: this.setActive.bind(this, "jsonInput") }, "Input"), " "))) } }]), e
    }(s.Component);
    e.default = l
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function i(t, e) { if (!t) throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); return !e || "object" != typeof e && "function" != typeof e ? t : e }

    function a(t, e) {
        if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e);
        t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
    }
    Object.defineProperty(e, "__esModule", { value: !0 });
    var u = function () {
        function t(t, e) {
            for (var n = 0; n < e.length; n++) {
                var r = e[n];
                r.enumerable = r.enumerable || !1, r.configurable = !0, "value" in r && (r.writable = !0), Object.defineProperty(t, r.key, r)
            }
        }
        return function (e, n, r) { return n && t(e.prototype, n), r && t(e, r), e }
    }(),
        s = n(23),
        c = r(s),
        l = n(45),
        f = (r(l), n(64));
    n(134);
    var p = n(147),
        h = function (t) {
            function e(t) { o(this, e); var n = i(this, (e.__proto__ || Object.getPrototypeOf(e)).call(this, t)); return n.state = { top: 0, showCopier: !1, actualPath: null, value: null }, n.changeCopyIconLocation = n.changeCopyIconLocation.bind(n), n.toggleSection = n.toggleSection.bind(n), n }
            return a(e, t), u(e, [{
                key: "copy",
                value: function (t, e) {
                    t.preventDefault();
                    var n = void 0;
                    n = "path" === e ? this.state.actualPath : this.state.value;
                    var r = void 0,
                        o = void 0,
                        i = void 0;
                    r = document.createElement("span"), o = document.createRange(), r.innerText = n, document.body.appendChild(r), o.selectNodeContents(r), i = window.getSelection(), i.removeAllRanges(), i.addRange(o), document.execCommand("Copy"), document.body.removeChild(r)
                }
            }, { key: "changeCopyIconLocation", value: function (t) { var e = this; return this.findPath(e, t), e.setState({ top: p(t.target).offset().top, showCopier: !0 }), !1 } }, {
                key: "getArrayIndex",
                value: function (t) {
                    var e = t.lastIndexOf("["),
                        n = t.lastIndexOf("]");
                    return e > -1 ? t.substring(e + 1, n) : t
                }
            }, { key: "createValidPath", value: function (t) { var e = ""; return t.forEach(function (t, n) { e = 0 === n ? e.concat(t) : t.indexOf("-") > -1 ? e + "['" + t + "']" : isNaN(t) === !1 ? e + "[" + t + "]" : e.concat(".").concat(t) }), e } }, {
                key: "findPath",
                value: function (t, e) {
                    var n = [],
                        r = p(e.target).parents("li").first().text(),
                        o = r.indexOf(":"),
                        i = r.substring(o + 1),
                        a = p(e.target).parentsUntil("#json-rb");
                    p(a).each(function (e, r) {
                        if ("LI" == p(r).get(0).tagName && "UL" == p(r).parent()[0].tagName) {
                            var o = p(r).find("span.property").eq(0).text();
                            n.push(t.getArrayIndex(o.replace(/\"+/g, "")))
                        }
                        if ("LI" == p(r).get(0).tagName && "OL" == p(r).parent()[0].tagName) {
                            var i = p(r).parent("OL").parent("li").find("span.property").eq(0).text() + "[" + p(r).index() + "]";
                            n.push(t.getArrayIndex(i.replace(/\"+/g, "")))
                        }
                    }), "," === i[i.length - 1] && (i = i.substring(0, i.length - 1)), t.setState({ actualPath: t.createValidPath(n.reverse()), value: i })
                }
            }, {
                key: "toggleSection",
                value: function (t) {
                    t.preventDefault();
                    var e = p(t.target).toggleClass("collapsed").siblings("ul.json-dict, ol.json-array");
                    if (e.toggle(), e.is(":visible")) e.siblings(".json-placeholder").remove();
                    else {
                        var n = e.children("li").length,
                            r = n + (n > 1 ? " items" : " item");
                        e.after('<a href class="json-placeholder">' + r + "</a>")
                    }
                }
            }, { key: "componentDidMount", value: function () { window.json = this.props.data, this.$node = p(this.refs.jsonRenderer), p && ((0, f.initPlugin)(this.$node, p, this.props.data, { collapsed: !1, withQuotes: !0 }), p(document).on("click", "span.property", this.changeCopyIconLocation), p(document).on("click", "a.json-toggle", this.toggleSection)) } }, { key: "componentWillUnmount", value: function () { p(document).off("click", "span.property", this.changeCopyIconLocation), p(document).off("click", "a.json-toggle", this.toggleSection) } }, { key: "render", value: function () { return window.json = this.props.data, c.default.createElement("div", null, c.default.createElement("a", { className: "copier", style: { top: this.state.top, display: this.state.showCopier ? "block" : "none" } }, c.default.createElement("ul", { className: "copyMenu" }, c.default.createElement("li", null, c.default.createElement("a", { onClick: this.copy.bind(this, event, "path") }, "Copy path")), c.default.createElement("li", null, c.default.createElement("a", { onClick: this.copy.bind(this, event, "value") }, "Copy Value")))), c.default.createElement("pre", { ref: "jsonRenderer", id: "json-rb" })) } }]), e
        }(s.Component);
    e.default = h
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }
    var o = n(23),
        i = r(o),
        a = n(45),
        u = r(a),
        s = n(116),
        c = r(s);
    n(133), "undefined" != typeof window && (window.React = i.default), window.addEventListener("DOMContentLoaded", function () {
        var t = document.body.textContent;
        try {
            var e = JSON.parse(t);
            window.json = e;
            var n = document.createElement("div");
            n.setAttribute("id", "rbrahul-awesome-json"), document.body.innerHTML = "", document.body.appendChild(n), u.default.render(i.default.createElement(c.default, { json: e }), document.getElementById("rbrahul-awesome-json"))
        } catch (t) { }
    }, !1)
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }
    e.__esModule = !0, e.tree = void 0;
    var o = n(124),
        i = r(o);
    e.tree = i.default
},
function (t, e, n) {
    "use strict";

    function r(t, e) { if (t instanceof Array) { var n = void 0; return n = e ? t.sort() : t } if (t && "object" === ("undefined" == typeof t ? "undefined" : i(t))) { var o = function () { var e = {}; return Object.keys(t).sort().forEach(function (n) { return e[n] = r(t[n]) }), { v: e } }(); if ("object" === ("undefined" == typeof o ? "undefined" : i(o))) return o.v } return t }

    function o(t) { return JSON.stringify(r(t, !0), void 0, 2) }
    e.__esModule = !0;
    var i = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) { return typeof t } : function (t) { return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t };
    e.default = o;
    n(16)
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }
    e.__esModule = !0;
    var o = Object.assign || function (t) { for (var e = 1; e < arguments.length; e++) { var n = arguments[e]; for (var r in n) Object.prototype.hasOwnProperty.call(n, r) && (t[r] = n[r]) } return t };
    e.default = function (t) {
        var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {},
            n = (0, f.default)(v, e),
            r = n.id,
            i = n.style,
            s = n.size,
            l = n.aspectRatio,
            h = n.initialZoom,
            g = n.margin,
            y = n.isSorted,
            m = n.widthBetweenNodesCoeff,
            b = n.heightBetweenNodesCoeff,
            x = n.transitionDuration,
            w = n.state,
            _ = n.rootKeyName,
            C = n.pushMethod,
            E = n.tree,
            M = n.tooltipOptions,
            k = n.onClickText,
            T = s - g.left - g.right,
            S = s * l - g.top - g.bottom,
            N = s,
            O = s * l,
            A = { id: r, preserveAspectRatio: "xMinYMin slice" };
        i.width || (A.width = N), i.width && i.height || (A.viewBox = "0 0 " + N + " " + O);
        var P = a.default.select(t),
            j = a.default.behavior.zoom().scaleExtent([.1, 3]).scale(h),
            D = P.append("svg").attr(A).style(o({ cursor: "-webkit-grab" }, i)).call(j.on("zoom", function () {
                var t = a.default.event,
                    e = t.translate,
                    n = t.scale;
                D.attr("transform", "translate(" + e + ")scale(" + n + ")")
            })).append("g").attr({ transform: "translate(" + (g.left + i.node.radius) + ", " + g.top + ") scale(" + h + ")" }),
            I = a.default.layout.tree().size([T, S]),
            R = void 0;
        return y && I.sort(function (t, e) { return e.name.toLowerCase() < t.name.toLowerCase() ? 1 : -1 }),
            function () {
                function t(e) {
                    var u = a.default.svg.diagonal().projection(function (t) { return [t.y, t.x] }),
                        s = Math.max.apply(Math, (0, p.getNodeGroupByDepthCount)(R));
                    I = I.size([25 * s * b, T]);
                    var c = I.nodes(R),
                        l = I.links(c);
                    c.forEach(function (t) { return t.y = t.depth * (7 * r * m) });
                    var f = D.selectAll("g.node").property("__oldData__", function (t) { return t }).data(c, function (t) { return t.id || (t.id = ++n) }),
                        h = f.enter().append("g").attr({ class: "node", transform: function (t) { return "translate(" + e.y0 + "," + e.x0 + ")" } }).style({ fill: i.text.colors.default, cursor: "pointer" }).on({ mouseover: function (t, e) { a.default.select(this).style({ fill: i.text.colors.hover }) }, mouseout: function (t, e) { a.default.select(this).style({ fill: i.text.colors.default }) } });
                    M.disabled || h.call((0, d.default)(a.default, "tooltip", o({}, M, { root: P })).text(function (t, e) { return (0, p.getTooltipString)(t, e, M) }).style(M.style)), h.append("circle").attr({ class: "nodeCircle" }).on({ click: function (e) { a.default.event.defaultPrevented || t((0, p.toggleChildren)(e)) } }), h.append("text").attr({ class: "nodeText", dy: ".35em" }).style({ "fill-opacity": 0 }).text(function (t) { return t.name }).on({ click: k }), f.select("text").attr({ x: function (t) { return t.children || t._children ? -(i.node.radius + 10) : i.node.radius + 10 }, "text-anchor": function (t) { return t.children || t._children ? "end" : "start" } }).text(function (t) { return t.name }), f.select("circle.nodeCircle").attr({ r: i.node.radius }).style({ stroke: "black", "stroke-width": "1.5px", fill: function (t) { return t._children ? i.node.colors.collapsed : t.children ? i.node.colors.parent : i.node.colors.default } });
                    var v = f.transition().duration(x).attr({ transform: function (t) { return "translate(" + t.y + "," + t.x + ")" } });
                    v.select("text").style("fill-opacity", 1), v.select("circle").attr("r", 7), v.filter(function (t) { return !this.__oldData__ || t.value !== this.__oldData__.value }).style("fill-opacity", "0.3").transition().duration(100).style("fill-opacity", "1");
                    var g = f.exit().transition().duration(x).attr({ transform: function (t) { return "translate(" + e.y + "," + e.x + ")" } }).remove();
                    g.select("circle").attr("r", 0), g.select("text").style("fill-opacity", 0);
                    var y = D.selectAll("path.link").data(l, function (t) { return t.target.id });
                    y.enter().insert("path", "g").attr({ class: "link", d: function (t) { var n = { x: e.x0, y: e.y0 }; return u({ source: n, target: n }) } }).style(i.link), y.transition().duration(x).attr({ d: u }), y.exit().transition().duration(x).attr({ d: function (t) { var n = { x: e.x, y: e.y }; return u({ source: n, target: n }) } }).remove(), f.property("__oldData__", null), c.forEach(function (t) { t.x0 = t.x, t.y0 = t.y })
                }
                var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : E || w;
                R = E ? e : (0, c.default)(e, { key: _, pushMethod: C }), !(0, u.isEmpty)(R) && R.name || (R = { name: "error", message: "Please provide a state map or a tree structure" });
                var n = 0,
                    r = 0;
                (0, p.visit)(R, function (t) { return r = Math.max(t.name.length, r) }, function (t) { return t.children && t.children.length > 0 ? t.children : null }), R.x0 = S / 2, R.y0 = 0, t(R)
            }
    };
    var i = n(127),
        a = r(i),
        u = n(16),
        s = n(148),
        c = r(s),
        l = n(131),
        f = r(l),
        p = n(125),
        h = n(128),
        d = r(h),
        v = { state: void 0, rootKeyName: "state", pushMethod: "push", tree: void 0, id: "d3svg", style: { node: { colors: { default: "#ccc", collapsed: "lightsteelblue", parent: "white" }, radius: 5 }, text: { colors: { default: "black", hover: "skyblue" } }, link: { stroke: "#000", fill: "none" } }, size: 500, aspectRatio: 1, initialZoom: 1, margin: { top: 10, right: 10, bottom: 10, left: 50 }, isSorted: !1, heightBetweenNodesCoeff: 2, widthBetweenNodesCoeff: 1, transitionDuration: 750, onClickText: function () { }, tooltipOptions: { disabled: !1, left: void 0, right: void 0, offset: { left: 0, top: 0 }, style: void 0 } }
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t) { t.children && (t._children = t.children, t._children.forEach(o), t.children = null) }

    function i(t) { t._children && (t.children = t._children, t.children.forEach(i), t._children = null) }

    function a(t) { return t.children ? (t._children = t.children, t.children = null) : t._children && (t.children = t._children, t._children = null), t }

    function u(t, e, n) {
        if (t) {
            e(t);
            var r = n(t);
            if (r)
                for (var o = r.length, i = 0; i < o; i++) u(r[i], e, n)
        }
    }

    function s(t) {
        var e = [1],
            n = function t(n) { var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0; return n.children && 0 !== n.children.length ? (e.length <= r + 1 && e.push(0), e[r + 1] += n.children.length, void n.children.forEach(function (e) { t(e, r + 1) })) : 0 };
        return n(t), e
    }

    function c(t, e, n) {
        var r = n.indentationSize,
            o = void 0 === r ? 4 : r;
        if (!(0, l.is)(Object, t)) return "";
        var i = (0, l.join)("&nbsp;&nbsp;"),
            a = (0, l.replace)(/\n/g, "<br/>"),
            u = (0, l.replace)(/\s{2}/g, i(new Array(o))),
            s = (0, l.pipe)(p.default, a, u),
            c = t.children || t._children;
        return "undefined" != typeof t.value ? s(t.value) : "undefined" != typeof t.object ? s(t.object) : c && c.length ? "childrenCount: " + c.length : "empty"
    }
    e.__esModule = !0, e.collapseChildren = o, e.expandChildren = i, e.toggleChildren = a, e.visit = u, e.getNodeGroupByDepthCount = s, e.getTooltipString = c;
    var l = n(16),
        f = n(123),
        p = r(f)
},
function (t, e, n) {
    "use strict";

    function r(t) {
        if (t && t.__esModule) return t;
        var e = {};
        if (null != t)
            for (var n in t) Object.prototype.hasOwnProperty.call(t, n) && (e[n] = t[n]);
        return e.default = t, e
    }
    e.__esModule = !0, e.tree = void 0;
    var o = n(122);
    Object.defineProperty(e, "tree", { enumerable: !0, get: function () { return o.tree } });
    var i = r(o);
    e.default = i
},
function (t, e, n) {
    var r, o;
    ! function () {
        function i(t) { return t && (t.ownerDocument || t.document || t).documentElement }

        function a(t) { return t && (t.ownerDocument && t.ownerDocument.defaultView || t.document && t || t.defaultView) }

        function u(t, e) { return t < e ? -1 : t > e ? 1 : t >= e ? 0 : NaN }

        function s(t) { return null === t ? NaN : +t }

        function c(t) { return !isNaN(t) }

        function l(t) {
            return {
                left: function (e, n, r, o) {
                    for (arguments.length < 3 && (r = 0), arguments.length < 4 && (o = e.length); r < o;) {
                        var i = r + o >>> 1;
                        t(e[i], n) < 0 ? r = i + 1 : o = i
                    }
                    return r
                },
                right: function (e, n, r, o) {
                    for (arguments.length < 3 && (r = 0), arguments.length < 4 && (o = e.length); r < o;) {
                        var i = r + o >>> 1;
                        t(e[i], n) > 0 ? o = i : r = i + 1
                    }
                    return r
                }
            }
        }

        function f(t) { return t.length }

        function p(t) { for (var e = 1; t * e % 1;) e *= 10; return e }

        function h(t, e) { for (var n in e) Object.defineProperty(t.prototype, n, { value: e[n], enumerable: !1 }) }

        function d() { this._ = Object.create(null) }

        function v(t) { return (t += "") === Ea || t[0] === Ma ? Ma + t : t }

        function g(t) { return (t += "")[0] === Ma ? t.slice(1) : t }

        function y(t) { return v(t) in this._ }

        function m(t) { return (t = v(t)) in this._ && delete this._[t] }

        function b() { var t = []; for (var e in this._) t.push(g(e)); return t }

        function x() { var t = 0; for (var e in this._)++t; return t }

        function w() { for (var t in this._) return !1; return !0 }

        function _() { this._ = Object.create(null) }

        function C(t) { return t }

        function E(t, e, n) { return function () { var r = n.apply(e, arguments); return r === e ? t : r } }

        function M(t, e) {
            if (e in t) return e;
            e = e.charAt(0).toUpperCase() + e.slice(1);
            for (var n = 0, r = ka.length; n < r; ++n) { var o = ka[n] + e; if (o in t) return o }
        }

        function k() { }

        function T() { }

        function S(t) {
            function e() { for (var e, r = n, o = -1, i = r.length; ++o < i;)(e = r[o].on) && e.apply(this, arguments); return t }
            var n = [],
                r = new d;
            return e.on = function (e, o) { var i, a = r.get(e); return arguments.length < 2 ? a && a.on : (a && (a.on = null, n = n.slice(0, i = n.indexOf(a)).concat(n.slice(i + 1)), r.remove(e)), o && n.push(r.set(e, { on: o })), t) }, e
        }

        function N() {
            ha.event.preventDefault();
        }

        function O() { for (var t, e = ha.event; t = e.sourceEvent;) e = t; return e }

        function A(t) {
            for (var e = new T, n = 0, r = arguments.length; ++n < r;) e[arguments[n]] = S(e);
            return e.of = function (n, r) {
                return function (o) {
                    try {
                        var i = o.sourceEvent = ha.event;
                        o.target = t, ha.event = o, e[o.type].apply(n, r)
                    } finally { ha.event = i }
                }
            }, e
        }

        function P(t) { return Sa(t, Pa), t }

        function j(t) { return "function" == typeof t ? t : function () { return Na(t, this) } }

        function D(t) { return "function" == typeof t ? t : function () { return Oa(t, this) } }

        function I(t, e) {
            function n() { this.removeAttribute(t) }

            function r() { this.removeAttributeNS(t.space, t.local) }

            function o() { this.setAttribute(t, e) }

            function i() { this.setAttributeNS(t.space, t.local, e) }

            function a() {
                var n = e.apply(this, arguments);
                null == n ? this.removeAttribute(t) : this.setAttribute(t, n)
            }

            function u() {
                var n = e.apply(this, arguments);
                null == n ? this.removeAttributeNS(t.space, t.local) : this.setAttributeNS(t.space, t.local, n)
            }
            return t = ha.ns.qualify(t), null == e ? t.local ? r : n : "function" == typeof e ? t.local ? u : a : t.local ? i : o
        }

        function R(t) { return t.trim().replace(/\s+/g, " ") }

        function L(t) { return new RegExp("(?:^|\\s+)" + ha.requote(t) + "(?:\\s+|$)", "g") }

        function F(t) { return (t + "").trim().split(/^|\s+/) }

        function U(t, e) {
            function n() { for (var n = -1; ++n < o;) t[n](this, e) }

            function r() { for (var n = -1, r = e.apply(this, arguments); ++n < o;) t[n](this, r) }
            t = F(t).map(q);
            var o = t.length;
            return "function" == typeof e ? r : n
        }

        function q(t) {
            var e = L(t);
            return function (n, r) {
                if (o = n.classList) return r ? o.add(t) : o.remove(t);
                var o = n.getAttribute("class") || "";
                r ? (e.lastIndex = 0, e.test(o) || n.setAttribute("class", R(o + " " + t))) : n.setAttribute("class", R(o.replace(e, " ")))
            }
        }

        function H(t, e, n) {
            function r() { this.style.removeProperty(t) }

            function o() { this.style.setProperty(t, e, n) }

            function i() {
                var r = e.apply(this, arguments);
                null == r ? this.style.removeProperty(t) : this.style.setProperty(t, r, n)
            }
            return null == e ? r : "function" == typeof e ? i : o
        }

        function B(t, e) {
            function n() { delete this[t] }

            function r() { this[t] = e }

            function o() {
                var n = e.apply(this, arguments);
                null == n ? delete this[t] : this[t] = n
            }
            return null == e ? n : "function" == typeof e ? o : r
        }

        function W(t) {
            function e() {
                var e = this.ownerDocument,
                    n = this.namespaceURI;
                return n === ja && e.documentElement.namespaceURI === ja ? e.createElement(t) : e.createElementNS(n, t)
            }

            function n() { return this.ownerDocument.createElementNS(t.space, t.local) }
            return "function" == typeof t ? t : (t = ha.ns.qualify(t)).local ? n : e
        }

        function z() {
            var t = this.parentNode;
            t && t.removeChild(this)
        }

        function V(t) { return { __data__: t } }

        function Y(t) { return function () { return Aa(this, t) } }

        function $(t) {
            return arguments.length || (t = u),
                function (e, n) { return e && n ? t(e.__data__, n.__data__) : !e - !n }
        }

        function K(t, e) {
            for (var n = 0, r = t.length; n < r; n++)
                for (var o, i = t[n], a = 0, u = i.length; a < u; a++)(o = i[a]) && e(o, a, n);
            return t
        }

        function X(t) { return Sa(t, Ia), t }

        function G(t) {
            var e, n;
            return function (r, o, i) {
                var a, u = t[i].update,
                    s = u.length;
                for (i != n && (n = i, e = 0), o >= e && (e = o + 1); !(a = u[e]) && ++e < s;);
                return a
            }
        }

        function J(t, e, n) {
            function r() {
                var e = this[a];
                e && (this.removeEventListener(t, e, e.$), delete this[a])
            }

            function o() {
                var o = s(e, va(arguments));
                r.call(this), this.addEventListener(t, this[a] = o, o.$ = n), o._ = e
            }

            function i() {
                var e, n = new RegExp("^__on([^.]+)" + ha.requote(t) + "$");
                for (var r in this)
                    if (e = r.match(n)) {
                        var o = this[r];
                        this.removeEventListener(e[1], o, o.$), delete this[r]
                    }
            }
            var a = "__on" + t,
                u = t.indexOf("."),
                s = Q;
            u > 0 && (t = t.slice(0, u));
            var c = Ra.get(t);
            return c && (t = c, s = Z), u ? e ? o : r : e ? k : i
        }

        function Q(t, e) {
            return function (n) {
                var r = ha.event;
                ha.event = n, e[0] = this.__data__;
                try { t.apply(this, e) } finally { ha.event = r }
            }
        }

        function Z(t, e) {
            var n = Q(t, e);
            return function (t) {
                var e = this,
                    r = t.relatedTarget;
                r && (r === e || 8 & r.compareDocumentPosition(e)) || n.call(e, t)
            }
        }

        function tt(t) {
            var e = ".dragsuppress-" + ++Fa,
                n = "click" + e,
                r = ha.select(a(t)).on("touchmove" + e, N).on("dragstart" + e, N).on("selectstart" + e, N);
            if (null == La && (La = !("onselectstart" in t) && M(t.style, "userSelect")), La) {
                var o = i(t).style,
                    u = o[La];
                o[La] = "none"
            }
            return function (t) {
                if (r.on(e, null), La && (o[La] = u), t) {
                    var i = function () { r.on(n, null) };
                    r.on(n, function () { N(), i() }, !0), setTimeout(i, 0)
                }
            }
        }

        function et(t, e) {
            e.changedTouches && (e = e.changedTouches[0]);
            var n = t.ownerSVGElement || t;
            if (n.createSVGPoint) {
                var r = n.createSVGPoint();
                if (Ua < 0) {
                    var o = a(t);
                    if (o.scrollX || o.scrollY) {
                        n = ha.select("body").append("svg").style({ position: "absolute", top: 0, left: 0, margin: 0, padding: 0, border: "none" }, "important");
                        var i = n[0][0].getScreenCTM();
                        Ua = !(i.f || i.e), n.remove()
                    }
                }
                return Ua ? (r.x = e.pageX, r.y = e.pageY) : (r.x = e.clientX, r.y = e.clientY), r = r.matrixTransform(t.getScreenCTM().inverse()), [r.x, r.y]
            }
            var u = t.getBoundingClientRect();
            return [e.clientX - u.left - t.clientLeft, e.clientY - u.top - t.clientTop]
        }

        function nt() { return ha.event.changedTouches[0].identifier }

        function rt(t) { return t > 0 ? 1 : t < 0 ? -1 : 0 }

        function ot(t, e, n) { return (e[0] - t[0]) * (n[1] - t[1]) - (e[1] - t[1]) * (n[0] - t[0]) }

        function it(t) { return t > 1 ? 0 : t < -1 ? Ba : Math.acos(t) }

        function at(t) { return t > 1 ? Va : t < -1 ? -Va : Math.asin(t) }

        function ut(t) { return ((t = Math.exp(t)) - 1 / t) / 2 }

        function st(t) { return ((t = Math.exp(t)) + 1 / t) / 2 }

        function ct(t) { return ((t = Math.exp(2 * t)) - 1) / (t + 1) }

        function lt(t) { return (t = Math.sin(t / 2)) * t }

        function ft() { }

        function pt(t, e, n) { return this instanceof pt ? (this.h = +t, this.s = +e, void (this.l = +n)) : arguments.length < 2 ? t instanceof pt ? new pt(t.h, t.s, t.l) : kt("" + t, Tt, pt) : new pt(t, e, n) }

        function ht(t, e, n) {
            function r(t) { return t > 360 ? t -= 360 : t < 0 && (t += 360), t < 60 ? i + (a - i) * t / 60 : t < 180 ? a : t < 240 ? i + (a - i) * (240 - t) / 60 : i }

            function o(t) { return Math.round(255 * r(t)) }
            var i, a;
            return t = isNaN(t) ? 0 : (t %= 360) < 0 ? t + 360 : t, e = isNaN(e) ? 0 : e < 0 ? 0 : e > 1 ? 1 : e, n = n < 0 ? 0 : n > 1 ? 1 : n, a = n <= .5 ? n * (1 + e) : n + e - n * e, i = 2 * n - a, new _t(o(t + 120), o(t), o(t - 120))
        }

        function dt(t, e, n) { return this instanceof dt ? (this.h = +t, this.c = +e, void (this.l = +n)) : arguments.length < 2 ? t instanceof dt ? new dt(t.h, t.c, t.l) : t instanceof gt ? mt(t.l, t.a, t.b) : mt((t = St((t = ha.rgb(t)).r, t.g, t.b)).l, t.a, t.b) : new dt(t, e, n) }

        function vt(t, e, n) { return isNaN(t) && (t = 0), isNaN(e) && (e = 0), new gt(n, Math.cos(t *= Ya) * e, Math.sin(t) * e) }

        function gt(t, e, n) { return this instanceof gt ? (this.l = +t, this.a = +e, void (this.b = +n)) : arguments.length < 2 ? t instanceof gt ? new gt(t.l, t.a, t.b) : t instanceof dt ? vt(t.h, t.c, t.l) : St((t = _t(t)).r, t.g, t.b) : new gt(t, e, n) }

        function yt(t, e, n) {
            var r = (t + 16) / 116,
                o = r + e / 500,
                i = r - n / 200;
            return o = bt(o) * ru, r = bt(r) * ou, i = bt(i) * iu, new _t(wt(3.2404542 * o - 1.5371385 * r - .4985314 * i), wt(-.969266 * o + 1.8760108 * r + .041556 * i), wt(.0556434 * o - .2040259 * r + 1.0572252 * i))
        }

        function mt(t, e, n) { return t > 0 ? new dt(Math.atan2(n, e) * $a, Math.sqrt(e * e + n * n), t) : new dt(NaN, NaN, t) }

        function bt(t) { return t > .206893034 ? t * t * t : (t - 4 / 29) / 7.787037 }

        function xt(t) { return t > .008856 ? Math.pow(t, 1 / 3) : 7.787037 * t + 4 / 29 }

        function wt(t) { return Math.round(255 * (t <= .00304 ? 12.92 * t : 1.055 * Math.pow(t, 1 / 2.4) - .055)) }

        function _t(t, e, n) { return this instanceof _t ? (this.r = ~~t, this.g = ~~e, void (this.b = ~~n)) : arguments.length < 2 ? t instanceof _t ? new _t(t.r, t.g, t.b) : kt("" + t, _t, ht) : new _t(t, e, n) }

        function Ct(t) { return new _t(t >> 16, t >> 8 & 255, 255 & t) }

        function Et(t) { return Ct(t) + "" }

        function Mt(t) { return t < 16 ? "0" + Math.max(0, t).toString(16) : Math.min(255, t).toString(16) }

        function kt(t, e, n) {
            var r, o, i, a = 0,
                u = 0,
                s = 0;
            if (r = /([a-z]+)\((.*)\)/.exec(t = t.toLowerCase())) switch (o = r[2].split(","), r[1]) {
                case "hsl":
                    return n(parseFloat(o[0]), parseFloat(o[1]) / 100, parseFloat(o[2]) / 100);
                case "rgb":
                    return e(Ot(o[0]), Ot(o[1]), Ot(o[2]))
            }
            return (i = su.get(t)) ? e(i.r, i.g, i.b) : (null == t || "#" !== t.charAt(0) || isNaN(i = parseInt(t.slice(1), 16)) || (4 === t.length ? (a = (3840 & i) >> 4, a |= a >> 4, u = 240 & i, u |= u >> 4, s = 15 & i, s |= s << 4) : 7 === t.length && (a = (16711680 & i) >> 16, u = (65280 & i) >> 8, s = 255 & i)), e(a, u, s))
        }

        function Tt(t, e, n) {
            var r, o, i = Math.min(t /= 255, e /= 255, n /= 255),
                a = Math.max(t, e, n),
                u = a - i,
                s = (a + i) / 2;
            return u ? (o = s < .5 ? u / (a + i) : u / (2 - a - i), r = t == a ? (e - n) / u + (e < n ? 6 : 0) : e == a ? (n - t) / u + 2 : (t - e) / u + 4, r *= 60) : (r = NaN, o = s > 0 && s < 1 ? 0 : r), new pt(r, o, s)
        }

        function St(t, e, n) {
            t = Nt(t), e = Nt(e), n = Nt(n);
            var r = xt((.4124564 * t + .3575761 * e + .1804375 * n) / ru),
                o = xt((.2126729 * t + .7151522 * e + .072175 * n) / ou),
                i = xt((.0193339 * t + .119192 * e + .9503041 * n) / iu);
            return gt(116 * o - 16, 500 * (r - o), 200 * (o - i))
        }

        function Nt(t) { return (t /= 255) <= .04045 ? t / 12.92 : Math.pow((t + .055) / 1.055, 2.4) }

        function Ot(t) { var e = parseFloat(t); return "%" === t.charAt(t.length - 1) ? Math.round(2.55 * e) : e }

        function At(t) { return "function" == typeof t ? t : function () { return t } }

        function Pt(t) { return function (e, n, r) { return 2 === arguments.length && "function" == typeof n && (r = n, n = null), jt(e, n, t, r) } }

        function jt(t, e, n, r) {
            function o() {
                var t, e = s.status;
                if (!e && It(s) || e >= 200 && e < 300 || 304 === e) {
                    try { t = n.call(i, s) } catch (t) { return void a.error.call(i, t) }
                    a.load.call(i, t)
                } else a.error.call(i, s)
            }
            var i = {},
                a = ha.dispatch("beforesend", "progress", "load", "error"),
                u = {},
                s = new XMLHttpRequest,
                c = null;
            return !this.XDomainRequest || "withCredentials" in s || !/^(http(s)?:)?\/\//.test(t) || (s = new XDomainRequest), "onload" in s ? s.onload = s.onerror = o : s.onreadystatechange = function () { s.readyState > 3 && o() }, s.onprogress = function (t) {
                var e = ha.event;
                ha.event = t;
                try { a.progress.call(i, s) } finally { ha.event = e }
            }, i.header = function (t, e) { return t = (t + "").toLowerCase(), arguments.length < 2 ? u[t] : (null == e ? delete u[t] : u[t] = e + "", i) }, i.mimeType = function (t) { return arguments.length ? (e = null == t ? null : t + "", i) : e }, i.responseType = function (t) { return arguments.length ? (c = t, i) : c }, i.response = function (t) { return n = t, i }, ["get", "post"].forEach(function (t) { i[t] = function () { return i.send.apply(i, [t].concat(va(arguments))) } }), i.send = function (n, r, o) {
                if (2 === arguments.length && "function" == typeof r && (o = r, r = null), s.open(n, t, !0), null == e || "accept" in u || (u.accept = e + ",*/*"), s.setRequestHeader)
                    for (var l in u) s.setRequestHeader(l, u[l]);
                return null != e && s.overrideMimeType && s.overrideMimeType(e), null != c && (s.responseType = c), null != o && i.on("error", o).on("load", function (t) { o(null, t) }), a.beforesend.call(i, s), s.send(null == r ? null : r), i
            }, i.abort = function () { return s.abort(), i }, ha.rebind(i, a, "on"), null == r ? i : i.get(Dt(r))
        }

        function Dt(t) { return 1 === t.length ? function (e, n) { t(null == e ? n : null) } : t }

        function It(t) { var e = t.responseType; return e && "text" !== e ? t.response : t.responseText }

        function Rt(t, e, n) {
            var r = arguments.length;
            r < 2 && (e = 0), r < 3 && (n = Date.now());
            var o = n + e,
                i = { c: t, t: o, n: null };
            return lu ? lu.n = i : cu = i, lu = i, fu || (pu = clearTimeout(pu), fu = 1, hu(Lt)), i
        }

        function Lt() {
            var t = Ft(),
                e = Ut() - t;
            e > 24 ? (isFinite(e) && (clearTimeout(pu), pu = setTimeout(Lt, e)), fu = 0) : (fu = 1, hu(Lt))
        }

        function Ft() { for (var t = Date.now(), e = cu; e;) t >= e.t && e.c(t - e.t) && (e.c = null), e = e.n; return t }

        function Ut() { for (var t, e = cu, n = 1 / 0; e;) e.c ? (e.t < n && (n = e.t), e = (t = e).n) : e = t ? t.n = e.n : cu = e.n; return lu = t, n }

        function qt(t, e) { return e - (t ? Math.ceil(Math.log(t) / Math.LN10) : 1) }

        function Ht(t, e) { var n = Math.pow(10, 3 * Ca(8 - e)); return { scale: e > 8 ? function (t) { return t / n } : function (t) { return t * n }, symbol: t } }

        function Bt(t) {
            var e = t.decimal,
                n = t.thousands,
                r = t.grouping,
                o = t.currency,
                i = r && n ? function (t, e) { for (var o = t.length, i = [], a = 0, u = r[0], s = 0; o > 0 && u > 0 && (s + u + 1 > e && (u = Math.max(1, e - s)), i.push(t.substring(o -= u, o + u)), !((s += u + 1) > e));) u = r[a = (a + 1) % r.length]; return i.reverse().join(n) } : C;
            return function (t) {
                var n = vu.exec(t),
                    r = n[1] || " ",
                    a = n[2] || ">",
                    u = n[3] || "-",
                    s = n[4] || "",
                    c = n[5],
                    l = +n[6],
                    f = n[7],
                    p = n[8],
                    h = n[9],
                    d = 1,
                    v = "",
                    g = "",
                    y = !1,
                    m = !0;
                switch (p && (p = +p.substring(1)), (c || "0" === r && "=" === a) && (c = r = "0", a = "="), h) {
                    case "n":
                        f = !0, h = "g";
                        break;
                    case "%":
                        d = 100, g = "%", h = "f";
                        break;
                    case "p":
                        d = 100, g = "%", h = "r";
                        break;
                    case "b":
                    case "o":
                    case "x":
                    case "X":
                        "#" === s && (v = "0" + h.toLowerCase());
                    case "c":
                        m = !1;
                    case "d":
                        y = !0, p = 0;
                        break;
                    case "s":
                        d = -1, h = "r"
                }
                "$" === s && (v = o[0], g = o[1]), "r" != h || p || (h = "g"), null != p && ("g" == h ? p = Math.max(1, Math.min(21, p)) : "e" != h && "f" != h || (p = Math.max(0, Math.min(20, p)))), h = gu.get(h) || Wt;
                var b = c && f;
                return function (t) {
                    var n = g;
                    if (y && t % 1) return "";
                    var o = t < 0 || 0 === t && 1 / t < 0 ? (t = -t, "-") : "-" === u ? "" : u;
                    if (d < 0) {
                        var s = ha.formatPrefix(t, p);
                        t = s.scale(t), n = s.symbol + g
                    } else t *= d;
                    t = h(t, p);
                    var x, w, _ = t.lastIndexOf(".");
                    if (_ < 0) {
                        var C = m ? t.lastIndexOf("e") : -1;
                        C < 0 ? (x = t, w = "") : (x = t.substring(0, C), w = t.substring(C))
                    } else x = t.substring(0, _), w = e + t.substring(_ + 1);
                    !c && f && (x = i(x, 1 / 0));
                    var E = v.length + x.length + w.length + (b ? 0 : o.length),
                        M = E < l ? new Array(E = l - E + 1).join(r) : "";
                    return b && (x = i(M + x, M.length ? l - w.length : 1 / 0)), o += v, t = x + w, ("<" === a ? o + t + M : ">" === a ? M + o + t : "^" === a ? M.substring(0, E >>= 1) + o + t + M.substring(E) : o + (b ? t : M + t)) + n
                }
            }
        }

        function Wt(t) { return t + "" }

        function zt() { this._ = new Date(arguments.length > 1 ? Date.UTC.apply(this, arguments) : arguments[0]) }

        function Vt(t, e, n) {
            function r(e) {
                var n = t(e),
                    r = i(n, 1);
                return e - n < r - e ? n : r
            }

            function o(n) { return e(n = t(new mu(n - 1)), 1), n }

            function i(t, n) { return e(t = new mu(+t), n), t }

            function a(t, r, i) {
                var a = o(t),
                    u = [];
                if (i > 1)
                    for (; a < r;) n(a) % i || u.push(new Date(+a)), e(a, 1);
                else
                    for (; a < r;) u.push(new Date(+a)), e(a, 1);
                return u
            }

            function u(t, e, n) { try { mu = zt; var r = new zt; return r._ = t, a(r, e, n) } finally { mu = Date } }
            t.floor = t, t.round = r, t.ceil = o, t.offset = i, t.range = a;
            var s = t.utc = Yt(t);
            return s.floor = s, s.round = Yt(r), s.ceil = Yt(o), s.offset = Yt(i), s.range = u, t
        }

        function Yt(t) { return function (e, n) { try { mu = zt; var r = new zt; return r._ = e, t(r, n)._ } finally { mu = Date } } }

        function $t(t) {
            function e(t) {
                function e(e) { for (var n, o, i, a = [], u = -1, s = 0; ++u < r;) 37 === t.charCodeAt(u) && (a.push(t.slice(s, u)), null != (o = xu[n = t.charAt(++u)]) && (n = t.charAt(++u)), (i = S[n]) && (n = i(e, null == o ? "e" === n ? " " : "0" : o)), a.push(n), s = u + 1); return a.push(t.slice(s, u)), a.join("") }
                var r = t.length;
                return e.parse = function (e) {
                    var r = { y: 1900, m: 0, d: 1, H: 0, M: 0, S: 0, L: 0, Z: null },
                        o = n(r, t, e, 0);
                    if (o != e.length) return null;
                    "p" in r && (r.H = r.H % 12 + 12 * r.p);
                    var i = null != r.Z && mu !== zt,
                        a = new (i ? zt : mu);
                    return "j" in r ? a.setFullYear(r.y, 0, r.j) : "W" in r || "U" in r ? ("w" in r || (r.w = "W" in r ? 1 : 0), a.setFullYear(r.y, 0, 1), a.setFullYear(r.y, 0, "W" in r ? (r.w + 6) % 7 + 7 * r.W - (a.getDay() + 5) % 7 : r.w + 7 * r.U - (a.getDay() + 6) % 7)) : a.setFullYear(r.y, r.m, r.d), a.setHours(r.H + (r.Z / 100 | 0), r.M + r.Z % 100, r.S, r.L), i ? a._ : a
                }, e.toString = function () { return t }, e
            }

            function n(t, e, n, r) { for (var o, i, a, u = 0, s = e.length, c = n.length; u < s;) { if (r >= c) return -1; if (o = e.charCodeAt(u++), 37 === o) { if (a = e.charAt(u++), i = N[a in xu ? e.charAt(u++) : a], !i || (r = i(t, n, r)) < 0) return -1 } else if (o != n.charCodeAt(r++)) return -1 } return r }

            function r(t, e, n) { _.lastIndex = 0; var r = _.exec(e.slice(n)); return r ? (t.w = C.get(r[0].toLowerCase()), n + r[0].length) : -1 }

            function o(t, e, n) { x.lastIndex = 0; var r = x.exec(e.slice(n)); return r ? (t.w = w.get(r[0].toLowerCase()), n + r[0].length) : -1 }

            function i(t, e, n) { k.lastIndex = 0; var r = k.exec(e.slice(n)); return r ? (t.m = T.get(r[0].toLowerCase()), n + r[0].length) : -1 }

            function a(t, e, n) { E.lastIndex = 0; var r = E.exec(e.slice(n)); return r ? (t.m = M.get(r[0].toLowerCase()), n + r[0].length) : -1 }

            function u(t, e, r) { return n(t, S.c.toString(), e, r) }

            function s(t, e, r) { return n(t, S.x.toString(), e, r) }

            function c(t, e, r) { return n(t, S.X.toString(), e, r) }

            function l(t, e, n) { var r = b.get(e.slice(n, n += 2).toLowerCase()); return null == r ? -1 : (t.p = r, n) }
            var f = t.dateTime,
                p = t.date,
                h = t.time,
                d = t.periods,
                v = t.days,
                g = t.shortDays,
                y = t.months,
                m = t.shortMonths;
            e.utc = function (t) {
                function n(t) { try { mu = zt; var e = new mu; return e._ = t, r(e) } finally { mu = Date } }
                var r = e(t);
                return n.parse = function (t) { try { mu = zt; var e = r.parse(t); return e && e._ } finally { mu = Date } }, n.toString = r.toString, n
            }, e.multi = e.utc.multi = he;
            var b = ha.map(),
                x = Xt(v),
                w = Gt(v),
                _ = Xt(g),
                C = Gt(g),
                E = Xt(y),
                M = Gt(y),
                k = Xt(m),
                T = Gt(m);
            d.forEach(function (t, e) { b.set(t.toLowerCase(), e) });
            var S = { a: function (t) { return g[t.getDay()] }, A: function (t) { return v[t.getDay()] }, b: function (t) { return m[t.getMonth()] }, B: function (t) { return y[t.getMonth()] }, c: e(f), d: function (t, e) { return Kt(t.getDate(), e, 2) }, e: function (t, e) { return Kt(t.getDate(), e, 2) }, H: function (t, e) { return Kt(t.getHours(), e, 2) }, I: function (t, e) { return Kt(t.getHours() % 12 || 12, e, 2) }, j: function (t, e) { return Kt(1 + yu.dayOfYear(t), e, 3) }, L: function (t, e) { return Kt(t.getMilliseconds(), e, 3) }, m: function (t, e) { return Kt(t.getMonth() + 1, e, 2) }, M: function (t, e) { return Kt(t.getMinutes(), e, 2) }, p: function (t) { return d[+(t.getHours() >= 12)] }, S: function (t, e) { return Kt(t.getSeconds(), e, 2) }, U: function (t, e) { return Kt(yu.sundayOfYear(t), e, 2) }, w: function (t) { return t.getDay() }, W: function (t, e) { return Kt(yu.mondayOfYear(t), e, 2) }, x: e(p), X: e(h), y: function (t, e) { return Kt(t.getFullYear() % 100, e, 2) }, Y: function (t, e) { return Kt(t.getFullYear() % 1e4, e, 4) }, Z: fe, "%": function () { return "%" } },
                N = { a: r, A: o, b: i, B: a, c: u, d: ie, e: ie, H: ue, I: ue, j: ae, L: le, m: oe, M: se, p: l, S: ce, U: Qt, w: Jt, W: Zt, x: s, X: c, y: ee, Y: te, Z: ne, "%": pe };
            return e
        }

        function Kt(t, e, n) {
            var r = t < 0 ? "-" : "",
                o = (r ? -t : t) + "",
                i = o.length;
            return r + (i < n ? new Array(n - i + 1).join(e) + o : o)
        }

        function Xt(t) { return new RegExp("^(?:" + t.map(ha.requote).join("|") + ")", "i") }

        function Gt(t) { for (var e = new d, n = -1, r = t.length; ++n < r;) e.set(t[n].toLowerCase(), n); return e }

        function Jt(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 1)); return r ? (t.w = +r[0], n + r[0].length) : -1 }

        function Qt(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n)); return r ? (t.U = +r[0], n + r[0].length) : -1 }

        function Zt(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n)); return r ? (t.W = +r[0], n + r[0].length) : -1 }

        function te(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 4)); return r ? (t.y = +r[0], n + r[0].length) : -1 }

        function ee(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.y = re(+r[0]), n + r[0].length) : -1 }

        function ne(t, e, n) { return /^[+-]\d{4}$/.test(e = e.slice(n, n + 5)) ? (t.Z = -e, n + 5) : -1 }

        function re(t) { return t + (t > 68 ? 1900 : 2e3) }

        function oe(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.m = r[0] - 1, n + r[0].length) : -1 }

        function ie(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.d = +r[0], n + r[0].length) : -1 }

        function ae(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 3)); return r ? (t.j = +r[0], n + r[0].length) : -1 }

        function ue(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.H = +r[0], n + r[0].length) : -1 }

        function se(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.M = +r[0], n + r[0].length) : -1 }

        function ce(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 2)); return r ? (t.S = +r[0], n + r[0].length) : -1 }

        function le(t, e, n) { wu.lastIndex = 0; var r = wu.exec(e.slice(n, n + 3)); return r ? (t.L = +r[0], n + r[0].length) : -1 }

        function fe(t) {
            var e = t.getTimezoneOffset(),
                n = e > 0 ? "-" : "+",
                r = Ca(e) / 60 | 0,
                o = Ca(e) % 60;
            return n + Kt(r, "0", 2) + Kt(o, "0", 2)
        }

        function pe(t, e, n) { _u.lastIndex = 0; var r = _u.exec(e.slice(n, n + 1)); return r ? n + r[0].length : -1 }

        function he(t) { for (var e = t.length, n = -1; ++n < e;) t[n][0] = this(t[n][0]); return function (e) { for (var n = 0, r = t[n]; !r[1](e);) r = t[++n]; return r[0](e) } }

        function de() { }

        function ve(t, e, n) {
            var r = n.s = t + e,
                o = r - t,
                i = r - o;
            n.t = t - i + (e - o)
        }

        function ge(t, e) { t && ku.hasOwnProperty(t.type) && ku[t.type](t, e) }

        function ye(t, e, n) {
            var r, o = -1,
                i = t.length - n;
            for (e.lineStart(); ++o < i;) r = t[o], e.point(r[0], r[1], r[2]);
            e.lineEnd()
        }

        function me(t, e) {
            var n = -1,
                r = t.length;
            for (e.polygonStart(); ++n < r;) ye(t[n], e, 1);
            e.polygonEnd()
        }

        function be() {
            function t(t, e) {
                t *= Ya, e = e * Ya / 2 + Ba / 4;
                var n = t - r,
                    a = n >= 0 ? 1 : -1,
                    u = a * n,
                    s = Math.cos(e),
                    c = Math.sin(e),
                    l = i * c,
                    f = o * s + l * Math.cos(u),
                    p = l * a * Math.sin(u);
                Su.add(Math.atan2(p, f)), r = t, o = s, i = c
            }
            var e, n, r, o, i;
            Nu.point = function (a, u) { Nu.point = t, r = (e = a) * Ya, o = Math.cos(u = (n = u) * Ya / 2 + Ba / 4), i = Math.sin(u) }, Nu.lineEnd = function () { t(e, n) }
        }

        function xe(t) {
            var e = t[0],
                n = t[1],
                r = Math.cos(n);
            return [r * Math.cos(e), r * Math.sin(e), Math.sin(n)]
        }

        function we(t, e) { return t[0] * e[0] + t[1] * e[1] + t[2] * e[2] }

        function _e(t, e) { return [t[1] * e[2] - t[2] * e[1], t[2] * e[0] - t[0] * e[2], t[0] * e[1] - t[1] * e[0]] }

        function Ce(t, e) { t[0] += e[0], t[1] += e[1], t[2] += e[2] }

        function Ee(t, e) { return [t[0] * e, t[1] * e, t[2] * e] }

        function Me(t) {
            var e = Math.sqrt(t[0] * t[0] + t[1] * t[1] + t[2] * t[2]);
            t[0] /= e, t[1] /= e, t[2] /= e
        }

        function ke(t) { return [Math.atan2(t[1], t[0]), at(t[2])] }

        function Te(t, e) { return Ca(t[0] - e[0]) < qa && Ca(t[1] - e[1]) < qa }

        function Se(t, e) {
            t *= Ya;
            var n = Math.cos(e *= Ya);
            Ne(n * Math.cos(t), n * Math.sin(t), Math.sin(e))
        }

        function Ne(t, e, n) { ++Ou, Pu += (t - Pu) / Ou, ju += (e - ju) / Ou, Du += (n - Du) / Ou }

        function Oe() {
            function t(t, o) {
                t *= Ya;
                var i = Math.cos(o *= Ya),
                    a = i * Math.cos(t),
                    u = i * Math.sin(t),
                    s = Math.sin(o),
                    c = Math.atan2(Math.sqrt((c = n * s - r * u) * c + (c = r * a - e * s) * c + (c = e * u - n * a) * c), e * a + n * u + r * s);
                Au += c, Iu += c * (e + (e = a)), Ru += c * (n + (n = u)), Lu += c * (r + (r = s)), Ne(e, n, r)
            }
            var e, n, r;
            Hu.point = function (o, i) {
                o *= Ya;
                var a = Math.cos(i *= Ya);
                e = a * Math.cos(o), n = a * Math.sin(o), r = Math.sin(i), Hu.point = t, Ne(e, n, r)
            }
        }

        function Ae() { Hu.point = Se }

        function Pe() {
            function t(t, e) {
                t *= Ya;
                var n = Math.cos(e *= Ya),
                    a = n * Math.cos(t),
                    u = n * Math.sin(t),
                    s = Math.sin(e),
                    c = o * s - i * u,
                    l = i * a - r * s,
                    f = r * u - o * a,
                    p = Math.sqrt(c * c + l * l + f * f),
                    h = r * a + o * u + i * s,
                    d = p && -it(h) / p,
                    v = Math.atan2(p, h);
                Fu += d * c, Uu += d * l, qu += d * f, Au += v, Iu += v * (r + (r = a)), Ru += v * (o + (o = u)), Lu += v * (i + (i = s)), Ne(r, o, i)
            }
            var e, n, r, o, i;
            Hu.point = function (a, u) {
                e = a, n = u, Hu.point = t, a *= Ya;
                var s = Math.cos(u *= Ya);
                r = s * Math.cos(a), o = s * Math.sin(a), i = Math.sin(u), Ne(r, o, i)
            }, Hu.lineEnd = function () { t(e, n), Hu.lineEnd = Ae, Hu.point = Se }
        }

        function je(t, e) {
            function n(n, r) { return n = t(n, r), e(n[0], n[1]) }
            return t.invert && e.invert && (n.invert = function (n, r) { return n = e.invert(n, r), n && t.invert(n[0], n[1]) }), n
        }

        function De() { return !0 }

        function Ie(t, e, n, r, o) {
            var i = [],
                a = [];
            if (t.forEach(function (t) {
                if (!((e = t.length - 1) <= 0)) {
                    var e, n = t[0],
                        r = t[e];
                    if (Te(n, r)) { o.lineStart(); for (var u = 0; u < e; ++u) o.point((n = t[u])[0], n[1]); return void o.lineEnd() }
                    var s = new Le(n, t, null, !0),
                        c = new Le(n, null, s, !1);
                    s.o = c, i.push(s), a.push(c), s = new Le(r, t, null, !1), c = new Le(r, null, s, !0), s.o = c, i.push(s), a.push(c)
                }
            }), a.sort(e), Re(i), Re(a), i.length) {
                for (var u = 0, s = n, c = a.length; u < c; ++u) a[u].e = s = !s;
                for (var l, f, p = i[0]; ;) {
                    for (var h = p, d = !0; h.v;)
                        if ((h = h.n) === p) return;
                    l = h.z, o.lineStart();
                    do {
                        if (h.v = h.o.v = !0, h.e) {
                            if (d)
                                for (var u = 0, c = l.length; u < c; ++u) o.point((f = l[u])[0], f[1]);
                            else r(h.x, h.n.x, 1, o);
                            h = h.n
                        } else {
                            if (d) { l = h.p.z; for (var u = l.length - 1; u >= 0; --u) o.point((f = l[u])[0], f[1]) } else r(h.x, h.p.x, -1, o);
                            h = h.p
                        }
                        h = h.o, l = h.z, d = !d
                    } while (!h.v);
                    o.lineEnd()
                }
            }
        }

        function Re(t) {
            if (e = t.length) {
                for (var e, n, r = 0, o = t[0]; ++r < e;) o.n = n = t[r], n.p = o, o = n;
                o.n = n = t[0], n.p = o
            }
        }

        function Le(t, e, n, r) { this.x = t, this.z = e, this.o = n, this.e = r, this.v = !1, this.n = this.p = null }

        function Fe(t, e, n, r) {
            return function (o, i) {
                function a(e, n) {
                    var r = o(e, n);
                    t(e = r[0], n = r[1]) && i.point(e, n)
                }

                function u(t, e) {
                    var n = o(t, e);
                    g.point(n[0], n[1])
                }

                function s() { m.point = u, g.lineStart() }

                function c() { m.point = a, g.lineEnd() }

                function l(t, e) {
                    v.push([t, e]);
                    var n = o(t, e);
                    x.point(n[0], n[1])
                }

                function f() { x.lineStart(), v = [] }

                function p() {
                    l(v[0][0], v[0][1]), x.lineEnd();
                    var t, e = x.clean(),
                        n = b.buffer(),
                        r = n.length;
                    if (v.pop(), d.push(v), v = null, r)
                        if (1 & e) {
                            t = n[0];
                            var o, r = t.length - 1,
                                a = -1;
                            if (r > 0) {
                                for (w || (i.polygonStart(), w = !0), i.lineStart(); ++a < r;) i.point((o = t[a])[0], o[1]);
                                i.lineEnd()
                            }
                        } else r > 1 && 2 & e && n.push(n.pop().concat(n.shift())), h.push(n.filter(Ue))
                }
                var h, d, v, g = e(i),
                    y = o.invert(r[0], r[1]),
                    m = {
                        point: a,
                        lineStart: s,
                        lineEnd: c,
                        polygonStart: function () { m.point = l, m.lineStart = f, m.lineEnd = p, h = [], d = [] },
                        polygonEnd: function () {
                            m.point = a, m.lineStart = s, m.lineEnd = c, h = ha.merge(h);
                            var t = Ve(y, d);
                            h.length ? (w || (i.polygonStart(), w = !0), Ie(h, He, t, n, i)) : t && (w || (i.polygonStart(), w = !0), i.lineStart(), n(null, null, 1, i), i.lineEnd()), w && (i.polygonEnd(), w = !1), h = d = null
                        },
                        sphere: function () { i.polygonStart(), i.lineStart(), n(null, null, 1, i), i.lineEnd(), i.polygonEnd() }
                    },
                    b = qe(),
                    x = e(b),
                    w = !1;
                return m
            }
        }

        function Ue(t) { return t.length > 1 }

        function qe() { var t, e = []; return { lineStart: function () { e.push(t = []) }, point: function (e, n) { t.push([e, n]) }, lineEnd: k, buffer: function () { var n = e; return e = [], t = null, n }, rejoin: function () { e.length > 1 && e.push(e.pop().concat(e.shift())) } } }

        function He(t, e) { return ((t = t.x)[0] < 0 ? t[1] - Va - qa : Va - t[1]) - ((e = e.x)[0] < 0 ? e[1] - Va - qa : Va - e[1]) }

        function Be(t) {
            var e, n = NaN,
                r = NaN,
                o = NaN;
            return {
                lineStart: function () { t.lineStart(), e = 1 },
                point: function (i, a) {
                    var u = i > 0 ? Ba : -Ba,
                        s = Ca(i - n);
                    Ca(s - Ba) < qa ? (t.point(n, r = (r + a) / 2 > 0 ? Va : -Va), t.point(o, r), t.lineEnd(), t.lineStart(), t.point(u, r), t.point(i, r), e = 0) : o !== u && s >= Ba && (Ca(n - o) < qa && (n -= o * qa), Ca(i - u) < qa && (i -= u * qa), r = We(n, r, i, a), t.point(o, r), t.lineEnd(), t.lineStart(), t.point(u, r), e = 0), t.point(n = i, r = a), o = u
                },
                lineEnd: function () { t.lineEnd(), n = r = NaN },
                clean: function () { return 2 - e }
            }
        }

        function We(t, e, n, r) { var o, i, a = Math.sin(t - n); return Ca(a) > qa ? Math.atan((Math.sin(e) * (i = Math.cos(r)) * Math.sin(n) - Math.sin(r) * (o = Math.cos(e)) * Math.sin(t)) / (o * i * a)) : (e + r) / 2 }

        function ze(t, e, n, r) {
            var o;
            if (null == t) o = n * Va, r.point(-Ba, o), r.point(0, o), r.point(Ba, o), r.point(Ba, 0), r.point(Ba, -o), r.point(0, -o), r.point(-Ba, -o), r.point(-Ba, 0), r.point(-Ba, o);
            else if (Ca(t[0] - e[0]) > qa) {
                var i = t[0] < e[0] ? Ba : -Ba;
                o = n * i / 2, r.point(-i, o), r.point(0, o), r.point(i, o)
            } else r.point(e[0], e[1])
        }

        function Ve(t, e) {
            var n = t[0],
                r = t[1],
                o = [Math.sin(n), -Math.cos(n), 0],
                i = 0,
                a = 0;
            Su.reset();
            for (var u = 0, s = e.length; u < s; ++u) {
                var c = e[u],
                    l = c.length;
                if (l)
                    for (var f = c[0], p = f[0], h = f[1] / 2 + Ba / 4, d = Math.sin(h), v = Math.cos(h), g = 1; ;) {
                        g === l && (g = 0), t = c[g];
                        var y = t[0],
                            m = t[1] / 2 + Ba / 4,
                            b = Math.sin(m),
                            x = Math.cos(m),
                            w = y - p,
                            _ = w >= 0 ? 1 : -1,
                            C = _ * w,
                            E = C > Ba,
                            M = d * b;
                        if (Su.add(Math.atan2(M * _ * Math.sin(C), v * x + M * Math.cos(C))), i += E ? w + _ * Wa : w, E ^ p >= n ^ y >= n) {
                            var k = _e(xe(f), xe(t));
                            Me(k);
                            var T = _e(o, k);
                            Me(T);
                            var S = (E ^ w >= 0 ? -1 : 1) * at(T[2]);
                            (r > S || r === S && (k[0] || k[1])) && (a += E ^ w >= 0 ? 1 : -1)
                        }
                        if (!g++) break;
                        p = y, d = b, v = x, f = t
                    }
            }
            return (i < -qa || i < qa && Su < -qa) ^ 1 & a
        }

        function Ye(t) {
            function e(t, e) { return Math.cos(t) * Math.cos(e) > i }

            function n(t) {
                var n, i, s, c, l;
                return {
                    lineStart: function () { c = s = !1, l = 1 },
                    point: function (f, p) {
                        var h, d = [f, p],
                            v = e(f, p),
                            g = a ? v ? 0 : o(f, p) : v ? o(f + (f < 0 ? Ba : -Ba), p) : 0;
                        if (!n && (c = s = v) && t.lineStart(), v !== s && (h = r(n, d), (Te(n, h) || Te(d, h)) && (d[0] += qa, d[1] += qa, v = e(d[0], d[1]))), v !== s) l = 0, v ? (t.lineStart(), h = r(d, n), t.point(h[0], h[1])) : (h = r(n, d), t.point(h[0], h[1]), t.lineEnd()), n = h;
                        else if (u && n && a ^ v) {
                            var y;
                            g & i || !(y = r(d, n, !0)) || (l = 0, a ? (t.lineStart(), t.point(y[0][0], y[0][1]), t.point(y[1][0], y[1][1]), t.lineEnd()) : (t.point(y[1][0], y[1][1]), t.lineEnd(), t.lineStart(), t.point(y[0][0], y[0][1])))
                        } !v || n && Te(n, d) || t.point(d[0], d[1]), n = d, s = v, i = g
                    },
                    lineEnd: function () { s && t.lineEnd(), n = null },
                    clean: function () { return l | (c && s) << 1 }
                }
            }

            function r(t, e, n) {
                var r = xe(t),
                    o = xe(e),
                    a = [1, 0, 0],
                    u = _e(r, o),
                    s = we(u, u),
                    c = u[0],
                    l = s - c * c;
                if (!l) return !n && t;
                var f = i * s / l,
                    p = -i * c / l,
                    h = _e(a, u),
                    d = Ee(a, f),
                    v = Ee(u, p);
                Ce(d, v);
                var g = h,
                    y = we(d, g),
                    m = we(g, g),
                    b = y * y - m * (we(d, d) - 1);
                if (!(b < 0)) {
                    var x = Math.sqrt(b),
                        w = Ee(g, (-y - x) / m);
                    if (Ce(w, d), w = ke(w), !n) return w;
                    var _, C = t[0],
                        E = e[0],
                        M = t[1],
                        k = e[1];
                    E < C && (_ = C, C = E, E = _);
                    var T = E - C,
                        S = Ca(T - Ba) < qa,
                        N = S || T < qa;
                    if (!S && k < M && (_ = M, M = k, k = _), N ? S ? M + k > 0 ^ w[1] < (Ca(w[0] - C) < qa ? M : k) : M <= w[1] && w[1] <= k : T > Ba ^ (C <= w[0] && w[0] <= E)) { var O = Ee(g, (-y + x) / m); return Ce(O, d), [w, ke(O)] }
                }
            }

            function o(e, n) {
                var r = a ? t : Ba - t,
                    o = 0;
                return e < -r ? o |= 1 : e > r && (o |= 2), n < -r ? o |= 4 : n > r && (o |= 8), o
            }
            var i = Math.cos(t),
                a = i > 0,
                u = Ca(i) > qa,
                s = xn(t, 6 * Ya);
            return Fe(e, n, s, a ? [0, -t] : [-Ba, t - Ba])
        }

        function $e(t, e, n, r) {
            return function (o) {
                var i, a = o.a,
                    u = o.b,
                    s = a.x,
                    c = a.y,
                    l = u.x,
                    f = u.y,
                    p = 0,
                    h = 1,
                    d = l - s,
                    v = f - c;
                if (i = t - s, d || !(i > 0)) {
                    if (i /= d, d < 0) {
                        if (i < p) return;
                        i < h && (h = i)
                    } else if (d > 0) {
                        if (i > h) return;
                        i > p && (p = i)
                    }
                    if (i = n - s, d || !(i < 0)) {
                        if (i /= d, d < 0) {
                            if (i > h) return;
                            i > p && (p = i)
                        } else if (d > 0) {
                            if (i < p) return;
                            i < h && (h = i)
                        }
                        if (i = e - c, v || !(i > 0)) {
                            if (i /= v, v < 0) {
                                if (i < p) return;
                                i < h && (h = i)
                            } else if (v > 0) {
                                if (i > h) return;
                                i > p && (p = i)
                            }
                            if (i = r - c, v || !(i < 0)) {
                                if (i /= v, v < 0) {
                                    if (i > h) return;
                                    i > p && (p = i)
                                } else if (v > 0) {
                                    if (i < p) return;
                                    i < h && (h = i)
                                }
                                return p > 0 && (o.a = { x: s + p * d, y: c + p * v }), h < 1 && (o.b = { x: s + h * d, y: c + h * v }), o
                            }
                        }
                    }
                }
            }
        }

        function Ke(t, e, n, r) {
            function o(r, o) { return Ca(r[0] - t) < qa ? o > 0 ? 0 : 3 : Ca(r[0] - n) < qa ? o > 0 ? 2 : 1 : Ca(r[1] - e) < qa ? o > 0 ? 1 : 0 : o > 0 ? 3 : 2 }

            function i(t, e) { return a(t.x, e.x) }

            function a(t, e) {
                var n = o(t, 1),
                    r = o(e, 1);
                return n !== r ? n - r : 0 === n ? e[1] - t[1] : 1 === n ? t[0] - e[0] : 2 === n ? t[1] - e[1] : e[0] - t[0]
            }
            return function (u) {
                function s(t) {
                    for (var e = 0, n = g.length, r = t[1], o = 0; o < n; ++o)
                        for (var i, a = 1, u = g[o], s = u.length, c = u[0]; a < s; ++a) i = u[a], c[1] <= r ? i[1] > r && ot(c, i, t) > 0 && ++e : i[1] <= r && ot(c, i, t) < 0 && --e, c = i;
                    return 0 !== e
                }

                function c(i, u, s, c) {
                    var l = 0,
                        f = 0;
                    if (null == i || (l = o(i, s)) !== (f = o(u, s)) || a(i, u) < 0 ^ s > 0) { do c.point(0 === l || 3 === l ? t : n, l > 1 ? r : e); while ((l = (l + s + 4) % 4) !== f) } else c.point(u[0], u[1])
                }

                function l(o, i) { return t <= o && o <= n && e <= i && i <= r }

                function f(t, e) { l(t, e) && u.point(t, e) }

                function p() { N.point = d, g && g.push(y = []), E = !0, C = !1, w = _ = NaN }

                function h() { v && (d(m, b), x && C && T.rejoin(), v.push(T.buffer())), N.point = f, C && u.lineEnd() }

                function d(t, e) {
                    t = Math.max(-Wu, Math.min(Wu, t)), e = Math.max(-Wu, Math.min(Wu, e));
                    var n = l(t, e);
                    if (g && y.push([t, e]), E) m = t, b = e, x = n, E = !1, n && (u.lineStart(), u.point(t, e));
                    else if (n && C) u.point(t, e);
                    else {
                        var r = { a: { x: w, y: _ }, b: { x: t, y: e } };
                        S(r) ? (C || (u.lineStart(), u.point(r.a.x, r.a.y)), u.point(r.b.x, r.b.y), n || u.lineEnd(), M = !1) : n && (u.lineStart(), u.point(t, e), M = !1)
                    }
                    w = t, _ = e, C = n
                }
                var v, g, y, m, b, x, w, _, C, E, M, k = u,
                    T = qe(),
                    S = $e(t, e, n, r),
                    N = {
                        point: f,
                        lineStart: p,
                        lineEnd: h,
                        polygonStart: function () { u = T, v = [], g = [], M = !0 },
                        polygonEnd: function () {
                            u = k, v = ha.merge(v);
                            var e = s([t, r]),
                                n = M && e,
                                o = v.length;
                            (n || o) && (u.polygonStart(), n && (u.lineStart(), c(null, null, 1, u), u.lineEnd()), o && Ie(v, i, e, c, u), u.polygonEnd()), v = g = y = null
                        }
                    };
                return N
            }
        }

        function Xe(t) {
            var e = 0,
                n = Ba / 3,
                r = pn(t),
                o = r(e, n);
            return o.parallels = function (t) { return arguments.length ? r(e = t[0] * Ba / 180, n = t[1] * Ba / 180) : [e / Ba * 180, n / Ba * 180] }, o
        }

        function Ge(t, e) {
            function n(t, e) { var n = Math.sqrt(i - 2 * o * Math.sin(e)) / o; return [n * Math.sin(t *= o), a - n * Math.cos(t)] }
            var r = Math.sin(t),
                o = (r + Math.sin(e)) / 2,
                i = 1 + r * (2 * o - r),
                a = Math.sqrt(i) / o;
            return n.invert = function (t, e) { var n = a - e; return [Math.atan2(t, n) / o, at((i - (t * t + n * n) * o * o) / (2 * o))] }, n
        }

        function Je() {
            function t(t, e) { Vu += o * t - r * e, r = t, o = e }
            var e, n, r, o;
            Gu.point = function (i, a) { Gu.point = t, e = r = i, n = o = a }, Gu.lineEnd = function () { t(e, n) }
        }

        function Qe(t, e) { t < Yu && (Yu = t), t > Ku && (Ku = t), e < $u && ($u = e), e > Xu && (Xu = e) }

        function Ze() {
            function t(t, e) { a.push("M", t, ",", e, i) }

            function e(t, e) { a.push("M", t, ",", e), u.point = n }

            function n(t, e) { a.push("L", t, ",", e) }

            function r() { u.point = t }

            function o() { a.push("Z") }
            var i = tn(4.5),
                a = [],
                u = { point: t, lineStart: function () { u.point = e }, lineEnd: r, polygonStart: function () { u.lineEnd = o }, polygonEnd: function () { u.lineEnd = r, u.point = t }, pointRadius: function (t) { return i = tn(t), u }, result: function () { if (a.length) { var t = a.join(""); return a = [], t } } };
            return u
        }

        function tn(t) { return "m0," + t + "a" + t + "," + t + " 0 1,1 0," + -2 * t + "a" + t + "," + t + " 0 1,1 0," + 2 * t + "z" }

        function en(t, e) { Pu += t, ju += e, ++Du }

        function nn() {
            function t(t, r) {
                var o = t - e,
                    i = r - n,
                    a = Math.sqrt(o * o + i * i);
                Iu += a * (e + t) / 2, Ru += a * (n + r) / 2, Lu += a, en(e = t, n = r)
            }
            var e, n;
            Qu.point = function (r, o) { Qu.point = t, en(e = r, n = o) }
        }

        function rn() { Qu.point = en }

        function on() {
            function t(t, e) {
                var n = t - r,
                    i = e - o,
                    a = Math.sqrt(n * n + i * i);
                Iu += a * (r + t) / 2, Ru += a * (o + e) / 2, Lu += a, a = o * t - r * e, Fu += a * (r + t), Uu += a * (o + e), qu += 3 * a, en(r = t, o = e)
            }
            var e, n, r, o;
            Qu.point = function (i, a) { Qu.point = t, en(e = r = i, n = o = a) }, Qu.lineEnd = function () { t(e, n) }
        }

        function an(t) {
            function e(e, n) { t.moveTo(e + a, n), t.arc(e, n, a, 0, Wa) }

            function n(e, n) { t.moveTo(e, n), u.point = r }

            function r(e, n) { t.lineTo(e, n) }

            function o() { u.point = e }

            function i() { t.closePath() }
            var a = 4.5,
                u = { point: e, lineStart: function () { u.point = n }, lineEnd: o, polygonStart: function () { u.lineEnd = i }, polygonEnd: function () { u.lineEnd = o, u.point = e }, pointRadius: function (t) { return a = t, u }, result: k };
            return u
        }

        function un(t) {
            function e(t) { return (u ? r : n)(t) }

            function n(e) { return ln(e, function (n, r) { n = t(n, r), e.point(n[0], n[1]) }) }

            function r(e) {
                function n(n, r) { n = t(n, r), e.point(n[0], n[1]) }

                function r() { b = NaN, E.point = i, e.lineStart() }

                function i(n, r) {
                    var i = xe([n, r]),
                        a = t(n, r);
                    o(b, x, m, w, _, C, b = a[0], x = a[1], m = n, w = i[0], _ = i[1], C = i[2], u, e), e.point(b, x)
                }

                function a() { E.point = n, e.lineEnd() }

                function s() { r(), E.point = c, E.lineEnd = l }

                function c(t, e) { i(f = t, p = e), h = b, d = x, v = w, g = _, y = C, E.point = i }

                function l() { o(b, x, m, w, _, C, h, d, f, v, g, y, u, e), E.lineEnd = a, a() }
                var f, p, h, d, v, g, y, m, b, x, w, _, C, E = { point: n, lineStart: r, lineEnd: a, polygonStart: function () { e.polygonStart(), E.lineStart = s }, polygonEnd: function () { e.polygonEnd(), E.lineStart = r } };
                return E
            }

            function o(e, n, r, u, s, c, l, f, p, h, d, v, g, y) {
                var m = l - e,
                    b = f - n,
                    x = m * m + b * b;
                if (x > 4 * i && g--) {
                    var w = u + h,
                        _ = s + d,
                        C = c + v,
                        E = Math.sqrt(w * w + _ * _ + C * C),
                        M = Math.asin(C /= E),
                        k = Ca(Ca(C) - 1) < qa || Ca(r - p) < qa ? (r + p) / 2 : Math.atan2(_, w),
                        T = t(k, M),
                        S = T[0],
                        N = T[1],
                        O = S - e,
                        A = N - n,
                        P = b * O - m * A;
                    (P * P / x > i || Ca((m * O + b * A) / x - .5) > .3 || u * h + s * d + c * v < a) && (o(e, n, r, u, s, c, S, N, k, w /= E, _ /= E, C, g, y), y.point(S, N), o(S, N, k, w, _, C, l, f, p, h, d, v, g, y))
                }
            }
            var i = .5,
                a = Math.cos(30 * Ya),
                u = 16;
            return e.precision = function (t) { return arguments.length ? (u = (i = t * t) > 0 && 16, e) : Math.sqrt(i) }, e
        }

        function sn(t) { var e = un(function (e, n) { return t([e * $a, n * $a]) }); return function (t) { return hn(e(t)) } }

        function cn(t) { this.stream = t }

        function ln(t, e) { return { point: e, sphere: function () { t.sphere() }, lineStart: function () { t.lineStart() }, lineEnd: function () { t.lineEnd() }, polygonStart: function () { t.polygonStart() }, polygonEnd: function () { t.polygonEnd() } } }

        function fn(t) { return pn(function () { return t })() }

        function pn(t) {
            function e(t) { return t = u(t[0] * Ya, t[1] * Ya), [t[0] * p + s, c - t[1] * p] }

            function n(t) { return t = u.invert((t[0] - s) / p, (c - t[1]) / p), t && [t[0] * $a, t[1] * $a] }

            function r() { u = je(a = gn(y, m, b), i); var t = i(v, g); return s = h - t[0] * p, c = d + t[1] * p, o() }

            function o() { return l && (l.valid = !1, l = null), e }
            var i, a, u, s, c, l, f = un(function (t, e) { return t = i(t, e), [t[0] * p + s, c - t[1] * p] }),
                p = 150,
                h = 480,
                d = 250,
                v = 0,
                g = 0,
                y = 0,
                m = 0,
                b = 0,
                x = Bu,
                w = C,
                _ = null,
                E = null;
            return e.stream = function (t) { return l && (l.valid = !1), l = hn(x(a, f(w(t)))), l.valid = !0, l }, e.clipAngle = function (t) { return arguments.length ? (x = null == t ? (_ = t, Bu) : Ye((_ = +t) * Ya), o()) : _ }, e.clipExtent = function (t) {
                return arguments.length ? (E = t, w = t ? Ke(t[0][0], t[0][1], t[1][0], t[1][1]) : C, o()) : E
            }, e.scale = function (t) { return arguments.length ? (p = +t, r()) : p }, e.translate = function (t) { return arguments.length ? (h = +t[0], d = +t[1], r()) : [h, d] }, e.center = function (t) { return arguments.length ? (v = t[0] % 360 * Ya, g = t[1] % 360 * Ya, r()) : [v * $a, g * $a] }, e.rotate = function (t) { return arguments.length ? (y = t[0] % 360 * Ya, m = t[1] % 360 * Ya, b = t.length > 2 ? t[2] % 360 * Ya : 0, r()) : [y * $a, m * $a, b * $a] }, ha.rebind(e, f, "precision"),
                function () { return i = t.apply(this, arguments), e.invert = i.invert && n, r() }
        }

        function hn(t) { return ln(t, function (e, n) { t.point(e * Ya, n * Ya) }) }

        function dn(t, e) { return [t, e] }

        function vn(t, e) { return [t > Ba ? t - Wa : t < -Ba ? t + Wa : t, e] }

        function gn(t, e, n) { return t ? e || n ? je(mn(t), bn(e, n)) : mn(t) : e || n ? bn(e, n) : vn }

        function yn(t) { return function (e, n) { return e += t, [e > Ba ? e - Wa : e < -Ba ? e + Wa : e, n] } }

        function mn(t) { var e = yn(t); return e.invert = yn(-t), e }

        function bn(t, e) {
            function n(t, e) {
                var n = Math.cos(e),
                    u = Math.cos(t) * n,
                    s = Math.sin(t) * n,
                    c = Math.sin(e),
                    l = c * r + u * o;
                return [Math.atan2(s * i - l * a, u * r - c * o), at(l * i + s * a)]
            }
            var r = Math.cos(t),
                o = Math.sin(t),
                i = Math.cos(e),
                a = Math.sin(e);
            return n.invert = function (t, e) {
                var n = Math.cos(e),
                    u = Math.cos(t) * n,
                    s = Math.sin(t) * n,
                    c = Math.sin(e),
                    l = c * i - s * a;
                return [Math.atan2(s * i + c * a, u * r + l * o), at(l * r - u * o)]
            }, n
        }

        function xn(t, e) {
            var n = Math.cos(t),
                r = Math.sin(t);
            return function (o, i, a, u) {
                var s = a * e;
                null != o ? (o = wn(n, o), i = wn(n, i), (a > 0 ? o < i : o > i) && (o += a * Wa)) : (o = t + a * Wa, i = t - .5 * s);
                for (var c, l = o; a > 0 ? l > i : l < i; l -= s) u.point((c = ke([n, -r * Math.cos(l), -r * Math.sin(l)]))[0], c[1])
            }
        }

        function wn(t, e) {
            var n = xe(e);
            n[0] -= t, Me(n);
            var r = it(-n[1]);
            return ((-n[2] < 0 ? -r : r) + 2 * Math.PI - qa) % (2 * Math.PI)
        }

        function _n(t, e, n) { var r = ha.range(t, e - qa, n).concat(e); return function (t) { return r.map(function (e) { return [t, e] }) } }

        function Cn(t, e, n) { var r = ha.range(t, e - qa, n).concat(e); return function (t) { return r.map(function (e) { return [e, t] }) } }

        function En(t) { return t.source }

        function Mn(t) { return t.target }

        function kn(t, e, n, r) {
            var o = Math.cos(e),
                i = Math.sin(e),
                a = Math.cos(r),
                u = Math.sin(r),
                s = o * Math.cos(t),
                c = o * Math.sin(t),
                l = a * Math.cos(n),
                f = a * Math.sin(n),
                p = 2 * Math.asin(Math.sqrt(lt(r - e) + o * a * lt(n - t))),
                h = 1 / Math.sin(p),
                d = p ? function (t) {
                    var e = Math.sin(t *= p) * h,
                        n = Math.sin(p - t) * h,
                        r = n * s + e * l,
                        o = n * c + e * f,
                        a = n * i + e * u;
                    return [Math.atan2(o, r) * $a, Math.atan2(a, Math.sqrt(r * r + o * o)) * $a]
                } : function () { return [t * $a, e * $a] };
            return d.distance = p, d
        }

        function Tn() {
            function t(t, o) {
                var i = Math.sin(o *= Ya),
                    a = Math.cos(o),
                    u = Ca((t *= Ya) - e),
                    s = Math.cos(u);
                Zu += Math.atan2(Math.sqrt((u = a * Math.sin(u)) * u + (u = r * i - n * a * s) * u), n * i + r * a * s), e = t, n = i, r = a
            }
            var e, n, r;
            ts.point = function (o, i) { e = o * Ya, n = Math.sin(i *= Ya), r = Math.cos(i), ts.point = t }, ts.lineEnd = function () { ts.point = ts.lineEnd = k }
        }

        function Sn(t, e) {
            function n(e, n) {
                var r = Math.cos(e),
                    o = Math.cos(n),
                    i = t(r * o);
                return [i * o * Math.sin(e), i * Math.sin(n)]
            }
            return n.invert = function (t, n) {
                var r = Math.sqrt(t * t + n * n),
                    o = e(r),
                    i = Math.sin(o),
                    a = Math.cos(o);
                return [Math.atan2(t * i, r * a), Math.asin(r && n * i / r)]
            }, n
        }

        function Nn(t, e) {
            function n(t, e) { a > 0 ? e < -Va + qa && (e = -Va + qa) : e > Va - qa && (e = Va - qa); var n = a / Math.pow(o(e), i); return [n * Math.sin(i * t), a - n * Math.cos(i * t)] }
            var r = Math.cos(t),
                o = function (t) { return Math.tan(Ba / 4 + t / 2) },
                i = t === e ? Math.sin(t) : Math.log(r / Math.cos(e)) / Math.log(o(e) / o(t)),
                a = r * Math.pow(o(t), i) / i;
            return i ? (n.invert = function (t, e) {
                var n = a - e,
                    r = rt(i) * Math.sqrt(t * t + n * n);
                return [Math.atan2(t, n) / i, 2 * Math.atan(Math.pow(a / r, 1 / i)) - Va]
            }, n) : An
        }

        function On(t, e) {
            function n(t, e) { var n = i - e; return [n * Math.sin(o * t), i - n * Math.cos(o * t)] }
            var r = Math.cos(t),
                o = t === e ? Math.sin(t) : (r - Math.cos(e)) / (e - t),
                i = r / o + t;
            return Ca(o) < qa ? dn : (n.invert = function (t, e) { var n = i - e; return [Math.atan2(t, n) / o, i - rt(o) * Math.sqrt(t * t + n * n)] }, n)
        }

        function An(t, e) { return [t, Math.log(Math.tan(Ba / 4 + e / 2))] }

        function Pn(t) {
            var e, n = fn(t),
                r = n.scale,
                o = n.translate,
                i = n.clipExtent;
            return n.scale = function () { var t = r.apply(n, arguments); return t === n ? e ? n.clipExtent(null) : n : t }, n.translate = function () { var t = o.apply(n, arguments); return t === n ? e ? n.clipExtent(null) : n : t }, n.clipExtent = function (t) {
                var a = i.apply(n, arguments);
                if (a === n) {
                    if (e = null == t) {
                        var u = Ba * r(),
                            s = o();
                        i([
                            [s[0] - u, s[1] - u],
                            [s[0] + u, s[1] + u]
                        ])
                    }
                } else e && (a = null);
                return a
            }, n.clipExtent(null)
        }

        function jn(t, e) { return [Math.log(Math.tan(Ba / 4 + e / 2)), -t] }

        function Dn(t) { return t[0] }

        function In(t) { return t[1] }

        function Rn(t) {
            for (var e = t.length, n = [0, 1], r = 2, o = 2; o < e; o++) {
                for (; r > 1 && ot(t[n[r - 2]], t[n[r - 1]], t[o]) <= 0;)--r;
                n[r++] = o
            }
            return n.slice(0, r)
        }

        function Ln(t, e) { return t[0] - e[0] || t[1] - e[1] }

        function Fn(t, e, n) { return (n[0] - e[0]) * (t[1] - e[1]) < (n[1] - e[1]) * (t[0] - e[0]) }

        function Un(t, e, n, r) {
            var o = t[0],
                i = n[0],
                a = e[0] - o,
                u = r[0] - i,
                s = t[1],
                c = n[1],
                l = e[1] - s,
                f = r[1] - c,
                p = (u * (s - c) - f * (o - i)) / (f * a - u * l);
            return [o + p * a, s + p * l]
        }

        function qn(t) {
            var e = t[0],
                n = t[t.length - 1];
            return !(e[0] - n[0] || e[1] - n[1])
        }

        function Hn() { sr(this), this.edge = this.site = this.circle = null }

        function Bn(t) { var e = ps.pop() || new Hn; return e.site = t, e }

        function Wn(t) { Zn(t), cs.remove(t), ps.push(t), sr(t) }

        function zn(t) {
            var e = t.circle,
                n = e.x,
                r = e.cy,
                o = { x: n, y: r },
                i = t.P,
                a = t.N,
                u = [t];
            Wn(t);
            for (var s = i; s.circle && Ca(n - s.circle.x) < qa && Ca(r - s.circle.cy) < qa;) i = s.P, u.unshift(s), Wn(s), s = i;
            u.unshift(s), Zn(s);
            for (var c = a; c.circle && Ca(n - c.circle.x) < qa && Ca(r - c.circle.cy) < qa;) a = c.N, u.push(c), Wn(c), c = a;
            u.push(c), Zn(c);
            var l, f = u.length;
            for (l = 1; l < f; ++l) c = u[l], s = u[l - 1], ir(c.edge, s.site, c.site, o);
            s = u[0], c = u[f - 1], c.edge = rr(s.site, c.site, null, o), Qn(s), Qn(c)
        }

        function Vn(t) {
            for (var e, n, r, o, i = t.x, a = t.y, u = cs._; u;)
                if (r = Yn(u, a) - i, r > qa) u = u.L;
                else {
                    if (o = i - $n(u, a), !(o > qa)) { r > -qa ? (e = u.P, n = u) : o > -qa ? (e = u, n = u.N) : e = n = u; break }
                    if (!u.R) { e = u; break }
                    u = u.R
                }
            var s = Bn(t);
            if (cs.insert(e, s), e || n) {
                if (e === n) return Zn(e), n = Bn(e.site), cs.insert(s, n), s.edge = n.edge = rr(e.site, s.site), Qn(e), void Qn(n);
                if (!n) return void (s.edge = rr(e.site, s.site));
                Zn(e), Zn(n);
                var c = e.site,
                    l = c.x,
                    f = c.y,
                    p = t.x - l,
                    h = t.y - f,
                    d = n.site,
                    v = d.x - l,
                    g = d.y - f,
                    y = 2 * (p * g - h * v),
                    m = p * p + h * h,
                    b = v * v + g * g,
                    x = { x: (g * m - h * b) / y + l, y: (p * b - v * m) / y + f };
                ir(n.edge, c, d, x), s.edge = rr(c, t, null, x), n.edge = rr(t, d, null, x), Qn(e), Qn(n)
            }
        }

        function Yn(t, e) {
            var n = t.site,
                r = n.x,
                o = n.y,
                i = o - e;
            if (!i) return r;
            var a = t.P;
            if (!a) return -(1 / 0);
            n = a.site;
            var u = n.x,
                s = n.y,
                c = s - e;
            if (!c) return u;
            var l = u - r,
                f = 1 / i - 1 / c,
                p = l / c;
            return f ? (-p + Math.sqrt(p * p - 2 * f * (l * l / (-2 * c) - s + c / 2 + o - i / 2))) / f + r : (r + u) / 2
        }

        function $n(t, e) { var n = t.N; if (n) return Yn(n, e); var r = t.site; return r.y === e ? r.x : 1 / 0 }

        function Kn(t) { this.site = t, this.edges = [] }

        function Xn(t) {
            for (var e, n, r, o, i, a, u, s, c, l, f = t[0][0], p = t[1][0], h = t[0][1], d = t[1][1], v = ss, g = v.length; g--;)
                if (i = v[g], i && i.prepare())
                    for (u = i.edges, s = u.length, a = 0; a < s;) l = u[a].end(), r = l.x, o = l.y, c = u[++a % s].start(), e = c.x, n = c.y, (Ca(r - e) > qa || Ca(o - n) > qa) && (u.splice(a, 0, new ar(or(i.site, l, Ca(r - f) < qa && d - o > qa ? { x: f, y: Ca(e - f) < qa ? n : d } : Ca(o - d) < qa && p - r > qa ? { x: Ca(n - d) < qa ? e : p, y: d } : Ca(r - p) < qa && o - h > qa ? { x: p, y: Ca(e - p) < qa ? n : h } : Ca(o - h) < qa && r - f > qa ? { x: Ca(n - h) < qa ? e : f, y: h } : null), i.site, null)), ++s)
        }

        function Gn(t, e) { return e.angle - t.angle }

        function Jn() { sr(this), this.x = this.y = this.arc = this.site = this.cy = null }

        function Qn(t) {
            var e = t.P,
                n = t.N;
            if (e && n) {
                var r = e.site,
                    o = t.site,
                    i = n.site;
                if (r !== i) {
                    var a = o.x,
                        u = o.y,
                        s = r.x - a,
                        c = r.y - u,
                        l = i.x - a,
                        f = i.y - u,
                        p = 2 * (s * f - c * l);
                    if (!(p >= -Ha)) {
                        var h = s * s + c * c,
                            d = l * l + f * f,
                            v = (f * h - c * d) / p,
                            g = (s * d - l * h) / p,
                            f = g + u,
                            y = hs.pop() || new Jn;
                        y.arc = t, y.site = o, y.x = v + a, y.y = f + Math.sqrt(v * v + g * g), y.cy = f, t.circle = y;
                        for (var m = null, b = fs._; b;)
                            if (y.y < b.y || y.y === b.y && y.x <= b.x) {
                                if (!b.L) { m = b.P; break }
                                b = b.L
                            } else {
                                if (!b.R) { m = b; break }
                                b = b.R
                            }
                        fs.insert(m, y), m || (ls = y)
                    }
                }
            }
        }

        function Zn(t) {
            var e = t.circle;
            e && (e.P || (ls = e.N), fs.remove(e), hs.push(e), sr(e), t.circle = null)
        }

        function tr(t) { for (var e, n = us, r = $e(t[0][0], t[0][1], t[1][0], t[1][1]), o = n.length; o--;) e = n[o], (!er(e, t) || !r(e) || Ca(e.a.x - e.b.x) < qa && Ca(e.a.y - e.b.y) < qa) && (e.a = e.b = null, n.splice(o, 1)) }

        function er(t, e) {
            var n = t.b;
            if (n) return !0;
            var r, o, i = t.a,
                a = e[0][0],
                u = e[1][0],
                s = e[0][1],
                c = e[1][1],
                l = t.l,
                f = t.r,
                p = l.x,
                h = l.y,
                d = f.x,
                v = f.y,
                g = (p + d) / 2,
                y = (h + v) / 2;
            if (v === h) {
                if (g < a || g >= u) return;
                if (p > d) {
                    if (i) { if (i.y >= c) return } else i = { x: g, y: s };
                    n = { x: g, y: c }
                } else {
                    if (i) { if (i.y < s) return } else i = { x: g, y: c };
                    n = { x: g, y: s }
                }
            } else if (r = (p - d) / (v - h), o = y - r * g, r < -1 || r > 1)
                if (p > d) {
                    if (i) { if (i.y >= c) return } else i = { x: (s - o) / r, y: s };
                    n = { x: (c - o) / r, y: c }
                } else {
                    if (i) { if (i.y < s) return } else i = { x: (c - o) / r, y: c };
                    n = { x: (s - o) / r, y: s }
                }
            else if (h < v) {
                if (i) { if (i.x >= u) return } else i = { x: a, y: r * a + o };
                n = { x: u, y: r * u + o }
            } else {
                if (i) { if (i.x < a) return } else i = { x: u, y: r * u + o };
                n = { x: a, y: r * a + o }
            }
            return t.a = i, t.b = n, !0
        }

        function nr(t, e) { this.l = t, this.r = e, this.a = this.b = null }

        function rr(t, e, n, r) { var o = new nr(t, e); return us.push(o), n && ir(o, t, e, n), r && ir(o, e, t, r), ss[t.i].edges.push(new ar(o, t, e)), ss[e.i].edges.push(new ar(o, e, t)), o }

        function or(t, e, n) { var r = new nr(t, null); return r.a = e, r.b = n, us.push(r), r }

        function ir(t, e, n, r) { t.a || t.b ? t.l === n ? t.b = r : t.a = r : (t.a = r, t.l = e, t.r = n) }

        function ar(t, e, n) {
            var r = t.a,
                o = t.b;
            this.edge = t, this.site = e, this.angle = n ? Math.atan2(n.y - e.y, n.x - e.x) : t.l === e ? Math.atan2(o.x - r.x, r.y - o.y) : Math.atan2(r.x - o.x, o.y - r.y)
        }

        function ur() { this._ = null }

        function sr(t) { t.U = t.C = t.L = t.R = t.P = t.N = null }

        function cr(t, e) {
            var n = e,
                r = e.R,
                o = n.U;
            o ? o.L === n ? o.L = r : o.R = r : t._ = r, r.U = o, n.U = r, n.R = r.L, n.R && (n.R.U = n), r.L = n
        }

        function lr(t, e) {
            var n = e,
                r = e.L,
                o = n.U;
            o ? o.L === n ? o.L = r : o.R = r : t._ = r, r.U = o, n.U = r, n.L = r.R, n.L && (n.L.U = n), r.R = n
        }

        function fr(t) { for (; t.L;) t = t.L; return t }

        function pr(t, e) {
            var n, r, o, i = t.sort(hr).pop();
            for (us = [], ss = new Array(t.length), cs = new ur, fs = new ur; ;)
                if (o = ls, i && (!o || i.y < o.y || i.y === o.y && i.x < o.x)) i.x === n && i.y === r || (ss[i.i] = new Kn(i), Vn(i), n = i.x, r = i.y), i = t.pop();
                else {
                    if (!o) break;
                    zn(o.arc)
                }
            e && (tr(e), Xn(e));
            var a = { cells: ss, edges: us };
            return cs = fs = us = ss = null, a
        }

        function hr(t, e) { return e.y - t.y || e.x - t.x }

        function dr(t, e, n) { return (t.x - n.x) * (e.y - t.y) - (t.x - e.x) * (n.y - t.y) }

        function vr(t) { return t.x }

        function gr(t) { return t.y }

        function yr() { return { leaf: !0, nodes: [], point: null, x: null, y: null } }

        function mr(t, e, n, r, o, i) {
            if (!t(e, n, r, o, i)) {
                var a = .5 * (n + o),
                    u = .5 * (r + i),
                    s = e.nodes;
                s[0] && mr(t, s[0], n, r, a, u), s[1] && mr(t, s[1], a, r, o, u), s[2] && mr(t, s[2], n, u, a, i), s[3] && mr(t, s[3], a, u, o, i)
            }
        }

        function br(t, e, n, r, o, i, a) {
            var u, s = 1 / 0;
            return function t(c, l, f, p, h) {
                if (!(l > i || f > a || p < r || h < o)) {
                    if (d = c.point) {
                        var d, v = e - c.x,
                            g = n - c.y,
                            y = v * v + g * g;
                        if (y < s) {
                            var m = Math.sqrt(s = y);
                            r = e - m, o = n - m, i = e + m, a = n + m, u = d
                        }
                    }
                    for (var b = c.nodes, x = .5 * (l + p), w = .5 * (f + h), _ = e >= x, C = n >= w, E = C << 1 | _, M = E + 4; E < M; ++E)
                        if (c = b[3 & E]) switch (3 & E) {
                            case 0:
                                t(c, l, f, x, w);
                                break;
                            case 1:
                                t(c, x, f, p, w);
                                break;
                            case 2:
                                t(c, l, w, x, h);
                                break;
                            case 3:
                                t(c, x, w, p, h)
                        }
                }
            }(t, r, o, i, a), u
        }

        function xr(t, e) {
            t = ha.rgb(t), e = ha.rgb(e);
            var n = t.r,
                r = t.g,
                o = t.b,
                i = e.r - n,
                a = e.g - r,
                u = e.b - o;
            return function (t) { return "#" + Mt(Math.round(n + i * t)) + Mt(Math.round(r + a * t)) + Mt(Math.round(o + u * t)) }
        }

        function wr(t, e) {
            var n, r = {},
                o = {};
            for (n in t) n in e ? r[n] = Er(t[n], e[n]) : o[n] = t[n];
            for (n in e) n in t || (o[n] = e[n]);
            return function (t) { for (n in r) o[n] = r[n](t); return o }
        }

        function _r(t, e) {
            return t = +t, e = +e,
                function (n) { return t * (1 - n) + e * n }
        }

        function Cr(t, e) {
            var n, r, o, i = vs.lastIndex = gs.lastIndex = 0,
                a = -1,
                u = [],
                s = [];
            for (t += "", e += "";
                (n = vs.exec(t)) && (r = gs.exec(e));)(o = r.index) > i && (o = e.slice(i, o), u[a] ? u[a] += o : u[++a] = o), (n = n[0]) === (r = r[0]) ? u[a] ? u[a] += r : u[++a] = r : (u[++a] = null, s.push({ i: a, x: _r(n, r) })), i = gs.lastIndex;
            return i < e.length && (o = e.slice(i), u[a] ? u[a] += o : u[++a] = o), u.length < 2 ? s[0] ? (e = s[0].x, function (t) { return e(t) + "" }) : function () { return e } : (e = s.length, function (t) { for (var n, r = 0; r < e; ++r) u[(n = s[r]).i] = n.x(t); return u.join("") })
        }

        function Er(t, e) { for (var n, r = ha.interpolators.length; --r >= 0 && !(n = ha.interpolators[r](t, e));); return n }

        function Mr(t, e) {
            var n, r = [],
                o = [],
                i = t.length,
                a = e.length,
                u = Math.min(t.length, e.length);
            for (n = 0; n < u; ++n) r.push(Er(t[n], e[n]));
            for (; n < i; ++n) o[n] = t[n];
            for (; n < a; ++n) o[n] = e[n];
            return function (t) { for (n = 0; n < u; ++n) o[n] = r[n](t); return o }
        }

        function kr(t) { return function (e) { return e <= 0 ? 0 : e >= 1 ? 1 : t(e) } }

        function Tr(t) { return function (e) { return 1 - t(1 - e) } }

        function Sr(t) { return function (e) { return .5 * (e < .5 ? t(2 * e) : 2 - t(2 - 2 * e)) } }

        function Nr(t) { return t * t }

        function Or(t) { return t * t * t }

        function Ar(t) {
            if (t <= 0) return 0;
            if (t >= 1) return 1;
            var e = t * t,
                n = e * t;
            return 4 * (t < .5 ? n : 3 * (t - e) + n - .75)
        }

        function Pr(t) { return function (e) { return Math.pow(e, t) } }

        function jr(t) { return 1 - Math.cos(t * Va) }

        function Dr(t) { return Math.pow(2, 10 * (t - 1)) }

        function Ir(t) { return 1 - Math.sqrt(1 - t * t) }

        function Rr(t, e) {
            var n;
            return arguments.length < 2 && (e = .45), arguments.length ? n = e / Wa * Math.asin(1 / t) : (t = 1, n = e / 4),
                function (r) { return 1 + t * Math.pow(2, -10 * r) * Math.sin((r - n) * Wa / e) }
        }

        function Lr(t) {
            return t || (t = 1.70158),
                function (e) { return e * e * ((t + 1) * e - t) }
        }

        function Fr(t) { return t < 1 / 2.75 ? 7.5625 * t * t : t < 2 / 2.75 ? 7.5625 * (t -= 1.5 / 2.75) * t + .75 : t < 2.5 / 2.75 ? 7.5625 * (t -= 2.25 / 2.75) * t + .9375 : 7.5625 * (t -= 2.625 / 2.75) * t + .984375 }

        function Ur(t, e) {
            t = ha.hcl(t), e = ha.hcl(e);
            var n = t.h,
                r = t.c,
                o = t.l,
                i = e.h - n,
                a = e.c - r,
                u = e.l - o;
            return isNaN(a) && (a = 0, r = isNaN(r) ? e.c : r), isNaN(i) ? (i = 0, n = isNaN(n) ? e.h : n) : i > 180 ? i -= 360 : i < -180 && (i += 360),
                function (t) { return vt(n + i * t, r + a * t, o + u * t) + "" }
        }

        function qr(t, e) {
            t = ha.hsl(t), e = ha.hsl(e);
            var n = t.h,
                r = t.s,
                o = t.l,
                i = e.h - n,
                a = e.s - r,
                u = e.l - o;
            return isNaN(a) && (a = 0, r = isNaN(r) ? e.s : r), isNaN(i) ? (i = 0, n = isNaN(n) ? e.h : n) : i > 180 ? i -= 360 : i < -180 && (i += 360),
                function (t) { return ht(n + i * t, r + a * t, o + u * t) + "" }
        }

        function Hr(t, e) {
            t = ha.lab(t), e = ha.lab(e);
            var n = t.l,
                r = t.a,
                o = t.b,
                i = e.l - n,
                a = e.a - r,
                u = e.b - o;
            return function (t) { return yt(n + i * t, r + a * t, o + u * t) + "" }
        }

        function Br(t, e) {
            return e -= t,
                function (n) { return Math.round(t + e * n) }
        }

        function Wr(t) {
            var e = [t.a, t.b],
                n = [t.c, t.d],
                r = Vr(e),
                o = zr(e, n),
                i = Vr(Yr(n, e, -o)) || 0;
            e[0] * n[1] < n[0] * e[1] && (e[0] *= -1, e[1] *= -1, r *= -1, o *= -1), this.rotate = (r ? Math.atan2(e[1], e[0]) : Math.atan2(-n[0], n[1])) * $a, this.translate = [t.e, t.f], this.scale = [r, i], this.skew = i ? Math.atan2(o, i) * $a : 0
        }

        function zr(t, e) { return t[0] * e[0] + t[1] * e[1] }

        function Vr(t) { var e = Math.sqrt(zr(t, t)); return e && (t[0] /= e, t[1] /= e), e }

        function Yr(t, e, n) { return t[0] += n * e[0], t[1] += n * e[1], t }

        function $r(t) { return t.length ? t.pop() + "," : "" }

        function Kr(t, e, n, r) {
            if (t[0] !== e[0] || t[1] !== e[1]) {
                var o = n.push("translate(", null, ",", null, ")");
                r.push({ i: o - 4, x: _r(t[0], e[0]) }, { i: o - 2, x: _r(t[1], e[1]) })
            } else (e[0] || e[1]) && n.push("translate(" + e + ")")
        }

        function Xr(t, e, n, r) { t !== e ? (t - e > 180 ? e += 360 : e - t > 180 && (t += 360), r.push({ i: n.push($r(n) + "rotate(", null, ")") - 2, x: _r(t, e) })) : e && n.push($r(n) + "rotate(" + e + ")") }

        function Gr(t, e, n, r) { t !== e ? r.push({ i: n.push($r(n) + "skewX(", null, ")") - 2, x: _r(t, e) }) : e && n.push($r(n) + "skewX(" + e + ")") }

        function Jr(t, e, n, r) {
            if (t[0] !== e[0] || t[1] !== e[1]) {
                var o = n.push($r(n) + "scale(", null, ",", null, ")");
                r.push({ i: o - 4, x: _r(t[0], e[0]) }, { i: o - 2, x: _r(t[1], e[1]) })
            } else 1 === e[0] && 1 === e[1] || n.push($r(n) + "scale(" + e + ")")
        }

        function Qr(t, e) {
            var n = [],
                r = [];
            return t = ha.transform(t), e = ha.transform(e), Kr(t.translate, e.translate, n, r), Xr(t.rotate, e.rotate, n, r), Gr(t.skew, e.skew, n, r), Jr(t.scale, e.scale, n, r), t = e = null,
                function (t) { for (var e, o = -1, i = r.length; ++o < i;) n[(e = r[o]).i] = e.x(t); return n.join("") }
        }

        function Zr(t, e) {
            return e = (e -= t = +t) || 1 / e,
                function (n) { return (n - t) / e }
        }

        function to(t, e) {
            return e = (e -= t = +t) || 1 / e,
                function (n) { return Math.max(0, Math.min(1, (n - t) / e)) }
        }

        function eo(t) { for (var e = t.source, n = t.target, r = ro(e, n), o = [e]; e !== r;) e = e.parent, o.push(e); for (var i = o.length; n !== r;) o.splice(i, 0, n), n = n.parent; return o }

        function no(t) { for (var e = [], n = t.parent; null != n;) e.push(t), t = n, n = n.parent; return e.push(t), e }

        function ro(t, e) { if (t === e) return t; for (var n = no(t), r = no(e), o = n.pop(), i = r.pop(), a = null; o === i;) a = o, o = n.pop(), i = r.pop(); return a }

        function oo(t) { t.fixed |= 2 }

        function io(t) { t.fixed &= -7 }

        function ao(t) { t.fixed |= 4, t.px = t.x, t.py = t.y }

        function uo(t) { t.fixed &= -5 }

        function so(t, e, n) {
            var r = 0,
                o = 0;
            if (t.charge = 0, !t.leaf)
                for (var i, a = t.nodes, u = a.length, s = -1; ++s < u;) i = a[s], null != i && (so(i, e, n), t.charge += i.charge, r += i.charge * i.cx, o += i.charge * i.cy);
            if (t.point) {
                t.leaf || (t.point.x += Math.random() - .5, t.point.y += Math.random() - .5);
                var c = e * n[t.point.index];
                t.charge += t.pointCharge = c, r += c * t.point.x, o += c * t.point.y
            }
            t.cx = r / t.charge, t.cy = o / t.charge
        }

        function co(t, e) { return ha.rebind(t, e, "sort", "children", "value"), t.nodes = t, t.links = go, t }

        function lo(t, e) {
            for (var n = [t]; null != (t = n.pop());)
                if (e(t), (o = t.children) && (r = o.length))
                    for (var r, o; --r >= 0;) n.push(o[r])
        }

        function fo(t, e) {
            for (var n = [t], r = []; null != (t = n.pop());)
                if (r.push(t), (i = t.children) && (o = i.length))
                    for (var o, i, a = -1; ++a < o;) n.push(i[a]);
            for (; null != (t = r.pop());) e(t)
        }

        function po(t) { return t.children }

        function ho(t) { return t.value }

        function vo(t, e) { return e.value - t.value }

        function go(t) { return ha.merge(t.map(function (t) { return (t.children || []).map(function (e) { return { source: t, target: e } }) })) }

        function yo(t) { return t.x }

        function mo(t) { return t.y }

        function bo(t, e, n) { t.y0 = e, t.y = n }

        function xo(t) { return ha.range(t.length) }

        function wo(t) { for (var e = -1, n = t[0].length, r = []; ++e < n;) r[e] = 0; return r }

        function _o(t) { for (var e, n = 1, r = 0, o = t[0][1], i = t.length; n < i; ++n)(e = t[n][1]) > o && (r = n, o = e); return r }

        function Co(t) { return t.reduce(Eo, 0) }

        function Eo(t, e) { return t + e[1] }

        function Mo(t, e) { return ko(t, Math.ceil(Math.log(e.length) / Math.LN2 + 1)) }

        function ko(t, e) { for (var n = -1, r = +t[0], o = (t[1] - r) / e, i = []; ++n <= e;) i[n] = o * n + r; return i }

        function To(t) { return [ha.min(t), ha.max(t)] }

        function So(t, e) { return t.value - e.value }

        function No(t, e) {
            var n = t._pack_next;
            t._pack_next = e, e._pack_prev = t, e._pack_next = n, n._pack_prev = e
        }

        function Oo(t, e) { t._pack_next = e, e._pack_prev = t }

        function Ao(t, e) {
            var n = e.x - t.x,
                r = e.y - t.y,
                o = t.r + e.r;
            return .999 * o * o > n * n + r * r
        }

        function Po(t) {
            function e(t) { l = Math.min(t.x - t.r, l), f = Math.max(t.x + t.r, f), p = Math.min(t.y - t.r, p), h = Math.max(t.y + t.r, h) }
            if ((n = t.children) && (c = n.length)) {
                var n, r, o, i, a, u, s, c, l = 1 / 0,
                    f = -(1 / 0),
                    p = 1 / 0,
                    h = -(1 / 0);
                if (n.forEach(jo), r = n[0], r.x = -r.r, r.y = 0, e(r), c > 1 && (o = n[1], o.x = o.r, o.y = 0, e(o), c > 2))
                    for (i = n[2], Ro(r, o, i), e(i), No(r, i), r._pack_prev = i, No(i, o), o = r._pack_next, a = 3; a < c; a++) {
                        Ro(r, o, i = n[a]);
                        var d = 0,
                            v = 1,
                            g = 1;
                        for (u = o._pack_next; u !== o; u = u._pack_next, v++)
                            if (Ao(u, i)) { d = 1; break }
                        if (1 == d)
                            for (s = r._pack_prev; s !== u._pack_prev && !Ao(s, i); s = s._pack_prev, g++);
                        d ? (v < g || v == g && o.r < r.r ? Oo(r, o = u) : Oo(r = s, o), a--) : (No(r, i), o = i, e(i))
                    }
                var y = (l + f) / 2,
                    m = (p + h) / 2,
                    b = 0;
                for (a = 0; a < c; a++) i = n[a], i.x -= y, i.y -= m, b = Math.max(b, i.r + Math.sqrt(i.x * i.x + i.y * i.y));
                t.r = b, n.forEach(Do)
            }
        }

        function jo(t) { t._pack_next = t._pack_prev = t }

        function Do(t) { delete t._pack_next, delete t._pack_prev }

        function Io(t, e, n, r) {
            var o = t.children;
            if (t.x = e += r * t.x, t.y = n += r * t.y, t.r *= r, o)
                for (var i = -1, a = o.length; ++i < a;) Io(o[i], e, n, r)
        }

        function Ro(t, e, n) {
            var r = t.r + n.r,
                o = e.x - t.x,
                i = e.y - t.y;
            if (r && (o || i)) {
                var a = e.r + n.r,
                    u = o * o + i * i;
                a *= a, r *= r;
                var s = .5 + (r - a) / (2 * u),
                    c = Math.sqrt(Math.max(0, 2 * a * (r + u) - (r -= u) * r - a * a)) / (2 * u);
                n.x = t.x + s * o + c * i, n.y = t.y + s * i - c * o
            } else n.x = t.x + r, n.y = t.y
        }

        function Lo(t, e) { return t.parent == e.parent ? 1 : 2 }

        function Fo(t) { var e = t.children; return e.length ? e[0] : t.t }

        function Uo(t) { var e, n = t.children; return (e = n.length) ? n[e - 1] : t.t }

        function qo(t, e, n) {
            var r = n / (e.i - t.i);
            e.c -= r, e.s += n, t.c += r, e.z += n, e.m += n
        }

        function Ho(t) { for (var e, n = 0, r = 0, o = t.children, i = o.length; --i >= 0;) e = o[i], e.z += n, e.m += n, n += e.s + (r += e.c) }

        function Bo(t, e, n) { return t.a.parent === e.parent ? t.a : n }

        function Wo(t) { return 1 + ha.max(t, function (t) { return t.y }) }

        function zo(t) { return t.reduce(function (t, e) { return t + e.x }, 0) / t.length }

        function Vo(t) { var e = t.children; return e && e.length ? Vo(e[0]) : t }

        function Yo(t) { var e, n = t.children; return n && (e = n.length) ? Yo(n[e - 1]) : t }

        function $o(t) { return { x: t.x, y: t.y, dx: t.dx, dy: t.dy } }

        function Ko(t, e) {
            var n = t.x + e[3],
                r = t.y + e[0],
                o = t.dx - e[1] - e[3],
                i = t.dy - e[0] - e[2];
            return o < 0 && (n += o / 2, o = 0), i < 0 && (r += i / 2, i = 0), { x: n, y: r, dx: o, dy: i }
        }

        function Xo(t) {
            var e = t[0],
                n = t[t.length - 1];
            return e < n ? [e, n] : [n, e]
        }

        function Go(t) { return t.rangeExtent ? t.rangeExtent() : Xo(t.range()) }

        function Jo(t, e, n, r) {
            var o = n(t[0], t[1]),
                i = r(e[0], e[1]);
            return function (t) { return i(o(t)) }
        }

        function Qo(t, e) {
            var n, r = 0,
                o = t.length - 1,
                i = t[r],
                a = t[o];
            return a < i && (n = r, r = o, o = n, n = i, i = a, a = n), t[r] = e.floor(i), t[o] = e.ceil(a), t
        }

        function Zo(t) { return t ? { floor: function (e) { return Math.floor(e / t) * t }, ceil: function (e) { return Math.ceil(e / t) * t } } : Ts }

        function ti(t, e, n, r) {
            var o = [],
                i = [],
                a = 0,
                u = Math.min(t.length, e.length) - 1;
            for (t[u] < t[0] && (t = t.slice().reverse(), e = e.slice().reverse()); ++a <= u;) o.push(n(t[a - 1], t[a])), i.push(r(e[a - 1], e[a]));
            return function (e) { var n = ha.bisect(t, e, 1, u) - 1; return i[n](o[n](e)) }
        }

        function ei(t, e, n, r) {
            function o() {
                var o = Math.min(t.length, e.length) > 2 ? ti : Jo,
                    s = r ? to : Zr;
                return a = o(t, e, s, n), u = o(e, t, s, Er), i
            }

            function i(t) { return a(t) }
            var a, u;
            return i.invert = function (t) { return u(t) }, i.domain = function (e) { return arguments.length ? (t = e.map(Number), o()) : t }, i.range = function (t) { return arguments.length ? (e = t, o()) : e }, i.rangeRound = function (t) { return i.range(t).interpolate(Br) }, i.clamp = function (t) { return arguments.length ? (r = t, o()) : r }, i.interpolate = function (t) { return arguments.length ? (n = t, o()) : n }, i.ticks = function (e) { return ii(t, e) }, i.tickFormat = function (e, n) { return ai(t, e, n) }, i.nice = function (e) { return ri(t, e), o() }, i.copy = function () { return ei(t, e, n, r) }, o()
        }

        function ni(t, e) { return ha.rebind(t, e, "range", "rangeRound", "interpolate", "clamp") }

        function ri(t, e) { return Qo(t, Zo(oi(t, e)[2])), Qo(t, Zo(oi(t, e)[2])), t }

        function oi(t, e) {
            null == e && (e = 10);
            var n = Xo(t),
                r = n[1] - n[0],
                o = Math.pow(10, Math.floor(Math.log(r / e) / Math.LN10)),
                i = e / r * o;
            return i <= .15 ? o *= 10 : i <= .35 ? o *= 5 : i <= .75 && (o *= 2), n[0] = Math.ceil(n[0] / o) * o, n[1] = Math.floor(n[1] / o) * o + .5 * o, n[2] = o, n
        }

        function ii(t, e) { return ha.range.apply(ha, oi(t, e)) }

        function ai(t, e, n) {
            var r = oi(t, e);
            if (n) {
                var o = vu.exec(n);
                if (o.shift(), "s" === o[8]) {
                    var i = ha.formatPrefix(Math.max(Ca(r[0]), Ca(r[1])));
                    return o[7] || (o[7] = "." + ui(i.scale(r[2]))), o[8] = "f", n = ha.format(o.join("")),
                        function (t) { return n(i.scale(t)) + i.symbol }
                }
                o[7] || (o[7] = "." + si(o[8], r)), n = o.join("")
            } else n = ",." + ui(r[2]) + "f";
            return ha.format(n)
        }

        function ui(t) { return -Math.floor(Math.log(t) / Math.LN10 + .01) }

        function si(t, e) { var n = ui(e[2]); return t in Ss ? Math.abs(n - ui(Math.max(Ca(e[0]), Ca(e[1])))) + +("e" !== t) : n - 2 * ("%" === t) }

        function ci(t, e, n, r) {
            function o(t) { return (n ? Math.log(t < 0 ? 0 : t) : -Math.log(t > 0 ? 0 : -t)) / Math.log(e) }

            function i(t) { return n ? Math.pow(e, t) : -Math.pow(e, -t) }

            function a(e) { return t(o(e)) }
            return a.invert = function (e) { return i(t.invert(e)) }, a.domain = function (e) { return arguments.length ? (n = e[0] >= 0, t.domain((r = e.map(Number)).map(o)), a) : r }, a.base = function (n) { return arguments.length ? (e = +n, t.domain(r.map(o)), a) : e }, a.nice = function () { var e = Qo(r.map(o), n ? Math : Os); return t.domain(e), r = e.map(i), a }, a.ticks = function () {
                var t = Xo(r),
                    a = [],
                    u = t[0],
                    s = t[1],
                    c = Math.floor(o(u)),
                    l = Math.ceil(o(s)),
                    f = e % 1 ? 2 : e;
                if (isFinite(l - c)) {
                    if (n) {
                        for (; c < l; c++)
                            for (var p = 1; p < f; p++) a.push(i(c) * p);
                        a.push(i(c))
                    } else
                        for (a.push(i(c)); c++ < l;)
                            for (var p = f - 1; p > 0; p--) a.push(i(c) * p);
                    for (c = 0; a[c] < u; c++);
                    for (l = a.length; a[l - 1] > s; l--);
                    a = a.slice(c, l)
                }
                return a
            }, a.tickFormat = function (t, n) {
                if (!arguments.length) return Ns;
                arguments.length < 2 ? n = Ns : "function" != typeof n && (n = ha.format(n));
                var r = Math.max(1, e * t / a.ticks().length);
                return function (t) { var a = t / i(Math.round(o(t))); return a * e < e - .5 && (a *= e), a <= r ? n(t) : "" }
            }, a.copy = function () { return ci(t.copy(), e, n, r) }, ni(a, t)
        }

        function li(t, e, n) {
            function r(e) { return t(o(e)) }
            var o = fi(e),
                i = fi(1 / e);
            return r.invert = function (e) { return i(t.invert(e)) }, r.domain = function (e) { return arguments.length ? (t.domain((n = e.map(Number)).map(o)), r) : n }, r.ticks = function (t) { return ii(n, t) }, r.tickFormat = function (t, e) { return ai(n, t, e) }, r.nice = function (t) { return r.domain(ri(n, t)) }, r.exponent = function (a) { return arguments.length ? (o = fi(e = a), i = fi(1 / e), t.domain(n.map(o)), r) : e }, r.copy = function () { return li(t.copy(), e, n) }, ni(r, t)
        }

        function fi(t) { return function (e) { return e < 0 ? -Math.pow(-e, t) : Math.pow(e, t) } }

        function pi(t, e) {
            function n(n) { return i[((o.get(n) || ("range" === e.t ? o.set(n, t.push(n)) : NaN)) - 1) % i.length] }

            function r(e, n) { return ha.range(t.length).map(function (t) { return e + n * t }) }
            var o, i, a;
            return n.domain = function (r) {
                if (!arguments.length) return t;
                t = [], o = new d;
                for (var i, a = -1, u = r.length; ++a < u;) o.has(i = r[a]) || o.set(i, t.push(i));
                return n[e.t].apply(n, e.a)
            }, n.range = function (t) { return arguments.length ? (i = t, a = 0, e = { t: "range", a: arguments }, n) : i }, n.rangePoints = function (o, u) {
                arguments.length < 2 && (u = 0);
                var s = o[0],
                    c = o[1],
                    l = t.length < 2 ? (s = (s + c) / 2, 0) : (c - s) / (t.length - 1 + u);
                return i = r(s + l * u / 2, l), a = 0, e = { t: "rangePoints", a: arguments }, n
            }, n.rangeRoundPoints = function (o, u) {
                arguments.length < 2 && (u = 0);
                var s = o[0],
                    c = o[1],
                    l = t.length < 2 ? (s = c = Math.round((s + c) / 2), 0) : (c - s) / (t.length - 1 + u) | 0;
                return i = r(s + Math.round(l * u / 2 + (c - s - (t.length - 1 + u) * l) / 2), l), a = 0, e = { t: "rangeRoundPoints", a: arguments }, n
            }, n.rangeBands = function (o, u, s) {
                arguments.length < 2 && (u = 0), arguments.length < 3 && (s = u);
                var c = o[1] < o[0],
                    l = o[c - 0],
                    f = o[1 - c],
                    p = (f - l) / (t.length - u + 2 * s);
                return i = r(l + p * s, p), c && i.reverse(), a = p * (1 - u), e = { t: "rangeBands", a: arguments }, n
            }, n.rangeRoundBands = function (o, u, s) {
                arguments.length < 2 && (u = 0), arguments.length < 3 && (s = u);
                var c = o[1] < o[0],
                    l = o[c - 0],
                    f = o[1 - c],
                    p = Math.floor((f - l) / (t.length - u + 2 * s));
                return i = r(l + Math.round((f - l - (t.length - u) * p) / 2), p), c && i.reverse(), a = Math.round(p * (1 - u)), e = { t: "rangeRoundBands", a: arguments }, n
            }, n.rangeBand = function () { return a }, n.rangeExtent = function () { return Xo(e.a[0]) }, n.copy = function () { return pi(t, e) }, n.domain(t)
        }

        function hi(t, e) {
            function n() {
                var n = 0,
                    i = e.length;
                for (o = []; ++n < i;) o[n - 1] = ha.quantile(t, n / i);
                return r
            }

            function r(t) { if (!isNaN(t = +t)) return e[ha.bisect(o, t)] }
            var o;
            return r.domain = function (e) { return arguments.length ? (t = e.map(s).filter(c).sort(u), n()) : t }, r.range = function (t) { return arguments.length ? (e = t, n()) : e }, r.quantiles = function () { return o }, r.invertExtent = function (n) { return n = e.indexOf(n), n < 0 ? [NaN, NaN] : [n > 0 ? o[n - 1] : t[0], n < o.length ? o[n] : t[t.length - 1]] }, r.copy = function () { return hi(t, e) }, n()
        }

        function di(t, e, n) {
            function r(e) { return n[Math.max(0, Math.min(a, Math.floor(i * (e - t))))] }

            function o() { return i = n.length / (e - t), a = n.length - 1, r }
            var i, a;
            return r.domain = function (n) { return arguments.length ? (t = +n[0], e = +n[n.length - 1], o()) : [t, e] }, r.range = function (t) { return arguments.length ? (n = t, o()) : n }, r.invertExtent = function (e) { return e = n.indexOf(e), e = e < 0 ? NaN : e / i + t, [e, e + 1 / i] }, r.copy = function () { return di(t, e, n) }, o()
        }

        function vi(t, e) {
            function n(n) { if (n <= n) return e[ha.bisect(t, n)] }
            return n.domain = function (e) { return arguments.length ? (t = e, n) : t }, n.range = function (t) { return arguments.length ? (e = t, n) : e }, n.invertExtent = function (n) { return n = e.indexOf(n), [t[n - 1], t[n]] }, n.copy = function () { return vi(t, e) }, n
        }

        function gi(t) {
            function e(t) { return +t }
            return e.invert = e, e.domain = e.range = function (n) { return arguments.length ? (t = n.map(e), e) : t }, e.ticks = function (e) { return ii(t, e) }, e.tickFormat = function (e, n) { return ai(t, e, n) }, e.copy = function () { return gi(t) }, e
        }

        function yi() { return 0 }

        function mi(t) { return t.innerRadius }

        function bi(t) { return t.outerRadius }

        function xi(t) { return t.startAngle }

        function wi(t) { return t.endAngle }

        function _i(t) { return t && t.padAngle }

        function Ci(t, e, n, r) { return (t - n) * e - (e - r) * t > 0 ? 0 : 1 }

        function Ei(t, e, n, r, o) {
            var i = t[0] - e[0],
                a = t[1] - e[1],
                u = (o ? r : -r) / Math.sqrt(i * i + a * a),
                s = u * a,
                c = -u * i,
                l = t[0] + s,
                f = t[1] + c,
                p = e[0] + s,
                h = e[1] + c,
                d = (l + p) / 2,
                v = (f + h) / 2,
                g = p - l,
                y = h - f,
                m = g * g + y * y,
                b = n - r,
                x = l * h - p * f,
                w = (y < 0 ? -1 : 1) * Math.sqrt(Math.max(0, b * b * m - x * x)),
                _ = (x * y - g * w) / m,
                C = (-x * g - y * w) / m,
                E = (x * y + g * w) / m,
                M = (-x * g + y * w) / m,
                k = _ - d,
                T = C - v,
                S = E - d,
                N = M - v;
            return k * k + T * T > S * S + N * N && (_ = E, C = M), [
                [_ - s, C - c],
                [_ * n / b, C * n / b]
            ]
        }

        function Mi(t) {
            function e(e) {
                function a() { c.push("M", i(t(l), u)) }
                for (var s, c = [], l = [], f = -1, p = e.length, h = At(n), d = At(r); ++f < p;) o.call(this, s = e[f], f) ? l.push([+h.call(this, s, f), +d.call(this, s, f)]) : l.length && (a(), l = []);
                return l.length && a(), c.length ? c.join("") : null
            }
            var n = Dn,
                r = In,
                o = De,
                i = ki,
                a = i.key,
                u = .7;
            return e.x = function (t) { return arguments.length ? (n = t, e) : n }, e.y = function (t) { return arguments.length ? (r = t, e) : r }, e.defined = function (t) { return arguments.length ? (o = t, e) : o }, e.interpolate = function (t) { return arguments.length ? (a = "function" == typeof t ? i = t : (i = Rs.get(t) || ki).key, e) : a }, e.tension = function (t) { return arguments.length ? (u = t, e) : u }, e
        }

        function ki(t) { return t.length > 1 ? t.join("L") : t + "Z" }

        function Ti(t) { return t.join("L") + "Z" }

        function Si(t) { for (var e = 0, n = t.length, r = t[0], o = [r[0], ",", r[1]]; ++e < n;) o.push("H", (r[0] + (r = t[e])[0]) / 2, "V", r[1]); return n > 1 && o.push("H", r[0]), o.join("") }

        function Ni(t) { for (var e = 0, n = t.length, r = t[0], o = [r[0], ",", r[1]]; ++e < n;) o.push("V", (r = t[e])[1], "H", r[0]); return o.join("") }

        function Oi(t) { for (var e = 0, n = t.length, r = t[0], o = [r[0], ",", r[1]]; ++e < n;) o.push("H", (r = t[e])[0], "V", r[1]); return o.join("") }

        function Ai(t, e) { return t.length < 4 ? ki(t) : t[1] + Di(t.slice(1, -1), Ii(t, e)) }

        function Pi(t, e) { return t.length < 3 ? Ti(t) : t[0] + Di((t.push(t[0]), t), Ii([t[t.length - 2]].concat(t, [t[1]]), e)) }

        function ji(t, e) { return t.length < 3 ? ki(t) : t[0] + Di(t, Ii(t, e)) }

        function Di(t, e) {
            if (e.length < 1 || t.length != e.length && t.length != e.length + 2) return ki(t);
            var n = t.length != e.length,
                r = "",
                o = t[0],
                i = t[1],
                a = e[0],
                u = a,
                s = 1;
            if (n && (r += "Q" + (i[0] - 2 * a[0] / 3) + "," + (i[1] - 2 * a[1] / 3) + "," + i[0] + "," + i[1], o = t[1], s = 2), e.length > 1) { u = e[1], i = t[s], s++ , r += "C" + (o[0] + a[0]) + "," + (o[1] + a[1]) + "," + (i[0] - u[0]) + "," + (i[1] - u[1]) + "," + i[0] + "," + i[1]; for (var c = 2; c < e.length; c++ , s++) i = t[s], u = e[c], r += "S" + (i[0] - u[0]) + "," + (i[1] - u[1]) + "," + i[0] + "," + i[1] }
            if (n) {
                var l = t[s];
                r += "Q" + (i[0] + 2 * u[0] / 3) + "," + (i[1] + 2 * u[1] / 3) + "," + l[0] + "," + l[1]
            }
            return r
        }

        function Ii(t, e) { for (var n, r = [], o = (1 - e) / 2, i = t[0], a = t[1], u = 1, s = t.length; ++u < s;) n = i, i = a, a = t[u], r.push([o * (a[0] - n[0]), o * (a[1] - n[1])]); return r }

        function Ri(t) {
            if (t.length < 3) return ki(t);
            var e = 1,
                n = t.length,
                r = t[0],
                o = r[0],
                i = r[1],
                a = [o, o, o, (r = t[1])[0]],
                u = [i, i, i, r[1]],
                s = [o, ",", i, "L", qi(Us, a), ",", qi(Us, u)];
            for (t.push(t[n - 1]); ++e <= n;) r = t[e], a.shift(), a.push(r[0]), u.shift(), u.push(r[1]), Hi(s, a, u);
            return t.pop(), s.push("L", r), s.join("")
        }

        function Li(t) { if (t.length < 4) return ki(t); for (var e, n = [], r = -1, o = t.length, i = [0], a = [0]; ++r < 3;) e = t[r], i.push(e[0]), a.push(e[1]); for (n.push(qi(Us, i) + "," + qi(Us, a)), --r; ++r < o;) e = t[r], i.shift(), i.push(e[0]), a.shift(), a.push(e[1]), Hi(n, i, a); return n.join("") }

        function Fi(t) { for (var e, n, r = -1, o = t.length, i = o + 4, a = [], u = []; ++r < 4;) n = t[r % o], a.push(n[0]), u.push(n[1]); for (e = [qi(Us, a), ",", qi(Us, u)], --r; ++r < i;) n = t[r % o], a.shift(), a.push(n[0]), u.shift(), u.push(n[1]), Hi(e, a, u); return e.join("") }

        function Ui(t, e) {
            var n = t.length - 1;
            if (n)
                for (var r, o, i = t[0][0], a = t[0][1], u = t[n][0] - i, s = t[n][1] - a, c = -1; ++c <= n;) r = t[c], o = c / n, r[0] = e * r[0] + (1 - e) * (i + o * u), r[1] = e * r[1] + (1 - e) * (a + o * s);
            return Ri(t)
        }

        function qi(t, e) { return t[0] * e[0] + t[1] * e[1] + t[2] * e[2] + t[3] * e[3] }

        function Hi(t, e, n) { t.push("C", qi(Ls, e), ",", qi(Ls, n), ",", qi(Fs, e), ",", qi(Fs, n), ",", qi(Us, e), ",", qi(Us, n)) }

        function Bi(t, e) { return (e[1] - t[1]) / (e[0] - t[0]) }

        function Wi(t) { for (var e = 0, n = t.length - 1, r = [], o = t[0], i = t[1], a = r[0] = Bi(o, i); ++e < n;) r[e] = (a + (a = Bi(o = i, i = t[e + 1]))) / 2; return r[e] = a, r }

        function zi(t) { for (var e, n, r, o, i = [], a = Wi(t), u = -1, s = t.length - 1; ++u < s;) e = Bi(t[u], t[u + 1]), Ca(e) < qa ? a[u] = a[u + 1] = 0 : (n = a[u] / e, r = a[u + 1] / e, o = n * n + r * r, o > 9 && (o = 3 * e / Math.sqrt(o), a[u] = o * n, a[u + 1] = o * r)); for (u = -1; ++u <= s;) o = (t[Math.min(s, u + 1)][0] - t[Math.max(0, u - 1)][0]) / (6 * (1 + a[u] * a[u])), i.push([o || 0, a[u] * o || 0]); return i }

        function Vi(t) { return t.length < 3 ? ki(t) : t[0] + Di(t, zi(t)) }

        function Yi(t) { for (var e, n, r, o = -1, i = t.length; ++o < i;) e = t[o], n = e[0], r = e[1] - Va, e[0] = n * Math.cos(r), e[1] = n * Math.sin(r); return t }

        function $i(t) {
            function e(e) {
                function s() { v.push("M", u(t(y), f), l, c(t(g.reverse()), f), "Z") }
                for (var p, h, d, v = [], g = [], y = [], m = -1, b = e.length, x = At(n), w = At(o), _ = n === r ? function () { return h } : At(r), C = o === i ? function () { return d } : At(i); ++m < b;) a.call(this, p = e[m], m) ? (g.push([h = +x.call(this, p, m), d = +w.call(this, p, m)]), y.push([+_.call(this, p, m), +C.call(this, p, m)])) : g.length && (s(), g = [], y = []);
                return g.length && s(), v.length ? v.join("") : null
            }
            var n = Dn,
                r = Dn,
                o = 0,
                i = In,
                a = De,
                u = ki,
                s = u.key,
                c = u,
                l = "L",
                f = .7;
            return e.x = function (t) { return arguments.length ? (n = r = t, e) : r }, e.x0 = function (t) { return arguments.length ? (n = t, e) : n }, e.x1 = function (t) { return arguments.length ? (r = t, e) : r }, e.y = function (t) { return arguments.length ? (o = i = t, e) : i }, e.y0 = function (t) { return arguments.length ? (o = t, e) : o }, e.y1 = function (t) { return arguments.length ? (i = t, e) : i }, e.defined = function (t) { return arguments.length ? (a = t, e) : a }, e.interpolate = function (t) { return arguments.length ? (s = "function" == typeof t ? u = t : (u = Rs.get(t) || ki).key, c = u.reverse || u, l = u.closed ? "M" : "L", e) : s }, e.tension = function (t) { return arguments.length ? (f = t, e) : f }, e
        }

        function Ki(t) { return t.radius }

        function Xi(t) { return [t.x, t.y] }

        function Gi(t) {
            return function () {
                var e = t.apply(this, arguments),
                    n = e[0],
                    r = e[1] - Va;
                return [n * Math.cos(r), n * Math.sin(r)]
            }
        }

        function Ji() { return 64 }

        function Qi() { return "circle" }

        function Zi(t) { var e = Math.sqrt(t / Ba); return "M0," + e + "A" + e + "," + e + " 0 1,1 0," + -e + "A" + e + "," + e + " 0 1,1 0," + e + "Z" }

        function ta(t) {
            return function () {
                var e, n, r;
                (e = this[t]) && (r = e[n = e.active]) && (r.timer.c = null, r.timer.t = NaN, --e.count ? delete e[n] : delete this[t], e.active += .5, r.event && r.event.interrupt.call(this, this.__data__, r.index))
            }
        }

        function ea(t, e, n) { return Sa(t, Ys), t.namespace = e, t.id = n, t }

        function na(t, e, n, r) {
            var o = t.id,
                i = t.namespace;
            return K(t, "function" == typeof n ? function (t, a, u) { t[i][o].tween.set(e, r(n.call(t, t.__data__, a, u))) } : (n = r(n), function (t) { t[i][o].tween.set(e, n) }))
        }

        function ra(t) {
            return null == t && (t = ""),
                function () { this.textContent = t }
        }

        function oa(t) {
            return null == t ? "__transition__" : "__transition_" + t + "__";
        }

        function ia(t, e, n, r, o) {
            function i(t) { var e = v.delay; return c.t = e + s, e <= t ? a(t - e) : void (c.c = a) }

            function a(n) {
                var o = h.active,
                    i = h[o];
                i && (i.timer.c = null, i.timer.t = NaN, --h.count, delete h[o], i.event && i.event.interrupt.call(t, t.__data__, i.index));
                for (var a in h)
                    if (+a < r) {
                        var d = h[a];
                        d.timer.c = null, d.timer.t = NaN, --h.count, delete h[a]
                    }
                c.c = u, Rt(function () { return c.c && u(n || 1) && (c.c = null, c.t = NaN), 1 }, 0, s), h.active = r, v.event && v.event.start.call(t, t.__data__, e), p = [], v.tween.forEach(function (n, r) {
                    (r = r.call(t, t.__data__, e)) && p.push(r)
                }), f = v.ease, l = v.duration
            }

            function u(o) { for (var i = o / l, a = f(i), u = p.length; u > 0;) p[--u].call(t, a); if (i >= 1) return v.event && v.event.end.call(t, t.__data__, e), --h.count ? delete h[r] : delete t[n], 1 }
            var s, c, l, f, p, h = t[n] || (t[n] = { active: 0, count: 0 }),
                v = h[r];
            v || (s = o.time, c = Rt(i, 0, s), v = h[r] = { tween: new d, time: s, timer: c, delay: o.delay, duration: o.duration, ease: o.ease, index: e }, o = null, ++h.count)
        }

        function aa(t, e, n) { t.attr("transform", function (t) { var r = e(t); return "translate(" + (isFinite(r) ? r : n(t)) + ",0)" }) }

        function ua(t, e, n) { t.attr("transform", function (t) { var r = e(t); return "translate(0," + (isFinite(r) ? r : n(t)) + ")" }) }

        function sa(t) { return t.toISOString() }

        function ca(t, e, n) {
            function r(e) { return t(e) }

            function o(t, n) {
                var r = t[1] - t[0],
                    o = r / n,
                    i = ha.bisect(ec, o);
                return i == ec.length ? [e.year, oi(t.map(function (t) { return t / 31536e6 }), n)[2]] : i ? e[o / ec[i - 1] < ec[i] / o ? i - 1 : i] : [oc, oi(t, n)[2]]
            }
            return r.invert = function (e) { return la(t.invert(e)) }, r.domain = function (e) { return arguments.length ? (t.domain(e), r) : t.domain().map(la) }, r.nice = function (t, e) {
                function n(n) { return !isNaN(n) && !t.range(n, la(+n + 1), e).length }
                var i = r.domain(),
                    a = Xo(i),
                    u = null == t ? o(a, 10) : "number" == typeof t && o(a, t);
                return u && (t = u[0], e = u[1]), r.domain(Qo(i, e > 1 ? { floor: function (e) { for (; n(e = t.floor(e));) e = la(e - 1); return e }, ceil: function (e) { for (; n(e = t.ceil(e));) e = la(+e + 1); return e } } : t))
            }, r.ticks = function (t, e) {
                var n = Xo(r.domain()),
                    i = null == t ? o(n, 10) : "number" == typeof t ? o(n, t) : !t.range && [{ range: t }, e];
                return i && (t = i[0], e = i[1]), t.range(n[0], la(+n[1] + 1), e < 1 ? 1 : e)
            }, r.tickFormat = function () { return n }, r.copy = function () { return ca(t.copy(), e, n) }, ni(r, t)
        }

        function la(t) { return new Date(t) }

        function fa(t) { return JSON.parse(t.responseText) }

        function pa(t) { var e = ga.createRange(); return e.selectNode(ga.body), e.createContextualFragment(t.responseText) }
        var ha = { version: "3.5.17" },
            da = [].slice,
            va = function (t) { return da.call(t) },
            ga = this.document;
        if (ga) try { va(ga.documentElement.childNodes)[0].nodeType } catch (t) { va = function (t) { for (var e = t.length, n = new Array(e); e--;) n[e] = t[e]; return n } }
        if (Date.now || (Date.now = function () { return +new Date }), ga) try { ga.createElement("DIV").style.setProperty("opacity", 0, "") } catch (t) {
            var ya = this.Element.prototype,
                ma = ya.setAttribute,
                ba = ya.setAttributeNS,
                xa = this.CSSStyleDeclaration.prototype,
                wa = xa.setProperty;
            ya.setAttribute = function (t, e) { ma.call(this, t, e + "") }, ya.setAttributeNS = function (t, e, n) { ba.call(this, t, e, n + "") }, xa.setProperty = function (t, e, n) { wa.call(this, t, e + "", n) }
        }
        ha.ascending = u, ha.descending = function (t, e) { return e < t ? -1 : e > t ? 1 : e >= t ? 0 : NaN }, ha.min = function (t, e) {
            var n, r, o = -1,
                i = t.length;
            if (1 === arguments.length) {
                for (; ++o < i;)
                    if (null != (r = t[o]) && r >= r) { n = r; break }
                for (; ++o < i;) null != (r = t[o]) && n > r && (n = r)
            } else {
                for (; ++o < i;)
                    if (null != (r = e.call(t, t[o], o)) && r >= r) { n = r; break }
                for (; ++o < i;) null != (r = e.call(t, t[o], o)) && n > r && (n = r)
            }
            return n
        }, ha.max = function (t, e) {
            var n, r, o = -1,
                i = t.length;
            if (1 === arguments.length) {
                for (; ++o < i;)
                    if (null != (r = t[o]) && r >= r) { n = r; break }
                for (; ++o < i;) null != (r = t[o]) && r > n && (n = r)
            } else {
                for (; ++o < i;)
                    if (null != (r = e.call(t, t[o], o)) && r >= r) { n = r; break }
                for (; ++o < i;) null != (r = e.call(t, t[o], o)) && r > n && (n = r)
            }
            return n
        }, ha.extent = function (t, e) {
            var n, r, o, i = -1,
                a = t.length;
            if (1 === arguments.length) {
                for (; ++i < a;)
                    if (null != (r = t[i]) && r >= r) { n = o = r; break }
                for (; ++i < a;) null != (r = t[i]) && (n > r && (n = r), o < r && (o = r))
            } else {
                for (; ++i < a;)
                    if (null != (r = e.call(t, t[i], i)) && r >= r) { n = o = r; break }
                for (; ++i < a;) null != (r = e.call(t, t[i], i)) && (n > r && (n = r), o < r && (o = r))
            }
            return [n, o]
        }, ha.sum = function (t, e) {
            var n, r = 0,
                o = t.length,
                i = -1;
            if (1 === arguments.length)
                for (; ++i < o;) c(n = +t[i]) && (r += n);
            else
                for (; ++i < o;) c(n = +e.call(t, t[i], i)) && (r += n);
            return r
        }, ha.mean = function (t, e) {
            var n, r = 0,
                o = t.length,
                i = -1,
                a = o;
            if (1 === arguments.length)
                for (; ++i < o;) c(n = s(t[i])) ? r += n : --a;
            else
                for (; ++i < o;) c(n = s(e.call(t, t[i], i))) ? r += n : --a;
            if (a) return r / a
        }, ha.quantile = function (t, e) {
            var n = (t.length - 1) * e + 1,
                r = Math.floor(n),
                o = +t[r - 1],
                i = n - r;
            return i ? o + i * (t[r] - o) : o
        }, ha.median = function (t, e) {
            var n, r = [],
                o = t.length,
                i = -1;
            if (1 === arguments.length)
                for (; ++i < o;) c(n = s(t[i])) && r.push(n);
            else
                for (; ++i < o;) c(n = s(e.call(t, t[i], i))) && r.push(n);
            if (r.length) return ha.quantile(r.sort(u), .5)
        }, ha.variance = function (t, e) {
            var n, r, o = t.length,
                i = 0,
                a = 0,
                u = -1,
                l = 0;
            if (1 === arguments.length)
                for (; ++u < o;) c(n = s(t[u])) && (r = n - i, i += r / ++l, a += r * (n - i));
            else
                for (; ++u < o;) c(n = s(e.call(t, t[u], u))) && (r = n - i, i += r / ++l, a += r * (n - i));
            if (l > 1) return a / (l - 1)
        }, ha.deviation = function () { var t = ha.variance.apply(this, arguments); return t ? Math.sqrt(t) : t };
        var _a = l(u);
        ha.bisectLeft = _a.left, ha.bisect = ha.bisectRight = _a.right, ha.bisector = function (t) { return l(1 === t.length ? function (e, n) { return u(t(e), n) } : t) }, ha.shuffle = function (t, e, n) {
            (i = arguments.length) < 3 && (n = t.length, i < 2 && (e = 0));
            for (var r, o, i = n - e; i;) o = Math.random() * i-- | 0, r = t[i + e], t[i + e] = t[o + e], t[o + e] = r;
            return t
        }, ha.permute = function (t, e) { for (var n = e.length, r = new Array(n); n--;) r[n] = t[e[n]]; return r }, ha.pairs = function (t) { for (var e, n = 0, r = t.length - 1, o = t[0], i = new Array(r < 0 ? 0 : r); n < r;) i[n] = [e = o, o = t[++n]]; return i }, ha.transpose = function (t) {
            if (!(o = t.length)) return [];
            for (var e = -1, n = ha.min(t, f), r = new Array(n); ++e < n;)
                for (var o, i = -1, a = r[e] = new Array(o); ++i < o;) a[i] = t[i][e];
            return r
        }, ha.zip = function () { return ha.transpose(arguments) }, ha.keys = function (t) { var e = []; for (var n in t) e.push(n); return e }, ha.values = function (t) { var e = []; for (var n in t) e.push(t[n]); return e }, ha.entries = function (t) { var e = []; for (var n in t) e.push({ key: n, value: t[n] }); return e }, ha.merge = function (t) {
            for (var e, n, r, o = t.length, i = -1, a = 0; ++i < o;) a += t[i].length;
            for (n = new Array(a); --o >= 0;)
                for (r = t[o], e = r.length; --e >= 0;) n[--a] = r[e];
            return n
        };
        var Ca = Math.abs;
        ha.range = function (t, e, n) {
            if (arguments.length < 3 && (n = 1, arguments.length < 2 && (e = t, t = 0)), (e - t) / n === 1 / 0) throw new Error("infinite range");
            var r, o = [],
                i = p(Ca(n)),
                a = -1;
            if (t *= i, e *= i, n *= i, n < 0)
                for (;
                    (r = t + n * ++a) > e;) o.push(r / i);
            else
                for (;
                    (r = t + n * ++a) < e;) o.push(r / i);
            return o
        }, ha.map = function (t, e) {
            var n = new d;
            if (t instanceof d) t.forEach(function (t, e) { n.set(t, e) });
            else if (Array.isArray(t)) {
                var r, o = -1,
                    i = t.length;
                if (1 === arguments.length)
                    for (; ++o < i;) n.set(o, t[o]);
                else
                    for (; ++o < i;) n.set(e.call(t, r = t[o], o), r)
            } else
                for (var a in t) n.set(a, t[a]);
            return n
        };
        var Ea = "__proto__",
            Ma = "\0";
        h(d, { has: y, get: function (t) { return this._[v(t)] }, set: function (t, e) { return this._[v(t)] = e }, remove: m, keys: b, values: function () { var t = []; for (var e in this._) t.push(this._[e]); return t }, entries: function () { var t = []; for (var e in this._) t.push({ key: g(e), value: this._[e] }); return t }, size: x, empty: w, forEach: function (t) { for (var e in this._) t.call(this, g(e), this._[e]) } }), ha.nest = function () {
            function t(e, a, u) { if (u >= i.length) return r ? r.call(o, a) : n ? a.sort(n) : a; for (var s, c, l, f, p = -1, h = a.length, v = i[u++], g = new d; ++p < h;)(f = g.get(s = v(c = a[p]))) ? f.push(c) : g.set(s, [c]); return e ? (c = e(), l = function (n, r) { c.set(n, t(e, r, u)) }) : (c = {}, l = function (n, r) { c[n] = t(e, r, u) }), g.forEach(l), c }

            function e(t, n) {
                if (n >= i.length) return t;
                var r = [],
                    o = a[n++];
                return t.forEach(function (t, o) { r.push({ key: t, values: e(o, n) }) }), o ? r.sort(function (t, e) { return o(t.key, e.key) }) : r
            }
            var n, r, o = {},
                i = [],
                a = [];
            return o.map = function (e, n) { return t(n, e, 0) }, o.entries = function (n) { return e(t(ha.map, n, 0), 0) }, o.key = function (t) { return i.push(t), o }, o.sortKeys = function (t) { return a[i.length - 1] = t, o }, o.sortValues = function (t) { return n = t, o }, o.rollup = function (t) { return r = t, o }, o
        }, ha.set = function (t) {
            var e = new _;
            if (t)
                for (var n = 0, r = t.length; n < r; ++n) e.add(t[n]);
            return e
        }, h(_, { has: y, add: function (t) { return this._[v(t += "")] = !0, t }, remove: m, values: b, size: x, empty: w, forEach: function (t) { for (var e in this._) t.call(this, g(e)) } }), ha.behavior = {}, ha.rebind = function (t, e) { for (var n, r = 1, o = arguments.length; ++r < o;) t[n = arguments[r]] = E(t, e, e[n]); return t };
        var ka = ["webkit", "ms", "moz", "Moz", "o", "O"];
        ha.dispatch = function () { for (var t = new T, e = -1, n = arguments.length; ++e < n;) t[arguments[e]] = S(t); return t }, T.prototype.on = function (t, e) {
            var n = t.indexOf("."),
                r = "";
            if (n >= 0 && (r = t.slice(n + 1), t = t.slice(0, n)), t) return arguments.length < 2 ? this[t].on(r) : this[t].on(r, e);
            if (2 === arguments.length) {
                if (null == e)
                    for (t in this) this.hasOwnProperty(t) && this[t].on(r, null);
                return this
            }
        }, ha.event = null, ha.requote = function (t) { return t.replace(Ta, "\\$&") };
        var Ta = /[\\\^\$\*\+\?\|\[\]\(\)\.\{\}]/g,
            Sa = {}.__proto__ ? function (t, e) { t.__proto__ = e } : function (t, e) { for (var n in e) t[n] = e[n] },
            Na = function (t, e) { return e.querySelector(t) },
            Oa = function (t, e) { return e.querySelectorAll(t) },
            Aa = function (t, e) { var n = t.matches || t[M(t, "matchesSelector")]; return (Aa = function (t, e) { return n.call(t, e) })(t, e) };
        "function" == typeof Sizzle && (Na = function (t, e) { return Sizzle(t, e)[0] || null }, Oa = Sizzle, Aa = Sizzle.matchesSelector), ha.selection = function () { return ha.select(ga.documentElement) };
        var Pa = ha.selection.prototype = [];
        Pa.select = function (t) {
            var e, n, r, o, i = [];
            t = j(t);
            for (var a = -1, u = this.length; ++a < u;) { i.push(e = []), e.parentNode = (r = this[a]).parentNode; for (var s = -1, c = r.length; ++s < c;)(o = r[s]) ? (e.push(n = t.call(o, o.__data__, s, a)), n && "__data__" in o && (n.__data__ = o.__data__)) : e.push(null) }
            return P(i)
        }, Pa.selectAll = function (t) {
            var e, n, r = [];
            t = D(t);
            for (var o = -1, i = this.length; ++o < i;)
                for (var a = this[o], u = -1, s = a.length; ++u < s;)(n = a[u]) && (r.push(e = va(t.call(n, n.__data__, u, o))), e.parentNode = n);
            return P(r)
        };
        var ja = "http://www.w3.org/1999/xhtml",
            Da = { svg: "http://www.w3.org/2000/svg", xhtml: ja, xlink: "http://www.w3.org/1999/xlink", xml: "http://www.w3.org/XML/1998/namespace", xmlns: "http://www.w3.org/2000/xmlns/" };
        ha.ns = {
            prefix: Da,
            qualify: function (t) {
                var e = t.indexOf(":"),
                    n = t;
                return e >= 0 && "xmlns" !== (n = t.slice(0, e)) && (t = t.slice(e + 1)), Da.hasOwnProperty(n) ? { space: Da[n], local: t } : t
            }
        }, Pa.attr = function (t, e) { if (arguments.length < 2) { if ("string" == typeof t) { var n = this.node(); return t = ha.ns.qualify(t), t.local ? n.getAttributeNS(t.space, t.local) : n.getAttribute(t) } for (e in t) this.each(I(e, t[e])); return this } return this.each(I(t, e)) }, Pa.classed = function (t, e) {
            if (arguments.length < 2) {
                if ("string" == typeof t) {
                    var n = this.node(),
                        r = (t = F(t)).length,
                        o = -1;
                    if (e = n.classList) {
                        for (; ++o < r;)
                            if (!e.contains(t[o])) return !1
                    } else
                        for (e = n.getAttribute("class"); ++o < r;)
                            if (!L(t[o]).test(e)) return !1; return !0
                }
                for (e in t) this.each(U(e, t[e]));
                return this
            }
            return this.each(U(t, e))
        }, Pa.style = function (t, e, n) {
            var r = arguments.length;
            if (r < 3) {
                if ("string" != typeof t) { r < 2 && (e = ""); for (n in t) this.each(H(n, t[n], e)); return this }
                if (r < 2) { var o = this.node(); return a(o).getComputedStyle(o, null).getPropertyValue(t) }
                n = ""
            }
            return this.each(H(t, e, n))
        }, Pa.property = function (t, e) { if (arguments.length < 2) { if ("string" == typeof t) return this.node()[t]; for (e in t) this.each(B(e, t[e])); return this } return this.each(B(t, e)) }, Pa.text = function (t) {
            return arguments.length ? this.each("function" == typeof t ? function () {
                var e = t.apply(this, arguments);
                this.textContent = null == e ? "" : e
            } : null == t ? function () { this.textContent = "" } : function () { this.textContent = t }) : this.node().textContent
        }, Pa.html = function (t) {
            return arguments.length ? this.each("function" == typeof t ? function () {
                var e = t.apply(this, arguments);
                this.innerHTML = null == e ? "" : e
            } : null == t ? function () { this.innerHTML = "" } : function () { this.innerHTML = t }) : this.node().innerHTML
        }, Pa.append = function (t) { return t = W(t), this.select(function () { return this.appendChild(t.apply(this, arguments)) }) }, Pa.insert = function (t, e) { return t = W(t), e = j(e), this.select(function () { return this.insertBefore(t.apply(this, arguments), e.apply(this, arguments) || null) }) }, Pa.remove = function () { return this.each(z) }, Pa.data = function (t, e) {
            function n(t, n) {
                var r, o, i, a = t.length,
                    l = n.length,
                    f = Math.min(a, l),
                    p = new Array(l),
                    h = new Array(l),
                    v = new Array(a);
                if (e) {
                    var g, y = new d,
                        m = new Array(a);
                    for (r = -1; ++r < a;)(o = t[r]) && (y.has(g = e.call(o, o.__data__, r)) ? v[r] = o : y.set(g, o), m[r] = g);
                    for (r = -1; ++r < l;)(o = y.get(g = e.call(n, i = n[r], r))) ? o !== !0 && (p[r] = o, o.__data__ = i) : h[r] = V(i), y.set(g, !0);
                    for (r = -1; ++r < a;) r in m && y.get(m[r]) !== !0 && (v[r] = t[r])
                } else { for (r = -1; ++r < f;) o = t[r], i = n[r], o ? (o.__data__ = i, p[r] = o) : h[r] = V(i); for (; r < l; ++r) h[r] = V(n[r]); for (; r < a; ++r) v[r] = t[r] }
                h.update = p, h.parentNode = p.parentNode = v.parentNode = t.parentNode, u.push(h), s.push(p), c.push(v)
            }
            var r, o, i = -1,
                a = this.length;
            if (!arguments.length) { for (t = new Array(a = (r = this[0]).length); ++i < a;)(o = r[i]) && (t[i] = o.__data__); return t }
            var u = X([]),
                s = P([]),
                c = P([]);
            if ("function" == typeof t)
                for (; ++i < a;) n(r = this[i], t.call(r, r.parentNode.__data__, i));
            else
                for (; ++i < a;) n(r = this[i], t);
            return s.enter = function () { return u }, s.exit = function () { return c }, s
        }, Pa.datum = function (t) { return arguments.length ? this.property("__data__", t) : this.property("__data__") }, Pa.filter = function (t) { var e, n, r, o = []; "function" != typeof t && (t = Y(t)); for (var i = 0, a = this.length; i < a; i++) { o.push(e = []), e.parentNode = (n = this[i]).parentNode; for (var u = 0, s = n.length; u < s; u++)(r = n[u]) && t.call(r, r.__data__, u, i) && e.push(r) } return P(o) }, Pa.order = function () {
            for (var t = -1, e = this.length; ++t < e;)
                for (var n, r = this[t], o = r.length - 1, i = r[o]; --o >= 0;)(n = r[o]) && (i && i !== n.nextSibling && i.parentNode.insertBefore(n, i), i = n);
            return this
        }, Pa.sort = function (t) { t = $.apply(this, arguments); for (var e = -1, n = this.length; ++e < n;) this[e].sort(t); return this.order() }, Pa.each = function (t) { return K(this, function (e, n, r) { t.call(e, e.__data__, n, r) }) }, Pa.call = function (t) { var e = va(arguments); return t.apply(e[0] = this, e), this }, Pa.empty = function () { return !this.node() }, Pa.node = function () {
            for (var t = 0, e = this.length; t < e; t++)
                for (var n = this[t], r = 0, o = n.length; r < o; r++) { var i = n[r]; if (i) return i }
            return null
        }, Pa.size = function () { var t = 0; return K(this, function () { ++t }), t };
        var Ia = [];
        ha.selection.enter = X, ha.selection.enter.prototype = Ia, Ia.append = Pa.append, Ia.empty = Pa.empty, Ia.node = Pa.node, Ia.call = Pa.call, Ia.size = Pa.size, Ia.select = function (t) { for (var e, n, r, o, i, a = [], u = -1, s = this.length; ++u < s;) { r = (o = this[u]).update, a.push(e = []), e.parentNode = o.parentNode; for (var c = -1, l = o.length; ++c < l;)(i = o[c]) ? (e.push(r[c] = n = t.call(o.parentNode, i.__data__, c, u)), n.__data__ = i.__data__) : e.push(null) } return P(a) }, Ia.insert = function (t, e) { return arguments.length < 2 && (e = G(this)), Pa.insert.call(this, t, e) }, ha.select = function (t) { var e; return "string" == typeof t ? (e = [Na(t, ga)], e.parentNode = ga.documentElement) : (e = [t], e.parentNode = i(t)), P([e]) }, ha.selectAll = function (t) { var e; return "string" == typeof t ? (e = va(Oa(t, ga)), e.parentNode = ga.documentElement) : (e = va(t), e.parentNode = null), P([e]) }, Pa.on = function (t, e, n) {
            var r = arguments.length;
            if (r < 3) {
                if ("string" != typeof t) { r < 2 && (e = !1); for (n in t) this.each(J(n, t[n], e)); return this }
                if (r < 2) return (r = this.node()["__on" + t]) && r._;
                n = !1
            }
            return this.each(J(t, e, n))
        };
        var Ra = ha.map({ mouseenter: "mouseover", mouseleave: "mouseout" });
        ga && Ra.forEach(function (t) { "on" + t in ga && Ra.remove(t) });
        var La, Fa = 0;
        ha.mouse = function (t) { return et(t, O()) };
        var Ua = this.navigator && /WebKit/.test(this.navigator.userAgent) ? -1 : 0;
        ha.touch = function (t, e, n) {
            if (arguments.length < 3 && (n = e, e = O().changedTouches), e)
                for (var r, o = 0, i = e.length; o < i; ++o)
                    if ((r = e[o]).identifier === n) return et(t, r)
        }, ha.behavior.drag = function () {
            function t() { this.on("mousedown.drag", o).on("touchstart.drag", i) }

            function e(t, e, o, i, a) {
                return function () {
                    function u() {
                        var t, n, r = e(p, v);
                        r && (t = r[0] - b[0], n = r[1] - b[1], d |= t | n, b = r, h({ type: "drag", x: r[0] + c[0], y: r[1] + c[1], dx: t, dy: n }))
                    }

                    function s() { e(p, v) && (y.on(i + g, null).on(a + g, null), m(d), h({ type: "dragend" })) }
                    var c, l = this,
                        f = ha.event.target.correspondingElement || ha.event.target,
                        p = l.parentNode,
                        h = n.of(l, arguments),
                        d = 0,
                        v = t(),
                        g = ".drag" + (null == v ? "" : "-" + v),
                        y = ha.select(o(f)).on(i + g, u).on(a + g, s),
                        m = tt(f),
                        b = e(p, v);
                    r ? (c = r.apply(l, arguments), c = [c.x - b[0], c.y - b[1]]) : c = [0, 0], h({ type: "dragstart" })
                }
            }
            var n = A(t, "drag", "dragstart", "dragend"),
                r = null,
                o = e(k, ha.mouse, a, "mousemove", "mouseup"),
                i = e(nt, ha.touch, C, "touchmove", "touchend");
            return t.origin = function (e) { return arguments.length ? (r = e, t) : r }, ha.rebind(t, n, "on")
        }, ha.touches = function (t, e) { return arguments.length < 2 && (e = O().touches), e ? va(e).map(function (e) { var n = et(t, e); return n.identifier = e.identifier, n }) : [] };
        var qa = 1e-6,
            Ha = qa * qa,
            Ba = Math.PI,
            Wa = 2 * Ba,
            za = Wa - qa,
            Va = Ba / 2,
            Ya = Ba / 180,
            $a = 180 / Ba,
            Ka = Math.SQRT2,
            Xa = 2,
            Ga = 4;
        ha.interpolateZoom = function (t, e) {
            var n, r, o = t[0],
                i = t[1],
                a = t[2],
                u = e[0],
                s = e[1],
                c = e[2],
                l = u - o,
                f = s - i,
                p = l * l + f * f;
            if (p < Ha) r = Math.log(c / a) / Ka, n = function (t) { return [o + t * l, i + t * f, a * Math.exp(Ka * t * r)] };
            else {
                var h = Math.sqrt(p),
                    d = (c * c - a * a + Ga * p) / (2 * a * Xa * h),
                    v = (c * c - a * a - Ga * p) / (2 * c * Xa * h),
                    g = Math.log(Math.sqrt(d * d + 1) - d),
                    y = Math.log(Math.sqrt(v * v + 1) - v);
                r = (y - g) / Ka, n = function (t) {
                    var e = t * r,
                        n = st(g),
                        u = a / (Xa * h) * (n * ct(Ka * e + g) - ut(g));
                    return [o + u * l, i + u * f, a * n / st(Ka * e + g)]
                }
            }
            return n.duration = 1e3 * r, n
        }, ha.behavior.zoom = function () {
            function t(t) { t.on(O, f).on(Qa + ".zoom", h).on("dblclick.zoom", d).on(D, p) }

            function e(t) { return [(t[0] - E.x) / E.k, (t[1] - E.y) / E.k] }

            function n(t) { return [t[0] * E.k + E.x, t[1] * E.k + E.y] }

            function r(t) { E.k = Math.max(k[0], Math.min(k[1], t)) }

            function o(t, e) { e = n(e), E.x += t[0] - e[0], E.y += t[1] - e[1] }

            function i(e, n, i, a) { e.__chart__ = { x: E.x, y: E.y, k: E.k }, r(Math.pow(2, a)), o(g = n, i), e = ha.select(e), T > 0 && (e = e.transition().duration(T)), e.call(t.event) }

            function u() { w && w.domain(x.range().map(function (t) { return (t - E.x) / E.k }).map(x.invert)), C && C.domain(_.range().map(function (t) { return (t - E.y) / E.k }).map(_.invert)) }

            function s(t) { S++ || t({ type: "zoomstart" }) }

            function c(t) { u(), t({ type: "zoom", scale: E.k, translate: [E.x, E.y] }) }

            function l(t) { --S || (t({ type: "zoomend" }), g = null) }

            function f() {
                function t() { u = 1, o(ha.mouse(r), p), c(i) }

                function n() { f.on(P, null).on(j, null), h(u), l(i) }
                var r = this,
                    i = I.of(r, arguments),
                    u = 0,
                    f = ha.select(a(r)).on(P, t).on(j, n),
                    p = e(ha.mouse(r)),
                    h = tt(r);
                Vs.call(r), s(i)
            }

            function p() {
                function t() { var t = ha.touches(d); return h = E.k, t.forEach(function (t) { t.identifier in g && (g[t.identifier] = e(t)) }), t }

                function n() {
                    var e = ha.event.target;
                    ha.select(e).on(x, a).on(w, u), _.push(e);
                    for (var n = ha.event.changedTouches, r = 0, o = n.length; r < o; ++r) g[n[r].identifier] = null;
                    var s = t(),
                        c = Date.now();
                    if (1 === s.length) {
                        if (c - b < 500) {
                            var l = s[0];
                            i(d, l, g[l.identifier], Math.floor(Math.log(E.k) / Math.LN2) + 1), N()
                        }
                        b = c
                    } else if (s.length > 1) {
                        var l = s[0],
                            f = s[1],
                            p = l[0] - f[0],
                            h = l[1] - f[1];
                        y = p * p + h * h
                    }
                }

                function a() {
                    var t, e, n, i, a = ha.touches(d);
                    Vs.call(d);
                    for (var u = 0, s = a.length; u < s; ++u, i = null)
                        if (n = a[u], i = g[n.identifier]) {
                            if (e) break;
                            t = n, e = i
                        }
                    if (i) {
                        var l = (l = n[0] - t[0]) * l + (l = n[1] - t[1]) * l,
                            f = y && Math.sqrt(l / y);
                        t = [(t[0] + n[0]) / 2, (t[1] + n[1]) / 2], e = [(e[0] + i[0]) / 2, (e[1] + i[1]) / 2], r(f * h)
                    }
                    b = null, o(t, e), c(v)
                }

                function u() {
                    if (ha.event.touches.length) { for (var e = ha.event.changedTouches, n = 0, r = e.length; n < r; ++n) delete g[e[n].identifier]; for (var o in g) return void t() }
                    ha.selectAll(_).on(m, null), C.on(O, f).on(D, p), M(), l(v)
                }
                var h, d = this,
                    v = I.of(d, arguments),
                    g = {},
                    y = 0,
                    m = ".zoom-" + ha.event.changedTouches[0].identifier,
                    x = "touchmove" + m,
                    w = "touchend" + m,
                    _ = [],
                    C = ha.select(d),
                    M = tt(d);
                n(), s(v), C.on(O, null).on(D, n)
            }

            function h() {
                var t = I.of(this, arguments);
                m ? clearTimeout(m) : (Vs.call(this), v = e(g = y || ha.mouse(this)), s(t)), m = setTimeout(function () { m = null, l(t) }, 50), N(), r(Math.pow(2, .002 * Ja()) * E.k), o(g, v), c(t)
            }

            function d() {
                var t = ha.mouse(this),
                    n = Math.log(E.k) / Math.LN2;
                i(this, t, e(t), ha.event.shiftKey ? Math.ceil(n) - 1 : Math.floor(n) + 1)
            }
            var v, g, y, m, b, x, w, _, C, E = { x: 0, y: 0, k: 1 },
                M = [960, 500],
                k = Za,
                T = 250,
                S = 0,
                O = "mousedown.zoom",
                P = "mousemove.zoom",
                j = "mouseup.zoom",
                D = "touchstart.zoom",
                I = A(t, "zoomstart", "zoom", "zoomend");
            return Qa || (Qa = "onwheel" in ga ? (Ja = function () { return -ha.event.deltaY * (ha.event.deltaMode ? 120 : 1) }, "wheel") : "onmousewheel" in ga ? (Ja = function () { return ha.event.wheelDelta }, "mousewheel") : (Ja = function () { return -ha.event.detail }, "MozMousePixelScroll")), t.event = function (t) {
                t.each(function () {
                    var t = I.of(this, arguments),
                        e = E;
                    Ws ? ha.select(this).transition().each("start.zoom", function () { E = this.__chart__ || { x: 0, y: 0, k: 1 }, s(t) }).tween("zoom:zoom", function () {
                        var n = M[0],
                            r = M[1],
                            o = g ? g[0] : n / 2,
                            i = g ? g[1] : r / 2,
                            a = ha.interpolateZoom([(o - E.x) / E.k, (i - E.y) / E.k, n / E.k], [(o - e.x) / e.k, (i - e.y) / e.k, n / e.k]);
                        return function (e) {
                            var r = a(e),
                                u = n / r[2];
                            this.__chart__ = E = { x: o - r[0] * u, y: i - r[1] * u, k: u }, c(t)
                        }
                    }).each("interrupt.zoom", function () { l(t) }).each("end.zoom", function () { l(t) }) : (this.__chart__ = E, s(t), c(t), l(t))
                })
            }, t.translate = function (e) { return arguments.length ? (E = { x: +e[0], y: +e[1], k: E.k }, u(), t) : [E.x, E.y] }, t.scale = function (e) { return arguments.length ? (E = { x: E.x, y: E.y, k: null }, r(+e), u(), t) : E.k }, t.scaleExtent = function (e) { return arguments.length ? (k = null == e ? Za : [+e[0], +e[1]], t) : k }, t.center = function (e) { return arguments.length ? (y = e && [+e[0], +e[1]], t) : y }, t.size = function (e) { return arguments.length ? (M = e && [+e[0], +e[1]], t) : M }, t.duration = function (e) { return arguments.length ? (T = +e, t) : T }, t.x = function (e) { return arguments.length ? (w = e, x = e.copy(), E = { x: 0, y: 0, k: 1 }, t) : w }, t.y = function (e) { return arguments.length ? (C = e, _ = e.copy(), E = { x: 0, y: 0, k: 1 }, t) : C }, ha.rebind(t, I, "on")
        };
        var Ja, Qa, Za = [0, 1 / 0];
        ha.color = ft, ft.prototype.toString = function () { return this.rgb() + "" }, ha.hsl = pt;
        var tu = pt.prototype = new ft;
        tu.brighter = function (t) { return t = Math.pow(.7, arguments.length ? t : 1), new pt(this.h, this.s, this.l / t) }, tu.darker = function (t) { return t = Math.pow(.7, arguments.length ? t : 1), new pt(this.h, this.s, t * this.l) }, tu.rgb = function () { return ht(this.h, this.s, this.l) }, ha.hcl = dt;
        var eu = dt.prototype = new ft;
        eu.brighter = function (t) { return new dt(this.h, this.c, Math.min(100, this.l + nu * (arguments.length ? t : 1))) }, eu.darker = function (t) { return new dt(this.h, this.c, Math.max(0, this.l - nu * (arguments.length ? t : 1))) }, eu.rgb = function () { return vt(this.h, this.c, this.l).rgb() }, ha.lab = gt;
        var nu = 18,
            ru = .95047,
            ou = 1,
            iu = 1.08883,
            au = gt.prototype = new ft;
        au.brighter = function (t) { return new gt(Math.min(100, this.l + nu * (arguments.length ? t : 1)), this.a, this.b) }, au.darker = function (t) { return new gt(Math.max(0, this.l - nu * (arguments.length ? t : 1)), this.a, this.b) }, au.rgb = function () { return yt(this.l, this.a, this.b) }, ha.rgb = _t;
        var uu = _t.prototype = new ft;
        uu.brighter = function (t) {
            t = Math.pow(.7, arguments.length ? t : 1);
            var e = this.r,
                n = this.g,
                r = this.b,
                o = 30;
            return e || n || r ? (e && e < o && (e = o), n && n < o && (n = o), r && r < o && (r = o), new _t(Math.min(255, e / t), Math.min(255, n / t), Math.min(255, r / t))) : new _t(o, o, o)
        }, uu.darker = function (t) { return t = Math.pow(.7, arguments.length ? t : 1), new _t(t * this.r, t * this.g, t * this.b) }, uu.hsl = function () { return Tt(this.r, this.g, this.b) }, uu.toString = function () { return "#" + Mt(this.r) + Mt(this.g) + Mt(this.b) };
        var su = ha.map({ aliceblue: 15792383, antiquewhite: 16444375, aqua: 65535, aquamarine: 8388564, azure: 15794175, beige: 16119260, bisque: 16770244, black: 0, blanchedalmond: 16772045, blue: 255, blueviolet: 9055202, brown: 10824234, burlywood: 14596231, cadetblue: 6266528, chartreuse: 8388352, chocolate: 13789470, coral: 16744272, cornflowerblue: 6591981, cornsilk: 16775388, crimson: 14423100, cyan: 65535, darkblue: 139, darkcyan: 35723, darkgoldenrod: 12092939, darkgray: 11119017, darkgreen: 25600, darkgrey: 11119017, darkkhaki: 12433259, darkmagenta: 9109643, darkolivegreen: 5597999, darkorange: 16747520, darkorchid: 10040012, darkred: 9109504, darksalmon: 15308410, darkseagreen: 9419919, darkslateblue: 4734347, darkslategray: 3100495, darkslategrey: 3100495, darkturquoise: 52945, darkviolet: 9699539, deeppink: 16716947, deepskyblue: 49151, dimgray: 6908265, dimgrey: 6908265, dodgerblue: 2003199, firebrick: 11674146, floralwhite: 16775920, forestgreen: 2263842, fuchsia: 16711935, gainsboro: 14474460, ghostwhite: 16316671, gold: 16766720, goldenrod: 14329120, gray: 8421504, green: 32768, greenyellow: 11403055, grey: 8421504, honeydew: 15794160, hotpink: 16738740, indianred: 13458524, indigo: 4915330, ivory: 16777200, khaki: 15787660, lavender: 15132410, lavenderblush: 16773365, lawngreen: 8190976, lemonchiffon: 16775885, lightblue: 11393254, lightcoral: 15761536, lightcyan: 14745599, lightgoldenrodyellow: 16448210, lightgray: 13882323, lightgreen: 9498256, lightgrey: 13882323, lightpink: 16758465, lightsalmon: 16752762, lightseagreen: 2142890, lightskyblue: 8900346, lightslategray: 7833753, lightslategrey: 7833753, lightsteelblue: 11584734, lightyellow: 16777184, lime: 65280, limegreen: 3329330, linen: 16445670, magenta: 16711935, maroon: 8388608, mediumaquamarine: 6737322, mediumblue: 205, mediumorchid: 12211667, mediumpurple: 9662683, mediumseagreen: 3978097, mediumslateblue: 8087790, mediumspringgreen: 64154, mediumturquoise: 4772300, mediumvioletred: 13047173, midnightblue: 1644912, mintcream: 16121850, mistyrose: 16770273, moccasin: 16770229, navajowhite: 16768685, navy: 128, oldlace: 16643558, olive: 8421376, olivedrab: 7048739, orange: 16753920, orangered: 16729344, orchid: 14315734, palegoldenrod: 15657130, palegreen: 10025880, paleturquoise: 11529966, palevioletred: 14381203, papayawhip: 16773077, peachpuff: 16767673, peru: 13468991, pink: 16761035, plum: 14524637, powderblue: 11591910, purple: 8388736, rebeccapurple: 6697881, red: 16711680, rosybrown: 12357519, royalblue: 4286945, saddlebrown: 9127187, salmon: 16416882, sandybrown: 16032864, seagreen: 3050327, seashell: 16774638, sienna: 10506797, silver: 12632256, skyblue: 8900331, slateblue: 6970061, slategray: 7372944, slategrey: 7372944, snow: 16775930, springgreen: 65407, steelblue: 4620980, tan: 13808780, teal: 32896, thistle: 14204888, tomato: 16737095, turquoise: 4251856, violet: 15631086, wheat: 16113331, white: 16777215, whitesmoke: 16119285, yellow: 16776960, yellowgreen: 10145074 });
        su.forEach(function (t, e) { su.set(t, Ct(e)) }), ha.functor = At, ha.xhr = Pt(C), ha.dsv = function (t, e) {
            function n(t, n, i) { arguments.length < 3 && (i = n, n = null); var a = jt(t, e, null == n ? r : o(n), i); return a.row = function (t) { return arguments.length ? a.response(null == (n = t) ? r : o(t)) : n }, a }

            function r(t) { return n.parse(t.responseText) }

            function o(t) { return function (e) { return n.parse(e.responseText, t) } }

            function i(e) { return e.map(a).join(t) }

            function a(t) { return u.test(t) ? '"' + t.replace(/\"/g, '""') + '"' : t }
            var u = new RegExp('["' + t + "\n]"),
                s = t.charCodeAt(0);
            return n.parse = function (t, e) {
                var r;
                return n.parseRows(t, function (t, n) {
                    if (r) return r(t, n - 1);
                    var o = new Function("d", "return {" + t.map(function (t, e) { return JSON.stringify(t) + ": d[" + e + "]" }).join(",") + "}");
                    r = e ? function (t, n) { return e(o(t), n) } : o
                })
            }, n.parseRows = function (t, e) {
                function n() {
                    if (l >= c) return a;
                    if (o) return o = !1, i;
                    var e = l;
                    if (34 === t.charCodeAt(e)) {
                        for (var n = e; n++ < c;)
                            if (34 === t.charCodeAt(n)) { if (34 !== t.charCodeAt(n + 1)) break; ++n }
                        l = n + 2;
                        var r = t.charCodeAt(n + 1);
                        return 13 === r ? (o = !0, 10 === t.charCodeAt(n + 2) && ++l) : 10 === r && (o = !0), t.slice(e + 1, n).replace(/""/g, '"')
                    }
                    for (; l < c;) {
                        var r = t.charCodeAt(l++),
                            u = 1;
                        if (10 === r) o = !0;
                        else if (13 === r) o = !0, 10 === t.charCodeAt(l) && (++l, ++u);
                        else if (r !== s) continue;
                        return t.slice(e, l - u)
                    }
                    return t.slice(e)
                }
                for (var r, o, i = {}, a = {}, u = [], c = t.length, l = 0, f = 0;
                    (r = n()) !== a;) {
                    for (var p = []; r !== i && r !== a;) p.push(r), r = n();
                    e && null == (p = e(p, f++)) || u.push(p)
                }
                return u
            }, n.format = function (e) {
                if (Array.isArray(e[0])) return n.formatRows(e);
                var r = new _,
                    o = [];
                return e.forEach(function (t) { for (var e in t) r.has(e) || o.push(r.add(e)) }), [o.map(a).join(t)].concat(e.map(function (e) { return o.map(function (t) { return a(e[t]) }).join(t) })).join("\n")
            }, n.formatRows = function (t) { return t.map(i).join("\n") }, n
        }, ha.csv = ha.dsv(",", "text/csv"), ha.tsv = ha.dsv("\t", "text/tab-separated-values");
        var cu, lu, fu, pu, hu = this[M(this, "requestAnimationFrame")] || function (t) { setTimeout(t, 17) };
        ha.timer = function () { Rt.apply(this, arguments) }, ha.timer.flush = function () { Ft(), Ut() }, ha.round = function (t, e) { return e ? Math.round(t * (e = Math.pow(10, e))) / e : Math.round(t) };
        var du = ["y", "z", "a", "f", "p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y"].map(Ht);
        ha.formatPrefix = function (t, e) { var n = 0; return (t = +t) && (t < 0 && (t *= -1), e && (t = ha.round(t, qt(t, e))), n = 1 + Math.floor(1e-12 + Math.log(t) / Math.LN10), n = Math.max(-24, Math.min(24, 3 * Math.floor((n - 1) / 3)))), du[8 + n / 3] };
        var vu = /(?:([^{])?([<>=^]))?([+\- ])?([$#])?(0)?(\d+)?(,)?(\.-?\d+)?([a-z%])?/i,
            gu = ha.map({ b: function (t) { return t.toString(2) }, c: function (t) { return String.fromCharCode(t) }, o: function (t) { return t.toString(8) }, x: function (t) { return t.toString(16) }, X: function (t) { return t.toString(16).toUpperCase() }, g: function (t, e) { return t.toPrecision(e) }, e: function (t, e) { return t.toExponential(e) }, f: function (t, e) { return t.toFixed(e) }, r: function (t, e) { return (t = ha.round(t, qt(t, e))).toFixed(Math.max(0, Math.min(20, qt(t * (1 + 1e-15), e)))) } }),
            yu = ha.time = {},
            mu = Date;
        zt.prototype = { getDate: function () { return this._.getUTCDate() }, getDay: function () { return this._.getUTCDay() }, getFullYear: function () { return this._.getUTCFullYear() }, getHours: function () { return this._.getUTCHours() }, getMilliseconds: function () { return this._.getUTCMilliseconds() }, getMinutes: function () { return this._.getUTCMinutes() }, getMonth: function () { return this._.getUTCMonth() }, getSeconds: function () { return this._.getUTCSeconds() }, getTime: function () { return this._.getTime() }, getTimezoneOffset: function () { return 0 }, valueOf: function () { return this._.valueOf() }, setDate: function () { bu.setUTCDate.apply(this._, arguments) }, setDay: function () { bu.setUTCDay.apply(this._, arguments) }, setFullYear: function () { bu.setUTCFullYear.apply(this._, arguments) }, setHours: function () { bu.setUTCHours.apply(this._, arguments) }, setMilliseconds: function () { bu.setUTCMilliseconds.apply(this._, arguments) }, setMinutes: function () { bu.setUTCMinutes.apply(this._, arguments) }, setMonth: function () { bu.setUTCMonth.apply(this._, arguments) }, setSeconds: function () { bu.setUTCSeconds.apply(this._, arguments) }, setTime: function () { bu.setTime.apply(this._, arguments) } };
        var bu = Date.prototype;
        yu.year = Vt(function (t) { return t = yu.day(t), t.setMonth(0, 1), t }, function (t, e) { t.setFullYear(t.getFullYear() + e) }, function (t) { return t.getFullYear() }), yu.years = yu.year.range, yu.years.utc = yu.year.utc.range, yu.day = Vt(function (t) { var e = new mu(2e3, 0); return e.setFullYear(t.getFullYear(), t.getMonth(), t.getDate()), e }, function (t, e) { t.setDate(t.getDate() + e) }, function (t) { return t.getDate() - 1 }), yu.days = yu.day.range, yu.days.utc = yu.day.utc.range, yu.dayOfYear = function (t) { var e = yu.year(t); return Math.floor((t - e - 6e4 * (t.getTimezoneOffset() - e.getTimezoneOffset())) / 864e5) }, ["sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday"].forEach(function (t, e) {
            e = 7 - e;
            var n = yu[t] = Vt(function (t) { return (t = yu.day(t)).setDate(t.getDate() - (t.getDay() + e) % 7), t }, function (t, e) { t.setDate(t.getDate() + 7 * Math.floor(e)) }, function (t) { var n = yu.year(t).getDay(); return Math.floor((yu.dayOfYear(t) + (n + e) % 7) / 7) - (n !== e) });
            yu[t + "s"] = n.range, yu[t + "s"].utc = n.utc.range, yu[t + "OfYear"] = function (t) { var n = yu.year(t).getDay(); return Math.floor((yu.dayOfYear(t) + (n + e) % 7) / 7) }
        }), yu.week = yu.sunday, yu.weeks = yu.sunday.range, yu.weeks.utc = yu.sunday.utc.range, yu.weekOfYear = yu.sundayOfYear;
        var xu = { "-": "", _: " ", 0: "0" },
            wu = /^\s*\d+/,
            _u = /^%/;
        ha.locale = function (t) { return { numberFormat: Bt(t), timeFormat: $t(t) } };
        var Cu = ha.locale({ decimal: ".", thousands: ",", grouping: [3], currency: ["$", ""], dateTime: "%a %b %e %X %Y", date: "%m/%d/%Y", time: "%H:%M:%S", periods: ["AM", "PM"], days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"], shortDays: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"], months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"], shortMonths: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"] });
        ha.format = Cu.numberFormat, ha.geo = {}, de.prototype = { s: 0, t: 0, add: function (t) { ve(t, this.t, Eu), ve(Eu.s, this.s, this), this.s ? this.t += Eu.t : this.s = Eu.t }, reset: function () { this.s = this.t = 0 }, valueOf: function () { return this.s } };
        var Eu = new de;
        ha.geo.stream = function (t, e) { t && Mu.hasOwnProperty(t.type) ? Mu[t.type](t, e) : ge(t, e) };
        var Mu = { Feature: function (t, e) { ge(t.geometry, e) }, FeatureCollection: function (t, e) { for (var n = t.features, r = -1, o = n.length; ++r < o;) ge(n[r].geometry, e) } },
            ku = { Sphere: function (t, e) { e.sphere() }, Point: function (t, e) { t = t.coordinates, e.point(t[0], t[1], t[2]) }, MultiPoint: function (t, e) { for (var n = t.coordinates, r = -1, o = n.length; ++r < o;) t = n[r], e.point(t[0], t[1], t[2]) }, LineString: function (t, e) { ye(t.coordinates, e, 0) }, MultiLineString: function (t, e) { for (var n = t.coordinates, r = -1, o = n.length; ++r < o;) ye(n[r], e, 0) }, Polygon: function (t, e) { me(t.coordinates, e) }, MultiPolygon: function (t, e) { for (var n = t.coordinates, r = -1, o = n.length; ++r < o;) me(n[r], e) }, GeometryCollection: function (t, e) { for (var n = t.geometries, r = -1, o = n.length; ++r < o;) ge(n[r], e) } };
        ha.geo.area = function (t) { return Tu = 0, ha.geo.stream(t, Nu), Tu };
        var Tu, Su = new de,
            Nu = {
                sphere: function () { Tu += 4 * Ba },
                point: k,
                lineStart: k,
                lineEnd: k,
                polygonStart: function () { Su.reset(), Nu.lineStart = be },
                polygonEnd: function () {
                    var t = 2 * Su;
                    Tu += t < 0 ? 4 * Ba + t : t, Nu.lineStart = Nu.lineEnd = Nu.point = k
                }
            };
        ha.geo.bounds = function () {
            function t(t, e) { b.push(x = [l = t, p = t]), e < f && (f = e), e > h && (h = e) }

            function e(e, n) {
                var r = xe([e * Ya, n * Ya]);
                if (y) {
                    var o = _e(y, r),
                        i = [o[1], -o[0], 0],
                        a = _e(i, o);
                    Me(a), a = ke(a);
                    var s = e - d,
                        c = s > 0 ? 1 : -1,
                        v = a[0] * $a * c,
                        g = Ca(s) > 180;
                    if (g ^ (c * d < v && v < c * e)) {
                        var m = a[1] * $a;
                        m > h && (h = m)
                    } else if (v = (v + 360) % 360 - 180, g ^ (c * d < v && v < c * e)) {
                        var m = -a[1] * $a;
                        m < f && (f = m)
                    } else n < f && (f = n), n > h && (h = n);
                    g ? e < d ? u(l, e) > u(l, p) && (p = e) : u(e, p) > u(l, p) && (l = e) : p >= l ? (e < l && (l = e), e > p && (p = e)) : e > d ? u(l, e) > u(l, p) && (p = e) : u(e, p) > u(l, p) && (l = e)
                } else t(e, n);
                y = r, d = e
            }

            function n() { w.point = e }

            function r() { x[0] = l, x[1] = p, w.point = t, y = null }

            function o(t, n) {
                if (y) {
                    var r = t - d;
                    m += Ca(r) > 180 ? r + (r > 0 ? 360 : -360) : r
                } else v = t, g = n;
                Nu.point(t, n), e(t, n)
            }

            function i() { Nu.lineStart() }

            function a() { o(v, g), Nu.lineEnd(), Ca(m) > qa && (l = -(p = 180)), x[0] = l, x[1] = p, y = null }

            function u(t, e) { return (e -= t) < 0 ? e + 360 : e }

            function s(t, e) { return t[0] - e[0] }

            function c(t, e) { return e[0] <= e[1] ? e[0] <= t && t <= e[1] : t < e[0] || e[1] < t }
            var l, f, p, h, d, v, g, y, m, b, x, w = { point: t, lineStart: n, lineEnd: r, polygonStart: function () { w.point = o, w.lineStart = i, w.lineEnd = a, m = 0, Nu.polygonStart() }, polygonEnd: function () { Nu.polygonEnd(), w.point = t, w.lineStart = n, w.lineEnd = r, Su < 0 ? (l = -(p = 180), f = -(h = 90)) : m > qa ? h = 90 : m < -qa && (f = -90), x[0] = l, x[1] = p } };
            return function (t) {
                h = p = -(l = f = 1 / 0), b = [], ha.geo.stream(t, w);
                var e = b.length;
                if (e) { b.sort(s); for (var n, r = 1, o = b[0], i = [o]; r < e; ++r) n = b[r], c(n[0], o) || c(n[1], o) ? (u(o[0], n[1]) > u(o[0], o[1]) && (o[1] = n[1]), u(n[0], o[1]) > u(o[0], o[1]) && (o[0] = n[0])) : i.push(o = n); for (var a, n, d = -(1 / 0), e = i.length - 1, r = 0, o = i[e]; r <= e; o = n, ++r) n = i[r], (a = u(o[1], n[0])) > d && (d = a, l = n[0], p = o[1]) }
                return b = x = null, l === 1 / 0 || f === 1 / 0 ? [
                    [NaN, NaN],
                    [NaN, NaN]
                ] : [
                        [l, f],
                        [p, h]
                    ]
            }
        }(), ha.geo.centroid = function (t) {
            Ou = Au = Pu = ju = Du = Iu = Ru = Lu = Fu = Uu = qu = 0, ha.geo.stream(t, Hu);
            var e = Fu,
                n = Uu,
                r = qu,
                o = e * e + n * n + r * r;
            return o < Ha && (e = Iu, n = Ru, r = Lu, Au < qa && (e = Pu, n = ju, r = Du), o = e * e + n * n + r * r, o < Ha) ? [NaN, NaN] : [Math.atan2(n, e) * $a, at(r / Math.sqrt(o)) * $a]
        };
        var Ou, Au, Pu, ju, Du, Iu, Ru, Lu, Fu, Uu, qu, Hu = { sphere: k, point: Se, lineStart: Oe, lineEnd: Ae, polygonStart: function () { Hu.lineStart = Pe }, polygonEnd: function () { Hu.lineStart = Oe } },
            Bu = Fe(De, Be, ze, [-Ba, -Ba / 2]),
            Wu = 1e9;
        ha.geo.clipExtent = function () {
            var t, e, n, r, o, i, a = {
                stream: function (t) { return o && (o.valid = !1), o = i(t), o.valid = !0, o },
                extent: function (u) {
                    return arguments.length ? (i = Ke(t = +u[0][0], e = +u[0][1], n = +u[1][0], r = +u[1][1]), o && (o.valid = !1, o = null), a) : [
                        [t, e],
                        [n, r]
                    ]
                }
            };
            return a.extent([
                [0, 0],
                [960, 500]
            ])
        }, (ha.geo.conicEqualArea = function () { return Xe(Ge) }).raw = Ge, ha.geo.albers = function () { return ha.geo.conicEqualArea().rotate([96, 0]).center([-.6, 38.7]).parallels([29.5, 45.5]).scale(1070) }, ha.geo.albersUsa = function () {
            function t(t) {
                var i = t[0],
                    a = t[1];
                return e = null, n(i, a), e || (r(i, a), e) || o(i, a), e
            }
            var e, n, r, o, i = ha.geo.albers(),
                a = ha.geo.conicEqualArea().rotate([154, 0]).center([-2, 58.5]).parallels([55, 65]),
                u = ha.geo.conicEqualArea().rotate([157, 0]).center([-3, 19.9]).parallels([8, 18]),
                s = { point: function (t, n) { e = [t, n] } };
            return t.invert = function (t) {
                var e = i.scale(),
                    n = i.translate(),
                    r = (t[0] - n[0]) / e,
                    o = (t[1] - n[1]) / e;
                return (o >= .12 && o < .234 && r >= -.425 && r < -.214 ? a : o >= .166 && o < .234 && r >= -.214 && r < -.115 ? u : i).invert(t)
            }, t.stream = function (t) {
                var e = i.stream(t),
                    n = a.stream(t),
                    r = u.stream(t);
                return { point: function (t, o) { e.point(t, o), n.point(t, o), r.point(t, o) }, sphere: function () { e.sphere(), n.sphere(), r.sphere() }, lineStart: function () { e.lineStart(), n.lineStart(), r.lineStart() }, lineEnd: function () { e.lineEnd(), n.lineEnd(), r.lineEnd() }, polygonStart: function () { e.polygonStart(), n.polygonStart(), r.polygonStart() }, polygonEnd: function () { e.polygonEnd(), n.polygonEnd(), r.polygonEnd() } }
            }, t.precision = function (e) { return arguments.length ? (i.precision(e), a.precision(e), u.precision(e), t) : i.precision() }, t.scale = function (e) { return arguments.length ? (i.scale(e), a.scale(.35 * e), u.scale(e), t.translate(i.translate())) : i.scale() }, t.translate = function (e) {
                if (!arguments.length) return i.translate();
                var c = i.scale(),
                    l = +e[0],
                    f = +e[1];
                return n = i.translate(e).clipExtent([
                    [l - .455 * c, f - .238 * c],
                    [l + .455 * c, f + .238 * c]
                ]).stream(s).point, r = a.translate([l - .307 * c, f + .201 * c]).clipExtent([
                    [l - .425 * c + qa, f + .12 * c + qa],
                    [l - .214 * c - qa, f + .234 * c - qa]
                ]).stream(s).point, o = u.translate([l - .205 * c, f + .212 * c]).clipExtent([
                    [l - .214 * c + qa, f + .166 * c + qa],
                    [l - .115 * c - qa, f + .234 * c - qa]
                ]).stream(s).point, t
            }, t.scale(1070)
        };
        var zu, Vu, Yu, $u, Ku, Xu, Gu = { point: k, lineStart: k, lineEnd: k, polygonStart: function () { Vu = 0, Gu.lineStart = Je }, polygonEnd: function () { Gu.lineStart = Gu.lineEnd = Gu.point = k, zu += Ca(Vu / 2) } },
            Ju = { point: Qe, lineStart: k, lineEnd: k, polygonStart: k, polygonEnd: k },
            Qu = { point: en, lineStart: nn, lineEnd: rn, polygonStart: function () { Qu.lineStart = on }, polygonEnd: function () { Qu.point = en, Qu.lineStart = nn, Qu.lineEnd = rn } };
        ha.geo.path = function () {
            function t(t) { return t && ("function" == typeof u && i.pointRadius(+u.apply(this, arguments)), a && a.valid || (a = o(i)), ha.geo.stream(t, a)), i.result() }

            function e() { return a = null, t }
            var n, r, o, i, a, u = 4.5;
            return t.area = function (t) { return zu = 0, ha.geo.stream(t, o(Gu)), zu }, t.centroid = function (t) { return Pu = ju = Du = Iu = Ru = Lu = Fu = Uu = qu = 0, ha.geo.stream(t, o(Qu)), qu ? [Fu / qu, Uu / qu] : Lu ? [Iu / Lu, Ru / Lu] : Du ? [Pu / Du, ju / Du] : [NaN, NaN] }, t.bounds = function (t) {
                return Ku = Xu = -(Yu = $u = 1 / 0), ha.geo.stream(t, o(Ju)), [
                    [Yu, $u],
                    [Ku, Xu]
                ]
            }, t.projection = function (t) { return arguments.length ? (o = (n = t) ? t.stream || sn(t) : C, e()) : n }, t.context = function (t) { return arguments.length ? (i = null == (r = t) ? new Ze : new an(t), "function" != typeof u && i.pointRadius(u), e()) : r }, t.pointRadius = function (e) { return arguments.length ? (u = "function" == typeof e ? e : (i.pointRadius(+e), +e), t) : u }, t.projection(ha.geo.albersUsa()).context(null)
        }, ha.geo.transform = function (t) { return { stream: function (e) { var n = new cn(e); for (var r in t) n[r] = t[r]; return n } } }, cn.prototype = { point: function (t, e) { this.stream.point(t, e) }, sphere: function () { this.stream.sphere() }, lineStart: function () { this.stream.lineStart() }, lineEnd: function () { this.stream.lineEnd() }, polygonStart: function () { this.stream.polygonStart() }, polygonEnd: function () { this.stream.polygonEnd() } }, ha.geo.projection = fn, ha.geo.projectionMutator = pn, (ha.geo.equirectangular = function () { return fn(dn) }).raw = dn.invert = dn, ha.geo.rotation = function (t) {
            function e(e) { return e = t(e[0] * Ya, e[1] * Ya), e[0] *= $a, e[1] *= $a, e }
            return t = gn(t[0] % 360 * Ya, t[1] * Ya, t.length > 2 ? t[2] * Ya : 0), e.invert = function (e) { return e = t.invert(e[0] * Ya, e[1] * Ya), e[0] *= $a, e[1] *= $a, e }, e
        }, vn.invert = dn, ha.geo.circle = function () {
            function t() {
                var t = "function" == typeof r ? r.apply(this, arguments) : r,
                    e = gn(-t[0] * Ya, -t[1] * Ya, 0).invert,
                    o = [];
                return n(null, null, 1, { point: function (t, n) { o.push(t = e(t, n)), t[0] *= $a, t[1] *= $a } }), { type: "Polygon", coordinates: [o] }
            }
            var e, n, r = [0, 0],
                o = 6;
            return t.origin = function (e) { return arguments.length ? (r = e, t) : r }, t.angle = function (r) { return arguments.length ? (n = xn((e = +r) * Ya, o * Ya), t) : e }, t.precision = function (r) { return arguments.length ? (n = xn(e * Ya, (o = +r) * Ya), t) : o }, t.angle(90)
        }, ha.geo.distance = function (t, e) {
            var n, r = (e[0] - t[0]) * Ya,
                o = t[1] * Ya,
                i = e[1] * Ya,
                a = Math.sin(r),
                u = Math.cos(r),
                s = Math.sin(o),
                c = Math.cos(o),
                l = Math.sin(i),
                f = Math.cos(i);
            return Math.atan2(Math.sqrt((n = f * a) * n + (n = c * l - s * f * u) * n), s * l + c * f * u)
        }, ha.geo.graticule = function () {
            function t() { return { type: "MultiLineString", coordinates: e() } }

            function e() { return ha.range(Math.ceil(i / g) * g, o, g).map(p).concat(ha.range(Math.ceil(c / y) * y, s, y).map(h)).concat(ha.range(Math.ceil(r / d) * d, n, d).filter(function (t) { return Ca(t % g) > qa }).map(l)).concat(ha.range(Math.ceil(u / v) * v, a, v).filter(function (t) { return Ca(t % y) > qa }).map(f)) }
            var n, r, o, i, a, u, s, c, l, f, p, h, d = 10,
                v = d,
                g = 90,
                y = 360,
                m = 2.5;
            return t.lines = function () { return e().map(function (t) { return { type: "LineString", coordinates: t } }) }, t.outline = function () { return { type: "Polygon", coordinates: [p(i).concat(h(s).slice(1), p(o).reverse().slice(1), h(c).reverse().slice(1))] } }, t.extent = function (e) { return arguments.length ? t.majorExtent(e).minorExtent(e) : t.minorExtent() }, t.majorExtent = function (e) {
                return arguments.length ? (i = +e[0][0], o = +e[1][0], c = +e[0][1], s = +e[1][1], i > o && (e = i, i = o, o = e), c > s && (e = c, c = s, s = e), t.precision(m)) : [
                    [i, c],
                    [o, s]
                ]
            }, t.minorExtent = function (e) {
                return arguments.length ? (r = +e[0][0], n = +e[1][0], u = +e[0][1], a = +e[1][1], r > n && (e = r, r = n, n = e), u > a && (e = u, u = a, a = e), t.precision(m)) : [
                    [r, u],
                    [n, a]
                ]
            }, t.step = function (e) { return arguments.length ? t.majorStep(e).minorStep(e) : t.minorStep() }, t.majorStep = function (e) { return arguments.length ? (g = +e[0], y = +e[1], t) : [g, y] }, t.minorStep = function (e) { return arguments.length ? (d = +e[0], v = +e[1], t) : [d, v] }, t.precision = function (e) { return arguments.length ? (m = +e, l = _n(u, a, 90), f = Cn(r, n, m), p = _n(c, s, 90), h = Cn(i, o, m), t) : m }, t.majorExtent([
                [-180, -90 + qa],
                [180, 90 - qa]
            ]).minorExtent([
                [-180, -80 - qa],
                [180, 80 + qa]
            ])
        }, ha.geo.greatArc = function () {
            function t() { return { type: "LineString", coordinates: [e || r.apply(this, arguments), n || o.apply(this, arguments)] } }
            var e, n, r = En,
                o = Mn;
            return t.distance = function () { return ha.geo.distance(e || r.apply(this, arguments), n || o.apply(this, arguments)) }, t.source = function (n) { return arguments.length ? (r = n, e = "function" == typeof n ? null : n, t) : r }, t.target = function (e) { return arguments.length ? (o = e, n = "function" == typeof e ? null : e, t) : o }, t.precision = function () { return arguments.length ? t : 0 }, t
        }, ha.geo.interpolate = function (t, e) { return kn(t[0] * Ya, t[1] * Ya, e[0] * Ya, e[1] * Ya) }, ha.geo.length = function (t) { return Zu = 0, ha.geo.stream(t, ts), Zu };
        var Zu, ts = { sphere: k, point: k, lineStart: Tn, lineEnd: k, polygonStart: k, polygonEnd: k },
            es = Sn(function (t) { return Math.sqrt(2 / (1 + t)) }, function (t) { return 2 * Math.asin(t / 2) });
        (ha.geo.azimuthalEqualArea = function () { return fn(es) }).raw = es;
        var ns = Sn(function (t) { var e = Math.acos(t); return e && e / Math.sin(e) }, C);
        (ha.geo.azimuthalEquidistant = function () { return fn(ns) }).raw = ns, (ha.geo.conicConformal = function () { return Xe(Nn) }).raw = Nn, (ha.geo.conicEquidistant = function () { return Xe(On) }).raw = On;
        var rs = Sn(function (t) { return 1 / t }, Math.atan);
        (ha.geo.gnomonic = function () { return fn(rs) }).raw = rs, An.invert = function (t, e) { return [t, 2 * Math.atan(Math.exp(e)) - Va] }, (ha.geo.mercator = function () { return Pn(An) }).raw = An;
        var os = Sn(function () { return 1 }, Math.asin);
        (ha.geo.orthographic = function () { return fn(os) }).raw = os;
        var is = Sn(function (t) { return 1 / (1 + t) }, function (t) { return 2 * Math.atan(t) });
        (ha.geo.stereographic = function () { return fn(is) }).raw = is, jn.invert = function (t, e) { return [-e, 2 * Math.atan(Math.exp(t)) - Va] }, (ha.geo.transverseMercator = function () {
            var t = Pn(jn),
                e = t.center,
                n = t.rotate;
            return t.center = function (t) { return t ? e([-t[1], t[0]]) : (t = e(), [t[1], -t[0]]) }, t.rotate = function (t) { return t ? n([t[0], t[1], t.length > 2 ? t[2] + 90 : 90]) : (t = n(), [t[0], t[1], t[2] - 90]) }, n([0, 0, 90])
        }).raw = jn, ha.geom = {}, ha.geom.hull = function (t) {
            function e(t) {
                if (t.length < 3) return [];
                var e, o = At(n),
                    i = At(r),
                    a = t.length,
                    u = [],
                    s = [];
                for (e = 0; e < a; e++) u.push([+o.call(this, t[e], e), +i.call(this, t[e], e), e]);
                for (u.sort(Ln), e = 0; e < a; e++) s.push([u[e][0], -u[e][1]]);
                var c = Rn(u),
                    l = Rn(s),
                    f = l[0] === c[0],
                    p = l[l.length - 1] === c[c.length - 1],
                    h = [];
                for (e = c.length - 1; e >= 0; --e) h.push(t[u[c[e]][2]]);
                for (e = +f; e < l.length - p; ++e) h.push(t[u[l[e]][2]]);
                return h
            }
            var n = Dn,
                r = In;
            return arguments.length ? e(t) : (e.x = function (t) { return arguments.length ? (n = t, e) : n }, e.y = function (t) { return arguments.length ? (r = t, e) : r }, e)
        }, ha.geom.polygon = function (t) { return Sa(t, as), t };
        var as = ha.geom.polygon.prototype = [];
        as.area = function () { for (var t, e = -1, n = this.length, r = this[n - 1], o = 0; ++e < n;) t = r, r = this[e], o += t[1] * r[0] - t[0] * r[1]; return .5 * o }, as.centroid = function (t) {
            var e, n, r = -1,
                o = this.length,
                i = 0,
                a = 0,
                u = this[o - 1];
            for (arguments.length || (t = -1 / (6 * this.area())); ++r < o;) e = u, u = this[r], n = e[0] * u[1] - u[0] * e[1], i += (e[0] + u[0]) * n, a += (e[1] + u[1]) * n;
            return [i * t, a * t]
        }, as.clip = function (t) {
            for (var e, n, r, o, i, a, u = qn(t), s = -1, c = this.length - qn(this), l = this[c - 1]; ++s < c;) {
                for (e = t.slice(), t.length = 0, o = this[s], i = e[(r = e.length - u) - 1], n = -1; ++n < r;) a = e[n], Fn(a, l, o) ? (Fn(i, l, o) || t.push(Un(i, a, l, o)), t.push(a)) : Fn(i, l, o) && t.push(Un(i, a, l, o)), i = a;
                u && t.push(t[0]), l = o
            }
            return t
        };
        var us, ss, cs, ls, fs, ps = [],
            hs = [];
        Kn.prototype.prepare = function () { for (var t, e = this.edges, n = e.length; n--;) t = e[n].edge, t.b && t.a || e.splice(n, 1); return e.sort(Gn), e.length }, ar.prototype = { start: function () { return this.edge.l === this.site ? this.edge.a : this.edge.b }, end: function () { return this.edge.l === this.site ? this.edge.b : this.edge.a } }, ur.prototype = {
            insert: function (t, e) {
                var n, r, o;
                if (t) {
                    if (e.P = t, e.N = t.N, t.N && (t.N.P = e), t.N = e, t.R) {
                        for (t = t.R; t.L;) t = t.L;
                        t.L = e
                    } else t.R = e;
                    n = t
                } else this._ ? (t = fr(this._), e.P = null, e.N = t, t.P = t.L = e, n = t) : (e.P = e.N = null, this._ = e, n = null);
                for (e.L = e.R = null, e.U = n, e.C = !0, t = e; n && n.C;) r = n.U, n === r.L ? (o = r.R, o && o.C ? (n.C = o.C = !1, r.C = !0, t = r) : (t === n.R && (cr(this, n), t = n, n = t.U), n.C = !1, r.C = !0, lr(this, r))) : (o = r.L, o && o.C ? (n.C = o.C = !1, r.C = !0, t = r) : (t === n.L && (lr(this, n), t = n, n = t.U), n.C = !1, r.C = !0, cr(this, r))), n = t.U;
                this._.C = !1
            },
            remove: function (t) {
                t.N && (t.N.P = t.P), t.P && (t.P.N = t.N), t.N = t.P = null;
                var e, n, r, o = t.U,
                    i = t.L,
                    a = t.R;
                if (n = i ? a ? fr(a) : i : a, o ? o.L === t ? o.L = n : o.R = n : this._ = n, i && a ? (r = n.C, n.C = t.C, n.L = i, i.U = n, n !== a ? (o = n.U, n.U = t.U, t = n.R, o.L = t, n.R = a, a.U = n) : (n.U = o, o = n, t = n.R)) : (r = t.C, t = n), t && (t.U = o), !r) {
                    if (t && t.C) return void (t.C = !1);
                    do {
                        if (t === this._) break;
                        if (t === o.L) { if (e = o.R, e.C && (e.C = !1, o.C = !0, cr(this, o), e = o.R), e.L && e.L.C || e.R && e.R.C) { e.R && e.R.C || (e.L.C = !1, e.C = !0, lr(this, e), e = o.R), e.C = o.C, o.C = e.R.C = !1, cr(this, o), t = this._; break } } else if (e = o.L, e.C && (e.C = !1, o.C = !0, lr(this, o), e = o.L), e.L && e.L.C || e.R && e.R.C) { e.L && e.L.C || (e.R.C = !1, e.C = !0, cr(this, e), e = o.L), e.C = o.C, o.C = e.L.C = !1, lr(this, o), t = this._; break }
                        e.C = !0, t = o, o = o.U
                    } while (!t.C);
                    t && (t.C = !1)
                }
            }
        }, ha.geom.voronoi = function (t) {
            function e(t) {
                var e = new Array(t.length),
                    r = u[0][0],
                    o = u[0][1],
                    i = u[1][0],
                    a = u[1][1];
                return pr(n(t), u).cells.forEach(function (n, u) {
                    var s = n.edges,
                        c = n.site,
                        l = e[u] = s.length ? s.map(function (t) { var e = t.start(); return [e.x, e.y] }) : c.x >= r && c.x <= i && c.y >= o && c.y <= a ? [
                            [r, a],
                            [i, a],
                            [i, o],
                            [r, o]
                        ] : [];
                    l.point = t[u]
                }), e
            }

            function n(t) { return t.map(function (t, e) { return { x: Math.round(i(t, e) / qa) * qa, y: Math.round(a(t, e) / qa) * qa, i: e } }) }
            var r = Dn,
                o = In,
                i = r,
                a = o,
                u = ds;
            return t ? e(t) : (e.links = function (t) { return pr(n(t)).edges.filter(function (t) { return t.l && t.r }).map(function (e) { return { source: t[e.l.i], target: t[e.r.i] } }) }, e.triangles = function (t) { var e = []; return pr(n(t)).cells.forEach(function (n, r) { for (var o, i, a = n.site, u = n.edges.sort(Gn), s = -1, c = u.length, l = u[c - 1].edge, f = l.l === a ? l.r : l.l; ++s < c;) o = l, i = f, l = u[s].edge, f = l.l === a ? l.r : l.l, r < i.i && r < f.i && dr(a, i, f) < 0 && e.push([t[r], t[i.i], t[f.i]]) }), e }, e.x = function (t) { return arguments.length ? (i = At(r = t), e) : r }, e.y = function (t) { return arguments.length ? (a = At(o = t), e) : o }, e.clipExtent = function (t) { return arguments.length ? (u = null == t ? ds : t, e) : u === ds ? null : u }, e.size = function (t) {
                return arguments.length ? e.clipExtent(t && [
                    [0, 0], t
                ]) : u === ds ? null : u && u[1]
            }, e)
        };
        var ds = [
            [-1e6, -1e6],
            [1e6, 1e6]
        ];
        ha.geom.delaunay = function (t) { return ha.geom.voronoi().triangles(t) }, ha.geom.quadtree = function (t, e, n, r, o) {
            function i(t) {
                function i(t, e, n, r, o, i, a, u) {
                    if (!isNaN(n) && !isNaN(r))
                        if (t.leaf) {
                            var s = t.x,
                                l = t.y;
                            if (null != s)
                                if (Ca(s - n) + Ca(l - r) < .01) c(t, e, n, r, o, i, a, u);
                                else {
                                    var f = t.point;
                                    t.x = t.y = t.point = null, c(t, f, s, l, o, i, a, u), c(t, e, n, r, o, i, a, u)
                                }
                            else t.x = n, t.y = r, t.point = e
                        } else c(t, e, n, r, o, i, a, u)
                }

                function c(t, e, n, r, o, a, u, s) {
                    var c = .5 * (o + u),
                        l = .5 * (a + s),
                        f = n >= c,
                        p = r >= l,
                        h = p << 1 | f;
                    t.leaf = !1, t = t.nodes[h] || (t.nodes[h] = yr()), f ? o = c : u = c, p ? a = l : s = l, i(t, e, n, r, o, a, u, s)
                }
                var l, f, p, h, d, v, g, y, m, b = At(u),
                    x = At(s);
                if (null != e) v = e, g = n, y = r, m = o;
                else if (y = m = -(v = g = 1 / 0), f = [], p = [], d = t.length, a)
                    for (h = 0; h < d; ++h) l = t[h], l.x < v && (v = l.x), l.y < g && (g = l.y), l.x > y && (y = l.x), l.y > m && (m = l.y), f.push(l.x), p.push(l.y);
                else
                    for (h = 0; h < d; ++h) {
                        var w = +b(l = t[h], h),
                            _ = +x(l, h);
                        w < v && (v = w), _ < g && (g = _), w > y && (y = w), _ > m && (m = _), f.push(w), p.push(_)
                    }
                var C = y - v,
                    E = m - g;
                C > E ? m = g + C : y = v + E;
                var M = yr();
                if (M.add = function (t) { i(M, t, +b(t, ++h), +x(t, h), v, g, y, m) }, M.visit = function (t) { mr(t, M, v, g, y, m) }, M.find = function (t) { return br(M, t[0], t[1], v, g, y, m) }, h = -1, null == e) { for (; ++h < d;) i(M, t[h], f[h], p[h], v, g, y, m); --h } else t.forEach(M.add);
                return f = p = t = l = null, M
            }
            var a, u = Dn,
                s = In;
            return (a = arguments.length) ? (u = vr, s = gr, 3 === a && (o = n, r = e, n = e = 0), i(t)) : (i.x = function (t) { return arguments.length ? (u = t, i) : u }, i.y = function (t) { return arguments.length ? (s = t, i) : s }, i.extent = function (t) {
                return arguments.length ? (null == t ? e = n = r = o = null : (e = +t[0][0], n = +t[0][1], r = +t[1][0], o = +t[1][1]), i) : null == e ? null : [
                    [e, n],
                    [r, o]
                ]
            }, i.size = function (t) { return arguments.length ? (null == t ? e = n = r = o = null : (e = n = 0, r = +t[0], o = +t[1]), i) : null == e ? null : [r - e, o - n] }, i)
        }, ha.interpolateRgb = xr, ha.interpolateObject = wr, ha.interpolateNumber = _r, ha.interpolateString = Cr;
        var vs = /[-+]?(?:\d+\.?\d*|\.?\d+)(?:[eE][-+]?\d+)?/g,
            gs = new RegExp(vs.source, "g");
        ha.interpolate = Er, ha.interpolators = [function (t, e) { var n = typeof e; return ("string" === n ? su.has(e.toLowerCase()) || /^(#|rgb\(|hsl\()/i.test(e) ? xr : Cr : e instanceof ft ? xr : Array.isArray(e) ? Mr : "object" === n && isNaN(e) ? wr : _r)(t, e) }], ha.interpolateArray = Mr;
        var ys = function () { return C },
            ms = ha.map({ linear: ys, poly: Pr, quad: function () { return Nr }, cubic: function () { return Or }, sin: function () { return jr }, exp: function () { return Dr }, circle: function () { return Ir }, elastic: Rr, back: Lr, bounce: function () { return Fr } }),
            bs = ha.map({ in: C, out: Tr, "in-out": Sr, "out-in": function (t) { return Sr(Tr(t)) } });
        ha.ease = function (t) {
            var e = t.indexOf("-"),
                n = e >= 0 ? t.slice(0, e) : t,
                r = e >= 0 ? t.slice(e + 1) : "in";
            return n = ms.get(n) || ys, r = bs.get(r) || C, kr(r(n.apply(null, da.call(arguments, 1))))
        }, ha.interpolateHcl = Ur, ha.interpolateHsl = qr, ha.interpolateLab = Hr, ha.interpolateRound = Br, ha.transform = function (t) { var e = ga.createElementNS(ha.ns.prefix.svg, "g"); return (ha.transform = function (t) { if (null != t) { e.setAttribute("transform", t); var n = e.transform.baseVal.consolidate() } return new Wr(n ? n.matrix : xs) })(t) }, Wr.prototype.toString = function () { return "translate(" + this.translate + ")rotate(" + this.rotate + ")skewX(" + this.skew + ")scale(" + this.scale + ")" };
        var xs = { a: 1, b: 0, c: 0, d: 1, e: 0, f: 0 };
        ha.interpolateTransform = Qr, ha.layout = {}, ha.layout.bundle = function () { return function (t) { for (var e = [], n = -1, r = t.length; ++n < r;) e.push(eo(t[n])); return e } }, ha.layout.chord = function () {
            function t() {
                var t, c, f, p, h, d = {},
                    v = [],
                    g = ha.range(i),
                    y = [];
                for (n = [], r = [], t = 0, p = -1; ++p < i;) {
                    for (c = 0, h = -1; ++h < i;) c += o[p][h];
                    v.push(c), y.push(ha.range(i)), t += c
                }
                for (a && g.sort(function (t, e) { return a(v[t], v[e]) }), u && y.forEach(function (t, e) { t.sort(function (t, n) { return u(o[e][t], o[e][n]) }) }), t = (Wa - l * i) / t, c = 0, p = -1; ++p < i;) {
                    for (f = c, h = -1; ++h < i;) {
                        var m = g[p],
                            b = y[m][h],
                            x = o[m][b],
                            w = c,
                            _ = c += x * t;
                        d[m + "-" + b] = { index: m, subindex: b, startAngle: w, endAngle: _, value: x }
                    }
                    r[m] = { index: m, startAngle: f, endAngle: c, value: v[m] }, c += l
                }
                for (p = -1; ++p < i;)
                    for (h = p - 1; ++h < i;) {
                        var C = d[p + "-" + h],
                            E = d[h + "-" + p];
                        (C.value || E.value) && n.push(C.value < E.value ? { source: E, target: C } : { source: C, target: E })
                    }
                s && e()
            }

            function e() { n.sort(function (t, e) { return s((t.source.value + t.target.value) / 2, (e.source.value + e.target.value) / 2) }) }
            var n, r, o, i, a, u, s, c = {},
                l = 0;
            return c.matrix = function (t) { return arguments.length ? (i = (o = t) && o.length, n = r = null, c) : o }, c.padding = function (t) { return arguments.length ? (l = t, n = r = null, c) : l }, c.sortGroups = function (t) { return arguments.length ? (a = t, n = r = null, c) : a }, c.sortSubgroups = function (t) { return arguments.length ? (u = t, n = null, c) : u }, c.sortChords = function (t) { return arguments.length ? (s = t, n && e(), c) : s }, c.chords = function () { return n || t(), n }, c.groups = function () { return r || t(), r }, c
        }, ha.layout.force = function () {
            function t(t) {
                return function (e, n, r, o) {
                    if (e.point !== t) {
                        var i = e.cx - t.x,
                            a = e.cy - t.y,
                            u = o - n,
                            s = i * i + a * a;
                        if (u * u / y < s) {
                            if (s < v) {
                                var c = e.charge / s;
                                t.px -= i * c, t.py -= a * c
                            }
                            return !0
                        }
                        if (e.point && s && s < v) {
                            var c = e.pointCharge / s;
                            t.px -= i * c, t.py -= a * c
                        }
                    }
                    return !e.charge
                }
            }

            function e(t) { t.px = ha.event.x, t.py = ha.event.y, s.resume() }
            var n, r, o, i, a, u, s = {},
                c = ha.dispatch("start", "tick", "end"),
                l = [1, 1],
                f = .9,
                p = ws,
                h = _s,
                d = -30,
                v = Cs,
                g = .1,
                y = .64,
                m = [],
                b = [];
            return s.tick = function () {
                if ((o *= .99) < .005) return n = null, c.end({ type: "end", alpha: o = 0 }), !0;
                var e, r, s, p, h, v, y, x, w, _ = m.length,
                    C = b.length;
                for (r = 0; r < C; ++r) s = b[r], p = s.source, h = s.target, x = h.x - p.x, w = h.y - p.y, (v = x * x + w * w) && (v = o * a[r] * ((v = Math.sqrt(v)) - i[r]) / v, x *= v, w *= v, h.x -= x * (y = p.weight + h.weight ? p.weight / (p.weight + h.weight) : .5), h.y -= w * y, p.x += x * (y = 1 - y), p.y += w * y);
                if ((y = o * g) && (x = l[0] / 2, w = l[1] / 2, r = -1, y))
                    for (; ++r < _;) s = m[r], s.x += (x - s.x) * y, s.y += (w - s.y) * y;
                if (d)
                    for (so(e = ha.geom.quadtree(m), o, u), r = -1; ++r < _;)(s = m[r]).fixed || e.visit(t(s));
                for (r = -1; ++r < _;) s = m[r], s.fixed ? (s.x = s.px, s.y = s.py) : (s.x -= (s.px - (s.px = s.x)) * f, s.y -= (s.py - (s.py = s.y)) * f);
                c.tick({ type: "tick", alpha: o })
            }, s.nodes = function (t) { return arguments.length ? (m = t, s) : m }, s.links = function (t) { return arguments.length ? (b = t, s) : b }, s.size = function (t) { return arguments.length ? (l = t, s) : l }, s.linkDistance = function (t) { return arguments.length ? (p = "function" == typeof t ? t : +t, s) : p }, s.distance = s.linkDistance, s.linkStrength = function (t) { return arguments.length ? (h = "function" == typeof t ? t : +t, s) : h }, s.friction = function (t) { return arguments.length ? (f = +t, s) : f }, s.charge = function (t) { return arguments.length ? (d = "function" == typeof t ? t : +t, s) : d }, s.chargeDistance = function (t) { return arguments.length ? (v = t * t, s) : Math.sqrt(v) }, s.gravity = function (t) { return arguments.length ? (g = +t, s) : g }, s.theta = function (t) { return arguments.length ? (y = t * t, s) : Math.sqrt(y) }, s.alpha = function (t) { return arguments.length ? (t = +t, o ? t > 0 ? o = t : (n.c = null, n.t = NaN, n = null, c.end({ type: "end", alpha: o = 0 })) : t > 0 && (c.start({ type: "start", alpha: o = t }), n = Rt(s.tick)), s) : o }, s.start = function () {
                function t(t, r) {
                    if (!n) {
                        for (n = new Array(o), s = 0; s < o; ++s) n[s] = [];
                        for (s = 0; s < c; ++s) {
                            var i = b[s];
                            n[i.source.index].push(i.target), n[i.target.index].push(i.source)
                        }
                    }
                    for (var a, u = n[e], s = -1, l = u.length; ++s < l;)
                        if (!isNaN(a = u[s][t])) return a;
                    return Math.random() * r
                }
                var e, n, r, o = m.length,
                    c = b.length,
                    f = l[0],
                    v = l[1];
                for (e = 0; e < o; ++e)(r = m[e]).index = e, r.weight = 0;
                for (e = 0; e < c; ++e) r = b[e], "number" == typeof r.source && (r.source = m[r.source]), "number" == typeof r.target && (r.target = m[r.target]), ++r.source.weight, ++r.target.weight;
                for (e = 0; e < o; ++e) r = m[e], isNaN(r.x) && (r.x = t("x", f)), isNaN(r.y) && (r.y = t("y", v)), isNaN(r.px) && (r.px = r.x), isNaN(r.py) && (r.py = r.y);
                if (i = [], "function" == typeof p)
                    for (e = 0; e < c; ++e) i[e] = +p.call(this, b[e], e);
                else
                    for (e = 0; e < c; ++e) i[e] = p;
                if (a = [], "function" == typeof h)
                    for (e = 0; e < c; ++e) a[e] = +h.call(this, b[e], e);
                else
                    for (e = 0; e < c; ++e) a[e] = h;
                if (u = [], "function" == typeof d)
                    for (e = 0; e < o; ++e) u[e] = +d.call(this, m[e], e);
                else
                    for (e = 0; e < o; ++e) u[e] = d;
                return s.resume()
            }, s.resume = function () { return s.alpha(.1) }, s.stop = function () { return s.alpha(0) }, s.drag = function () { return r || (r = ha.behavior.drag().origin(C).on("dragstart.force", oo).on("drag.force", e).on("dragend.force", io)), arguments.length ? void this.on("mouseover.force", ao).on("mouseout.force", uo).call(r) : r }, ha.rebind(s, c, "on")
        };
        var ws = 20,
            _s = 1,
            Cs = 1 / 0;
        ha.layout.hierarchy = function () {
            function t(o) {
                var i, a = [o],
                    u = [];
                for (o.depth = 0; null != (i = a.pop());)
                    if (u.push(i), (c = n.call(t, i, i.depth)) && (s = c.length)) {
                        for (var s, c, l; --s >= 0;) a.push(l = c[s]), l.parent = i, l.depth = i.depth + 1;
                        r && (i.value = 0), i.children = c
                    } else r && (i.value = +r.call(t, i, i.depth) || 0), delete i.children;
                return fo(o, function (t) {
                    var n, o;
                    e && (n = t.children) && n.sort(e), r && (o = t.parent) && (o.value += t.value)
                }), u
            }
            var e = vo,
                n = po,
                r = ho;
            return t.sort = function (n) { return arguments.length ? (e = n, t) : e }, t.children = function (e) { return arguments.length ? (n = e, t) : n }, t.value = function (e) { return arguments.length ? (r = e, t) : r }, t.revalue = function (e) {
                return r && (lo(e, function (t) { t.children && (t.value = 0) }), fo(e, function (e) {
                    var n;
                    e.children || (e.value = +r.call(t, e, e.depth) || 0), (n = e.parent) && (n.value += e.value)
                })), e
            }, t
        }, ha.layout.partition = function () {
            function t(e, n, r, o) { var i = e.children; if (e.x = n, e.y = e.depth * o, e.dx = r, e.dy = o, i && (a = i.length)) { var a, u, s, c = -1; for (r = e.value ? r / e.value : 0; ++c < a;) t(u = i[c], n, s = u.value * r, o), n += s } }

            function e(t) {
                var n = t.children,
                    r = 0;
                if (n && (o = n.length))
                    for (var o, i = -1; ++i < o;) r = Math.max(r, e(n[i]));
                return 1 + r
            }

            function n(n, i) { var a = r.call(this, n, i); return t(a[0], 0, o[0], o[1] / e(a[0])), a }
            var r = ha.layout.hierarchy(),
                o = [1, 1];
            return n.size = function (t) { return arguments.length ? (o = t, n) : o }, co(n, r)
        }, ha.layout.pie = function () {
            function t(a) {
                var u, s = a.length,
                    c = a.map(function (n, r) { return +e.call(t, n, r) }),
                    l = +("function" == typeof r ? r.apply(this, arguments) : r),
                    f = ("function" == typeof o ? o.apply(this, arguments) : o) - l,
                    p = Math.min(Math.abs(f) / s, +("function" == typeof i ? i.apply(this, arguments) : i)),
                    h = p * (f < 0 ? -1 : 1),
                    d = ha.sum(c),
                    v = d ? (f - s * h) / d : 0,
                    g = ha.range(s),
                    y = [];
                return null != n && g.sort(n === Es ? function (t, e) { return c[e] - c[t] } : function (t, e) { return n(a[t], a[e]) }), g.forEach(function (t) { y[t] = { data: a[t], value: u = c[t], startAngle: l, endAngle: l += u * v + h, padAngle: p } }), y
            }
            var e = Number,
                n = Es,
                r = 0,
                o = Wa,
                i = 0;
            return t.value = function (n) { return arguments.length ? (e = n, t) : e }, t.sort = function (e) { return arguments.length ? (n = e, t) : n }, t.startAngle = function (e) { return arguments.length ? (r = e, t) : r }, t.endAngle = function (e) { return arguments.length ? (o = e, t) : o }, t.padAngle = function (e) { return arguments.length ? (i = e, t) : i }, t
        };
        var Es = {};
        ha.layout.stack = function () {
            function t(u, s) {
                if (!(p = u.length)) return u;
                var c = u.map(function (n, r) { return e.call(t, n, r) }),
                    l = c.map(function (e) { return e.map(function (e, n) { return [i.call(t, e, n), a.call(t, e, n)] }) }),
                    f = n.call(t, l, s);
                c = ha.permute(c, f), l = ha.permute(l, f);
                var p, h, d, v, g = r.call(t, l, s),
                    y = c[0].length;
                for (d = 0; d < y; ++d)
                    for (o.call(t, c[0][d], v = g[d], l[0][d][1]), h = 1; h < p; ++h) o.call(t, c[h][d], v += l[h - 1][d][1], l[h][d][1]);
                return u
            }
            var e = C,
                n = xo,
                r = wo,
                o = bo,
                i = yo,
                a = mo;
            return t.values = function (n) { return arguments.length ? (e = n, t) : e }, t.order = function (e) { return arguments.length ? (n = "function" == typeof e ? e : Ms.get(e) || xo, t) : n }, t.offset = function (e) { return arguments.length ? (r = "function" == typeof e ? e : ks.get(e) || wo, t) : r }, t.x = function (e) { return arguments.length ? (i = e, t) : i }, t.y = function (e) { return arguments.length ? (a = e, t) : a }, t.out = function (e) { return arguments.length ? (o = e, t) : o }, t
        };
        var Ms = ha.map({
            "inside-out": function (t) {
                var e, n, r = t.length,
                    o = t.map(_o),
                    i = t.map(Co),
                    a = ha.range(r).sort(function (t, e) { return o[t] - o[e] }),
                    u = 0,
                    s = 0,
                    c = [],
                    l = [];
                for (e = 0; e < r; ++e) n = a[e], u < s ? (u += i[n], c.push(n)) : (s += i[n], l.push(n));
                return l.reverse().concat(c)
            },
            reverse: function (t) { return ha.range(t.length).reverse() },
            default: xo
        }),
            ks = ha.map({
                silhouette: function (t) {
                    var e, n, r, o = t.length,
                        i = t[0].length,
                        a = [],
                        u = 0,
                        s = [];
                    for (n = 0; n < i; ++n) {
                        for (e = 0, r = 0; e < o; e++) r += t[e][n][1];
                        r > u && (u = r), a.push(r)
                    }
                    for (n = 0; n < i; ++n) s[n] = (u - a[n]) / 2;
                    return s
                },
                wiggle: function (t) {
                    var e, n, r, o, i, a, u, s, c, l = t.length,
                        f = t[0],
                        p = f.length,
                        h = [];
                    for (h[0] = s = c = 0, n = 1; n < p; ++n) {
                        for (e = 0, o = 0; e < l; ++e) o += t[e][n][1];
                        for (e = 0, i = 0, u = f[n][0] - f[n - 1][0]; e < l; ++e) {
                            for (r = 0, a = (t[e][n][1] - t[e][n - 1][1]) / (2 * u); r < e; ++r) a += (t[r][n][1] - t[r][n - 1][1]) / u;
                            i += a * t[e][n][1]
                        }
                        h[n] = s -= o ? i / o * u : 0, s < c && (c = s)
                    }
                    for (n = 0; n < p; ++n) h[n] -= c;
                    return h
                },
                expand: function (t) {
                    var e, n, r, o = t.length,
                        i = t[0].length,
                        a = 1 / o,
                        u = [];
                    for (n = 0; n < i; ++n) {
                        for (e = 0, r = 0; e < o; e++) r += t[e][n][1];
                        if (r)
                            for (e = 0; e < o; e++) t[e][n][1] /= r;
                        else
                            for (e = 0; e < o; e++) t[e][n][1] = a
                    }
                    for (n = 0; n < i; ++n) u[n] = 0;
                    return u
                },
                zero: wo
            });
        ha.layout.histogram = function () {
            function t(t, i) {
                for (var a, u, s = [], c = t.map(n, this), l = r.call(this, c, i), f = o.call(this, l, c, i), i = -1, p = c.length, h = f.length - 1, d = e ? 1 : 1 / p; ++i < h;) a = s[i] = [], a.dx = f[i + 1] - (a.x = f[i]), a.y = 0;
                if (h > 0)
                    for (i = -1; ++i < p;) u = c[i], u >= l[0] && u <= l[1] && (a = s[ha.bisect(f, u, 1, h) - 1], a.y += d, a.push(t[i]));
                return s
            }
            var e = !0,
                n = Number,
                r = To,
                o = Mo;
            return t.value = function (e) { return arguments.length ? (n = e, t) : n }, t.range = function (e) { return arguments.length ? (r = At(e), t) : r }, t.bins = function (e) { return arguments.length ? (o = "number" == typeof e ? function (t) { return ko(t, e) } : At(e), t) : o }, t.frequency = function (n) { return arguments.length ? (e = !!n, t) : e }, t
        }, ha.layout.pack = function () {
            function t(t, i) {
                var a = n.call(this, t, i),
                    u = a[0],
                    s = o[0],
                    c = o[1],
                    l = null == e ? Math.sqrt : "function" == typeof e ? e : function () { return e };
                if (u.x = u.y = 0, fo(u, function (t) { t.r = +l(t.value) }), fo(u, Po), r) {
                    var f = r * (e ? 1 : Math.max(2 * u.r / s, 2 * u.r / c)) / 2;
                    fo(u, function (t) { t.r += f }), fo(u, Po), fo(u, function (t) { t.r -= f })
                }
                return Io(u, s / 2, c / 2, e ? 1 : 1 / Math.max(2 * u.r / s, 2 * u.r / c)), a
            }
            var e, n = ha.layout.hierarchy().sort(So),
                r = 0,
                o = [1, 1];
            return t.size = function (e) { return arguments.length ? (o = e, t) : o }, t.radius = function (n) { return arguments.length ? (e = null == n || "function" == typeof n ? n : +n, t) : e }, t.padding = function (e) { return arguments.length ? (r = +e, t) : r }, co(t, n)
        }, ha.layout.tree = function () {
            function t(t, o) {
                var l = a.call(this, t, o),
                    f = l[0],
                    p = e(f);
                if (fo(p, n), p.parent.m = -p.z, lo(p, r), c) lo(f, i);
                else {
                    var h = f,
                        d = f,
                        v = f;
                    lo(f, function (t) { t.x < h.x && (h = t), t.x > d.x && (d = t), t.depth > v.depth && (v = t) });
                    var g = u(h, d) / 2 - h.x,
                        y = s[0] / (d.x + u(d, h) / 2 + g),
                        m = s[1] / (v.depth || 1);
                    lo(f, function (t) { t.x = (t.x + g) * y, t.y = t.depth * m })
                }
                return l
            }

            function e(t) {
                for (var e, n = { A: null, children: [t] }, r = [n]; null != (e = r.pop());)
                    for (var o, i = e.children, a = 0, u = i.length; a < u; ++a) r.push((i[a] = o = { _: i[a], parent: e, children: (o = i[a].children) && o.slice() || [], A: null, a: null, z: 0, m: 0, c: 0, s: 0, t: null, i: a }).a = o);
                return n.children[0]
            }

            function n(t) {
                var e = t.children,
                    n = t.parent.children,
                    r = t.i ? n[t.i - 1] : null;
                if (e.length) {
                    Ho(t);
                    var i = (e[0].z + e[e.length - 1].z) / 2;
                    r ? (t.z = r.z + u(t._, r._), t.m = t.z - i) : t.z = i
                } else r && (t.z = r.z + u(t._, r._));
                t.parent.A = o(t, r, t.parent.A || n[0])
            }

            function r(t) { t._.x = t.z + t.parent.m, t.m += t.parent.m }

            function o(t, e, n) {
                if (e) {
                    for (var r, o = t, i = t, a = e, s = o.parent.children[0], c = o.m, l = i.m, f = a.m, p = s.m; a = Uo(a), o = Fo(o), a && o;) s = Fo(s), i = Uo(i), i.a = t, r = a.z + f - o.z - c + u(a._, o._), r > 0 && (qo(Bo(a, t, n), t, r), c += r, l += r), f += a.m, c += o.m, p += s.m, l += i.m;
                    a && !Uo(i) && (i.t = a, i.m += f - l), o && !Fo(s) && (s.t = o, s.m += c - p, n = t)
                }
                return n
            }

            function i(t) { t.x *= s[0], t.y = t.depth * s[1] }
            var a = ha.layout.hierarchy().sort(null).value(null),
                u = Lo,
                s = [1, 1],
                c = null;
            return t.separation = function (e) { return arguments.length ? (u = e, t) : u }, t.size = function (e) { return arguments.length ? (c = null == (s = e) ? i : null, t) : c ? null : s }, t.nodeSize = function (e) { return arguments.length ? (c = null == (s = e) ? null : i, t) : c ? s : null }, co(t, a)
        }, ha.layout.cluster = function () {
            function t(t, i) {
                var a, u = e.call(this, t, i),
                    s = u[0],
                    c = 0;
                fo(s, function (t) {
                    var e = t.children;
                    e && e.length ? (t.x = zo(e), t.y = Wo(e)) : (t.x = a ? c += n(t, a) : 0, t.y = 0, a = t)
                });
                var l = Vo(s),
                    f = Yo(s),
                    p = l.x - n(l, f) / 2,
                    h = f.x + n(f, l) / 2;
                return fo(s, o ? function (t) { t.x = (t.x - s.x) * r[0], t.y = (s.y - t.y) * r[1] } : function (t) { t.x = (t.x - p) / (h - p) * r[0], t.y = (1 - (s.y ? t.y / s.y : 1)) * r[1] }), u
            }
            var e = ha.layout.hierarchy().sort(null).value(null),
                n = Lo,
                r = [1, 1],
                o = !1;
            return t.separation = function (e) { return arguments.length ? (n = e, t) : n }, t.size = function (e) { return arguments.length ? (o = null == (r = e), t) : o ? null : r }, t.nodeSize = function (e) { return arguments.length ? (o = null != (r = e), t) : o ? r : null }, co(t, e)
        }, ha.layout.treemap = function () {
            function t(t, e) { for (var n, r, o = -1, i = t.length; ++o < i;) r = (n = t[o]).value * (e < 0 ? 0 : e), n.area = isNaN(r) || r <= 0 ? 0 : r }

            function e(n) {
                var i = n.children;
                if (i && i.length) {
                    var a, u, s, c = f(n),
                        l = [],
                        p = i.slice(),
                        d = 1 / 0,
                        v = "slice" === h ? c.dx : "dice" === h ? c.dy : "slice-dice" === h ? 1 & n.depth ? c.dy : c.dx : Math.min(c.dx, c.dy);
                    for (t(p, c.dx * c.dy / n.value), l.area = 0;
                        (s = p.length) > 0;) l.push(a = p[s - 1]), l.area += a.area, "squarify" !== h || (u = r(l, v)) <= d ? (p.pop(), d = u) : (l.area -= l.pop().area, o(l, v, c, !1), v = Math.min(c.dx, c.dy), l.length = l.area = 0, d = 1 / 0);
                    l.length && (o(l, v, c, !0), l.length = l.area = 0), i.forEach(e)
                }
            }

            function n(e) {
                var r = e.children;
                if (r && r.length) {
                    var i, a = f(e),
                        u = r.slice(),
                        s = [];
                    for (t(u, a.dx * a.dy / e.value), s.area = 0; i = u.pop();) s.push(i), s.area += i.area, null != i.z && (o(s, i.z ? a.dx : a.dy, a, !u.length), s.length = s.area = 0);
                    r.forEach(n)
                }
            }

            function r(t, e) { for (var n, r = t.area, o = 0, i = 1 / 0, a = -1, u = t.length; ++a < u;)(n = t[a].area) && (n < i && (i = n), n > o && (o = n)); return r *= r, e *= e, r ? Math.max(e * o * d / r, r / (e * i * d)) : 1 / 0 }

            function o(t, e, n, r) {
                var o, i = -1,
                    a = t.length,
                    u = n.x,
                    c = n.y,
                    l = e ? s(t.area / e) : 0;
                if (e == n.dx) {
                    for ((r || l > n.dy) && (l = n.dy); ++i < a;) o = t[i], o.x = u, o.y = c, o.dy = l, u += o.dx = Math.min(n.x + n.dx - u, l ? s(o.area / l) : 0);
                    o.z = !0, o.dx += n.x + n.dx - u, n.y += l, n.dy -= l
                } else {
                    for ((r || l > n.dx) && (l = n.dx); ++i < a;) o = t[i], o.x = u, o.y = c, o.dx = l, c += o.dy = Math.min(n.y + n.dy - c, l ? s(o.area / l) : 0);
                    o.z = !1, o.dy += n.y + n.dy - c, n.x += l, n.dx -= l
                }
            }

            function i(r) {
                var o = a || u(r),
                    i = o[0];
                return i.x = i.y = 0, i.value ? (i.dx = c[0], i.dy = c[1]) : i.dx = i.dy = 0, a && u.revalue(i), t([i], i.dx * i.dy / i.value), (a ? n : e)(i), p && (a = o), o
            }
            var a, u = ha.layout.hierarchy(),
                s = Math.round,
                c = [1, 1],
                l = null,
                f = $o,
                p = !1,
                h = "squarify",
                d = .5 * (1 + Math.sqrt(5));
            return i.size = function (t) { return arguments.length ? (c = t, i) : c }, i.padding = function (t) {
                function e(e) { var n = t.call(i, e, e.depth); return null == n ? $o(e) : Ko(e, "number" == typeof n ? [n, n, n, n] : n) }

                function n(e) { return Ko(e, t) }
                if (!arguments.length) return l;
                var r;
                return f = null == (l = t) ? $o : "function" == (r = typeof t) ? e : "number" === r ? (t = [t, t, t, t], n) : n, i
            }, i.round = function (t) { return arguments.length ? (s = t ? Math.round : Number, i) : s != Number }, i.sticky = function (t) { return arguments.length ? (p = t, a = null, i) : p }, i.ratio = function (t) { return arguments.length ? (d = t, i) : d }, i.mode = function (t) { return arguments.length ? (h = t + "", i) : h }, co(i, u)
        }, ha.random = {
            normal: function (t, e) {
                var n = arguments.length;
                return n < 2 && (e = 1), n < 1 && (t = 0),
                    function () {
                        var n, r, o;
                        do n = 2 * Math.random() - 1, r = 2 * Math.random() - 1, o = n * n + r * r; while (!o || o > 1);
                        return t + e * n * Math.sqrt(-2 * Math.log(o) / o)
                    }
            },
            logNormal: function () { var t = ha.random.normal.apply(ha, arguments); return function () { return Math.exp(t()) } },
            bates: function (t) { var e = ha.random.irwinHall(t); return function () { return e() / t } },
            irwinHall: function (t) { return function () { for (var e = 0, n = 0; n < t; n++) e += Math.random(); return e } }
        }, ha.scale = {};
        var Ts = { floor: C, ceil: C };
        ha.scale.linear = function () { return ei([0, 1], [0, 1], Er, !1) };
        var Ss = {
            s: 1,
            g: 1,
            p: 1,
            r: 1,
            e: 1
        };
        ha.scale.log = function () { return ci(ha.scale.linear().domain([0, 1]), 10, !0, [1, 10]) };
        var Ns = ha.format(".0e"),
            Os = { floor: function (t) { return -Math.ceil(-t) }, ceil: function (t) { return -Math.floor(-t) } };
        ha.scale.pow = function () { return li(ha.scale.linear(), 1, [0, 1]) }, ha.scale.sqrt = function () { return ha.scale.pow().exponent(.5) }, ha.scale.ordinal = function () {
            return pi([], {
                t: "range",
                a: [
                    []
                ]
            })
        }, ha.scale.category10 = function () { return ha.scale.ordinal().range(As) }, ha.scale.category20 = function () { return ha.scale.ordinal().range(Ps) }, ha.scale.category20b = function () { return ha.scale.ordinal().range(js) }, ha.scale.category20c = function () { return ha.scale.ordinal().range(Ds) };
        var As = [2062260, 16744206, 2924588, 14034728, 9725885, 9197131, 14907330, 8355711, 12369186, 1556175].map(Et),
            Ps = [2062260, 11454440, 16744206, 16759672, 2924588, 10018698, 14034728, 16750742, 9725885, 12955861, 9197131, 12885140, 14907330, 16234194, 8355711, 13092807, 12369186, 14408589, 1556175, 10410725].map(Et),
            js = [3750777, 5395619, 7040719, 10264286, 6519097, 9216594, 11915115, 13556636, 9202993, 12426809, 15186514, 15190932, 8666169, 11356490, 14049643, 15177372, 8077683, 10834324, 13528509, 14589654].map(Et),
            Ds = [3244733, 7057110, 10406625, 13032431, 15095053, 16616764, 16625259, 16634018, 3253076, 7652470, 10607003, 13101504, 7695281, 10394312, 12369372, 14342891, 6513507, 9868950, 12434877, 14277081].map(Et);
        ha.scale.quantile = function () { return hi([], []) }, ha.scale.quantize = function () { return di(0, 1, [0, 1]) }, ha.scale.threshold = function () { return vi([.5], [0, 1]) }, ha.scale.identity = function () { return gi([0, 1]) }, ha.svg = {}, ha.svg.arc = function () {
            function t() {
                var t = Math.max(0, +n.apply(this, arguments)),
                    c = Math.max(0, +r.apply(this, arguments)),
                    l = a.apply(this, arguments) - Va,
                    f = u.apply(this, arguments) - Va,
                    p = Math.abs(f - l),
                    h = l > f ? 0 : 1;
                if (c < t && (d = c, c = t, t = d), p >= za) return e(c, h) + (t ? e(t, 1 - h) : "") + "Z";
                var d, v, g, y, m, b, x, w, _, C, E, M, k = 0,
                    T = 0,
                    S = [];
                if ((y = (+s.apply(this, arguments) || 0) / 2) && (g = i === Is ? Math.sqrt(t * t + c * c) : +i.apply(this, arguments), h || (T *= -1), c && (T = at(g / c * Math.sin(y))), t && (k = at(g / t * Math.sin(y)))), c) {
                    m = c * Math.cos(l + T), b = c * Math.sin(l + T), x = c * Math.cos(f - T), w = c * Math.sin(f - T);
                    var N = Math.abs(f - l - 2 * T) <= Ba ? 0 : 1;
                    if (T && Ci(m, b, x, w) === h ^ N) {
                        var O = (l + f) / 2;
                        m = c * Math.cos(O), b = c * Math.sin(O), x = w = null
                    }
                } else m = b = 0;
                if (t) {
                    _ = t * Math.cos(f - k), C = t * Math.sin(f - k), E = t * Math.cos(l + k), M = t * Math.sin(l + k);
                    var A = Math.abs(l - f + 2 * k) <= Ba ? 0 : 1;
                    if (k && Ci(_, C, E, M) === 1 - h ^ A) {
                        var P = (l + f) / 2;
                        _ = t * Math.cos(P), C = t * Math.sin(P), E = M = null
                    }
                } else _ = C = 0;
                if (p > qa && (d = Math.min(Math.abs(c - t) / 2, +o.apply(this, arguments))) > .001) {
                    v = t < c ^ h ? 0 : 1;
                    var j = d,
                        D = d;
                    if (p < Ba) {
                        var I = null == E ? [_, C] : null == x ? [m, b] : Un([m, b], [E, M], [x, w], [_, C]),
                            R = m - I[0],
                            L = b - I[1],
                            F = x - I[0],
                            U = w - I[1],
                            q = 1 / Math.sin(Math.acos((R * F + L * U) / (Math.sqrt(R * R + L * L) * Math.sqrt(F * F + U * U))) / 2),
                            H = Math.sqrt(I[0] * I[0] + I[1] * I[1]);
                        D = Math.min(d, (t - H) / (q - 1)), j = Math.min(d, (c - H) / (q + 1))
                    }
                    if (null != x) {
                        var B = Ei(null == E ? [_, C] : [E, M], [m, b], c, j, h),
                            W = Ei([x, w], [_, C], c, j, h);
                        d === j ? S.push("M", B[0], "A", j, ",", j, " 0 0,", v, " ", B[1], "A", c, ",", c, " 0 ", 1 - h ^ Ci(B[1][0], B[1][1], W[1][0], W[1][1]), ",", h, " ", W[1], "A", j, ",", j, " 0 0,", v, " ", W[0]) : S.push("M", B[0], "A", j, ",", j, " 0 1,", v, " ", W[0])
                    } else S.push("M", m, ",", b);
                    if (null != E) {
                        var z = Ei([m, b], [E, M], t, -D, h),
                            V = Ei([_, C], null == x ? [m, b] : [x, w], t, -D, h);
                        d === D ? S.push("L", V[0], "A", D, ",", D, " 0 0,", v, " ", V[1], "A", t, ",", t, " 0 ", h ^ Ci(V[1][0], V[1][1], z[1][0], z[1][1]), ",", 1 - h, " ", z[1], "A", D, ",", D, " 0 0,", v, " ", z[0]) : S.push("L", V[0], "A", D, ",", D, " 0 0,", v, " ", z[0])
                    } else S.push("L", _, ",", C)
                } else S.push("M", m, ",", b), null != x && S.push("A", c, ",", c, " 0 ", N, ",", h, " ", x, ",", w), S.push("L", _, ",", C), null != E && S.push("A", t, ",", t, " 0 ", A, ",", 1 - h, " ", E, ",", M);
                return S.push("Z"), S.join("")
            }

            function e(t, e) { return "M0," + t + "A" + t + "," + t + " 0 1," + e + " 0," + -t + "A" + t + "," + t + " 0 1," + e + " 0," + t }
            var n = mi,
                r = bi,
                o = yi,
                i = Is,
                a = xi,
                u = wi,
                s = _i;
            return t.innerRadius = function (e) { return arguments.length ? (n = At(e), t) : n }, t.outerRadius = function (e) { return arguments.length ? (r = At(e), t) : r }, t.cornerRadius = function (e) { return arguments.length ? (o = At(e), t) : o }, t.padRadius = function (e) { return arguments.length ? (i = e == Is ? Is : At(e), t) : i }, t.startAngle = function (e) { return arguments.length ? (a = At(e), t) : a }, t.endAngle = function (e) { return arguments.length ? (u = At(e), t) : u }, t.padAngle = function (e) { return arguments.length ? (s = At(e), t) : s }, t.centroid = function () {
                var t = (+n.apply(this, arguments) + +r.apply(this, arguments)) / 2,
                    e = (+a.apply(this, arguments) + +u.apply(this, arguments)) / 2 - Va;
                return [Math.cos(e) * t, Math.sin(e) * t]
            }, t
        };
        var Is = "auto";
        ha.svg.line = function () { return Mi(C) };
        var Rs = ha.map({ linear: ki, "linear-closed": Ti, step: Si, "step-before": Ni, "step-after": Oi, basis: Ri, "basis-open": Li, "basis-closed": Fi, bundle: Ui, cardinal: ji, "cardinal-open": Ai, "cardinal-closed": Pi, monotone: Vi });
        Rs.forEach(function (t, e) { e.key = t, e.closed = /-closed$/.test(t) });
        var Ls = [0, 2 / 3, 1 / 3, 0],
            Fs = [0, 1 / 3, 2 / 3, 0],
            Us = [0, 1 / 6, 2 / 3, 1 / 6];
        ha.svg.line.radial = function () { var t = Mi(Yi); return t.radius = t.x, delete t.x, t.angle = t.y, delete t.y, t }, Ni.reverse = Oi, Oi.reverse = Ni, ha.svg.area = function () { return $i(C) }, ha.svg.area.radial = function () { var t = $i(Yi); return t.radius = t.x, delete t.x, t.innerRadius = t.x0, delete t.x0, t.outerRadius = t.x1, delete t.x1, t.angle = t.y, delete t.y, t.startAngle = t.y0, delete t.y0, t.endAngle = t.y1, delete t.y1, t }, ha.svg.chord = function () {
            function t(t, u) {
                var s = e(this, i, t, u),
                    c = e(this, a, t, u);
                return "M" + s.p0 + r(s.r, s.p1, s.a1 - s.a0) + (n(s, c) ? o(s.r, s.p1, s.r, s.p0) : o(s.r, s.p1, c.r, c.p0) + r(c.r, c.p1, c.a1 - c.a0) + o(c.r, c.p1, s.r, s.p0)) + "Z"
            }

            function e(t, e, n, r) {
                var o = e.call(t, n, r),
                    i = u.call(t, o, r),
                    a = s.call(t, o, r) - Va,
                    l = c.call(t, o, r) - Va;
                return { r: i, a0: a, a1: l, p0: [i * Math.cos(a), i * Math.sin(a)], p1: [i * Math.cos(l), i * Math.sin(l)] }
            }

            function n(t, e) { return t.a0 == e.a0 && t.a1 == e.a1 }

            function r(t, e, n) { return "A" + t + "," + t + " 0 " + +(n > Ba) + ",1 " + e }

            function o(t, e, n, r) { return "Q 0,0 " + r }
            var i = En,
                a = Mn,
                u = Ki,
                s = xi,
                c = wi;
            return t.radius = function (e) { return arguments.length ? (u = At(e), t) : u }, t.source = function (e) { return arguments.length ? (i = At(e), t) : i }, t.target = function (e) { return arguments.length ? (a = At(e), t) : a }, t.startAngle = function (e) { return arguments.length ? (s = At(e), t) : s }, t.endAngle = function (e) { return arguments.length ? (c = At(e), t) : c }, t
        }, ha.svg.diagonal = function () {
            function t(t, o) {
                var i = e.call(this, t, o),
                    a = n.call(this, t, o),
                    u = (i.y + a.y) / 2,
                    s = [i, { x: i.x, y: u }, { x: a.x, y: u }, a];
                return s = s.map(r), "M" + s[0] + "C" + s[1] + " " + s[2] + " " + s[3]
            }
            var e = En,
                n = Mn,
                r = Xi;
            return t.source = function (n) { return arguments.length ? (e = At(n), t) : e }, t.target = function (e) { return arguments.length ? (n = At(e), t) : n }, t.projection = function (e) { return arguments.length ? (r = e, t) : r }, t
        }, ha.svg.diagonal.radial = function () {
            var t = ha.svg.diagonal(),
                e = Xi,
                n = t.projection;
            return t.projection = function (t) { return arguments.length ? n(Gi(e = t)) : e }, t
        }, ha.svg.symbol = function () {
            function t(t, r) { return (qs.get(e.call(this, t, r)) || Zi)(n.call(this, t, r)) }
            var e = Qi,
                n = Ji;
            return t.type = function (n) { return arguments.length ? (e = At(n), t) : e }, t.size = function (e) { return arguments.length ? (n = At(e), t) : n }, t
        };
        var qs = ha.map({
            circle: Zi,
            cross: function (t) { var e = Math.sqrt(t / 5) / 2; return "M" + -3 * e + "," + -e + "H" + -e + "V" + -3 * e + "H" + e + "V" + -e + "H" + 3 * e + "V" + e + "H" + e + "V" + 3 * e + "H" + -e + "V" + e + "H" + -3 * e + "Z" },
            diamond: function (t) {
                var e = Math.sqrt(t / (2 * Bs)),
                    n = e * Bs;
                return "M0," + -e + "L" + n + ",0 0," + e + " " + -n + ",0Z"
            },
            square: function (t) { var e = Math.sqrt(t) / 2; return "M" + -e + "," + -e + "L" + e + "," + -e + " " + e + "," + e + " " + -e + "," + e + "Z" },
            "triangle-down": function (t) {
                var e = Math.sqrt(t / Hs),
                    n = e * Hs / 2;
                return "M0," + n + "L" + e + "," + -n + " " + -e + "," + -n + "Z"
            },
            "triangle-up": function (t) {
                var e = Math.sqrt(t / Hs),
                    n = e * Hs / 2;
                return "M0," + -n + "L" + e + "," + n + " " + -e + "," + n + "Z"
            }
        });
        ha.svg.symbolTypes = qs.keys();
        var Hs = Math.sqrt(3),
            Bs = Math.tan(30 * Ya);
        Pa.transition = function (t) { for (var e, n, r = Ws || ++$s, o = oa(t), i = [], a = zs || { time: Date.now(), ease: Ar, delay: 0, duration: 250 }, u = -1, s = this.length; ++u < s;) { i.push(e = []); for (var c = this[u], l = -1, f = c.length; ++l < f;)(n = c[l]) && ia(n, l, o, r, a), e.push(n) } return ea(i, o, r) }, Pa.interrupt = function (t) { return this.each(null == t ? Vs : ta(oa(t))) };
        var Ws, zs, Vs = ta(oa()),
            Ys = [],
            $s = 0;
        Ys.call = Pa.call, Ys.empty = Pa.empty, Ys.node = Pa.node, Ys.size = Pa.size, ha.transition = function (t, e) { return t && t.transition ? Ws ? t.transition(e) : t : ha.selection().transition(t) }, ha.transition.prototype = Ys, Ys.select = function (t) {
            var e, n, r, o = this.id,
                i = this.namespace,
                a = [];
            t = j(t);
            for (var u = -1, s = this.length; ++u < s;) { a.push(e = []); for (var c = this[u], l = -1, f = c.length; ++l < f;)(r = c[l]) && (n = t.call(r, r.__data__, l, u)) ? ("__data__" in r && (n.__data__ = r.__data__), ia(n, l, i, o, r[i][o]), e.push(n)) : e.push(null) }
            return ea(a, i, o)
        }, Ys.selectAll = function (t) {
            var e, n, r, o, i, a = this.id,
                u = this.namespace,
                s = [];
            t = D(t);
            for (var c = -1, l = this.length; ++c < l;)
                for (var f = this[c], p = -1, h = f.length; ++p < h;)
                    if (r = f[p]) { i = r[u][a], n = t.call(r, r.__data__, p, c), s.push(e = []); for (var d = -1, v = n.length; ++d < v;)(o = n[d]) && ia(o, d, u, a, i), e.push(o) }
            return ea(s, u, a)
        }, Ys.filter = function (t) { var e, n, r, o = []; "function" != typeof t && (t = Y(t)); for (var i = 0, a = this.length; i < a; i++) { o.push(e = []); for (var n = this[i], u = 0, s = n.length; u < s; u++)(r = n[u]) && t.call(r, r.__data__, u, i) && e.push(r) } return ea(o, this.namespace, this.id) }, Ys.tween = function (t, e) {
            var n = this.id,
                r = this.namespace;
            return arguments.length < 2 ? this.node()[r][n].tween.get(t) : K(this, null == e ? function (e) { e[r][n].tween.remove(t) } : function (o) { o[r][n].tween.set(t, e) })
        }, Ys.attr = function (t, e) {
            function n() { this.removeAttribute(u) }

            function r() { this.removeAttributeNS(u.space, u.local) }

            function o(t) { return null == t ? n : (t += "", function () { var e, n = this.getAttribute(u); return n !== t && (e = a(n, t), function (t) { this.setAttribute(u, e(t)) }) }) }

            function i(t) { return null == t ? r : (t += "", function () { var e, n = this.getAttributeNS(u.space, u.local); return n !== t && (e = a(n, t), function (t) { this.setAttributeNS(u.space, u.local, e(t)) }) }) }
            if (arguments.length < 2) { for (e in t) this.attr(e, t[e]); return this }
            var a = "transform" == t ? Qr : Er,
                u = ha.ns.qualify(t);
            return na(this, "attr." + t, e, u.local ? i : o)
        }, Ys.attrTween = function (t, e) {
            function n(t, n) { var r = e.call(this, t, n, this.getAttribute(o)); return r && function (t) { this.setAttribute(o, r(t)) } }

            function r(t, n) { var r = e.call(this, t, n, this.getAttributeNS(o.space, o.local)); return r && function (t) { this.setAttributeNS(o.space, o.local, r(t)) } }
            var o = ha.ns.qualify(t);
            return this.tween("attr." + t, o.local ? r : n)
        }, Ys.style = function (t, e, n) {
            function r() { this.style.removeProperty(t) }

            function o(e) { return null == e ? r : (e += "", function () { var r, o = a(this).getComputedStyle(this, null).getPropertyValue(t); return o !== e && (r = Er(o, e), function (e) { this.style.setProperty(t, r(e), n) }) }) }
            var i = arguments.length;
            if (i < 3) {
                if ("string" != typeof t) { i < 2 && (e = ""); for (n in t) this.style(n, t[n], e); return this }
                n = ""
            }
            return na(this, "style." + t, e, o)
        }, Ys.styleTween = function (t, e, n) {
            function r(r, o) { var i = e.call(this, r, o, a(this).getComputedStyle(this, null).getPropertyValue(t)); return i && function (e) { this.style.setProperty(t, i(e), n) } }
            return arguments.length < 3 && (n = ""), this.tween("style." + t, r)
        }, Ys.text = function (t) { return na(this, "text", t, ra) }, Ys.remove = function () {
            var t = this.namespace;
            return this.each("end.transition", function () {
                var e;
                this[t].count < 2 && (e = this.parentNode) && e.removeChild(this)
            })
        }, Ys.ease = function (t) {
            var e = this.id,
                n = this.namespace;
            return arguments.length < 1 ? this.node()[n][e].ease : ("function" != typeof t && (t = ha.ease.apply(ha, arguments)), K(this, function (r) { r[n][e].ease = t }))
        }, Ys.delay = function (t) {
            var e = this.id,
                n = this.namespace;
            return arguments.length < 1 ? this.node()[n][e].delay : K(this, "function" == typeof t ? function (r, o, i) { r[n][e].delay = +t.call(r, r.__data__, o, i) } : (t = +t, function (r) { r[n][e].delay = t }))
        }, Ys.duration = function (t) {
            var e = this.id,
                n = this.namespace;
            return arguments.length < 1 ? this.node()[n][e].duration : K(this, "function" == typeof t ? function (r, o, i) { r[n][e].duration = Math.max(1, t.call(r, r.__data__, o, i)) } : (t = Math.max(1, t), function (r) { r[n][e].duration = t }))
        }, Ys.each = function (t, e) {
            var n = this.id,
                r = this.namespace;
            if (arguments.length < 2) {
                var o = zs,
                    i = Ws;
                try { Ws = n, K(this, function (e, o, i) { zs = e[r][n], t.call(e, e.__data__, o, i) }) } finally { zs = o, Ws = i }
            } else K(this, function (o) {
                var i = o[r][n];
                (i.event || (i.event = ha.dispatch("start", "end", "interrupt"))).on(t, e)
            });
            return this
        }, Ys.transition = function () { for (var t, e, n, r, o = this.id, i = ++$s, a = this.namespace, u = [], s = 0, c = this.length; s < c; s++) { u.push(t = []); for (var e = this[s], l = 0, f = e.length; l < f; l++)(n = e[l]) && (r = n[a][o], ia(n, l, a, i, { time: r.time, ease: r.ease, delay: r.delay + r.duration, duration: r.duration })), t.push(n) } return ea(u, a, i) }, ha.svg.axis = function () {
            function t(t) {
                t.each(function () {
                    var t, c = ha.select(this),
                        l = this.__chart__ || n,
                        f = this.__chart__ = n.copy(),
                        p = null == s ? f.ticks ? f.ticks.apply(f, u) : f.domain() : s,
                        h = null == e ? f.tickFormat ? f.tickFormat.apply(f, u) : C : e,
                        d = c.selectAll(".tick").data(p, f),
                        v = d.enter().insert("g", ".domain").attr("class", "tick").style("opacity", qa),
                        g = ha.transition(d.exit()).style("opacity", qa).remove(),
                        y = ha.transition(d.order()).style("opacity", 1),
                        m = Math.max(o, 0) + a,
                        b = Go(f),
                        x = c.selectAll(".domain").data([0]),
                        w = (x.enter().append("path").attr("class", "domain"), ha.transition(x));
                    v.append("line"), v.append("text");
                    var _, E, M, k, T = v.select("line"),
                        S = y.select("line"),
                        N = d.select("text").text(h),
                        O = v.select("text"),
                        A = y.select("text"),
                        P = "top" === r || "left" === r ? -1 : 1;
                    if ("bottom" === r || "top" === r ? (t = aa, _ = "x", M = "y", E = "x2", k = "y2", N.attr("dy", P < 0 ? "0em" : ".71em").style("text-anchor", "middle"), w.attr("d", "M" + b[0] + "," + P * i + "V0H" + b[1] + "V" + P * i)) : (t = ua, _ = "y", M = "x", E = "y2", k = "x2", N.attr("dy", ".32em").style("text-anchor", P < 0 ? "end" : "start"), w.attr("d", "M" + P * i + "," + b[0] + "H0V" + b[1] + "H" + P * i)), T.attr(k, P * o), O.attr(M, P * m), S.attr(E, 0).attr(k, P * o), A.attr(_, 0).attr(M, P * m), f.rangeBand) {
                        var j = f,
                            D = j.rangeBand() / 2;
                        l = f = function (t) { return j(t) + D }
                    } else l.rangeBand ? l = f : g.call(t, f, l);
                    v.call(t, l, f), y.call(t, f, f)
                })
            }
            var e, n = ha.scale.linear(),
                r = Ks,
                o = 6,
                i = 6,
                a = 3,
                u = [10],
                s = null;
            return t.scale = function (e) { return arguments.length ? (n = e, t) : n }, t.orient = function (e) { return arguments.length ? (r = e in Xs ? e + "" : Ks, t) : r }, t.ticks = function () { return arguments.length ? (u = va(arguments), t) : u }, t.tickValues = function (e) { return arguments.length ? (s = e, t) : s }, t.tickFormat = function (n) { return arguments.length ? (e = n, t) : e }, t.tickSize = function (e) { var n = arguments.length; return n ? (o = +e, i = +arguments[n - 1], t) : o }, t.innerTickSize = function (e) { return arguments.length ? (o = +e, t) : o }, t.outerTickSize = function (e) { return arguments.length ? (i = +e, t) : i }, t.tickPadding = function (e) { return arguments.length ? (a = +e, t) : a }, t.tickSubdivide = function () { return arguments.length && t }, t
        };
        var Ks = "bottom",
            Xs = { top: 1, right: 1, bottom: 1, left: 1 };
        ha.svg.brush = function () {
            function t(i) {
                i.each(function () {
                    var i = ha.select(this).style("pointer-events", "all").style("-webkit-tap-highlight-color", "rgba(0,0,0,0)").on("mousedown.brush", o).on("touchstart.brush", o),
                        a = i.selectAll(".background").data([0]);
                    a.enter().append("rect").attr("class", "background").style("visibility", "hidden").style("cursor", "crosshair"), i.selectAll(".extent").data([0]).enter().append("rect").attr("class", "extent").style("cursor", "move");
                    var u = i.selectAll(".resize").data(v, C);
                    u.exit().remove(), u.enter().append("g").attr("class", function (t) { return "resize " + t }).style("cursor", function (t) { return Gs[t] }).append("rect").attr("x", function (t) { return /[ew]$/.test(t) ? -3 : null }).attr("y", function (t) { return /^[ns]/.test(t) ? -3 : null }).attr("width", 6).attr("height", 6).style("visibility", "hidden"), u.style("display", t.empty() ? "none" : null);
                    var s, f = ha.transition(i),
                        p = ha.transition(a);
                    c && (s = Go(c), p.attr("x", s[0]).attr("width", s[1] - s[0]), n(f)), l && (s = Go(l), p.attr("y", s[0]).attr("height", s[1] - s[0]), r(f)), e(f)
                })
            }

            function e(t) { t.selectAll(".resize").attr("transform", function (t) { return "translate(" + f[+/e$/.test(t)] + "," + p[+/^s/.test(t)] + ")" }) }

            function n(t) { t.select(".extent").attr("x", f[0]), t.selectAll(".extent,.n>rect,.s>rect").attr("width", f[1] - f[0]) }

            function r(t) { t.select(".extent").attr("y", p[0]), t.selectAll(".extent,.e>rect,.w>rect").attr("height", p[1] - p[0]) }

            function o() {
                function o() { 32 == ha.event.keyCode && (S || (b = null, A[0] -= f[1], A[1] -= p[1], S = 2), N()) }

                function v() { 32 == ha.event.keyCode && 2 == S && (A[0] += f[1], A[1] += p[1], S = 0, N()) }

                function g() {
                    var t = ha.mouse(w),
                        o = !1;
                    x && (t[0] += x[0], t[1] += x[1]), S || (ha.event.altKey ? (b || (b = [(f[0] + f[1]) / 2, (p[0] + p[1]) / 2]), A[0] = f[+(t[0] < b[0])], A[1] = p[+(t[1] < b[1])]) : b = null), k && y(t, c, 0) && (n(E), o = !0), T && y(t, l, 1) && (r(E), o = !0), o && (e(E), C({ type: "brush", mode: S ? "move" : "resize" }))
                }

                function y(t, e, n) {
                    var r, o, a = Go(e),
                        s = a[0],
                        c = a[1],
                        l = A[n],
                        v = n ? p : f,
                        g = v[1] - v[0];
                    if (S && (s -= l, c -= g + l), r = (n ? d : h) ? Math.max(s, Math.min(c, t[n])) : t[n], S ? o = (r += l) + g : (b && (l = Math.max(s, Math.min(c, 2 * b[n] - r))), l < r ? (o = r, r = l) : o = l), v[0] != r || v[1] != o) return n ? u = null : i = null, v[0] = r, v[1] = o, !0
                }

                function m() { g(), E.style("pointer-events", "all").selectAll(".resize").style("display", t.empty() ? "none" : null), ha.select("body").style("cursor", null), P.on("mousemove.brush", null).on("mouseup.brush", null).on("touchmove.brush", null).on("touchend.brush", null).on("keydown.brush", null).on("keyup.brush", null), O(), C({ type: "brushend" }) }
                var b, x, w = this,
                    _ = ha.select(ha.event.target),
                    C = s.of(w, arguments),
                    E = ha.select(w),
                    M = _.datum(),
                    k = !/^(n|s)$/.test(M) && c,
                    T = !/^(e|w)$/.test(M) && l,
                    S = _.classed("extent"),
                    O = tt(w),
                    A = ha.mouse(w),
                    P = ha.select(a(w)).on("keydown.brush", o).on("keyup.brush", v);
                if (ha.event.changedTouches ? P.on("touchmove.brush", g).on("touchend.brush", m) : P.on("mousemove.brush", g).on("mouseup.brush", m), E.interrupt().selectAll("*").interrupt(), S) A[0] = f[0] - A[0], A[1] = p[0] - A[1];
                else if (M) {
                    var j = +/w$/.test(M),
                        D = +/^n/.test(M);
                    x = [f[1 - j] - A[0], p[1 - D] - A[1]], A[0] = f[j], A[1] = p[D]
                } else ha.event.altKey && (b = A.slice());
                E.style("pointer-events", "none").selectAll(".resize").style("display", null), ha.select("body").style("cursor", _.style("cursor")), C({ type: "brushstart" }), g()
            }
            var i, u, s = A(t, "brushstart", "brush", "brushend"),
                c = null,
                l = null,
                f = [0, 0],
                p = [0, 0],
                h = !0,
                d = !0,
                v = Js[0];
            return t.event = function (t) {
                t.each(function () {
                    var t = s.of(this, arguments),
                        e = { x: f, y: p, i: i, j: u },
                        n = this.__chart__ || e;
                    this.__chart__ = e, Ws ? ha.select(this).transition().each("start.brush", function () { i = n.i, u = n.j, f = n.x, p = n.y, t({ type: "brushstart" }) }).tween("brush:brush", function () {
                        var n = Mr(f, e.x),
                            r = Mr(p, e.y);
                        return i = u = null,
                            function (o) { f = e.x = n(o), p = e.y = r(o), t({ type: "brush", mode: "resize" }) }
                    }).each("end.brush", function () { i = e.i, u = e.j, t({ type: "brush", mode: "resize" }), t({ type: "brushend" }) }) : (t({ type: "brushstart" }), t({ type: "brush", mode: "resize" }), t({ type: "brushend" }))
                })
            }, t.x = function (e) { return arguments.length ? (c = e, v = Js[!c << 1 | !l], t) : c }, t.y = function (e) { return arguments.length ? (l = e, v = Js[!c << 1 | !l], t) : l }, t.clamp = function (e) { return arguments.length ? (c && l ? (h = !!e[0], d = !!e[1]) : c ? h = !!e : l && (d = !!e), t) : c && l ? [h, d] : c ? h : l ? d : null }, t.extent = function (e) {
                var n, r, o, a, s;
                return arguments.length ? (c && (n = e[0], r = e[1], l && (n = n[0], r = r[0]), i = [n, r], c.invert && (n = c(n), r = c(r)), r < n && (s = n, n = r, r = s), n == f[0] && r == f[1] || (f = [n, r])), l && (o = e[0], a = e[1], c && (o = o[1], a = a[1]), u = [o, a], l.invert && (o = l(o), a = l(a)), a < o && (s = o, o = a, a = s), o == p[0] && a == p[1] || (p = [o, a])), t) : (c && (i ? (n = i[0], r = i[1]) : (n = f[0], r = f[1], c.invert && (n = c.invert(n), r = c.invert(r)), r < n && (s = n, n = r, r = s))), l && (u ? (o = u[0], a = u[1]) : (o = p[0], a = p[1], l.invert && (o = l.invert(o), a = l.invert(a)), a < o && (s = o, o = a, a = s))), c && l ? [
                    [n, o],
                    [r, a]
                ] : c ? [n, r] : l && [o, a])
            }, t.clear = function () { return t.empty() || (f = [0, 0], p = [0, 0], i = u = null), t }, t.empty = function () { return !!c && f[0] == f[1] || !!l && p[0] == p[1] }, ha.rebind(t, s, "on")
        };
        var Gs = { n: "ns-resize", e: "ew-resize", s: "ns-resize", w: "ew-resize", nw: "nwse-resize", ne: "nesw-resize", se: "nwse-resize", sw: "nesw-resize" },
            Js = [
                ["n", "e", "s", "w", "nw", "ne", "se", "sw"],
                ["e", "w"],
                ["n", "s"],
                []
            ],
            Qs = yu.format = Cu.timeFormat,
            Zs = Qs.utc,
            tc = Zs("%Y-%m-%dT%H:%M:%S.%LZ");
        Qs.iso = Date.prototype.toISOString && +new Date("2000-01-01T00:00:00.000Z") ? sa : tc, sa.parse = function (t) { var e = new Date(t); return isNaN(e) ? null : e }, sa.toString = tc.toString, yu.second = Vt(function (t) { return new mu(1e3 * Math.floor(t / 1e3)) }, function (t, e) { t.setTime(t.getTime() + 1e3 * Math.floor(e)) }, function (t) { return t.getSeconds() }), yu.seconds = yu.second.range, yu.seconds.utc = yu.second.utc.range, yu.minute = Vt(function (t) { return new mu(6e4 * Math.floor(t / 6e4)) }, function (t, e) { t.setTime(t.getTime() + 6e4 * Math.floor(e)) }, function (t) { return t.getMinutes() }), yu.minutes = yu.minute.range, yu.minutes.utc = yu.minute.utc.range, yu.hour = Vt(function (t) { var e = t.getTimezoneOffset() / 60; return new mu(36e5 * (Math.floor(t / 36e5 - e) + e)) }, function (t, e) { t.setTime(t.getTime() + 36e5 * Math.floor(e)) }, function (t) { return t.getHours() }), yu.hours = yu.hour.range, yu.hours.utc = yu.hour.utc.range, yu.month = Vt(function (t) { return t = yu.day(t), t.setDate(1), t }, function (t, e) { t.setMonth(t.getMonth() + e) }, function (t) { return t.getMonth() }), yu.months = yu.month.range, yu.months.utc = yu.month.utc.range;
        var ec = [1e3, 5e3, 15e3, 3e4, 6e4, 3e5, 9e5, 18e5, 36e5, 108e5, 216e5, 432e5, 864e5, 1728e5, 6048e5, 2592e6, 7776e6, 31536e6],
            nc = [
                [yu.second, 1],
                [yu.second, 5],
                [yu.second, 15],
                [yu.second, 30],
                [yu.minute, 1],
                [yu.minute, 5],
                [yu.minute, 15],
                [yu.minute, 30],
                [yu.hour, 1],
                [yu.hour, 3],
                [yu.hour, 6],
                [yu.hour, 12],
                [yu.day, 1],
                [yu.day, 2],
                [yu.week, 1],
                [yu.month, 1],
                [yu.month, 3],
                [yu.year, 1]
            ],
            rc = Qs.multi([
                [".%L", function (t) { return t.getMilliseconds() }],
                [":%S", function (t) { return t.getSeconds() }],
                ["%I:%M", function (t) { return t.getMinutes() }],
                ["%I %p", function (t) { return t.getHours() }],
                ["%a %d", function (t) { return t.getDay() && 1 != t.getDate() }],
                ["%b %d", function (t) { return 1 != t.getDate() }],
                ["%B", function (t) { return t.getMonth() }],
                ["%Y", De]
            ]),
            oc = { range: function (t, e, n) { return ha.range(Math.ceil(t / n) * n, +e, n).map(la) }, floor: C, ceil: C };
        nc.year = yu.year, yu.scale = function () { return ca(ha.scale.linear(), nc, rc) };
        var ic = nc.map(function (t) { return [t[0].utc, t[1]] }),
            ac = Zs.multi([
                [".%L", function (t) { return t.getUTCMilliseconds() }],
                [":%S", function (t) { return t.getUTCSeconds() }],
                ["%I:%M", function (t) { return t.getUTCMinutes() }],
                ["%I %p", function (t) { return t.getUTCHours() }],
                ["%a %d", function (t) { return t.getUTCDay() && 1 != t.getUTCDate() }],
                ["%b %d", function (t) { return 1 != t.getUTCDate() }],
                ["%B", function (t) { return t.getUTCMonth() }],
                ["%Y", De]
            ]);
        ic.year = yu.year.utc, yu.scale.utc = function () { return ca(ha.scale.linear(), ic, ac) }, ha.text = Pt(function (t) { return t.responseText }), ha.json = function (t, e) { return jt(t, "application/json", fa, e) }, ha.html = function (t, e) { return jt(t, "text/html", pa, e) }, ha.xml = Pt(function (t) { return t.responseXML }), this.d3 = ha, r = ha, o = "function" == typeof r ? r.call(e, n, e, t) : r, !(void 0 !== o && (t.exports = o))
    }()
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t) {
        function e(e) {
            e.on({
                "mouseover.tip": function (e) {
                    var r = t.mouse(b),
                        o = r[0],
                        a = r[1],
                        f = u || o + c.left,
                        p = s || a - c.top;
                    m.selectAll("div." + n).remove(), y = m.append("div").attr(l(n)(d)).style(i({ position: "absolute", "z-index": 1001, left: f + "px", top: p + "px" }, g)).html(function () { return v(e) })
                },
                "mousemove.tip": function (e) {
                    var n = t.mouse(b),
                        r = n[0],
                        o = n[1],
                        i = u || r + c.left,
                        a = s || o - c.top;
                    y.style({ left: i + "px", top: a + "px" }).html(function () { return v(e) })
                },
                "mouseout.tip": function () { return y.remove() }
            })
        }
        var n = arguments.length <= 1 || void 0 === arguments[1] ? "tooltip" : arguments[1],
            r = arguments.length <= 2 || void 0 === arguments[2] ? {} : arguments[2],
            o = i({}, p, r),
            u = o.left,
            s = o.top,
            c = o.offset,
            h = o.root,
            d = { class: n },
            v = function () { return "" },
            g = {},
            y = void 0,
            m = h || t.select("body"),
            b = m.node();
        return e.attr = function (t) { return (0, a.is)(Object, t) && (d = i({}, d, t)), this }, e.style = function (t) { return (0, a.is)(Object, t) && (g = i({}, g, t)), this }, e.text = function (t) { return v = f(t), this }, e
    }
    var i = Object.assign || function (t) { for (var e = 1; e < arguments.length; e++) { var n = arguments[e]; for (var r in n) Object.prototype.hasOwnProperty.call(n, r) && (t[r] = n[r]) } return t };
    e.__esModule = !0, e.default = o;
    var a = n(16),
        u = n(129),
        s = r(u),
        c = s.default.default || s.default,
        l = c.prependClass,
        f = c.functor,
        p = { left: void 0, top: void 0, offset: { left: 0, top: 0 }, root: void 0 }
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }
    e.__esModule = !0;
    var o = n(130),
        i = r(o),
        a = n(65),
        u = r(a);
    e.default = { prependClass: i.default, functor: u.default }
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t) { return (0, a.mapObjIndexed)(function (e, n) { if ("class" === n) { var r = function () { var n = (0, s.default)(e); return { v: function (e, r) { var o = n(e, r); return o !== t ? (0, a.join)(" ", [t, o]) : o } } }(); if ("object" === ("undefined" == typeof r ? "undefined" : i(r))) return r.v } return e }) }
    var i = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) { return typeof t } : function (t) { return t && "function" == typeof Symbol && t.constructor === Symbol ? "symbol" : typeof t };
    e.__esModule = !0, e.default = o;
    var a = n(16),
        u = n(65),
        s = r(u)
},
function (t, e, n) {
    var r, o;
    ! function (i, a) { r = a, o = "function" == typeof r ? r.call(e, n, e, t) : r, !(void 0 !== o && (t.exports = o)) }(this, function () {
        return function t(e, n) {
            var r = Array.isArray(n),
                o = r && [] || {};
            return r ? (e = e || [], o = o.concat(e), n.forEach(function (n, r) { "undefined" == typeof o[r] ? o[r] = n : "object" == typeof n ? o[r] = t(e[r], n) : e.indexOf(n) === -1 && o.push(n) })) : (e && "object" == typeof e && Object.keys(e).forEach(function (t) { o[t] = e[t] }), Object.keys(n).forEach(function (r) { "object" == typeof n[r] && n[r] && e[r] ? o[r] = t(e[r], n[r]) : o[r] = n[r] })), o
        }
    })
},
    66, 66, 66,
function (t, e) {
    "use strict";

    function n(t) { return t.replace(r, function (t, e) { return e.toUpperCase() }) }
    var r = /-(.)/g;
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { return o(t.replace(i, "ms-")) }
    var o = n(135),
        i = /^-ms-/;
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return !(!t || !e) && (t === e || !o(t) && (o(e) ? r(t, e.parentNode) : "contains" in t ? t.contains(e) : !!t.compareDocumentPosition && !!(16 & t.compareDocumentPosition(e)))) }
    var o = n(145);
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t) {
        var e = t.length;
        if (Array.isArray(t) || "object" != typeof t && "function" != typeof t ? a(!1) : void 0, "number" != typeof e ? a(!1) : void 0, 0 === e || e - 1 in t ? void 0 : a(!1), "function" == typeof t.callee ? a(!1) : void 0, t.hasOwnProperty) try { return Array.prototype.slice.call(t) } catch (t) { }
        for (var n = Array(e), r = 0; r < e; r++) n[r] = t[r];
        return n
    }

    function o(t) { return !!t && ("object" == typeof t || "function" == typeof t) && "length" in t && !("setInterval" in t) && "number" != typeof t.nodeType && (Array.isArray(t) || "callee" in t || "item" in t) }

    function i(t) { return o(t) ? Array.isArray(t) ? t.slice() : r(t) : [t] }
    var a = n(1);
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t) { var e = t.match(l); return e && e[1].toLowerCase() }

    function o(t, e) {
        var n = c;
        c ? void 0 : s(!1);
        var o = r(t),
            i = o && u(o);
        if (i) { n.innerHTML = i[1] + t + i[2]; for (var l = i[0]; l--;) n = n.lastChild } else n.innerHTML = t;
        var f = n.getElementsByTagName("script");
        f.length && (e ? void 0 : s(!1), a(f).forEach(e));
        for (var p = Array.from(n.childNodes); n.lastChild;) n.removeChild(n.lastChild);
        return p
    }
    var i = n(6),
        a = n(138),
        u = n(140),
        s = n(1),
        c = i.canUseDOM ? document.createElement("div") : null,
        l = /^\s*<(\w+)/;
    t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t) { return a ? void 0 : i(!1), p.hasOwnProperty(t) || (t = "*"), u.hasOwnProperty(t) || ("*" === t ? a.innerHTML = "<link />" : a.innerHTML = "<" + t + "></" + t + ">", u[t] = !a.firstChild), u[t] ? p[t] : null }
    var o = n(6),
        i = n(1),
        a = o.canUseDOM ? document.createElement("div") : null,
        u = {},
        s = [1, '<select multiple="true">', "</select>"],
        c = [1, "<table>", "</table>"],
        l = [3, "<table><tbody><tr>", "</tr></tbody></table>"],
        f = [1, '<svg xmlns="http://www.w3.org/2000/svg">', "</svg>"],
        p = { "*": [1, "?<div>", "</div>"], area: [1, "<map>", "</map>"], col: [2, "<table><tbody></tbody><colgroup>", "</colgroup></table>"], legend: [1, "<fieldset>", "</fieldset>"], param: [1, "<object>", "</object>"], tr: [2, "<table><tbody>", "</tbody></table>"], optgroup: s, option: s, caption: c, colgroup: c, tbody: c, tfoot: c, thead: c, td: l, th: l },
        h = ["circle", "clipPath", "defs", "ellipse", "g", "image", "line", "linearGradient", "mask", "path", "pattern", "polygon", "polyline", "radialGradient", "rect", "stop", "text", "tspan"];
    h.forEach(function (t) { p[t] = f, u[t] = !0 }), t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) { return t === window ? { x: window.pageXOffset || document.documentElement.scrollLeft, y: window.pageYOffset || document.documentElement.scrollTop } : { x: t.scrollLeft, y: t.scrollTop } }
    t.exports = n
},
function (t, e) {
    "use strict";

    function n(t) { return t.replace(r, "-$1").toLowerCase() }
    var r = /([A-Z])/g;
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { return o(t).replace(i, "-ms-") }
    var o = n(142),
        i = /^ms-/;
    t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) { return !(!t || !("function" == typeof Node ? t instanceof Node : "object" == typeof t && "number" == typeof t.nodeType && "string" == typeof t.nodeName)) }
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { return o(t) && 3 == t.nodeType }
    var o = n(144);
    t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) { var e = {}; return function (n) { return e.hasOwnProperty(n) || (e[n] = t.call(this, n)), e[n] } }
    t.exports = n
},
function (t, e, n) {
    var r, o;
    ! function (e, n) { "use strict"; "object" == typeof t && "object" == typeof t.exports ? t.exports = e.document ? n(e, !0) : function (t) { if (!t.document) throw new Error("jQuery requires a window with a document"); return n(t) } : n(e) }("undefined" != typeof window ? window : this, function (n, i) {
        "use strict";

        function a(t, e) {
            e = e || it;
            var n = e.createElement("script");
            n.text = t, e.head.appendChild(n).parentNode.removeChild(n)
        }

        function u(t) {
            var e = !!t && "length" in t && t.length,
                n = mt.type(t);
            return "function" !== n && !mt.isWindow(t) && ("array" === n || 0 === e || "number" == typeof e && e > 0 && e - 1 in t)
        }

        function s(t, e, n) { return mt.isFunction(e) ? mt.grep(t, function (t, r) { return !!e.call(t, r, t) !== n }) : e.nodeType ? mt.grep(t, function (t) { return t === e !== n }) : "string" != typeof e ? mt.grep(t, function (t) { return lt.call(e, t) > -1 !== n }) : St.test(e) ? mt.filter(e, t, n) : (e = mt.filter(e, t), mt.grep(t, function (t) { return lt.call(e, t) > -1 !== n && 1 === t.nodeType })) }

        function c(t, e) {
            for (;
                (t = t[e]) && 1 !== t.nodeType;);
            return t
        }

        function l(t) { var e = {}; return mt.each(t.match(Dt) || [], function (t, n) { e[n] = !0 }), e }

        function f(t) { return t }

        function p(t) { throw t }

        function h(t, e, n) { var r; try { t && mt.isFunction(r = t.promise) ? r.call(t).done(e).fail(n) : t && mt.isFunction(r = t.then) ? r.call(t, e, n) : e.call(void 0, t) } catch (t) { n.call(void 0, t) } }

        function d() { it.removeEventListener("DOMContentLoaded", d), n.removeEventListener("load", d), mt.ready() }

        function v() { this.expando = mt.expando + v.uid++ }

        function g(t) { return "true" === t || "false" !== t && ("null" === t ? null : t === +t + "" ? +t : Ht.test(t) ? JSON.parse(t) : t) }

        function y(t, e, n) {
            var r;
            if (void 0 === n && 1 === t.nodeType)
                if (r = "data-" + e.replace(Bt, "-$&").toLowerCase(), n = t.getAttribute(r), "string" == typeof n) {
                    try { n = g(n) } catch (t) { }
                    qt.set(t, e, n)
                } else n = void 0;
            return n
        }

        function m(t, e, n, r) {
            var o, i = 1,
                a = 20,
                u = r ? function () { return r.cur() } : function () { return mt.css(t, e, "") },
                s = u(),
                c = n && n[3] || (mt.cssNumber[e] ? "" : "px"),
                l = (mt.cssNumber[e] || "px" !== c && +s) && zt.exec(mt.css(t, e));
            if (l && l[3] !== c) {
                c = c || l[3], n = n || [], l = +s || 1;
                do i = i || ".5", l /= i, mt.style(t, e, l + c); while (i !== (i = u() / s) && 1 !== i && --a)
            }
            return n && (l = +l || +s || 0, o = n[1] ? l + (n[1] + 1) * n[2] : +n[2], r && (r.unit = c, r.start = l, r.end = o)), o
        }

        function b(t) {
            var e, n = t.ownerDocument,
                r = t.nodeName,
                o = Kt[r];
            return o ? o : (e = n.body.appendChild(n.createElement(r)), o = mt.css(e, "display"), e.parentNode.removeChild(e), "none" === o && (o = "block"), Kt[r] = o, o)
        }

        function x(t, e) { for (var n, r, o = [], i = 0, a = t.length; i < a; i++) r = t[i], r.style && (n = r.style.display, e ? ("none" === n && (o[i] = Ut.get(r, "display") || null, o[i] || (r.style.display = "")), "" === r.style.display && Yt(r) && (o[i] = b(r))) : "none" !== n && (o[i] = "none", Ut.set(r, "display", n))); for (i = 0; i < a; i++) null != o[i] && (t[i].style.display = o[i]); return t }

        function w(t, e) { var n; return n = "undefined" != typeof t.getElementsByTagName ? t.getElementsByTagName(e || "*") : "undefined" != typeof t.querySelectorAll ? t.querySelectorAll(e || "*") : [], void 0 === e || e && mt.nodeName(t, e) ? mt.merge([t], n) : n }

        function _(t, e) { for (var n = 0, r = t.length; n < r; n++) Ut.set(t[n], "globalEval", !e || Ut.get(e[n], "globalEval")) }

        function C(t, e, n, r, o) {
            for (var i, a, u, s, c, l, f = e.createDocumentFragment(), p = [], h = 0, d = t.length; h < d; h++)
                if (i = t[h], i || 0 === i)
                    if ("object" === mt.type(i)) mt.merge(p, i.nodeType ? [i] : i);
                    else if (Zt.test(i)) {
                        for (a = a || f.appendChild(e.createElement("div")), u = (Gt.exec(i) || ["", ""])[1].toLowerCase(), s = Qt[u] || Qt._default, a.innerHTML = s[1] + mt.htmlPrefilter(i) + s[2], l = s[0]; l--;) a = a.lastChild;
                        mt.merge(p, a.childNodes), a = f.firstChild, a.textContent = ""
                    } else p.push(e.createTextNode(i));
            for (f.textContent = "", h = 0; i = p[h++];)
                if (r && mt.inArray(i, r) > -1) o && o.push(i);
                else if (c = mt.contains(i.ownerDocument, i), a = w(f.appendChild(i), "script"), c && _(a), n)
                    for (l = 0; i = a[l++];) Jt.test(i.type || "") && n.push(i);
            return f
        }

        function E() { return !0 }

        function M() { return !1 }

        function k() { try { return it.activeElement } catch (t) { } }

        function T(t, e, n, r, o, i) {
            var a, u;
            if ("object" == typeof e) { "string" != typeof n && (r = r || n, n = void 0); for (u in e) T(t, u, n, r, e[u], i); return t }
            if (null == r && null == o ? (o = n, r = n = void 0) : null == o && ("string" == typeof n ? (o = r, r = void 0) : (o = r, r = n, n = void 0)), o === !1) o = M;
            else if (!o) return t;
            return 1 === i && (a = o, o = function (t) { return mt().off(t), a.apply(this, arguments) }, o.guid = a.guid || (a.guid = mt.guid++)), t.each(function () { mt.event.add(this, e, o, r, n) })
        }

        function S(t, e) {
            return mt.nodeName(t, "table") && mt.nodeName(11 !== e.nodeType ? e : e.firstChild, "tr") ? t.getElementsByTagName("tbody")[0] || t : t;
        }

        function N(t) { return t.type = (null !== t.getAttribute("type")) + "/" + t.type, t }

        function O(t) { var e = ue.exec(t.type); return e ? t.type = e[1] : t.removeAttribute("type"), t }

        function A(t, e) {
            var n, r, o, i, a, u, s, c;
            if (1 === e.nodeType) {
                if (Ut.hasData(t) && (i = Ut.access(t), a = Ut.set(e, i), c = i.events)) {
                    delete a.handle, a.events = {};
                    for (o in c)
                        for (n = 0, r = c[o].length; n < r; n++) mt.event.add(e, o, c[o][n])
                }
                qt.hasData(t) && (u = qt.access(t), s = mt.extend({}, u), qt.set(e, s))
            }
        }

        function P(t, e) { var n = e.nodeName.toLowerCase(); "input" === n && Xt.test(t.type) ? e.checked = t.checked : "input" !== n && "textarea" !== n || (e.defaultValue = t.defaultValue) }

        function j(t, e, n, r) {
            e = st.apply([], e);
            var o, i, u, s, c, l, f = 0,
                p = t.length,
                h = p - 1,
                d = e[0],
                v = mt.isFunction(d);
            if (v || p > 1 && "string" == typeof d && !gt.checkClone && ae.test(d)) return t.each(function (o) {
                var i = t.eq(o);
                v && (e[0] = d.call(this, o, i.html())), j(i, e, n, r)
            });
            if (p && (o = C(e, t[0].ownerDocument, !1, t, r), i = o.firstChild, 1 === o.childNodes.length && (o = i), i || r)) {
                for (u = mt.map(w(o, "script"), N), s = u.length; f < p; f++) c = o, f !== h && (c = mt.clone(c, !0, !0), s && mt.merge(u, w(c, "script"))), n.call(t[f], c, f);
                if (s)
                    for (l = u[u.length - 1].ownerDocument, mt.map(u, O), f = 0; f < s; f++) c = u[f], Jt.test(c.type || "") && !Ut.access(c, "globalEval") && mt.contains(l, c) && (c.src ? mt._evalUrl && mt._evalUrl(c.src) : a(c.textContent.replace(se, ""), l))
            }
            return t
        }

        function D(t, e, n) { for (var r, o = e ? mt.filter(e, t) : t, i = 0; null != (r = o[i]); i++) n || 1 !== r.nodeType || mt.cleanData(w(r)), r.parentNode && (n && mt.contains(r.ownerDocument, r) && _(w(r, "script")), r.parentNode.removeChild(r)); return t }

        function I(t, e, n) { var r, o, i, a, u = t.style; return n = n || fe(t), n && (a = n.getPropertyValue(e) || n[e], "" !== a || mt.contains(t.ownerDocument, t) || (a = mt.style(t, e)), !gt.pixelMarginRight() && le.test(a) && ce.test(e) && (r = u.width, o = u.minWidth, i = u.maxWidth, u.minWidth = u.maxWidth = u.width = a, a = n.width, u.width = r, u.minWidth = o, u.maxWidth = i)), void 0 !== a ? a + "" : a }

        function R(t, e) { return { get: function () { return t() ? void delete this.get : (this.get = e).apply(this, arguments) } } }

        function L(t) {
            if (t in ge) return t;
            for (var e = t[0].toUpperCase() + t.slice(1), n = ve.length; n--;)
                if (t = ve[n] + e, t in ge) return t
        }

        function F(t, e, n) { var r = zt.exec(e); return r ? Math.max(0, r[2] - (n || 0)) + (r[3] || "px") : e }

        function U(t, e, n, r, o) { var i, a = 0; for (i = n === (r ? "border" : "content") ? 4 : "width" === e ? 1 : 0; i < 4; i += 2) "margin" === n && (a += mt.css(t, n + Vt[i], !0, o)), r ? ("content" === n && (a -= mt.css(t, "padding" + Vt[i], !0, o)), "margin" !== n && (a -= mt.css(t, "border" + Vt[i] + "Width", !0, o))) : (a += mt.css(t, "padding" + Vt[i], !0, o), "padding" !== n && (a += mt.css(t, "border" + Vt[i] + "Width", !0, o))); return a }

        function q(t, e, n) {
            var r, o = !0,
                i = fe(t),
                a = "border-box" === mt.css(t, "boxSizing", !1, i);
            if (t.getClientRects().length && (r = t.getBoundingClientRect()[e]), r <= 0 || null == r) {
                if (r = I(t, e, i), (r < 0 || null == r) && (r = t.style[e]), le.test(r)) return r;
                o = a && (gt.boxSizingReliable() || r === t.style[e]), r = parseFloat(r) || 0
            }
            return r + U(t, e, n || (a ? "border" : "content"), o, i) + "px"
        }

        function H(t, e, n, r, o) { return new H.prototype.init(t, e, n, r, o) }

        function B() { me && (n.requestAnimationFrame(B), mt.fx.tick()) }

        function W() { return n.setTimeout(function () { ye = void 0 }), ye = mt.now() }

        function z(t, e) {
            var n, r = 0,
                o = { height: t };
            for (e = e ? 1 : 0; r < 4; r += 2 - e) n = Vt[r], o["margin" + n] = o["padding" + n] = t;
            return e && (o.opacity = o.width = t), o
        }

        function V(t, e, n) {
            for (var r, o = (K.tweeners[e] || []).concat(K.tweeners["*"]), i = 0, a = o.length; i < a; i++)
                if (r = o[i].call(n, e, t)) return r
        }

        function Y(t, e, n) {
            var r, o, i, a, u, s, c, l, f = "width" in e || "height" in e,
                p = this,
                h = {},
                d = t.style,
                v = t.nodeType && Yt(t),
                g = Ut.get(t, "fxshow");
            n.queue || (a = mt._queueHooks(t, "fx"), null == a.unqueued && (a.unqueued = 0, u = a.empty.fire, a.empty.fire = function () { a.unqueued || u() }), a.unqueued++ , p.always(function () { p.always(function () { a.unqueued-- , mt.queue(t, "fx").length || a.empty.fire() }) }));
            for (r in e)
                if (o = e[r], be.test(o)) {
                    if (delete e[r], i = i || "toggle" === o, o === (v ? "hide" : "show")) {
                        if ("show" !== o || !g || void 0 === g[r]) continue;
                        v = !0
                    }
                    h[r] = g && g[r] || mt.style(t, r)
                }
            if (s = !mt.isEmptyObject(e), s || !mt.isEmptyObject(h)) { f && 1 === t.nodeType && (n.overflow = [d.overflow, d.overflowX, d.overflowY], c = g && g.display, null == c && (c = Ut.get(t, "display")), l = mt.css(t, "display"), "none" === l && (c ? l = c : (x([t], !0), c = t.style.display || c, l = mt.css(t, "display"), x([t]))), ("inline" === l || "inline-block" === l && null != c) && "none" === mt.css(t, "float") && (s || (p.done(function () { d.display = c }), null == c && (l = d.display, c = "none" === l ? "" : l)), d.display = "inline-block")), n.overflow && (d.overflow = "hidden", p.always(function () { d.overflow = n.overflow[0], d.overflowX = n.overflow[1], d.overflowY = n.overflow[2] })), s = !1; for (r in h) s || (g ? "hidden" in g && (v = g.hidden) : g = Ut.access(t, "fxshow", { display: c }), i && (g.hidden = !v), v && x([t], !0), p.done(function () { v || x([t]), Ut.remove(t, "fxshow"); for (r in h) mt.style(t, r, h[r]) })), s = V(v ? g[r] : 0, r, p), r in g || (g[r] = s.start, v && (s.end = s.start, s.start = 0)) }
        }

        function $(t, e) {
            var n, r, o, i, a;
            for (n in t)
                if (r = mt.camelCase(n), o = e[r], i = t[n], mt.isArray(i) && (o = i[1], i = t[n] = i[0]), n !== r && (t[r] = i, delete t[n]), a = mt.cssHooks[r], a && "expand" in a) { i = a.expand(i), delete t[r]; for (n in i) n in t || (t[n] = i[n], e[n] = o) } else e[r] = o
        }

        function K(t, e, n) {
            var r, o, i = 0,
                a = K.prefilters.length,
                u = mt.Deferred().always(function () { delete s.elem }),
                s = function () { if (o) return !1; for (var e = ye || W(), n = Math.max(0, c.startTime + c.duration - e), r = n / c.duration || 0, i = 1 - r, a = 0, s = c.tweens.length; a < s; a++) c.tweens[a].run(i); return u.notifyWith(t, [c, i, n]), i < 1 && s ? n : (u.resolveWith(t, [c]), !1) },
                c = u.promise({
                    elem: t,
                    props: mt.extend({}, e),
                    opts: mt.extend(!0, { specialEasing: {}, easing: mt.easing._default }, n),
                    originalProperties: e,
                    originalOptions: n,
                    startTime: ye || W(),
                    duration: n.duration,
                    tweens: [],
                    createTween: function (e, n) { var r = mt.Tween(t, c.opts, e, n, c.opts.specialEasing[e] || c.opts.easing); return c.tweens.push(r), r },
                    stop: function (e) {
                        var n = 0,
                            r = e ? c.tweens.length : 0;
                        if (o) return this;
                        for (o = !0; n < r; n++) c.tweens[n].run(1);
                        return e ? (u.notifyWith(t, [c, 1, 0]), u.resolveWith(t, [c, e])) : u.rejectWith(t, [c, e]), this
                    }
                }),
                l = c.props;
            for ($(l, c.opts.specialEasing); i < a; i++)
                if (r = K.prefilters[i].call(c, t, l, c.opts)) return mt.isFunction(r.stop) && (mt._queueHooks(c.elem, c.opts.queue).stop = mt.proxy(r.stop, r)), r;
            return mt.map(l, V, c), mt.isFunction(c.opts.start) && c.opts.start.call(t, c), mt.fx.timer(mt.extend(s, { elem: t, anim: c, queue: c.opts.queue })), c.progress(c.opts.progress).done(c.opts.done, c.opts.complete).fail(c.opts.fail).always(c.opts.always)
        }

        function X(t) { var e = t.match(Dt) || []; return e.join(" ") }

        function G(t) { return t.getAttribute && t.getAttribute("class") || "" }

        function J(t, e, n, r) {
            var o;
            if (mt.isArray(e)) mt.each(e, function (e, o) { n || Oe.test(t) ? r(t, o) : J(t + "[" + ("object" == typeof o && null != o ? e : "") + "]", o, n, r) });
            else if (n || "object" !== mt.type(e)) r(t, e);
            else
                for (o in e) J(t + "[" + o + "]", e[o], n, r)
        }

        function Q(t) {
            return function (e, n) {
                "string" != typeof e && (n = e, e = "*");
                var r, o = 0,
                    i = e.toLowerCase().match(Dt) || [];
                if (mt.isFunction(n))
                    for (; r = i[o++];) "+" === r[0] ? (r = r.slice(1) || "*", (t[r] = t[r] || []).unshift(n)) : (t[r] = t[r] || []).push(n)
            }
        }

        function Z(t, e, n, r) {
            function o(u) { var s; return i[u] = !0, mt.each(t[u] || [], function (t, u) { var c = u(e, n, r); return "string" != typeof c || a || i[c] ? a ? !(s = c) : void 0 : (e.dataTypes.unshift(c), o(c), !1) }), s }
            var i = {},
                a = t === Be;
            return o(e.dataTypes[0]) || !i["*"] && o("*")
        }

        function tt(t, e) { var n, r, o = mt.ajaxSettings.flatOptions || {}; for (n in e) void 0 !== e[n] && ((o[n] ? t : r || (r = {}))[n] = e[n]); return r && mt.extend(!0, t, r), t }

        function et(t, e, n) {
            for (var r, o, i, a, u = t.contents, s = t.dataTypes;
                "*" === s[0];) s.shift(), void 0 === r && (r = t.mimeType || e.getResponseHeader("Content-Type"));
            if (r)
                for (o in u)
                    if (u[o] && u[o].test(r)) { s.unshift(o); break }
            if (s[0] in n) i = s[0];
            else {
                for (o in n) {
                    if (!s[0] || t.converters[o + " " + s[0]]) { i = o; break }
                    a || (a = o)
                }
                i = i || a
            }
            if (i) return i !== s[0] && s.unshift(i), n[i]
        }

        function nt(t, e, n, r) {
            var o, i, a, u, s, c = {},
                l = t.dataTypes.slice();
            if (l[1])
                for (a in t.converters) c[a.toLowerCase()] = t.converters[a];
            for (i = l.shift(); i;)
                if (t.responseFields[i] && (n[t.responseFields[i]] = e), !s && r && t.dataFilter && (e = t.dataFilter(e, t.dataType)), s = i, i = l.shift())
                    if ("*" === i) i = s;
                    else if ("*" !== s && s !== i) {
                        if (a = c[s + " " + i] || c["* " + i], !a)
                            for (o in c)
                                if (u = o.split(" "), u[1] === i && (a = c[s + " " + u[0]] || c["* " + u[0]])) { a === !0 ? a = c[o] : c[o] !== !0 && (i = u[0], l.unshift(u[1])); break }
                        if (a !== !0)
                            if (a && t.throws) e = a(e);
                            else try { e = a(e) } catch (t) { return { state: "parsererror", error: a ? t : "No conversion from " + s + " to " + i } }
                    }
            return { state: "success", data: e }
        }

        function rt(t) { return mt.isWindow(t) ? t : 9 === t.nodeType && t.defaultView }
        var ot = [],
            it = n.document,
            at = Object.getPrototypeOf,
            ut = ot.slice,
            st = ot.concat,
            ct = ot.push,
            lt = ot.indexOf,
            ft = {},
            pt = ft.toString,
            ht = ft.hasOwnProperty,
            dt = ht.toString,
            vt = dt.call(Object),
            gt = {},
            yt = "3.1.1",
            mt = function (t, e) { return new mt.fn.init(t, e) },
            bt = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,
            xt = /^-ms-/,
            wt = /-([a-z])/g,
            _t = function (t, e) { return e.toUpperCase() };
        mt.fn = mt.prototype = {
            jquery: yt,
            constructor: mt,
            length: 0,
            toArray: function () { return ut.call(this) },
            get: function (t) { return null == t ? ut.call(this) : t < 0 ? this[t + this.length] : this[t] },
            pushStack: function (t) { var e = mt.merge(this.constructor(), t); return e.prevObject = this, e },
            each: function (t) { return mt.each(this, t) },
            map: function (t) { return this.pushStack(mt.map(this, function (e, n) { return t.call(e, n, e) })) },
            slice: function () { return this.pushStack(ut.apply(this, arguments)) },
            first: function () { return this.eq(0) },
            last: function () { return this.eq(-1) },
            eq: function (t) {
                var e = this.length,
                    n = +t + (t < 0 ? e : 0);
                return this.pushStack(n >= 0 && n < e ? [this[n]] : [])
            },
            end: function () { return this.prevObject || this.constructor() },
            push: ct,
            sort: ot.sort,
            splice: ot.splice
        }, mt.extend = mt.fn.extend = function () {
            var t, e, n, r, o, i, a = arguments[0] || {},
                u = 1,
                s = arguments.length,
                c = !1;
            for ("boolean" == typeof a && (c = a, a = arguments[u] || {}, u++), "object" == typeof a || mt.isFunction(a) || (a = {}), u === s && (a = this, u--); u < s; u++)
                if (null != (t = arguments[u]))
                    for (e in t) n = a[e], r = t[e], a !== r && (c && r && (mt.isPlainObject(r) || (o = mt.isArray(r))) ? (o ? (o = !1, i = n && mt.isArray(n) ? n : []) : i = n && mt.isPlainObject(n) ? n : {}, a[e] = mt.extend(c, i, r)) : void 0 !== r && (a[e] = r));
            return a
        }, mt.extend({
            expando: "jQuery" + (yt + Math.random()).replace(/\D/g, ""),
            isReady: !0,
            error: function (t) { throw new Error(t) },
            noop: function () { },
            isFunction: function (t) { return "function" === mt.type(t) },
            isArray: Array.isArray,
            isWindow: function (t) { return null != t && t === t.window },
            isNumeric: function (t) { var e = mt.type(t); return ("number" === e || "string" === e) && !isNaN(t - parseFloat(t)) },
            isPlainObject: function (t) { var e, n; return !(!t || "[object Object]" !== pt.call(t)) && (!(e = at(t)) || (n = ht.call(e, "constructor") && e.constructor, "function" == typeof n && dt.call(n) === vt)) },
            isEmptyObject: function (t) { var e; for (e in t) return !1; return !0 },
            type: function (t) { return null == t ? t + "" : "object" == typeof t || "function" == typeof t ? ft[pt.call(t)] || "object" : typeof t },
            globalEval: function (t) { a(t) },
            camelCase: function (t) { return t.replace(xt, "ms-").replace(wt, _t) },
            nodeName: function (t, e) { return t.nodeName && t.nodeName.toLowerCase() === e.toLowerCase() },
            each: function (t, e) {
                var n, r = 0;
                if (u(t))
                    for (n = t.length; r < n && e.call(t[r], r, t[r]) !== !1; r++);
                else
                    for (r in t)
                        if (e.call(t[r], r, t[r]) === !1) break; return t
            },
            trim: function (t) { return null == t ? "" : (t + "").replace(bt, "") },
            makeArray: function (t, e) { var n = e || []; return null != t && (u(Object(t)) ? mt.merge(n, "string" == typeof t ? [t] : t) : ct.call(n, t)), n },
            inArray: function (t, e, n) { return null == e ? -1 : lt.call(e, t, n) },
            merge: function (t, e) { for (var n = +e.length, r = 0, o = t.length; r < n; r++) t[o++] = e[r]; return t.length = o, t },
            grep: function (t, e, n) { for (var r, o = [], i = 0, a = t.length, u = !n; i < a; i++) r = !e(t[i], i), r !== u && o.push(t[i]); return o },
            map: function (t, e, n) {
                var r, o, i = 0,
                    a = [];
                if (u(t))
                    for (r = t.length; i < r; i++) o = e(t[i], i, n), null != o && a.push(o);
                else
                    for (i in t) o = e(t[i], i, n), null != o && a.push(o);
                return st.apply([], a)
            },
            guid: 1,
            proxy: function (t, e) { var n, r, o; if ("string" == typeof e && (n = t[e], e = t, t = n), mt.isFunction(t)) return r = ut.call(arguments, 2), o = function () { return t.apply(e || this, r.concat(ut.call(arguments))) }, o.guid = t.guid = t.guid || mt.guid++ , o },
            now: Date.now,
            support: gt
        }), "function" == typeof Symbol && (mt.fn[Symbol.iterator] = ot[Symbol.iterator]), mt.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "), function (t, e) { ft["[object " + e + "]"] = e.toLowerCase() });
        var Ct = function (t) {
            function e(t, e, n, r) {
                var o, i, a, u, s, c, l, p = e && e.ownerDocument,
                    d = e ? e.nodeType : 9;
                if (n = n || [], "string" != typeof t || !t || 1 !== d && 9 !== d && 11 !== d) return n;
                if (!r && ((e ? e.ownerDocument || e : H) !== j && P(e), e = e || j, I)) {
                    if (11 !== d && (s = yt.exec(t)))
                        if (o = s[1]) { if (9 === d) { if (!(a = e.getElementById(o))) return n; if (a.id === o) return n.push(a), n } else if (p && (a = p.getElementById(o)) && U(e, a) && a.id === o) return n.push(a), n } else { if (s[2]) return Q.apply(n, e.getElementsByTagName(t)), n; if ((o = s[3]) && _.getElementsByClassName && e.getElementsByClassName) return Q.apply(n, e.getElementsByClassName(o)), n }
                    if (_.qsa && !Y[t + " "] && (!R || !R.test(t))) {
                        if (1 !== d) p = e, l = t;
                        else if ("object" !== e.nodeName.toLowerCase()) {
                            for ((u = e.getAttribute("id")) ? u = u.replace(wt, _t) : e.setAttribute("id", u = q), c = k(t), i = c.length; i--;) c[i] = "#" + u + " " + h(c[i]);
                            l = c.join(","), p = mt.test(t) && f(e.parentNode) || e
                        }
                        if (l) try { return Q.apply(n, p.querySelectorAll(l)), n } catch (t) { } finally { u === q && e.removeAttribute("id") }
                    }
                }
                return S(t.replace(ut, "$1"), e, n, r)
            }

            function n() {
                function t(n, r) { return e.push(n + " ") > C.cacheLength && delete t[e.shift()], t[n + " "] = r }
                var e = [];
                return t
            }

            function r(t) { return t[q] = !0, t }

            function o(t) { var e = j.createElement("fieldset"); try { return !!t(e) } catch (t) { return !1 } finally { e.parentNode && e.parentNode.removeChild(e), e = null } }

            function i(t, e) { for (var n = t.split("|"), r = n.length; r--;) C.attrHandle[n[r]] = e }

            function a(t, e) {
                var n = e && t,
                    r = n && 1 === t.nodeType && 1 === e.nodeType && t.sourceIndex - e.sourceIndex;
                if (r) return r;
                if (n)
                    for (; n = n.nextSibling;)
                        if (n === e) return -1;
                return t ? 1 : -1
            }

            function u(t) { return function (e) { var n = e.nodeName.toLowerCase(); return "input" === n && e.type === t } }

            function s(t) { return function (e) { var n = e.nodeName.toLowerCase(); return ("input" === n || "button" === n) && e.type === t } }

            function c(t) { return function (e) { return "form" in e ? e.parentNode && e.disabled === !1 ? "label" in e ? "label" in e.parentNode ? e.parentNode.disabled === t : e.disabled === t : e.isDisabled === t || e.isDisabled !== !t && Et(e) === t : e.disabled === t : "label" in e && e.disabled === t } }

            function l(t) { return r(function (e) { return e = +e, r(function (n, r) { for (var o, i = t([], n.length, e), a = i.length; a--;) n[o = i[a]] && (n[o] = !(r[o] = n[o])) }) }) }

            function f(t) { return t && "undefined" != typeof t.getElementsByTagName && t }

            function p() { }

            function h(t) { for (var e = 0, n = t.length, r = ""; e < n; e++) r += t[e].value; return r }

            function d(t, e, n) {
                var r = e.dir,
                    o = e.next,
                    i = o || r,
                    a = n && "parentNode" === i,
                    u = W++;
                return e.first ? function (e, n, o) {
                    for (; e = e[r];)
                        if (1 === e.nodeType || a) return t(e, n, o);
                    return !1
                } : function (e, n, s) {
                    var c, l, f, p = [B, u];
                    if (s) {
                        for (; e = e[r];)
                            if ((1 === e.nodeType || a) && t(e, n, s)) return !0
                    } else
                        for (; e = e[r];)
                            if (1 === e.nodeType || a)
                                if (f = e[q] || (e[q] = {}), l = f[e.uniqueID] || (f[e.uniqueID] = {}), o && o === e.nodeName.toLowerCase()) e = e[r] || e;
                                else { if ((c = l[i]) && c[0] === B && c[1] === u) return p[2] = c[2]; if (l[i] = p, p[2] = t(e, n, s)) return !0 } return !1
                }
            }

            function v(t) {
                return t.length > 1 ? function (e, n, r) {
                    for (var o = t.length; o--;)
                        if (!t[o](e, n, r)) return !1;
                    return !0
                } : t[0]
            }

            function g(t, n, r) { for (var o = 0, i = n.length; o < i; o++) e(t, n[o], r); return r }

            function y(t, e, n, r, o) { for (var i, a = [], u = 0, s = t.length, c = null != e; u < s; u++)(i = t[u]) && (n && !n(i, r, o) || (a.push(i), c && e.push(u))); return a }

            function m(t, e, n, o, i, a) {
                return o && !o[q] && (o = m(o)), i && !i[q] && (i = m(i, a)), r(function (r, a, u, s) {
                    var c, l, f, p = [],
                        h = [],
                        d = a.length,
                        v = r || g(e || "*", u.nodeType ? [u] : u, []),
                        m = !t || !r && e ? v : y(v, p, t, u, s),
                        b = n ? i || (r ? t : d || o) ? [] : a : m;
                    if (n && n(m, b, u, s), o)
                        for (c = y(b, h), o(c, [], u, s), l = c.length; l--;)(f = c[l]) && (b[h[l]] = !(m[h[l]] = f));
                    if (r) {
                        if (i || t) {
                            if (i) {
                                for (c = [], l = b.length; l--;)(f = b[l]) && c.push(m[l] = f);
                                i(null, b = [], c, s)
                            }
                            for (l = b.length; l--;)(f = b[l]) && (c = i ? tt(r, f) : p[l]) > -1 && (r[c] = !(a[c] = f))
                        }
                    } else b = y(b === a ? b.splice(d, b.length) : b), i ? i(null, a, b, s) : Q.apply(a, b)
                })
            }

            function b(t) {
                for (var e, n, r, o = t.length, i = C.relative[t[0].type], a = i || C.relative[" "], u = i ? 1 : 0, s = d(function (t) { return t === e }, a, !0), c = d(function (t) { return tt(e, t) > -1 }, a, !0), l = [function (t, n, r) { var o = !i && (r || n !== N) || ((e = n).nodeType ? s(t, n, r) : c(t, n, r)); return e = null, o }]; u < o; u++)
                    if (n = C.relative[t[u].type]) l = [d(v(l), n)];
                    else {
                        if (n = C.filter[t[u].type].apply(null, t[u].matches), n[q]) { for (r = ++u; r < o && !C.relative[t[r].type]; r++); return m(u > 1 && v(l), u > 1 && h(t.slice(0, u - 1).concat({ value: " " === t[u - 2].type ? "*" : "" })).replace(ut, "$1"), n, u < r && b(t.slice(u, r)), r < o && b(t = t.slice(r)), r < o && h(t)) }
                        l.push(n)
                    }
                return v(l)
            }

            function x(t, n) {
                var o = n.length > 0,
                    i = t.length > 0,
                    a = function (r, a, u, s, c) {
                        var l, f, p, h = 0,
                            d = "0",
                            v = r && [],
                            g = [],
                            m = N,
                            b = r || i && C.find.TAG("*", c),
                            x = B += null == m ? 1 : Math.random() || .1,
                            w = b.length;
                        for (c && (N = a === j || a || c); d !== w && null != (l = b[d]); d++) {
                            if (i && l) {
                                for (f = 0, a || l.ownerDocument === j || (P(l), u = !I); p = t[f++];)
                                    if (p(l, a || j, u)) { s.push(l); break }
                                c && (B = x)
                            }
                            o && ((l = !p && l) && h-- , r && v.push(l))
                        }
                        if (h += d, o && d !== h) {
                            for (f = 0; p = n[f++];) p(v, g, a, u);
                            if (r) {
                                if (h > 0)
                                    for (; d--;) v[d] || g[d] || (g[d] = G.call(s));
                                g = y(g)
                            }
                            Q.apply(s, g), c && !r && g.length > 0 && h + n.length > 1 && e.uniqueSort(s)
                        }
                        return c && (B = x, N = m), v
                    };
                return o ? r(a) : a
            }
            var w, _, C, E, M, k, T, S, N, O, A, P, j, D, I, R, L, F, U, q = "sizzle" + 1 * new Date,
                H = t.document,
                B = 0,
                W = 0,
                z = n(),
                V = n(),
                Y = n(),
                $ = function (t, e) { return t === e && (A = !0), 0 },
                K = {}.hasOwnProperty,
                X = [],
                G = X.pop,
                J = X.push,
                Q = X.push,
                Z = X.slice,
                tt = function (t, e) {
                    for (var n = 0, r = t.length; n < r; n++)
                        if (t[n] === e) return n;
                    return -1
                },
                et = "checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",
                nt = "[\\x20\\t\\r\\n\\f]",
                rt = "(?:\\\\.|[\\w-]|[^\0-\\xa0])+",
                ot = "\\[" + nt + "*(" + rt + ")(?:" + nt + "*([*^$|!~]?=)" + nt + "*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|(" + rt + "))|)" + nt + "*\\]",
                it = ":(" + rt + ")(?:\\((('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|((?:\\\\.|[^\\\\()[\\]]|" + ot + ")*)|.*)\\)|)",
                at = new RegExp(nt + "+", "g"),
                ut = new RegExp("^" + nt + "+|((?:^|[^\\\\])(?:\\\\.)*)" + nt + "+$", "g"),
                st = new RegExp("^" + nt + "*," + nt + "*"),
                ct = new RegExp("^" + nt + "*([>+~]|" + nt + ")" + nt + "*"),
                lt = new RegExp("=" + nt + "*([^\\]'\"]*?)" + nt + "*\\]", "g"),
                ft = new RegExp(it),
                pt = new RegExp("^" + rt + "$"),
                ht = { ID: new RegExp("^#(" + rt + ")"), CLASS: new RegExp("^\\.(" + rt + ")"), TAG: new RegExp("^(" + rt + "|[*])"), ATTR: new RegExp("^" + ot), PSEUDO: new RegExp("^" + it), CHILD: new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\(" + nt + "*(even|odd|(([+-]|)(\\d*)n|)" + nt + "*(?:([+-]|)" + nt + "*(\\d+)|))" + nt + "*\\)|)", "i"), bool: new RegExp("^(?:" + et + ")$", "i"), needsContext: new RegExp("^" + nt + "*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\(" + nt + "*((?:-\\d)?\\d*)" + nt + "*\\)|)(?=[^-]|$)", "i") },
                dt = /^(?:input|select|textarea|button)$/i,
                vt = /^h\d$/i,
                gt = /^[^{]+\{\s*\[native \w/,
                yt = /^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,
                mt = /[+~]/,
                bt = new RegExp("\\\\([\\da-f]{1,6}" + nt + "?|(" + nt + ")|.)", "ig"),
                xt = function (t, e, n) { var r = "0x" + e - 65536; return r !== r || n ? e : r < 0 ? String.fromCharCode(r + 65536) : String.fromCharCode(r >> 10 | 55296, 1023 & r | 56320) },
                wt = /([\0-\x1f\x7f]|^-?\d)|^-$|[^\0-\x1f\x7f-\uFFFF\w-]/g,
                _t = function (t, e) { return e ? "\0" === t ? "�" : t.slice(0, -1) + "\\" + t.charCodeAt(t.length - 1).toString(16) + " " : "\\" + t },
                Ct = function () { P() },
                Et = d(function (t) { return t.disabled === !0 && ("form" in t || "label" in t) }, { dir: "parentNode", next: "legend" });
            try { Q.apply(X = Z.call(H.childNodes), H.childNodes), X[H.childNodes.length].nodeType } catch (t) {
                Q = {
                    apply: X.length ? function (t, e) { J.apply(t, Z.call(e)) } : function (t, e) {
                        for (var n = t.length, r = 0; t[n++] = e[r++];);
                        t.length = n - 1
                    }
                }
            }
            _ = e.support = {}, M = e.isXML = function (t) { var e = t && (t.ownerDocument || t).documentElement; return !!e && "HTML" !== e.nodeName }, P = e.setDocument = function (t) {
                var e, n, r = t ? t.ownerDocument || t : H;
                return r !== j && 9 === r.nodeType && r.documentElement ? (j = r, D = j.documentElement, I = !M(j), H !== j && (n = j.defaultView) && n.top !== n && (n.addEventListener ? n.addEventListener("unload", Ct, !1) : n.attachEvent && n.attachEvent("onunload", Ct)), _.attributes = o(function (t) { return t.className = "i", !t.getAttribute("className") }), _.getElementsByTagName = o(function (t) { return t.appendChild(j.createComment("")), !t.getElementsByTagName("*").length }), _.getElementsByClassName = gt.test(j.getElementsByClassName), _.getById = o(function (t) { return D.appendChild(t).id = q, !j.getElementsByName || !j.getElementsByName(q).length }), _.getById ? (C.filter.ID = function (t) { var e = t.replace(bt, xt); return function (t) { return t.getAttribute("id") === e } }, C.find.ID = function (t, e) { if ("undefined" != typeof e.getElementById && I) { var n = e.getElementById(t); return n ? [n] : [] } }) : (C.filter.ID = function (t) { var e = t.replace(bt, xt); return function (t) { var n = "undefined" != typeof t.getAttributeNode && t.getAttributeNode("id"); return n && n.value === e } }, C.find.ID = function (t, e) {
                    if ("undefined" != typeof e.getElementById && I) {
                        var n, r, o, i = e.getElementById(t);
                        if (i) {
                            if (n = i.getAttributeNode("id"), n && n.value === t) return [i];
                            for (o = e.getElementsByName(t), r = 0; i = o[r++];)
                                if (n = i.getAttributeNode("id"), n && n.value === t) return [i]
                        }
                        return []
                    }
                }), C.find.TAG = _.getElementsByTagName ? function (t, e) { return "undefined" != typeof e.getElementsByTagName ? e.getElementsByTagName(t) : _.qsa ? e.querySelectorAll(t) : void 0 } : function (t, e) {
                    var n, r = [],
                        o = 0,
                        i = e.getElementsByTagName(t);
                    if ("*" === t) { for (; n = i[o++];) 1 === n.nodeType && r.push(n); return r }
                    return i
                }, C.find.CLASS = _.getElementsByClassName && function (t, e) { if ("undefined" != typeof e.getElementsByClassName && I) return e.getElementsByClassName(t) }, L = [], R = [], (_.qsa = gt.test(j.querySelectorAll)) && (o(function (t) { D.appendChild(t).innerHTML = "<a id='" + q + "'></a><select id='" + q + "-\r\\' msallowcapture=''><option selected=''></option></select>", t.querySelectorAll("[msallowcapture^='']").length && R.push("[*^$]=" + nt + "*(?:''|\"\")"), t.querySelectorAll("[selected]").length || R.push("\\[" + nt + "*(?:value|" + et + ")"), t.querySelectorAll("[id~=" + q + "-]").length || R.push("~="), t.querySelectorAll(":checked").length || R.push(":checked"), t.querySelectorAll("a#" + q + "+*").length || R.push(".#.+[+~]") }), o(function (t) {
                    t.innerHTML = "<a href='' disabled='disabled'></a><select disabled='disabled'><option/></select>";
                    var e = j.createElement("input");
                    e.setAttribute("type", "hidden"), t.appendChild(e).setAttribute("name", "D"), t.querySelectorAll("[name=d]").length && R.push("name" + nt + "*[*^$|!~]?="), 2 !== t.querySelectorAll(":enabled").length && R.push(":enabled", ":disabled"), D.appendChild(t).disabled = !0, 2 !== t.querySelectorAll(":disabled").length && R.push(":enabled", ":disabled"), t.querySelectorAll("*,:x"), R.push(",.*:")
                })), (_.matchesSelector = gt.test(F = D.matches || D.webkitMatchesSelector || D.mozMatchesSelector || D.oMatchesSelector || D.msMatchesSelector)) && o(function (t) { _.disconnectedMatch = F.call(t, "*"), F.call(t, "[s!='']:x"), L.push("!=", it) }), R = R.length && new RegExp(R.join("|")), L = L.length && new RegExp(L.join("|")), e = gt.test(D.compareDocumentPosition), U = e || gt.test(D.contains) ? function (t, e) {
                    var n = 9 === t.nodeType ? t.documentElement : t,
                        r = e && e.parentNode;
                    return t === r || !(!r || 1 !== r.nodeType || !(n.contains ? n.contains(r) : t.compareDocumentPosition && 16 & t.compareDocumentPosition(r)))
                } : function (t, e) {
                    if (e)
                        for (; e = e.parentNode;)
                            if (e === t) return !0;
                    return !1
                }, $ = e ? function (t, e) { if (t === e) return A = !0, 0; var n = !t.compareDocumentPosition - !e.compareDocumentPosition; return n ? n : (n = (t.ownerDocument || t) === (e.ownerDocument || e) ? t.compareDocumentPosition(e) : 1, 1 & n || !_.sortDetached && e.compareDocumentPosition(t) === n ? t === j || t.ownerDocument === H && U(H, t) ? -1 : e === j || e.ownerDocument === H && U(H, e) ? 1 : O ? tt(O, t) - tt(O, e) : 0 : 4 & n ? -1 : 1) } : function (t, e) {
                    if (t === e) return A = !0, 0;
                    var n, r = 0,
                        o = t.parentNode,
                        i = e.parentNode,
                        u = [t],
                        s = [e];
                    if (!o || !i) return t === j ? -1 : e === j ? 1 : o ? -1 : i ? 1 : O ? tt(O, t) - tt(O, e) : 0;
                    if (o === i) return a(t, e);
                    for (n = t; n = n.parentNode;) u.unshift(n);
                    for (n = e; n = n.parentNode;) s.unshift(n);
                    for (; u[r] === s[r];) r++;
                    return r ? a(u[r], s[r]) : u[r] === H ? -1 : s[r] === H ? 1 : 0
                }, j) : j
            }, e.matches = function (t, n) { return e(t, null, null, n) }, e.matchesSelector = function (t, n) {
                if ((t.ownerDocument || t) !== j && P(t), n = n.replace(lt, "='$1']"), _.matchesSelector && I && !Y[n + " "] && (!L || !L.test(n)) && (!R || !R.test(n))) try { var r = F.call(t, n); if (r || _.disconnectedMatch || t.document && 11 !== t.document.nodeType) return r } catch (t) { }
                return e(n, j, null, [t]).length > 0
            }, e.contains = function (t, e) { return (t.ownerDocument || t) !== j && P(t), U(t, e) }, e.attr = function (t, e) {
                (t.ownerDocument || t) !== j && P(t);
                var n = C.attrHandle[e.toLowerCase()],
                    r = n && K.call(C.attrHandle, e.toLowerCase()) ? n(t, e, !I) : void 0;
                return void 0 !== r ? r : _.attributes || !I ? t.getAttribute(e) : (r = t.getAttributeNode(e)) && r.specified ? r.value : null
            }, e.escape = function (t) { return (t + "").replace(wt, _t) }, e.error = function (t) { throw new Error("Syntax error, unrecognized expression: " + t) }, e.uniqueSort = function (t) {
                var e, n = [],
                    r = 0,
                    o = 0;
                if (A = !_.detectDuplicates, O = !_.sortStable && t.slice(0), t.sort($), A) { for (; e = t[o++];) e === t[o] && (r = n.push(o)); for (; r--;) t.splice(n[r], 1) }
                return O = null, t
            }, E = e.getText = function (t) {
                var e, n = "",
                    r = 0,
                    o = t.nodeType;
                if (o) { if (1 === o || 9 === o || 11 === o) { if ("string" == typeof t.textContent) return t.textContent; for (t = t.firstChild; t; t = t.nextSibling) n += E(t) } else if (3 === o || 4 === o) return t.nodeValue } else
                    for (; e = t[r++];) n += E(e);
                return n
            }, C = e.selectors = {
                cacheLength: 50,
                createPseudo: r,
                match: ht,
                attrHandle: {},
                find: {},
                relative: { ">": { dir: "parentNode", first: !0 }, " ": { dir: "parentNode" }, "+": { dir: "previousSibling", first: !0 }, "~": { dir: "previousSibling" } },
                preFilter: { ATTR: function (t) { return t[1] = t[1].replace(bt, xt), t[3] = (t[3] || t[4] || t[5] || "").replace(bt, xt), "~=" === t[2] && (t[3] = " " + t[3] + " "), t.slice(0, 4) }, CHILD: function (t) { return t[1] = t[1].toLowerCase(), "nth" === t[1].slice(0, 3) ? (t[3] || e.error(t[0]), t[4] = +(t[4] ? t[5] + (t[6] || 1) : 2 * ("even" === t[3] || "odd" === t[3])), t[5] = +(t[7] + t[8] || "odd" === t[3])) : t[3] && e.error(t[0]), t }, PSEUDO: function (t) { var e, n = !t[6] && t[2]; return ht.CHILD.test(t[0]) ? null : (t[3] ? t[2] = t[4] || t[5] || "" : n && ft.test(n) && (e = k(n, !0)) && (e = n.indexOf(")", n.length - e) - n.length) && (t[0] = t[0].slice(0, e), t[2] = n.slice(0, e)), t.slice(0, 3)) } },
                filter: {
                    TAG: function (t) { var e = t.replace(bt, xt).toLowerCase(); return "*" === t ? function () { return !0 } : function (t) { return t.nodeName && t.nodeName.toLowerCase() === e } },
                    CLASS: function (t) { var e = z[t + " "]; return e || (e = new RegExp("(^|" + nt + ")" + t + "(" + nt + "|$)")) && z(t, function (t) { return e.test("string" == typeof t.className && t.className || "undefined" != typeof t.getAttribute && t.getAttribute("class") || "") }) },
                    ATTR: function (t, n, r) { return function (o) { var i = e.attr(o, t); return null == i ? "!=" === n : !n || (i += "", "=" === n ? i === r : "!=" === n ? i !== r : "^=" === n ? r && 0 === i.indexOf(r) : "*=" === n ? r && i.indexOf(r) > -1 : "$=" === n ? r && i.slice(-r.length) === r : "~=" === n ? (" " + i.replace(at, " ") + " ").indexOf(r) > -1 : "|=" === n && (i === r || i.slice(0, r.length + 1) === r + "-")) } },
                    CHILD: function (t, e, n, r, o) {
                        var i = "nth" !== t.slice(0, 3),
                            a = "last" !== t.slice(-4),
                            u = "of-type" === e;
                        return 1 === r && 0 === o ? function (t) { return !!t.parentNode } : function (e, n, s) {
                            var c, l, f, p, h, d, v = i !== a ? "nextSibling" : "previousSibling",
                                g = e.parentNode,
                                y = u && e.nodeName.toLowerCase(),
                                m = !s && !u,
                                b = !1;
                            if (g) {
                                if (i) {
                                    for (; v;) {
                                        for (p = e; p = p[v];)
                                            if (u ? p.nodeName.toLowerCase() === y : 1 === p.nodeType) return !1;
                                        d = v = "only" === t && !d && "nextSibling"
                                    }
                                    return !0
                                }
                                if (d = [a ? g.firstChild : g.lastChild], a && m) {
                                    for (p = g, f = p[q] || (p[q] = {}), l = f[p.uniqueID] || (f[p.uniqueID] = {}), c = l[t] || [], h = c[0] === B && c[1], b = h && c[2], p = h && g.childNodes[h]; p = ++h && p && p[v] || (b = h = 0) || d.pop();)
                                        if (1 === p.nodeType && ++b && p === e) { l[t] = [B, h, b]; break }
                                } else if (m && (p = e, f = p[q] || (p[q] = {}), l = f[p.uniqueID] || (f[p.uniqueID] = {}), c = l[t] || [], h = c[0] === B && c[1], b = h), b === !1)
                                    for (;
                                        (p = ++h && p && p[v] || (b = h = 0) || d.pop()) && ((u ? p.nodeName.toLowerCase() !== y : 1 !== p.nodeType) || !++b || (m && (f = p[q] || (p[q] = {}), l = f[p.uniqueID] || (f[p.uniqueID] = {}), l[t] = [B, b]), p !== e)););
                                return b -= o, b === r || b % r === 0 && b / r >= 0
                            }
                        }
                    },
                    PSEUDO: function (t, n) { var o, i = C.pseudos[t] || C.setFilters[t.toLowerCase()] || e.error("unsupported pseudo: " + t); return i[q] ? i(n) : i.length > 1 ? (o = [t, t, "", n], C.setFilters.hasOwnProperty(t.toLowerCase()) ? r(function (t, e) { for (var r, o = i(t, n), a = o.length; a--;) r = tt(t, o[a]), t[r] = !(e[r] = o[a]) }) : function (t) { return i(t, 0, o) }) : i }
                },
                pseudos: {
                    not: r(function (t) {
                        var e = [],
                            n = [],
                            o = T(t.replace(ut, "$1"));
                        return o[q] ? r(function (t, e, n, r) { for (var i, a = o(t, null, r, []), u = t.length; u--;)(i = a[u]) && (t[u] = !(e[u] = i)) }) : function (t, r, i) { return e[0] = t, o(e, null, i, n), e[0] = null, !n.pop() }
                    }),
                    has: r(function (t) { return function (n) { return e(t, n).length > 0 } }),
                    contains: r(function (t) {
                        return t = t.replace(bt, xt),
                            function (e) { return (e.textContent || e.innerText || E(e)).indexOf(t) > -1 }
                    }),
                    lang: r(function (t) {
                        return pt.test(t || "") || e.error("unsupported lang: " + t), t = t.replace(bt, xt).toLowerCase(),
                            function (e) {
                                var n;
                                do
                                    if (n = I ? e.lang : e.getAttribute("xml:lang") || e.getAttribute("lang")) return n = n.toLowerCase(), n === t || 0 === n.indexOf(t + "-");
                                while ((e = e.parentNode) && 1 === e.nodeType);
                                return !1
                            }
                    }),
                    target: function (e) { var n = t.location && t.location.hash; return n && n.slice(1) === e.id },
                    root: function (t) { return t === D },
                    focus: function (t) { return t === j.activeElement && (!j.hasFocus || j.hasFocus()) && !!(t.type || t.href || ~t.tabIndex) },
                    enabled: c(!1),
                    disabled: c(!0),
                    checked: function (t) { var e = t.nodeName.toLowerCase(); return "input" === e && !!t.checked || "option" === e && !!t.selected },
                    selected: function (t) { return t.parentNode && t.parentNode.selectedIndex, t.selected === !0 },
                    empty: function (t) {
                        for (t = t.firstChild; t; t = t.nextSibling)
                            if (t.nodeType < 6) return !1;
                        return !0
                    },
                    parent: function (t) { return !C.pseudos.empty(t) },
                    header: function (t) { return vt.test(t.nodeName) },
                    input: function (t) { return dt.test(t.nodeName) },
                    button: function (t) { var e = t.nodeName.toLowerCase(); return "input" === e && "button" === t.type || "button" === e },
                    text: function (t) { var e; return "input" === t.nodeName.toLowerCase() && "text" === t.type && (null == (e = t.getAttribute("type")) || "text" === e.toLowerCase()) },
                    first: l(function () { return [0] }),
                    last: l(function (t, e) { return [e - 1] }),
                    eq: l(function (t, e, n) { return [n < 0 ? n + e : n] }),
                    even: l(function (t, e) { for (var n = 0; n < e; n += 2) t.push(n); return t }),
                    odd: l(function (t, e) { for (var n = 1; n < e; n += 2) t.push(n); return t }),
                    lt: l(function (t, e, n) { for (var r = n < 0 ? n + e : n; --r >= 0;) t.push(r); return t }),
                    gt: l(function (t, e, n) { for (var r = n < 0 ? n + e : n; ++r < e;) t.push(r); return t })
                }
            }, C.pseudos.nth = C.pseudos.eq;
            for (w in { radio: !0, checkbox: !0, file: !0, password: !0, image: !0 }) C.pseudos[w] = u(w);
            for (w in { submit: !0, reset: !0 }) C.pseudos[w] = s(w);
            return p.prototype = C.filters = C.pseudos, C.setFilters = new p, k = e.tokenize = function (t, n) { var r, o, i, a, u, s, c, l = V[t + " "]; if (l) return n ? 0 : l.slice(0); for (u = t, s = [], c = C.preFilter; u;) { r && !(o = st.exec(u)) || (o && (u = u.slice(o[0].length) || u), s.push(i = [])), r = !1, (o = ct.exec(u)) && (r = o.shift(), i.push({ value: r, type: o[0].replace(ut, " ") }), u = u.slice(r.length)); for (a in C.filter) !(o = ht[a].exec(u)) || c[a] && !(o = c[a](o)) || (r = o.shift(), i.push({ value: r, type: a, matches: o }), u = u.slice(r.length)); if (!r) break } return n ? u.length : u ? e.error(t) : V(t, s).slice(0) }, T = e.compile = function (t, e) {
                var n, r = [],
                    o = [],
                    i = Y[t + " "];
                if (!i) {
                    for (e || (e = k(t)), n = e.length; n--;) i = b(e[n]), i[q] ? r.push(i) : o.push(i);
                    i = Y(t, x(o, r)), i.selector = t
                }
                return i
            }, S = e.select = function (t, e, n, r) {
                var o, i, a, u, s, c = "function" == typeof t && t,
                    l = !r && k(t = c.selector || t);
                if (n = n || [], 1 === l.length) {
                    if (i = l[0] = l[0].slice(0), i.length > 2 && "ID" === (a = i[0]).type && 9 === e.nodeType && I && C.relative[i[1].type]) {
                        if (e = (C.find.ID(a.matches[0].replace(bt, xt), e) || [])[0], !e) return n;
                        c && (e = e.parentNode), t = t.slice(i.shift().value.length)
                    }
                    for (o = ht.needsContext.test(t) ? 0 : i.length; o-- && (a = i[o], !C.relative[u = a.type]);)
                        if ((s = C.find[u]) && (r = s(a.matches[0].replace(bt, xt), mt.test(i[0].type) && f(e.parentNode) || e))) { if (i.splice(o, 1), t = r.length && h(i), !t) return Q.apply(n, r), n; break }
                }
                return (c || T(t, l))(r, e, !I, n, !e || mt.test(t) && f(e.parentNode) || e), n
            }, _.sortStable = q.split("").sort($).join("") === q, _.detectDuplicates = !!A, P(), _.sortDetached = o(function (t) { return 1 & t.compareDocumentPosition(j.createElement("fieldset")) }), o(function (t) { return t.innerHTML = "<a href='#'></a>", "#" === t.firstChild.getAttribute("href") }) || i("type|href|height|width", function (t, e, n) { if (!n) return t.getAttribute(e, "type" === e.toLowerCase() ? 1 : 2) }), _.attributes && o(function (t) { return t.innerHTML = "<input/>", t.firstChild.setAttribute("value", ""), "" === t.firstChild.getAttribute("value") }) || i("value", function (t, e, n) { if (!n && "input" === t.nodeName.toLowerCase()) return t.defaultValue }), o(function (t) { return null == t.getAttribute("disabled") }) || i(et, function (t, e, n) { var r; if (!n) return t[e] === !0 ? e.toLowerCase() : (r = t.getAttributeNode(e)) && r.specified ? r.value : null }), e
        }(n);
        mt.find = Ct, mt.expr = Ct.selectors, mt.expr[":"] = mt.expr.pseudos, mt.uniqueSort = mt.unique = Ct.uniqueSort, mt.text = Ct.getText, mt.isXMLDoc = Ct.isXML, mt.contains = Ct.contains, mt.escapeSelector = Ct.escape;
        var Et = function (t, e, n) {
            for (var r = [], o = void 0 !== n;
                (t = t[e]) && 9 !== t.nodeType;)
                if (1 === t.nodeType) {
                    if (o && mt(t).is(n)) break;
                    r.push(t)
                }
            return r
        },
            Mt = function (t, e) { for (var n = []; t; t = t.nextSibling) 1 === t.nodeType && t !== e && n.push(t); return n },
            kt = mt.expr.match.needsContext,
            Tt = /^<([a-z][^\/\0>:\x20\t\r\n\f]*)[\x20\t\r\n\f]*\/?>(?:<\/\1>|)$/i,
            St = /^.[^:#\[\.,]*$/;
        mt.filter = function (t, e, n) {
            var r = e[0];
            return n && (t = ":not(" + t + ")"), 1 === e.length && 1 === r.nodeType ? mt.find.matchesSelector(r, t) ? [r] : [] : mt.find.matches(t, mt.grep(e, function (t) {
                return 1 === t.nodeType
            }))
        }, mt.fn.extend({
            find: function (t) {
                var e, n, r = this.length,
                    o = this;
                if ("string" != typeof t) return this.pushStack(mt(t).filter(function () {
                    for (e = 0; e < r; e++)
                        if (mt.contains(o[e], this)) return !0
                }));
                for (n = this.pushStack([]), e = 0; e < r; e++) mt.find(t, o[e], n);
                return r > 1 ? mt.uniqueSort(n) : n
            },
            filter: function (t) { return this.pushStack(s(this, t || [], !1)) },
            not: function (t) { return this.pushStack(s(this, t || [], !0)) },
            is: function (t) { return !!s(this, "string" == typeof t && kt.test(t) ? mt(t) : t || [], !1).length }
        });
        var Nt, Ot = /^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]+))$/,
            At = mt.fn.init = function (t, e, n) {
                var r, o;
                if (!t) return this;
                if (n = n || Nt, "string" == typeof t) {
                    if (r = "<" === t[0] && ">" === t[t.length - 1] && t.length >= 3 ? [null, t, null] : Ot.exec(t), !r || !r[1] && e) return !e || e.jquery ? (e || n).find(t) : this.constructor(e).find(t);
                    if (r[1]) {
                        if (e = e instanceof mt ? e[0] : e, mt.merge(this, mt.parseHTML(r[1], e && e.nodeType ? e.ownerDocument || e : it, !0)), Tt.test(r[1]) && mt.isPlainObject(e))
                            for (r in e) mt.isFunction(this[r]) ? this[r](e[r]) : this.attr(r, e[r]);
                        return this
                    }
                    return o = it.getElementById(r[2]), o && (this[0] = o, this.length = 1), this
                }
                return t.nodeType ? (this[0] = t, this.length = 1, this) : mt.isFunction(t) ? void 0 !== n.ready ? n.ready(t) : t(mt) : mt.makeArray(t, this)
            };
        At.prototype = mt.fn, Nt = mt(it);
        var Pt = /^(?:parents|prev(?:Until|All))/,
            jt = { children: !0, contents: !0, next: !0, prev: !0 };
        mt.fn.extend({
            has: function (t) {
                var e = mt(t, this),
                    n = e.length;
                return this.filter(function () {
                    for (var t = 0; t < n; t++)
                        if (mt.contains(this, e[t])) return !0
                })
            },
            closest: function (t, e) {
                var n, r = 0,
                    o = this.length,
                    i = [],
                    a = "string" != typeof t && mt(t);
                if (!kt.test(t))
                    for (; r < o; r++)
                        for (n = this[r]; n && n !== e; n = n.parentNode)
                            if (n.nodeType < 11 && (a ? a.index(n) > -1 : 1 === n.nodeType && mt.find.matchesSelector(n, t))) { i.push(n); break }
                return this.pushStack(i.length > 1 ? mt.uniqueSort(i) : i)
            },
            index: function (t) { return t ? "string" == typeof t ? lt.call(mt(t), this[0]) : lt.call(this, t.jquery ? t[0] : t) : this[0] && this[0].parentNode ? this.first().prevAll().length : -1 },
            add: function (t, e) { return this.pushStack(mt.uniqueSort(mt.merge(this.get(), mt(t, e)))) },
            addBack: function (t) { return this.add(null == t ? this.prevObject : this.prevObject.filter(t)) }
        }), mt.each({ parent: function (t) { var e = t.parentNode; return e && 11 !== e.nodeType ? e : null }, parents: function (t) { return Et(t, "parentNode") }, parentsUntil: function (t, e, n) { return Et(t, "parentNode", n) }, next: function (t) { return c(t, "nextSibling") }, prev: function (t) { return c(t, "previousSibling") }, nextAll: function (t) { return Et(t, "nextSibling") }, prevAll: function (t) { return Et(t, "previousSibling") }, nextUntil: function (t, e, n) { return Et(t, "nextSibling", n) }, prevUntil: function (t, e, n) { return Et(t, "previousSibling", n) }, siblings: function (t) { return Mt((t.parentNode || {}).firstChild, t) }, children: function (t) { return Mt(t.firstChild) }, contents: function (t) { return t.contentDocument || mt.merge([], t.childNodes) } }, function (t, e) { mt.fn[t] = function (n, r) { var o = mt.map(this, e, n); return "Until" !== t.slice(-5) && (r = n), r && "string" == typeof r && (o = mt.filter(r, o)), this.length > 1 && (jt[t] || mt.uniqueSort(o), Pt.test(t) && o.reverse()), this.pushStack(o) } });
        var Dt = /[^\x20\t\r\n\f]+/g;
        mt.Callbacks = function (t) {
            t = "string" == typeof t ? l(t) : mt.extend({}, t);
            var e, n, r, o, i = [],
                a = [],
                u = -1,
                s = function () {
                    for (o = t.once, r = e = !0; a.length; u = -1)
                        for (n = a.shift(); ++u < i.length;) i[u].apply(n[0], n[1]) === !1 && t.stopOnFalse && (u = i.length, n = !1);
                    t.memory || (n = !1), e = !1, o && (i = n ? [] : "")
                },
                c = {
                    add: function () { return i && (n && !e && (u = i.length - 1, a.push(n)), function e(n) { mt.each(n, function (n, r) { mt.isFunction(r) ? t.unique && c.has(r) || i.push(r) : r && r.length && "string" !== mt.type(r) && e(r) }) }(arguments), n && !e && s()), this },
                    remove: function () {
                        return mt.each(arguments, function (t, e) {
                            for (var n;
                                (n = mt.inArray(e, i, n)) > -1;) i.splice(n, 1), n <= u && u--
                        }), this
                    },
                    has: function (t) { return t ? mt.inArray(t, i) > -1 : i.length > 0 },
                    empty: function () { return i && (i = []), this },
                    disable: function () { return o = a = [], i = n = "", this },
                    disabled: function () { return !i },
                    lock: function () { return o = a = [], n || e || (i = n = ""), this },
                    locked: function () { return !!o },
                    fireWith: function (t, n) { return o || (n = n || [], n = [t, n.slice ? n.slice() : n], a.push(n), e || s()), this },
                    fire: function () { return c.fireWith(this, arguments), this },
                    fired: function () { return !!r }
                };
            return c
        }, mt.extend({
            Deferred: function (t) {
                var e = [
                    ["notify", "progress", mt.Callbacks("memory"), mt.Callbacks("memory"), 2],
                    ["resolve", "done", mt.Callbacks("once memory"), mt.Callbacks("once memory"), 0, "resolved"],
                    ["reject", "fail", mt.Callbacks("once memory"), mt.Callbacks("once memory"), 1, "rejected"]
                ],
                    r = "pending",
                    o = {
                        state: function () { return r },
                        always: function () { return i.done(arguments).fail(arguments), this },
                        catch: function (t) { return o.then(null, t) },
                        pipe: function () {
                            var t = arguments;
                            return mt.Deferred(function (n) {
                                mt.each(e, function (e, r) {
                                    var o = mt.isFunction(t[r[4]]) && t[r[4]];
                                    i[r[1]](function () {
                                        var t = o && o.apply(this, arguments);
                                        t && mt.isFunction(t.promise) ? t.promise().progress(n.notify).done(n.resolve).fail(n.reject) : n[r[0] + "With"](this, o ? [t] : arguments)
                                    })
                                }), t = null
                            }).promise()
                        },
                        then: function (t, r, o) {
                            function i(t, e, r, o) {
                                return function () {
                                    var u = this,
                                        s = arguments,
                                        c = function () {
                                            var n, c;
                                            if (!(t < a)) {
                                                if (n = r.apply(u, s), n === e.promise()) throw new TypeError("Thenable self-resolution");
                                                c = n && ("object" == typeof n || "function" == typeof n) && n.then, mt.isFunction(c) ? o ? c.call(n, i(a, e, f, o), i(a, e, p, o)) : (a++ , c.call(n, i(a, e, f, o), i(a, e, p, o), i(a, e, f, e.notifyWith))) : (r !== f && (u = void 0, s = [n]), (o || e.resolveWith)(u, s))
                                            }
                                        },
                                        l = o ? c : function () { try { c() } catch (n) { mt.Deferred.exceptionHook && mt.Deferred.exceptionHook(n, l.stackTrace), t + 1 >= a && (r !== p && (u = void 0, s = [n]), e.rejectWith(u, s)) } };
                                    t ? l() : (mt.Deferred.getStackHook && (l.stackTrace = mt.Deferred.getStackHook()), n.setTimeout(l))
                                }
                            }
                            var a = 0;
                            return mt.Deferred(function (n) { e[0][3].add(i(0, n, mt.isFunction(o) ? o : f, n.notifyWith)), e[1][3].add(i(0, n, mt.isFunction(t) ? t : f)), e[2][3].add(i(0, n, mt.isFunction(r) ? r : p)) }).promise()
                        },
                        promise: function (t) { return null != t ? mt.extend(t, o) : o }
                    },
                    i = {};
                return mt.each(e, function (t, n) {
                    var a = n[2],
                        u = n[5];
                    o[n[1]] = a.add, u && a.add(function () { r = u }, e[3 - t][2].disable, e[0][2].lock), a.add(n[3].fire), i[n[0]] = function () { return i[n[0] + "With"](this === i ? void 0 : this, arguments), this }, i[n[0] + "With"] = a.fireWith
                }), o.promise(i), t && t.call(i, i), i
            },
            when: function (t) {
                var e = arguments.length,
                    n = e,
                    r = Array(n),
                    o = ut.call(arguments),
                    i = mt.Deferred(),
                    a = function (t) { return function (n) { r[t] = this, o[t] = arguments.length > 1 ? ut.call(arguments) : n, --e || i.resolveWith(r, o) } };
                if (e <= 1 && (h(t, i.done(a(n)).resolve, i.reject), "pending" === i.state() || mt.isFunction(o[n] && o[n].then))) return i.then();
                for (; n--;) h(o[n], a(n), i.reject);
                return i.promise()
            }
        });
        var It = /^(Eval|Internal|Range|Reference|Syntax|Type|URI)Error$/;
        mt.Deferred.exceptionHook = function (t, e) { n.console && n.console.warn && t && It.test(t.name) && n.console.warn("jQuery.Deferred exception: " + t.message, t.stack, e) }, mt.readyException = function (t) { n.setTimeout(function () { throw t }) };
        var Rt = mt.Deferred();
        mt.fn.ready = function (t) { return Rt.then(t).catch(function (t) { mt.readyException(t) }), this }, mt.extend({
            isReady: !1,
            readyWait: 1,
            holdReady: function (t) { t ? mt.readyWait++ : mt.ready(!0) },
            ready: function (t) {
                (t === !0 ? --mt.readyWait : mt.isReady) || (mt.isReady = !0, t !== !0 && --mt.readyWait > 0 || Rt.resolveWith(it, [mt]))
            }
        }), mt.ready.then = Rt.then, "complete" === it.readyState || "loading" !== it.readyState && !it.documentElement.doScroll ? n.setTimeout(mt.ready) : (it.addEventListener("DOMContentLoaded", d), n.addEventListener("load", d));
        var Lt = function (t, e, n, r, o, i, a) {
            var u = 0,
                s = t.length,
                c = null == n;
            if ("object" === mt.type(n)) { o = !0; for (u in n) Lt(t, e, u, n[u], !0, i, a) } else if (void 0 !== r && (o = !0, mt.isFunction(r) || (a = !0), c && (a ? (e.call(t, r), e = null) : (c = e, e = function (t, e, n) { return c.call(mt(t), n) })), e))
                for (; u < s; u++) e(t[u], n, a ? r : r.call(t[u], u, e(t[u], n)));
            return o ? t : c ? e.call(t) : s ? e(t[0], n) : i
        },
            Ft = function (t) { return 1 === t.nodeType || 9 === t.nodeType || !+t.nodeType };
        v.uid = 1, v.prototype = {
            cache: function (t) { var e = t[this.expando]; return e || (e = {}, Ft(t) && (t.nodeType ? t[this.expando] = e : Object.defineProperty(t, this.expando, { value: e, configurable: !0 }))), e },
            set: function (t, e, n) {
                var r, o = this.cache(t);
                if ("string" == typeof e) o[mt.camelCase(e)] = n;
                else
                    for (r in e) o[mt.camelCase(r)] = e[r];
                return o
            },
            get: function (t, e) { return void 0 === e ? this.cache(t) : t[this.expando] && t[this.expando][mt.camelCase(e)] },
            access: function (t, e, n) { return void 0 === e || e && "string" == typeof e && void 0 === n ? this.get(t, e) : (this.set(t, e, n), void 0 !== n ? n : e) },
            remove: function (t, e) { var n, r = t[this.expando]; if (void 0 !== r) { if (void 0 !== e) { mt.isArray(e) ? e = e.map(mt.camelCase) : (e = mt.camelCase(e), e = e in r ? [e] : e.match(Dt) || []), n = e.length; for (; n--;) delete r[e[n]] } (void 0 === e || mt.isEmptyObject(r)) && (t.nodeType ? t[this.expando] = void 0 : delete t[this.expando]) } },
            hasData: function (t) { var e = t[this.expando]; return void 0 !== e && !mt.isEmptyObject(e) }
        };
        var Ut = new v,
            qt = new v,
            Ht = /^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,
            Bt = /[A-Z]/g;
        mt.extend({ hasData: function (t) { return qt.hasData(t) || Ut.hasData(t) }, data: function (t, e, n) { return qt.access(t, e, n) }, removeData: function (t, e) { qt.remove(t, e) }, _data: function (t, e, n) { return Ut.access(t, e, n) }, _removeData: function (t, e) { Ut.remove(t, e) } }), mt.fn.extend({
            data: function (t, e) {
                var n, r, o, i = this[0],
                    a = i && i.attributes;
                if (void 0 === t) {
                    if (this.length && (o = qt.get(i), 1 === i.nodeType && !Ut.get(i, "hasDataAttrs"))) {
                        for (n = a.length; n--;) a[n] && (r = a[n].name, 0 === r.indexOf("data-") && (r = mt.camelCase(r.slice(5)), y(i, r, o[r])));
                        Ut.set(i, "hasDataAttrs", !0)
                    }
                    return o
                }
                return "object" == typeof t ? this.each(function () { qt.set(this, t) }) : Lt(this, function (e) { var n; if (i && void 0 === e) { if (n = qt.get(i, t), void 0 !== n) return n; if (n = y(i, t), void 0 !== n) return n } else this.each(function () { qt.set(this, t, e) }) }, null, e, arguments.length > 1, null, !0)
            },
            removeData: function (t) { return this.each(function () { qt.remove(this, t) }) }
        }), mt.extend({
            queue: function (t, e, n) { var r; if (t) return e = (e || "fx") + "queue", r = Ut.get(t, e), n && (!r || mt.isArray(n) ? r = Ut.access(t, e, mt.makeArray(n)) : r.push(n)), r || [] },
            dequeue: function (t, e) {
                e = e || "fx";
                var n = mt.queue(t, e),
                    r = n.length,
                    o = n.shift(),
                    i = mt._queueHooks(t, e),
                    a = function () { mt.dequeue(t, e) };
                "inprogress" === o && (o = n.shift(), r--), o && ("fx" === e && n.unshift("inprogress"), delete i.stop, o.call(t, a, i)), !r && i && i.empty.fire()
            },
            _queueHooks: function (t, e) { var n = e + "queueHooks"; return Ut.get(t, n) || Ut.access(t, n, { empty: mt.Callbacks("once memory").add(function () { Ut.remove(t, [e + "queue", n]) }) }) }
        }), mt.fn.extend({
            queue: function (t, e) {
                var n = 2;
                return "string" != typeof t && (e = t, t = "fx", n--), arguments.length < n ? mt.queue(this[0], t) : void 0 === e ? this : this.each(function () {
                    var n = mt.queue(this, t, e);
                    mt._queueHooks(this, t), "fx" === t && "inprogress" !== n[0] && mt.dequeue(this, t)
                })
            },
            dequeue: function (t) { return this.each(function () { mt.dequeue(this, t) }) },
            clearQueue: function (t) { return this.queue(t || "fx", []) },
            promise: function (t, e) {
                var n, r = 1,
                    o = mt.Deferred(),
                    i = this,
                    a = this.length,
                    u = function () { --r || o.resolveWith(i, [i]) };
                for ("string" != typeof t && (e = t, t = void 0), t = t || "fx"; a--;) n = Ut.get(i[a], t + "queueHooks"), n && n.empty && (r++ , n.empty.add(u));
                return u(), o.promise(e)
            }
        });
        var Wt = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source,
            zt = new RegExp("^(?:([+-])=|)(" + Wt + ")([a-z%]*)$", "i"),
            Vt = ["Top", "Right", "Bottom", "Left"],
            Yt = function (t, e) { return t = e || t, "none" === t.style.display || "" === t.style.display && mt.contains(t.ownerDocument, t) && "none" === mt.css(t, "display") },
            $t = function (t, e, n, r) {
                var o, i, a = {};
                for (i in e) a[i] = t.style[i], t.style[i] = e[i];
                o = n.apply(t, r || []);
                for (i in e) t.style[i] = a[i];
                return o
            },
            Kt = {};
        mt.fn.extend({ show: function () { return x(this, !0) }, hide: function () { return x(this) }, toggle: function (t) { return "boolean" == typeof t ? t ? this.show() : this.hide() : this.each(function () { Yt(this) ? mt(this).show() : mt(this).hide() }) } });
        var Xt = /^(?:checkbox|radio)$/i,
            Gt = /<([a-z][^\/\0>\x20\t\r\n\f]+)/i,
            Jt = /^$|\/(?:java|ecma)script/i,
            Qt = { option: [1, "<select multiple='multiple'>", "</select>"], thead: [1, "<table>", "</table>"], col: [2, "<table><colgroup>", "</colgroup></table>"], tr: [2, "<table><tbody>", "</tbody></table>"], td: [3, "<table><tbody><tr>", "</tr></tbody></table>"], _default: [0, "", ""] };
        Qt.optgroup = Qt.option, Qt.tbody = Qt.tfoot = Qt.colgroup = Qt.caption = Qt.thead, Qt.th = Qt.td;
        var Zt = /<|&#?\w+;/;
        ! function () {
            var t = it.createDocumentFragment(),
                e = t.appendChild(it.createElement("div")),
                n = it.createElement("input");
            n.setAttribute("type", "radio"), n.setAttribute("checked", "checked"), n.setAttribute("name", "t"), e.appendChild(n), gt.checkClone = e.cloneNode(!0).cloneNode(!0).lastChild.checked, e.innerHTML = "<textarea>x</textarea>", gt.noCloneChecked = !!e.cloneNode(!0).lastChild.defaultValue
        }();
        var te = it.documentElement,
            ee = /^key/,
            ne = /^(?:mouse|pointer|contextmenu|drag|drop)|click/,
            re = /^([^.]*)(?:\.(.+)|)/;
        mt.event = {
            global: {},
            add: function (t, e, n, r, o) {
                var i, a, u, s, c, l, f, p, h, d, v, g = Ut.get(t);
                if (g)
                    for (n.handler && (i = n, n = i.handler, o = i.selector), o && mt.find.matchesSelector(te, o), n.guid || (n.guid = mt.guid++), (s = g.events) || (s = g.events = {}), (a = g.handle) || (a = g.handle = function (e) { return "undefined" != typeof mt && mt.event.triggered !== e.type ? mt.event.dispatch.apply(t, arguments) : void 0 }), e = (e || "").match(Dt) || [""], c = e.length; c--;) u = re.exec(e[c]) || [], h = v = u[1], d = (u[2] || "").split(".").sort(), h && (f = mt.event.special[h] || {}, h = (o ? f.delegateType : f.bindType) || h, f = mt.event.special[h] || {}, l = mt.extend({ type: h, origType: v, data: r, handler: n, guid: n.guid, selector: o, needsContext: o && mt.expr.match.needsContext.test(o), namespace: d.join(".") }, i), (p = s[h]) || (p = s[h] = [], p.delegateCount = 0, f.setup && f.setup.call(t, r, d, a) !== !1 || t.addEventListener && t.addEventListener(h, a)), f.add && (f.add.call(t, l), l.handler.guid || (l.handler.guid = n.guid)), o ? p.splice(p.delegateCount++, 0, l) : p.push(l), mt.event.global[h] = !0)
            },
            remove: function (t, e, n, r, o) {
                var i, a, u, s, c, l, f, p, h, d, v, g = Ut.hasData(t) && Ut.get(t);
                if (g && (s = g.events)) {
                    for (e = (e || "").match(Dt) || [""], c = e.length; c--;)
                        if (u = re.exec(e[c]) || [], h = v = u[1], d = (u[2] || "").split(".").sort(), h) {
                            for (f = mt.event.special[h] || {}, h = (r ? f.delegateType : f.bindType) || h, p = s[h] || [], u = u[2] && new RegExp("(^|\\.)" + d.join("\\.(?:.*\\.|)") + "(\\.|$)"), a = i = p.length; i--;) l = p[i], !o && v !== l.origType || n && n.guid !== l.guid || u && !u.test(l.namespace) || r && r !== l.selector && ("**" !== r || !l.selector) || (p.splice(i, 1), l.selector && p.delegateCount-- , f.remove && f.remove.call(t, l));
                            a && !p.length && (f.teardown && f.teardown.call(t, d, g.handle) !== !1 || mt.removeEvent(t, h, g.handle), delete s[h])
                        } else
                            for (h in s) mt.event.remove(t, h + e[c], n, r, !0);
                    mt.isEmptyObject(s) && Ut.remove(t, "handle events")
                }
            },
            dispatch: function (t) {
                var e, n, r, o, i, a, u = mt.event.fix(t),
                    s = new Array(arguments.length),
                    c = (Ut.get(this, "events") || {})[u.type] || [],
                    l = mt.event.special[u.type] || {};
                for (s[0] = u, e = 1; e < arguments.length; e++) s[e] = arguments[e];
                if (u.delegateTarget = this, !l.preDispatch || l.preDispatch.call(this, u) !== !1) {
                    for (a = mt.event.handlers.call(this, u, c), e = 0;
                        (o = a[e++]) && !u.isPropagationStopped();)
                        for (u.currentTarget = o.elem, n = 0;
                            (i = o.handlers[n++]) && !u.isImmediatePropagationStopped();) u.rnamespace && !u.rnamespace.test(i.namespace) || (u.handleObj = i, u.data = i.data, r = ((mt.event.special[i.origType] || {}).handle || i.handler).apply(o.elem, s), void 0 !== r && (u.result = r) === !1 && (u.preventDefault(), u.stopPropagation()));
                    return l.postDispatch && l.postDispatch.call(this, u), u.result
                }
            },
            handlers: function (t, e) {
                var n, r, o, i, a, u = [],
                    s = e.delegateCount,
                    c = t.target;
                if (s && c.nodeType && !("click" === t.type && t.button >= 1))
                    for (; c !== this; c = c.parentNode || this)
                        if (1 === c.nodeType && ("click" !== t.type || c.disabled !== !0)) {
                            for (i = [], a = {}, n = 0; n < s; n++) r = e[n], o = r.selector + " ", void 0 === a[o] && (a[o] = r.needsContext ? mt(o, this).index(c) > -1 : mt.find(o, this, null, [c]).length), a[o] && i.push(r);
                            i.length && u.push({ elem: c, handlers: i })
                        }
                return c = this, s < e.length && u.push({ elem: c, handlers: e.slice(s) }), u
            },
            addProp: function (t, e) { Object.defineProperty(mt.Event.prototype, t, { enumerable: !0, configurable: !0, get: mt.isFunction(e) ? function () { if (this.originalEvent) return e(this.originalEvent) } : function () { if (this.originalEvent) return this.originalEvent[t] }, set: function (e) { Object.defineProperty(this, t, { enumerable: !0, configurable: !0, writable: !0, value: e }) } }) },
            fix: function (t) { return t[mt.expando] ? t : new mt.Event(t) },
            special: { load: { noBubble: !0 }, focus: { trigger: function () { if (this !== k() && this.focus) return this.focus(), !1 }, delegateType: "focusin" }, blur: { trigger: function () { if (this === k() && this.blur) return this.blur(), !1 }, delegateType: "focusout" }, click: { trigger: function () { if ("checkbox" === this.type && this.click && mt.nodeName(this, "input")) return this.click(), !1 }, _default: function (t) { return mt.nodeName(t.target, "a") } }, beforeunload: { postDispatch: function (t) { void 0 !== t.result && t.originalEvent && (t.originalEvent.returnValue = t.result) } } }
        }, mt.removeEvent = function (t, e, n) { t.removeEventListener && t.removeEventListener(e, n) }, mt.Event = function (t, e) { return this instanceof mt.Event ? (t && t.type ? (this.originalEvent = t, this.type = t.type, this.isDefaultPrevented = t.defaultPrevented || void 0 === t.defaultPrevented && t.returnValue === !1 ? E : M, this.target = t.target && 3 === t.target.nodeType ? t.target.parentNode : t.target, this.currentTarget = t.currentTarget, this.relatedTarget = t.relatedTarget) : this.type = t, e && mt.extend(this, e), this.timeStamp = t && t.timeStamp || mt.now(), void (this[mt.expando] = !0)) : new mt.Event(t, e) }, mt.Event.prototype = {
            constructor: mt.Event,
            isDefaultPrevented: M,
            isPropagationStopped: M,
            isImmediatePropagationStopped: M,
            isSimulated: !1,
            preventDefault: function () {
                var t = this.originalEvent;
                this.isDefaultPrevented = E, t && !this.isSimulated && t.preventDefault()
            },
            stopPropagation: function () {
                var t = this.originalEvent;
                this.isPropagationStopped = E, t && !this.isSimulated && t.stopPropagation()
            },
            stopImmediatePropagation: function () {
                var t = this.originalEvent;
                this.isImmediatePropagationStopped = E, t && !this.isSimulated && t.stopImmediatePropagation(), this.stopPropagation()
            }
        }, mt.each({ altKey: !0, bubbles: !0, cancelable: !0, changedTouches: !0, ctrlKey: !0, detail: !0, eventPhase: !0, metaKey: !0, pageX: !0, pageY: !0, shiftKey: !0, view: !0, char: !0, charCode: !0, key: !0, keyCode: !0, button: !0, buttons: !0, clientX: !0, clientY: !0, offsetX: !0, offsetY: !0, pointerId: !0, pointerType: !0, screenX: !0, screenY: !0, targetTouches: !0, toElement: !0, touches: !0, which: function (t) { var e = t.button; return null == t.which && ee.test(t.type) ? null != t.charCode ? t.charCode : t.keyCode : !t.which && void 0 !== e && ne.test(t.type) ? 1 & e ? 1 : 2 & e ? 3 : 4 & e ? 2 : 0 : t.which } }, mt.event.addProp), mt.each({ mouseenter: "mouseover", mouseleave: "mouseout", pointerenter: "pointerover", pointerleave: "pointerout" }, function (t, e) {
            mt.event.special[t] = {
                delegateType: e,
                bindType: e,
                handle: function (t) {
                    var n, r = this,
                        o = t.relatedTarget,
                        i = t.handleObj;
                    return o && (o === r || mt.contains(r, o)) || (t.type = i.origType, n = i.handler.apply(this, arguments), t.type = e), n
                }
            }
        }), mt.fn.extend({ on: function (t, e, n, r) { return T(this, t, e, n, r) }, one: function (t, e, n, r) { return T(this, t, e, n, r, 1) }, off: function (t, e, n) { var r, o; if (t && t.preventDefault && t.handleObj) return r = t.handleObj, mt(t.delegateTarget).off(r.namespace ? r.origType + "." + r.namespace : r.origType, r.selector, r.handler), this; if ("object" == typeof t) { for (o in t) this.off(o, e, t[o]); return this } return e !== !1 && "function" != typeof e || (n = e, e = void 0), n === !1 && (n = M), this.each(function () { mt.event.remove(this, t, n, e) }) } });
        var oe = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([a-z][^\/\0>\x20\t\r\n\f]*)[^>]*)\/>/gi,
            ie = /<script|<style|<link/i,
            ae = /checked\s*(?:[^=]|=\s*.checked.)/i,
            ue = /^true\/(.*)/,
            se = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;
        mt.extend({
            htmlPrefilter: function (t) { return t.replace(oe, "<$1></$2>") },
            clone: function (t, e, n) {
                var r, o, i, a, u = t.cloneNode(!0),
                    s = mt.contains(t.ownerDocument, t);
                if (!(gt.noCloneChecked || 1 !== t.nodeType && 11 !== t.nodeType || mt.isXMLDoc(t)))
                    for (a = w(u), i = w(t), r = 0, o = i.length; r < o; r++) P(i[r], a[r]);
                if (e)
                    if (n)
                        for (i = i || w(t), a = a || w(u), r = 0, o = i.length; r < o; r++) A(i[r], a[r]);
                    else A(t, u);
                return a = w(u, "script"), a.length > 0 && _(a, !s && w(t, "script")), u
            },
            cleanData: function (t) {
                for (var e, n, r, o = mt.event.special, i = 0; void 0 !== (n = t[i]); i++)
                    if (Ft(n)) {
                        if (e = n[Ut.expando]) {
                            if (e.events)
                                for (r in e.events) o[r] ? mt.event.remove(n, r) : mt.removeEvent(n, r, e.handle);
                            n[Ut.expando] = void 0
                        }
                        n[qt.expando] && (n[qt.expando] = void 0)
                    }
            }
        }), mt.fn.extend({
            detach: function (t) { return D(this, t, !0) },
            remove: function (t) { return D(this, t) },
            text: function (t) { return Lt(this, function (t) { return void 0 === t ? mt.text(this) : this.empty().each(function () { 1 !== this.nodeType && 11 !== this.nodeType && 9 !== this.nodeType || (this.textContent = t) }) }, null, t, arguments.length) },
            append: function () {
                return j(this, arguments, function (t) {
                    if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) {
                        var e = S(this, t);
                        e.appendChild(t)
                    }
                })
            },
            prepend: function () {
                return j(this, arguments, function (t) {
                    if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) {
                        var e = S(this, t);
                        e.insertBefore(t, e.firstChild)
                    }
                })
            },
            before: function () { return j(this, arguments, function (t) { this.parentNode && this.parentNode.insertBefore(t, this) }) },
            after: function () { return j(this, arguments, function (t) { this.parentNode && this.parentNode.insertBefore(t, this.nextSibling) }) },
            empty: function () { for (var t, e = 0; null != (t = this[e]); e++) 1 === t.nodeType && (mt.cleanData(w(t, !1)), t.textContent = ""); return this },
            clone: function (t, e) { return t = null != t && t, e = null == e ? t : e, this.map(function () { return mt.clone(this, t, e) }) },
            html: function (t) {
                return Lt(this, function (t) {
                    var e = this[0] || {},
                        n = 0,
                        r = this.length;
                    if (void 0 === t && 1 === e.nodeType) return e.innerHTML;
                    if ("string" == typeof t && !ie.test(t) && !Qt[(Gt.exec(t) || ["", ""])[1].toLowerCase()]) {
                        t = mt.htmlPrefilter(t);
                        try {
                            for (; n < r; n++) e = this[n] || {}, 1 === e.nodeType && (mt.cleanData(w(e, !1)), e.innerHTML = t);
                            e = 0
                        } catch (t) { }
                    }
                    e && this.empty().append(t)
                }, null, t, arguments.length)
            },
            replaceWith: function () {
                var t = [];
                return j(this, arguments, function (e) {
                    var n = this.parentNode;
                    mt.inArray(this, t) < 0 && (mt.cleanData(w(this)), n && n.replaceChild(e, this))
                }, t)
            }
        }), mt.each({ appendTo: "append", prependTo: "prepend", insertBefore: "before", insertAfter: "after", replaceAll: "replaceWith" }, function (t, e) { mt.fn[t] = function (t) { for (var n, r = [], o = mt(t), i = o.length - 1, a = 0; a <= i; a++) n = a === i ? this : this.clone(!0), mt(o[a])[e](n), ct.apply(r, n.get()); return this.pushStack(r) } });
        var ce = /^margin/,
            le = new RegExp("^(" + Wt + ")(?!px)[a-z%]+$", "i"),
            fe = function (t) { var e = t.ownerDocument.defaultView; return e && e.opener || (e = n), e.getComputedStyle(t) };
        ! function () {
            function t() {
                if (u) {
                    u.style.cssText = "box-sizing:border-box;position:relative;display:block;margin:auto;border:1px;padding:1px;top:1%;width:50%", u.innerHTML = "", te.appendChild(a);
                    var t = n.getComputedStyle(u);
                    e = "1%" !== t.top, i = "2px" === t.marginLeft, r = "4px" === t.width, u.style.marginRight = "50%", o = "4px" === t.marginRight, te.removeChild(a), u = null
                }
            }
            var e, r, o, i, a = it.createElement("div"),
                u = it.createElement("div");
            u.style && (u.style.backgroundClip = "content-box", u.cloneNode(!0).style.backgroundClip = "", gt.clearCloneStyle = "content-box" === u.style.backgroundClip, a.style.cssText = "border:0;width:8px;height:0;top:0;left:-9999px;padding:0;margin-top:1px;position:absolute", a.appendChild(u), mt.extend(gt, { pixelPosition: function () { return t(), e }, boxSizingReliable: function () { return t(), r }, pixelMarginRight: function () { return t(), o }, reliableMarginLeft: function () { return t(), i } }))
        }();
        var pe = /^(none|table(?!-c[ea]).+)/,
            he = { position: "absolute", visibility: "hidden", display: "block" },
            de = { letterSpacing: "0", fontWeight: "400" },
            ve = ["Webkit", "Moz", "ms"],
            ge = it.createElement("div").style;
        mt.extend({
            cssHooks: { opacity: { get: function (t, e) { if (e) { var n = I(t, "opacity"); return "" === n ? "1" : n } } } },
            cssNumber: { animationIterationCount: !0, columnCount: !0, fillOpacity: !0, flexGrow: !0, flexShrink: !0, fontWeight: !0, lineHeight: !0, opacity: !0, order: !0, orphans: !0, widows: !0, zIndex: !0, zoom: !0 },
            cssProps: { float: "cssFloat" },
            style: function (t, e, n, r) {
                if (t && 3 !== t.nodeType && 8 !== t.nodeType && t.style) {
                    var o, i, a, u = mt.camelCase(e),
                        s = t.style;
                    return e = mt.cssProps[u] || (mt.cssProps[u] = L(u) || u), a = mt.cssHooks[e] || mt.cssHooks[u], void 0 === n ? a && "get" in a && void 0 !== (o = a.get(t, !1, r)) ? o : s[e] : (i = typeof n, "string" === i && (o = zt.exec(n)) && o[1] && (n = m(t, e, o), i = "number"), null != n && n === n && ("number" === i && (n += o && o[3] || (mt.cssNumber[u] ? "" : "px")), gt.clearCloneStyle || "" !== n || 0 !== e.indexOf("background") || (s[e] = "inherit"), a && "set" in a && void 0 === (n = a.set(t, n, r)) || (s[e] = n)), void 0)
                }
            },
            css: function (t, e, n, r) { var o, i, a, u = mt.camelCase(e); return e = mt.cssProps[u] || (mt.cssProps[u] = L(u) || u), a = mt.cssHooks[e] || mt.cssHooks[u], a && "get" in a && (o = a.get(t, !0, n)), void 0 === o && (o = I(t, e, r)), "normal" === o && e in de && (o = de[e]), "" === n || n ? (i = parseFloat(o), n === !0 || isFinite(i) ? i || 0 : o) : o }
        }), mt.each(["height", "width"], function (t, e) {
            mt.cssHooks[e] = {
                get: function (t, n, r) { if (n) return !pe.test(mt.css(t, "display")) || t.getClientRects().length && t.getBoundingClientRect().width ? q(t, e, r) : $t(t, he, function () { return q(t, e, r) }) },
                set: function (t, n, r) {
                    var o, i = r && fe(t),
                        a = r && U(t, e, r, "border-box" === mt.css(t, "boxSizing", !1, i), i);
                    return a && (o = zt.exec(n)) && "px" !== (o[3] || "px") && (t.style[e] = n, n = mt.css(t, e)), F(t, n, a)
                }
            }
        }), mt.cssHooks.marginLeft = R(gt.reliableMarginLeft, function (t, e) { if (e) return (parseFloat(I(t, "marginLeft")) || t.getBoundingClientRect().left - $t(t, { marginLeft: 0 }, function () { return t.getBoundingClientRect().left })) + "px" }), mt.each({ margin: "", padding: "", border: "Width" }, function (t, e) { mt.cssHooks[t + e] = { expand: function (n) { for (var r = 0, o = {}, i = "string" == typeof n ? n.split(" ") : [n]; r < 4; r++) o[t + Vt[r] + e] = i[r] || i[r - 2] || i[0]; return o } }, ce.test(t) || (mt.cssHooks[t + e].set = F) }), mt.fn.extend({
            css: function (t, e) {
                return Lt(this, function (t, e, n) {
                    var r, o, i = {},
                        a = 0;
                    if (mt.isArray(e)) { for (r = fe(t), o = e.length; a < o; a++) i[e[a]] = mt.css(t, e[a], !1, r); return i }
                    return void 0 !== n ? mt.style(t, e, n) : mt.css(t, e)
                }, t, e, arguments.length > 1)
            }
        }), mt.Tween = H, H.prototype = { constructor: H, init: function (t, e, n, r, o, i) { this.elem = t, this.prop = n, this.easing = o || mt.easing._default, this.options = e, this.start = this.now = this.cur(), this.end = r, this.unit = i || (mt.cssNumber[n] ? "" : "px") }, cur: function () { var t = H.propHooks[this.prop]; return t && t.get ? t.get(this) : H.propHooks._default.get(this) }, run: function (t) { var e, n = H.propHooks[this.prop]; return this.options.duration ? this.pos = e = mt.easing[this.easing](t, this.options.duration * t, 0, 1, this.options.duration) : this.pos = e = t, this.now = (this.end - this.start) * e + this.start, this.options.step && this.options.step.call(this.elem, this.now, this), n && n.set ? n.set(this) : H.propHooks._default.set(this), this } }, H.prototype.init.prototype = H.prototype, H.propHooks = { _default: { get: function (t) { var e; return 1 !== t.elem.nodeType || null != t.elem[t.prop] && null == t.elem.style[t.prop] ? t.elem[t.prop] : (e = mt.css(t.elem, t.prop, ""), e && "auto" !== e ? e : 0) }, set: function (t) { mt.fx.step[t.prop] ? mt.fx.step[t.prop](t) : 1 !== t.elem.nodeType || null == t.elem.style[mt.cssProps[t.prop]] && !mt.cssHooks[t.prop] ? t.elem[t.prop] = t.now : mt.style(t.elem, t.prop, t.now + t.unit) } } }, H.propHooks.scrollTop = H.propHooks.scrollLeft = { set: function (t) { t.elem.nodeType && t.elem.parentNode && (t.elem[t.prop] = t.now) } }, mt.easing = { linear: function (t) { return t }, swing: function (t) { return .5 - Math.cos(t * Math.PI) / 2 }, _default: "swing" }, mt.fx = H.prototype.init, mt.fx.step = {};
        var ye, me, be = /^(?:toggle|show|hide)$/,
            xe = /queueHooks$/;
        mt.Animation = mt.extend(K, { tweeners: { "*": [function (t, e) { var n = this.createTween(t, e); return m(n.elem, t, zt.exec(e), n), n }] }, tweener: function (t, e) { mt.isFunction(t) ? (e = t, t = ["*"]) : t = t.match(Dt); for (var n, r = 0, o = t.length; r < o; r++) n = t[r], K.tweeners[n] = K.tweeners[n] || [], K.tweeners[n].unshift(e) }, prefilters: [Y], prefilter: function (t, e) { e ? K.prefilters.unshift(t) : K.prefilters.push(t) } }), mt.speed = function (t, e, n) { var r = t && "object" == typeof t ? mt.extend({}, t) : { complete: n || !n && e || mt.isFunction(t) && t, duration: t, easing: n && e || e && !mt.isFunction(e) && e }; return mt.fx.off || it.hidden ? r.duration = 0 : "number" != typeof r.duration && (r.duration in mt.fx.speeds ? r.duration = mt.fx.speeds[r.duration] : r.duration = mt.fx.speeds._default), null != r.queue && r.queue !== !0 || (r.queue = "fx"), r.old = r.complete, r.complete = function () { mt.isFunction(r.old) && r.old.call(this), r.queue && mt.dequeue(this, r.queue) }, r }, mt.fn.extend({
            fadeTo: function (t, e, n, r) { return this.filter(Yt).css("opacity", 0).show().end().animate({ opacity: e }, t, n, r) },
            animate: function (t, e, n, r) {
                var o = mt.isEmptyObject(t),
                    i = mt.speed(e, n, r),
                    a = function () {
                        var e = K(this, mt.extend({}, t), i);
                        (o || Ut.get(this, "finish")) && e.stop(!0)
                    };
                return a.finish = a, o || i.queue === !1 ? this.each(a) : this.queue(i.queue, a)
            },
            stop: function (t, e, n) {
                var r = function (t) {
                    var e = t.stop;
                    delete t.stop, e(n)
                };
                return "string" != typeof t && (n = e, e = t, t = void 0), e && t !== !1 && this.queue(t || "fx", []), this.each(function () {
                    var e = !0,
                        o = null != t && t + "queueHooks",
                        i = mt.timers,
                        a = Ut.get(this);
                    if (o) a[o] && a[o].stop && r(a[o]);
                    else
                        for (o in a) a[o] && a[o].stop && xe.test(o) && r(a[o]);
                    for (o = i.length; o--;) i[o].elem !== this || null != t && i[o].queue !== t || (i[o].anim.stop(n), e = !1, i.splice(o, 1));
                    !e && n || mt.dequeue(this, t)
                })
            },
            finish: function (t) {
                return t !== !1 && (t = t || "fx"), this.each(function () {
                    var e, n = Ut.get(this),
                        r = n[t + "queue"],
                        o = n[t + "queueHooks"],
                        i = mt.timers,
                        a = r ? r.length : 0;
                    for (n.finish = !0, mt.queue(this, t, []), o && o.stop && o.stop.call(this, !0), e = i.length; e--;) i[e].elem === this && i[e].queue === t && (i[e].anim.stop(!0), i.splice(e, 1));
                    for (e = 0; e < a; e++) r[e] && r[e].finish && r[e].finish.call(this);
                    delete n.finish
                })
            }
        }), mt.each(["toggle", "show", "hide"], function (t, e) {
            var n = mt.fn[e];
            mt.fn[e] = function (t, r, o) { return null == t || "boolean" == typeof t ? n.apply(this, arguments) : this.animate(z(e, !0), t, r, o) }
        }), mt.each({ slideDown: z("show"), slideUp: z("hide"), slideToggle: z("toggle"), fadeIn: { opacity: "show" }, fadeOut: { opacity: "hide" }, fadeToggle: { opacity: "toggle" } }, function (t, e) { mt.fn[t] = function (t, n, r) { return this.animate(e, t, n, r) } }), mt.timers = [], mt.fx.tick = function () {
            var t, e = 0,
                n = mt.timers;
            for (ye = mt.now(); e < n.length; e++) t = n[e], t() || n[e] !== t || n.splice(e--, 1);
            n.length || mt.fx.stop(), ye = void 0
        }, mt.fx.timer = function (t) { mt.timers.push(t), t() ? mt.fx.start() : mt.timers.pop() }, mt.fx.interval = 13, mt.fx.start = function () { me || (me = n.requestAnimationFrame ? n.requestAnimationFrame(B) : n.setInterval(mt.fx.tick, mt.fx.interval)) }, mt.fx.stop = function () { n.cancelAnimationFrame ? n.cancelAnimationFrame(me) : n.clearInterval(me), me = null }, mt.fx.speeds = { slow: 600, fast: 200, _default: 400 }, mt.fn.delay = function (t, e) {
            return t = mt.fx ? mt.fx.speeds[t] || t : t, e = e || "fx", this.queue(e, function (e, r) {
                var o = n.setTimeout(e, t);
                r.stop = function () { n.clearTimeout(o) }
            })
        },
            function () {
                var t = it.createElement("input"),
                    e = it.createElement("select"),
                    n = e.appendChild(it.createElement("option"));
                t.type = "checkbox", gt.checkOn = "" !== t.value, gt.optSelected = n.selected, t = it.createElement("input"), t.value = "t", t.type = "radio", gt.radioValue = "t" === t.value
            }();
        var we, _e = mt.expr.attrHandle;
        mt.fn.extend({ attr: function (t, e) { return Lt(this, mt.attr, t, e, arguments.length > 1) }, removeAttr: function (t) { return this.each(function () { mt.removeAttr(this, t) }) } }), mt.extend({
            attr: function (t, e, n) { var r, o, i = t.nodeType; if (3 !== i && 8 !== i && 2 !== i) return "undefined" == typeof t.getAttribute ? mt.prop(t, e, n) : (1 === i && mt.isXMLDoc(t) || (o = mt.attrHooks[e.toLowerCase()] || (mt.expr.match.bool.test(e) ? we : void 0)), void 0 !== n ? null === n ? void mt.removeAttr(t, e) : o && "set" in o && void 0 !== (r = o.set(t, n, e)) ? r : (t.setAttribute(e, n + ""), n) : o && "get" in o && null !== (r = o.get(t, e)) ? r : (r = mt.find.attr(t, e), null == r ? void 0 : r)) },
            attrHooks: { type: { set: function (t, e) { if (!gt.radioValue && "radio" === e && mt.nodeName(t, "input")) { var n = t.value; return t.setAttribute("type", e), n && (t.value = n), e } } } },
            removeAttr: function (t, e) {
                var n, r = 0,
                    o = e && e.match(Dt);
                if (o && 1 === t.nodeType)
                    for (; n = o[r++];) t.removeAttribute(n)
            }
        }), we = { set: function (t, e, n) { return e === !1 ? mt.removeAttr(t, n) : t.setAttribute(n, n), n } }, mt.each(mt.expr.match.bool.source.match(/\w+/g), function (t, e) {
            var n = _e[e] || mt.find.attr;
            _e[e] = function (t, e, r) { var o, i, a = e.toLowerCase(); return r || (i = _e[a], _e[a] = o, o = null != n(t, e, r) ? a : null, _e[a] = i), o }
        });
        var Ce = /^(?:input|select|textarea|button)$/i,
            Ee = /^(?:a|area)$/i;
        mt.fn.extend({ prop: function (t, e) { return Lt(this, mt.prop, t, e, arguments.length > 1) }, removeProp: function (t) { return this.each(function () { delete this[mt.propFix[t] || t] }) } }), mt.extend({ prop: function (t, e, n) { var r, o, i = t.nodeType; if (3 !== i && 8 !== i && 2 !== i) return 1 === i && mt.isXMLDoc(t) || (e = mt.propFix[e] || e, o = mt.propHooks[e]), void 0 !== n ? o && "set" in o && void 0 !== (r = o.set(t, n, e)) ? r : t[e] = n : o && "get" in o && null !== (r = o.get(t, e)) ? r : t[e] }, propHooks: { tabIndex: { get: function (t) { var e = mt.find.attr(t, "tabindex"); return e ? parseInt(e, 10) : Ce.test(t.nodeName) || Ee.test(t.nodeName) && t.href ? 0 : -1 } } }, propFix: { for: "htmlFor", class: "className" } }), gt.optSelected || (mt.propHooks.selected = {
            get: function (t) { var e = t.parentNode; return e && e.parentNode && e.parentNode.selectedIndex, null },
            set: function (t) {
                var e = t.parentNode;
                e && (e.selectedIndex, e.parentNode && e.parentNode.selectedIndex)
            }
        }), mt.each(["tabIndex", "readOnly", "maxLength", "cellSpacing", "cellPadding", "rowSpan", "colSpan", "useMap", "frameBorder", "contentEditable"], function () { mt.propFix[this.toLowerCase()] = this }), mt.fn.extend({
            addClass: function (t) {
                var e, n, r, o, i, a, u, s = 0;
                if (mt.isFunction(t)) return this.each(function (e) { mt(this).addClass(t.call(this, e, G(this))) });
                if ("string" == typeof t && t)
                    for (e = t.match(Dt) || []; n = this[s++];)
                        if (o = G(n), r = 1 === n.nodeType && " " + X(o) + " ") {
                            for (a = 0; i = e[a++];) r.indexOf(" " + i + " ") < 0 && (r += i + " ");
                            u = X(r), o !== u && n.setAttribute("class", u)
                        }
                return this
            },
            removeClass: function (t) {
                var e, n, r, o, i, a, u, s = 0;
                if (mt.isFunction(t)) return this.each(function (e) { mt(this).removeClass(t.call(this, e, G(this))) });
                if (!arguments.length) return this.attr("class", "");
                if ("string" == typeof t && t)
                    for (e = t.match(Dt) || []; n = this[s++];)
                        if (o = G(n), r = 1 === n.nodeType && " " + X(o) + " ") {
                            for (a = 0; i = e[a++];)
                                for (; r.indexOf(" " + i + " ") > -1;) r = r.replace(" " + i + " ", " ");
                            u = X(r), o !== u && n.setAttribute("class", u)
                        }
                return this
            },
            toggleClass: function (t, e) {
                var n = typeof t;
                return "boolean" == typeof e && "string" === n ? e ? this.addClass(t) : this.removeClass(t) : mt.isFunction(t) ? this.each(function (n) { mt(this).toggleClass(t.call(this, n, G(this), e), e) }) : this.each(function () {
                    var e, r, o, i;
                    if ("string" === n)
                        for (r = 0, o = mt(this), i = t.match(Dt) || []; e = i[r++];) o.hasClass(e) ? o.removeClass(e) : o.addClass(e);
                    else void 0 !== t && "boolean" !== n || (e = G(this), e && Ut.set(this, "__className__", e), this.setAttribute && this.setAttribute("class", e || t === !1 ? "" : Ut.get(this, "__className__") || ""))
                })
            },
            hasClass: function (t) {
                var e, n, r = 0;
                for (e = " " + t + " "; n = this[r++];)
                    if (1 === n.nodeType && (" " + X(G(n)) + " ").indexOf(e) > -1) return !0;
                return !1
            }
        });
        var Me = /\r/g;
        mt.fn.extend({
            val: function (t) {
                var e, n, r, o = this[0]; {
                    if (arguments.length) return r = mt.isFunction(t), this.each(function (n) {
                        var o;
                        1 === this.nodeType && (o = r ? t.call(this, n, mt(this).val()) : t, null == o ? o = "" : "number" == typeof o ? o += "" : mt.isArray(o) && (o = mt.map(o, function (t) { return null == t ? "" : t + "" })), e = mt.valHooks[this.type] || mt.valHooks[this.nodeName.toLowerCase()], e && "set" in e && void 0 !== e.set(this, o, "value") || (this.value = o))
                    });
                    if (o) return e = mt.valHooks[o.type] || mt.valHooks[o.nodeName.toLowerCase()], e && "get" in e && void 0 !== (n = e.get(o, "value")) ? n : (n = o.value, "string" == typeof n ? n.replace(Me, "") : null == n ? "" : n)
                }
            }
        }), mt.extend({
            valHooks: {
                option: { get: function (t) { var e = mt.find.attr(t, "value"); return null != e ? e : X(mt.text(t)) } },
                select: {
                    get: function (t) {
                        var e, n, r, o = t.options,
                            i = t.selectedIndex,
                            a = "select-one" === t.type,
                            u = a ? null : [],
                            s = a ? i + 1 : o.length;
                        for (r = i < 0 ? s : a ? i : 0; r < s; r++)
                            if (n = o[r], (n.selected || r === i) && !n.disabled && (!n.parentNode.disabled || !mt.nodeName(n.parentNode, "optgroup"))) {
                                if (e = mt(n).val(), a) return e;
                                u.push(e)
                            }
                        return u
                    },
                    set: function (t, e) { for (var n, r, o = t.options, i = mt.makeArray(e), a = o.length; a--;) r = o[a], (r.selected = mt.inArray(mt.valHooks.option.get(r), i) > -1) && (n = !0); return n || (t.selectedIndex = -1), i }
                }
            }
        }), mt.each(["radio", "checkbox"], function () { mt.valHooks[this] = { set: function (t, e) { if (mt.isArray(e)) return t.checked = mt.inArray(mt(t).val(), e) > -1 } }, gt.checkOn || (mt.valHooks[this].get = function (t) { return null === t.getAttribute("value") ? "on" : t.value }) });
        var ke = /^(?:focusinfocus|focusoutblur)$/;
        mt.extend(mt.event, {
            trigger: function (t, e, r, o) {
                var i, a, u, s, c, l, f, p = [r || it],
                    h = ht.call(t, "type") ? t.type : t,
                    d = ht.call(t, "namespace") ? t.namespace.split(".") : [];
                if (a = u = r = r || it, 3 !== r.nodeType && 8 !== r.nodeType && !ke.test(h + mt.event.triggered) && (h.indexOf(".") > -1 && (d = h.split("."), h = d.shift(), d.sort()), c = h.indexOf(":") < 0 && "on" + h, t = t[mt.expando] ? t : new mt.Event(h, "object" == typeof t && t), t.isTrigger = o ? 2 : 3, t.namespace = d.join("."), t.rnamespace = t.namespace ? new RegExp("(^|\\.)" + d.join("\\.(?:.*\\.|)") + "(\\.|$)") : null, t.result = void 0, t.target || (t.target = r), e = null == e ? [t] : mt.makeArray(e, [t]), f = mt.event.special[h] || {}, o || !f.trigger || f.trigger.apply(r, e) !== !1)) {
                    if (!o && !f.noBubble && !mt.isWindow(r)) {
                        for (s = f.delegateType || h, ke.test(s + h) || (a = a.parentNode); a; a = a.parentNode) p.push(a), u = a;
                        u === (r.ownerDocument || it) && p.push(u.defaultView || u.parentWindow || n)
                    }
                    for (i = 0;
                        (a = p[i++]) && !t.isPropagationStopped();) t.type = i > 1 ? s : f.bindType || h, l = (Ut.get(a, "events") || {})[t.type] && Ut.get(a, "handle"), l && l.apply(a, e), l = c && a[c], l && l.apply && Ft(a) && (t.result = l.apply(a, e), t.result === !1 && t.preventDefault());
                    return t.type = h, o || t.isDefaultPrevented() || f._default && f._default.apply(p.pop(), e) !== !1 || !Ft(r) || c && mt.isFunction(r[h]) && !mt.isWindow(r) && (u = r[c], u && (r[c] = null), mt.event.triggered = h, r[h](), mt.event.triggered = void 0, u && (r[c] = u)), t.result
                }
            },
            simulate: function (t, e, n) {
                var r = mt.extend(new mt.Event, n, { type: t, isSimulated: !0 });
                mt.event.trigger(r, null, e)
            }
        }), mt.fn.extend({ trigger: function (t, e) { return this.each(function () { mt.event.trigger(t, e, this) }) }, triggerHandler: function (t, e) { var n = this[0]; if (n) return mt.event.trigger(t, e, n, !0) } }), mt.each("blur focus focusin focusout resize scroll click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup contextmenu".split(" "), function (t, e) { mt.fn[e] = function (t, n) { return arguments.length > 0 ? this.on(e, null, t, n) : this.trigger(e) } }), mt.fn.extend({ hover: function (t, e) { return this.mouseenter(t).mouseleave(e || t) } }), gt.focusin = "onfocusin" in n, gt.focusin || mt.each({ focus: "focusin", blur: "focusout" }, function (t, e) {
            var n = function (t) { mt.event.simulate(e, t.target, mt.event.fix(t)) };
            mt.event.special[e] = {
                setup: function () {
                    var r = this.ownerDocument || this,
                        o = Ut.access(r, e);
                    o || r.addEventListener(t, n, !0), Ut.access(r, e, (o || 0) + 1)
                },
                teardown: function () {
                    var r = this.ownerDocument || this,
                        o = Ut.access(r, e) - 1;
                    o ? Ut.access(r, e, o) : (r.removeEventListener(t, n, !0), Ut.remove(r, e))
                }
            }
        });
        var Te = n.location,
            Se = mt.now(),
            Ne = /\?/;
        mt.parseXML = function (t) { var e; if (!t || "string" != typeof t) return null; try { e = (new n.DOMParser).parseFromString(t, "text/xml") } catch (t) { e = void 0 } return e && !e.getElementsByTagName("parsererror").length || mt.error("Invalid XML: " + t), e };
        var Oe = /\[\]$/,
            Ae = /\r?\n/g,
            Pe = /^(?:submit|button|image|reset|file)$/i,
            je = /^(?:input|select|textarea|keygen)/i;
        mt.param = function (t, e) {
            var n, r = [],
                o = function (t, e) {
                    var n = mt.isFunction(e) ? e() : e;
                    r[r.length] = encodeURIComponent(t) + "=" + encodeURIComponent(null == n ? "" : n)
                };
            if (mt.isArray(t) || t.jquery && !mt.isPlainObject(t)) mt.each(t, function () { o(this.name, this.value) });
            else
                for (n in t) J(n, t[n], e, o);
            return r.join("&")
        }, mt.fn.extend({ serialize: function () { return mt.param(this.serializeArray()) }, serializeArray: function () { return this.map(function () { var t = mt.prop(this, "elements"); return t ? mt.makeArray(t) : this }).filter(function () { var t = this.type; return this.name && !mt(this).is(":disabled") && je.test(this.nodeName) && !Pe.test(t) && (this.checked || !Xt.test(t)) }).map(function (t, e) { var n = mt(this).val(); return null == n ? null : mt.isArray(n) ? mt.map(n, function (t) { return { name: e.name, value: t.replace(Ae, "\r\n") } }) : { name: e.name, value: n.replace(Ae, "\r\n") } }).get() } });
        var De = /%20/g,
            Ie = /#.*$/,
            Re = /([?&])_=[^&]*/,
            Le = /^(.*?):[ \t]*([^\r\n]*)$/gm,
            Fe = /^(?:about|app|app-storage|.+-extension|file|res|widget):$/,
            Ue = /^(?:GET|HEAD)$/,
            qe = /^\/\//,
            He = {},
            Be = {},
            We = "*/".concat("*"),
            ze = it.createElement("a");
        ze.href = Te.href, mt.extend({
            active: 0,
            lastModified: {},
            etag: {},
            ajaxSettings: { url: Te.href, type: "GET", isLocal: Fe.test(Te.protocol), global: !0, processData: !0, async: !0, contentType: "application/x-www-form-urlencoded; charset=UTF-8", accepts: { "*": We, text: "text/plain", html: "text/html", xml: "application/xml, text/xml", json: "application/json, text/javascript" }, contents: { xml: /\bxml\b/, html: /\bhtml/, json: /\bjson\b/ }, responseFields: { xml: "responseXML", text: "responseText", json: "responseJSON" }, converters: { "* text": String, "text html": !0, "text json": JSON.parse, "text xml": mt.parseXML }, flatOptions: { url: !0, context: !0 } },
            ajaxSetup: function (t, e) { return e ? tt(tt(t, mt.ajaxSettings), e) : tt(mt.ajaxSettings, t) },
            ajaxPrefilter: Q(He),
            ajaxTransport: Q(Be),
            ajax: function (t, e) {
                function r(t, e, r, u) {
                    var c, p, h, x, w, _ = e;
                    l || (l = !0, s && n.clearTimeout(s), o = void 0, a = u || "", C.readyState = t > 0 ? 4 : 0, c = t >= 200 && t < 300 || 304 === t, r && (x = et(d, C, r)), x = nt(d, x, C, c), c ? (d.ifModified && (w = C.getResponseHeader("Last-Modified"), w && (mt.lastModified[i] = w), w = C.getResponseHeader("etag"), w && (mt.etag[i] = w)), 204 === t || "HEAD" === d.type ? _ = "nocontent" : 304 === t ? _ = "notmodified" : (_ = x.state, p = x.data, h = x.error, c = !h)) : (h = _, !t && _ || (_ = "error", t < 0 && (t = 0))), C.status = t, C.statusText = (e || _) + "", c ? y.resolveWith(v, [p, _, C]) : y.rejectWith(v, [C, _, h]), C.statusCode(b), b = void 0, f && g.trigger(c ? "ajaxSuccess" : "ajaxError", [C, d, c ? p : h]), m.fireWith(v, [C, _]), f && (g.trigger("ajaxComplete", [C, d]), --mt.active || mt.event.trigger("ajaxStop")))
                }
                "object" == typeof t && (e = t, t = void 0), e = e || {};
                var o, i, a, u, s, c, l, f, p, h, d = mt.ajaxSetup({}, e),
                    v = d.context || d,
                    g = d.context && (v.nodeType || v.jquery) ? mt(v) : mt.event,
                    y = mt.Deferred(),
                    m = mt.Callbacks("once memory"),
                    b = d.statusCode || {},
                    x = {},
                    w = {},
                    _ = "canceled",
                    C = {
                        readyState: 0,
                        getResponseHeader: function (t) {
                            var e;
                            if (l) {
                                if (!u)
                                    for (u = {}; e = Le.exec(a);) u[e[1].toLowerCase()] = e[2];
                                e = u[t.toLowerCase()]
                            }
                            return null == e ? null : e
                        },
                        getAllResponseHeaders: function () { return l ? a : null },
                        setRequestHeader: function (t, e) { return null == l && (t = w[t.toLowerCase()] = w[t.toLowerCase()] || t, x[t] = e), this },
                        overrideMimeType: function (t) { return null == l && (d.mimeType = t), this },
                        statusCode: function (t) {
                            var e;
                            if (t)
                                if (l) C.always(t[C.status]);
                                else
                                    for (e in t) b[e] = [b[e], t[e]];
                            return this
                        },
                        abort: function (t) { var e = t || _; return o && o.abort(e), r(0, e), this }
                    };
                if (y.promise(C), d.url = ((t || d.url || Te.href) + "").replace(qe, Te.protocol + "//"), d.type = e.method || e.type || d.method || d.type, d.dataTypes = (d.dataType || "*").toLowerCase().match(Dt) || [""], null == d.crossDomain) { c = it.createElement("a"); try { c.href = d.url, c.href = c.href, d.crossDomain = ze.protocol + "//" + ze.host != c.protocol + "//" + c.host } catch (t) { d.crossDomain = !0 } }
                if (d.data && d.processData && "string" != typeof d.data && (d.data = mt.param(d.data, d.traditional)), Z(He, d, e, C), l) return C;
                f = mt.event && d.global, f && 0 === mt.active++ && mt.event.trigger("ajaxStart"), d.type = d.type.toUpperCase(), d.hasContent = !Ue.test(d.type), i = d.url.replace(Ie, ""), d.hasContent ? d.data && d.processData && 0 === (d.contentType || "").indexOf("application/x-www-form-urlencoded") && (d.data = d.data.replace(De, "+")) : (h = d.url.slice(i.length), d.data && (i += (Ne.test(i) ? "&" : "?") + d.data, delete d.data), d.cache === !1 && (i = i.replace(Re, "$1"), h = (Ne.test(i) ? "&" : "?") + "_=" + Se++ + h), d.url = i + h), d.ifModified && (mt.lastModified[i] && C.setRequestHeader("If-Modified-Since", mt.lastModified[i]), mt.etag[i] && C.setRequestHeader("If-None-Match", mt.etag[i])), (d.data && d.hasContent && d.contentType !== !1 || e.contentType) && C.setRequestHeader("Content-Type", d.contentType), C.setRequestHeader("Accept", d.dataTypes[0] && d.accepts[d.dataTypes[0]] ? d.accepts[d.dataTypes[0]] + ("*" !== d.dataTypes[0] ? ", " + We + "; q=0.01" : "") : d.accepts["*"]);
                for (p in d.headers) C.setRequestHeader(p, d.headers[p]);
                if (d.beforeSend && (d.beforeSend.call(v, C, d) === !1 || l)) return C.abort();
                if (_ = "abort", m.add(d.complete), C.done(d.success), C.fail(d.error), o = Z(Be, d, e, C)) {
                    if (C.readyState = 1, f && g.trigger("ajaxSend", [C, d]), l) return C;
                    d.async && d.timeout > 0 && (s = n.setTimeout(function () { C.abort("timeout") }, d.timeout));
                    try { l = !1, o.send(x, r) } catch (t) {
                        if (l) throw t;
                        r(-1, t)
                    }
                } else r(-1, "No Transport");
                return C
            },
            getJSON: function (t, e, n) { return mt.get(t, e, n, "json") },
            getScript: function (t, e) { return mt.get(t, void 0, e, "script") }
        }), mt.each(["get", "post"], function (t, e) { mt[e] = function (t, n, r, o) { return mt.isFunction(n) && (o = o || r, r = n, n = void 0), mt.ajax(mt.extend({ url: t, type: e, dataType: o, data: n, success: r }, mt.isPlainObject(t) && t)) } }), mt._evalUrl = function (t) { return mt.ajax({ url: t, type: "GET", dataType: "script", cache: !0, async: !1, global: !1, throws: !0 }) }, mt.fn.extend({
            wrapAll: function (t) { var e; return this[0] && (mt.isFunction(t) && (t = t.call(this[0])), e = mt(t, this[0].ownerDocument).eq(0).clone(!0), this[0].parentNode && e.insertBefore(this[0]), e.map(function () { for (var t = this; t.firstElementChild;) t = t.firstElementChild; return t }).append(this)), this },
            wrapInner: function (t) {
                return mt.isFunction(t) ? this.each(function (e) { mt(this).wrapInner(t.call(this, e)) }) : this.each(function () {
                    var e = mt(this),
                        n = e.contents();
                    n.length ? n.wrapAll(t) : e.append(t)
                })
            },
            wrap: function (t) { var e = mt.isFunction(t); return this.each(function (n) { mt(this).wrapAll(e ? t.call(this, n) : t) }) },
            unwrap: function (t) { return this.parent(t).not("body").each(function () { mt(this).replaceWith(this.childNodes) }), this }
        }), mt.expr.pseudos.hidden = function (t) { return !mt.expr.pseudos.visible(t) }, mt.expr.pseudos.visible = function (t) { return !!(t.offsetWidth || t.offsetHeight || t.getClientRects().length) }, mt.ajaxSettings.xhr = function () { try { return new n.XMLHttpRequest } catch (t) { } };
        var Ve = { 0: 200, 1223: 204 },
            Ye = mt.ajaxSettings.xhr();
        gt.cors = !!Ye && "withCredentials" in Ye, gt.ajax = Ye = !!Ye, mt.ajaxTransport(function (t) {
            var e, r;
            if (gt.cors || Ye && !t.crossDomain) return {
                send: function (o, i) {
                    var a, u = t.xhr();
                    if (u.open(t.type, t.url, t.async, t.username, t.password), t.xhrFields)
                        for (a in t.xhrFields) u[a] = t.xhrFields[a];
                    t.mimeType && u.overrideMimeType && u.overrideMimeType(t.mimeType), t.crossDomain || o["X-Requested-With"] || (o["X-Requested-With"] = "XMLHttpRequest");
                    for (a in o) u.setRequestHeader(a, o[a]);
                    e = function (t) { return function () { e && (e = r = u.onload = u.onerror = u.onabort = u.onreadystatechange = null, "abort" === t ? u.abort() : "error" === t ? "number" != typeof u.status ? i(0, "error") : i(u.status, u.statusText) : i(Ve[u.status] || u.status, u.statusText, "text" !== (u.responseType || "text") || "string" != typeof u.responseText ? { binary: u.response } : { text: u.responseText }, u.getAllResponseHeaders())) } }, u.onload = e(), r = u.onerror = e("error"), void 0 !== u.onabort ? u.onabort = r : u.onreadystatechange = function () { 4 === u.readyState && n.setTimeout(function () { e && r() }) }, e = e("abort");
                    try { u.send(t.hasContent && t.data || null) } catch (t) { if (e) throw t }
                },
                abort: function () { e && e() }
            }
        }), mt.ajaxPrefilter(function (t) { t.crossDomain && (t.contents.script = !1) }), mt.ajaxSetup({ accepts: { script: "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript" }, contents: { script: /\b(?:java|ecma)script\b/ }, converters: { "text script": function (t) { return mt.globalEval(t), t } } }), mt.ajaxPrefilter("script", function (t) { void 0 === t.cache && (t.cache = !1), t.crossDomain && (t.type = "GET") }), mt.ajaxTransport("script", function (t) { if (t.crossDomain) { var e, n; return { send: function (r, o) { e = mt("<script>").prop({ charset: t.scriptCharset, src: t.url }).on("load error", n = function (t) { e.remove(), n = null, t && o("error" === t.type ? 404 : 200, t.type) }), it.head.appendChild(e[0]) }, abort: function () { n && n() } } } });
        var $e = [],
            Ke = /(=)\?(?=&|$)|\?\?/;
        mt.ajaxSetup({ jsonp: "callback", jsonpCallback: function () { var t = $e.pop() || mt.expando + "_" + Se++; return this[t] = !0, t } }), mt.ajaxPrefilter("json jsonp", function (t, e, r) { var o, i, a, u = t.jsonp !== !1 && (Ke.test(t.url) ? "url" : "string" == typeof t.data && 0 === (t.contentType || "").indexOf("application/x-www-form-urlencoded") && Ke.test(t.data) && "data"); if (u || "jsonp" === t.dataTypes[0]) return o = t.jsonpCallback = mt.isFunction(t.jsonpCallback) ? t.jsonpCallback() : t.jsonpCallback, u ? t[u] = t[u].replace(Ke, "$1" + o) : t.jsonp !== !1 && (t.url += (Ne.test(t.url) ? "&" : "?") + t.jsonp + "=" + o), t.converters["script json"] = function () { return a || mt.error(o + " was not called"), a[0] }, t.dataTypes[0] = "json", i = n[o], n[o] = function () { a = arguments }, r.always(function () { void 0 === i ? mt(n).removeProp(o) : n[o] = i, t[o] && (t.jsonpCallback = e.jsonpCallback, $e.push(o)), a && mt.isFunction(i) && i(a[0]), a = i = void 0 }), "script" }), gt.createHTMLDocument = function () { var t = it.implementation.createHTMLDocument("").body; return t.innerHTML = "<form></form><form></form>", 2 === t.childNodes.length }(), mt.parseHTML = function (t, e, n) { if ("string" != typeof t) return []; "boolean" == typeof e && (n = e, e = !1); var r, o, i; return e || (gt.createHTMLDocument ? (e = it.implementation.createHTMLDocument(""), r = e.createElement("base"), r.href = it.location.href, e.head.appendChild(r)) : e = it), o = Tt.exec(t), i = !n && [], o ? [e.createElement(o[1])] : (o = C([t], e, i), i && i.length && mt(i).remove(), mt.merge([], o.childNodes)) }, mt.fn.load = function (t, e, n) {
            var r, o, i, a = this,
                u = t.indexOf(" ");
            return u > -1 && (r = X(t.slice(u)), t = t.slice(0, u)), mt.isFunction(e) ? (n = e, e = void 0) : e && "object" == typeof e && (o = "POST"), a.length > 0 && mt.ajax({ url: t, type: o || "GET", dataType: "html", data: e }).done(function (t) { i = arguments, a.html(r ? mt("<div>").append(mt.parseHTML(t)).find(r) : t) }).always(n && function (t, e) { a.each(function () { n.apply(this, i || [t.responseText, e, t]) }) }), this
        }, mt.each(["ajaxStart", "ajaxStop", "ajaxComplete", "ajaxError", "ajaxSuccess", "ajaxSend"], function (t, e) { mt.fn[e] = function (t) { return this.on(e, t) } }), mt.expr.pseudos.animated = function (t) { return mt.grep(mt.timers, function (e) { return t === e.elem }).length }, mt.offset = {
            setOffset: function (t, e, n) {
                var r, o, i, a, u, s, c, l = mt.css(t, "position"),
                    f = mt(t),
                    p = {};
                "static" === l && (t.style.position = "relative"), u = f.offset(), i = mt.css(t, "top"), s = mt.css(t, "left"), c = ("absolute" === l || "fixed" === l) && (i + s).indexOf("auto") > -1, c ? (r = f.position(), a = r.top, o = r.left) : (a = parseFloat(i) || 0, o = parseFloat(s) || 0), mt.isFunction(e) && (e = e.call(t, n, mt.extend({}, u))), null != e.top && (p.top = e.top - u.top + a), null != e.left && (p.left = e.left - u.left + o), "using" in e ? e.using.call(t, p) : f.css(p)
            }
        }, mt.fn.extend({
            offset: function (t) { if (arguments.length) return void 0 === t ? this : this.each(function (e) { mt.offset.setOffset(this, t, e) }); var e, n, r, o, i = this[0]; if (i) return i.getClientRects().length ? (r = i.getBoundingClientRect(), r.width || r.height ? (o = i.ownerDocument, n = rt(o), e = o.documentElement, { top: r.top + n.pageYOffset - e.clientTop, left: r.left + n.pageXOffset - e.clientLeft }) : r) : { top: 0, left: 0 } },
            position: function () {
                if (this[0]) {
                    var t, e, n = this[0],
                        r = { top: 0, left: 0 };
                    return "fixed" === mt.css(n, "position") ? e = n.getBoundingClientRect() : (t = this.offsetParent(), e = this.offset(), mt.nodeName(t[0], "html") || (r = t.offset()), r = { top: r.top + mt.css(t[0], "borderTopWidth", !0), left: r.left + mt.css(t[0], "borderLeftWidth", !0) }), { top: e.top - r.top - mt.css(n, "marginTop", !0), left: e.left - r.left - mt.css(n, "marginLeft", !0) }
                }
            },
            offsetParent: function () { return this.map(function () { for (var t = this.offsetParent; t && "static" === mt.css(t, "position");) t = t.offsetParent; return t || te }) }
        }), mt.each({ scrollLeft: "pageXOffset", scrollTop: "pageYOffset" }, function (t, e) {
            var n = "pageYOffset" === e;
            mt.fn[t] = function (r) { return Lt(this, function (t, r, o) { var i = rt(t); return void 0 === o ? i ? i[e] : t[r] : void (i ? i.scrollTo(n ? i.pageXOffset : o, n ? o : i.pageYOffset) : t[r] = o) }, t, r, arguments.length) }
        }), mt.each(["top", "left"], function (t, e) { mt.cssHooks[e] = R(gt.pixelPosition, function (t, n) { if (n) return n = I(t, e), le.test(n) ? mt(t).position()[e] + "px" : n }) }), mt.each({ Height: "height", Width: "width" }, function (t, e) {
            mt.each({ padding: "inner" + t, content: e, "": "outer" + t }, function (n, r) {
                mt.fn[r] = function (o, i) {
                    var a = arguments.length && (n || "boolean" != typeof o),
                        u = n || (o === !0 || i === !0 ? "margin" : "border");
                    return Lt(this, function (e, n, o) { var i; return mt.isWindow(e) ? 0 === r.indexOf("outer") ? e["inner" + t] : e.document.documentElement["client" + t] : 9 === e.nodeType ? (i = e.documentElement, Math.max(e.body["scroll" + t], i["scroll" + t], e.body["offset" + t], i["offset" + t], i["client" + t])) : void 0 === o ? mt.css(e, n, u) : mt.style(e, n, o, u) }, e, a ? o : void 0, a)
                }
            })
        }), mt.fn.extend({ bind: function (t, e, n) { return this.on(t, null, e, n) }, unbind: function (t, e) { return this.off(t, null, e) }, delegate: function (t, e, n, r) { return this.on(e, t, n, r) }, undelegate: function (t, e, n) { return 1 === arguments.length ? this.off(t, "**") : this.off(e, t || "**", n) } }), mt.parseJSON = JSON.parse, r = [], o = function () { return mt }.apply(e, r), !(void 0 !== o && (t.exports = o));
        var Xe = n.jQuery,
            Ge = n.$;
        return mt.noConflict = function (t) { return n.$ === mt && (n.$ = Ge), t && n.jQuery === mt && (n.jQuery = Xe), mt }, i || (n.jQuery = n.$ = mt), mt
    })
},
function (t, e, n) {
    "use strict";

    function r(t) { return t && t.__esModule ? t : { default: t } }

    function o(t, e, n) {
        if (t) {
            e(t);
            var r = n(t);
            if (r)
                for (var i = r.length, a = 0; a < i; a++) o(r[a], e, n)
        }
    }

    function i(t, e) { var n = null; return o(t, function (t) { t.name === e && (n = t) }, function (t) { return t.children }), n }

    function a(t) {
        var e = arguments.length <= 1 || void 0 === arguments[1] ? {} : arguments[1],
            n = arguments.length <= 2 || void 0 === arguments[2] ? { name: e.key || "state", children: [] } : arguments[2];
        if (!(0, l.default)(t) && t && !t.toJS) return {};
        var r = e.key,
            o = void 0 === r ? "state" : r,
            u = e.pushMethod,
            c = void 0 === u ? "push" : u,
            f = i(n, o);
        return null === f ? {} : ((0, p.default)(t && t.toJS ? t.toJS() : t, function (t, e) {
            var r = t && t.toJS ? t.toJS() : t,
                o = { name: e };
            if ((0, s.default)(r)) {
                o.children = [];
                for (var i = 0; i < r.length; i++) {
                    var u;
                    o.children[c]((u = { name: e + "[" + i + "]" }, u[(0, l.default)(r[i]) ? "object" : "value"] = r[i], u))
                }
            } else (0, l.default)(r) ? o.children = [] : o.value = r;
            f.children[c](o), a(r, { key: e, pushMethod: c }, n)
        }), n)
    }
    e.__esModule = !0, e.default = a;
    var u = n(12),
        s = r(u),
        c = n(202),
        l = r(c),
        f = n(206),
        p = r(f)
},
function (t, e, n) {
    function r() { }
    var o = n(31),
        i = Object.prototype;
    r.prototype = o ? o(null) : i, t.exports = r
},
function (t, e, n) {
    function r(t) {
        var e = -1,
            n = t ? t.length : 0;
        for (this.clear(); ++e < n;) {
            var r = t[e];
            this.set(r[0], r[1])
        }
    }
    var o = n(183),
        i = n(184),
        a = n(185),
        u = n(186),
        s = n(187);
    r.prototype.clear = o, r.prototype.delete = i, r.prototype.get = a, r.prototype.has = u, r.prototype.set = s, t.exports = r
},
function (t, e, n) {
    var r = n(40),
        o = n(32),
        i = r(o, "Set");
    t.exports = i
},
function (t, e, n) {
    var r = n(32),
        o = r.Uint8Array;
    t.exports = o
},
function (t, e) {
    function n(t, e) { for (var n = -1, r = t.length, o = Array(r); ++n < r;) o[n] = e(t[n], n, t); return o }
    t.exports = n
},
function (t, e) {
    function n(t, e) {
        for (var n = -1, r = t.length; ++n < r;)
            if (e(t[n], n, t)) return !0;
        return !1
    }
    t.exports = n
},
function (t, e, n) {
    var r = n(169),
        o = r();
    t.exports = o
},
function (t, e, n) {
    function r(t, e) { return t && o(t, e, i) }
    var o = n(155),
        i = n(44);
    t.exports = r
},
function (t, e) {
    function n(t, e) { return e in Object(t) }
    t.exports = n
},
function (t, e, n) {
    function r(t, e, n, r, g, m) {
        var b = c(t),
            x = c(e),
            w = d,
            _ = d;
        b || (w = s(t), w == h ? w = v : w != v && (b = f(t))), x || (_ = s(e), _ == h ? _ = v : _ != v && (x = f(e)));
        var C = w == v && !l(t),
            E = _ == v && !l(e),
            M = w == _;
        if (M && !b && !C) return a(t, e, w, n, r, g);
        var k = g & p;
        if (!k) {
            var T = C && y.call(t, "__wrapped__"),
                S = E && y.call(e, "__wrapped__");
            if (T || S) return n(T ? t.value() : t, S ? e.value() : e, r, g, m)
        }
        return !!M && (m || (m = new o), (b ? i : u)(t, e, n, r, g, m))
    }
    var o = n(70),
        i = n(170),
        a = n(171),
        u = n(172),
        s = n(175),
        c = n(12),
        l = n(41),
        f = n(204),
        p = 2,
        h = "[object Arguments]",
        d = "[object Array]",
        v = "[object Object]",
        g = Object.prototype,
        y = g.hasOwnProperty;
    t.exports = r
},
function (t, e, n) {
    function r(t, e, n, r) {
        var s = n.length,
            c = s,
            l = !r;
        if (null == t) return !c;
        for (t = Object(t); s--;) { var f = n[s]; if (l && f[2] ? f[1] !== t[f[0]] : !(f[0] in t)) return !1 }
        for (; ++s < c;) {
            f = n[s];
            var p = f[0],
                h = t[p],
                d = f[1];
            if (l && f[2]) { if (void 0 === h && !(p in t)) return !1 } else {
                var v = new o,
                    g = r ? r(h, d, p, t, e, v) : void 0;
                if (!(void 0 === g ? i(d, h, r, a | u, v) : g)) return !1
            }
        }
        return !0
    }
    var o = n(70),
        i = n(78),
        a = 1,
        u = 2;
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = typeof t; return "function" == e ? t : null == t ? a : "object" == e ? u(t) ? i(t[0], t[1]) : o(t) : s(t) }
    var o = n(162),
        i = n(163),
        a = n(199),
        u = n(12),
        s = n(207);
    t.exports = r
},
function (t, e) {
    function n(t) { return r(Object(t)) }
    var r = Object.keys;
    t.exports = n
},
function (t, e, n) {
    function r(t) {
        var e = i(t);
        if (1 == e.length && e[0][2]) {
            var n = e[0][0],
                r = e[0][1];
            return function (t) { return null != t && (t[n] === r && (void 0 !== r || n in Object(t))) }
        }
        return function (n) { return n === t || o(n, t, e) }
    }
    var o = n(159),
        i = n(174);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { return function (n) { var r = i(n, t); return void 0 === r && r === e ? a(n, t) : o(e, r, void 0, u | s) } }
    var o = n(78),
        i = n(83),
        a = n(198),
        u = 1,
        s = 2;
    t.exports = r
},
function (t, e, n) {
    function r(t) { return function (e) { return o(e, t) } }
    var o = n(76);
    t.exports = r
},
function (t, e) {
    function n(t, e, n) {
        var r = -1,
            o = t.length;
        e < 0 && (e = -e > o ? 0 : o + e), n = n > o ? o : n, n < 0 && (n += o), o = e > n ? 0 : n - e >>> 0, e >>>= 0;
        for (var i = Array(o); ++r < o;) i[r] = t[r + e];
        return i
    }
    t.exports = n
},
function (t, e) {
    function n(t, e) { for (var n = -1, r = Array(t); ++n < t;) r[n] = e(n); return r }
    t.exports = n
},
function (t, e, n) {
    function r(t, e) { return o(e, function (e) { return [e, t[e]] }) }
    var o = n(153);
    t.exports = r
},
function (t, e) {
    function n(t) { return t && t.Object === Object ? t : null }
    t.exports = n
},
function (t, e) {
    function n(t) { return function (e, n, r) { for (var o = -1, i = Object(e), a = r(e), u = a.length; u--;) { var s = a[t ? u : ++o]; if (n(i[s], s, i) === !1) break } return e } }
    t.exports = n
},
function (t, e, n) {
    function r(t, e, n, r, u, s) {
        var c = -1,
            l = u & a,
            f = u & i,
            p = t.length,
            h = e.length;
        if (p != h && !(l && h > p)) return !1;
        var d = s.get(t);
        if (d) return d == e;
        var v = !0;
        for (s.set(t, e); ++c < p;) {
            var g = t[c],
                y = e[c];
            if (r) var m = l ? r(y, g, c, e, t, s) : r(g, y, c, t, e, s);
            if (void 0 !== m) {
                if (m) continue;
                v = !1;
                break
            }
            if (f) { if (!o(e, function (t) { return g === t || n(g, t, r, u, s) })) { v = !1; break } } else if (g !== y && !n(g, y, r, u, s)) { v = !1; break }
        }
        return s.delete(t), v
    }
    var o = n(154),
        i = 1,
        a = 2;
    t.exports = r
},
function (t, e, n) {
    function r(t, e, n, r, x, _) {
        switch (n) {
            case b:
                return !(t.byteLength != e.byteLength || !r(new i(t), new i(e)));
            case l:
            case f:
                return +t == +e;
            case p:
                return t.name == e.name && t.message == e.message;
            case d:
                return t != +t ? e != +e : t == +e;
            case v:
            case y:
                return t == e + "";
            case h:
                var C = a;
            case g:
                var E = _ & c;
                return C || (C = u), (E || t.size == e.size) && r(C(t), C(e), x, _ | s);
            case m:
                return !!o && w.call(t) == w.call(e)
        }
        return !1
    }
    var o = n(71),
        i = n(152),
        a = n(188),
        u = n(190),
        s = 1,
        c = 2,
        l = "[object Boolean]",
        f = "[object Date]",
        p = "[object Error]",
        h = "[object Map]",
        d = "[object Number]",
        v = "[object RegExp]",
        g = "[object Set]",
        y = "[object String]",
        m = "[object Symbol]",
        b = "[object ArrayBuffer]",
        x = o ? o.prototype : void 0,
        w = o ? x.valueOf : void 0;
    t.exports = r
},
function (t, e, n) {
    function r(t, e, n, r, u, s) {
        var c = u & a,
            l = i(t),
            f = l.length,
            p = i(e),
            h = p.length;
        if (f != h && !c) return !1;
        for (var d = f; d--;) { var v = l[d]; if (!(c ? v in e : o(e, v))) return !1 }
        var g = s.get(t);
        if (g) return g == e;
        var y = !0;
        s.set(t, e);
        for (var m = c; ++d < f;) {
            v = l[d];
            var b = t[v],
                x = e[v];
            if (r) var w = c ? r(x, b, v, e, t, s) : r(b, x, v, t, e, s);
            if (!(void 0 === w ? b === x || n(b, x, r, u, s) : w)) { y = !1; break }
            m || (m = "constructor" == v)
        }
        if (y && !m) {
            var _ = t.constructor,
                C = e.constructor;
            _ != C && "constructor" in t && "constructor" in e && !("function" == typeof _ && _ instanceof _ && "function" == typeof C && C instanceof C) && (y = !1)
        }
        return s.delete(t), y
    }
    var o = n(77),
        i = n(44),
        a = 2;
    t.exports = r
},
function (t, e, n) {
    var r = n(79),
        o = r("length");
    t.exports = o
},
function (t, e, n) {
    function r(t) { for (var e = i(t), n = e.length; n--;) e[n][2] = o(e[n][1]); return e }
    var o = n(182),
        i = n(208);
    t.exports = r
},
function (t, e, n) {
    function r(t) { return f.call(t) }
    var o = n(15),
        i = n(151),
        a = "[object Map]",
        u = "[object Object]",
        s = "[object Set]",
        c = Object.prototype,
        l = Function.prototype.toString,
        f = c.toString,
        p = o ? l.call(o) : "",
        h = i ? l.call(i) : "";
    (o && r(new o) != a || i && r(new i) != s) && (r = function (t) {
        var e = f.call(t),
            n = e == u ? t.constructor : null,
            r = "function" == typeof n ? l.call(n) : "";
        if (r) { if (r == p) return a; if (r == h) return s }
        return e
    }), t.exports = r
},
function (t, e, n) {
    function r(t, e, n) {
        if (null == t) return !1;
        var r = n(t, e);
        r || s(e) || (e = o(e), t = p(t, e), null != t && (e = f(e), r = n(t, e)));
        var h = t ? t.length : void 0;
        return r || !!h && c(h) && u(e, h) && (a(t) || l(t) || i(t))
    }
    var o = n(80),
        i = n(84),
        a = n(12),
        u = n(82),
        s = n(42),
        c = n(33),
        l = n(87),
        f = n(205),
        p = n(189);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { return o(t, e) && delete t[e] }
    var o = n(81);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { if (o) { var n = t[e]; return n === i ? void 0 : n } return u.call(t, e) ? t[e] : void 0 }
    var o = n(31),
        i = "__lodash_hash_undefined__",
        a = Object.prototype,
        u = a.hasOwnProperty;
    t.exports = r
},
function (t, e, n) {
    function r(t, e, n) { t[e] = o && void 0 === n ? i : n }
    var o = n(31),
        i = "__lodash_hash_undefined__";
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = t ? t.length : void 0; return u(e) && (a(t) || s(t) || i(t)) ? o(e, String) : null }
    var o = n(166),
        i = n(84),
        a = n(12),
        u = n(33),
        s = n(87);
    t.exports = r
},
function (t, e) {
    function n(t) {
        var e = t && t.constructor,
            n = "function" == typeof e && e.prototype || r;
        return t === n
    }
    var r = Object.prototype;
    t.exports = n
},
function (t, e, n) {
    function r(t) { return t === t && !o(t) }
    var o = n(43);
    t.exports = r
},
function (t, e, n) {
    function r() { this.__data__ = { hash: new o, map: i ? new i : [], string: new o } }
    var o = n(149),
        i = n(15);
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = this.__data__; return u(t) ? a("string" == typeof t ? e.string : e.hash, t) : o ? e.map.delete(t) : i(e.map, t) }
    var o = n(15),
        i = n(72),
        a = n(177),
        u = n(30);
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = this.__data__; return u(t) ? a("string" == typeof t ? e.string : e.hash, t) : o ? e.map.get(t) : i(e.map, t) }
    var o = n(15),
        i = n(73),
        a = n(178),
        u = n(30);
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = this.__data__; return u(t) ? a("string" == typeof t ? e.string : e.hash, t) : o ? e.map.has(t) : i(e.map, t) }
    var o = n(15),
        i = n(74),
        a = n(81),
        u = n(30);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) { var n = this.__data__; return u(t) ? a("string" == typeof t ? n.string : n.hash, t, e) : o ? n.map.set(t, e) : i(n.map, t, e), this }
    var o = n(15),
        i = n(75),
        a = n(179),
        u = n(30);
    t.exports = r
},
function (t, e) {
    function n(t) {
        var e = -1,
            n = Array(t.size);
        return t.forEach(function (t, r) { n[++e] = [r, t] }), n
    }
    t.exports = n
},
function (t, e, n) {
    function r(t, e) { return 1 == e.length ? t : i(t, o(e, 0, -1)) }
    var o = n(165),
        i = n(83);
    t.exports = r
},
function (t, e) {
    function n(t) {
        var e = -1,
            n = Array(t.size);
        return t.forEach(function (t) { n[++e] = t }), n
    }
    t.exports = n
},
function (t, e) {
    function n() { this.__data__ = { array: [], map: null } }
    t.exports = n
},
function (t, e, n) {
    function r(t) {
        var e = this.__data__,
            n = e.array;
        return n ? o(n, t) : e.map.delete(t)
    }
    var o = n(72);
    t.exports = r
},
function (t, e, n) {
    function r(t) {
        var e = this.__data__,
            n = e.array;
        return n ? o(n, t) : e.map.get(t)
    }
    var o = n(73);
    t.exports = r
},
function (t, e, n) {
    function r(t) {
        var e = this.__data__,
            n = e.array;
        return n ? o(n, t) : e.map.has(t)
    }
    var o = n(74);
    t.exports = r
},
function (t, e, n) {
    function r(t, e) {
        var n = this.__data__,
            r = n.array;
        r && (r.length < a - 1 ? i(r, t, e) : (n.array = null, n.map = new o(r)));
        var u = n.map;
        return u && u.set(t, e), this
    }
    var o = n(150),
        i = n(75),
        a = 200;
    t.exports = r
},
function (t, e, n) {
    function r(t) { var e = []; return o(t).replace(i, function (t, n, r, o) { e.push(r ? o.replace(a, "$1") : n || t) }), e }
    var o = n(209),
        i = /[^.[\]]+|\[(?:(-?\d+(?:\.\d+)?)|(["'])((?:(?!\2)[^\\]|\\.)*?)\2)\]/g,
        a = /\\(\\)?/g;
    t.exports = r
},
function (t, e) {
    function n(t, e) { return t === e || t !== t && e !== e }
    t.exports = n
},
function (t, e, n) {
    function r(t, e) { return i(t, e, o) }
    var o = n(157),
        i = n(176);
    t.exports = r
},
function (t, e) {
    function n(t) { return t }
    t.exports = n
},
function (t, e, n) {
    function r(t) { return i(t) && o(t) }
    var o = n(85),
        i = n(13);
    t.exports = r
},
function (t, e, n) {
    function r(t) { return null != t && (o(t) ? p.test(l.call(t)) : a(t) && (i(t) ? p : s).test(t)) }
    var o = n(86),
        i = n(41),
        a = n(13),
        u = /[\\^$.*+?()[\]{}|]/g,
        s = /^\[object .+?Constructor\]$/,
        c = Object.prototype,
        l = Function.prototype.toString,
        f = c.hasOwnProperty,
        p = RegExp("^" + l.call(f).replace(u, "\\$&").replace(/hasOwnProperty|(function).*?(?=\\\()| for .+?(?=\\\])/g, "$1.*?") + "$");
    t.exports = r
},
function (t, e, n) {
    function r(t) { if (!i(t) || l.call(t) != a || o(t)) return !1; var e = u; if ("function" == typeof t.constructor && (e = f(t)), null === e) return !0; var n = e.constructor; return "function" == typeof n && n instanceof n && s.call(n) == c }
    var o = n(41),
        i = n(13),
        a = "[object Object]",
        u = Object.prototype,
        s = Function.prototype.toString,
        c = s.call(Object),
        l = u.toString,
        f = Object.getPrototypeOf;
    t.exports = r
},
function (t, e, n) {
    function r(t) { return "symbol" == typeof t || o(t) && u.call(t) == i }
    var o = n(13),
        i = "[object Symbol]",
        a = Object.prototype,
        u = a.toString;
    t.exports = r
},
function (t, e, n) {
    function r(t) { return i(t) && o(t.length) && !!N[A.call(t)] }
    var o = n(33),
        i = n(13),
        a = "[object Arguments]",
        u = "[object Array]",
        s = "[object Boolean]",
        c = "[object Date]",
        l = "[object Error]",
        f = "[object Function]",
        p = "[object Map]",
        h = "[object Number]",
        d = "[object Object]",
        v = "[object RegExp]",
        g = "[object Set]",
        y = "[object String]",
        m = "[object WeakMap]",
        b = "[object ArrayBuffer]",
        x = "[object Float32Array]",
        w = "[object Float64Array]",
        _ = "[object Int8Array]",
        C = "[object Int16Array]",
        E = "[object Int32Array]",
        M = "[object Uint8Array]",
        k = "[object Uint8ClampedArray]",
        T = "[object Uint16Array]",
        S = "[object Uint32Array]",
        N = {};
    N[x] = N[w] = N[_] = N[C] = N[E] = N[M] = N[k] = N[T] = N[S] = !0, N[a] = N[u] = N[b] = N[s] = N[c] = N[l] = N[f] = N[p] = N[h] = N[d] = N[v] = N[g] = N[y] = N[m] = !1;
    var O = Object.prototype,
        A = O.toString;
    t.exports = r
},
function (t, e) {
    function n(t) { var e = t ? t.length : 0; return e ? t[e - 1] : void 0 }
    t.exports = n
},
function (t, e, n) {
    function r(t, e) { var n = {}; return e = i(e, 3), o(t, function (t, r, o) { n[r] = e(t, r, o) }), n }
    var o = n(156),
        i = n(160);
    t.exports = r
},
function (t, e, n) {
    function r(t) { return a(t) ? o(t) : i(t) }
    var o = n(79),
        i = n(164),
        a = n(42);
    t.exports = r
},
function (t, e, n) {
    function r(t) { return o(t, i(t)) }
    var o = n(167),
        i = n(44);
    t.exports = r
},
function (t, e, n) {
    function r(t) { if ("string" == typeof t) return t; if (null == t) return ""; if (i(t)) return o ? s.call(t) : ""; var e = t + ""; return "0" == e && 1 / t == -a ? "-0" : e }
    var o = n(71),
        i = n(203),
        a = 1 / 0,
        u = o ? o.prototype : void 0,
        s = o ? u.toString : void 0;
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t) { var e = new o(o._61); return e._81 = 1, e._65 = t, e }
    var o = n(89);
    t.exports = o;
    var i = r(!0),
        a = r(!1),
        u = r(null),
        s = r(void 0),
        c = r(0),
        l = r("");
    o.resolve = function (t) {
        if (t instanceof o) return t;
        if (null === t) return u;
        if (void 0 === t) return s;
        if (t === !0) return i;
        if (t === !1) return a;
        if (0 === t) return c;
        if ("" === t) return l;
        if ("object" == typeof t || "function" == typeof t) try { var e = t.then; if ("function" == typeof e) return new o(e.bind(t)) } catch (t) { return new o(function (e, n) { n(t) }) }
        return r(t)
    }, o.all = function (t) {
        var e = Array.prototype.slice.call(t);
        return new o(function (t, n) {
            function r(a, u) {
                if (u && ("object" == typeof u || "function" == typeof u)) {
                    if (u instanceof o && u.then === o.prototype.then) { for (; 3 === u._81;) u = u._65; return 1 === u._81 ? r(a, u._65) : (2 === u._81 && n(u._65), void u.then(function (t) { r(a, t) }, n)) }
                    var s = u.then;
                    if ("function" == typeof s) {
                        var c = new o(s.bind(u));
                        return void c.then(function (t) {
                            r(a, t)
                        }, n)
                    }
                }
                e[a] = u, 0 === --i && t(e)
            }
            if (0 === e.length) return t([]);
            for (var i = e.length, a = 0; a < e.length; a++) r(a, e[a])
        })
    }, o.reject = function (t) { return new o(function (e, n) { n(t) }) }, o.race = function (t) { return new o(function (e, n) { t.forEach(function (t) { o.resolve(t).then(e, n) }) }) }, o.prototype.catch = function (t) { return this.then(null, t) }
},
function (t, e, n) {
    "use strict";

    function r() { c = !1, u._10 = null, u._97 = null }

    function o(t) {
        function e(e) {
            (t.allRejections || a(f[e].error, t.whitelist || s)) && (f[e].displayId = l++ , t.onUnhandled ? (f[e].logged = !0, t.onUnhandled(f[e].displayId, f[e].error)) : (f[e].logged = !0, i(f[e].displayId, f[e].error)))
        }

        function n(e) { f[e].logged && (t.onHandled ? t.onHandled(f[e].displayId, f[e].error) : f[e].onUnhandled || (console.warn("Promise Rejection Handled (id: " + f[e].displayId + "):"), console.warn('  This means you can ignore any previous messages of the form "Possible Unhandled Promise Rejection" with id ' + f[e].displayId + "."))) }
        t = t || {}, c && r(), c = !0;
        var o = 0,
            l = 0,
            f = {};
        u._10 = function (t) { 2 === t._81 && f[t._72] && (f[t._72].logged ? n(t._72) : clearTimeout(f[t._72].timeout), delete f[t._72]) }, u._97 = function (t, n) { 0 === t._45 && (t._72 = o++ , f[t._72] = { displayId: null, error: n, timeout: setTimeout(e.bind(null, t._72), a(n, s) ? 100 : 2e3), logged: !1 }) }
    }

    function i(t, e) {
        console.warn("Possible Unhandled Promise Rejection (id: " + t + "):");
        var n = (e && (e.stack || e)) + "";
        n.split("\n").forEach(function (t) { console.warn("  " + t) })
    }

    function a(t, e) { return e.some(function (e) { return t instanceof e }) }
    var u = n(89),
        s = [ReferenceError, TypeError, RangeError],
        c = !1;
    e.disable = r, e.enable = o
},
function (t, e) {
    "use strict";
    var n = { Properties: { "aria-current": 0, "aria-details": 0, "aria-disabled": 0, "aria-hidden": 0, "aria-invalid": 0, "aria-keyshortcuts": 0, "aria-label": 0, "aria-roledescription": 0, "aria-autocomplete": 0, "aria-checked": 0, "aria-expanded": 0, "aria-haspopup": 0, "aria-level": 0, "aria-modal": 0, "aria-multiline": 0, "aria-multiselectable": 0, "aria-orientation": 0, "aria-placeholder": 0, "aria-pressed": 0, "aria-readonly": 0, "aria-required": 0, "aria-selected": 0, "aria-sort": 0, "aria-valuemax": 0, "aria-valuemin": 0, "aria-valuenow": 0, "aria-valuetext": 0, "aria-atomic": 0, "aria-busy": 0, "aria-live": 0, "aria-relevant": 0, "aria-dropeffect": 0, "aria-grabbed": 0, "aria-activedescendant": 0, "aria-colcount": 0, "aria-colindex": 0, "aria-colspan": 0, "aria-controls": 0, "aria-describedby": 0, "aria-errormessage": 0, "aria-flowto": 0, "aria-labelledby": 0, "aria-owns": 0, "aria-posinset": 0, "aria-rowcount": 0, "aria-rowindex": 0, "aria-rowspan": 0, "aria-setsize": 0 }, DOMAttributeNames: {}, DOMPropertyNames: {} };
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = n(5),
        o = n(68),
        i = { focusDOMComponent: function () { o(r.getNodeFromInstance(this)) } };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r() { var t = window.opera; return "object" == typeof t && "function" == typeof t.version && parseInt(t.version(), 10) <= 12 }

    function o(t) { return (t.ctrlKey || t.altKey || t.metaKey) && !(t.ctrlKey && t.altKey) }

    function i(t) {
        switch (t) {
            case "topCompositionStart":
                return k.compositionStart;
            case "topCompositionEnd":
                return k.compositionEnd;
            case "topCompositionUpdate":
                return k.compositionUpdate
        }
    }

    function a(t, e) { return "topKeyDown" === t && e.keyCode === b }

    function u(t, e) {
        switch (t) {
            case "topKeyUp":
                return m.indexOf(e.keyCode) !== -1;
            case "topKeyDown":
                return e.keyCode !== b;
            case "topKeyPress":
            case "topMouseDown":
            case "topBlur":
                return !0;
            default:
                return !1
        }
    }

    function s(t) { var e = t.detail; return "object" == typeof e && "data" in e ? e.data : null }

    function c(t, e, n, r) {
        var o, c;
        if (x ? o = i(t) : S ? u(t, n) && (o = k.compositionEnd) : a(t, n) && (o = k.compositionStart), !o) return null;
        C && (S || o !== k.compositionStart ? o === k.compositionEnd && S && (c = S.getData()) : S = v.getPooled(r));
        var l = g.getPooled(o, e, n, r);
        if (c) l.data = c;
        else {
            var f = s(n);
            null !== f && (l.data = f)
        }
        return h.accumulateTwoPhaseDispatches(l), l
    }

    function l(t, e) {
        switch (t) {
            case "topCompositionEnd":
                return s(e);
            case "topKeyPress":
                var n = e.which;
                return n !== E ? null : (T = !0, M);
            case "topTextInput":
                var r = e.data;
                return r === M && T ? null : r;
            default:
                return null
        }
    }

    function f(t, e) {
        if (S) { if ("topCompositionEnd" === t || !x && u(t, e)) { var n = S.getData(); return v.release(S), S = null, n } return null }
        switch (t) {
            case "topPaste":
                return null;
            case "topKeyPress":
                return e.which && !o(e) ? String.fromCharCode(e.which) : null;
            case "topCompositionEnd":
                return C ? null : e.data;
            default:
                return null
        }
    }

    function p(t, e, n, r) { var o; if (o = _ ? l(t, n) : f(t, n), !o) return null; var i = y.getPooled(k.beforeInput, e, n, r); return i.data = o, h.accumulateTwoPhaseDispatches(i), i }
    var h = n(26),
        d = n(6),
        v = n(220),
        g = n(257),
        y = n(260),
        m = [9, 13, 27, 32],
        b = 229,
        x = d.canUseDOM && "CompositionEvent" in window,
        w = null;
    d.canUseDOM && "documentMode" in document && (w = document.documentMode);
    var _ = d.canUseDOM && "TextEvent" in window && !w && !r(),
        C = d.canUseDOM && (!x || w && w > 8 && w <= 11),
        E = 32,
        M = String.fromCharCode(E),
        k = { beforeInput: { phasedRegistrationNames: { bubbled: "onBeforeInput", captured: "onBeforeInputCapture" }, dependencies: ["topCompositionEnd", "topKeyPress", "topTextInput", "topPaste"] }, compositionEnd: { phasedRegistrationNames: { bubbled: "onCompositionEnd", captured: "onCompositionEndCapture" }, dependencies: ["topBlur", "topCompositionEnd", "topKeyDown", "topKeyPress", "topKeyUp", "topMouseDown"] }, compositionStart: { phasedRegistrationNames: { bubbled: "onCompositionStart", captured: "onCompositionStartCapture" }, dependencies: ["topBlur", "topCompositionStart", "topKeyDown", "topKeyPress", "topKeyUp", "topMouseDown"] }, compositionUpdate: { phasedRegistrationNames: { bubbled: "onCompositionUpdate", captured: "onCompositionUpdateCapture" }, dependencies: ["topBlur", "topCompositionUpdate", "topKeyDown", "topKeyPress", "topKeyUp", "topMouseDown"] } },
        T = !1,
        S = null,
        N = { eventTypes: k, extractEvents: function (t, e, n, r) { return [c(t, e, n, r), p(t, e, n, r)] } };
    t.exports = N
},
function (t, e, n) {
    "use strict";
    var r = n(90),
        o = n(6),
        i = (n(8), n(136), n(266)),
        a = n(143),
        u = n(146),
        s = (n(2), u(function (t) { return a(t) })),
        c = !1,
        l = "cssFloat";
    if (o.canUseDOM) {
        var f = document.createElement("div").style;
        try { f.font = "" } catch (t) { c = !0 }
        void 0 === document.documentElement.style.cssFloat && (l = "styleFloat")
    }
    var p = {
        createMarkupForStyles: function (t, e) {
            var n = "";
            for (var r in t)
                if (t.hasOwnProperty(r)) {
                    var o = t[r];
                    null != o && (n += s(r) + ":", n += i(r, o, e) + ";")
                }
            return n || null
        },
        setValueForStyles: function (t, e, n) {
            var o = t.style;
            for (var a in e)
                if (e.hasOwnProperty(a)) {
                    var u = i(a, e[a], n);
                    if ("float" !== a && "cssFloat" !== a || (a = l), u) o[a] = u;
                    else {
                        var s = c && r.shorthandPropertyExpansions[a];
                        if (s)
                            for (var f in s) o[f] = "";
                        else o[a] = ""
                    }
                }
        }
    };
    t.exports = p
},
function (t, e, n) {
    "use strict";

    function r(t) { var e = t.nodeName && t.nodeName.toLowerCase(); return "select" === e || "input" === e && "file" === t.type }

    function o(t) {
        var e = C.getPooled(T.change, N, t, E(t));
        b.accumulateTwoPhaseDispatches(e), _.batchedUpdates(i, e)
    }

    function i(t) { m.enqueueEvents(t), m.processEventQueue(!1) }

    function a(t, e) { S = t, N = e, S.attachEvent("onchange", o) }

    function u() { S && (S.detachEvent("onchange", o), S = null, N = null) }

    function s(t, e) { if ("topChange" === t) return e }

    function c(t, e, n) { "topFocus" === t ? (u(), a(e, n)) : "topBlur" === t && u() }

    function l(t, e) { S = t, N = e, O = t.value, A = Object.getOwnPropertyDescriptor(t.constructor.prototype, "value"), Object.defineProperty(S, "value", D), S.attachEvent ? S.attachEvent("onpropertychange", p) : S.addEventListener("propertychange", p, !1) }

    function f() { S && (delete S.value, S.detachEvent ? S.detachEvent("onpropertychange", p) : S.removeEventListener("propertychange", p, !1), S = null, N = null, O = null, A = null) }

    function p(t) {
        if ("value" === t.propertyName) {
            var e = t.srcElement.value;
            e !== O && (O = e, o(t))
        }
    }

    function h(t, e) { if ("topInput" === t) return e }

    function d(t, e, n) { "topFocus" === t ? (f(), l(e, n)) : "topBlur" === t && f() }

    function v(t, e) { if (("topSelectionChange" === t || "topKeyUp" === t || "topKeyDown" === t) && S && S.value !== O) return O = S.value, N }

    function g(t) { return t.nodeName && "input" === t.nodeName.toLowerCase() && ("checkbox" === t.type || "radio" === t.type) }

    function y(t, e) { if ("topClick" === t) return e }
    var m = n(25),
        b = n(26),
        x = n(6),
        w = n(5),
        _ = n(9),
        C = n(10),
        E = n(58),
        M = n(59),
        k = n(107),
        T = { change: { phasedRegistrationNames: { bubbled: "onChange", captured: "onChangeCapture" }, dependencies: ["topBlur", "topChange", "topClick", "topFocus", "topInput", "topKeyDown", "topKeyUp", "topSelectionChange"] } },
        S = null,
        N = null,
        O = null,
        A = null,
        P = !1;
    x.canUseDOM && (P = M("change") && (!document.documentMode || document.documentMode > 8));
    var j = !1;
    x.canUseDOM && (j = M("input") && (!document.documentMode || document.documentMode > 11));
    var D = { get: function () { return A.get.call(this) }, set: function (t) { O = "" + t, A.set.call(this, t) } },
        I = {
            eventTypes: T,
            extractEvents: function (t, e, n, o) {
                var i, a, u = e ? w.getNodeFromInstance(e) : window;
                if (r(u) ? P ? i = s : a = c : k(u) ? j ? i = h : (i = v, a = d) : g(u) && (i = y), i) { var l = i(t, e); if (l) { var f = C.getPooled(T.change, l, n, o); return f.type = "change", b.accumulateTwoPhaseDispatches(f), f } }
                a && a(t, u, e)
            }
        };
    t.exports = I
},
function (t, e, n) {
    "use strict";
    var r = n(3),
        o = n(17),
        i = n(6),
        a = n(139),
        u = n(7),
        s = (n(1), {
            dangerouslyReplaceNodeWithMarkup: function (t, e) {
                if (i.canUseDOM ? void 0 : r("56"), e ? void 0 : r("57"), "HTML" === t.nodeName ? r("58") : void 0, "string" == typeof e) {
                    var n = a(e, u)[0];
                    t.parentNode.replaceChild(n, t)
                } else o.replaceChildWithTree(t, e)
            }
        });
    t.exports = s
},
function (t, e) {
    "use strict";
    var n = ["ResponderEventPlugin", "SimpleEventPlugin", "TapEventPlugin", "EnterLeaveEventPlugin", "ChangeEventPlugin", "SelectEventPlugin", "BeforeInputEventPlugin"];
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = n(26),
        o = n(5),
        i = n(35),
        a = { mouseEnter: { registrationName: "onMouseEnter", dependencies: ["topMouseOut", "topMouseOver"] }, mouseLeave: { registrationName: "onMouseLeave", dependencies: ["topMouseOut", "topMouseOver"] } },
        u = {
            eventTypes: a,
            extractEvents: function (t, e, n, u) {
                if ("topMouseOver" === t && (n.relatedTarget || n.fromElement)) return null;
                if ("topMouseOut" !== t && "topMouseOver" !== t) return null;
                var s;
                if (u.window === u) s = u;
                else {
                    var c = u.ownerDocument;
                    s = c ? c.defaultView || c.parentWindow : window
                }
                var l, f;
                if ("topMouseOut" === t) {
                    l = e;
                    var p = n.relatedTarget || n.toElement;
                    f = p ? o.getClosestInstanceFromNode(p) : null
                } else l = null, f = e;
                if (l === f) return null;
                var h = null == l ? s : o.getNodeFromInstance(l),
                    d = null == f ? s : o.getNodeFromInstance(f),
                    v = i.getPooled(a.mouseLeave, l, n, u);
                v.type = "mouseleave", v.target = h, v.relatedTarget = d;
                var g = i.getPooled(a.mouseEnter, f, n, u);
                return g.type = "mouseenter", g.target = d, g.relatedTarget = h, r.accumulateEnterLeaveDispatches(v, g, l, f), [v, g]
            }
        };
    t.exports = u
},
function (t, e, n) {
    "use strict";

    function r(t) { this._root = t, this._startText = this.getText(), this._fallbackText = null }
    var o = n(4),
        i = n(14),
        a = n(105);
    o(r.prototype, {
        destructor: function () { this._root = null, this._startText = null, this._fallbackText = null },
        getText: function () { return "value" in this._root ? this._root.value : this._root[a()] },
        getData: function () {
            if (this._fallbackText) return this._fallbackText;
            var t, e, n = this._startText,
                r = n.length,
                o = this.getText(),
                i = o.length;
            for (t = 0; t < r && n[t] === o[t]; t++);
            var a = r - t;
            for (e = 1; e <= a && n[r - e] === o[i - e]; e++);
            var u = e > 1 ? 1 - e : void 0;
            return this._fallbackText = o.slice(t, u), this._fallbackText
        }
    }), i.addPoolingTo(r), t.exports = r
},
function (t, e, n) {
    "use strict";
    var r = n(18),
        o = r.injection.MUST_USE_PROPERTY,
        i = r.injection.HAS_BOOLEAN_VALUE,
        a = r.injection.HAS_NUMERIC_VALUE,
        u = r.injection.HAS_POSITIVE_NUMERIC_VALUE,
        s = r.injection.HAS_OVERLOADED_BOOLEAN_VALUE,
        c = { isCustomAttribute: RegExp.prototype.test.bind(new RegExp("^(data|aria)-[" + r.ATTRIBUTE_NAME_CHAR + "]*$")), Properties: { accept: 0, acceptCharset: 0, accessKey: 0, action: 0, allowFullScreen: i, allowTransparency: 0, alt: 0, as: 0, async: i, autoComplete: 0, autoPlay: i, capture: i, cellPadding: 0, cellSpacing: 0, charSet: 0, challenge: 0, checked: o | i, cite: 0, classID: 0, className: 0, cols: u, colSpan: 0, content: 0, contentEditable: 0, contextMenu: 0, controls: i, coords: 0, crossOrigin: 0, data: 0, dateTime: 0, default: i, defer: i, dir: 0, disabled: i, download: s, draggable: 0, encType: 0, form: 0, formAction: 0, formEncType: 0, formMethod: 0, formNoValidate: i, formTarget: 0, frameBorder: 0, headers: 0, height: 0, hidden: i, high: 0, href: 0, hrefLang: 0, htmlFor: 0, httpEquiv: 0, icon: 0, id: 0, inputMode: 0, integrity: 0, is: 0, keyParams: 0, keyType: 0, kind: 0, label: 0, lang: 0, list: 0, loop: i, low: 0, manifest: 0, marginHeight: 0, marginWidth: 0, max: 0, maxLength: 0, media: 0, mediaGroup: 0, method: 0, min: 0, minLength: 0, multiple: o | i, muted: o | i, name: 0, nonce: 0, noValidate: i, open: i, optimum: 0, pattern: 0, placeholder: 0, playsInline: i, poster: 0, preload: 0, profile: 0, radioGroup: 0, readOnly: i, referrerPolicy: 0, rel: 0, required: i, reversed: i, role: 0, rows: u, rowSpan: a, sandbox: 0, scope: 0, scoped: i, scrolling: 0, seamless: i, selected: o | i, shape: 0, size: u, sizes: 0, span: u, spellCheck: 0, src: 0, srcDoc: 0, srcLang: 0, srcSet: 0, start: a, step: 0, style: 0, summary: 0, tabIndex: 0, target: 0, title: 0, type: 0, useMap: 0, value: 0, width: 0, wmode: 0, wrap: 0, about: 0, datatype: 0, inlist: 0, prefix: 0, property: 0, resource: 0, typeof: 0, vocab: 0, autoCapitalize: 0, autoCorrect: 0, autoSave: 0, color: 0, itemProp: 0, itemScope: i, itemType: 0, itemID: 0, itemRef: 0, results: 0, security: 0, unselectable: 0 }, DOMAttributeNames: { acceptCharset: "accept-charset", className: "class", htmlFor: "for", httpEquiv: "http-equiv" }, DOMPropertyNames: {} };
    t.exports = c
},
function (t, e, n) {
    (function (e) {
        "use strict";

        function r(t, e, n, r) {
            var o = void 0 === t[n];
            null != e && o && (t[n] = i(e, !0))
        }
        var o = n(19),
            i = n(106),
            a = (n(50), n(60)),
            u = n(109),
            s = (n(2), {
                instantiateChildren: function (t, e, n, o) { if (null == t) return null; var i = {}; return u(t, r, i), i },
                updateChildren: function (t, e, n, r, u, s, c, l, f) {
                    if (e || t) {
                        var p, h;
                        for (p in e)
                            if (e.hasOwnProperty(p)) {
                                h = t && t[p];
                                var d = h && h._currentElement,
                                    v = e[p];
                                if (null != h && a(d, v)) o.receiveComponent(h, v, u, l), e[p] = h;
                                else {
                                    h && (r[p] = o.getHostNode(h), o.unmountComponent(h, !1));
                                    var g = i(v, !0);
                                    e[p] = g;
                                    var y = o.mountComponent(g, u, s, c, l, f);
                                    n.push(y)
                                }
                            }
                        for (p in t) !t.hasOwnProperty(p) || e && e.hasOwnProperty(p) || (h = t[p], r[p] = o.getHostNode(h), o.unmountComponent(h, !1))
                    }
                },
                unmountChildren: function (t, e) {
                    for (var n in t)
                        if (t.hasOwnProperty(n)) {
                            var r = t[n];
                            o.unmountComponent(r, e)
                        }
                }
            });
        t.exports = s
    }).call(e, n(88))
},
function (t, e, n) {
    "use strict";
    var r = n(46),
        o = n(230),
        i = { processChildrenUpdates: o.dangerouslyProcessChildrenUpdates, replaceNodeWithMarkup: r.dangerouslyReplaceNodeWithMarkup };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t) { }

    function o(t, e) { }

    function i(t) { return !(!t.prototype || !t.prototype.isReactComponent) }

    function a(t) { return !(!t.prototype || !t.prototype.isPureReactComponent) }
    var u = n(3),
        s = n(4),
        c = n(20),
        l = n(52),
        f = n(11),
        p = n(53),
        h = n(27),
        d = (n(8), n(100)),
        v = n(19),
        g = n(24),
        y = (n(1), n(39)),
        m = n(60),
        b = (n(2), { ImpureClass: 0, PureClass: 1, StatelessFunctional: 2 });
    r.prototype.render = function () {
        var t = h.get(this)._currentElement.type,
            e = t(this.props, this.context, this.updater);
        return o(t, e), e
    };
    var x = 1,
        w = {
            construct: function (t) { this._currentElement = t, this._rootNodeID = 0, this._compositeType = null, this._instance = null, this._hostParent = null, this._hostContainerInfo = null, this._updateBatchNumber = null, this._pendingElement = null, this._pendingStateQueue = null, this._pendingReplaceState = !1, this._pendingForceUpdate = !1, this._renderedNodeType = null, this._renderedComponent = null, this._context = null, this._mountOrder = 0, this._topLevelWrapper = null, this._pendingCallbacks = null, this._calledComponentWillUnmount = !1 },
            mountComponent: function (t, e, n, s) {
                this._context = s, this._mountOrder = x++ , this._hostParent = e, this._hostContainerInfo = n;
                var l, f = this._currentElement.props,
                    p = this._processContext(s),
                    d = this._currentElement.type,
                    v = t.getUpdateQueue(),
                    y = i(d),
                    m = this._constructComponent(y, f, p, v);
                y || null != m && null != m.render ? a(d) ? this._compositeType = b.PureClass : this._compositeType = b.ImpureClass : (l = m, o(d, l), null === m || m === !1 || c.isValidElement(m) ? void 0 : u("105", d.displayName || d.name || "Component"), m = new r(d), this._compositeType = b.StatelessFunctional);
                m.props = f, m.context = p, m.refs = g, m.updater = v, this._instance = m, h.set(m, this);
                var w = m.state;
                void 0 === w && (m.state = w = null), "object" != typeof w || Array.isArray(w) ? u("106", this.getName() || "ReactCompositeComponent") : void 0, this._pendingStateQueue = null, this._pendingReplaceState = !1, this._pendingForceUpdate = !1;
                var _;
                return _ = m.unstable_handleError ? this.performInitialMountWithErrorHandling(l, e, n, t, s) : this.performInitialMount(l, e, n, t, s), m.componentDidMount && t.getReactMountReady().enqueue(m.componentDidMount, m), _
            },
            _constructComponent: function (t, e, n, r) { return this._constructComponentWithoutOwner(t, e, n, r) },
            _constructComponentWithoutOwner: function (t, e, n, r) { var o = this._currentElement.type; return t ? new o(e, n, r) : o(e, n, r) },
            performInitialMountWithErrorHandling: function (t, e, n, r, o) { var i, a = r.checkpoint(); try { i = this.performInitialMount(t, e, n, r, o) } catch (u) { r.rollback(a), this._instance.unstable_handleError(u), this._pendingStateQueue && (this._instance.state = this._processPendingState(this._instance.props, this._instance.context)), a = r.checkpoint(), this._renderedComponent.unmountComponent(!0), r.rollback(a), i = this.performInitialMount(t, e, n, r, o) } return i },
            performInitialMount: function (t, e, n, r, o) {
                var i = this._instance,
                    a = 0;
                i.componentWillMount && (i.componentWillMount(), this._pendingStateQueue && (i.state = this._processPendingState(i.props, i.context))), void 0 === t && (t = this._renderValidatedComponent());
                var u = d.getType(t);
                this._renderedNodeType = u;
                var s = this._instantiateReactComponent(t, u !== d.EMPTY);
                this._renderedComponent = s;
                var c = v.mountComponent(s, r, e, n, this._processChildContext(o), a);
                return c
            },
            getHostNode: function () { return v.getHostNode(this._renderedComponent) },
            unmountComponent: function (t) {
                if (this._renderedComponent) {
                    var e = this._instance;
                    if (e.componentWillUnmount && !e._calledComponentWillUnmount)
                        if (e._calledComponentWillUnmount = !0, t) {
                            var n = this.getName() + ".componentWillUnmount()";
                            p.invokeGuardedCallback(n, e.componentWillUnmount.bind(e))
                        } else e.componentWillUnmount();
                    this._renderedComponent && (v.unmountComponent(this._renderedComponent, t), this._renderedNodeType = null, this._renderedComponent = null, this._instance = null), this._pendingStateQueue = null, this._pendingReplaceState = !1, this._pendingForceUpdate = !1, this._pendingCallbacks = null, this._pendingElement = null, this._context = null, this._rootNodeID = 0, this._topLevelWrapper = null, h.remove(e)
                }
            },
            _maskContext: function (t) {
                var e = this._currentElement.type,
                    n = e.contextTypes;
                if (!n) return g;
                var r = {};
                for (var o in n) r[o] = t[o];
                return r
            },
            _processContext: function (t) { var e = this._maskContext(t); return e },
            _processChildContext: function (t) {
                var e, n = this._currentElement.type,
                    r = this._instance;
                if (r.getChildContext && (e = r.getChildContext()), e) { "object" != typeof n.childContextTypes ? u("107", this.getName() || "ReactCompositeComponent") : void 0; for (var o in e) o in n.childContextTypes ? void 0 : u("108", this.getName() || "ReactCompositeComponent", o); return s({}, t, e) }
                return t
            },
            _checkContextTypes: function (t, e, n) { },
            receiveComponent: function (t, e, n) {
                var r = this._currentElement,
                    o = this._context;
                this._pendingElement = null, this.updateComponent(e, r, t, o, n)
            },
            performUpdateIfNecessary: function (t) { null != this._pendingElement ? v.receiveComponent(this, this._pendingElement, t, this._context) : null !== this._pendingStateQueue || this._pendingForceUpdate ? this.updateComponent(t, this._currentElement, this._currentElement, this._context, this._context) : this._updateBatchNumber = null },
            updateComponent: function (t, e, n, r, o) {
                var i = this._instance;
                null == i ? u("136", this.getName() || "ReactCompositeComponent") : void 0;
                var a, s = !1;
                this._context === o ? a = i.context : (a = this._processContext(o), s = !0);
                var c = e.props,
                    l = n.props;
                e !== n && (s = !0), s && i.componentWillReceiveProps && i.componentWillReceiveProps(l, a);
                var f = this._processPendingState(l, a),
                    p = !0;
                this._pendingForceUpdate || (i.shouldComponentUpdate ? p = i.shouldComponentUpdate(l, f, a) : this._compositeType === b.PureClass && (p = !y(c, l) || !y(i.state, f))), this._updateBatchNumber = null, p ? (this._pendingForceUpdate = !1, this._performComponentUpdate(n, l, f, a, t, o)) : (this._currentElement = n, this._context = o, i.props = l, i.state = f, i.context = a)
            },
            _processPendingState: function (t, e) {
                var n = this._instance,
                    r = this._pendingStateQueue,
                    o = this._pendingReplaceState;
                if (this._pendingReplaceState = !1, this._pendingStateQueue = null, !r) return n.state;
                if (o && 1 === r.length) return r[0];
                for (var i = s({}, o ? r[0] : n.state), a = o ? 1 : 0; a < r.length; a++) {
                    var u = r[a];
                    s(i, "function" == typeof u ? u.call(n, i, t, e) : u)
                }
                return i
            },
            _performComponentUpdate: function (t, e, n, r, o, i) {
                var a, u, s, c = this._instance,
                    l = Boolean(c.componentDidUpdate);
                l && (a = c.props, u = c.state, s = c.context), c.componentWillUpdate && c.componentWillUpdate(e, n, r), this._currentElement = t, this._context = i, c.props = e, c.state = n, c.context = r, this._updateRenderedComponent(o, i), l && o.getReactMountReady().enqueue(c.componentDidUpdate.bind(c, a, u, s), c)
            },
            _updateRenderedComponent: function (t, e) {
                var n = this._renderedComponent,
                    r = n._currentElement,
                    o = this._renderValidatedComponent(),
                    i = 0;
                if (m(r, o)) v.receiveComponent(n, o, t, this._processChildContext(e));
                else {
                    var a = v.getHostNode(n);
                    v.unmountComponent(n, !1);
                    var u = d.getType(o);
                    this._renderedNodeType = u;
                    var s = this._instantiateReactComponent(o, u !== d.EMPTY);
                    this._renderedComponent = s;
                    var c = v.mountComponent(s, t, this._hostParent, this._hostContainerInfo, this._processChildContext(e), i);
                    this._replaceNodeWithMarkup(a, c, n)
                }
            },
            _replaceNodeWithMarkup: function (t, e, n) { l.replaceNodeWithMarkup(t, e, n) },
            _renderValidatedComponentWithoutOwnerOrContext: function () { var t, e = this._instance; return t = e.render() },
            _renderValidatedComponent: function () { var t; if (this._compositeType !== b.StatelessFunctional) { f.current = this; try { t = this._renderValidatedComponentWithoutOwnerOrContext() } finally { f.current = null } } else t = this._renderValidatedComponentWithoutOwnerOrContext(); return null === t || t === !1 || c.isValidElement(t) ? void 0 : u("109", this.getName() || "ReactCompositeComponent"), t },
            attachRef: function (t, e) {
                var n = this.getPublicInstance();
                null == n ? u("110") : void 0;
                var r = e.getPublicInstance(),
                    o = n.refs === g ? n.refs = {} : n.refs;
                o[t] = r
            },
            detachRef: function (t) {
                var e = this.getPublicInstance().refs;
                delete e[t]
            },
            getName: function () {
                var t = this._currentElement.type,
                    e = this._instance && this._instance.constructor;
                return t.displayName || e && e.displayName || t.name || e && e.name || null
            },
            getPublicInstance: function () { var t = this._instance; return this._compositeType === b.StatelessFunctional ? null : t },
            _instantiateReactComponent: null
        };
    t.exports = w
},
function (t, e, n) {
    "use strict";
    var r = n(5),
        o = n(238),
        i = n(99),
        a = n(19),
        u = n(9),
        s = n(251),
        c = n(267),
        l = n(104),
        f = n(275);
    n(2);
    o.inject();
    var p = { findDOMNode: c, render: i.render, unmountComponentAtNode: i.unmountComponentAtNode, version: s, unstable_batchedUpdates: u.batchedUpdates, unstable_renderSubtreeIntoContainer: f };
    "undefined" != typeof __REACT_DEVTOOLS_GLOBAL_HOOK__ && "function" == typeof __REACT_DEVTOOLS_GLOBAL_HOOK__.inject && __REACT_DEVTOOLS_GLOBAL_HOOK__.inject({ ComponentTree: { getClosestInstanceFromNode: r.getClosestInstanceFromNode, getNodeFromInstance: function (t) { return t._renderedComponent && (t = l(t)), t ? r.getNodeFromInstance(t) : null } }, Mount: i, Reconciler: a });
    t.exports = p
},
function (t, e, n) {
    "use strict";

    function r(t) { if (t) { var e = t._currentElement._owner || null; if (e) { var n = e.getName(); if (n) return " This DOM node was rendered by `" + n + "`." } } return "" }

    function o(t, e) { e && (K[t._tag] && (null != e.children || null != e.dangerouslySetInnerHTML ? v("137", t._tag, t._currentElement._owner ? " Check the render method of " + t._currentElement._owner.getName() + "." : "") : void 0), null != e.dangerouslySetInnerHTML && (null != e.children ? v("60") : void 0, "object" == typeof e.dangerouslySetInnerHTML && B in e.dangerouslySetInnerHTML ? void 0 : v("61")), null != e.style && "object" != typeof e.style ? v("62", r(t)) : void 0) }

    function i(t, e, n, r) {
        if (!(r instanceof j)) {
            var o = t._hostContainerInfo,
                i = o._node && o._node.nodeType === z,
                u = i ? o._node : o._ownerDocument;
            F(e, u), r.getReactMountReady().enqueue(a, { inst: t, registrationName: e, listener: n })
        }
    }

    function a() {
        var t = this;
        C.putListener(t.inst, t.registrationName, t.listener)
    }

    function u() {
        var t = this;
        S.postMountWrapper(t)
    }

    function s() {
        var t = this;
        A.postMountWrapper(t)
    }

    function c() {
        var t = this;
        N.postMountWrapper(t)
    }

    function l() {
        var t = this;
        t._rootNodeID ? void 0 : v("63");
        var e = L(t);
        switch (e ? void 0 : v("64"), t._tag) {
            case "iframe":
            case "object":
                t._wrapperState.listeners = [M.trapBubbledEvent("topLoad", "load", e)];
                break;
            case "video":
            case "audio":
                t._wrapperState.listeners = [];
                for (var n in V) V.hasOwnProperty(n) && t._wrapperState.listeners.push(M.trapBubbledEvent(n, V[n], e));
                break;
            case "source":
                t._wrapperState.listeners = [M.trapBubbledEvent("topError", "error", e)];
                break;
            case "img":
                t._wrapperState.listeners = [M.trapBubbledEvent("topError", "error", e), M.trapBubbledEvent("topLoad", "load", e)];
                break;
            case "form":
                t._wrapperState.listeners = [M.trapBubbledEvent("topReset", "reset", e), M.trapBubbledEvent("topSubmit", "submit", e)];
                break;
            case "input":
            case "select":
            case "textarea":
                t._wrapperState.listeners = [M.trapBubbledEvent("topInvalid", "invalid", e)]
        }
    }

    function f() { O.postUpdateWrapper(this) }

    function p(t) { J.call(G, t) || (X.test(t) ? void 0 : v("65", t), G[t] = !0) }

    function h(t, e) { return t.indexOf("-") >= 0 || null != e.is }

    function d(t) {
        var e = t.type;
        p(e), this._currentElement = t, this._tag = e.toLowerCase(), this._namespaceURI = null, this._renderedChildren = null, this._previousStyle = null, this._previousStyleCopy = null, this._hostNode = null, this._hostParent = null, this._rootNodeID = 0, this._domID = 0, this._hostContainerInfo = null, this._wrapperState = null, this._topLevelWrapper = null, this._flags = 0
    }
    var v = n(3),
        g = n(4),
        y = n(213),
        m = n(215),
        b = n(17),
        x = n(47),
        w = n(18),
        _ = n(92),
        C = n(25),
        E = n(48),
        M = n(34),
        k = n(93),
        T = n(5),
        S = n(231),
        N = n(232),
        O = n(94),
        A = n(235),
        P = (n(8), n(244)),
        j = n(249),
        D = (n(7), n(37)),
        I = (n(1), n(59), n(39), n(61), n(2), k),
        R = C.deleteListener,
        L = T.getNodeFromInstance,
        F = M.listenTo,
        U = E.registrationNameModules,
        q = { string: !0, number: !0 },
        H = "style",
        B = "__html",
        W = { children: null, dangerouslySetInnerHTML: null, suppressContentEditableWarning: null },
        z = 11,
        V = { topAbort: "abort", topCanPlay: "canplay", topCanPlayThrough: "canplaythrough", topDurationChange: "durationchange", topEmptied: "emptied", topEncrypted: "encrypted", topEnded: "ended", topError: "error", topLoadedData: "loadeddata", topLoadedMetadata: "loadedmetadata", topLoadStart: "loadstart", topPause: "pause", topPlay: "play", topPlaying: "playing", topProgress: "progress", topRateChange: "ratechange", topSeeked: "seeked", topSeeking: "seeking", topStalled: "stalled", topSuspend: "suspend", topTimeUpdate: "timeupdate", topVolumeChange: "volumechange", topWaiting: "waiting" },
        Y = { area: !0, base: !0, br: !0, col: !0, embed: !0, hr: !0, img: !0, input: !0, keygen: !0, link: !0, meta: !0, param: !0, source: !0, track: !0, wbr: !0 },
        $ = { listing: !0, pre: !0, textarea: !0 },
        K = g({ menuitem: !0 }, Y),
        X = /^[a-zA-Z][a-zA-Z:_\.\-\d]*$/,
        G = {},
        J = {}.hasOwnProperty,
        Q = 1;
    d.displayName = "ReactDOMComponent", d.Mixin = {
        mountComponent: function (t, e, n, r) {
            this._rootNodeID = Q++ , this._domID = n._idCounter++ , this._hostParent = e, this._hostContainerInfo = n;
            var i = this._currentElement.props;
            switch (this._tag) {
                case "audio":
                case "form":
                case "iframe":
                case "img":
                case "link":
                case "object":
                case "source":
                case "video":
                    this._wrapperState = { listeners: null }, t.getReactMountReady().enqueue(l, this);
                    break;
                case "input":
                    S.mountWrapper(this, i, e), i = S.getHostProps(this, i), t.getReactMountReady().enqueue(l, this);
                    break;
                case "option":
                    N.mountWrapper(this, i, e), i = N.getHostProps(this, i);
                    break;
                case "select":
                    O.mountWrapper(this, i, e), i = O.getHostProps(this, i), t.getReactMountReady().enqueue(l, this);
                    break;
                case "textarea":
                    A.mountWrapper(this, i, e), i = A.getHostProps(this, i), t.getReactMountReady().enqueue(l, this)
            }
            o(this, i);
            var a, f;
            null != e ? (a = e._namespaceURI, f = e._tag) : n._tag && (a = n._namespaceURI, f = n._tag), (null == a || a === x.svg && "foreignobject" === f) && (a = x.html), a === x.html && ("svg" === this._tag ? a = x.svg : "math" === this._tag && (a = x.mathml)), this._namespaceURI = a;
            var p;
            if (t.useCreateElement) {
                var h, d = n._ownerDocument;
                if (a === x.html)
                    if ("script" === this._tag) {
                        var v = d.createElement("div"),
                            g = this._currentElement.type;
                        v.innerHTML = "<" + g + "></" + g + ">", h = v.removeChild(v.firstChild)
                    } else h = i.is ? d.createElement(this._currentElement.type, i.is) : d.createElement(this._currentElement.type);
                else h = d.createElementNS(a, this._currentElement.type);
                T.precacheNode(this, h), this._flags |= I.hasCachedChildNodes, this._hostParent || _.setAttributeForRoot(h), this._updateDOMProperties(null, i, t);
                var m = b(h);
                this._createInitialChildren(t, i, r, m), p = m
            } else {
                var w = this._createOpenTagMarkupAndPutListeners(t, i),
                    C = this._createContentMarkup(t, i, r);
                p = !C && Y[this._tag] ? w + "/>" : w + ">" + C + "</" + this._currentElement.type + ">"
            }
            switch (this._tag) {
                case "input":
                    t.getReactMountReady().enqueue(u, this), i.autoFocus && t.getReactMountReady().enqueue(y.focusDOMComponent, this);
                    break;
                case "textarea":
                    t.getReactMountReady().enqueue(s, this), i.autoFocus && t.getReactMountReady().enqueue(y.focusDOMComponent, this);
                    break;
                case "select":
                    i.autoFocus && t.getReactMountReady().enqueue(y.focusDOMComponent, this);
                    break;
                case "button":
                    i.autoFocus && t.getReactMountReady().enqueue(y.focusDOMComponent, this);
                    break;
                case "option":
                    t.getReactMountReady().enqueue(c, this)
            }
            return p
        },
        _createOpenTagMarkupAndPutListeners: function (t, e) {
            var n = "<" + this._currentElement.type;
            for (var r in e)
                if (e.hasOwnProperty(r)) {
                    var o = e[r];
                    if (null != o)
                        if (U.hasOwnProperty(r)) o && i(this, r, o, t);
                        else {
                            r === H && (o && (o = this._previousStyleCopy = g({}, e.style)), o = m.createMarkupForStyles(o, this));
                            var a = null;
                            null != this._tag && h(this._tag, e) ? W.hasOwnProperty(r) || (a = _.createMarkupForCustomAttribute(r, o)) : a = _.createMarkupForProperty(r, o), a && (n += " " + a)
                        }
                }
            return t.renderToStaticMarkup ? n : (this._hostParent || (n += " " + _.createMarkupForRoot()), n += " " + _.createMarkupForID(this._domID))
        },
        _createContentMarkup: function (t, e, n) {
            var r = "",
                o = e.dangerouslySetInnerHTML;
            if (null != o) null != o.__html && (r = o.__html);
            else {
                var i = q[typeof e.children] ? e.children : null,
                    a = null != i ? null : e.children;
                if (null != i) r = D(i);
                else if (null != a) {
                    var u = this.mountChildren(a, t, n);
                    r = u.join("")
                }
            }
            return $[this._tag] && "\n" === r.charAt(0) ? "\n" + r : r
        },
        _createInitialChildren: function (t, e, n, r) {
            var o = e.dangerouslySetInnerHTML;
            if (null != o) null != o.__html && b.queueHTML(r, o.__html);
            else {
                var i = q[typeof e.children] ? e.children : null,
                    a = null != i ? null : e.children;
                if (null != i) "" !== i && b.queueText(r, i);
                else if (null != a)
                    for (var u = this.mountChildren(a, t, n), s = 0; s < u.length; s++) b.queueChild(r, u[s])
            }
        },
        receiveComponent: function (t, e, n) {
            var r = this._currentElement;
            this._currentElement = t, this.updateComponent(e, r, t, n)
        },
        updateComponent: function (t, e, n, r) {
            var i = e.props,
                a = this._currentElement.props;
            switch (this._tag) {
                case "input":
                    i = S.getHostProps(this, i), a = S.getHostProps(this, a);
                    break;
                case "option":
                    i = N.getHostProps(this, i), a = N.getHostProps(this, a);
                    break;
                case "select":
                    i = O.getHostProps(this, i), a = O.getHostProps(this, a);
                    break;
                case "textarea":
                    i = A.getHostProps(this, i), a = A.getHostProps(this, a)
            }
            switch (o(this, a), this._updateDOMProperties(i, a, t), this._updateDOMChildren(i, a, t, r), this._tag) {
                case "input":
                    S.updateWrapper(this);
                    break;
                case "textarea":
                    A.updateWrapper(this);
                    break;
                case "select":
                    t.getReactMountReady().enqueue(f, this)
            }
        },
        _updateDOMProperties: function (t, e, n) {
            var r, o, a;
            for (r in t)
                if (!e.hasOwnProperty(r) && t.hasOwnProperty(r) && null != t[r])
                    if (r === H) {
                        var u = this._previousStyleCopy;
                        for (o in u) u.hasOwnProperty(o) && (a = a || {}, a[o] = "");
                        this._previousStyleCopy = null
                    } else U.hasOwnProperty(r) ? t[r] && R(this, r) : h(this._tag, t) ? W.hasOwnProperty(r) || _.deleteValueForAttribute(L(this), r) : (w.properties[r] || w.isCustomAttribute(r)) && _.deleteValueForProperty(L(this), r);
            for (r in e) {
                var s = e[r],
                    c = r === H ? this._previousStyleCopy : null != t ? t[r] : void 0;
                if (e.hasOwnProperty(r) && s !== c && (null != s || null != c))
                    if (r === H)
                        if (s ? s = this._previousStyleCopy = g({}, s) : this._previousStyleCopy = null, c) { for (o in c) !c.hasOwnProperty(o) || s && s.hasOwnProperty(o) || (a = a || {}, a[o] = ""); for (o in s) s.hasOwnProperty(o) && c[o] !== s[o] && (a = a || {}, a[o] = s[o]) } else a = s;
                    else if (U.hasOwnProperty(r)) s ? i(this, r, s, n) : c && R(this, r);
                    else if (h(this._tag, e)) W.hasOwnProperty(r) || _.setValueForAttribute(L(this), r, s);
                    else if (w.properties[r] || w.isCustomAttribute(r)) {
                        var l = L(this);
                        null != s ? _.setValueForProperty(l, r, s) : _.deleteValueForProperty(l, r)
                    }
            }
            a && m.setValueForStyles(L(this), a, this)
        },
        _updateDOMChildren: function (t, e, n, r) {
            var o = q[typeof t.children] ? t.children : null,
                i = q[typeof e.children] ? e.children : null,
                a = t.dangerouslySetInnerHTML && t.dangerouslySetInnerHTML.__html,
                u = e.dangerouslySetInnerHTML && e.dangerouslySetInnerHTML.__html,
                s = null != o ? null : t.children,
                c = null != i ? null : e.children,
                l = null != o || null != a,
                f = null != i || null != u;
            null != s && null == c ? this.updateChildren(null, n, r) : l && !f && this.updateTextContent(""), null != i ? o !== i && this.updateTextContent("" + i) : null != u ? a !== u && this.updateMarkup("" + u) : null != c && this.updateChildren(c, n, r)
        },
        getHostNode: function () { return L(this) },
        unmountComponent: function (t) {
            switch (this._tag) {
                case "audio":
                case "form":
                case "iframe":
                case "img":
                case "link":
                case "object":
                case "source":
                case "video":
                    var e = this._wrapperState.listeners;
                    if (e)
                        for (var n = 0; n < e.length; n++) e[n].remove();
                    break;
                case "html":
                case "head":
                case "body":
                    v("66", this._tag)
            }
            this.unmountChildren(t), T.uncacheNode(this), C.deleteAllListeners(this), this._rootNodeID = 0, this._domID = 0, this._wrapperState = null
        },
        getPublicInstance: function () { return L(this) }
    }, g(d.prototype, d.Mixin, P.Mixin), t.exports = d
},
function (t, e, n) {
    "use strict";

    function r(t, e) {
        var n = {
            _topLevelWrapper: t,
            _idCounter: 1,
            _ownerDocument: e ? e.nodeType === o ? e : e.ownerDocument : null,
            _node: e,
            _tag: e ? e.nodeName.toLowerCase() : null,
            _namespaceURI: e ? e.namespaceURI : null
        };
        return n
    }
    var o = (n(61), 9);
    t.exports = r
},
function (t, e, n) {
    "use strict";
    var r = n(4),
        o = n(17),
        i = n(5),
        a = function (t) { this._currentElement = null, this._hostNode = null, this._hostParent = null, this._hostContainerInfo = null, this._domID = 0 };
    r(a.prototype, {
        mountComponent: function (t, e, n, r) {
            var a = n._idCounter++;
            this._domID = a, this._hostParent = e, this._hostContainerInfo = n;
            var u = " react-empty: " + this._domID + " ";
            if (t.useCreateElement) {
                var s = n._ownerDocument,
                    c = s.createComment(u);
                return i.precacheNode(this, c), o(c)
            }
            return t.renderToStaticMarkup ? "" : "<!--" + u + "-->"
        },
        receiveComponent: function () { },
        getHostNode: function () { return i.getNodeFromInstance(this) },
        unmountComponent: function () { i.uncacheNode(this) }
    }), t.exports = a
},
function (t, e) {
    "use strict";
    var n = { useCreateElement: !0, useFiber: !1 };
    t.exports = n
},
function (t, e, n) {
    "use strict";
    var r = n(46),
        o = n(5),
        i = {
            dangerouslyProcessChildrenUpdates: function (t, e) {
                var n = o.getNodeFromInstance(t);
                r.processUpdates(n, e)
            }
        };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r() { this._rootNodeID && f.updateWrapper(this) }

    function o(t) {
        var e = this._currentElement.props,
            n = s.executeOnChange(e, t);
        l.asap(r, this);
        var o = e.name;
        if ("radio" === e.type && null != o) {
            for (var a = c.getNodeFromInstance(this), u = a; u.parentNode;) u = u.parentNode;
            for (var f = u.querySelectorAll("input[name=" + JSON.stringify("" + o) + '][type="radio"]'), p = 0; p < f.length; p++) {
                var h = f[p];
                if (h !== a && h.form === a.form) {
                    var d = c.getInstanceFromNode(h);
                    d ? void 0 : i("90"), l.asap(r, d)
                }
            }
        }
        return n
    }
    var i = n(3),
        a = n(4),
        u = n(92),
        s = n(51),
        c = n(5),
        l = n(9),
        f = (n(1), n(2), {
            getHostProps: function (t, e) {
                var n = s.getValue(e),
                    r = s.getChecked(e),
                    o = a({ type: void 0, step: void 0, min: void 0, max: void 0 }, e, { defaultChecked: void 0, defaultValue: void 0, value: null != n ? n : t._wrapperState.initialValue, checked: null != r ? r : t._wrapperState.initialChecked, onChange: t._wrapperState.onChange });
                return o
            },
            mountWrapper: function (t, e) {
                var n = e.defaultValue;
                t._wrapperState = { initialChecked: null != e.checked ? e.checked : e.defaultChecked, initialValue: null != e.value ? e.value : n, listeners: null, onChange: o.bind(t) }
            },
            updateWrapper: function (t) {
                var e = t._currentElement.props,
                    n = e.checked;
                null != n && u.setValueForProperty(c.getNodeFromInstance(t), "checked", n || !1);
                var r = c.getNodeFromInstance(t),
                    o = s.getValue(e);
                if (null != o) {
                    var i = "" + o;
                    i !== r.value && (r.value = i)
                } else null == e.value && null != e.defaultValue && r.defaultValue !== "" + e.defaultValue && (r.defaultValue = "" + e.defaultValue), null == e.checked && null != e.defaultChecked && (r.defaultChecked = !!e.defaultChecked)
            },
            postMountWrapper: function (t) {
                var e = t._currentElement.props,
                    n = c.getNodeFromInstance(t);
                switch (e.type) {
                    case "submit":
                    case "reset":
                        break;
                    case "color":
                    case "date":
                    case "datetime":
                    case "datetime-local":
                    case "month":
                    case "time":
                    case "week":
                        n.value = "", n.value = n.defaultValue;
                        break;
                    default:
                        n.value = n.value
                }
                var r = n.name;
                "" !== r && (n.name = ""), n.defaultChecked = !n.defaultChecked, n.defaultChecked = !n.defaultChecked, "" !== r && (n.name = r)
            }
        });
    t.exports = f
},
function (t, e, n) {
    "use strict";

    function r(t) { var e = ""; return i.Children.forEach(t, function (t) { null != t && ("string" == typeof t || "number" == typeof t ? e += t : s || (s = !0)) }), e }
    var o = n(4),
        i = n(20),
        a = n(5),
        u = n(94),
        s = (n(2), !1),
        c = {
            mountWrapper: function (t, e, n) {
                var o = null;
                if (null != n) { var i = n; "optgroup" === i._tag && (i = i._hostParent), null != i && "select" === i._tag && (o = u.getSelectValueContext(i)) }
                var a = null;
                if (null != o) {
                    var s;
                    if (s = null != e.value ? e.value + "" : r(e.children), a = !1, Array.isArray(o)) {
                        for (var c = 0; c < o.length; c++)
                            if ("" + o[c] === s) { a = !0; break }
                    } else a = "" + o === s
                }
                t._wrapperState = { selected: a }
            },
            postMountWrapper: function (t) {
                var e = t._currentElement.props;
                if (null != e.value) {
                    var n = a.getNodeFromInstance(t);
                    n.setAttribute("value", e.value)
                }
            },
            getHostProps: function (t, e) {
                var n = o({ selected: void 0, children: void 0 }, e);
                null != t._wrapperState.selected && (n.selected = t._wrapperState.selected);
                var i = r(e.children);
                return i && (n.children = i), n
            }
        };
    t.exports = c
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return t === n && e === r }

    function o(t) {
        var e = document.selection,
            n = e.createRange(),
            r = n.text.length,
            o = n.duplicate();
        o.moveToElementText(t), o.setEndPoint("EndToStart", n);
        var i = o.text.length,
            a = i + r;
        return { start: i, end: a }
    }

    function i(t) {
        var e = window.getSelection && window.getSelection();
        if (!e || 0 === e.rangeCount) return null;
        var n = e.anchorNode,
            o = e.anchorOffset,
            i = e.focusNode,
            a = e.focusOffset,
            u = e.getRangeAt(0);
        try { u.startContainer.nodeType, u.endContainer.nodeType } catch (t) { return null }
        var s = r(e.anchorNode, e.anchorOffset, e.focusNode, e.focusOffset),
            c = s ? 0 : u.toString().length,
            l = u.cloneRange();
        l.selectNodeContents(t), l.setEnd(u.startContainer, u.startOffset);
        var f = r(l.startContainer, l.startOffset, l.endContainer, l.endOffset),
            p = f ? 0 : l.toString().length,
            h = p + c,
            d = document.createRange();
        d.setStart(n, o), d.setEnd(i, a);
        var v = d.collapsed;
        return { start: v ? h : p, end: v ? p : h }
    }

    function a(t, e) {
        var n, r, o = document.selection.createRange().duplicate();
        void 0 === e.end ? (n = e.start, r = n) : e.start > e.end ? (n = e.end, r = e.start) : (n = e.start, r = e.end), o.moveToElementText(t), o.moveStart("character", n), o.setEndPoint("EndToStart", o), o.moveEnd("character", r - n), o.select()
    }

    function u(t, e) {
        if (window.getSelection) {
            var n = window.getSelection(),
                r = t[l()].length,
                o = Math.min(e.start, r),
                i = void 0 === e.end ? o : Math.min(e.end, r);
            if (!n.extend && o > i) {
                var a = i;
                i = o, o = a
            }
            var u = c(t, o),
                s = c(t, i);
            if (u && s) {
                var f = document.createRange();
                f.setStart(u.node, u.offset), n.removeAllRanges(), o > i ? (n.addRange(f), n.extend(s.node, s.offset)) : (f.setEnd(s.node, s.offset), n.addRange(f))
            }
        }
    }
    var s = n(6),
        c = n(272),
        l = n(105),
        f = s.canUseDOM && "selection" in document && !("getSelection" in window),
        p = { getOffsets: f ? o : i, setOffsets: f ? a : u };
    t.exports = p
},
function (t, e, n) {
    "use strict";
    var r = n(3),
        o = n(4),
        i = n(46),
        a = n(17),
        u = n(5),
        s = n(37),
        c = (n(1), n(61), function (t) { this._currentElement = t, this._stringText = "" + t, this._hostNode = null, this._hostParent = null, this._domID = 0, this._mountIndex = 0, this._closingComment = null, this._commentNodes = null });
    o(c.prototype, {
        mountComponent: function (t, e, n, r) {
            var o = n._idCounter++,
                i = " react-text: " + o + " ",
                c = " /react-text ";
            if (this._domID = o, this._hostParent = e, t.useCreateElement) {
                var l = n._ownerDocument,
                    f = l.createComment(i),
                    p = l.createComment(c),
                    h = a(l.createDocumentFragment());
                return a.queueChild(h, a(f)), this._stringText && a.queueChild(h, a(l.createTextNode(this._stringText))), a.queueChild(h, a(p)), u.precacheNode(this, f), this._closingComment = p, h
            }
            var d = s(this._stringText);
            return t.renderToStaticMarkup ? d : "<!--" + i + "-->" + d + "<!--" + c + "-->"
        },
        receiveComponent: function (t, e) {
            if (t !== this._currentElement) {
                this._currentElement = t;
                var n = "" + t;
                if (n !== this._stringText) {
                    this._stringText = n;
                    var r = this.getHostNode();
                    i.replaceDelimitedText(r[0], r[1], n)
                }
            }
        },
        getHostNode: function () {
            var t = this._commentNodes;
            if (t) return t;
            if (!this._closingComment)
                for (var e = u.getNodeFromInstance(this), n = e.nextSibling; ;) {
                    if (null == n ? r("67", this._domID) : void 0, 8 === n.nodeType && " /react-text " === n.nodeValue) { this._closingComment = n; break }
                    n = n.nextSibling
                }
            return t = [this._hostNode, this._closingComment], this._commentNodes = t, t
        },
        unmountComponent: function () { this._closingComment = null, this._commentNodes = null, u.uncacheNode(this) }
    }), t.exports = c
},
function (t, e, n) {
    "use strict";

    function r() { this._rootNodeID && l.updateWrapper(this) }

    function o(t) {
        var e = this._currentElement.props,
            n = u.executeOnChange(e, t);
        return c.asap(r, this), n
    }
    var i = n(3),
        a = n(4),
        u = n(51),
        s = n(5),
        c = n(9),
        l = (n(1), n(2), {
            getHostProps: function (t, e) { null != e.dangerouslySetInnerHTML ? i("91") : void 0; var n = a({}, e, { value: void 0, defaultValue: void 0, children: "" + t._wrapperState.initialValue, onChange: t._wrapperState.onChange }); return n },
            mountWrapper: function (t, e) {
                var n = u.getValue(e),
                    r = n;
                if (null == n) {
                    var a = e.defaultValue,
                        s = e.children;
                    null != s && (null != a ? i("92") : void 0, Array.isArray(s) && (s.length <= 1 ? void 0 : i("93"), s = s[0]), a = "" + s), null == a && (a = ""), r = a
                }
                t._wrapperState = { initialValue: "" + r, listeners: null, onChange: o.bind(t) }
            },
            updateWrapper: function (t) {
                var e = t._currentElement.props,
                    n = s.getNodeFromInstance(t),
                    r = u.getValue(e);
                if (null != r) {
                    var o = "" + r;
                    o !== n.value && (n.value = o), null == e.defaultValue && (n.defaultValue = o)
                }
                null != e.defaultValue && (n.defaultValue = e.defaultValue)
            },
            postMountWrapper: function (t) {
                var e = s.getNodeFromInstance(t),
                    n = e.textContent;
                n === t._wrapperState.initialValue && (e.value = n)
            }
        });
    t.exports = l
},
function (t, e, n) {
    "use strict";

    function r(t, e) {
        "_hostNode" in t ? void 0 : s("33"), "_hostNode" in e ? void 0 : s("33");
        for (var n = 0, r = t; r; r = r._hostParent) n++;
        for (var o = 0, i = e; i; i = i._hostParent) o++;
        for (; n - o > 0;) t = t._hostParent, n--;
        for (; o - n > 0;) e = e._hostParent, o--;
        for (var a = n; a--;) {
            if (t === e) return t;
            t = t._hostParent, e = e._hostParent
        }
        return null
    }

    function o(t, e) {
        "_hostNode" in t ? void 0 : s("35"), "_hostNode" in e ? void 0 : s("35");
        for (; e;) {
            if (e === t) return !0;
            e = e._hostParent
        }
        return !1
    }

    function i(t) { return "_hostNode" in t ? void 0 : s("36"), t._hostParent }

    function a(t, e, n) { for (var r = []; t;) r.push(t), t = t._hostParent; var o; for (o = r.length; o-- > 0;) e(r[o], "captured", n); for (o = 0; o < r.length; o++) e(r[o], "bubbled", n) }

    function u(t, e, n, o, i) { for (var a = t && e ? r(t, e) : null, u = []; t && t !== a;) u.push(t), t = t._hostParent; for (var s = []; e && e !== a;) s.push(e), e = e._hostParent; var c; for (c = 0; c < u.length; c++) n(u[c], "bubbled", o); for (c = s.length; c-- > 0;) n(s[c], "captured", i) }
    var s = n(3);
    n(1);
    t.exports = { isAncestor: o, getLowestCommonAncestor: r, getParentInstance: i, traverseTwoPhase: a, traverseEnterLeave: u }
},
function (t, e, n) {
    "use strict";

    function r() { this.reinitializeTransaction() }
    var o = n(4),
        i = n(9),
        a = n(36),
        u = n(7),
        s = { initialize: u, close: function () { p.isBatchingUpdates = !1 } },
        c = { initialize: u, close: i.flushBatchedUpdates.bind(i) },
        l = [c, s];
    o(r.prototype, a, { getTransactionWrappers: function () { return l } });
    var f = new r,
        p = { isBatchingUpdates: !1, batchedUpdates: function (t, e, n, r, o, i) { var a = p.isBatchingUpdates; return p.isBatchingUpdates = !0, a ? t(e, n, r, o, i) : f.perform(t, null, e, n, r, o, i) } };
    t.exports = p
},
function (t, e, n) {
    "use strict";

    function r() { C || (C = !0, m.EventEmitter.injectReactEventListener(y), m.EventPluginHub.injectEventPluginOrder(u), m.EventPluginUtils.injectComponentTree(p), m.EventPluginUtils.injectTreeTraversal(d), m.EventPluginHub.injectEventPluginsByName({ SimpleEventPlugin: _, EnterLeaveEventPlugin: s, ChangeEventPlugin: a, SelectEventPlugin: w, BeforeInputEventPlugin: i }), m.HostComponent.injectGenericComponentClass(f), m.HostComponent.injectTextComponentClass(v), m.DOMProperty.injectDOMPropertyConfig(o), m.DOMProperty.injectDOMPropertyConfig(c), m.DOMProperty.injectDOMPropertyConfig(x), m.EmptyComponent.injectEmptyComponentFactory(function (t) { return new h(t) }), m.Updates.injectReconcileTransaction(b), m.Updates.injectBatchingStrategy(g), m.Component.injectEnvironment(l)) }
    var o = n(212),
        i = n(214),
        a = n(216),
        u = n(218),
        s = n(219),
        c = n(221),
        l = n(223),
        f = n(226),
        p = n(5),
        h = n(228),
        d = n(236),
        v = n(234),
        g = n(237),
        y = n(241),
        m = n(242),
        b = n(247),
        x = n(252),
        w = n(253),
        _ = n(254),
        C = !1;
    t.exports = { inject: r }
},
    111,
function (t, e, n) {
    "use strict";

    function r(t) { o.enqueueEvents(t), o.processEventQueue(!1) }
    var o = n(25),
        i = {
            handleTopLevel: function (t, e, n, i) {
                var a = o.extractEvents(t, e, n, i);
                r(a)
            }
        };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t) {
        for (; t._hostParent;) t = t._hostParent;
        var e = f.getNodeFromInstance(t),
            n = e.parentNode;
        return f.getClosestInstanceFromNode(n)
    }

    function o(t, e) { this.topLevelType = t, this.nativeEvent = e, this.ancestors = [] }

    function i(t) {
        var e = h(t.nativeEvent),
            n = f.getClosestInstanceFromNode(e),
            o = n;
        do t.ancestors.push(o), o = o && r(o); while (o);
        for (var i = 0; i < t.ancestors.length; i++) n = t.ancestors[i], v._handleTopLevel(t.topLevelType, n, t.nativeEvent, h(t.nativeEvent))
    }

    function a(t) {
        var e = d(window);
        t(e)
    }
    var u = n(4),
        s = n(67),
        c = n(6),
        l = n(14),
        f = n(5),
        p = n(9),
        h = n(58),
        d = n(141);
    u(o.prototype, { destructor: function () { this.topLevelType = null, this.nativeEvent = null, this.ancestors.length = 0 } }), l.addPoolingTo(o, l.twoArgumentPooler);
    var v = {
        _enabled: !0,
        _handleTopLevel: null,
        WINDOW_HANDLE: c.canUseDOM ? window : null,
        setHandleTopLevel: function (t) { v._handleTopLevel = t },
        setEnabled: function (t) { v._enabled = !!t },
        isEnabled: function () { return v._enabled },
        trapBubbledEvent: function (t, e, n) { return n ? s.listen(n, e, v.dispatchEvent.bind(null, t)) : null },
        trapCapturedEvent: function (t, e, n) { return n ? s.capture(n, e, v.dispatchEvent.bind(null, t)) : null },
        monitorScrollValue: function (t) {
            var e = a.bind(null, t);
            s.listen(window, "scroll", e)
        },
        dispatchEvent: function (t, e) { if (v._enabled) { var n = o.getPooled(t, e); try { p.batchedUpdates(i, n) } finally { o.release(n) } } }
    };
    t.exports = v
},
function (t, e, n) {
    "use strict";
    var r = n(18),
        o = n(25),
        i = n(49),
        a = n(52),
        u = n(95),
        s = n(34),
        c = n(97),
        l = n(9),
        f = { Component: a.injection, DOMProperty: r.injection, EmptyComponent: u.injection, EventPluginHub: o.injection, EventPluginUtils: i.injection, EventEmitter: s.injection, HostComponent: c.injection, Updates: l.injection };
    t.exports = f
},
function (t, e, n) {
    "use strict";
    var r = n(265),
        o = /\/?>/,
        i = /^<\!\-\-/,
        a = {
            CHECKSUM_ATTR_NAME: "data-react-checksum",
            addChecksumToMarkup: function (t) { var e = r(t); return i.test(t) ? t : t.replace(o, " " + a.CHECKSUM_ATTR_NAME + '="' + e + '"$&') },
            canReuseMarkup: function (t, e) {
                var n = e.getAttribute(a.CHECKSUM_ATTR_NAME);
                n = n && parseInt(n, 10);
                var o = r(t);
                return o === n
            }
        };
    t.exports = a
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { return { type: "INSERT_MARKUP", content: t, fromIndex: null, fromNode: null, toIndex: n, afterNode: e } }

    function o(t, e, n) { return { type: "MOVE_EXISTING", content: null, fromIndex: t._mountIndex, fromNode: p.getHostNode(t), toIndex: n, afterNode: e } }

    function i(t, e) { return { type: "REMOVE_NODE", content: null, fromIndex: t._mountIndex, fromNode: e, toIndex: null, afterNode: null } }

    function a(t) { return { type: "SET_MARKUP", content: t, fromIndex: null, fromNode: null, toIndex: null, afterNode: null } }

    function u(t) { return { type: "TEXT_CONTENT", content: t, fromIndex: null, fromNode: null, toIndex: null, afterNode: null } }

    function s(t, e) { return e && (t = t || [], t.push(e)), t }

    function c(t, e) { f.processChildrenUpdates(t, e) }
    var l = n(3),
        f = n(52),
        p = (n(27), n(8), n(11), n(19)),
        h = n(222),
        d = (n(7), n(268)),
        v = (n(1), {
            Mixin: {
                _reconcilerInstantiateChildren: function (t, e, n) { return h.instantiateChildren(t, e, n) },
                _reconcilerUpdateChildren: function (t, e, n, r, o, i) { var a, u = 0; return a = d(e, u), h.updateChildren(t, a, n, r, o, this, this._hostContainerInfo, i, u), a },
                mountChildren: function (t, e, n) {
                    var r = this._reconcilerInstantiateChildren(t, e, n);
                    this._renderedChildren = r;
                    var o = [],
                        i = 0;
                    for (var a in r)
                        if (r.hasOwnProperty(a)) {
                            var u = r[a],
                                s = 0,
                                c = p.mountComponent(u, e, this, this._hostContainerInfo, n, s);
                            u._mountIndex = i++ , o.push(c)
                        }
                    return o
                },
                updateTextContent: function (t) {
                    var e = this._renderedChildren;
                    h.unmountChildren(e, !1);
                    for (var n in e) e.hasOwnProperty(n) && l("118");
                    var r = [u(t)];
                    c(this, r)
                },
                updateMarkup: function (t) {
                    var e = this._renderedChildren;
                    h.unmountChildren(e, !1);
                    for (var n in e) e.hasOwnProperty(n) && l("118");
                    var r = [a(t)];
                    c(this, r)
                },
                updateChildren: function (t, e, n) { this._updateChildren(t, e, n) },
                _updateChildren: function (t, e, n) {
                    var r = this._renderedChildren,
                        o = {},
                        i = [],
                        a = this._reconcilerUpdateChildren(r, t, i, o, e, n);
                    if (a || r) {
                        var u, l = null,
                            f = 0,
                            h = 0,
                            d = 0,
                            v = null;
                        for (u in a)
                            if (a.hasOwnProperty(u)) {
                                var g = r && r[u],
                                    y = a[u];
                                g === y ? (l = s(l, this.moveChild(g, v, f, h)), h = Math.max(g._mountIndex, h), g._mountIndex = f) : (g && (h = Math.max(g._mountIndex, h)), l = s(l, this._mountChildAtIndex(y, i[d], v, f, e, n)), d++), f++ , v = p.getHostNode(y)
                            }
                        for (u in o) o.hasOwnProperty(u) && (l = s(l, this._unmountChild(r[u], o[u])));
                        l && c(this, l), this._renderedChildren = a
                    }
                },
                unmountChildren: function (t) {
                    var e = this._renderedChildren;
                    h.unmountChildren(e, t), this._renderedChildren = null
                },
                moveChild: function (t, e, n, r) { if (t._mountIndex < r) return o(t, e, n) },
                createChild: function (t, e, n) { return r(n, e, t._mountIndex) },
                removeChild: function (t, e) { return i(t, e) },
                _mountChildAtIndex: function (t, e, n, r, o, i) { return t._mountIndex = r, this.createChild(t, n, e) },
                _unmountChild: function (t, e) { var n = this.removeChild(t, e); return t._mountIndex = null, n }
            }
        });
    t.exports = v
},
function (t, e, n) {
    "use strict";

    function r(t) { return !(!t || "function" != typeof t.attachRef || "function" != typeof t.detachRef) }
    var o = n(3),
        i = (n(1), {
            addComponentAsRefTo: function (t, e, n) { r(n) ? void 0 : o("119"), n.attachRef(e, t) },
            removeComponentAsRefFrom: function (t, e, n) {
                r(n) ? void 0 : o("120");
                var i = n.getPublicInstance();
                i && i.refs[e] === t.getPublicInstance() && n.detachRef(e)
            }
        });
    t.exports = i
},
function (t, e) {
    "use strict";
    var n = "SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED";
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t) { this.reinitializeTransaction(), this.renderToStaticMarkup = !1, this.reactMountReady = i.getPooled(null), this.useCreateElement = t }
    var o = n(4),
        i = n(91),
        a = n(14),
        u = n(34),
        s = n(98),
        c = (n(8), n(36)),
        l = n(54),
        f = { initialize: s.getSelectionInformation, close: s.restoreSelection },
        p = { initialize: function () { var t = u.isEnabled(); return u.setEnabled(!1), t }, close: function (t) { u.setEnabled(t) } },
        h = { initialize: function () { this.reactMountReady.reset() }, close: function () { this.reactMountReady.notifyAll() } },
        d = [f, p, h],
        v = { getTransactionWrappers: function () { return d }, getReactMountReady: function () { return this.reactMountReady }, getUpdateQueue: function () { return l }, checkpoint: function () { return this.reactMountReady.checkpoint() }, rollback: function (t) { this.reactMountReady.rollback(t) }, destructor: function () { i.release(this.reactMountReady), this.reactMountReady = null } };
    o(r.prototype, c, v), a.addPoolingTo(r), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { "function" == typeof t ? t(e.getPublicInstance()) : i.addComponentAsRefTo(e, t, n) }

    function o(t, e, n) { "function" == typeof t ? t(null) : i.removeComponentAsRefFrom(e, t, n) }
    var i = n(245),
        a = {};
    a.attachRefs = function (t, e) {
        if (null !== e && "object" == typeof e) {
            var n = e.ref;
            null != n && r(n, t, e._owner)
        }
    }, a.shouldUpdateRefs = function (t, e) {
        var n = null,
            r = null;
        null !== t && "object" == typeof t && (n = t.ref, r = t._owner);
        var o = null,
            i = null;
        return null !== e && "object" == typeof e && (o = e.ref, i = e._owner), n !== o || "string" == typeof o && i !== r
    }, a.detachRefs = function (t, e) {
        if (null !== e && "object" == typeof e) {
            var n = e.ref;
            null != n && o(n, t, e._owner)
        }
    }, t.exports = a
},
function (t, e, n) {
    "use strict";

    function r(t) { this.reinitializeTransaction(), this.renderToStaticMarkup = t, this.useCreateElement = !1, this.updateQueue = new u(this) }
    var o = n(4),
        i = n(14),
        a = n(36),
        u = (n(8), n(250)),
        s = [],
        c = { enqueue: function () { } },
        l = { getTransactionWrappers: function () { return s }, getReactMountReady: function () { return c }, getUpdateQueue: function () { return this.updateQueue }, destructor: function () { }, checkpoint: function () { }, rollback: function () { } };
    o(r.prototype, a, l), i.addPoolingTo(r), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") }

    function o(t, e) { }
    var i = n(54),
        a = (n(2), function () {
            function t(e) { r(this, t), this.transaction = e }
            return t.prototype.isMounted = function (t) { return !1 }, t.prototype.enqueueCallback = function (t, e, n) { this.transaction.isInTransaction() && i.enqueueCallback(t, e, n) }, t.prototype.enqueueForceUpdate = function (t) { this.transaction.isInTransaction() ? i.enqueueForceUpdate(t) : o(t, "forceUpdate") }, t.prototype.enqueueReplaceState = function (t, e) { this.transaction.isInTransaction() ? i.enqueueReplaceState(t, e) : o(t, "replaceState") }, t.prototype.enqueueSetState = function (t, e) { this.transaction.isInTransaction() ? i.enqueueSetState(t, e) : o(t, "setState") }, t
        }());
    t.exports = a
},
function (t, e) {
    "use strict";
    t.exports = "15.4.2"
},
function (t, e) {
    "use strict";
    var n = { xlink: "http://www.w3.org/1999/xlink", xml: "http://www.w3.org/XML/1998/namespace" },
        r = { accentHeight: "accent-height", accumulate: 0, additive: 0, alignmentBaseline: "alignment-baseline", allowReorder: "allowReorder", alphabetic: 0, amplitude: 0, arabicForm: "arabic-form", ascent: 0, attributeName: "attributeName", attributeType: "attributeType", autoReverse: "autoReverse", azimuth: 0, baseFrequency: "baseFrequency", baseProfile: "baseProfile", baselineShift: "baseline-shift", bbox: 0, begin: 0, bias: 0, by: 0, calcMode: "calcMode", capHeight: "cap-height", clip: 0, clipPath: "clip-path", clipRule: "clip-rule", clipPathUnits: "clipPathUnits", colorInterpolation: "color-interpolation", colorInterpolationFilters: "color-interpolation-filters", colorProfile: "color-profile", colorRendering: "color-rendering", contentScriptType: "contentScriptType", contentStyleType: "contentStyleType", cursor: 0, cx: 0, cy: 0, d: 0, decelerate: 0, descent: 0, diffuseConstant: "diffuseConstant", direction: 0, display: 0, divisor: 0, dominantBaseline: "dominant-baseline", dur: 0, dx: 0, dy: 0, edgeMode: "edgeMode", elevation: 0, enableBackground: "enable-background", end: 0, exponent: 0, externalResourcesRequired: "externalResourcesRequired", fill: 0, fillOpacity: "fill-opacity", fillRule: "fill-rule", filter: 0, filterRes: "filterRes", filterUnits: "filterUnits", floodColor: "flood-color", floodOpacity: "flood-opacity", focusable: 0, fontFamily: "font-family", fontSize: "font-size", fontSizeAdjust: "font-size-adjust", fontStretch: "font-stretch", fontStyle: "font-style", fontVariant: "font-variant", fontWeight: "font-weight", format: 0, from: 0, fx: 0, fy: 0, g1: 0, g2: 0, glyphName: "glyph-name", glyphOrientationHorizontal: "glyph-orientation-horizontal", glyphOrientationVertical: "glyph-orientation-vertical", glyphRef: "glyphRef", gradientTransform: "gradientTransform", gradientUnits: "gradientUnits", hanging: 0, horizAdvX: "horiz-adv-x", horizOriginX: "horiz-origin-x", ideographic: 0, imageRendering: "image-rendering", in: 0, in2: 0, intercept: 0, k: 0, k1: 0, k2: 0, k3: 0, k4: 0, kernelMatrix: "kernelMatrix", kernelUnitLength: "kernelUnitLength", kerning: 0, keyPoints: "keyPoints", keySplines: "keySplines", keyTimes: "keyTimes", lengthAdjust: "lengthAdjust", letterSpacing: "letter-spacing", lightingColor: "lighting-color", limitingConeAngle: "limitingConeAngle", local: 0, markerEnd: "marker-end", markerMid: "marker-mid", markerStart: "marker-start", markerHeight: "markerHeight", markerUnits: "markerUnits", markerWidth: "markerWidth", mask: 0, maskContentUnits: "maskContentUnits", maskUnits: "maskUnits", mathematical: 0, mode: 0, numOctaves: "numOctaves", offset: 0, opacity: 0, operator: 0, order: 0, orient: 0, orientation: 0, origin: 0, overflow: 0, overlinePosition: "overline-position", overlineThickness: "overline-thickness", paintOrder: "paint-order", panose1: "panose-1", pathLength: "pathLength", patternContentUnits: "patternContentUnits", patternTransform: "patternTransform", patternUnits: "patternUnits", pointerEvents: "pointer-events", points: 0, pointsAtX: "pointsAtX", pointsAtY: "pointsAtY", pointsAtZ: "pointsAtZ", preserveAlpha: "preserveAlpha", preserveAspectRatio: "preserveAspectRatio", primitiveUnits: "primitiveUnits", r: 0, radius: 0, refX: "refX", refY: "refY", renderingIntent: "rendering-intent", repeatCount: "repeatCount", repeatDur: "repeatDur", requiredExtensions: "requiredExtensions", requiredFeatures: "requiredFeatures", restart: 0, result: 0, rotate: 0, rx: 0, ry: 0, scale: 0, seed: 0, shapeRendering: "shape-rendering", slope: 0, spacing: 0, specularConstant: "specularConstant", specularExponent: "specularExponent", speed: 0, spreadMethod: "spreadMethod", startOffset: "startOffset", stdDeviation: "stdDeviation", stemh: 0, stemv: 0, stitchTiles: "stitchTiles", stopColor: "stop-color", stopOpacity: "stop-opacity", strikethroughPosition: "strikethrough-position", strikethroughThickness: "strikethrough-thickness", string: 0, stroke: 0, strokeDasharray: "stroke-dasharray", strokeDashoffset: "stroke-dashoffset", strokeLinecap: "stroke-linecap", strokeLinejoin: "stroke-linejoin", strokeMiterlimit: "stroke-miterlimit", strokeOpacity: "stroke-opacity", strokeWidth: "stroke-width", surfaceScale: "surfaceScale", systemLanguage: "systemLanguage", tableValues: "tableValues", targetX: "targetX", targetY: "targetY", textAnchor: "text-anchor", textDecoration: "text-decoration", textRendering: "text-rendering", textLength: "textLength", to: 0, transform: 0, u1: 0, u2: 0, underlinePosition: "underline-position", underlineThickness: "underline-thickness", unicode: 0, unicodeBidi: "unicode-bidi", unicodeRange: "unicode-range", unitsPerEm: "units-per-em", vAlphabetic: "v-alphabetic", vHanging: "v-hanging", vIdeographic: "v-ideographic", vMathematical: "v-mathematical", values: 0, vectorEffect: "vector-effect", version: 0, vertAdvY: "vert-adv-y", vertOriginX: "vert-origin-x", vertOriginY: "vert-origin-y", viewBox: "viewBox", viewTarget: "viewTarget", visibility: 0, widths: 0, wordSpacing: "word-spacing", writingMode: "writing-mode", x: 0, xHeight: "x-height", x1: 0, x2: 0, xChannelSelector: "xChannelSelector", xlinkActuate: "xlink:actuate", xlinkArcrole: "xlink:arcrole", xlinkHref: "xlink:href", xlinkRole: "xlink:role", xlinkShow: "xlink:show", xlinkTitle: "xlink:title", xlinkType: "xlink:type", xmlBase: "xml:base", xmlns: 0, xmlnsXlink: "xmlns:xlink", xmlLang: "xml:lang", xmlSpace: "xml:space", y: 0, y1: 0, y2: 0, yChannelSelector: "yChannelSelector", z: 0, zoomAndPan: "zoomAndPan" },
        o = { Properties: {}, DOMAttributeNamespaces: { xlinkActuate: n.xlink, xlinkArcrole: n.xlink, xlinkHref: n.xlink, xlinkRole: n.xlink, xlinkShow: n.xlink, xlinkTitle: n.xlink, xlinkType: n.xlink, xmlBase: n.xml, xmlLang: n.xml, xmlSpace: n.xml }, DOMAttributeNames: {} };
    Object.keys(r).forEach(function (t) { o.Properties[t] = 0, r[t] && (o.DOMAttributeNames[t] = r[t]) }), t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t) { if ("selectionStart" in t && s.hasSelectionCapabilities(t)) return { start: t.selectionStart, end: t.selectionEnd }; if (window.getSelection) { var e = window.getSelection(); return { anchorNode: e.anchorNode, anchorOffset: e.anchorOffset, focusNode: e.focusNode, focusOffset: e.focusOffset } } if (document.selection) { var n = document.selection.createRange(); return { parentElement: n.parentElement(), text: n.text, top: n.boundingTop, left: n.boundingLeft } } }

    function o(t, e) { if (m || null == v || v !== l()) return null; var n = r(v); if (!y || !p(y, n)) { y = n; var o = c.getPooled(d.select, g, t, e); return o.type = "select", o.target = v, i.accumulateTwoPhaseDispatches(o), o } return null }
    var i = n(26),
        a = n(6),
        u = n(5),
        s = n(98),
        c = n(10),
        l = n(69),
        f = n(107),
        p = n(39),
        h = a.canUseDOM && "documentMode" in document && document.documentMode <= 11,
        d = { select: { phasedRegistrationNames: { bubbled: "onSelect", captured: "onSelectCapture" }, dependencies: ["topBlur", "topContextMenu", "topFocus", "topKeyDown", "topKeyUp", "topMouseDown", "topMouseUp", "topSelectionChange"] } },
        v = null,
        g = null,
        y = null,
        m = !1,
        b = !1,
        x = {
            eventTypes: d,
            extractEvents: function (t, e, n, r) {
                if (!b) return null;
                var i = e ? u.getNodeFromInstance(e) : window;
                switch (t) {
                    case "topFocus":
                        (f(i) || "true" === i.contentEditable) && (v = i, g = e, y = null);
                        break;
                    case "topBlur":
                        v = null, g = null, y = null;
                        break;
                    case "topMouseDown":
                        m = !0;
                        break;
                    case "topContextMenu":
                    case "topMouseUp":
                        return m = !1, o(n, r);
                    case "topSelectionChange":
                        if (h) break;
                    case "topKeyDown":
                    case "topKeyUp":
                        return o(n, r)
                }
                return null
            },
            didPutListener: function (t, e, n) { "onSelect" === e && (b = !0) }
        };
    t.exports = x
},
function (t, e, n) {
    "use strict";

    function r(t) { return "." + t._rootNodeID }

    function o(t) { return "button" === t || "input" === t || "select" === t || "textarea" === t }
    var i = n(3),
        a = n(67),
        u = n(26),
        s = n(5),
        c = n(255),
        l = n(256),
        f = n(10),
        p = n(259),
        h = n(261),
        d = n(35),
        v = n(258),
        g = n(262),
        y = n(263),
        m = n(28),
        b = n(264),
        x = n(7),
        w = n(56),
        _ = (n(1), {}),
        C = {};
    ["abort", "animationEnd", "animationIteration", "animationStart", "blur", "canPlay", "canPlayThrough", "click", "contextMenu", "copy", "cut", "doubleClick", "drag", "dragEnd", "dragEnter", "dragExit", "dragLeave", "dragOver", "dragStart", "drop", "durationChange", "emptied", "encrypted", "ended", "error", "focus", "input", "invalid", "keyDown", "keyPress", "keyUp", "load", "loadedData", "loadedMetadata", "loadStart", "mouseDown", "mouseMove", "mouseOut", "mouseOver", "mouseUp", "paste", "pause", "play", "playing", "progress", "rateChange", "reset", "scroll", "seeked", "seeking", "stalled", "submit", "suspend", "timeUpdate", "touchCancel", "touchEnd", "touchMove", "touchStart", "transitionEnd", "volumeChange", "waiting", "wheel"].forEach(function (t) {
        var e = t[0].toUpperCase() + t.slice(1),
            n = "on" + e,
            r = "top" + e,
            o = { phasedRegistrationNames: { bubbled: n, captured: n + "Capture" }, dependencies: [r] };
        _[t] = o, C[r] = o
    });
    var E = {},
        M = {
            eventTypes: _,
            extractEvents: function (t, e, n, r) {
                var o = C[t];
                if (!o) return null;
                var a;
                switch (t) {
                    case "topAbort":
                    case "topCanPlay":
                    case "topCanPlayThrough":
                    case "topDurationChange":
                    case "topEmptied":
                    case "topEncrypted":
                    case "topEnded":
                    case "topError":
                    case "topInput":
                    case "topInvalid":
                    case "topLoad":
                    case "topLoadedData":
                    case "topLoadedMetadata":
                    case "topLoadStart":
                    case "topPause":
                    case "topPlay":
                    case "topPlaying":
                    case "topProgress":
                    case "topRateChange":
                    case "topReset":
                    case "topSeeked":
                    case "topSeeking":
                    case "topStalled":
                    case "topSubmit":
                    case "topSuspend":
                    case "topTimeUpdate":
                    case "topVolumeChange":
                    case "topWaiting":
                        a = f;
                        break;
                    case "topKeyPress":
                        if (0 === w(n)) return null;
                    case "topKeyDown":
                    case "topKeyUp":
                        a = h;
                        break;
                    case "topBlur":
                    case "topFocus":
                        a = p;
                        break;
                    case "topClick":
                        if (2 === n.button) return null;
                    case "topDoubleClick":
                    case "topMouseDown":
                    case "topMouseMove":
                    case "topMouseUp":
                    case "topMouseOut":
                    case "topMouseOver":
                    case "topContextMenu":
                        a = d;
                        break;
                    case "topDrag":
                    case "topDragEnd":
                    case "topDragEnter":
                    case "topDragExit":
                    case "topDragLeave":
                    case "topDragOver":
                    case "topDragStart":
                    case "topDrop":
                        a = v;
                        break;
                    case "topTouchCancel":
                    case "topTouchEnd":
                    case "topTouchMove":
                    case "topTouchStart":
                        a = g;
                        break;
                    case "topAnimationEnd":
                    case "topAnimationIteration":
                    case "topAnimationStart":
                        a = c;
                        break;
                    case "topTransitionEnd":
                        a = y;
                        break;
                    case "topScroll":
                        a = m;
                        break;
                    case "topWheel":
                        a = b;
                        break;
                    case "topCopy":
                    case "topCut":
                    case "topPaste":
                        a = l
                }
                a ? void 0 : i("86", t);
                var s = a.getPooled(o, e, n, r);
                return u.accumulateTwoPhaseDispatches(s), s
            },
            didPutListener: function (t, e, n) {
                if ("onClick" === e && !o(t._tag)) {
                    var i = r(t),
                        u = s.getNodeFromInstance(t);
                    E[i] || (E[i] = a.listen(u, "click", x))
                }
            },
            willDeleteListener: function (t, e) {
                if ("onClick" === e && !o(t._tag)) {
                    var n = r(t);
                    E[n].remove(), delete E[n]
                }
            }
        };
    t.exports = M
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = { animationName: null, elapsedTime: null, pseudoElement: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = { clipboardData: function (t) { return "clipboardData" in t ? t.clipboardData : window.clipboardData } };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = { data: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(35),
        i = { dataTransfer: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(28),
        i = { relatedTarget: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = { data: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(28),
        i = n(56),
        a = n(269),
        u = n(57),
        s = { key: a, location: null, ctrlKey: null, shiftKey: null, altKey: null, metaKey: null, repeat: null, locale: null, getModifierState: u, charCode: function (t) { return "keypress" === t.type ? i(t) : 0 }, keyCode: function (t) { return "keydown" === t.type || "keyup" === t.type ? t.keyCode : 0 }, which: function (t) { return "keypress" === t.type ? i(t) : "keydown" === t.type || "keyup" === t.type ? t.keyCode : 0 } };
    o.augmentClass(r, s), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(28),
        i = n(57),
        a = { touches: null, targetTouches: null, changedTouches: null, altKey: null, metaKey: null, ctrlKey: null, shiftKey: null, getModifierState: i };
    o.augmentClass(r, a), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(10),
        i = { propertyName: null, elapsedTime: null, pseudoElement: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e, n, r) { return o.call(this, t, e, n, r) }
    var o = n(35),
        i = { deltaX: function (t) { return "deltaX" in t ? t.deltaX : "wheelDeltaX" in t ? -t.wheelDeltaX : 0 }, deltaY: function (t) { return "deltaY" in t ? t.deltaY : "wheelDeltaY" in t ? -t.wheelDeltaY : "wheelDelta" in t ? -t.wheelDelta : 0 }, deltaZ: null, deltaMode: null };
    o.augmentClass(r, i), t.exports = r
},
function (t, e) {
    "use strict";

    function n(t) {
        for (var e = 1, n = 0, o = 0, i = t.length, a = i & -4; o < a;) {
            for (var u = Math.min(o + 4096, a); o < u; o += 4) n += (e += t.charCodeAt(o)) + (e += t.charCodeAt(o + 1)) + (e += t.charCodeAt(o + 2)) + (e += t.charCodeAt(o + 3));
            e %= r, n %= r
        }
        for (; o < i; o++) n += e += t.charCodeAt(o);
        return e %= r, n %= r, e | n << 16
    }
    var r = 65521;
    t.exports = n
},
function (t, e, n) {
    "use strict";

    function r(t, e, n) { var r = null == e || "boolean" == typeof e || "" === e; if (r) return ""; var o = isNaN(e); if (o || 0 === e || i.hasOwnProperty(t) && i[t]) return "" + e; if ("string" == typeof e) { e = e.trim() } return e + "px" }
    var o = n(90),
        i = (n(2), o.isUnitlessNumber);
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t) { if (null == t) return null; if (1 === t.nodeType) return t; var e = a.get(t); return e ? (e = u(e), e ? i.getNodeFromInstance(e) : null) : void ("function" == typeof t.render ? o("44") : o("45", Object.keys(t))) }
    var o = n(3),
        i = (n(11), n(5)),
        a = n(27),
        u = n(104);
    n(1), n(2);
    t.exports = r
},
function (t, e, n) {
    (function (e) {
        "use strict";

        function r(t, e, n, r) {
            if (t && "object" == typeof t) {
                var o = t,
                    i = void 0 === o[n];
                i && null != e && (o[n] = e)
            }
        }

        function o(t, e) { if (null == t) return t; var n = {}; return i(t, r, n), n }
        var i = (n(50), n(109));
        n(2);
        t.exports = o
    }).call(e, n(88))
},
function (t, e, n) {
    "use strict";

    function r(t) {
        if (t.key) {
            var e = i[t.key] || t.key;
            if ("Unidentified" !== e) return e
        }
        if ("keypress" === t.type) { var n = o(t); return 13 === n ? "Enter" : String.fromCharCode(n) }
        return "keydown" === t.type || "keyup" === t.type ? a[t.keyCode] || "Unidentified" : ""
    }
    var o = n(56),
        i = { Esc: "Escape", Spacebar: " ", Left: "ArrowLeft", Up: "ArrowUp", Right: "ArrowRight", Down: "ArrowDown", Del: "Delete", Win: "OS", Menu: "ContextMenu", Apps: "ContextMenu", Scroll: "ScrollLock", MozPrintableKey: "Unidentified" },
        a = { 8: "Backspace", 9: "Tab", 12: "Clear", 13: "Enter", 16: "Shift", 17: "Control", 18: "Alt", 19: "Pause", 20: "CapsLock", 27: "Escape", 32: " ", 33: "PageUp", 34: "PageDown", 35: "End", 36: "Home", 37: "ArrowLeft", 38: "ArrowUp", 39: "ArrowRight", 40: "ArrowDown", 45: "Insert", 46: "Delete", 112: "F1", 113: "F2", 114: "F3", 115: "F4", 116: "F5", 117: "F6", 118: "F7", 119: "F8", 120: "F9", 121: "F10", 122: "F11", 123: "F12", 144: "NumLock", 145: "ScrollLock", 224: "Meta" };
    t.exports = r
},
    114,
function (t, e) {
    "use strict";

    function n() { return r++ }
    var r = 1;
    t.exports = n
},
function (t, e) {
    "use strict";

    function n(t) { for (; t && t.firstChild;) t = t.firstChild; return t }

    function r(t) {
        for (; t;) {
            if (t.nextSibling) return t.nextSibling;
            t = t.parentNode
        }
    }

    function o(t, e) {
        for (var o = n(t), i = 0, a = 0; o;) {
            if (3 === o.nodeType) {
                if (a = i + o.textContent.length, i <= e && a >= e) return { node: o, offset: e - i };
                i = a
            }
            o = n(r(o))
        }
    }
    t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t, e) { var n = {}; return n[t.toLowerCase()] = e.toLowerCase(), n["Webkit" + t] = "webkit" + e, n["Moz" + t] = "moz" + e, n["ms" + t] = "MS" + e, n["O" + t] = "o" + e.toLowerCase(), n }

    function o(t) {
        if (u[t]) return u[t];
        if (!a[t]) return t;
        var e = a[t];
        for (var n in e)
            if (e.hasOwnProperty(n) && n in s) return u[t] = e[n];
        return ""
    }
    var i = n(6),
        a = { animationend: r("Animation", "AnimationEnd"), animationiteration: r("Animation", "AnimationIteration"), animationstart: r("Animation", "AnimationStart"), transitionend: r("Transition", "TransitionEnd") },
        u = {},
        s = {};
    i.canUseDOM && (s = document.createElement("div").style, "AnimationEvent" in window || (delete a.animationend.animation, delete a.animationiteration.animation, delete a.animationstart.animation), "TransitionEvent" in window || delete a.transitionend.transition), t.exports = o
},
function (t, e, n) {
    "use strict";

    function r(t) { return '"' + o(t) + '"' }
    var o = n(37);
    t.exports = r
},
function (t, e, n) {
    "use strict";
    var r = n(99);
    t.exports = r.renderSubtreeIntoContainer
},
function (t, e, n) { "undefined" == typeof Promise && (n(211).enable(), window.Promise = n(210)), n(290), Object.assign = n(277) },
function (t, e) {
    "use strict";

    function n(t) { if (null === t || void 0 === t) throw new TypeError("Object.assign cannot be called with null or undefined"); return Object(t) }

    function r() { try { if (!Object.assign) return !1; var t = new String("abc"); if (t[5] = "de", "5" === Object.getOwnPropertyNames(t)[0]) return !1; for (var e = {}, n = 0; n < 10; n++) e["_" + String.fromCharCode(n)] = n; var r = Object.getOwnPropertyNames(e).map(function (t) { return e[t] }); if ("0123456789" !== r.join("")) return !1; var o = {}; return "abcdefghijklmnopqrst".split("").forEach(function (t) { o[t] = t }), "abcdefghijklmnopqrst" === Object.keys(Object.assign({}, o)).join("") } catch (t) { return !1 } }
    var o = Object.prototype.hasOwnProperty,
        i = Object.prototype.propertyIsEnumerable;
    t.exports = r() ? Object.assign : function (t, e) { for (var r, a, u = n(t), s = 1; s < arguments.length; s++) { r = Object(arguments[s]); for (var c in r) o.call(r, c) && (u[c] = r[c]); if (Object.getOwnPropertySymbols) { a = Object.getOwnPropertySymbols(r); for (var l = 0; l < a.length; l++) i.call(r, a[l]) && (u[a[l]] = r[a[l]]) } } return u }
},
    50, [291, 22],
function (t, e, n) {
    "use strict";

    function r(t) { return ("" + t).replace(x, "$&/") }

    function o(t, e) { this.func = t, this.context = e, this.count = 0 }

    function i(t, e, n) {
        var r = t.func,
            o = t.context;
        r.call(o, e, t.count++)
    }

    function a(t, e, n) {
        if (null == t) return t;
        var r = o.getPooled(e, n);
        y(t, i, r), o.release(r)
    }

    function u(t, e, n, r) { this.result = t, this.keyPrefix = e, this.func = n, this.context = r, this.count = 0 }

    function s(t, e, n) {
        var o = t.result,
            i = t.keyPrefix,
            a = t.func,
            u = t.context,
            s = a.call(u, e, t.count++);
        Array.isArray(s) ? c(s, o, n, g.thatReturnsArgument) : null != s && (v.isValidElement(s) && (s = v.cloneAndReplaceKey(s, i + (!s.key || e && e.key === s.key ? "" : r(s.key) + "/") + n)), o.push(s))
    }

    function c(t, e, n, o, i) {
        var a = "";
        null != n && (a = r(n) + "/");
        var c = u.getPooled(e, a, o, i);
        y(t, s, c), u.release(c)
    }

    function l(t, e, n) { if (null == t) return t; var r = []; return c(t, r, null, e, n), r }

    function f(t, e, n) { return null }

    function p(t, e) { return y(t, f, null) }

    function h(t) { var e = []; return c(t, e, null, g.thatReturnsArgument), e }
    var d = n(279),
        v = n(21),
        g = n(7),
        y = n(288),
        m = d.twoArgumentPooler,
        b = d.fourArgumentPooler,
        x = /\/+/g;
    o.prototype.destructor = function () { this.func = null, this.context = null, this.count = 0 }, d.addPoolingTo(o, m), u.prototype.destructor = function () { this.result = null, this.keyPrefix = null, this.func = null, this.context = null, this.count = 0 }, d.addPoolingTo(u, b);
    var w = { forEach: a, map: l, mapIntoWithKeyPrefixInternal: c, count: p, toArray: h };
    t.exports = w
},
function (t, e, n) {
    "use strict";

    function r(t) { return t }

    function o(t, e) {
        var n = x.hasOwnProperty(e) ? x[e] : null;
        _.hasOwnProperty(e) && ("OVERRIDE_BASE" !== n ? p("73", e) : void 0), t && ("DEFINE_MANY" !== n && "DEFINE_MANY_MERGED" !== n ? p("74", e) : void 0)
    }

    function i(t, e) {
        if (e) {
            "function" == typeof e ? p("75") : void 0, v.isValidElement(e) ? p("76") : void 0;
            var n = t.prototype,
                r = n.__reactAutoBindPairs;
            e.hasOwnProperty(m) && w.mixins(t, e.mixins);
            for (var i in e)
                if (e.hasOwnProperty(i) && i !== m) {
                    var a = e[i],
                        u = n.hasOwnProperty(i);
                    if (o(u, i), w.hasOwnProperty(i)) w[i](t, a);
                    else {
                        var l = x.hasOwnProperty(i),
                            f = "function" == typeof a,
                            h = f && !l && !u && e.autobind !== !1;
                        if (h) r.push(i, a), n[i] = a;
                        else if (u) { var d = x[i]; !l || "DEFINE_MANY_MERGED" !== d && "DEFINE_MANY" !== d ? p("77", d, i) : void 0, "DEFINE_MANY_MERGED" === d ? n[i] = s(n[i], a) : "DEFINE_MANY" === d && (n[i] = c(n[i], a)) } else n[i] = a
                    }
                }
        } else;
    }

    function a(t, e) {
        if (e)
            for (var n in e) {
                var r = e[n];
                if (e.hasOwnProperty(n)) {
                    var o = n in w;
                    o ? p("78", n) : void 0;
                    var i = n in t;
                    i ? p("79", n) : void 0, t[n] = r
                }
            }
    }

    function u(t, e) { t && e && "object" == typeof t && "object" == typeof e ? void 0 : p("80"); for (var n in e) e.hasOwnProperty(n) && (void 0 !== t[n] ? p("81", n) : void 0, t[n] = e[n]); return t }

    function s(t, e) {
        return function () {
            var n = t.apply(this, arguments),
                r = e.apply(this, arguments);
            if (null == n) return r;
            if (null == r) return n;
            var o = {};
            return u(o, n), u(o, r), o
        }
    }

    function c(t, e) { return function () { t.apply(this, arguments), e.apply(this, arguments) } }

    function l(t, e) { var n = e.bind(t); return n }

    function f(t) {
        for (var e = t.__reactAutoBindPairs, n = 0; n < e.length; n += 2) {
            var r = e[n],
                o = e[n + 1];
            t[r] = l(t, o)
        }
    }
    var p = n(22),
        h = n(4),
        d = n(62),
        v = n(21),
        g = (n(112), n(63)),
        y = n(24),
        m = (n(1), n(2), "mixins"),
        b = [],
        x = { mixins: "DEFINE_MANY", statics: "DEFINE_MANY", propTypes: "DEFINE_MANY", contextTypes: "DEFINE_MANY", childContextTypes: "DEFINE_MANY", getDefaultProps: "DEFINE_MANY_MERGED", getInitialState: "DEFINE_MANY_MERGED", getChildContext: "DEFINE_MANY_MERGED", render: "DEFINE_ONCE", componentWillMount: "DEFINE_MANY", componentDidMount: "DEFINE_MANY", componentWillReceiveProps: "DEFINE_MANY", shouldComponentUpdate: "DEFINE_ONCE", componentWillUpdate: "DEFINE_MANY", componentDidUpdate: "DEFINE_MANY", componentWillUnmount: "DEFINE_MANY", updateComponent: "OVERRIDE_BASE" },
        w = {
            displayName: function (t, e) { t.displayName = e },
            mixins: function (t, e) {
                if (e)
                    for (var n = 0; n < e.length; n++) i(t, e[n])
            },
            childContextTypes: function (t, e) { t.childContextTypes = h({}, t.childContextTypes, e) },
            contextTypes: function (t, e) { t.contextTypes = h({}, t.contextTypes, e) },
            getDefaultProps: function (t, e) { t.getDefaultProps ? t.getDefaultProps = s(t.getDefaultProps, e) : t.getDefaultProps = e },
            propTypes: function (t, e) { t.propTypes = h({}, t.propTypes, e) },
            statics: function (t, e) { a(t, e) },
            autobind: function () { }
        },
        _ = { replaceState: function (t, e) { this.updater.enqueueReplaceState(this, t), e && this.updater.enqueueCallback(this, e, "replaceState") }, isMounted: function () { return this.updater.isMounted(this) } },
        C = function () { };
    h(C.prototype, d.prototype, _);
    var E = {
        createClass: function (t) {
            var e = r(function (t, n, r) { this.__reactAutoBindPairs.length && f(this), this.props = t, this.context = n, this.refs = y, this.updater = r || g, this.state = null; var o = this.getInitialState ? this.getInitialState() : null; "object" != typeof o || Array.isArray(o) ? p("82", e.displayName || "ReactCompositeComponent") : void 0, this.state = o });
            e.prototype = new C, e.prototype.constructor = e, e.prototype.__reactAutoBindPairs = [], b.forEach(i.bind(null, e)), i(e, t), e.getDefaultProps && (e.defaultProps = e.getDefaultProps()), e.prototype.render ? void 0 : p("83");
            for (var n in x) e.prototype[n] || (e.prototype[n] = null);
            return e
        },
        injection: { injectMixin: function (t) { b.push(t) } }
    };
    t.exports = E
},
function (t, e, n) {
    "use strict";
    var r = n(21),
        o = r.createFactory,
        i = { a: o("a"), abbr: o("abbr"), address: o("address"), area: o("area"), article: o("article"), aside: o("aside"), audio: o("audio"), b: o("b"), base: o("base"), bdi: o("bdi"), bdo: o("bdo"), big: o("big"), blockquote: o("blockquote"), body: o("body"), br: o("br"), button: o("button"), canvas: o("canvas"), caption: o("caption"), cite: o("cite"), code: o("code"), col: o("col"), colgroup: o("colgroup"), data: o("data"), datalist: o("datalist"), dd: o("dd"), del: o("del"), details: o("details"), dfn: o("dfn"), dialog: o("dialog"), div: o("div"), dl: o("dl"), dt: o("dt"), em: o("em"), embed: o("embed"), fieldset: o("fieldset"), figcaption: o("figcaption"), figure: o("figure"), footer: o("footer"), form: o("form"), h1: o("h1"), h2: o("h2"), h3: o("h3"), h4: o("h4"), h5: o("h5"), h6: o("h6"), head: o("head"), header: o("header"), hgroup: o("hgroup"), hr: o("hr"), html: o("html"), i: o("i"), iframe: o("iframe"), img: o("img"), input: o("input"), ins: o("ins"), kbd: o("kbd"), keygen: o("keygen"), label: o("label"), legend: o("legend"), li: o("li"), link: o("link"), main: o("main"), map: o("map"), mark: o("mark"), menu: o("menu"), menuitem: o("menuitem"), meta: o("meta"), meter: o("meter"), nav: o("nav"), noscript: o("noscript"), object: o("object"), ol: o("ol"), optgroup: o("optgroup"), option: o("option"), output: o("output"), p: o("p"), param: o("param"), picture: o("picture"), pre: o("pre"), progress: o("progress"), q: o("q"), rp: o("rp"), rt: o("rt"), ruby: o("ruby"), s: o("s"), samp: o("samp"), script: o("script"), section: o("section"), select: o("select"), small: o("small"), source: o("source"), span: o("span"), strong: o("strong"), style: o("style"), sub: o("sub"), summary: o("summary"), sup: o("sup"), table: o("table"), tbody: o("tbody"), td: o("td"), textarea: o("textarea"), tfoot: o("tfoot"), th: o("th"), thead: o("thead"), time: o("time"), title: o("title"), tr: o("tr"), track: o("track"), u: o("u"), ul: o("ul"), var: o("var"), video: o("video"), wbr: o("wbr"), circle: o("circle"), clipPath: o("clipPath"), defs: o("defs"), ellipse: o("ellipse"), g: o("g"), image: o("image"), line: o("line"), linearGradient: o("linearGradient"), mask: o("mask"), path: o("path"), pattern: o("pattern"), polygon: o("polygon"), polyline: o("polyline"), radialGradient: o("radialGradient"), rect: o("rect"), stop: o("stop"), svg: o("svg"), text: o("text"), tspan: o("tspan") };
    t.exports = i
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return t === e ? 0 !== t || 1 / t === 1 / e : t !== t && e !== e }

    function o(t) { this.message = t, this.stack = "" }

    function i(t) {
        function e(e, n, r, i, a, u, s) { i = i || k, u = u || r; if (null == n[r]) { var c = _[a]; return e ? new o(null === n[r] ? "The " + c + " `" + u + "` is marked as required " + ("in `" + i + "`, but its value is `null`.") : "The " + c + " `" + u + "` is marked as required in " + ("`" + i + "`, but its value is `undefined`.")) : null } return t(n, r, i, a, u) }
        var n = e.bind(null, !1);
        return n.isRequired = e.bind(null, !0), n
    }

    function a(t) {
        function e(e, n, r, i, a, u) {
            var s = e[n],
                c = m(s);
            if (c !== t) {
                var l = _[i],
                    f = b(s);
                return new o("Invalid " + l + " `" + a + "` of type " + ("`" + f + "` supplied to `" + r + "`, expected ") + ("`" + t + "`."))
            }
            return null
        }
        return i(e)
    }

    function u() { return i(E.thatReturns(null)) }

    function s(t) {
        function e(e, n, r, i, a) {
            if ("function" != typeof t) return new o("Property `" + a + "` of component `" + r + "` has invalid PropType notation inside arrayOf.");
            var u = e[n];
            if (!Array.isArray(u)) {
                var s = _[i],
                    c = m(u);
                return new o("Invalid " + s + " `" + a + "` of type " + ("`" + c + "` supplied to `" + r + "`, expected an array."))
            }
            for (var l = 0; l < u.length; l++) { var f = t(u, l, r, i, a + "[" + l + "]", C); if (f instanceof Error) return f }
            return null
        }
        return i(e)
    }

    function c() {
        function t(t, e, n, r, i) {
            var a = t[e];
            if (!w.isValidElement(a)) {
                var u = _[r],
                    s = m(a);
                return new o("Invalid " + u + " `" + i + "` of type " + ("`" + s + "` supplied to `" + n + "`, expected a single ReactElement."))
            }
            return null
        }
        return i(t)
    }

    function l(t) {
        function e(e, n, r, i, a) {
            if (!(e[n] instanceof t)) {
                var u = _[i],
                    s = t.name || k,
                    c = x(e[n]);
                return new o("Invalid " + u + " `" + a + "` of type " + ("`" + c + "` supplied to `" + r + "`, expected ") + ("instance of `" + s + "`."))
            }
            return null
        }
        return i(e)
    }

    function f(t) {
        function e(e, n, i, a, u) {
            for (var s = e[n], c = 0; c < t.length; c++)
                if (r(s, t[c])) return null;
            var l = _[a],
                f = JSON.stringify(t);
            return new o("Invalid " + l + " `" + u + "` of value `" + s + "` " + ("supplied to `" + i + "`, expected one of " + f + "."))
        }
        return Array.isArray(t) ? i(e) : E.thatReturnsNull
    }

    function p(t) {
        function e(e, n, r, i, a) {
            if ("function" != typeof t) return new o("Property `" + a + "` of component `" + r + "` has invalid PropType notation inside objectOf.");
            var u = e[n],
                s = m(u);
            if ("object" !== s) { var c = _[i]; return new o("Invalid " + c + " `" + a + "` of type " + ("`" + s + "` supplied to `" + r + "`, expected an object.")) }
            for (var l in u)
                if (u.hasOwnProperty(l)) { var f = t(u, l, r, i, a + "." + l, C); if (f instanceof Error) return f }
            return null
        }
        return i(e)
    }

    function h(t) {
        function e(e, n, r, i, a) { for (var u = 0; u < t.length; u++) { var s = t[u]; if (null == s(e, n, r, i, a, C)) return null } var c = _[i]; return new o("Invalid " + c + " `" + a + "` supplied to " + ("`" + r + "`.")) }
        return Array.isArray(t) ? i(e) : E.thatReturnsNull
    }

    function d() {
        function t(t, e, n, r, i) { if (!g(t[e])) { var a = _[r]; return new o("Invalid " + a + " `" + i + "` supplied to " + ("`" + n + "`, expected a ReactNode.")) } return null }
        return i(t)
    }

    function v(t) {
        function e(e, n, r, i, a) {
            var u = e[n],
                s = m(u);
            if ("object" !== s) { var c = _[i]; return new o("Invalid " + c + " `" + a + "` of type `" + s + "` " + ("supplied to `" + r + "`, expected `object`.")) }
            for (var l in t) { var f = t[l]; if (f) { var p = f(u, l, r, i, a + "." + l, C); if (p) return p } }
            return null
        }
        return i(e)
    }

    function g(t) {
        switch (typeof t) {
            case "number":
            case "string":
            case "undefined":
                return !0;
            case "boolean":
                return !t;
            case "object":
                if (Array.isArray(t)) return t.every(g);
                if (null === t || w.isValidElement(t)) return !0;
                var e = M(t);
                if (!e) return !1;
                var n, r = e.call(t);
                if (e !== t.entries) {
                    for (; !(n = r.next()).done;)
                        if (!g(n.value)) return !1
                } else
                    for (; !(n = r.next()).done;) { var o = n.value; if (o && !g(o[1])) return !1 }
                return !0;
            default:
                return !1
        }
    }

    function y(t, e) { return "symbol" === t || ("Symbol" === e["@@toStringTag"] || "function" == typeof Symbol && e instanceof Symbol) }

    function m(t) { var e = typeof t; return Array.isArray(t) ? "array" : t instanceof RegExp ? "object" : y(e, t) ? "symbol" : e }

    function b(t) { var e = m(t); if ("object" === e) { if (t instanceof Date) return "date"; if (t instanceof RegExp) return "regexp" } return e }

    function x(t) { return t.constructor && t.constructor.name ? t.constructor.name : k }
    var w = n(21),
        _ = n(112),
        C = n(284),
        E = n(7),
        M = n(114),
        k = (n(2), "<<anonymous>>"),
        T = { array: a("array"), bool: a("boolean"), func: a("function"), number: a("number"), object: a("object"), string: a("string"), symbol: a("symbol"), any: u(), arrayOf: s, element: c(), instanceOf: l, node: d(), objectOf: p, oneOf: f, oneOfType: h, shape: v };
    o.prototype = Error.prototype, t.exports = T
},
    246,
function (t, e, n) {
    "use strict";

    function r(t, e, n) { this.props = t, this.context = e, this.refs = s, this.updater = n || u }

    function o() { }
    var i = n(4),
        a = n(62),
        u = n(63),
        s = n(24);
    o.prototype = a.prototype, r.prototype = new o, r.prototype.constructor = r, i(r.prototype, a.prototype), r.prototype.isPureReactComponent = !0, t.exports = r
},
    251,
function (t, e, n) {
    "use strict";

    function r(t) { return i.isValidElement(t) ? void 0 : o("143"), t }
    var o = n(22),
        i = n(21);
    n(1);
    t.exports = r
},
function (t, e, n) {
    "use strict";

    function r(t, e) { return t && "object" == typeof t && null != t.key ? c.escape(t.key) : e.toString(36) }

    function o(t, e, n, i) {
        var p = typeof t;
        if ("undefined" !== p && "boolean" !== p || (t = null), null === t || "string" === p || "number" === p || "object" === p && t.$$typeof === u) return n(i, t, "" === e ? l + r(t, 0) : e), 1;
        var h, d, v = 0,
            g = "" === e ? l : e + f;
        if (Array.isArray(t))
            for (var y = 0; y < t.length; y++) h = t[y], d = g + r(h, y), v += o(h, d, n, i);
        else {
            var m = s(t);
            if (m) {
                var b, x = m.call(t);
                if (m !== t.entries)
                    for (var w = 0; !(b = x.next()).done;) h = b.value, d = g + r(h, w++), v += o(h, d, n, i);
                else
                    for (; !(b = x.next()).done;) {
                        var _ = b.value;
                        _ && (h = _[1], d = g + c.escape(_[0]) + f + r(h, 0), v += o(h, d, n, i))
                    }
            } else if ("object" === p) {
                var C = "",
                    E = String(t);
                a("31", "[object Object]" === E ? "object with keys {" + Object.keys(t).join(", ") + "}" : E, C)
            }
        }
        return v
    }

    function i(t, e, n) { return null == t ? 0 : o(t, "", e, n) }
    var a = n(22),
        u = (n(11), n(111)),
        s = n(114),
        c = (n(1), n(278)),
        l = (n(2), "."),
        f = ":";
    t.exports = i
},
function (t, e) { t.exports = function (t) { return t.webpackPolyfill || (t.deprecate = function () { }, t.paths = [], t.children = [], t.webpackPolyfill = 1), t } },
function (t, e) {
    ! function (t) {
        "use strict";

        function e(t) { if ("string" != typeof t && (t = String(t)), /[^a-z0-9\-#$%&'*+.\^_`|~]/i.test(t)) throw new TypeError("Invalid character in header field name"); return t.toLowerCase() }

        function n(t) { return "string" != typeof t && (t = String(t)), t }

        function r(t) { var e = { next: function () { var e = t.shift(); return { done: void 0 === e, value: e } } }; return v.iterable && (e[Symbol.iterator] = function () { return e }), e }

        function o(t) { this.map = {}, t instanceof o ? t.forEach(function (t, e) { this.append(e, t) }, this) : t && Object.getOwnPropertyNames(t).forEach(function (e) { this.append(e, t[e]) }, this) }

        function i(t) { return t.bodyUsed ? Promise.reject(new TypeError("Already read")) : void (t.bodyUsed = !0) }

        function a(t) { return new Promise(function (e, n) { t.onload = function () { e(t.result) }, t.onerror = function () { n(t.error) } }) }

        function u(t) { var e = new FileReader; return e.readAsArrayBuffer(t), a(e) }

        function s(t) { var e = new FileReader; return e.readAsText(t), a(e) }

        function c() {
            return this.bodyUsed = !1, this._initBody = function (t) {
                if (this._bodyInit = t, "string" == typeof t) this._bodyText = t;
                else if (v.blob && Blob.prototype.isPrototypeOf(t)) this._bodyBlob = t;
                else if (v.formData && FormData.prototype.isPrototypeOf(t)) this._bodyFormData = t;
                else if (v.searchParams && URLSearchParams.prototype.isPrototypeOf(t)) this._bodyText = t.toString();
                else if (t) { if (!v.arrayBuffer || !ArrayBuffer.prototype.isPrototypeOf(t)) throw new Error("unsupported BodyInit type") } else this._bodyText = "";
                this.headers.get("content-type") || ("string" == typeof t ? this.headers.set("content-type", "text/plain;charset=UTF-8") : this._bodyBlob && this._bodyBlob.type ? this.headers.set("content-type", this._bodyBlob.type) : v.searchParams && URLSearchParams.prototype.isPrototypeOf(t) && this.headers.set("content-type", "application/x-www-form-urlencoded;charset=UTF-8"))
            }, v.blob ? (this.blob = function () { var t = i(this); if (t) return t; if (this._bodyBlob) return Promise.resolve(this._bodyBlob); if (this._bodyFormData) throw new Error("could not read FormData body as blob"); return Promise.resolve(new Blob([this._bodyText])) }, this.arrayBuffer = function () { return this.blob().then(u) }, this.text = function () { var t = i(this); if (t) return t; if (this._bodyBlob) return s(this._bodyBlob); if (this._bodyFormData) throw new Error("could not read FormData body as text"); return Promise.resolve(this._bodyText) }) : this.text = function () { var t = i(this); return t ? t : Promise.resolve(this._bodyText) }, v.formData && (this.formData = function () { return this.text().then(p) }), this.json = function () { return this.text().then(JSON.parse) }, this
        }

        function l(t) { var e = t.toUpperCase(); return g.indexOf(e) > -1 ? e : t }

        function f(t, e) {
            e = e || {};
            var n = e.body;
            if (f.prototype.isPrototypeOf(t)) {
                if (t.bodyUsed) throw new TypeError("Already read");
                this.url = t.url, this.credentials = t.credentials, e.headers || (this.headers = new o(t.headers)), this.method = t.method, this.mode = t.mode, n || (n = t._bodyInit, t.bodyUsed = !0)
            } else this.url = t;
            if (this.credentials = e.credentials || this.credentials || "omit", !e.headers && this.headers || (this.headers = new o(e.headers)), this.method = l(e.method || this.method || "GET"), this.mode = e.mode || this.mode || null, this.referrer = null, ("GET" === this.method || "HEAD" === this.method) && n) throw new TypeError("Body not allowed for GET or HEAD requests");
            this._initBody(n)
        }

        function p(t) {
            var e = new FormData;
            return t.trim().split("&").forEach(function (t) {
                if (t) {
                    var n = t.split("="),
                        r = n.shift().replace(/\+/g, " "),
                        o = n.join("=").replace(/\+/g, " ");
                    e.append(decodeURIComponent(r), decodeURIComponent(o))
                }
            }), e
        }

        function h(t) {
            var e = new o,
                n = (t.getAllResponseHeaders() || "").trim().split("\n");
            return n.forEach(function (t) {
                var n = t.trim().split(":"),
                    r = n.shift().trim(),
                    o = n.join(":").trim();
                e.append(r, o)
            }), e
        }

        function d(t, e) { e || (e = {}), this.type = "default", this.status = e.status, this.ok = this.status >= 200 && this.status < 300, this.statusText = e.statusText, this.headers = e.headers instanceof o ? e.headers : new o(e.headers), this.url = e.url || "", this._initBody(t) }
        if (!t.fetch) {
            var v = { searchParams: "URLSearchParams" in t, iterable: "Symbol" in t && "iterator" in Symbol, blob: "FileReader" in t && "Blob" in t && function () { try { return new Blob, !0 } catch (t) { return !1 } }(), formData: "FormData" in t, arrayBuffer: "ArrayBuffer" in t };
            o.prototype.append = function (t, r) {
                t = e(t), r = n(r);
                var o = this.map[t];
                o || (o = [], this.map[t] = o), o.push(r)
            }, o.prototype.delete = function (t) { delete this.map[e(t)] }, o.prototype.get = function (t) { var n = this.map[e(t)]; return n ? n[0] : null }, o.prototype.getAll = function (t) { return this.map[e(t)] || [] }, o.prototype.has = function (t) { return this.map.hasOwnProperty(e(t)) }, o.prototype.set = function (t, r) { this.map[e(t)] = [n(r)] }, o.prototype.forEach = function (t, e) { Object.getOwnPropertyNames(this.map).forEach(function (n) { this.map[n].forEach(function (r) { t.call(e, r, n, this) }, this) }, this) }, o.prototype.keys = function () { var t = []; return this.forEach(function (e, n) { t.push(n) }), r(t) }, o.prototype.values = function () { var t = []; return this.forEach(function (e) { t.push(e) }), r(t) }, o.prototype.entries = function () { var t = []; return this.forEach(function (e, n) { t.push([n, e]) }), r(t) }, v.iterable && (o.prototype[Symbol.iterator] = o.prototype.entries);
            var g = ["DELETE", "GET", "HEAD", "OPTIONS", "POST", "PUT"];
            f.prototype.clone = function () { return new f(this) }, c.call(f.prototype), c.call(d.prototype), d.prototype.clone = function () { return new d(this._bodyInit, { status: this.status, statusText: this.statusText, headers: new o(this.headers), url: this.url }) }, d.error = function () { var t = new d(null, { status: 0, statusText: "" }); return t.type = "error", t };
            var y = [301, 302, 303, 307, 308];
            d.redirect = function (t, e) { if (y.indexOf(e) === -1) throw new RangeError("Invalid status code"); return new d(null, { status: e, headers: { location: t } }) }, t.Headers = o, t.Request = f, t.Response = d, t.fetch = function (t, e) {
                return new Promise(function (n, r) {
                    function o() { return "responseURL" in a ? a.responseURL : /^X-Request-URL:/m.test(a.getAllResponseHeaders()) ? a.getResponseHeader("X-Request-URL") : void 0 }
                    var i;
                    i = f.prototype.isPrototypeOf(t) && !e ? t : new f(t, e);
                    var a = new XMLHttpRequest;
                    a.onload = function () {
                        var t = { status: a.status, statusText: a.statusText, headers: h(a), url: o() },
                            e = "response" in a ? a.response : a.responseText;
                        n(new d(e, t))
                    }, a.onerror = function () { r(new TypeError("Network request failed")) }, a.ontimeout = function () { r(new TypeError("Network request failed")) }, a.open(i.method, i.url, !0), "include" === i.credentials && (a.withCredentials = !0), "responseType" in a && v.blob && (a.responseType = "blob"), i.headers.forEach(function (t, e) { a.setRequestHeader(e, t) }), a.send("undefined" == typeof i._bodyInit ? null : i._bodyInit)
                })
            }, t.fetch.polyfill = !0
        }
    }("undefined" != typeof self ? self : this)
},
function (t, e, n, r) {
    "use strict";
    var o = n(r),
        i = (n(1), function (t) { var e = this; if (e.instancePool.length) { var n = e.instancePool.pop(); return e.call(n, t), n } return new e(t) }),
        a = function (t, e) { var n = this; if (n.instancePool.length) { var r = n.instancePool.pop(); return n.call(r, t, e), r } return new n(t, e) },
        u = function (t, e, n) { var r = this; if (r.instancePool.length) { var o = r.instancePool.pop(); return r.call(o, t, e, n), o } return new r(t, e, n) },
        s = function (t, e, n, r) { var o = this; if (o.instancePool.length) { var i = o.instancePool.pop(); return o.call(i, t, e, n, r), i } return new o(t, e, n, r) },
        c = function (t) {
            var e = this;
            t instanceof e ? void 0 : o("25"), t.destructor(), e.instancePool.length < e.poolSize && e.instancePool.push(t)
        },
        l = 10,
        f = i,
        p = function (t, e) { var n = t; return n.instancePool = [], n.getPooled = e || f, n.poolSize || (n.poolSize = l), n.release = c, n },
        h = { addPoolingTo: p, oneArgumentPooler: i, twoArgumentPooler: a, threeArgumentPooler: u, fourArgumentPooler: s };
    t.exports = h
}
]));
//# sourceMappingURL=main.6e6198ed.js.map